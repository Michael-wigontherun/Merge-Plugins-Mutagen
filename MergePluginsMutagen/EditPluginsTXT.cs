using Mutagen.Bethesda.Plugins;

namespace MergePluginsMutagen
{
    public static class EditPluginsTXT
    {
        public static void ChangePluginsTXT(HashSet<ModKey> pluginNameList, string pluginsTXTPath, HashSet<ModKey> dontDisable)
        {
            Console.WriteLine("Disabling merged plugins inside plugins.txt");
            Console.WriteLine("Path: " + pluginsTXTPath);
            bool changed = false;
            string[] array = File.ReadAllLines(pluginsTXTPath);
            for (int i = 0; i < array.Length; i++)
            {
                if(!array[i].Contains('*')) continue;

                string line = array[i].TrimStart('*');
                try
                {
                    ModKey modKey = ModKey.FromFileName(line);
                    if(dontDisable.Contains(modKey)) continue;
                    
                    if (pluginNameList.Contains(modKey))
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
                var t = File.WriteAllLinesAsync(pluginsTXTPath, array);
                t.Wait();
                t.Dispose();
            }
            else Console.WriteLine("No changes to plugins.txt");
        }
    }
}
