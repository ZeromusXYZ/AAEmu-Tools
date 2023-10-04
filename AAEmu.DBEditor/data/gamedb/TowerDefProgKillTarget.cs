using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class TowerDefProgKillTarget
{
    public long? Id { get; set; }

    public long? TowerDefProgId { get; set; }

    public long? KillTargetId { get; set; }

    public string KillTargetType { get; set; }

    public long? KillCount { get; set; }
}
