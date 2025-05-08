using MergePluginsMutagen;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Environments.DI;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Masters.DI;
using Mutagen.Bethesda.Skyrim;

namespace MergePluginsMutagen.MergePluginClass
{
    public partial class MergePlugin : IMergeInformationInterface
    {
        public MergePlugin(string mergeModName, List<ModKey> pluginNameList, Settings? settings = null)
        {
            Settings = settings ?? Settings.GetDefaultLocation();
            MergeMod = new SkyrimMod(mergeModName, SkyrimRelease.SkyrimSE);
            MergeModKeys = pluginNameList;
        }

        private SkyrimMod MergeMod;

        public Dictionary<FormKey, FormKey> Build()
        {
            AddNested();
            
            ChangeFormKeys();

            foreach (var key in MergeModKeys)
            {
                //SkyrimMod mod = SkyrimMod.CreateFromBinary(Path.Combine(Settings.pDataFolder, key.FileName), SkyrimRelease.SkyrimSE);
                AddUniformKeys(SkyrimMod.CreateFromBinary(Path.Combine(Settings.pDataFolder, key.FileName), SkyrimRelease.SkyrimSE));
            }

            MergeMod.RemapLinks(MergeMap);

            Save();
            return MergeMap;
        }

        private HashSet<ModKey> BuildOnlyLoadTheseList()
        {
            //OnlyLoadThese = MergeModKeys.ToHashSet();

            var locator = new TransitiveMasterLocator(
                new System.IO.Abstractions.FileSystem(),
                new DataDirectoryInjection(Settings.pDataFolder),
                new GameReleaseInjection(GameRelease.SkyrimSE));
            return locator.GetAllMasters(MergeModKeys).ToHashSet();
        }

        private void Save()
        {
            foreach (var rec in MergeMod.EnumerateMajorRecords())
            {
                rec.IsCompressed = false;
            }

            Directory.CreateDirectory(Settings.pOutputFolder);
            MergeMod.BeginWrite
                .ToPath(Path.Combine(Settings.pOutputFolder, MergeMod.ModKey.FileName))
                .WithDefaultLoadOrder()
                .Write();

            new MergeMapJson(MergeMap, MergeMod.ModKey.FileName, MergeModKeys)
                .Output(Path.Combine(Settings.pOutputFolder, "merge - " + Path.GetFileNameWithoutExtension(MergeMod.ModKey.FileName), MergeMod.ModKey.FileName + ".json"));

        }

    }
    
}
