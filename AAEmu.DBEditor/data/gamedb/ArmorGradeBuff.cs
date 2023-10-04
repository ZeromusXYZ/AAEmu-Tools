using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class ArmorGradeBuff
{
    public long? Id { get; set; }

    public long? ArmorTypeId { get; set; }

    public long? ItemGradeId { get; set; }

    public long? BuffId { get; set; }
}
