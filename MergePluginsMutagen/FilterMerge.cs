using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;

namespace MergePluginsMutagen
{
    public static class FilterMerge
    {
        public static List<ModKey> WillNotMergeCheck(string mergeName, string[] pluginNameListArr, Settings settings, out HashSet<ModKey> dontChangeFormIDs)
        {
            Console.WriteLine("Checking for Valid plugins");
            List<ModKey> pluginNameList = new();
            HashSet<string> WontMergeList = new(StringComparer.OrdinalIgnoreCase)
            {
                mergeName,
                "Skyrim.esm",
                "Update.esm",
                "Dawnguard.esm",
                "HearthFires.esm",
                "Dragonborn.esm"
            };

            foreach (string pluginName in pluginNameListArr)
            {
                if (WontMergeList.Contains(pluginName)) continue;
                if (!File.Exists(Path.Combine(settings.pDataFolder, pluginName)))
                {
                    Console.WriteLine($"{pluginName} does not exists in data folder.");
                    continue;
                }

                try
                {
                    pluginNameList.Add(ModKey.FromFileName(pluginName));
                }
                catch (ArgumentException) { Console.WriteLine(pluginName + " is not a plugin"); }
            }

            return InvalidateModsForMerge(pluginNameList, settings, out dontChangeFormIDs);
        }

        public static List<ModKey> InvalidateModsForMerge(List<ModKey> pluginNameList, Settings settings, out HashSet<ModKey> dontChangeFormIDs)
        {
            dontChangeFormIDs = new();
            Console.WriteLine("Invalidating plugins");
            List<ModKey> invalidMods = new();
            foreach (var key in pluginNameList)
            {
                bool invalid = false;
                using var mod = SkyrimMod.CreateFromBinaryOverlay(Path.Combine(settings.pDataFolder, key.FileName), SkyrimRelease.SkyrimSE);

                if (!settings.aDisableNavigationMeshInfoMapsCheck)
                {
                    if (mod.NavigationMeshInfoMaps.Any())
                    {
                        invalidMods.Add(key);
                        continue;
                    }
                }

                foreach (var cellblock in mod.Cells)
                {
                    foreach (var subBlock in cellblock.SubBlocks)
                    {
                        foreach (var cell in subBlock.Cells)
                        {
                            if (cell.FormKey.ModKey.Equals(key))
                            {
                                invalid = true;
                                break;
                            }

                            foreach (var nav in cell.NavigationMeshes)
                            {
                                if (nav.FormKey.ModKey.Equals(key))
                                {
                                    invalid = true;
                                    break;
                                }
                            }
                            if (invalid) break;
                        }
                        if (invalid) break;
                    }
                    if (invalid) break;
                }

                if (invalid)
                {
                    invalidMods.Add(key);
                    continue;
                }

                foreach (var worldspace in mod.Worldspaces)
                {
                    foreach (var block in worldspace.SubCells)
                    {
                        foreach (var subblock in block.Items)
                        {
                            foreach (var cell in subblock.Items)
                            {
                                if (cell.FormKey.ModKey.Equals(key))
                                {
                                    invalid = true;
                                    break;
                                }

                                foreach (var nav in cell.NavigationMeshes)
                                {
                                    if (nav.FormKey.ModKey.Equals(key))
                                    {
                                        invalid = true;
                                        break;
                                    }
                                }
                                if (invalid) break;
                            }
                            if (invalid) break;
                        }
                        if (invalid) break;
                    }
                    if (invalid) break;
                }

                if (invalid)
                {
                    invalidMods.Add(key);
                    continue;
                }
            }

            if (invalidMods.Count > 0)
            {
                foreach (var key in invalidMods)
                {

                    Console.WriteLine($"Invalid for merge: {key.FileName}");
                    if (settings.aDisableInvalidateJustOverrideEverything)
                    {
                        dontChangeFormIDs.Add(key);
                    }
                    else pluginNameList.Remove(key);
                }

                if (!settings.aDisableInvalidateJustOverrideEverything)
                {
                    Console.WriteLine("Those Invalid for merge contain a new Cell, Worldspace, or a Navigation Mesh Info Map record.");
                    Console.WriteLine("These are not something that are supported by this merge tool for a number of reasons.");
                    Console.WriteLine("But the major reasons are without them the game is prone to crashing.");
                    Console.WriteLine("And I do not have a reliable way of combining Navigation Mesh Info Map record.");
                    Console.WriteLine("If someone does develop a solution and wants to add it to this I will gladly except it.");
                    Console.WriteLine("Press Enter to Continue with the Merge...");
                    Console.ReadLine();
                }
            }

            return pluginNameList;
        }

    }
}
