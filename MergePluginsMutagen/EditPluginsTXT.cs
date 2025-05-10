using Mutagen.Bethesda.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergePluginsMutagen
{
    public static class EditPluginsTXT
    {
        public static void ChangePluginsTXT(HashSet<ModKey> pluginNameList, string pluginsTXTPath)
        {
            string[] array = File.ReadAllLines(pluginsTXTPath);
            for (int i = 0; i < array.Length; i++)
            {
                string line = array[i].TrimStart('*');
                try
                {
                    if (pluginNameList.Contains(ModKey.FromFileName(line)))
                    {
                        array[i] = line;
                    }
                }
                catch (ArgumentException) { }
            }
            File.WriteAllLines(pluginsTXTPath, array);
        }
    }
}
