using Mutagen.Bethesda.Plugins;
using MergePluginsMutagen.MergePluginClass;
using MergePluginsMutagen.MergeData;
using Mutagen.Bethesda.Skyrim;

namespace MergePluginsMutagen
{
    internal static class Program
    {
        internal static Settings Settings = new();

        internal static bool Pause = true;

        internal static void Main(string[] args)
        {
            args = new string[]
            {
                "C:\\Modding\\SkyrimSE\\_Notes\\Merges\\TemsPatchesMerge.esp.txt",
                "C:\\Modding\\SkyrimSE\\_Notes\\Merges\\_MergePluginsMutagen.ini"
            };
            try
            {
                if (args.Length >= 2)
                {
                    if(File.Exists(args[1]))
                    {
                        Console.WriteLine("Getting Settings from custom Location");
                        Settings = new Settings(args[1]);
                    }
                    else if(args[1].Equals("-np"))
                    {
                        Pause = false;
                    }
                    if (args.Length >= 3)
                    {
                        if (args[3].Equals("-np"))
                        {
                            Pause = false;
                        }
                    }
                }
                else Settings.GetDefaultLocation();

                string mergeModName = Path.GetFileName(args[0].Replace(".txt", ""));

                List<ModKey> pluginNameList = WillNotMergeCheck(File.ReadAllLines(args[0]));

                if(pluginNameList.Count <= 1)
                {
                    Console.WriteLine("Can't Merge anything.");
                    Console.WriteLine("Press Enter to Close...");
                    Console.ReadLine();
                    return;
                }

                if (Settings.mSeperateFolders)
                {
                    Settings.pOutputFolder = Path.Combine(Settings.mModsPath, Path.GetFileNameWithoutExtension(mergeModName));
                    Directory.CreateDirectory(Settings.pOutputFolder);
                }

                Console.WriteLine("Starting Merge");
                var MergeInformation = new MergePlugin(mergeModName, pluginNameList, settings: Settings)
                    .Build();

                new MergeDataFiles(MergeInformation)
                    .ExtractData()
                    .HandleVoiceFiles()
                    .HandleFaceGenFiles();

                //EditPluginsTXT.ChangePluginsTXT(pluginNameList.ToHashSet(), Settings.pPluginstxt);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            if (Pause)
            {
                Console.WriteLine("Press Enter to Close...");
                Console.ReadLine();
            }
        }

        internal static List<ModKey> WillNotMergeCheck(string[] pluginNameListArr)
        {
            Console.WriteLine("Checking for Valid plugins");
            List<ModKey> pluginNameList = new();
            HashSet<string> WontMergeList = new(StringComparer.OrdinalIgnoreCase)
            {
                "Skyrim.esm",
                "Update.esm",
                "Dawnguard.esm",
                "HearthFires.esm",
                "Dragonborn.esm"
            };

            foreach (string pluginName in pluginNameListArr)
            {
                if(WontMergeList.Contains(pluginName)) continue;
                if(!File.Exists(Path.Combine(Settings.pDataFolder, pluginName)))
                {
                    Console.WriteLine($"{pluginName} does not exists in data folder.");
                    continue;
                }
                try
                {
                    pluginNameList.Add(ModKey.FromFileName(pluginName));
                }
                catch (ArgumentException) { Console.WriteLine("Not a plugin"); }
            }

            return InvalidateModsForMerge(pluginNameList);
        }

        internal static List<ModKey> InvalidateModsForMerge(List<ModKey> pluginNameList)
        {
            Console.WriteLine("Invalidating plugins");
            List<ModKey> invalidMods = new();
            foreach (var key in pluginNameList)
            {
                bool invalid = false;
                using var mod = SkyrimMod.CreateFromBinaryOverlay(Path.Combine(Settings.pDataFolder, key.FileName), SkyrimRelease.SkyrimSE);

                if (!Settings.aDisableNavigationMeshInfoMapsCheck)
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
                    pluginNameList.Remove(key);
                    Console.WriteLine($"Invalid for merge: {key.FileName}");
                }

                Console.WriteLine("Those Invalid for merge contain a new Cell, Worldspace, or a Navigation Mesh Info Map record.");
                Console.WriteLine("These are not something that are supported by this merge tool for a number of reasons.");
                Console.WriteLine("But the major reasons are without them the game is prone to crashing.");
                Console.WriteLine("And I do not have a reliable way of combining Navigation Mesh Info Map record.");
                Console.WriteLine("If someone does develop a solution and wants to add it to this I will gladly except it.");
                Console.WriteLine("Press Enter to Continue with the Merge...");
                Console.ReadLine();
            }

            return pluginNameList;
        }
    }
}