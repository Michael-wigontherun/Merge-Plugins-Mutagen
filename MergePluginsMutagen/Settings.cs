using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergePluginsMutagen
{
    public class Settings
    {
        public static Settings GetDefaultLocation()
        {
            return new Settings("MergePluginsMutagen.ini");
        }

        public Settings() { }

        public Settings(string settingsIniPath)
        {
            string RemoveExtraSlash(string s)
            {
                s = s.TrimEnd('/');
                s = s.TrimEnd('\\');
                s = s.Replace("\\\\", "\\");
                return s;
            }

            IConfiguration config = new ConfigurationBuilder()
                .AddIniFile(settingsIniPath)
                .Build();

            IConfigurationSection paths = config.GetSection("paths");
            pDataFolder = RemoveExtraSlash(paths["pDataFolder"]!);
            pOutputFolder = RemoveExtraSlash(paths["pOutputFolder"]!);
            pBSABrowser = RemoveExtraSlash(paths["pBSABrowser"]!);
            pPluginstxt = RemoveExtraSlash(paths["pPluginstxt"]!);

            IConfigurationSection Processing = config.GetSection("Processing");
            iMoveFiles = bool.Parse(Processing["iMoveFiles"]!);

            IConfigurationSection mo2 = config.GetSection("MO2");
            mSeperateFolders = bool.Parse(mo2["mSeperateFolders"]!);
            mModsPath = RemoveExtraSlash(mo2["mModsPath"]!);
        }

        //[paths]
        public string pDataFolder = "";
        public string pOutputFolder = "";
        public string pBSABrowser = "";
        public string pPluginstxt = "";

        //[Processing]
        public bool iMoveFiles = false;

        //[MO2]
        public bool mSeperateFolders = false;
        public string mModsPath = "";

    }
}
