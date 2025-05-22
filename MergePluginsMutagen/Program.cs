using Mutagen.Bethesda.Plugins;
using MergePluginsMutagen.MergePluginClass;
using MergePluginsMutagen.MergeData;

namespace MergePluginsMutagen
{
    internal static class Program
    {
        internal static Settings Settings = new();

        internal static bool Pause = true;

        internal static void Main(string[] args)
        {
            //args = new string[]
            //{
            //    "C:\\Modding\\SkyrimSE\\_Notes\\Merges\\TemsPatchesMerge.esp.txt",
            //    "C:\\Modding\\SkyrimSE\\_Notes\\Merges\\_MergePluginsMutagen.ini"
            //};
            try
            {
                if (args.Length >= 2)
                {
                    if(File.Exists(args[1]))
                    {
                        Console.WriteLine("Getting Settings from custom Location");
                        Settings = new Settings(args[1]);
                    }
                    else if(args[1].Equals("-np"))
                    {
                        Console.WriteLine("Setting No Pause");
                        Pause = false;
                    }
                    if (args.Length >= 3)
                    {
                        Console.WriteLine("Setting No Pause");
                        if (args[2].Equals("-np"))
                        {
                            Pause = false;
                        }
                    }
                }
                else Settings.GetDefaultLocation();

                string mergeModName = Path.GetFileName(args[0].Replace(".txt", ""));

                List<ModKey> pluginNameList = FilterMerge.WillNotMergeCheck(mergeModName, File.ReadAllLines(args[0]), Settings, out HashSet<ModKey> dontChangeFormIDs);

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

                Console.WriteLine("Starting Merge");
                var MergeInformation = new MergePlugin(mergeModName, pluginNameList, dontChangeFormIDs: dontChangeFormIDs, settings: Settings)
                    .Build();

                Settings = new MergeDataFiles(MergeInformation)
                    .ExtractData()
                    .HandleVoiceFiles()
                    .HandleFaceGenFiles()
                    .HandleTranslationFiles(Path.GetFileNameWithoutExtension(mergeModName))
                    .ReturnSettings();

                EditPluginsTXT.ChangePluginsTXT(pluginNameList.ToHashSet(), Settings.pPluginstxt, dontChangeFormIDs);

                Settings.OpenDefaultTXTProgram();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace!.ToString());
                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine("Press Enter to Close...");
                Console.ReadLine();
                return;
            }

            if (Pause)
            {
                Console.WriteLine("Press Enter to Close...");
                Console.ReadLine();
            }
        }
    }
}