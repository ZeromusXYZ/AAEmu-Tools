using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class Taxation
{
    public long? Id { get; set; }

    public long? Tax { get; set; }

    public string Desc { get; set; }

    public byte[] Show { get; set; }
}
