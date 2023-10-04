using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class ItemConvProduct
{
    public long? Id { get; set; }

    public long? ItemConvPpackId { get; set; }

    public long? ItemId { get; set; }

    public long? Weight { get; set; }

    public long? Min { get; set; }

    public long? Max { get; set; }
}
