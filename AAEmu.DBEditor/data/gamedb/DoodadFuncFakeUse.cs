using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class DoodadFuncFakeUse
{
    public long? Id { get; set; }

    public long? SkillId { get; set; }

    public long? FakeSkillId { get; set; }

    public byte[] TargetParent { get; set; }
}
