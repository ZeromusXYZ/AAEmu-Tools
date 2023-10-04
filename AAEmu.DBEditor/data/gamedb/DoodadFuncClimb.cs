using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class DoodadFuncClimb
{
    public long? Id { get; set; }

    public long? ClimbTypeId { get; set; }

    public byte[] AllowHorizontalMultiHanger { get; set; }
}
