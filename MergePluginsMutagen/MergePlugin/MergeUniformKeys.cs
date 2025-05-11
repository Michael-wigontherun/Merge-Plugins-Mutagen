using Mutagen.Bethesda;
using Mutagen.Bethesda.Plugins.Assets;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;
using nifly;

namespace MergePluginsMutagen.MergePluginClass
{
    public partial class MergePlugin : IMergeInformation
    {
        private void AddUniformKeys(SkyrimMod mod)
        {
            var linkCashe = mod.ToImmutableLinkCache();
            var assetLinkCache = linkCashe.CreateImmutableAssetLinkCache();

            #region Npcs
            foreach (var sourceForm in mod.Npcs)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Npcs[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Npcs[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);
                    MergeMod.Npcs.Add(formCopy);

                    var assetPaths = sourceForm.EnumerateResolvedAssetLinks(assetLinkCache)
                        .Select(x => x.DataRelativePath.ToString())
                        .ToHashSet();

                    NPCAssetLinks.Add(sourceForm.FormKey, assetPaths);
                }
                else
                {
                    MergeMod.Npcs.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Npcs

            //Do not remove above
            //------------------------------------------------------------------------

            #region GameSettings
            foreach (var sourceForm in mod.GameSettings)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.GameSettings[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.GameSettings[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.GameSettings.Add(formCopy);
                }
                else
                {
                    MergeMod.GameSettings.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion GameSettings

            #region Keywords
            foreach (var sourceForm in mod.Keywords)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Keywords[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Keywords[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Keywords.Add(formCopy);
                }
                else
                {
                    MergeMod.Keywords.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Keywords

            #region LocationReferenceTypes
            foreach (var sourceForm in mod.LocationReferenceTypes)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.LocationReferenceTypes[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.LocationReferenceTypes[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.LocationReferenceTypes.Add(formCopy);
                }
                else
                {
                    MergeMod.LocationReferenceTypes.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion LocationReferenceTypes

            #region Actions
            foreach (var sourceForm in mod.Actions)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Actions[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Actions[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Actions.Add(formCopy);
                }
                else
                {
                    MergeMod.Actions.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Actions

            #region TextureSets
            foreach (var sourceForm in mod.TextureSets)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.TextureSets[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.TextureSets[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.TextureSets.Add(formCopy);
                }
                else
                {
                    MergeMod.TextureSets.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion TextureSets

            #region Globals
            foreach (var sourceForm in mod.Globals)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Globals[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Globals[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Globals.Add(formCopy);
                }
                else
                {
                    MergeMod.Globals.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Globals

            #region Classes
            foreach (var sourceForm in mod.Classes)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Classes[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Classes[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Classes.Add(formCopy);
                }
                else
                {
                    MergeMod.Classes.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Classes

            #region Factions
            foreach (var sourceForm in mod.Factions)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Factions[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Factions[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Factions.Add(formCopy);
                }
                else
                {
                    MergeMod.Factions.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Factions

            #region HeadParts
            foreach (var sourceForm in mod.HeadParts)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.HeadParts[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.HeadParts[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.HeadParts.Add(formCopy);
                }
                else
                {
                    MergeMod.HeadParts.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion HeadParts

            #region Hairs
            foreach (var sourceForm in mod.Hairs)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Hairs[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Hairs[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Hairs.Add(formCopy);
                }
                else
                {
                    MergeMod.Hairs.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Hairs

            #region Eyes
            foreach (var sourceForm in mod.Eyes)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Eyes[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Eyes[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Eyes.Add(formCopy);
                }
                else
                {
                    MergeMod.Eyes.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Eyes

            #region Races
            foreach (var sourceForm in mod.Races)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Races[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Races[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Races.Add(formCopy);
                }
                else
                {
                    MergeMod.Races.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Races

            #region SoundMarkers
            foreach (var sourceForm in mod.SoundMarkers)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.SoundMarkers[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.SoundMarkers[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.SoundMarkers.Add(formCopy);
                }
                else
                {
                    MergeMod.SoundMarkers.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion SoundMarkers

            #region AcousticSpaces
            foreach (var sourceForm in mod.AcousticSpaces)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.AcousticSpaces[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.AcousticSpaces[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.AcousticSpaces.Add(formCopy);
                }
                else
                {
                    MergeMod.AcousticSpaces.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion AcousticSpaces

            #region MagicEffects
            foreach (var sourceForm in mod.MagicEffects)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.MagicEffects[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.MagicEffects[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.MagicEffects.Add(formCopy);
                }
                else
                {
                    MergeMod.MagicEffects.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion MagicEffects

            #region LandscapeTextures
            foreach (var sourceForm in mod.LandscapeTextures)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.LandscapeTextures[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.LandscapeTextures[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.LandscapeTextures.Add(formCopy);
                }
                else
                {
                    MergeMod.LandscapeTextures.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion LandscapeTextures

            #region ObjectEffects
            foreach (var sourceForm in mod.ObjectEffects)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.ObjectEffects[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.ObjectEffects[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.ObjectEffects.Add(formCopy);
                }
                else
                {
                    MergeMod.ObjectEffects.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion ObjectEffects

            #region Spells
            foreach (var sourceForm in mod.Spells)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Spells[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Spells[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Spells.Add(formCopy);
                }
                else
                {
                    MergeMod.Spells.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Spells

            #region Scrolls
            foreach (var sourceForm in mod.Scrolls)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Scrolls[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Scrolls[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Scrolls.Add(formCopy);
                }
                else
                {
                    MergeMod.Scrolls.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Scrolls

            #region Activators
            foreach (var sourceForm in mod.Activators)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Activators[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Activators[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Activators.Add(formCopy);
                }
                else
                {
                    MergeMod.Activators.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Activators

            #region TalkingActivators
            foreach (var sourceForm in mod.TalkingActivators)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.TalkingActivators[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.TalkingActivators[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.TalkingActivators.Add(formCopy);
                }
                else
                {
                    MergeMod.TalkingActivators.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion TalkingActivators

            #region Armors
            foreach (var sourceForm in mod.Armors)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Armors[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Armors[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Armors.Add(formCopy);
                }
                else
                {
                    MergeMod.Armors.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Armors

            #region Books
            foreach (var sourceForm in mod.Books)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Books[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Books[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Books.Add(formCopy);
                }
                else
                {
                    MergeMod.Books.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Books

            #region Containers
            foreach (var sourceForm in mod.Containers)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Containers[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Containers[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Containers.Add(formCopy);
                }
                else
                {
                    MergeMod.Containers.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Containers

            #region Doors
            foreach (var sourceForm in mod.Doors)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Doors[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Doors[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Doors.Add(formCopy);
                }
                else
                {
                    MergeMod.Doors.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Doors

            #region Ingredients
            foreach (var sourceForm in mod.Ingredients)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Ingredients[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Ingredients[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Ingredients.Add(formCopy);
                }
                else
                {
                    MergeMod.Ingredients.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Ingredients

            #region Lights
            foreach (var sourceForm in mod.Lights)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Lights[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Lights[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Lights.Add(formCopy);
                }
                else
                {
                    MergeMod.Lights.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Lights

            #region MiscItems
            foreach (var sourceForm in mod.MiscItems)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.MiscItems[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.MiscItems[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.MiscItems.Add(formCopy);
                }
                else
                {
                    MergeMod.MiscItems.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion MiscItems

            #region AlchemicalApparatuses
            foreach (var sourceForm in mod.AlchemicalApparatuses)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.AlchemicalApparatuses[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.AlchemicalApparatuses[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.AlchemicalApparatuses.Add(formCopy);
                }
                else
                {
                    MergeMod.AlchemicalApparatuses.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion AlchemicalApparatuses

            #region Statics
            foreach (var sourceForm in mod.Statics)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Statics[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Statics[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Statics.Add(formCopy);
                }
                else
                {
                    MergeMod.Statics.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Statics

            #region MoveableStatics
            foreach (var sourceForm in mod.MoveableStatics)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.MoveableStatics[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.MoveableStatics[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.MoveableStatics.Add(formCopy);
                }
                else
                {
                    MergeMod.MoveableStatics.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion MoveableStatics

            #region Grasses
            foreach (var sourceForm in mod.Grasses)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Grasses[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Grasses[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Grasses.Add(formCopy);
                }
                else
                {
                    MergeMod.Grasses.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Grasses

            #region Trees
            foreach (var sourceForm in mod.Trees)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Trees[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Trees[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Trees.Add(formCopy);
                }
                else
                {
                    MergeMod.Trees.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Trees

            #region Florae
            foreach (var sourceForm in mod.Florae)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Florae[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Florae[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Florae.Add(formCopy);
                }
                else
                {
                    MergeMod.Florae.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Florae

            #region Furniture
            foreach (var sourceForm in mod.Furniture)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Furniture[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Furniture[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Furniture.Add(formCopy);
                }
                else
                {
                    MergeMod.Furniture.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Furniture

            #region Weapons
            foreach (var sourceForm in mod.Weapons)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Weapons[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Weapons[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Weapons.Add(formCopy);
                }
                else
                {
                    MergeMod.Weapons.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Weapons

            #region Ammunitions
            foreach (var sourceForm in mod.Ammunitions)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Ammunitions[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Ammunitions[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Ammunitions.Add(formCopy);
                }
                else
                {
                    MergeMod.Ammunitions.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Ammunitions

            #region LeveledNpcs
            foreach (var sourceForm in mod.LeveledNpcs)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.LeveledNpcs[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.LeveledNpcs[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.LeveledNpcs.Add(formCopy);
                }
                else
                {
                    MergeMod.LeveledNpcs.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion LeveledNpcs

            #region Keys
            foreach (var sourceForm in mod.Keys)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Keys[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Keys[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Keys.Add(formCopy);
                }
                else
                {
                    MergeMod.Keys.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Keys

            #region Ingestibles
            foreach (var sourceForm in mod.Ingestibles)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Ingestibles[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Ingestibles[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Ingestibles.Add(formCopy);
                }
                else
                {
                    MergeMod.Ingestibles.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Ingestibles

            #region IdleMarkers
            foreach (var sourceForm in mod.IdleMarkers)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.IdleMarkers[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.IdleMarkers[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.IdleMarkers.Add(formCopy);
                }
                else
                {
                    MergeMod.IdleMarkers.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion IdleMarkers

            #region ConstructibleObjects
            foreach (var sourceForm in mod.ConstructibleObjects)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.ConstructibleObjects[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.ConstructibleObjects[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.ConstructibleObjects.Add(formCopy);
                }
                else
                {
                    MergeMod.ConstructibleObjects.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion ConstructibleObjects

            #region Projectiles
            foreach (var sourceForm in mod.Projectiles)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Projectiles[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Projectiles[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Projectiles.Add(formCopy);
                }
                else
                {
                    MergeMod.Projectiles.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Projectiles

            #region Hazards
            foreach (var sourceForm in mod.Hazards)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Hazards[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Hazards[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Hazards.Add(formCopy);
                }
                else
                {
                    MergeMod.Hazards.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Hazards

            #region SoulGems
            foreach (var sourceForm in mod.SoulGems)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.SoulGems[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.SoulGems[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.SoulGems.Add(formCopy);
                }
                else
                {
                    MergeMod.SoulGems.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion SoulGems

            #region LeveledItems
            foreach (var sourceForm in mod.LeveledItems)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.LeveledItems[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.LeveledItems[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.LeveledItems.Add(formCopy);
                }
                else
                {
                    MergeMod.LeveledItems.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion LeveledItems

            #region Weathers
            foreach (var sourceForm in mod.Weathers)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Weathers[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Weathers[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Weathers.Add(formCopy);
                }
                else
                {
                    MergeMod.Weathers.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Weathers

            #region Climates
            foreach (var sourceForm in mod.Climates)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Climates[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Climates[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Climates.Add(formCopy);
                }
                else
                {
                    MergeMod.Climates.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Climates

            #region ShaderParticleGeometries
            foreach (var sourceForm in mod.ShaderParticleGeometries)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.ShaderParticleGeometries[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.ShaderParticleGeometries[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.ShaderParticleGeometries.Add(formCopy);
                }
                else
                {
                    MergeMod.ShaderParticleGeometries.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion ShaderParticleGeometries

            #region VisualEffects
            foreach (var sourceForm in mod.VisualEffects)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.VisualEffects[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.VisualEffects[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.VisualEffects.Add(formCopy);
                }
                else
                {
                    MergeMod.VisualEffects.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion VisualEffects

            #region Regions
            foreach (var sourceForm in mod.Regions)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Regions[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Regions[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Regions.Add(formCopy);
                }
                else
                {
                    MergeMod.Regions.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Regions

            #region Quests
            foreach (var sourceForm in mod.Quests)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Quests[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Quests[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Quests.Add(formCopy);
                }
                else
                {
                    MergeMod.Quests.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Quests

            #region IdleAnimations
            foreach (var sourceForm in mod.IdleAnimations)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.IdleAnimations[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.IdleAnimations[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.IdleAnimations.Add(formCopy);
                }
                else
                {
                    MergeMod.IdleAnimations.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion IdleAnimations

            #region Packages
            foreach (var sourceForm in mod.Packages)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Packages[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Packages[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Packages.Add(formCopy);
                }
                else
                {
                    MergeMod.Packages.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Packages

            #region CombatStyles
            foreach (var sourceForm in mod.CombatStyles)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.CombatStyles[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.CombatStyles[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.CombatStyles.Add(formCopy);
                }
                else
                {
                    MergeMod.CombatStyles.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion CombatStyles

            #region LoadScreens
            foreach (var sourceForm in mod.LoadScreens)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.LoadScreens[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.LoadScreens[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.LoadScreens.Add(formCopy);
                }
                else
                {
                    MergeMod.LoadScreens.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion LoadScreens

            #region LeveledSpells
            foreach (var sourceForm in mod.LeveledSpells)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.LeveledSpells[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.LeveledSpells[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.LeveledSpells.Add(formCopy);
                }
                else
                {
                    MergeMod.LeveledSpells.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion LeveledSpells

            #region AnimatedObjects
            foreach (var sourceForm in mod.AnimatedObjects)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.AnimatedObjects[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.AnimatedObjects[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.AnimatedObjects.Add(formCopy);
                }
                else
                {
                    MergeMod.AnimatedObjects.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion AnimatedObjects

            #region Waters
            foreach (var sourceForm in mod.Waters)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Waters[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Waters[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Waters.Add(formCopy);
                }
                else
                {
                    MergeMod.Waters.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Waters

            #region EffectShaders
            foreach (var sourceForm in mod.EffectShaders)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.EffectShaders[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.EffectShaders[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.EffectShaders.Add(formCopy);
                }
                else
                {
                    MergeMod.EffectShaders.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion EffectShaders

            #region Explosions
            foreach (var sourceForm in mod.Explosions)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Explosions[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Explosions[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Explosions.Add(formCopy);
                }
                else
                {
                    MergeMod.Explosions.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Explosions

            #region Debris
            foreach (var sourceForm in mod.Debris)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Debris[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Debris[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Debris.Add(formCopy);
                }
                else
                {
                    MergeMod.Debris.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Debris

            #region ImageSpaces
            foreach (var sourceForm in mod.ImageSpaces)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.ImageSpaces[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.ImageSpaces[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.ImageSpaces.Add(formCopy);
                }
                else
                {
                    MergeMod.ImageSpaces.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion ImageSpaces

            #region ImageSpaceAdapters
            foreach (var sourceForm in mod.ImageSpaceAdapters)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.ImageSpaceAdapters[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.ImageSpaceAdapters[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.ImageSpaceAdapters.Add(formCopy);
                }
                else
                {
                    MergeMod.ImageSpaceAdapters.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion ImageSpaceAdapters

            #region FormLists
            foreach (var sourceForm in mod.FormLists)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.FormLists[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.FormLists[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.FormLists.Add(formCopy);
                }
                else
                {
                    MergeMod.FormLists.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion FormLists

            #region Perks
            foreach (var sourceForm in mod.Perks)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Perks[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Perks[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Perks.Add(formCopy);
                }
                else
                {
                    MergeMod.Perks.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Perks

            #region BodyParts
            foreach (var sourceForm in mod.BodyParts)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.BodyParts[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.BodyParts[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.BodyParts.Add(formCopy);
                }
                else
                {
                    MergeMod.BodyParts.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion BodyParts

            #region AddonNodes
            foreach (var sourceForm in mod.AddonNodes)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.AddonNodes[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.AddonNodes[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.AddonNodes.Add(formCopy);
                }
                else
                {
                    MergeMod.AddonNodes.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion AddonNodes

            #region ActorValueInformation
            foreach (var sourceForm in mod.ActorValueInformation)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.ActorValueInformation[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.ActorValueInformation[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.ActorValueInformation.Add(formCopy);
                }
                else
                {
                    MergeMod.ActorValueInformation.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion ActorValueInformation

            #region CameraShots
            foreach (var sourceForm in mod.CameraShots)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.CameraShots[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.CameraShots[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.CameraShots.Add(formCopy);
                }
                else
                {
                    MergeMod.CameraShots.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion CameraShots

            #region CameraPaths
            foreach (var sourceForm in mod.CameraPaths)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.CameraPaths[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.CameraPaths[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.CameraPaths.Add(formCopy);
                }
                else
                {
                    MergeMod.CameraPaths.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion CameraPaths

            #region VoiceTypes
            foreach (var sourceForm in mod.VoiceTypes)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.VoiceTypes[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.VoiceTypes[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.VoiceTypes.Add(formCopy);
                }
                else
                {
                    MergeMod.VoiceTypes.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion VoiceTypes

            #region MaterialTypes
            foreach (var sourceForm in mod.MaterialTypes)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.MaterialTypes[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.MaterialTypes[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.MaterialTypes.Add(formCopy);
                }
                else
                {
                    MergeMod.MaterialTypes.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion MaterialTypes

            #region Impacts
            foreach (var sourceForm in mod.Impacts)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Impacts[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Impacts[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Impacts.Add(formCopy);
                }
                else
                {
                    MergeMod.Impacts.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Impacts

            #region ImpactDataSets
            foreach (var sourceForm in mod.ImpactDataSets)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.ImpactDataSets[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.ImpactDataSets[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.ImpactDataSets.Add(formCopy);
                }
                else
                {
                    MergeMod.ImpactDataSets.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion ImpactDataSets

            #region ArmorAddons
            foreach (var sourceForm in mod.ArmorAddons)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.ArmorAddons[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.ArmorAddons[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.ArmorAddons.Add(formCopy);
                }
                else
                {
                    MergeMod.ArmorAddons.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion ArmorAddons

            #region EncounterZones
            foreach (var sourceForm in mod.EncounterZones)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.EncounterZones[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.EncounterZones[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.EncounterZones.Add(formCopy);
                }
                else
                {
                    MergeMod.EncounterZones.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion EncounterZones

            #region Locations
            foreach (var sourceForm in mod.Locations)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Locations[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Locations[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Locations.Add(formCopy);
                }
                else
                {
                    MergeMod.Locations.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Locations

            #region Messages
            foreach (var sourceForm in mod.Messages)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Messages[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Messages[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Messages.Add(formCopy);
                }
                else
                {
                    MergeMod.Messages.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Messages

            #region DefaultObjectManagers
            foreach (var sourceForm in mod.DefaultObjectManagers)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.DefaultObjectManagers[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.DefaultObjectManagers[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.DefaultObjectManagers.Add(formCopy);
                }
                else
                {
                    MergeMod.DefaultObjectManagers.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion DefaultObjectManagers

            #region LightingTemplates
            foreach (var sourceForm in mod.LightingTemplates)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.LightingTemplates[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.LightingTemplates[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.LightingTemplates.Add(formCopy);
                }
                else
                {
                    MergeMod.LightingTemplates.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion LightingTemplates

            #region MusicTypes
            foreach (var sourceForm in mod.MusicTypes)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.MusicTypes[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.MusicTypes[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.MusicTypes.Add(formCopy);
                }
                else
                {
                    MergeMod.MusicTypes.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion MusicTypes

            #region Footsteps
            foreach (var sourceForm in mod.Footsteps)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Footsteps[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Footsteps[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Footsteps.Add(formCopy);
                }
                else
                {
                    MergeMod.Footsteps.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Footsteps

            #region FootstepSets
            foreach (var sourceForm in mod.FootstepSets)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.FootstepSets[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.FootstepSets[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.FootstepSets.Add(formCopy);
                }
                else
                {
                    MergeMod.FootstepSets.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion FootstepSets

            #region StoryManagerBranchNodes
            foreach (var sourceForm in mod.StoryManagerBranchNodes)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.StoryManagerBranchNodes[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.StoryManagerBranchNodes[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.StoryManagerBranchNodes.Add(formCopy);
                }
                else
                {
                    MergeMod.StoryManagerBranchNodes.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion StoryManagerBranchNodes

            #region StoryManagerQuestNodes
            foreach (var sourceForm in mod.StoryManagerQuestNodes)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.StoryManagerQuestNodes[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.StoryManagerQuestNodes[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.StoryManagerQuestNodes.Add(formCopy);
                }
                else
                {
                    MergeMod.StoryManagerQuestNodes.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion StoryManagerQuestNodes

            #region StoryManagerEventNodes
            foreach (var sourceForm in mod.StoryManagerEventNodes)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.StoryManagerEventNodes[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.StoryManagerEventNodes[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.StoryManagerEventNodes.Add(formCopy);
                }
                else
                {
                    MergeMod.StoryManagerEventNodes.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion StoryManagerEventNodes

            #region DialogBranches
            foreach (var sourceForm in mod.DialogBranches)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.DialogBranches[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.DialogBranches[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.DialogBranches.Add(formCopy);
                }
                else
                {
                    MergeMod.DialogBranches.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion DialogBranches

            #region MusicTracks
            foreach (var sourceForm in mod.MusicTracks)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.MusicTracks[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.MusicTracks[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.MusicTracks.Add(formCopy);
                }
                else
                {
                    MergeMod.MusicTracks.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion MusicTracks

            #region DialogViews
            foreach (var sourceForm in mod.DialogViews)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.DialogViews[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.DialogViews[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.DialogViews.Add(formCopy);
                }
                else
                {
                    MergeMod.DialogViews.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion DialogViews

            #region WordsOfPower
            foreach (var sourceForm in mod.WordsOfPower)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.WordsOfPower[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.WordsOfPower[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.WordsOfPower.Add(formCopy);
                }
                else
                {
                    MergeMod.WordsOfPower.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion WordsOfPower

            #region Shouts
            foreach (var sourceForm in mod.Shouts)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Shouts[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Shouts[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Shouts.Add(formCopy);
                }
                else
                {
                    MergeMod.Shouts.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Shouts

            #region EquipTypes
            foreach (var sourceForm in mod.EquipTypes)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.EquipTypes[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.EquipTypes[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.EquipTypes.Add(formCopy);
                }
                else
                {
                    MergeMod.EquipTypes.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion EquipTypes

            #region Relationships
            foreach (var sourceForm in mod.Relationships)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Relationships[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Relationships[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Relationships.Add(formCopy);
                }
                else
                {
                    MergeMod.Relationships.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Relationships

            #region Scenes
            foreach (var sourceForm in mod.Scenes)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Scenes[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Scenes[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Scenes.Add(formCopy);
                }
                else
                {
                    MergeMod.Scenes.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Scenes

            #region AssociationTypes
            foreach (var sourceForm in mod.AssociationTypes)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.AssociationTypes[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.AssociationTypes[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.AssociationTypes.Add(formCopy);
                }
                else
                {
                    MergeMod.AssociationTypes.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion AssociationTypes

            #region Outfits
            foreach (var sourceForm in mod.Outfits)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Outfits[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Outfits[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Outfits.Add(formCopy);
                }
                else
                {
                    MergeMod.Outfits.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Outfits

            #region ArtObjects
            foreach (var sourceForm in mod.ArtObjects)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.ArtObjects[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.ArtObjects[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.ArtObjects.Add(formCopy);
                }
                else
                {
                    MergeMod.ArtObjects.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion ArtObjects

            #region MaterialObjects
            foreach (var sourceForm in mod.MaterialObjects)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.MaterialObjects[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.MaterialObjects[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.MaterialObjects.Add(formCopy);
                }
                else
                {
                    MergeMod.MaterialObjects.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion MaterialObjects

            #region MovementTypes
            foreach (var sourceForm in mod.MovementTypes)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.MovementTypes[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.MovementTypes[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.MovementTypes.Add(formCopy);
                }
                else
                {
                    MergeMod.MovementTypes.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion MovementTypes

            #region SoundDescriptors
            foreach (var sourceForm in mod.SoundDescriptors)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.SoundDescriptors[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.SoundDescriptors[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.SoundDescriptors.Add(formCopy);
                }
                else
                {
                    MergeMod.SoundDescriptors.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion SoundDescriptors

            #region DualCastData
            foreach (var sourceForm in mod.DualCastData)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.DualCastData[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.DualCastData[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.DualCastData.Add(formCopy);
                }
                else
                {
                    MergeMod.DualCastData.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion DualCastData

            #region SoundCategories
            foreach (var sourceForm in mod.SoundCategories)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.SoundCategories[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.SoundCategories[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.SoundCategories.Add(formCopy);
                }
                else
                {
                    MergeMod.SoundCategories.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion SoundCategories

            #region SoundOutputModels
            foreach (var sourceForm in mod.SoundOutputModels)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.SoundOutputModels[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.SoundOutputModels[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.SoundOutputModels.Add(formCopy);
                }
                else
                {
                    MergeMod.SoundOutputModels.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion SoundOutputModels

            #region CollisionLayers
            foreach (var sourceForm in mod.CollisionLayers)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.CollisionLayers[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.CollisionLayers[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.CollisionLayers.Add(formCopy);
                }
                else
                {
                    MergeMod.CollisionLayers.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion CollisionLayers

            #region Colors
            foreach (var sourceForm in mod.Colors)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.Colors[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.Colors[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.Colors.Add(formCopy);
                }
                else
                {
                    MergeMod.Colors.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion Colors

            #region ReverbParameters
            foreach (var sourceForm in mod.ReverbParameters)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.ReverbParameters[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.ReverbParameters[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.ReverbParameters.Add(formCopy);
                }
                else
                {
                    MergeMod.ReverbParameters.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion ReverbParameters

            #region VolumetricLightings
            foreach (var sourceForm in mod.VolumetricLightings)
            {
                if (MergeMap.ContainsKey(sourceForm.FormKey))
                {
                    //var m = MergeMod.VolumetricLightings[MergeMap[sourceForm.FormKey]];
                    //m.DeepCopyIn(sourceForm);
                    MergeMod.VolumetricLightings[MergeMap[sourceForm.FormKey]].DeepCopyIn(sourceForm);
                }
                //if (sourceForm.FormKey.ModKey.Equals(mod.ModKey))
                else if (MergeModKeysHashSet.Contains(sourceForm.FormKey.ModKey))
                {
                    var formCopy = sourceForm.Duplicate(MergeMod.GetNextFormKey());

                    MergeMap.Add(sourceForm.FormKey, formCopy.FormKey);

                    MergeMod.VolumetricLightings.Add(formCopy);
                }
                else
                {
                    MergeMod.VolumetricLightings.GetOrAddAsOverride(sourceForm);
                }
            }
            #endregion VolumetricLightings








        }
    }
}
