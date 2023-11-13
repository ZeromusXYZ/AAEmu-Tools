using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class ConversionEffect
{
    public long? Id { get; set; }

    public long? CategoryId { get; set; }

    public long? SourceCategoryId { get; set; }

    public long? SourceValue { get; set; }

    public long? TargetCategoryId { get; set; }

    public long? TargetValue { get; set; }
}
