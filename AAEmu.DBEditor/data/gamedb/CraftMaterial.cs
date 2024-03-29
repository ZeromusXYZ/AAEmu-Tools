﻿using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class CraftMaterial
{
    public long? Id { get; set; }

    public long? CraftId { get; set; }

    public long? ItemId { get; set; }

    public long? Amount { get; set; }

    public byte[] MainGrade { get; set; }

    public long? RequireGrade { get; set; }
}
