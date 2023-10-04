using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class QuestActEtcItemObtain
{
    public long? Id { get; set; }

    public long? ItemId { get; set; }

    public long? Count { get; set; }

    public long? HighlightDoodadId { get; set; }

    public byte[] Cleanup { get; set; }
}
