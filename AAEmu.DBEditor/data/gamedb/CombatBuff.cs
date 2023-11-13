using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class CombatBuff
{
    public long? Id { get; set; }

    public string HitSkillId { get; set; }

    public long? HitTypeId { get; set; }

    public string ReqSkillId { get; set; }

    public long? BuffId { get; set; }

    public string BuffFromSource { get; set; }

    public string BuffToSource { get; set; }

    public long? ReqBuffId { get; set; }

    public long? IsHealSpell { get; set; }
}
