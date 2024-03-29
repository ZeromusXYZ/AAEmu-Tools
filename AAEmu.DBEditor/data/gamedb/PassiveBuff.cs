﻿using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class PassiveBuff
{
    public long? Id { get; set; }

    public long? AbilityId { get; set; }

    public long? Level { get; set; }

    public long? BuffId { get; set; }

    public long? ReqPoints { get; set; }

    public byte[] Active { get; set; }
}
