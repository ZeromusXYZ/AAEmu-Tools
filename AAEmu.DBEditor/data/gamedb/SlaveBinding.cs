using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class SlaveBinding
{
    public long? Id { get; set; }

    public long? OwnerId { get; set; }

    public string OwnerType { get; set; }

    public long? SlaveId { get; set; }

    public long? AttachPointId { get; set; }
}
