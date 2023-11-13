using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class DoodadFuncSiegePeriod
{
    public long? Id { get; set; }

    public long? SiegePeriodId { get; set; }

    public long? NextPhase { get; set; }

    public byte[] Defense { get; set; }
}
