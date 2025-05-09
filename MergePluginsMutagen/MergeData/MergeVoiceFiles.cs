using Mutagen.Bethesda.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MergePluginsMutagen.MergeData
{
    public partial class MergeDataFiles : IMergeInformation
    {
        public MergeDataFiles HandleVoiceFiles()
        {
            HandleVoiceFiles(Settings.pOutputFolder, Settings.iMoveFiles);
            HandleVoiceFiles(Settings.pDataFolder, false);
            return this;
        }

        private void HandleVoiceFiles(string dataFolder, bool move = false)
        {
            Console.WriteLine("Copying Voice Files from: " + dataFolder);
            foreach(var responseData in ResponseAssetLinks)
            {
                foreach (var item in responseData.Value)
                {
                    if(LocateVoiceFile(dataFolder, item, responseData.Key, move))
                    {
                        Console.WriteLine("Could not find Voice File: " + Path.GetFileName(item));
                    }
                }
            }
        }

        private bool LocateVoiceFile(string dataPath, string soundFilePath, FormKey key, bool move = false)
        {
            bool located = false;
            string orgPath = Path.Combine(dataPath, soundFilePath);
            if (File.Exists(orgPath))
            {
                CopyFile(dataPath, soundFilePath, key, move);
                located = true;
            }
            orgPath = Path.ChangeExtension(orgPath,"wav");
            if (File.Exists(orgPath))
            {
                CopyFile(dataPath, soundFilePath, key, move);
                located = true;
            }
            orgPath = Path.ChangeExtension(orgPath, "lip");
            if (File.Exists(orgPath))
            {
                CopyFile(dataPath, soundFilePath, key, move);
                located = true;
            }
            return located;
        }

        #region Obsolete
        //private void HandleBSAVoiceFiles()
        //{
            //string voicePath = Path.Combine(Settings.pOutputFolder, @"sound\voice");
            //if (!Directory.Exists(voicePath))
            //{
            //    Console.WriteLine("No voice folders to handle from extraced BSA's");
            //    return;
            //}

            //IEnumerable<string> files = Directory.EnumerateFiles(voicePath, "*", SearchOption.AllDirectories);

            //if (!files.Any())
            //{
            //    Console.WriteLine("No voice files to handle from extraced BSA's");
            //}

            //foreach (string orgPath in files)
            //{
            //    foreach (var data in ConvertedData)
            //    {
            //        if (!orgPath.Contains(data.OrgFileName, StringComparison.OrdinalIgnoreCase)) continue;
            //        if (!orgPath.Contains(data.OrgTrimmedID, StringComparison.OrdinalIgnoreCase)) continue;
            //        string newPath = orgPath.Replace(data.OrgFileName, data.NewFileName, StringComparison.OrdinalIgnoreCase);
            //        newPath = newPath.Replace(data.OrgTrimmedID, data.NewTrimmedID, StringComparison.OrdinalIgnoreCase);

            //        Directory.CreateDirectory(Path.GetDirectoryName(newPath)!);
            //        if (Settings.iMoveFiles)
            //        {
            //            File.Move(orgPath, newPath, true);
            //        }
            //        else
            //        {
            //            File.Copy(orgPath, newPath, true);
            //        }
            //    }
            //}
        //}

        //private void HandleDataFolderVoiceFiles()
        //{
        //    string voicePath = Path.Combine(Settings.pDataFolder, @"sound\voice");
        //    if (!Directory.Exists(voicePath))
        //    {
        //        Console.WriteLine("No voice files at all in Data Folder");
        //        return;
        //    }

        //    foreach (var key in MergeModKeys)
        //    {
        //        string voicePathKey = Path.Combine(Settings.pDataFolder, @"sound\voice", key.FileName);
        //        if (!Directory.Exists(voicePathKey))
        //        {
        //            Console.WriteLine($"No voice folder for {key.FileName}");
        //            continue;
        //        }

        //        IEnumerable<string> files = Directory.EnumerateFiles(voicePathKey, "*", SearchOption.AllDirectories);

        //        if (!files.Any())
        //        {
        //            Console.WriteLine($"No voice files to handle from {key.FileName}");
        //            continue;
        //        }

        //        foreach (string orgPath in files)
        //        {
        //            foreach (var data in ConvertedData)
        //            {
        //                if (!data.OrgFileName.Equals(key.FileName, StringComparison.OrdinalIgnoreCase)) continue;
        //                if (!orgPath.Contains(data.OrgFileName, StringComparison.OrdinalIgnoreCase)) continue;
        //                if (!orgPath.Contains(data.OrgTrimmedID, StringComparison.OrdinalIgnoreCase)) continue;
        //                string newPath = orgPath.Replace(Settings.pDataFolder, Settings.pOutputFolder);
        //                newPath = newPath.Replace(data.OrgFileName, data.NewFileName, StringComparison.OrdinalIgnoreCase);
        //                newPath = newPath.Replace(data.OrgTrimmedID, data.NewTrimmedID, StringComparison.OrdinalIgnoreCase);

        //                Directory.CreateDirectory(Path.GetDirectoryName(newPath)!);
        //                File.Copy(orgPath, newPath, true);
        //            }
        //        }
        //    }
        //}
        #endregion Obsolete
    }//End Class
}
