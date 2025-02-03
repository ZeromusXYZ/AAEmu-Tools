namespace AAEmu.DBEditor.Models.aaemu.webapi;

public class JsonDeleteMailRequest
{
    public long MailId { get; set; }
    public uint SenderId { get; set; }
    public uint ReceiverId { get; set; }
    public bool TrashItems { get; set; }
}
