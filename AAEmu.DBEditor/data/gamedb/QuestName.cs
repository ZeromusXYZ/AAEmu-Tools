using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class QuestName
{
    public long? Id { get; set; }

    public long? QuestNameKindId { get; set; }

    public long? QuestContextId { get; set; }

    public string Name { get; set; }
}
