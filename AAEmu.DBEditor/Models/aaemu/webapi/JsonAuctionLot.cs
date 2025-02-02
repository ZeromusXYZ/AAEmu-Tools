using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using AAEmu.DBEditor.utils;
using AAEmu.DBEditor.data;

namespace AAEmu.DBEditor.Models.aaemu.webapi;

[JsonObject(MemberSerialization.OptIn)]
public class JsonAuctionLot
{
    [JsonProperty]
    public ulong Id { get; set; }

    [JsonProperty]
    public byte Duration { get; set; } // 0 is 6 hours, 1 is 12 hours, 2 is 24 hours, 3 is 48 hours

    [JsonProperty]
    public JsonItem Item { get; set; }

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
    public override string ToString()
    {
        var itemNamePart = Data.Server?.LocalizedText?.GetValueOrDefault(("items", "name", Item.TemplateId)) ?? $"<item:{Item.TemplateId}>";
        var countPart = Item?.Count > 1 ? $" x {Item?.Count}" : "";
        var gradePart = (Data.Server?.LocalizedText?.GetValueOrDefault(("item_grades", "name", Item?.Grade ?? 0)) ?? $"<grade:{Item?.Grade ?? 0}>") + " ";
        return $"(DB:{Id}) {gradePart}{itemNamePart}{countPart}";
        // return $"{Id}, Item:{Item.TemplateId}, Grade: {Item.Grade} x {Item.Count} @ {AaTextHelper.CopperToString(DirectMoney)}";
    }
}