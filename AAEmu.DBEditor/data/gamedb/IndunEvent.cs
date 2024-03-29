﻿using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class IndunEvent
{
    public long? Id { get; set; }

    public long? ZoneGroupId { get; set; }

    public string Name { get; set; }

    public long? ConditionId { get; set; }

    public string ConditionType { get; set; }

    public long? StartActionId { get; set; }
}
