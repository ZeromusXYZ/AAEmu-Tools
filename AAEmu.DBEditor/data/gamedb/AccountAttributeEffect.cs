using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class AccountAttributeEffect
{
    public long? Id { get; set; }

    public long? KindId { get; set; }

    public byte[] BindWorld { get; set; }

    public byte[] IsAdd { get; set; }

    public long? Count { get; set; }

    public long? Time { get; set; }
}
