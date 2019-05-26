using System;
using Microsoft.Data.Sqlite;

namespace AAEmu.Game.Utils.DB
{
    public static class SQLite
    {
        public static string SQLiteFileName = "data/compact.sqlite3";

        public static SqliteConnection CreateConnection()
        {
            var connection = new SqliteConnection($"Data Source=file:{SQLiteFileName}; Mode=ReadOnly");
            try
            {
                connection.Open();
            }
            catch
            {
                return null;
            }

            return connection;
        }
    }
}
