namespace AAEmu.DBEditor.Models.aaemu.webapi;

public class JsonAhGenerateItemRequest
{
    public int ItemTemplateId { get; set; }
    public long Quantity { get; set; }
    public byte GradeId { get; set; }
    public long BuyNowPrice { get; set; }
    public long StartPrice { get; set; }
    public byte Duration { get; set; }
    public uint ClientId { get; set; }
    public string ClientName { get; set; }
}