﻿using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace AAEmu.DBEditor.data.gamedb;

public partial class CompactContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var conString = new SqliteConnectionStringBuilder();
        conString.DataSource = ProgramSettings.Instance.ServerDb;
        optionsBuilder.UseSqlite(conString.ConnectionString);
    }
}
