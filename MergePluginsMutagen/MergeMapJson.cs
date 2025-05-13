using Mutagen.Bethesda.Plugins;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MergePluginsMutagen
{
    public class MergeMapJson
    {
        [JsonInclude, JsonPropertyOrder(0)]
        public bool ContainsMergedPluginsHoldingNavMap;
        [JsonInclude, JsonPropertyOrder(1)]
        public string MergeName;
        [JsonInclude, JsonPropertyOrder(2)]
        public List<MapKeyJson> MapKeysJson;
        [JsonInclude, JsonPropertyOrder(3)]
        public List<ModKey> MergeModKeys;

        public MergeMapJson()
        {
            MergeName = "";
            MapKeysJson = new();
            MergeModKeys = new();
            ContainsMergedPluginsHoldingNavMap = false;
        }
        
        public MergeMapJson(Dictionary<FormKey, FormKey> MergeMap, string mergeName, List<ModKey> mergeModKeys, bool containsMergedPluginsHoldingNavMap = false)
        {
            MergeName = mergeName;
            MapKeysJson = new();
            foreach (var m in MergeMap)
            {
                MapKeysJson.Add(new(m.Key.ToString(), m.Value.ToString()));
            }
            MergeModKeys = mergeModKeys;
            ContainsMergedPluginsHoldingNavMap = containsMergedPluginsHoldingNavMap;
        }

        public void Output(string path)
        {
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
