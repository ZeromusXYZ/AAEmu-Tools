using System.Collections.Generic;
using Newtonsoft.Json;

namespace AAEmu.DBEditor.Models.aaemu.webapi;

[JsonObject(MemberSerialization.OptIn)]
public class JsonAuctionLotList
{
    [JsonProperty]
    public List<JsonAuctionLot> Items { get; set; }
}