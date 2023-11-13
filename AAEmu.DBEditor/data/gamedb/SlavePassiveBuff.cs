using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class SlavePassiveBuff
{
    public long? Id { get; set; }

    public long? OwnerId { get; set; }

    public string OwnerType { get; set; }

    public long? PassiveBuffId { get; set; }
}
