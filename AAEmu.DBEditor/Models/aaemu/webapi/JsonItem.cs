using AAEmu.DBEditor.data.enums;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System;

namespace AAEmu.DBEditor.Models.aaemu.webapi;

public class JsonItem
{
    [JsonProperty]
    public byte WorldId { get; set; }

    [JsonProperty]
    public ulong OwnerId { get; set; }

    [JsonProperty]
    public ulong Id { get; set; }

    [JsonProperty]
    public uint TemplateId { get; set; }
        
    [JsonProperty]
    public uint DetailBytesLength { get; set; }

    [JsonProperty]
    public SlotType SlotType { get; set; }

    [JsonProperty]
    public int Slot { get; set; }

    [JsonProperty]
    public byte Grade { get; set; }

    [JsonProperty]
    public byte ItemFlags { get; set; }

    [JsonProperty]
    public int Count { get; set; }

    [JsonProperty]
    public int LifespanMins { get; set; }

    [JsonProperty]
    public uint MadeUnitId { get; set; }

    [JsonProperty]
    public DateTime CreateTime { get; set; }

    [JsonProperty]
    public DateTime UnsecureTime { get; set; }

    [JsonProperty]
    public DateTime UnpackTime { get; set; }

    [JsonProperty]
    public uint ImageItemTemplateId { get; set; }

    /// <summary>
    /// Internal representation of the exact time an item will expire (UTC)
    /// </summary>
    [JsonProperty]
    public DateTime ExpirationTime { get; set; }

    /// <summary>
    /// Internal representation of the time this item has left before expiring, only counting down if the owning character is online
    /// </summary>
    [JsonProperty]
    public double ExpirationOnlineMinutesLeft { get; set; }

    [JsonProperty]
    public ulong UccId { get; set; }

    [JsonProperty]
    public DateTime ChargeStartTime { get; set; } = DateTime.MinValue;

    [JsonProperty]
    public int ChargeCount { get; set; }

    [JsonProperty]
    public byte DetailType { get; set; }

    [JsonProperty]
    public byte[] Detail { get; set; }
}