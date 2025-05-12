using DynamicData;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Environments;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Assets;
using Mutagen.Bethesda.Skyrim;
using Noggog;

namespace MergePluginsMutagen.MergePluginClass
{
    public partial class MergePlugin : IMergeInformation
    {
        private List<string> LoadOrder = new List<string>();
        private void AddNested()
        {
            var loadOnlyThese = BuildOnlyLoadTheseList();
            using var env = GameEnvironment.Typical.Builder<ISkyrimMod, ISkyrimModGetter>(GameRelease.SkyrimSE)
                .TransformLoadOrderListings(x => x.Where(x => loadOnlyThese.Contains(x.ModKey)))
                .WithTargetDataFolder(Settings.pDataFolder)
                .Build();

            Console.WriteLine("Load Oder for Merge: ");
            foreach (var plugin in env.LoadOrder.Items)
            {
                LoadOrder.Add(plugin.ModKey.FileName);
                Console.WriteLine("\t" + plugin.ModKey.FileName);
            }

            Console.WriteLine("Building AssetLinkCashe");
            var assetLinkCache = env.LinkCache.CreateImmutableAssetLinkCache();


            foreach (ModKey modKey in MergeModKeys)
            {
                Console.WriteLine(modKey);
                if (!env.LoadOrder.TryGetValue(modKey, out var plugin) || plugin.Mod == null) continue;

                Console.WriteLine("Copying Cells Items");
                foreach (var cellblock in plugin.Mod.Cells)
                {
                    foreach (var subBlock in cellblock.SubBlocks)
                    {
                        foreach (var cell in subBlock.Cells)
                        {
                            foreach(INavigationMeshGetter rec in cell.NavigationMeshes)
                            {
                                var context = env.LinkCache.ResolveContext<INavigationMesh, INavigationMeshGetter>(rec.FormKey);

                                context.GetOrAddAsOverride(MergeMod);
                            }

                            foreach (var rec in cell.Persistent)
                            {
                                var context = env.LinkCache.ResolveContext<IPlaced, IPlacedGetter>(rec.FormKey);

                                context.GetOrAddAsOverride(MergeMod);
                            }

                            foreach (var rec in cell.Temporary)
                            {
                                var context = env.LinkCache.ResolveContext<IPlaced, IPlacedGetter>(rec.FormKey);

                                context.GetOrAddAsOverride(MergeMod);
                            }
                        }
                    }
                }//End Cell Import

                Console.WriteLine("Copying Worldspace Items");
                foreach (var worldSpace in plugin.Mod.Worldspaces)
                {
                    if(worldSpace.TopCell != null)
                    {
                        foreach(var rec in worldSpace.TopCell.NavigationMeshes)
                        {
                            var context = env.LinkCache.ResolveContext<INavigationMesh, INavigationMeshGetter>(rec.FormKey);

                            context.GetOrAddAsOverride(MergeMod);
                        }

                        foreach (var rec in worldSpace.TopCell.Persistent)
                        {
                            var context = env.LinkCache.ResolveContext<IPlaced, IPlacedGetter>(rec.FormKey);

                            context.GetOrAddAsOverride(MergeMod);
                        }
                        foreach (var rec in worldSpace.TopCell.Temporary)
                        {
                            var context = env.LinkCache.ResolveContext<IPlaced, IPlacedGetter>(rec.FormKey);

                            context.GetOrAddAsOverride(MergeMod);
                        }
                    }//End Worldspace's top Cell

                    foreach(var block in worldSpace.SubCells)
                    {
                        foreach(var subBlock in block.Items)
                        {
                            foreach(var cell in subBlock.Items)
                            {
                                foreach (INavigationMeshGetter rec in cell.NavigationMeshes)
                                {
                                    var context = env.LinkCache.ResolveContext<INavigationMesh, INavigationMeshGetter>(rec.FormKey);

                                    context.GetOrAddAsOverride(MergeMod);
                                }

                                foreach (var rec in cell.Persistent)
                                {
                                    var context = env.LinkCache.ResolveContext<IPlaced, IPlacedGetter>(rec.FormKey);

                                    context.GetOrAddAsOverride(MergeMod);
                                }

                                foreach (var rec in cell.Temporary)
                                {
                                    var context = env.LinkCache.ResolveContext<IPlaced, IPlacedGetter>(rec.FormKey);

                                    context.GetOrAddAsOverride(MergeMod);
                                }
                            }
                        }
                    }
                }//End Worldspace Import

                Console.WriteLine("Copying Dialoug and Responses");
                foreach (var dialougTopic in plugin.Mod.DialogTopics)
                {
                    foreach (var rec in dialougTopic.Responses)
                    {
                        var context = env.LinkCache.ResolveContext<IDialogResponses, IDialogResponsesGetter>(rec.FormKey);
                        context.GetOrAddAsOverride(MergeMod); 

                        if (MergeModKeysHashSet.Contains(rec.FormKey.ModKey))
                        {
                            var assetPaths = rec.EnumerateResolvedAssetLinks(assetLinkCache)
                                .Select(x => x.DataRelativePath.ToString())
                                .ToHashSet();

                            ResponseAssetLinks.Add(rec.FormKey, assetPaths);
                        }
                    }
                }//End Dialog Import
            }//End ModKeys foreach
        }//End AddNested()

