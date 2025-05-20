using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MergePluginsMutagen
{
    public class Settings
    {
        public static Settings GetDefaultLocation()
        {
            return new Settings("MergePluginsMutagen.ini");
        }

        public Settings() { }

        public Settings(string settingsIniPath)
        {
            string RemoveExtraSlash(string s)
            {
                s = s.TrimEnd('/');
                s = s.TrimEnd('\\');
                s = s.Replace("\\\\", "\\");
                return s;
            }

            IConfiguration config = new ConfigurationBuilder()
                .AddIniFile(settingsIniPath)
                .Build();

            IConfigurationSection paths = config.GetSection("paths");
            pDataFolder = RemoveExtraSlash(paths["pDataFolder"]!);
            pOutputFolder = RemoveExtraSlash(paths["pOutputFolder"]!);
            pBSABrowser = RemoveExtraSlash(paths["pBSABrowser"]!);
            pPluginstxt = RemoveExtraSlash(paths["pPluginstxt"]!);

            IConfigurationSection Processing = config.GetSection("Processing");
            iMoveFiles = bool.Parse(Processing["iMoveFiles"]!);
            iTranslations = Processing["iTranslations"]!.Split(',', StringSplitOptions.TrimEntries);

            IConfigurationSection OpenFiles = config.GetSection("OpenFiles");
            oDefaultTxtProgram = RemoveExtraSlash(OpenFiles["oDefaultTxtProgram"]!);
            oAsVSCWorkspace = bool.Parse(OpenFiles["oAsVSCWorkspace"]!);
            oMergeFolder = bool.Parse(OpenFiles["oMergeFolder"]!);
            oMergeJson = bool.Parse(OpenFiles["oMergeJson"]!);
            oZMergejson = bool.Parse(OpenFiles["oZMergejson"]!);
            oPluginsTXT = bool.Parse(OpenFiles["oPluginsTXT"]!);

            IConfigurationSection mo2 = config.GetSection("MO2");
            mSeperateFolders = bool.Parse(mo2["mSeperateFolders"]!);
            mModsPath = RemoveExtraSlash(mo2["mModsPath"]!);

            IConfigurationSection Advanced = config.GetSection("Advanced");
            aDisableNavigationMeshInfoMapsCheck = bool.Parse(Advanced["aDisableNavigationMeshInfoMapsCheck"]!);
            aDisableInvalidateJustOverrideEverything = bool.Parse(Advanced["aDisableInvalidateJustOverrideEverything"]!);




            LoadWorkspace();
        }

        //[paths]
        public string pDataFolder = "";
        public string pOutputFolder = "";
        public string pBSABrowser = "";
        public string pPluginstxt = "";

        //[Processing]
        public bool iMoveFiles = false;
        public string[] iTranslations = new string[]
        {
            "english"
        };

        //[OpenFiles]
        public string oDefaultTxtProgram = "";
        public bool oAsVSCWorkspace = false;
        public bool oMergeFolder = false;
        public bool oMergeJson = false;
        public bool oZMergejson = false;
        public bool oPluginsTXT = false;

        //[MO2]
        public bool mSeperateFolders = false;
        public string mModsPath = "";

        //[Advanced]
        public bool aDisableNavigationMeshInfoMapsCheck = false;
        public bool aDisableInvalidateJustOverrideEverything = false;


        private string workspacePath = "merges.code-workspace";
        private HashSet<string> Paths = new();

        public void AddOpenFile(string path)
        {
            if (oAsVSCWorkspace)
            {
                Paths.Add(Path.GetDirectoryName(path)!);
            }
            else Paths.Add(path);
        }

        public void AddOpenDirectory(string path)
        {
            if (!oAsVSCWorkspace) return;
            
            Paths.Add(path);
        }

        public void OpenDefaultTXTProgram()
        {
            if(!File.Exists(oDefaultTxtProgram)) return;

            string argumetents = "";
            if (oAsVSCWorkspace)
            {
                VSCWorkSpace workspace = new();
                foreach (string path in Paths)
                {
                    workspace.folders.Add(new Folders(path));
                }
                File.WriteAllText(workspacePath, JsonSerializer.Serialize(workspace, new JsonSerializerOptions
                {
                    WriteIndented = true
                }));

                argumetents = Path.GetFullPath(workspacePath);
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (string path in Paths)
                {
                    stringBuilder.Append($"\"{path}\" ");
                }

                if (File.Exists(pPluginstxt))
                {
                    if (oPluginsTXT) stringBuilder.Append($"\"{pPluginstxt}\" ");
                }
                argumetents = stringBuilder.ToString();
            }

            Process.Start($"\"{oDefaultTxtProgram}\" \"{argumetents}\"");
        }

        private void LoadWorkspace()
        {
            if (!oAsVSCWorkspace) return;
            if (!File.Exists(workspacePath)) return;

            VSCWorkSpace workSpace = JsonSerializer.Deserialize<VSCWorkSpace>(File.ReadAllText(workspacePath))!;
            foreach(Folders path in workSpace.folders)
            {
                Paths.Add(path.path);
            }
        }

        public class VSCWorkSpace
        {
            [JsonInclude]
            public List<Folders> folders = new();

            [JsonInclude]
            public Settings settings = new();

            
            public class Settings { }
        }
        public class Folders
        {
            [JsonInclude]
            public string path = "";

            public Folders(string path)
            {
                this.path = path;
            }
        }
    }
}
