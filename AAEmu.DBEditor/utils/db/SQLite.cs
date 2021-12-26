using System;
using Microsoft.Data.Sqlite;

namespace AAEmu.DbEditor.Utils.DB
{
    public static class SQLite
    {
        public static string SQLiteFileName = "data/compact.sqlite3";
        public static bool ReadOnly = true;

        public static SqliteConnection CreateConnection()
        {
            var connectionBuilder = new SqliteConnectionStringBuilder();
            connectionBuilder.DataSource = SQLiteFileName;
            connectionBuilder.Mode = ReadOnly ? SqliteOpenMode.ReadOnly : SqliteOpenMode.ReadWrite;
            SqliteConnection connection = null;
            try
            {
                connection = new SqliteConnection();
                connection.ConnectionString = connectionBuilder.ConnectionString;
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
