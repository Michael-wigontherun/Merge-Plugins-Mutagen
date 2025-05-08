using Mutagen.Bethesda.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergePluginsMutagen
{
    public class IMergeInformationInterface
    {
        public List<ModKey> MergeModKeys = new();

        public Dictionary<FormKey, FormKey> MergeMap = new();


        public Settings Settings = new();
        public IMergeInformationInterface LoadSettings(string settingsIniPath)
        {
            Settings = new Settings(settingsIniPath);

            return this;
        }
        public IMergeInformationInterface AddSettings(Settings settings)
        {
            Settings = settings;

            return this;
        }
    }
}
