using Mutagen.Bethesda.Plugins;

namespace MergePluginsMutagen
{
    public class IMergeInformation
    {
        public List<ModKey> MergeModKeys = new();

        public Dictionary<FormKey, FormKey> MergeMap = new();

        public Dictionary<FormKey, HashSet<string>> ResponseAssetLinks = new();

        public Dictionary<FormKey, HashSet<string>> NPCAssetLinks = new();

        public Settings Settings = new();

        public IMergeInformation() { }

        public IMergeInformation(List<ModKey> mergeModKeys, Dictionary<FormKey, FormKey> mergeMap, Dictionary<FormKey, HashSet<string>> responseAssetLinks, Dictionary<FormKey, HashSet<string>> nPCAssetLinks, Settings settings)
        {
            MergeModKeys = mergeModKeys;
            MergeMap = mergeMap;
            ResponseAssetLinks = responseAssetLinks;
            NPCAssetLinks = nPCAssetLinks;
            Settings = settings;
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
