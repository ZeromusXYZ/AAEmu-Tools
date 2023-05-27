using Microsoft.Data.Sqlite;

namespace AAEmu.Game.Utils.DB
{
    public static class SQLite
    {
        public static string SQLiteFileName = "data/compact.sqlite3";

        public static SqliteConnection CreateConnection(string overrideFileName = "")
        {
            var fName = overrideFileName != "" ? overrideFileName : SQLiteFileName;
            var connection = new SqliteConnection($"Data Source=file:{fName}; Mode=ReadOnly");
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
