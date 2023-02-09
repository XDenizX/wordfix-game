using System.IO;
using Models;
using Newtonsoft.Json;

namespace Helpers
{
    public static class SettingsHelper
    {
        public static GameSettings Load(string filepath)
        {
            var text = File.ReadAllText(filepath);
            return JsonConvert.DeserializeObject<GameSettings>(text);
        }
    
        public static bool TryLoad(string filepath, out GameSettings settings)
        {
            try
            {
                var text = File.ReadAllText(filepath);
                settings = JsonConvert.DeserializeObject<GameSettings>(text);
                return true;
            }
            catch
            {
                settings = null;
                return false;
            }
        }

        public static void Save(GameSettings setting, string filepath)
        {
            var jsonData = JsonConvert.SerializeObject(setting, Formatting.Indented);
            File.WriteAllText(filepath, jsonData);
        }
    }
}