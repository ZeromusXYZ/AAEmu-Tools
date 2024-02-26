using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class NpcControlEffect
{
    public long? Id { get; set; }

    public long? CategoryId { get; set; }

    public string ParamString { get; set; }

    public long? ParamInt { get; set; }
}
