namespace MergePluginsMutagen.MergeData
{
    public partial class MergeDataFiles : IMergeInformation
    {
        public MergeDataFiles HandleTranslationFiles(string mergeNameWithoutExtension)
        {
            string translationDirectory = "interface\\translations";
            foreach (string lang in Settings.iTranslations)
            {
                List<string> lines = new List<string>();
                foreach (var key in MergeModKeys)
                {
                    string langFileName = $"{key.Name}_{lang}.txt";
                    string path = Path.Combine(Settings.pDataFolder, translationDirectory, langFileName);
                    if (File.Exists(path))
                    {
                        lines.AddRange(File.ReadAllLines(path));
                        continue;
                    }
                    path = Path.Combine(Settings.pOutputFolder, translationDirectory, langFileName);
                    if (File.Exists(path))
                    {
                        lines.AddRange(File.ReadAllLines(path));
                    }
                }
                Directory.CreateDirectory(Path.Combine(Settings.pOutputFolder, translationDirectory));
                File.WriteAllLines(Path.Combine(Settings.pOutputFolder, translationDirectory, $"{mergeNameWithoutExtension}_{lang}.txt"), lines);
            }

            return this;
        }

    }
}
