﻿using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class BuffEffect
{
    public long? Id { get; set; }

    public long? BuffId { get; set; }

    public long? Chance { get; set; }

    public long? Stack { get; set; }

    public long? AbLevel { get; set; }
}
