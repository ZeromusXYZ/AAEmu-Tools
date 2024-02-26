using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class UiText
{
    public long? Id { get; set; }

    public string Key { get; set; }

    public string Text { get; set; }

    public long? CategoryId { get; set; }
}
