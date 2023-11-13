using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class KillNpcWithoutCorpseEffect
{
    public long? Id { get; set; }

    public long? NpcId { get; set; }

    public double? Radius { get; set; }

    public byte[] GiveExp { get; set; }

    public byte[] Vanish { get; set; }
}
