﻿using AAEmu.DBEditor.Utils.DB;
using AAEmu.DBEditor.data.gamedb;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore.Design;

namespace AAEmu.DBEditor.data
{
    public class ServerDB
    {
        private string fileName;

        public string FileName { get { return fileName; } }

        public CompactContext CompactSqlite { get; set; }

        public List<string> TableNames { get; set; } = new();

        public Dictionary<(string, string, long?), string> LocalizedText { get; set; } = new();

        public string GetText(string tableName, string columnName, long index, string defaultText, bool defaultIfEmpty = true)
        {
            if (LocalizedText.TryGetValue((tableName, columnName, index), out var v))
                return defaultIfEmpty && string.IsNullOrWhiteSpace(v) ? defaultText : v ?? defaultText;
            return defaultText;
        }

        public string GetUiText(int categoryId, string keyName, string defaultText, bool defaultIfEmpty = true)
        {
            keyName = keyName.ToLower();
            var uiText = CompactSqlite.UiTexts.FirstOrDefault(x => x.CategoryId == categoryId && x.Key == keyName);
            if (uiText == null)
                return defaultText;
            if (LocalizedText.TryGetValue(("ui_texts", "text", uiText.Id), out var v))
                return defaultIfEmpty && string.IsNullOrWhiteSpace(v) ? defaultText : v ?? defaultText;
            return defaultText;
        }


        public Dictionary<long, Item> ItemCache { get; set; } = new();

        public bool ReloadLocale(CompactContext context)
        {
            LocalizedText.Clear();
            try
            {
                var locals = context.LocalizedTexts.ToList();
                foreach (var localize in locals)
                {
                    var s = localize.EnUs;// localize.Ko;
                    switch (ProgramSettings.Instance.ClientLanguage.ToLower())
                    {
                        case "ko": s = localize.Ko; break;
                        case "en_us": s = localize.EnUs; break;
                        case "ru": s = localize.Ru; break;
                        case "de": s = localize.De; break;
                        case "fr": s = localize.Fr; break;
                        case "zh_cn": s = localize.ZhCn; break;
                        case "zh_tw": s = localize.ZhTw; break;
                        case "ja": s = localize.Ja; break;
                    }
                    LocalizedText.Add(new(localize.TblName, localize.TblColumnName, localize.Idx), s);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool OpenDB(string fileName)
        {
            MainForm.Self.UpdateProgress("Opening ServerDB ...");

            TableNames.Clear();
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

                CompactSqlite?.Dispose();
                CompactSqlite = null;
                var tempContext = new CompactContext();
                try
                {
                    if (!ReloadLocale(tempContext))
                        return false;
                    CompactSqlite = tempContext;
                }
                catch
                {
                    return false;
                }
            }
            return TableNames.Count > 0;
        }

        internal void LoadDbCache()
        {
            ItemCache.Clear();
            foreach(var item in CompactSqlite.Items)
                ItemCache.Add((long)item.Id, item);
        }

        public Item GetItem(long itemId)
        {
            if (ItemCache.TryGetValue(itemId, out var item))
                return item;
            return null;
        }

        public bool UiTextExists(string keyName)
        {
            keyName = keyName.ToLower();
            return CompactSqlite.UiTexts.Any(x => x.Key == keyName);
        }
    }
}
