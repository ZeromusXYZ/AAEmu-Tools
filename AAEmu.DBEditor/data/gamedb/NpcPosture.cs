using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class NpcPosture
{
    public long? Id { get; set; }

    public long? NpcPostureSetId { get; set; }

    public long? AnimActionId { get; set; }

    public string TalkAnim { get; set; }

    public long? StartTodTime { get; set; }
}
