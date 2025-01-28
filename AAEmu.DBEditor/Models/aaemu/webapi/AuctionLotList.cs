using System.Collections.Generic;
using Newtonsoft.Json;

namespace AAEmu.DBEditor.Models.aaemu.webapi;

[JsonObject(MemberSerialization.OptIn)]
public class AuctionLotList
{
    [JsonProperty]
    public List<AuctionLot> Items { get; set; }
}