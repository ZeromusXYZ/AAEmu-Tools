using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class FlyingStateChangeEffect
{
    public long? Id { get; set; }

    public byte[] FlyingState { get; set; }
}
