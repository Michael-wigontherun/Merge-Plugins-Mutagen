using DynamicData;
using MergePluginsMutagen;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Environments;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Noggog;

namespace MergePluginsMutagen.MergePluginClass
{
    public partial class MergePlugin : IMergeInformationInterface
    {
        private void AddNested()
        {
            using var env = GameEnvironment.Typical.Builder<ISkyrimMod, ISkyrimModGetter>(GameRelease.SkyrimSE)
                .TransformLoadOrderListings(x => x.Where(x => BuildOnlyLoadTheseList().Contains(x.ModKey)))
                .WithTargetDataFolder(Settings.pDataFolder)
                .Build();

            foreach (ModKey modKey in MergeModKeys)
            {
                Console.WriteLine(modKey);
                if (!env.LoadOrder.TryGetValue(modKey, out var plugin) || plugin.Mod == null) continue;

                foreach (var cellblock in plugin.Mod.Cells)
                {
                    foreach (var subBlock in cellblock.SubBlocks)
                    {
                        foreach (var cell in subBlock.Cells)
                        {
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

                foreach (var worldSpace in plugin.Mod.Worldspaces)
                {
                    if(worldSpace.TopCell != null)
                    {
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

                foreach (var dialougTopic in plugin.Mod.DialogTopics)
                {
                    foreach (var rec in dialougTopic.Responses)
                    {
                        var context = env.LinkCache.ResolveContext<IDialogResponses, IDialogResponsesGetter>(rec.FormKey);

                        context.GetOrAddAsOverride(MergeMod);
                    }
                }//End Dialog Import
            }//End ModKeys foreach
        }//End AddNested()

        private void ChangeFormKeys()
        {
            HashSet<ModKey> mergeModKeysHashSet = MergeModKeys.ToHashSet();
            foreach (var cellblock in MergeMod.Cells)
            {
                foreach (var subBlock in cellblock.SubBlocks)
                {
                    foreach (var cell in subBlock.Cells)
                    {
                        foreach (var rec in cell.Persistent)
                        {
                            if (mergeModKeysHashSet.Contains(rec.FormKey.ModKey))
                            {
                                FormKey formKey = MergeMod.GetNextFormKey();
                                MergeMap.Add(rec.FormKey, formKey);
                                rec.FormKey = formKey;
                            }
                        }

                        foreach (var rec in cell.Temporary)
                        {
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

            foreach (var worldSpace in MergeMod.Worldspaces)
            {
                if (worldSpace.TopCell != null)
                {
                    foreach (var rec in worldSpace.TopCell.Persistent)
                    {
                        if (mergeModKeysHashSet.Contains(rec.FormKey.ModKey))
                        {
                            FormKey formKey = MergeMod.GetNextFormKey();
                            MergeMap.Add(rec.FormKey, formKey);
                            rec.FormKey = formKey;
                        }
                    }
                    foreach (var rec in worldSpace.TopCell.Temporary)
                    {
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
                                if (mergeModKeysHashSet.Contains(rec.FormKey.ModKey))
                                {
                                    FormKey formKey = MergeMod.GetNextFormKey();
                                    MergeMap.Add(rec.FormKey, formKey);
                                    rec.FormKey = formKey;
                                }
                            }

                            foreach (var rec in cell.Temporary)
                            {
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

            foreach (var rec in MergeMod.DialogTopics.ToArray())
            {
                if (mergeModKeysHashSet.Contains(rec.FormKey.ModKey))
                {
                    FormKey formKey = MergeMod.GetNextFormKey();
                    MergeMap.Add(rec.FormKey, formKey);

                    MergeMod.Remove(rec.FormKey);
                    MergeMod.DialogTopics.Add(rec.Duplicate(formKey));
                }
            }//End Dialog Change
            
            foreach (var dialougTopic in MergeMod.DialogTopics)
            {
                foreach (var rec in dialougTopic.Responses.ToArray())
                {
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
