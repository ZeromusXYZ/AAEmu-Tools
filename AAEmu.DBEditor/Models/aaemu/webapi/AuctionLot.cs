using Newtonsoft.Json;
using System;

namespace AAEmu.DBEditor.Models.aaemu.webapi;

[JsonObject(MemberSerialization.OptIn)]
public class AuctionLot
{
    [JsonProperty]
    public ulong Id { get; set; }

    [JsonProperty]
    public byte Duration { get; set; } // 0 is 6 hours, 1 is 12 hours, 2 is 24 hours, 3 is 48 hours

    [JsonProperty]
    public Item Item { get; set; }

    [JsonProperty]
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Seconds left
    /// </summary>
    [JsonProperty]
    public ulong TimeLeft { get; set; }

    [JsonProperty]
    public byte WorldId { get; set; }

    [JsonProperty]
    public uint ClientId { get; set; }

    [JsonProperty]
    public string ClientName { get; set; }

    [JsonProperty]
    public int StartMoney { get; set; }

    [JsonProperty]
    public int DirectMoney { get; set; }

    [JsonProperty]
    public DateTime PostDate { get; set; }

    // [JsonProperty]
    // public int ChargePercent { get; set; } // added in 3+

    [JsonProperty]
    public byte BidWorldId { get; set; }

    [JsonProperty]
    public uint BidderId { get; set; }

    [JsonProperty]
    public string BidderName { get; set; }

    [JsonProperty]
    public int BidMoney { get; set; }

    [JsonProperty]
    public int Extra { get; set; }

    // [JsonProperty]
    // public int MinStack { get; set; } // added in 3+

    // [JsonProperty]
    // public int MaxStack { get; set; } // added in 3+
}