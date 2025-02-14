using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAEmu.DBEditor.utils;

namespace AAEmu.DBEditor.data.aaemu.login;

public partial class aaemu_login_context : DbContext
{
    public aaemu_login_context()
    {
        //
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var conString = new MySqlConnectionStringBuilder();
        var (ip, port) = IpHelper.SplitAsHostAndPort(AAEmu.DBEditor.Properties.Settings.Default.MySQLDB, 3306);
        conString.Server = ip; // "127.0.0.1";
        conString.Port = port; // "3306";
        conString.Database = AAEmu.DBEditor.Properties.Settings.Default.MySQLLogin; // "aaemu_login";
        conString.UserID = AAEmu.DBEditor.Properties.Settings.Default.MySQLUser; // "root";
        conString.Password = AAEmu.DBEditor.Properties.Settings.Default.MySQLPassword; // "password";
        optionsBuilder.UseMySql(conString.ConnectionString,
            Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.12-mysql"));
    }
}