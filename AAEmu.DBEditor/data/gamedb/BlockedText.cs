using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class BlockedText
{
    public long? Id { get; set; }

    public string Utf8str { get; set; }

    public long? Bytes { get; set; }

    public byte[] CheckName { get; set; }

    public byte[] CheckChat { get; set; }

    public byte[] PartialMatch { get; set; }
}
