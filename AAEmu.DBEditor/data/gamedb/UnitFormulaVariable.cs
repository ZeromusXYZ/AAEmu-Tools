using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class UnitFormulaVariable
{
    public long? Id { get; set; }

    public long? UnitFormulaId { get; set; }

    public long? VariableKindId { get; set; }

    public long? Key { get; set; }

    public double? Value { get; set; }
}
