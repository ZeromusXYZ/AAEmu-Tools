using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class NpcPostureSet
{
    public long? Id { get; set; }

    public string Name { get; set; }

    public long? QuestAnimActionId { get; set; }

    public string Comment { get; set; }
}
