using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class TransferPath
{
    public long? Id { get; set; }

    public long? OwnerId { get; set; }

    public string OwnerType { get; set; }

    public string PathName { get; set; }

    public double? WaitTimeStart { get; set; }

    public double? WaitTimeEnd { get; set; }
}
