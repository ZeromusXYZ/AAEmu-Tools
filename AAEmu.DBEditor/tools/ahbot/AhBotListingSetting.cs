using System.Collections.Generic;
using Newtonsoft.Json;

namespace AAEmu.DBEditor.tools.ahbot;

[JsonObject(MemberSerialization.OptIn)]
public class AhBotListingSetting
{
    [JsonProperty] public string Name { get; set; } = "Default";
    [JsonProperty] public List<AhBotEntry> Items { get; set; } = [];
}