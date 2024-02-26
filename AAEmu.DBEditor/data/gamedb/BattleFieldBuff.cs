using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class BattleFieldBuff
{
    public long? Id { get; set; }

    public long? BattleFieldId { get; set; }

    public long? BuffId { get; set; }

    public long? Value { get; set; }
}
