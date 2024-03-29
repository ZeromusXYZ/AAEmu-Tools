﻿using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class ActabilityGroup
{
    public long? Id { get; set; }

    public string Name { get; set; }

    public string IconPath { get; set; }

    public byte[] Visible { get; set; }

    public byte[] SkillPageVisible { get; set; }

    public long? UnitAttrId { get; set; }
}
