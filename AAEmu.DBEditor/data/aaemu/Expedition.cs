﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.aaemu;

/// <summary>
/// Guilds
/// </summary>
public partial class Expedition
{
    public int Id { get; set; }

    public int Owner { get; set; }

    public string OwnerName { get; set; }

    public string Name { get; set; }

    public int Mother { get; set; }

    public DateTime CreatedAt { get; set; }
}