using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class DoodadFuncFinal
{
    public long? Id { get; set; }

    public long? After { get; set; }

    public byte[] Respawn { get; set; }

    public long? MinTime { get; set; }

    public long? MaxTime { get; set; }

    public byte[] ShowTip { get; set; }

    public byte[] ShowEndTime { get; set; }

    public string Tip { get; set; }
}
