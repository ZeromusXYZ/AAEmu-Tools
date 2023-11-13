using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class NpPassiveBuff
{
    public long? Id { get; set; }

    public long? OwnerId { get; set; }

    public string OwnerType { get; set; }

    public long? PassiveBuffId { get; set; }
}
