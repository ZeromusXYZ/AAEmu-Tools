﻿using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class ArmorAsset
{
    public long? Id { get; set; }

    public string Name { get; set; }

    public long? SlotTypeId { get; set; }

    public long? DefaultAssetId { get; set; }
}