        private void ChangeNestedFormKeys()
        {
            HashSet<ModKey> mergeModKeysHashSet = MergeModKeys.ToHashSet();
            foreach (var cellblock in MergeMod.Cells)
            {
                Console.WriteLine("Changing Cell objects ID");
                foreach (var subBlock in cellblock.SubBlocks)
                {
                    foreach (var cell in subBlock.Cells)
                    {
                        foreach (var rec in cell.Persistent)
                        {
                            if(DontChangeFormIDs.Contains(rec.FormKey.ModKey)) continue;

                            if (mergeModKeysHashSet.Contains(rec.FormKey.ModKey))
                            {
                                FormKey formKey = MergeMod.GetNextFormKey();
                                MergeMap.Add(rec.FormKey, formKey);
                                rec.FormKey = formKey;
                            }
                        }

                        foreach (var rec in cell.Temporary)
                        {
                            if (DontChangeFormIDs.Contains(rec.FormKey.ModKey)) continue;

                            if (mergeModKeysHashSet.Contains(rec.FormKey.ModKey))
                            {
                                FormKey formKey = MergeMod.GetNextFormKey();
                                MergeMap.Add(rec.FormKey, formKey);
                                rec.FormKey = formKey;
                            }
                        }
                    }
                }
            }//End Placed Changes

            Console.WriteLine("Changing Worldspace objects ID");
            foreach (var worldSpace in MergeMod.Worldspaces)
            {
                if (worldSpace.TopCell != null)
                {
                    foreach (var rec in worldSpace.TopCell.Persistent)
                    {
                        if (DontChangeFormIDs.Contains(rec.FormKey.ModKey)) continue;

                        if (mergeModKeysHashSet.Contains(rec.FormKey.ModKey))
                        {
                            FormKey formKey = MergeMod.GetNextFormKey();
                            MergeMap.Add(rec.FormKey, formKey);
                            rec.FormKey = formKey;
                        }
                    }
                    foreach (var rec in worldSpace.TopCell.Temporary)
                    {
                        if (DontChangeFormIDs.Contains(rec.FormKey.ModKey)) continue;

                        if (mergeModKeysHashSet.Contains(rec.FormKey.ModKey))
                        {
                            FormKey formKey = MergeMod.GetNextFormKey();
                            MergeMap.Add(rec.FormKey, formKey);
                            rec.FormKey = formKey;
                        }
                    }
                }//End Worldspace's top Cell

                foreach (var block in worldSpace.SubCells)
                {
                    foreach (var subBlock in block.Items)
                    {
                        foreach (var cell in subBlock.Items)
                        {
                            foreach (var rec in cell.Persistent)
                            {
                                if (DontChangeFormIDs.Contains(rec.FormKey.ModKey)) continue;

                                if (mergeModKeysHashSet.Contains(rec.FormKey.ModKey))
                                {
                                    FormKey formKey = MergeMod.GetNextFormKey();
                                    MergeMap.Add(rec.FormKey, formKey);
                                    rec.FormKey = formKey;
                                }
                            }

                            foreach (var rec in cell.Temporary)
                            {
                                if (DontChangeFormIDs.Contains(rec.FormKey.ModKey)) continue;

                                if (mergeModKeysHashSet.Contains(rec.FormKey.ModKey))
                                {
                                    FormKey formKey = MergeMod.GetNextFormKey();
                                    MergeMap.Add(rec.FormKey, formKey);
                                    rec.FormKey = formKey;
                                }
                            }
                        }
                    }
                }
            }//End Worldspace Change

            Console.WriteLine("Changing DialogTopics ID");
            foreach (var rec in MergeMod.DialogTopics.ToArray())
            {
                if (DontChangeFormIDs.Contains(rec.FormKey.ModKey)) continue;

                if (mergeModKeysHashSet.Contains(rec.FormKey.ModKey))
                {
                    FormKey formKey = MergeMod.GetNextFormKey();
                    MergeMap.Add(rec.FormKey, formKey);

                    MergeMod.Remove(rec.FormKey);
                    MergeMod.DialogTopics.Add(rec.Duplicate(formKey));
                }
            }//End Dialog Change

            Console.WriteLine("Changing Response ID");
            foreach (var dialougTopic in MergeMod.DialogTopics)
            {
                foreach (var rec in dialougTopic.Responses.ToArray())
                {
                    if (DontChangeFormIDs.Contains(rec.FormKey.ModKey)) continue;

                    if (mergeModKeysHashSet.Contains(rec.FormKey.ModKey))
                    {
                        FormKey formKey = MergeMod.GetNextFormKey();
                        MergeMap.Add(rec.FormKey, formKey);

                        MergeMod.DialogTopics[dialougTopic.FormKey].Responses.Remove(rec);
                        MergeMod.DialogTopics[dialougTopic.FormKey].Responses.Add(rec.Duplicate(formKey));
                    }
                }
            }//End Dialog Responses Change
        }//End ChangeFormKeys()
    }
}
