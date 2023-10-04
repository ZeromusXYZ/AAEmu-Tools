using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class BattleField
{
    public long? Id { get; set; }

    public string Name { get; set; }

    public string Comments { get; set; }

    public long? ZoneKey { get; set; }

    public string Desc { get; set; }

    public long? FieldKindId { get; set; }
}
