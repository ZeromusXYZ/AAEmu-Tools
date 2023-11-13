using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class Specialty
{
    public long? Id { get; set; }

    public long? RowZoneGroupId { get; set; }

    public long? ColZoneGroupId { get; set; }

    public long? Ratio { get; set; }

    public long? Profit { get; set; }

    public byte[] VendorExist { get; set; }
}
