using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class SpecialtyNpc
{
    public long? Id { get; set; }

    public string Name { get; set; }

    public long? NpcId { get; set; }

    public long? SpecialtyBundleId { get; set; }
}
