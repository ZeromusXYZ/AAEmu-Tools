using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AAEmu.DBEditor.data;
using AAEmu.DBEditor.data.enums;
using AAEmu.DBEditor.data.gamedb;
using AAEmu.DBEditor.utils;

#pragma warning disable WFO1000

namespace AAEmu.DBEditor.forms.client
{
    public partial class ItemsForm : Form
    {
        public bool IsSelectionDialog { get; set; }
        public Item SelectedItem { get; set; }

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
            var items = new Dictionary<long, Item>();
            if (!uint.TryParse(s, out var id))
                id = 0;

            // Check Exact Id (not affected by filters)
            if (id > 0)
            {
                var exactItem = Data.Server.CompactSqlite.Items.FirstOrDefault(x => (x.Id == id));
                if (exactItem != null)
                {
                    items.Add((long)exactItem.Id, exactItem);
                }
            }

            var itemNameMatches = Data.Server.LocalizedText.Where(x =>
                (x.Key.Item1 == "items") && (x.Key.Item2 == "name") && (x.Value.ToLower().Contains(s)));

            foreach (var (iKey, iVal) in itemNameMatches)
                items.TryAdd((long)iKey.Item3, Data.Server.GetItem(iKey.Item3 ?? 0));

            if (cbDescriptionSearch.Checked)
            {
                var itemDescMatches = Data.Server.LocalizedText.Where(x =>
                    (x.Key.Item1 == "items") && (x.Key.Item2 == "description") && (x.Value.ToLower().Contains(s)));

                foreach (var (iKey, iVal) in itemDescMatches)
                    items.TryAdd((long)iKey.Item3, Data.Server.GetItem(iKey.Item3 ?? 0));
            }

            lvItems.Items.Clear();
            lvItems.SmallImageList = Data.Client.Icons16;
            lvItems.LargeImageList = Data.Client.Icons32;
            if (items != null)
            {
                foreach (var (key, item) in items)
                {
                    if ((item == null) || (item.Id == 0))
                        continue;

                    var valid = false;
                    if (cbSearchVanilla.Checked && (item.Id > 0) && (item.Id < 8000000))
                        valid = true;
                    else
                    if (cbSearchRegion.Checked && (item.Id >= 8000000) && (item.Id < 9000000))
                        valid = true;
                    else
                    if (cbSearchCustom.Checked && (item.Id >= 9000000))
                        valid = true;

                    if (cbFilterImplement.SelectedIndex > 0)
                    {
                        if (item.ImplId != cbFilterImplement.SelectedIndex - 1)
                            valid = false;
                    }

                    if ((cbFilterCategory.SelectedIndex > 0) && (cbFilterCategory.SelectedItem is CBoxItem cbi))
                    {
                        if (item.CategoryId != cbi.Value)
                            valid = false;
                    }

                    if (valid == false)
                        continue;

                    var itemEntry = Data.Server.GetItem((long)item.Id);
                    var newItem = new ListViewItem();
                    newItem.Tag = itemEntry;
                    newItem.Text = item.Id.ToString();
                    newItem.ImageIndex = Data.Client.GetIconIndexByItemTemplateId((long)itemEntry.Id);
                    newItem.SubItems.Add(Data.Server.GetText("items", "name", (long)itemEntry.Id, itemEntry.Name));
                    newItem.SubItems.Add(Data.Server.GetText("item_categories", "name", (long)itemEntry.CategoryId, itemEntry.CategoryId.ToString()));

                    lvItems.Items.Add(newItem);
                    if (lvItems.Items.Count > 500)
                    {
                        MessageBox.Show($"List truncated to 500 items");
                        break;
                    }
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

        private void lvItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedItem = null;
            btnSelect.Enabled = false;
            lItemID.Text = "";
            lItemName.Text = "";
            lItemImplId.Text = "0";
            lItemCategory.Text = "";
            lItemLevel.Text = "0";
            lItemRequires.Text = "<none>";
            lItemIcon.ImageList = Data.Client.Icons;
            lItemIcon.ImageIndex = -1;
            lItemIcon.Text = "???";
            lItemGMCommand.Text = "/item";
            rtItemDesc.Text = "";

            if (lvItems.SelectedItems.Count <= 0)
                return;

            ListViewItem lvi = null;
            foreach (ListViewItem i in lvItems.SelectedItems)
            {
                if (i != null)
                {
                    lvi = i;
                    break;
                }
            }

            if ((lvi == null) || (lvi.Tag is not Item item))
                return;

            SelectedItem = item;
            btnSelect.Enabled = IsSelectionDialog;

            lItemID.Text = item.Id.ToString();
            lItemName.Text = Data.Server.GetText("items", "name", (long)item.Id, item.Name);
            lItemImplId.Text = ((ItemImplement)item.ImplId).ToString() + " (" + item.ImplId.ToString() + ")"; ;
            lItemCategory.Text = Data.Server.GetText("item_categories", "name", (long)item.CategoryId, item.CategoryId.ToString());
            lItemLevel.Text = item.Level.ToString();
            if ((item.LevelRequirement > 0) && (item.LevelLimit > 0))
            {
                lItemRequires.Text = item.LevelRequirement.ToString() + " ~ " + item.LevelLimit.ToString();
            }
            else
            if ((item.LevelRequirement > 0) && (item.LevelLimit <= 0))
            {
                lItemRequires.Text = item.LevelRequirement.ToString() + " ~ MaxLv";
            }
            else
            if ((item.LevelRequirement <= 0) && (item.LevelLimit > 0))
            {
                lItemRequires.Text = "1 ~ " + item.LevelLimit.ToString();
            }
            else
            {
                lItemRequires.Text = "<none>";
            }

            var desc = Data.Server.GetText("items", "description", (long)item.Id, item.Description);
            // TODO: Add stats or equip effects to description

            AaTextHelper.FormattedTextToRichText(desc, rtItemDesc);

            lItemIcon.ImageIndex = lvi.ImageIndex;
            lItemIcon.Text = lItemIcon.ImageIndex > 0 ? "" : "not found";

            lItemGMCommand.Text = $"/item add self {item.Id}" + (item.MaxStackSize > 1 ? " " + item.MaxStackSize : "");
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (IsSelectionDialog && SelectedItem != null)
            {
                DialogResult = DialogResult.OK;
                return;
            }
        }

        private void ItemsForm_Load(object sender, EventArgs e)
        {
            cbFilterImplement.Items.Clear();
            cbFilterImplement.Items.Add("<show all>");
            cbFilterImplement.SelectedIndex = 0;
            foreach (var impl in Enum.GetValues(typeof(ItemImplement)))
            {
                cbFilterImplement.Items.Add($"{impl} ({(int)impl})");
            }

            cbFilterCategory.Items.Clear();
            cbFilterCategory.DisplayMember = "Text";
            cbFilterCategory.ValueMember = "Value";
            cbFilterCategory.Items.Add(new CBoxItem("<show all>", -1));

            cbFilterCategory.SelectedIndex = 0;
            foreach (var cat in Data.Server.CompactSqlite.ItemCategories)
            {
                cbFilterCategory.Items.Add(
                    new CBoxItem(
                    $"{Data.Server.GetText("item_categories", "name", (long)cat.Id, cat.Name)} ({cat.Id})"
                    , (long)cat.Id)
                    );
            }

            ClipboardHelper.MakeFormLabelsClickable(this);
        }
    }

    class CBoxItem(string text, long val)
    {
        public string Text { get; } = text;
        public long Value { get; } = val;
    }
}
