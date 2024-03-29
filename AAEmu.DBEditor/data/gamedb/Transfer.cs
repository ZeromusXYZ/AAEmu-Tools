﻿using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class Transfer
{
    public long? Id { get; set; }

    public string Comment { get; set; }

    public long? ModelId { get; set; }

    public double? WaitTime { get; set; }

    public byte[] Cyclic { get; set; }

    public double? PathSmoothing { get; set; }
}
