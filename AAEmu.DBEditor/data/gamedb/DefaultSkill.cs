using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class DefaultSkill
{
    public long? Id { get; set; }

    public long? SkillId { get; set; }

    public long? SlotIndex { get; set; }

    public byte[] AddToSlot { get; set; }

    public long? SkillBookCategoryId { get; set; }
}
