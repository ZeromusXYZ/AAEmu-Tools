using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class QuestCategory
{
    public long? Id { get; set; }

    public string Name { get; set; }

    public byte[] Translate { get; set; }
}
