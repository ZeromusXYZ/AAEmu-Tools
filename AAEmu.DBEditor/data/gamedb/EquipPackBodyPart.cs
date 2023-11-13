using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class EquipPackBodyPart
{
    public long? Id { get; set; }

    public string Name { get; set; }

    public long? ModelId { get; set; }

    public long? HairColorId { get; set; }

    public long? FaceId { get; set; }

    public long? HairId { get; set; }

    public long? BeardId { get; set; }

    public long? SkinColorId { get; set; }

    public long? BodyDiffuseMapId { get; set; }
}
