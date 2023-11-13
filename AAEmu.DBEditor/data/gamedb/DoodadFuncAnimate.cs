using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class DoodadFuncAnimate
{
    public long? Id { get; set; }

    public string Name { get; set; }

    public byte[] PlayOnce { get; set; }
}
