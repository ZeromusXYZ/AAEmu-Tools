using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class FxCamFov
{
    public long? Id { get; set; }

    public double? FadeIn { get; set; }

    public double? Duration { get; set; }

    public double? FadeOut { get; set; }

    public double? CamFov { get; set; }
}
