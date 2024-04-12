using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AAEmu.DBEditor.data;
using AAEmu.DBEditor.data.gamedb;

namespace AAEmu.DBEditor.forms.client
{
    public partial class ItemsForm : Form
    {
        public ItemsForm()
        {
            InitializeComponent();
        }

        private void ItemsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            cbSearchBox.Text = cbSearchBox.Text.ToLower();
            var s = cbSearchBox.Text;
            var items = new List<Item>();
            if (!uint.TryParse(s, out var id))
                id = 0;
            // Check Exact Id
            if (id > 0)
                items.AddRange(Data.Server.CompactSqlite.Items.Where(x => (x.Id == id)));

            var itemNameMatches = Data.Server.LocalizedText.Where(x =>
                (x.Key.Item1 == "items") && (x.Key.Item2 == "name") && (x.Value.ToLower().Contains(s)));

            foreach (var (iKey, iVal) in itemNameMatches)
                items.Add(Data.Server.GetItem(iKey.Item3 ?? 0));

            lvItems.Items.Clear();
            lvItems.SmallImageList = Data.Client.Icons16;
            lvItems.LargeImageList = Data.Client.Icons32;
            if (items != null)
            {
                foreach (var item in items)
                {
                    if ((item == null) || (item.Id == 0))
                        continue;
                    var itemEntry = Data.Server.GetItem((long)item.Id);
                    var newItem = new ListViewItem();
                    newItem.Text = item.Id.ToString();
                    newItem.ImageIndex = Data.Client.GetIconIndexByItemTemplateId((long)itemEntry.Id);
                    newItem.SubItems.Add(Data.Server.GetText("items", "name", (long)itemEntry.Id, itemEntry.Name));
                    newItem.SubItems.Add(Data.Server.GetText("item_categories", "name", (long)itemEntry.CategoryId, itemEntry.CategoryId.ToString()));

                    lvItems.Items.Add(newItem);
                }
            }

            lvItems.View = View.Details;
        }

        private void cbSearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(sender, null);
            }
        }
    }
}
