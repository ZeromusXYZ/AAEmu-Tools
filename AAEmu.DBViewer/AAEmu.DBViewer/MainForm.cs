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

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!File.Exists(SQLite.SQLiteFileName))
            {
                MessageBox.Show("File not found " + SQLite.SQLiteFileName, "Server DB not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }
            cbItemSearchLanguage.SelectedIndex = 0;
            GetTableNames();
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

        private void BtnItemSearch_Click(object sender, EventArgs e)
        {
            dgvItemSearch.Rows.Clear();
            string searchText = tItemSearch.Text ;
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
                    }
                }
            }

        }

        private string GetTranslationByID(long idx, string table, string field, string lng)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT idx, " + lng + " FROM localized_texts WHERE (tbl_name = @ttbl) AND (tbl_column_name = @tfld) AND (idx = @tidx) ORDER BY " + lng + " ASC LIMIT 1";
                    command.Prepare();
                    command.Parameters.AddWithValue("@tidx", idx.ToString());
                    command.Parameters.AddWithValue("@ttbl", table);
                    command.Parameters.AddWithValue("@tfld", field);
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        while (reader.Read())
                        {
                            if (reader.GetInt64("idx") == idx)
                                return reader.GetString(lng);
                        }
                    }
                }
            }
            return string.Empty;
        }

        private void ShowDBItem(long idx)
        {
            var lng = cbItemSearchLanguage.Text;
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
                                lItemName.Text = GetTranslationByID(idx, "items", "name", lng);
                                long cat = reader.GetInt64("category_id");
                                lItemCategory.Text = GetTranslationByID(cat, "item_categories", "name", lng) + " (" + cat.ToString() + ")";
                                tItemDesc.Text = GetTranslationByID(idx, "items", "description", lng);
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
    }
}
