﻿using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class HousingArea
{
    public long? Id { get; set; }

    public string Name { get; set; }

    public long? HousingGroupId { get; set; }

    public string Comments { get; set; }
}
