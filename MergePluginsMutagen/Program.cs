using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using MergePluginsMutagen.MergePluginClass;
using MergePluginsMutagen.MergeData.MergeDataClass;
using System.Collections.Generic;

namespace MergePluginsMutagen
{
    internal static class Program
    {
        internal static Settings Settings = Settings.GetDefaultLocation();

        internal static void Main(string[] args)
        {
            args = new string[] { "TemsPatchesMerge.esp.txt" };
            try
            {
                if(args.Length >= 2) Settings = new Settings(args[1]);
                string mergeModName = Path.GetFileName(args[0].Replace(".txt", ""));

                List<ModKey> pluginNameList = WillNotMergeCheck(File.ReadAllLines(args[0]));

                if(pluginNameList.Count <= 1)
                {
                    Console.WriteLine("Can't Merge anything.");
                    Console.WriteLine("Press Enter to Close...");
                    Console.ReadLine();
                    return;
                }

                if (Settings.mSeperateFolders)
                {
                    Settings.pOutputFolder = Path.Combine(Settings.mModsPath, Path.GetFileNameWithoutExtension(mergeModName));
                    Directory.CreateDirectory(Settings.pOutputFolder);
                }

                var mergeMap = new MergePlugin(mergeModName, pluginNameList, settings: Settings)
                    .Build();

                new MergeDataFiles(pluginNameList, mergeMap, settings: Settings)
                    .ExtractData()
                    .HandleVoiceFiles()
                    .HandleFaceGenFiles();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.WriteLine("Press Enter to Close...");
            Console.ReadLine();
        }

        internal static List<ModKey> WillNotMergeCheck(string[] pluginNameListArr)
        {
            List<ModKey> pluginNameList = new();
            HashSet<string> WontMergeList = new(StringComparer.OrdinalIgnoreCase)
            {
                "Skyrim.esm",
                "Update.esm",
                "Dawnguard.esm",
                "HearthFires.esm",
                "Dragonborn.esm"
            };

            foreach (string pluginName in pluginNameListArr)
            {
                if(WontMergeList.Contains(pluginName)) continue;

                pluginNameList.Add(ModKey.FromFileName(pluginName));
            }

            return pluginNameList;
        }
    }
}