using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class AiCommandSet
{
    public long? Id { get; set; }

    public string Name { get; set; }

    public string CanInteract { get; set; }
}
