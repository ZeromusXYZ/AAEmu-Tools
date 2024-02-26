using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class DistrictReturnPoint
{
    public long? Id { get; set; }

    public long? DistrictId { get; set; }

    public long? FactionId { get; set; }

    public long? ReturnPointId { get; set; }
}
