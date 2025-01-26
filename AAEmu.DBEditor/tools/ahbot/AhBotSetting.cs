using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace AAEmu.DBEditor.tools.ahbot;

public class AhBotSetting
{
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
    public string SettingsName { get; set; } = "Default";
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
    public List<AhBotEntry> Items { get; set; } = [];
}