using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class QuestAct
{
    public long? Id { get; set; }

    public long? QuestComponentId { get; set; }

    public long? ActDetailId { get; set; }

    public string ActDetailType { get; set; }
}
