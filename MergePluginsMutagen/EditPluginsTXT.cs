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
                try
                {
                    if (pluginNameList.Contains(ModKey.FromFileName(array[i].TrimStart('*'))))
                    {
                        array[i] = array[i].TrimStart('*');
                    }
                }
                catch (ArgumentException) { }
            }
        }
    }
}
