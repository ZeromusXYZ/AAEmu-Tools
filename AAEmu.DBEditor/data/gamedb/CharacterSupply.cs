﻿using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class CharacterSupply
{
    public long? Id { get; set; }

    public long? AbilityId { get; set; }

    public long? ItemId { get; set; }

    public long? Amount { get; set; }

    public long? GradeId { get; set; }
}
