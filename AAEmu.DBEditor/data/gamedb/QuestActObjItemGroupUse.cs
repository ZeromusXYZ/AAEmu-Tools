﻿using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class QuestActObjItemGroupUse
{
    public long? Id { get; set; }

    public long? ItemGroupId { get; set; }

    public long? Count { get; set; }

    public long? HighlightDoodadId { get; set; }

    public byte[] UseAlias { get; set; }

    public long? QuestActObjAliasId { get; set; }

    public byte[] DropWhenDestroy { get; set; }

    public long? HighlightDoodadPhase { get; set; }
}
