using Mutagen.Bethesda.Plugins;

namespace MergePluginsMutagen
{
    public static class EditPluginsTXT
    {
        public static void ChangePluginsTXT(HashSet<ModKey> pluginNameList, string pluginsTXTPath)
        {
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

            if(changed) File.WriteAllLines(pluginsTXTPath, array);
        }
    }
}
