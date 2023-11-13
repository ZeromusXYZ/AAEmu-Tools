using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class PhysicalEnchantAbility
{
    public long? Id { get; set; }

    public long? NpcId { get; set; }

    public string Armor { get; set; }

    public long? EnchantLevel { get; set; }

    public long? MinFriendship { get; set; }

    public long? SuccessRatio { get; set; }
}
