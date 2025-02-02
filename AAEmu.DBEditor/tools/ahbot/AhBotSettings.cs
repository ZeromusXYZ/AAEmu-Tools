using Newtonsoft.Json;

namespace AAEmu.DBEditor.tools.ahbot;

[JsonObject(MemberSerialization.OptIn)]
public class AhBotSettings
{
    public static readonly string DefaultAhBotListingsFileName = "ahbot_listing.json";
    [JsonProperty] public string CharacterName { get; set; } = string.Empty;
    [JsonProperty] public string AccountName { get; set; } = string.Empty;
    [JsonProperty] public string ServerName { get; set; } = string.Empty;
    [JsonProperty] public string ListingsFile { get; set; } = DefaultAhBotListingsFileName;
    [JsonProperty] public long TotalEarned { get; set; }
    [JsonProperty] public long TotalSalesCount { get; set; }
}