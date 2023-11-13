using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class MateEquipSlotPack
{
    public long? Id { get; set; }

    public string Name { get; set; }

    public byte[] Head { get; set; }

    public byte[] Chest { get; set; }

    public byte[] Waist { get; set; }

    public byte[] Feet { get; set; }
}
