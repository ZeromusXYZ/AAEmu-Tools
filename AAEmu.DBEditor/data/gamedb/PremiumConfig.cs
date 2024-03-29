﻿using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class PremiumConfig
{
    public long? Id { get; set; }

    public long? MaxGrade { get; set; }

    public long? ConnectPoint { get; set; }

    public long? DisconnectPoint { get; set; }

    public long? DeactivatePoint { get; set; }

    public long? MaxPoint { get; set; }
}
