using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class CustomHairTexture
{
    public long? Id { get; set; }

    public string Name { get; set; }

    public string DiffuseTexture { get; set; }

    public string SpecularTexture { get; set; }

    public string NormalTexture { get; set; }
}
