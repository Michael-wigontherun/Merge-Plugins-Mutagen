using Mutagen.Bethesda.Plugins;

namespace MergePluginsMutagen
{
    public class IMergeInformation
    {
        public List<ModKey> MergeModKeys;

        public Dictionary<FormKey, FormKey> MergeMap;

        public Dictionary<FormKey, HashSet<string>> ResponseAssetLinks;

        public Dictionary<FormKey, HashSet<string>> NPCAssetLinks;

        public Settings Settings;

        public IMergeInformation()
        {
            MergeModKeys = new();
            MergeMap = new();
            ResponseAssetLinks = new();
            NPCAssetLinks = new();
            Settings = new();
        }

        public IMergeInformation(List<ModKey> mergeModKeys, Dictionary<FormKey, FormKey> mergeMap, Dictionary<FormKey, HashSet<string>> responseAssetLinks, Dictionary<FormKey, HashSet<string>> nPCAssetLinks, Settings settings)
        {
            MergeModKeys = mergeModKeys;
            MergeMap = mergeMap;
            ResponseAssetLinks = responseAssetLinks;
            NPCAssetLinks = nPCAssetLinks;
            Settings = settings;
        }

        public IMergeInformation(IMergeInformation mergeInfo)
        {
            MergeModKeys = mergeInfo.MergeModKeys;
            MergeMap = mergeInfo.MergeMap;
            ResponseAssetLinks = mergeInfo.ResponseAssetLinks;
            NPCAssetLinks = mergeInfo.NPCAssetLinks;
            Settings = mergeInfo.Settings;
        }

        public IMergeInformation LoadSettings(string settingsIniPath)
        {
            Settings = new Settings(settingsIniPath);

            return this;
        }
        public IMergeInformation AddSettings(Settings settings)
        {
            Settings = settings;

            return this;
        }
    }
}
