using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class FxParticle
{
    public long? Id { get; set; }

    public long? SoundId { get; set; }

    public long? SoundPackId { get; set; }

    public byte[] InWater { get; set; }

    public double? Scale { get; set; }
}
