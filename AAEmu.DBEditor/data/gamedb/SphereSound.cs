using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class SphereSound
{
    public long? Id { get; set; }

    public long? SoundId { get; set; }

    public byte[] Broadcast { get; set; }
}
