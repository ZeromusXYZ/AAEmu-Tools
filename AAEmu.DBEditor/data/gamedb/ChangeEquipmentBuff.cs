using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class ChangeEquipmentBuff
{
    public long? Id { get; set; }

    public long? OwnerId { get; set; }

    public string OwnerType { get; set; }

    public long? BuffId { get; set; }
}
