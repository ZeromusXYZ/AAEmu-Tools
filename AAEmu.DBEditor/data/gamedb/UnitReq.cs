﻿using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class UnitReq
{
    public long? Id { get; set; }

    public long? OwnerId { get; set; }

    public string OwnerType { get; set; }

    public long? KindId { get; set; }

    public long? Value1 { get; set; }

    public long? Value2 { get; set; }
}
