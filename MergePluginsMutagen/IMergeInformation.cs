using Mutagen.Bethesda.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public IMergeInformation(List<ModKey> mergeModKeys, Dictionary<FormKey, FormKey> mergeMap, Dictionary<FormKey, HashSet<string>> responseAssetLinks, Settings settings)
        {
            MergeModKeys = mergeModKeys;
            MergeMap = mergeMap;
            this.ResponseAssetLinks = responseAssetLinks;
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
