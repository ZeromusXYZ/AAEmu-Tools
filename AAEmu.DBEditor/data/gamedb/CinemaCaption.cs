using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class CinemaCaption
{
    public long? Id { get; set; }

    public long? CinemaId { get; set; }

    public string Caption { get; set; }
}
