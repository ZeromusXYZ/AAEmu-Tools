using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class ItemCapScale
{
    public long? Id { get; set; }

    public string Name { get; set; }

    public long? SkillId { get; set; }

    public long? ScaleMin { get; set; }

    public long? ScaleMax { get; set; }
}
