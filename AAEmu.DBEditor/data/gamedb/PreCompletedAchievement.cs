using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class PreCompletedAchievement
{
    public long? Id { get; set; }

    public long? MyAchievementId { get; set; }

    public long? CompletedAchievementId { get; set; }
}
