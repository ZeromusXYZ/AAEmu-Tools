using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class EquipItemAttrModifier
{
    public long? Id { get; set; }

    public string Alias { get; set; }

    public long? StrWeight { get; set; }

    public long? DexWeight { get; set; }

    public long? StaWeight { get; set; }

    public long? IntWeight { get; set; }

    public long? SpiWeight { get; set; }
}
