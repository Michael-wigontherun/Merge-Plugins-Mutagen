using Mutagen.Bethesda.Plugins;

namespace MergePluginsMutagen
{
    public static class EditPluginsTXT
    {
        public static void ChangePluginsTXT(HashSet<ModKey> pluginNameList, string pluginsTXTPath)
        {
            Console.WriteLine("Disabling merged plugins inside plugins.txt");
            Console.WriteLine("Path: " + pluginsTXTPath);
            bool changed = false;
            string[] array = File.ReadAllLines(pluginsTXTPath);
            for (int i = 0; i < array.Length; i++)
            {
                string line = array[i].TrimStart('*');
                try
                {
                    if (pluginNameList.Contains(ModKey.FromFileName(line)))
                    {
                        array[i] = line;
                        changed = true;
                    }
                }
                catch (ArgumentException) { }
            }

            if (changed)
            {
                Console.WriteLine("Wrote to plugins.txt");
                File.WriteAllLines(pluginsTXTPath, array);
            }
            else Console.WriteLine("No changes to plugins.txt");
        }
    }
}
