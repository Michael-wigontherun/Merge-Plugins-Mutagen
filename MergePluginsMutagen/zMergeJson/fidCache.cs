﻿using Mutagen.Bethesda.Plugins;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace MergePluginsMutagen.zMergeJson
{
    internal class fidCache
    {
        public Dictionary<string, List<string>> Map = new();

        public fidCache(Dictionary<FormKey, FormKey> map, List<ModKey> modKeys)
        {
            foreach (var key in modKeys)
            {
                Map.Add(key.FileName, new List<string>());
            }
            foreach (var keyPair in map)
            {
                Map[keyPair.Key.ModKey.FileName].Add(keyPair.Key.IDString());
            }
        }

        internal void Output(string path)
        {
            File.WriteAllText(path, JsonSerializer.Serialize(Map, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            }));
        }
    }
}
