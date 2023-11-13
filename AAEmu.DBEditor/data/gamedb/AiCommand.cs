using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class AiCommand
{
    public long? Id { get; set; }

    public long? CmdSetId { get; set; }

    public long? CmdId { get; set; }

    public long? Param1 { get; set; }

    public string Param2 { get; set; }
}
