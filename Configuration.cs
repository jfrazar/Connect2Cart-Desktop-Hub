using System;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace Connect2Cart_Desktop_Hub
{
    public class ConfigurationData
    {
        public string Scale { get; set; }
        public string Printer { get; set; }
    }

    public static class Configuration
    {
        private static readonly string ConfigPath = Path.Combine(
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "",
            "config.json"
        );

        private static ConfigurationData _current;

        public static ConfigurationData Current
        {
            get
            {
                if (_current == null)
                {
                    _current = Load();
                }
                return _current;
            }
        }

        public static void Save(ConfigurationData data)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ConfigPath, json);
        }

        private static ConfigurationData Load()
        {
            if (!File.Exists(ConfigPath))
                return new ConfigurationData();

            var json = File.ReadAllText(ConfigPath);
            return JsonSerializer.Deserialize<ConfigurationData>(json) ?? new ConfigurationData();
        }
    }
}
