using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class Effect
{
    public long? Id { get; set; }

    public long? ActualId { get; set; }

    public string ActualType { get; set; }
}
