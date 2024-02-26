using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class NpcChatBubble
{
    public long? Id { get; set; }

    public string Bubble { get; set; }

    public long? AiEventId { get; set; }
}
