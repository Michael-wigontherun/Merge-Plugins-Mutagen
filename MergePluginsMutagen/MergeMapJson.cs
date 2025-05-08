using Mutagen.Bethesda.Plugins;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MergePluginsMutagen
{
    public class MergeMapJson
    {
        [JsonInclude]
        public string MergeName = "";
        [JsonInclude]
        public List<MapKeyJson> MapKeysJson = new();
        [JsonInclude]
        public List<ModKey> MergeModKeys = new();

        public MergeMapJson() { }
        
        public MergeMapJson(Dictionary<FormKey, FormKey> MergeMap, string mergeName, List<ModKey> mergeModKeys)
        {
            MergeName = mergeName;
            foreach(var m in MergeMap)
            {
                MapKeysJson.Add(new(m.Key.ToString(), m.Value.ToString()));
            }
            MergeModKeys = mergeModKeys;
        }

        public void Output(string path)
        {
            Console.WriteLine(path);
            Directory.CreateDirectory(path.Replace(Path.GetFileName(path),""));
            File.WriteAllText(path, JsonSerializer.Serialize(this, new JsonSerializerOptions { 
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping 
            }));
        }

        public static Dictionary<FormKey, FormKey> Load(string path)
        {
            Dictionary<FormKey, FormKey> mergeMap = new();

            MergeMapJson mergeMapJson = JsonSerializer.Deserialize<MergeMapJson>(path)!;

            foreach(var m in mergeMapJson.MapKeysJson)
            {
                mergeMap.Add(FormKey.Factory(m.OrigionalKey), FormKey.Factory(m.MergedKey));
            }

            return mergeMap;
        }

        public static Dictionary<FormKey, FormKey> Load(string path, out string MergeName)
        {
            Dictionary<FormKey, FormKey> mergeMap = new();

            MergeMapJson mergeMapJson = JsonSerializer.Deserialize<MergeMapJson>(path)!;
            MergeName = mergeMapJson.MergeName;

            foreach (var m in mergeMapJson.MapKeysJson)
            {
                mergeMap.Add(FormKey.Factory(m.OrigionalKey), FormKey.Factory(m.MergedKey));
            }

            return mergeMap;
        }
    }

    public class MapKeyJson
    {
        [JsonInclude]
        public string OrigionalKey = "";
        [JsonInclude]
        public string MergedKey = "";

        public MapKeyJson() { }

        public MapKeyJson(string origionalKey, string mergedKey)
        {
            OrigionalKey = origionalKey;
            MergedKey = mergedKey;
        }
    }
}
