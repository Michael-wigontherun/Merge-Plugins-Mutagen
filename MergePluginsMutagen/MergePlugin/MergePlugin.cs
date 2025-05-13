using DynamicData;
using MergePluginsMutagen.zMergeJson;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Environments.DI;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Analysis.DI;
using Mutagen.Bethesda.Plugins.Masters.DI;
using Mutagen.Bethesda.Skyrim;

namespace MergePluginsMutagen.MergePluginClass
{
    public partial class MergePlugin : IMergeInformation
    {
        public MergePlugin(string mergeModName, List<ModKey> pluginNameList, HashSet<ModKey>? dontChangeFormIDs = null, Settings? settings = null) : 
            base(pluginNameList, 
                new Dictionary<FormKey, FormKey>(), 
                new Dictionary<FormKey, HashSet<string>>(), 
                new Dictionary<FormKey, HashSet<string>>(), 
                settings ?? Settings.GetDefaultLocation())
        {
            MergeMod = new SkyrimMod(mergeModName, SkyrimRelease.SkyrimSE);
            MergeModKeysHashSet = pluginNameList.ToHashSet();
            DontChangeFormIDs = dontChangeFormIDs ?? new();
        }

        private SkyrimMod MergeMod;

        private HashSet<ModKey> MergeModKeysHashSet;

        private HashSet<ModKey> DontChangeFormIDs;

        private bool ContainsMergedPluginsHoldingNavMap = false;

        public IMergeInformation Build()
        {
            AddNested();
            
            ChangeNestedFormKeys();

            foreach (var key in MergeModKeys)
            {
                //SkyrimMod mod = SkyrimMod.CreateFromBinary(Path.Combine(Settings.pDataFolder, key.FileName), SkyrimRelease.SkyrimSE);
                Console.WriteLine("Duplicating records from " + key.FileName);
                AddUniformKeys(SkyrimMod.CreateFromBinary(Path.Combine(Settings.pDataFolder, key.FileName), SkyrimRelease.SkyrimSE));
            }

            Console.WriteLine("Remapping IDs");
            MergeMod.RemapLinks(MergeMap);

            Save();

            return (IMergeInformation)this;
        }

        private HashSet<ModKey> BuildOnlyLoadTheseList()
        {
            //OnlyLoadThese = MergeModKeys.ToHashSet();
            Console.WriteLine("Building Load order");
            var locator = new TransitiveMasterLocator(
                new System.IO.Abstractions.FileSystem(),
                new DataDirectoryInjection(Settings.pDataFolder),
                new GameReleaseInjection(GameRelease.SkyrimSE));
            return locator.GetAllMasters(MergeModKeys).ToHashSet();
        }

        private void Save()
        {
            Console.WriteLine("Saving Merge");

            foreach (var rec in MergeMod.EnumerateMajorRecords())
            {
                rec.IsCompressed = false;
            }

            if (new RecordCompactionCompatibilityDetector().IsSmallMasterCompatible(MergeMod)) MergeMod.ModHeader.Flags |= SkyrimModHeader.HeaderFlag.Small;
            
            string modOutputPath = Path.Combine(Settings.pOutputFolder, MergeMod.ModKey.FileName);
            Directory.CreateDirectory(Settings.pOutputFolder);
            MergeMod.BeginWrite
                .ToPath(modOutputPath)
                .WithDefaultLoadOrder()
                .Write();

            CreateSEQFileForMod();

            string baseJsonOutputPath = Path.Combine(Settings.pOutputFolder, "merge - " + Path.GetFileNameWithoutExtension(MergeMod.ModKey.FileName));

            var mapJSON = new MergeMapJson(MergeMap, MergeMod.ModKey.FileName, MergeModKeys, 
                containsMergedPluginsHoldingNavMap: ContainsMergedPluginsHoldingNavMap);
            mapJSON.Output(Path.Combine(baseJsonOutputPath, MergeMod.ModKey.FileName + ".json"));

            new mergeJson(MergeMod.ModKey.FileName, LoadOrder, File.GetLastWriteTime(modOutputPath).ToLongTimeString(), MergeModKeys)
                .Output(Path.Combine(baseJsonOutputPath, "merge.json"));

            new MapJSON(MergeMap, MergeModKeys)
                .Outout(Path.Combine(baseJsonOutputPath, "map.json"));

            new fidCache(MergeMap, MergeModKeys)
                .Output(Path.Combine(baseJsonOutputPath, "fidCache.json"));
        }

        private void CreateSEQFileForMod()
        {
            HashSet<string> masters = new();
            foreach (var rec in MergeMod.EnumerateMajorRecords())
            {
                foreach (var link in rec.EnumerateFormLinks())
                {
                    if (link.FormKey.ModKey.Equals(MergeMod.ModKey)) continue;
                    masters.Add(link.FormKey.ModKey.FileName);
                }
                if (rec.FormKey.ModKey.Equals(MergeMod.ModKey)) continue;
                masters.Add(rec.FormKey.ModKey.FileName);
            }

            uint uintToAdd = 0x01000000 * Convert.ToUInt32(masters.Count);

            List<byte> ids = new();
            foreach (var quest in MergeMod.Quests)
            {
                if (!quest.FormKey.ModKey.Equals(MergeMod.ModKey)) continue;//needed incase the quest is a overriden start game quest they sould not be in the seq generated file

                if (quest.Flags.HasFlag(Quest.Flag.StartGameEnabled))
                {
                    ids.Add(BitConverter.GetBytes(quest.FormKey.ID + uintToAdd));
                }
            }

            if (ids.Any())
            {
                string folderPath = Path.Combine(Settings.pOutputFolder, "seq");
                Directory.CreateDirectory(folderPath);
                File.WriteAllBytes(Path.Combine(folderPath, Path.ChangeExtension(MergeMod.ModKey.FileName, "seq")), ids.ToArray());
            }
        }
    }
    
}
