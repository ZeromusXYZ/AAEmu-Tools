using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class SkillVisualGroup
{
    public long? Id { get; set; }

    public long? OwnerId { get; set; }

    public string OwnerType { get; set; }

    public long? Level { get; set; }

    public long? FxGroupId { get; set; }

    public long? ProjectileId { get; set; }
}
