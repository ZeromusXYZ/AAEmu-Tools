using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class QuestActObjExpressFire
{
    public long? Id { get; set; }

    public long? ExpressKeyId { get; set; }

    public long? NpcGroupId { get; set; }

    public long? Count { get; set; }

    public byte[] UseAlias { get; set; }

    public long? QuestActObjAliasId { get; set; }
}
