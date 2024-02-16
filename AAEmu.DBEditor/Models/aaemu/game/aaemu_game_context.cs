﻿using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAEmu.DBEditor.data.aaemu.game
{
    public partial class aaemu_game_context : DbContext
    {
        public aaemu_game_context()
        {
            //
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conString = new MySqlConnectionStringBuilder();
            conString.Server = AAEmu.DBEditor.Properties.Settings.Default.MySQLDB; // "127.0.0.1";
            conString.Database = AAEmu.DBEditor.Properties.Settings.Default.MySQLGame; // "aaemu_game";
            conString.UserID = AAEmu.DBEditor.Properties.Settings.Default.MySQLUser; // "root";
            conString.Password = AAEmu.DBEditor.Properties.Settings.Default.MySQLPassword; // "password";
            optionsBuilder.UseMySql(conString.ConnectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.12-mysql"));
        }
    }
}
