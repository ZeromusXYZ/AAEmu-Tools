﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.aaemu.game;

/// <summary>
/// Guild role settings
/// </summary>
public partial class ExpeditionRolePolicies
{
    public int ExpeditionId { get; set; }

    public byte Role { get; set; }

    public string Name { get; set; }

    public bool DominionDeclare { get; set; }

    public bool Invite { get; set; }

    public bool Expel { get; set; }

    public bool Promote { get; set; }

    public bool Dismiss { get; set; }

    public bool Chat { get; set; }

    public bool ManagerChat { get; set; }

    public bool SiegeMaster { get; set; }

    public bool JoinSiege { get; set; }
}