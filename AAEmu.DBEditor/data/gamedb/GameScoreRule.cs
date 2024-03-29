﻿using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class GameScoreRule
{
    public long? Id { get; set; }

    public long? RuleSetId { get; set; }

    public long? RuleSetCorps { get; set; }

    public long? EventId { get; set; }

    public long? EventValue { get; set; }

    public long? EventScore { get; set; }
}
