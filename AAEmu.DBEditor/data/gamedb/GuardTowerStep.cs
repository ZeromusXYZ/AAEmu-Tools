﻿using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class GuardTowerStep
{
    public long? Id { get; set; }

    public long? GuardTowerSettingId { get; set; }

    public long? Step { get; set; }

    public long? NumGates { get; set; }

    public long? NumWalls { get; set; }

    public long? BuffId { get; set; }
}
