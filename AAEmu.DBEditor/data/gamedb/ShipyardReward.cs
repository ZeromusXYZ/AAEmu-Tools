﻿using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class ShipyardReward
{
    public long? Id { get; set; }

    public long? ShipyardId { get; set; }

    public long? DoodadId { get; set; }

    public byte[] OnWater { get; set; }

    public double? Radius { get; set; }

    public long? Count { get; set; }
}
