﻿using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class QuestActObjMonsterHunt
{
    public long? Id { get; set; }

    public long? NpcId { get; set; }

    public long? Count { get; set; }

    public byte[] UseAlias { get; set; }

    public long? QuestActObjAliasId { get; set; }

    public long? HighlightDoodadPhase { get; set; }

    public long? HighlightDoodadId { get; set; }
}
