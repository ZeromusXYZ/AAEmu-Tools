using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class ItemRecipe
{
    public long? Id { get; set; }

    public long? ItemId { get; set; }

    public long? CraftId { get; set; }
}
