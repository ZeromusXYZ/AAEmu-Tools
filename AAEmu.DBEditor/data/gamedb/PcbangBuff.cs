﻿using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class PcbangBuff
{
    public long? Id { get; set; }

    public long? BuffId { get; set; }

    public byte[] Active { get; set; }

    public long? KindId { get; set; }

    public long? PremiumGradeId { get; set; }
}
