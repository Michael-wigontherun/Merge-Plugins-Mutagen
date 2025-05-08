using Mutagen.Bethesda.Plugins;
using System.Security.Cryptography;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MergePluginsMutagen.zMergeJson
{
    public class mergeJson
    {
        [JsonInclude]
        public string name = "";
        [JsonInclude]
        public string filename = "";
        [JsonInclude]
        public string method = "clean";
        [JsonInclude]
        public List<string> loadOrder = new List<string>();
        [JsonInclude]
        public string archiveAction = "Extract";
        [JsonInclude]
        public bool buildMergedArchive = false;
        [JsonInclude]
        public bool useGameLoadOrder = false;
        [JsonInclude]
        public bool handleFaceData = true;
        [JsonInclude]
        public bool handleVoiceData = true;
        [JsonInclude]
        public bool handleBillboards = false;
        [JsonInclude]
        public bool handleStringFiles = false;
        [JsonInclude]
        public bool handleTranslations = false;
        [JsonInclude]
        public bool handleIniFiles = false;
        [JsonInclude]
        public bool handleDialogViews = false;
        [JsonInclude]
        public bool copyGeneralAssets = true;
        [JsonInclude]
        public CustomMetadata customMetadata = new CustomMetadata();
        [JsonInclude]
        public string dateBuilt = "";
        [JsonInclude]
        public List<Plugins> plugins = new();

        public mergeJson(string filename, List<string> loadOrder, string dateBuilt, List<ModKey> modKeys)
        {
            this.name = Path.GetFileNameWithoutExtension(filename);
            this.filename = filename;
            this.loadOrder = loadOrder;
            this.dateBuilt = dateBuilt;
            foreach (var plugin in modKeys)
            {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(Path.Combine(Program.Settings.pDataFolder, plugin.FileName));
                    byte[] hashBytes = md5.ComputeHash(inputBytes);

                    plugins.Add(new Plugins()
                    {
                        filename = plugin.FileName,
                        hash = Convert.ToHexString(hashBytes),
                        dataFolder = Program.Settings.pDataFolder
                    });
                }
            }
        }

        public void Output(string path)
        {
            File.WriteAllText(path, JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            }));
        }
    }
    public class CustomMetadata { }
    public class Plugins
    {
        [JsonInclude]
        public string filename = "";
        [JsonInclude]
        public string hash = "";
        [JsonInclude]
        public string dataFolder = "";

    }
}
