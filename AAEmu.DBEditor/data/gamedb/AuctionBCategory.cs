using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class AuctionBCategory
{
    public long? Id { get; set; }

    public string Name { get; set; }

    public long? ParentCategoryId { get; set; }
}
