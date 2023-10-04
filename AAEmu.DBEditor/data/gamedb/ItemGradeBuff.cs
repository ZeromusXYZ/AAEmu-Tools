using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class ItemGradeBuff
{
    public long? Id { get; set; }

    public long? ItemId { get; set; }

    public long? ItemGradeId { get; set; }

    public long? BuffId { get; set; }
}
