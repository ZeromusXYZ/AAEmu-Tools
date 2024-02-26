using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class HousingGroupCategory
{
    public long? Id { get; set; }

    public long? HousingGroupId { get; set; }

    public long? CategoryId { get; set; }

    public long? MaxConstructCount { get; set; }
}
