using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class QuestContextText
{
    public long? Id { get; set; }

    public long? QuestContextTextKindId { get; set; }

    public long? QuestContextId { get; set; }

    public string Text { get; set; }
}
