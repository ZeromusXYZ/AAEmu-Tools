using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class QuestActObjCondition
{
    public long? Id { get; set; }

    public long? ConditionId { get; set; }

    public long? QuestContextId { get; set; }

    public byte[] UseAlias { get; set; }

    public long? QuestActObjAliasId { get; set; }
}
