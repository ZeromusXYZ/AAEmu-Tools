﻿using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class CharacterBuff
{
    public long? Id { get; set; }

    public long? CharacterId { get; set; }

    public long? BuffId { get; set; }
}
