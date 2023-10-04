using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class UccApplicable
{
    public long? Id { get; set; }

    public string Name { get; set; }

    public long? KindId { get; set; }

    public long? ActualId { get; set; }

    public string TooltipMsg { get; set; }
}
