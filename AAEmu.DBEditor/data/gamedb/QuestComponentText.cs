using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class QuestComponentText
{
    public long? Id { get; set; }

    public long? QuestComponentTextKindId { get; set; }

    public long? QuestComponentId { get; set; }

    public string Text { get; set; }
}
