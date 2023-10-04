using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class IndunRoom
{
    public long? Id { get; set; }

    public long? ZoneGroupId { get; set; }

    public string Name { get; set; }

    public long? ShapeId { get; set; }

    public string ShapeType { get; set; }
}
