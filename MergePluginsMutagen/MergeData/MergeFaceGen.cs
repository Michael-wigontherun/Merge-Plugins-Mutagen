﻿
namespace MergePluginsMutagen.MergeData
{
    public partial class MergeDataFiles : IMergeInformation
    {
        public MergeDataFiles HandleFaceGenFiles()
        {
            HandleFaceGenFiles(Settings.pOutputFolder, Settings.iMoveFiles);
            HandleFaceGenFiles(Settings.pDataFolder, false);
            return this;
        }

        private void HandleFaceGenFiles(string dataPath, bool move = false)
        {
            Console.WriteLine("Copying FaceGen Files from: " + dataPath);
            foreach (var npcData in NPCAssetLinks)
            {
                foreach (var item in npcData.Value)
                {
                    if (File.Exists(Path.Combine(dataPath, item)))
                    //if(LocateFaceGenFile(dataFolder, item, responseData.Key, move))
                    {
                        //Console.WriteLine(Path.Combine(dataPath, item));
                        string? newPath = CopyFile(dataPath, item, npcData.Key, move);
                        if (newPath != null && !newPath.Equals(String.Empty) && !newPath.Equals(""))
                        { 
                            if (item.Contains(".Nif", StringComparison.OrdinalIgnoreCase))
                            {
                                if (MergeMap.TryGetValue(npcData.Key, out var newFormKey))
                                {
                                    ChangeNifInternalFolder(
                                        newPath,
                                    new FileComparableData(
                                        npcData.Key.ModKey.FileName,
                                        npcData.Key.IDString().TrimStart('0'),
                                        newFormKey.ModKey.FileName,
                                        newFormKey.IDString().TrimStart('0')
                                        ));
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Could not find FaceGen File: " + Path.GetFileName(item));
                    }
                }
            }
        }

        private void ChangeNifInternalFolder(string path, FileComparableData fileComparableData)
        {
            PatchNif(new NifFileWrapper(path),
                fileComparableData.OrgTrimmedID,
                fileComparableData.OrgFileName,
                fileComparableData.NewTrimmedID,
                fileComparableData.NewFileName
                ).SaveAs(path, true);
        }

        private NifFileWrapper PatchNif(NifFileWrapper nif, string OrgID, string OrgPluginName, string NewID, string NewPluginName)
        {
            for (var i = 0; i < nif.GetNumShapes(); ++i)
            {
                var shape = nif.GetShape(i);
                var subSurface = shape.SubsurfaceMap.ToLower();
                if (subSurface.Contains(OrgID, StringComparison.OrdinalIgnoreCase))
                {
                    subSurface = subSurface.Replace(OrgID, NewID, StringComparison.OrdinalIgnoreCase);
                    subSurface = subSurface.Replace(OrgPluginName, NewPluginName, StringComparison.OrdinalIgnoreCase);
                    shape.SubsurfaceMap = subSurface;
                }
            }
            return nif;
        }

        #region Obsolete
        //private void HandleBSAFaceGenGeomFiles()
        //{
        //    string facegenPath = Path.Combine(Settings.pOutputFolder, @"Meshes\Actors\Character\FaceGenData\FaceGeom");
        //    if (!Directory.Exists(facegenPath))
        //    {
        //        Console.WriteLine("No facegen folders to handle from extraced BSA's");
        //        return;
        //    }

        //    IEnumerable<string> files = Directory.EnumerateFiles(facegenPath, "*.nif", SearchOption.AllDirectories);

        //    if (!files.Any())
        //    {
        //        Console.WriteLine("No facegen files to handle from extraced BSA's");
        //    }

        //    foreach (string orgPath in files)
        //    {
        //        foreach (var data in ConvertedData)
        //        {
        //            if (!orgPath.Contains(data.OrgFileName, StringComparison.OrdinalIgnoreCase)) continue;
        //            if (!orgPath.Contains(data.OrgTrimmedID, StringComparison.OrdinalIgnoreCase)) continue;
        //            string newPath = orgPath.Replace(data.OrgFileName, data.NewFileName, StringComparison.OrdinalIgnoreCase);
        //            newPath = newPath.Replace(data.OrgTrimmedID, data.NewTrimmedID, StringComparison.OrdinalIgnoreCase);

        //            Directory.CreateDirectory(Path.GetDirectoryName(newPath)!);
        //            if (Settings.iMoveFiles)
        //            {
        //                File.Move(orgPath, newPath, true);
        //            }
        //            else
        //            {
        //                File.Copy(orgPath, newPath, true);
        //            }

        //            ChangeNifInternalFolder(newPath, data);
        //        }
        //    }
        //}

        //private void HandleDataFaceGenGeomFiles()
        //{
        //    string facegenPath = Path.Combine(Settings.pDataFolder, @"Meshes\Actors\Character\FaceGenData\FaceGeom");
        //    //Console.WriteLine(facegenPath);
        //    if (!Directory.Exists(facegenPath))
        //    {
        //        Console.WriteLine("No facegen folder at all in Data Folder");
        //        return;
        //    }

        //    foreach (var key in MergeModKeys)
        //    {
        //        string facegenPathKey = Path.Combine(Settings.pDataFolder, @"Meshes\Actors\Character\FaceGenData\FaceGeom", key.FileName);
        //        if (!Directory.Exists(facegenPathKey))
        //        {
        //            Console.WriteLine($"No facegen folder for {key.FileName}");
        //            continue;
        //        }

        //        IEnumerable<string> files = Directory.EnumerateFiles(facegenPathKey, "*.nif", SearchOption.AllDirectories);

        //        if (!files.Any())
        //        {
        //            Console.WriteLine($"No facegen files to handle from {key.FileName}");
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

        //                ChangeNifInternalFolder(newPath, data);
        //            }
        //        }
        //    }
        //}

        //private void HandleBSAFaceGenTintFiles()
        //{
        //    string facegenPath = Path.Combine(Settings.pOutputFolder, @"Textures\Actors\Character\FaceGenData\FaceTint");
        //    if (!Directory.Exists(facegenPath))
        //    {
        //        Console.WriteLine("No FaceTint folders to handle from extraced BSA's");
        //        return;
        //    }

        //    IEnumerable<string> files = Directory.EnumerateFiles(facegenPath, "*.dds", SearchOption.AllDirectories);

        //    if (!files.Any())
        //    {
        //        Console.WriteLine("No FaceTint files to handle from extraced BSA's");
        //    }

        //    foreach (string orgPath in files)
        //    {
        //        foreach (var data in ConvertedData)
        //        {
        //            if (!orgPath.Contains(data.OrgFileName, StringComparison.OrdinalIgnoreCase)) continue;
        //            if (!orgPath.Contains(data.OrgTrimmedID, StringComparison.OrdinalIgnoreCase)) continue;
        //            string newPath = orgPath.Replace(data.OrgFileName, data.NewFileName, StringComparison.OrdinalIgnoreCase);
        //            newPath = newPath.Replace(data.OrgTrimmedID, data.NewTrimmedID, StringComparison.OrdinalIgnoreCase);

        //            Directory.CreateDirectory(Path.GetDirectoryName(newPath)!);
        //            if (Settings.iMoveFiles)
        //            {
        //                File.Move(orgPath, newPath, true);
        //            }
        //            else
        //            {
        //                File.Copy(orgPath, newPath, true);
        //            }
        //        }
        //    }
        //}

        //private void HandleDataFaceGenTintFiles()
        //{
        //    string facegenPath = Path.Combine(Settings.pDataFolder, @"Textures\Actors\Character\FaceGenData\FaceTint");
        //    //Console.WriteLine(facegenPath);
        //    if (!Directory.Exists(facegenPath))
        //    {
        //        Console.WriteLine("No FaceTint folder at all in Data Folder");
        //        return;
        //    }

        //    foreach (var key in MergeModKeys)
        //    {
        //        string facegenPathKey = Path.Combine(Settings.pDataFolder, @"Textures\Actors\Character\FaceGenData\FaceTint", key.FileName);
        //        if (!Directory.Exists(facegenPathKey))
        //        {
        //            Console.WriteLine($"No FaceTint folder for {key.FileName}");
        //            continue;
        //        }

        //        IEnumerable<string> files = Directory.EnumerateFiles(facegenPathKey, "*.dds", SearchOption.AllDirectories);

        //        if (!files.Any())
        //        {
        //            Console.WriteLine($"No FaceTint files to handle from {key.FileName}");
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
    }
}
