using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using AAEmu.DBEditor.utils;
using Newtonsoft.Json;

namespace AAEmu.DBEditor.data;


[JsonObject]
public class ProgramSettings
{
    [JsonIgnore]
    public static ProgramSettings Instance { get; set; } = new ProgramSettings();

    public static ProgramSettings LoadFromFile(string fileName)
    {
        if (!File.Exists(fileName))
            return new ProgramSettings();
        var jsonData = File.ReadAllText(fileName);
        if (!JsonHelper.TryDeserializeObject<ProgramSettings>(jsonData, out var newSettings, out var ex))
        {
            return null;
        }

        return newSettings;
    }

    public static bool SaveToFile(ProgramSettings settings, string fileName)
    {
        var res = JsonConvert.SerializeObject(settings);
        try
        {
            File.WriteAllText(fileName, res);
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        return false;
    }

    public static string GetSettingsFolder()
    {
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ZeromusXYZ", "AAEmu.DBEditor");
    }

    public string ClientPak { get; set; } = string.Empty;
    public string ServerDb { get; set; } = string.Empty;
    public string ClientDb { get; set; } = string.Empty; // Unused
    public string MySqlDb { get; set; } = "127.0.0.1:3306";
    public string MySqlUser { get; set; } = "root";
    public string MySqlPassword { get; set; } = string.Empty;
    public string MySqlLogin { get; set; } = "aaemu_login";
    public string MySqlGame { get; set; } = "aaemu_game";
    public string ClientLanguage { get; set; } = "en_us";
}