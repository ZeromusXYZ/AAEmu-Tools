using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class DoodadFuncPulseTrigger
{
    public long? Id { get; set; }

    public byte[] Flag { get; set; }

    public long? NextPhase { get; set; }
}
