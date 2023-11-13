using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class SlaveDropDoodad
{
    public long? Id { get; set; }

    public long? OwnerId { get; set; }

    public string OwnerType { get; set; }

    public long? DoodadId { get; set; }

    public long? Count { get; set; }

    public double? Radius { get; set; }

    public byte[] OnWater { get; set; }
}
