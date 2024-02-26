using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class PrefabElement
{
    public long? Id { get; set; }

    public long? PrefabModelId { get; set; }

    public long? StateId { get; set; }

    public string FilePath { get; set; }
}
