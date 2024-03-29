﻿using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class SoundPackItem
{
    public long? Id { get; set; }

    public long? SoundPackId { get; set; }

    public string Name { get; set; }

    public long? SoundId { get; set; }

    public string Comment { get; set; }
}
