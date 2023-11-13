using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class CharacterCustomizingHairAsset
{
    public long? Id { get; set; }

    public long? DisplayOrder { get; set; }

    public long? HairId { get; set; }

    public byte[] IsHot { get; set; }

    public byte[] IsNew { get; set; }

    public long? ModelId { get; set; }
}
