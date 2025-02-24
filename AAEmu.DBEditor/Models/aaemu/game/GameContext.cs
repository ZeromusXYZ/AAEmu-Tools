﻿using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using AAEmu.DBEditor.utils;

namespace AAEmu.DBEditor.data.aaemu.game;

public partial class GameContext : DbContext
{
    public GameContext()
    {
        //
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var conString = new MySqlConnectionStringBuilder();
        var (ip, port) = IpHelper.SplitAsHostAndPort(AAEmu.DBEditor.Properties.Settings.Default.MySQLDB, 3306);
        conString.Server = ip; // "127.0.0.1";
        conString.Port = port; // "3306";
        conString.Database = AAEmu.DBEditor.Properties.Settings.Default.MySQLGame; // "aaemu_game";
        conString.UserID = AAEmu.DBEditor.Properties.Settings.Default.MySQLUser; // "root";
        conString.Password = AAEmu.DBEditor.Properties.Settings.Default.MySQLPassword; // "password";
        optionsBuilder.UseMySql(conString.ConnectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.12-mysql"));
    }
}