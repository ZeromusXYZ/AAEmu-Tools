﻿using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class ActabilityCategory
{
    public long? Id { get; set; }

    public string Name { get; set; }

    public long? GroupId { get; set; }

    public byte[] VisibleUi { get; set; }

    public long? VisibleOrder { get; set; }
}
