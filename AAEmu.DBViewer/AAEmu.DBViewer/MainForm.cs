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
            dgvItemSearch.Rows.Clear();
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
                        dgvItemSearch.Visible = false;
                        while (reader.Read())
                        {
                            int line = dgvItemSearch.Rows.Add();
                            var row = dgvItemSearch.Rows[line];
                            long itemIdx = reader.GetInt64("idx");
                            row.Cells[0].Value = itemIdx.ToString();
                            row.Cells[1].Value = reader.GetString(lng);

                            if (showFirst)
                            {
                                showFirst = false;
                                ShowDBItem(itemIdx);
                            }
                        }
                        dgvItemSearch.Visible = true;
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



        private void TItemSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnItemSearch_Click(null,null);
            }
        }

        private void DgvItemSearch_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvItemSearch.SelectedRows.Count <= 0)
                return;
            var row = dgvItemSearch.SelectedRows[0];
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
    }
}
