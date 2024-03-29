﻿using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class ItemGroup
{
    public long? Id { get; set; }

    public string Name { get; set; }

    public byte[] VisibleUi { get; set; }

    public string Description { get; set; }
}
