using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class ItemHousingDecoration
{
    public long? Id { get; set; }

    public long? ItemId { get; set; }

    public long? DesignId { get; set; }

    public byte[] Restore { get; set; }
}
