using Mutagen.Bethesda.Plugins;
using Noggog.StructuredStrings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MergePluginsMutagen.MergeData
{
    public partial class MergeDataFiles : IMergeInformation
    {
        public MergeDataFiles(IMergeInformation mergeInformation)
        {
            Settings = mergeInformation.Settings;
            MergeModKeys = mergeInformation.MergeModKeys;
            MergeMap = mergeInformation.MergeMap;
            ResponseAssetLinks = mergeInformation.ResponseAssetLinks;
            NPCAssetLinks = mergeInformation.NPCAssetLinks;
            //ConvertMap();
        }

        public MergeDataFiles(List<ModKey> pluginNameList, 
            Dictionary<FormKey, FormKey> mergeMap, 
            Dictionary<FormKey, HashSet<string>> responseAssetLinks, 
            Dictionary<FormKey, HashSet<string>> npcAssetLinks, 
            Settings? settings = null)
        {
            Settings = settings ?? Settings.GetDefaultLocation();
            MergeModKeys = pluginNameList;
            MergeMap = mergeMap;
            ResponseAssetLinks = responseAssetLinks;
            NPCAssetLinks = npcAssetLinks;
            //ConvertMap();
        }

        //private HashSet<FileComparableData> ConvertedData = new();

        //private MergeDataFiles ConvertMap()
        //{
        //    foreach (var data in MergeMap)
        //    {
        //        ConvertedData.Add(new FileComparableData(data.Key.ModKey.FileName, data.Key.IDString(), data.Value.ModKey.FileName, data.Value.IDString()));
        //    }
        //    return this;
        //}

        public MergeDataFiles ExtractData()
        {
            foreach (ModKey pluginName in MergeModKeys)
            {
                string bsaPath = GetBSAPath(pluginName.FileName, out var bsaTexPath);
                if (File.Exists(bsaPath))
                {
                    RunBSAB(bsaPath);
                }
                if (File.Exists(bsaTexPath))
                {
                    RunBSAB(bsaTexPath);
                }
            }
            return this;
        }

        private string GetBSAPath(string fileName, out string? bsaTexPath)
        {
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            string bsaPath = Path.Combine(Settings.pDataFolder, nameWithoutExtension + ".bsa");
            bsaTexPath = Path.Combine(Settings.pDataFolder, nameWithoutExtension + " - Textures.bsa");

            return bsaPath;
        }

        private void RunBSAB(string bsaPath)
        {
            Console.WriteLine($"Extracting: {bsaPath}");
            Process m = new Process();
            m.StartInfo.FileName = Settings.pBSABrowser;
            m.StartInfo.Arguments = $"-e -o \"{bsaPath}\" \"{Settings.pOutputFolder}\"";
            
            m.StartInfo.UseShellExecute = false;

            m.Start();

            m.WaitForExit();
            m.Dispose();
        }

        public string? CopyFile(string dataPath, string filePath, FormKey key, bool move = false)
        {
            if (MergeMap.TryGetValue(key, out var newFormKey))
            {
                string newPath = filePath.Replace(key.ModKey.FileName, newFormKey.ModKey.FileName, StringComparison.OrdinalIgnoreCase);
                newPath = newPath.Replace(key.IDString().TrimStart('0'), newFormKey.IDString().TrimStart('0'), StringComparison.OrdinalIgnoreCase);

                string orgPath = Path.Combine(dataPath, filePath);
                newPath = Path.Combine(Settings.pOutputFolder, newPath);

                Directory.CreateDirectory(Path.GetDirectoryName(newPath)!);
                if (move)
                {
                    File.Move(orgPath, newPath, true);
                }
                else
                {
                    File.Copy(orgPath, newPath, true);
                }

                return newPath;
            }
            return null;
        }

        public class FileComparableData
        {
            public FileComparableData(string orgFileName, string orgTrimmedID, string newFileName, string trimmedID)
            {
                OrgFileName = orgFileName;
                OrgTrimmedID = orgTrimmedID.TrimStart('0');
                NewFileName = newFileName;
                NewTrimmedID = trimmedID.TrimStart('0');
            }

            public string OrgFileName { get; private set; }
            public string OrgTrimmedID { get; private set; }

            public string NewFileName { get; private set; }
            public string NewTrimmedID { get; private set; }

            
        }
    }
}
