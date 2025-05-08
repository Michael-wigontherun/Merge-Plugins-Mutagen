using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergePluginsMutagen.MergeData
{
    public partial class MergeDataFiles : IMergeInformationInterface
    {
        public MergeDataFiles HandleVoiceFiles()
        {
            HandleBSAVoiceFiles();
            HandleDataFolderVoiceFiles();
            return this;
        }

        private void HandleBSAVoiceFiles()
        {
            string voicePath = Path.Combine(Settings.pOutputFolder, @"sound\voice");
            if (!Directory.Exists(voicePath))
            {
                Console.WriteLine("No voice folders to handle from extraced BSA's");
                return;
            }

            IEnumerable<string> files = Directory.EnumerateFiles(voicePath, "*", SearchOption.AllDirectories);

            if (!files.Any())
            {
                Console.WriteLine("No voice files to handle from extraced BSA's");
            }

            foreach (string orgPath in files)
            {
                foreach (var data in ConvertedData)
                {
                    if (!orgPath.Contains(data.OrgFileName, StringComparison.OrdinalIgnoreCase)) continue;
                    if (!orgPath.Contains(data.OrgTrimmedID, StringComparison.OrdinalIgnoreCase)) continue;
                    string newPath = orgPath.Replace(data.OrgFileName, data.NewFileName, StringComparison.OrdinalIgnoreCase);
                    newPath = newPath.Replace(data.OrgTrimmedID, data.NewTrimmedID, StringComparison.OrdinalIgnoreCase);

                    Directory.CreateDirectory(Path.GetDirectoryName(newPath)!);
                    if (Settings.iMoveFiles)
                    {
                        File.Move(orgPath, newPath, true);
                    }
                    else
                    {
                        File.Copy(orgPath, newPath, true);
                    }
                }
            }
        }

        private void HandleDataFolderVoiceFiles()
        {
            string voicePath = Path.Combine(Settings.pDataFolder, @"sound\voice");
            if (!Directory.Exists(voicePath))
            {
                Console.WriteLine("No voice files at all in Data Folder");
                return;
            }

            foreach(var key in MergeModKeys)
            {
                string voicePathKey = Path.Combine(Settings.pDataFolder, @"sound\voice" , key.FileName);
                if (!Directory.Exists(voicePathKey))
                {
                    Console.WriteLine($"No voice folder for {key.FileName}");
                    continue;
                }

                IEnumerable<string> files = Directory.EnumerateFiles(voicePathKey, "*", SearchOption.AllDirectories);

                if (!files.Any())
                {
                    Console.WriteLine($"No voice files to handle from {key.FileName}");
                    continue;
                }

                foreach (string orgPath in files)
                {
                    foreach (var data in ConvertedData)
                    {
                        if (!data.OrgFileName.Equals(key.FileName, StringComparison.OrdinalIgnoreCase)) continue;
                        if (!orgPath.Contains(data.OrgFileName, StringComparison.OrdinalIgnoreCase)) continue;
                        if (!orgPath.Contains(data.OrgTrimmedID, StringComparison.OrdinalIgnoreCase)) continue;
                        string newPath = orgPath.Replace(Settings.pDataFolder, Settings.pOutputFolder);
                        newPath = newPath.Replace(data.OrgFileName, data.NewFileName, StringComparison.OrdinalIgnoreCase);
                        newPath = newPath.Replace(data.OrgTrimmedID, data.NewTrimmedID, StringComparison.OrdinalIgnoreCase);

                        Directory.CreateDirectory(Path.GetDirectoryName(newPath)!);
                        File.Copy(orgPath, newPath, true);
                    }
                }
            }
        }
    }
}
