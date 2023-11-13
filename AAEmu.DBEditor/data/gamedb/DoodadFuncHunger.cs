using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class DoodadFuncHunger
{
    public long? Id { get; set; }

    public long? HungryTerm { get; set; }

    public long? FullStep { get; set; }

    public long? PhaseChangeLimit { get; set; }

    public long? NextPhase { get; set; }
}
