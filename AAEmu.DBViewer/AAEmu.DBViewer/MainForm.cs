using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using AAEmu.Game.Utils.DB;

namespace AAEmu.DBViewer
{
    public partial class MainForm : Form
    {

        class GameTranslation
        {
            public long idx = 0;
            public string table = string.Empty ;
            public string field = string.Empty ;
            public string value = string.Empty ;
        }
        Dictionary<long, GameTranslation> CurrentTranslation = new Dictionary<long, GameTranslation>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadDB(false);
            tcViewer.SelectedTab = tpItems;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void GetTableNames()
        {
            string sql = "SELECT name FROM sqlite_master WHERE type = 'table' ORDER BY name ASC";

            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        lbTableNames.Items.Clear();
                        while (reader.Read())
                        {
                            lbTableNames.Items.Add(reader.GetString("name"));
                        }
                    }
                }
            }
        }

        private void GetTranslations(string lng)
        {
            string sql = "SELECT id, tbl_name, tbl_column_name, idx, "+lng+" FROM localized_texts ORDER BY idx ASC";

            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    CurrentTranslation.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    //command.Parameters.AddWithValue("@lng", lng);
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            GameTranslation t = new GameTranslation();
                            t.idx = reader.GetInt64("idx");
                            t.table = reader.GetString("tbl_name");
                            t.field = reader.GetString("tbl_column_name");
                            t.value = reader.GetString(lng);
                            CurrentTranslation.Add(reader.GetInt64("id"), t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }
            }
            Properties.Settings.Default.DefaultGameLanguage = lng ;
        }

        private void BtnItemSearch_Click(object sender, EventArgs e)
        {
            dgvItem.Rows.Clear();
            string searchText = tItemSearch.Text ;
            if (searchText == string.Empty)
                return;
            string lng = cbItemSearchLanguage.Text;
            string sql = string.Empty;
            string sqlWhere = string.Empty;
            long searchID ;
            bool SearchByID = false;
            if (long.TryParse(searchText, out searchID))
                SearchByID = true;
            bool showFirst = true;

            // More Complex syntax with category names
            // SELECT t1.idx, t1.ru, t1.ru_ver, t2.ID, t2.category_id, t3.name, t4.en_us FROM localized_texts as t1 LEFT JOIN items as t2 ON (t1.idx = t2.ID) LEFT JOIN item_categories as t3 ON (t2.category_id = t3.ID) LEFT JOIN localized_texts as t4 ON ((t4.idx = t3.ID) AND (t4.tbl_name = 'item_categories') AND (t4.tbl_column_name = 'name') ) WHERE (t1.tbl_name = 'items') AND (t1.tbl_column_name = 'name') AND (t1.ru LIKE '%Камень%') ORDER BY t1.ru ASC 

            if (SearchByID)
            {
                sqlWhere = "(t1.tbl_name = 'items') AND (t1.tbl_column_name = 'name') AND (t1.idx = @searchid)";
            }
            else
            {
                sqlWhere = "(t1.tbl_name = 'items') AND (t1.tbl_column_name = 'name') AND (t1." + lng + " LIKE @searchtext)";
            }

            sql = "SELECT t1.idx, t1." + lng + ", t1." + lng + "_ver FROM localized_texts as t1 WHERE " + sqlWhere + " ORDER BY " + lng + " ASC";

            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Prepare();
                    if (SearchByID)
                    {
                        command.Parameters.AddWithValue("@searchid", searchID.ToString());
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@searchtext", "%" + searchText + "%");
                    }
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;
                        dgvItem.Visible = false;
                        while (reader.Read())
                        {
                            int line = dgvItem.Rows.Add();
                            var row = dgvItem.Rows[line];
                            long itemIdx = reader.GetInt64("idx");
                            row.Cells[0].Value = itemIdx.ToString();
                            row.Cells[1].Value = reader.GetString(lng);

                            if (showFirst)
                            {
                                showFirst = false;
                                ShowDBItem(itemIdx);
                            }
                        }
                        dgvItem.Visible = true;
                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }

        }

        private string GetTranslationByID(long idx, string table, string field, string defaultValue = "")
        {
            foreach(var t in CurrentTranslation)
            {
                if ((t.Value.idx == idx) && (t.Value.table == table) && (t.Value.field == field))
                {
                    string s = t.Value.value;
                    if (s != string.Empty)
                        return s;
                }
            }
            // If no translation found ...
            if (defaultValue == "")
                return "<no_translation_for_" + table + "_" + field + "_" + idx.ToString() + ">";
            else
                return defaultValue;
        }

        private void ShowDBItem(long idx)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM items WHERE (id = @tidx) ORDER BY id ASC";
                    command.Prepare();
                    command.Parameters.AddWithValue("@tidx", idx.ToString());
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        while (reader.Read())
                        {
                            if (reader.GetInt64("id") == idx)
                            {
                                lItemID.Text = idx.ToString();
                                lItemName.Text = GetTranslationByID(idx, "items", "name",reader.GetString("name"));
                                long cat = reader.GetInt64("category_id");
                                lItemCategory.Text = GetTranslationByID(cat, "item_categories", "name") + " (" + cat.ToString() + ")";
                                tItemDesc.Text = GetTranslationByID(idx, "items", "description", reader.GetString("description")).Replace("\n","\r\n");
                                lItemLevel.Text = reader.GetInt64("level").ToString();
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void ShowDBLootByItem(long idx)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM loots WHERE (item_id = @tidx) ORDER BY id ASC";
                    command.Prepare();
                    command.Parameters.AddWithValue("@tidx", idx.ToString());
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        dgvLoot.Rows.Clear();
                        while (reader.Read())
                        {
                            if (reader.GetInt64("item_id") == idx)
                            {
                                int line = dgvLoot.Rows.Add();
                                var row = dgvLoot.Rows[line];

                                row.Cells[0].Value = reader.GetInt64("id").ToString();
                                row.Cells[1].Value = reader.GetInt64("loot_pack_id").ToString();
                                row.Cells[2].Value = idx.ToString();
                                row.Cells[3].Value = GetTranslationByID(idx, "items", "name");
                                row.Cells[4].Value = reader.GetInt64("drop_rate").ToString();
                                row.Cells[5].Value = reader.GetInt64("min_amount").ToString();
                                row.Cells[6].Value = reader.GetInt64("max_amount").ToString();
                                row.Cells[7].Value = reader.GetInt64("grade_id").ToString();
                                row.Cells[8].Value = reader.GetString("always_drop").ToString();
                                row.Cells[9].Value = reader.GetInt64("group").ToString();
                            }
                        }
                    }
                }
            }
        }

        private void ShowDBLootByID(long loot_id)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM loots WHERE (loot_pack_id = @tpackid) ORDER BY id ASC";
                    command.Prepare();
                    command.Parameters.AddWithValue("@tpackid", loot_id.ToString());
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        dgvLoot.Rows.Clear();
                        while (reader.Read())
                        {
                            if (reader.GetInt64("loot_pack_id") == loot_id)
                            {
                                int line = dgvLoot.Rows.Add();
                                var row = dgvLoot.Rows[line];

                                var itemid = reader.GetInt64("item_id");
                                row.Cells[0].Value = reader.GetInt64("id").ToString();
                                row.Cells[1].Value = reader.GetInt64("loot_pack_id").ToString();
                                row.Cells[2].Value = itemid.ToString();
                                row.Cells[3].Value = GetTranslationByID(itemid, "items", "name");
                                row.Cells[4].Value = reader.GetInt64("drop_rate").ToString();
                                row.Cells[5].Value = reader.GetInt64("min_amount").ToString();
                                row.Cells[6].Value = reader.GetInt64("max_amount").ToString();
                                row.Cells[7].Value = reader.GetInt64("grade_id").ToString();
                                row.Cells[8].Value = reader.GetString("always_drop").ToString();
                                row.Cells[9].Value = reader.GetInt64("group").ToString();
                            }
                        }
                    }
                }
            }
        }




        private void TItemSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnItemSearch_Click(null,null);
            }
        }

        private void DgvItemSearch_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvItem.SelectedRows.Count <= 0)
                return;
            var row = dgvItem.SelectedRows[0];
            if (row.Cells.Count <= 0)
                return;

            var val = row.Cells[0].Value;
            if (val == null)
                return;
            ShowDBItem(long.Parse(val.ToString()));
        }

        private void CbItemSearchLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbItemSearchLanguage.Text != Properties.Settings.Default.DefaultGameLanguage)
            {
                GetTranslations(cbItemSearchLanguage.Text);
            }
        }

        private void BtnOpenDB_Click(object sender, EventArgs e)
        {
            LoadDB(true);
        }

        private void LoadDB(bool forceDlg)
        {
            string sqlfile = Properties.Settings.Default.DBFileName;

            while ( forceDlg || (!File.Exists(sqlfile)) )
            {
                forceDlg = false;
                if (openDBDlg.ShowDialog() != DialogResult.OK)
                {
                    Close();
                    return;
                }
                else
                {
                    sqlfile = openDBDlg.FileName;
                }
            }

            SQLite.SQLiteFileName = sqlfile;
            Properties.Settings.Default.DBFileName = sqlfile;

            var i = cbItemSearchLanguage.Items.IndexOf(Properties.Settings.Default.DefaultGameLanguage);
            cbItemSearchLanguage.SelectedIndex = i;
            GetTableNames();
            GetTranslations(Properties.Settings.Default.DefaultGameLanguage);
        }

        private void TItemSearch_TextChanged(object sender, EventArgs e)
        {
            btnItemSearch.Enabled = (tItemSearch.Text != string.Empty);
        }

        private void BtnFindItemInLoot_Click(object sender, EventArgs e)
        {
            if (dgvItem.SelectedRows.Count <= 0)
                return;
            var row = dgvItem.SelectedRows[0];
            if (row.Cells.Count <= 0)
                return;

            var val = row.Cells[0].Value;
            if (val == null)
                return;

            ShowDBLootByItem(long.Parse(val.ToString()));
            tcViewer.SelectedTab = tpLoot;
        }

        private void TLootSearch_TextChanged(object sender, EventArgs e)
        {
            btnLootSearch.Enabled = (tLootSearch.Text != string.Empty);
        }

        private void DgvLoot_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLoot.SelectedRows.Count <= 0)
                return;
            var row = dgvLoot.SelectedRows[0];
            if (row.Cells.Count <= 0)
                return;

            var val = row.Cells[1].Value;
            if (val == null)
                return;
            tLootSearch.Text = val.ToString();
        }

        private void BtnLootSearch_Click(object sender, EventArgs e)
        {
            string searchText = tLootSearch.Text;
            if (searchText == string.Empty)
                return;
            long searchID;
            if (!long.TryParse(searchText, out searchID))
                return;

            ShowDBLootByID(searchID);
        }
    }
}
