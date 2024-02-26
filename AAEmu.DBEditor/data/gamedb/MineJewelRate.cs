using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class MineJewelRate
{
    public long? Id { get; set; }

    public long? MineItemId { get; set; }

    public long? JewelItemId { get; set; }

    public long? Rate { get; set; }
}
