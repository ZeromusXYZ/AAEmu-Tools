using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class Sound
{
    public long? Id { get; set; }

    public string Name { get; set; }

    public string Path { get; set; }

    public long? EndMethodId { get; set; }

    public long? LevelId { get; set; }

    public string Comment { get; set; }

    public long? CategoryId { get; set; }
}
