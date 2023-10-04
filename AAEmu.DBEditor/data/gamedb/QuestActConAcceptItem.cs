using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class QuestActConAcceptItem
{
    public long? Id { get; set; }

    public long? ItemId { get; set; }

    public byte[] Cleanup { get; set; }

    public byte[] DropWhenDestroy { get; set; }

    public byte[] DestroyWhenDrop { get; set; }
}
