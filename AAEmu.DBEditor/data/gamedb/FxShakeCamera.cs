using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class FxShakeCamera
{
    public long? Id { get; set; }

    public double? AngX { get; set; }

    public double? AngY { get; set; }

    public double? AngZ { get; set; }

    public double? ShiftX { get; set; }

    public double? ShiftY { get; set; }

    public double? ShiftZ { get; set; }

    public double? Frequency { get; set; }

    public double? Randomness { get; set; }

    public double? Duration { get; set; }

    public double? Range { get; set; }
}
