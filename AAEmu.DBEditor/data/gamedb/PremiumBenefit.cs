using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class PremiumBenefit
{
    public long? Id { get; set; }

    public long? OnlineLabor { get; set; }

    public long? OfflineLabor { get; set; }

    public long? MaxLabor { get; set; }

    public long? IconId { get; set; }

    public long? GradeId { get; set; }
}
