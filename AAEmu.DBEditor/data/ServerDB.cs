using AAEmu.DbEditor.Utils.DB;
using System.Collections.Generic;
using System.IO;

namespace AAEmu.DbEditor.data
{
    public class ServerDB
    {
        private string fileName;

        public string FileName { get { return fileName; } }
        public List<string> TableNames { get; set; } = new();

        public Dictionary<(string, string), string> LocalizedText { get; set; } = new();

        public bool OpenDB(string fileName)
        {
            MainForm.Self.UpdateProgress("Opening ServerDB ...");

            TableNames.Clear();
            LocalizedText.Clear();
            fileName = Path.GetFullPath(fileName);
            if (File.Exists(fileName))
            {
                // Read Table Names
                SQLite.SqLiteFileName = fileName;
                var sql = "SELECT name FROM sqlite_master WHERE type = 'table' ORDER BY name ASC";

                using (var connection = SQLite.CreateConnection())
                {
                    if (connection == null)
                        return false;

                    MainForm.Self.UpdateProgress("Opening " + fileName + "...");


                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = sql;
                        command.Prepare();
                        using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                        {
                            TableNames.Clear();
                            MainForm.Self.UpdateProgress("Loading " + fileName + " table names ...");
                            while (reader.Read())
                            {
                                var tName = reader.GetString("name", "");
                                if (!string.IsNullOrWhiteSpace(tName))
                                    TableNames.Add(tName);
                            }
                        }
                    }
                }
                if (TableNames.Count > 0)
                    this.fileName = fileName;

                return TableNames.Count > 0;
            }
            return false;
        }

    }
}
