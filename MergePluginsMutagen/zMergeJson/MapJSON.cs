using Mutagen.Bethesda.Plugins;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace MergePluginsMutagen.zMergeJson
{
    public class MapJSON
    {
        public Dictionary<string, Dictionary<string, string>> Map = new();

        public MapJSON(Dictionary<FormKey, FormKey> map, List<ModKey> modKeys)
        {
            foreach (var key in modKeys)
            {
                Map.Add(key.FileName, new Dictionary<string, string>());
            }
            foreach (var keyPair in map)
            {
                Map[keyPair.Key.ModKey.FileName].Add(keyPair.Key.IDString(), keyPair.Value.IDString());
            }
        }

        internal void Outout(string path)
        {
            File.WriteAllText(path, JsonSerializer.Serialize(Map, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            }));
        }
    }
}
