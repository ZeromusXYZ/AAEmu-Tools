using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AAEmu.DBEditor.data;
using AAEmu.DBEditor.data.gamedb;
using AAEmu.DBEditor.forms.server;
using Newtonsoft.Json;

namespace AAEmu.DBEditor.tools.ahbot
{
    public partial class AhBotForm : Form
    {
        private static AhBotForm _instance;
        private string AhBotCharacter { get; set; }
        private string AhBotAccount { get; set; }
        private Dictionary<long, TreeNode> ItemNodes { get; set; } = [];
        private AhBotSetting Settings { get; set; } = new();
        private AhBotEntry SelectedAhBotItemEntry { get; set; }
        private Item SelectedItem { get; set; }


        public static AhBotForm Instance => _instance ??= new AhBotForm();

        public AhBotForm()
        {
            InitializeComponent();
        }

        private void btnPickAhCharacter_Click(object sender, EventArgs e)
        {
            using var characterForm = new CharacterForm();
            characterForm.btnSelectionOK.Visible = true;
            if (characterForm.ShowDialog() == DialogResult.OK)
            {
                var botCharacter = characterForm.GetSelectCharacter();
                if ((botCharacter != null) && !string.IsNullOrWhiteSpace(botCharacter.Name))
                {
                    lAhBotName.Text = botCharacter.Name;

                    var botAccount = Data.MySqlDb.Login.Users.FirstOrDefault(x => x.Id == botCharacter.AccountId);
                    if ((botAccount != null) && (botAccount.Id == botCharacter.AccountId))
                    {
                        lAhBotAccount.Text = botAccount.Username;

                        AhBotCharacter = botCharacter.Name;
                        AhBotAccount = botAccount.Username;
                        return;
                    }
                }

                lAhBotName.Text = @"<error>";
                lAhBotAccount.Text = @"<error>";
            }
        }

        private void AhBotForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Populate Servers
                cbServers.Items.Clear();
                var serverList = Data.MySqlDb.Login.GameServers.Where(s => s.Hidden == false);
                foreach (var server in serverList)
                {
                    cbServers.Items.Add(server.Name);
                }

                // Populate Items List
                var catA = new Dictionary<long, TreeNode>();
                var catB = new Dictionary<long, TreeNode>();
                var catC = new Dictionary<long, TreeNode>();
                tvAhList.Nodes.Clear();
                ItemNodes.Clear();
                // AH Category A
                foreach (var compactSqliteAuctionACategory in Data.Server.CompactSqlite.AuctionACategories.OrderBy(x =>
                             x.Id))
                {
                    catA.Add((compactSqliteAuctionACategory.Id ?? 0),
                        tvAhList.Nodes.Add(compactSqliteAuctionACategory.Name));
                }

                // AH Category B
                foreach (var (catAKey, catANode) in catA)
                {
                    var thisCatBResults = Data.Server.CompactSqlite.AuctionBCategories
                        .Where(x => x.ParentCategoryId == catAKey).OrderBy(x => x.Id);
                    foreach (var auctionBCategory in thisCatBResults)
                    {
                        catB.Add(auctionBCategory.Id ?? 0, catANode.Nodes.Add(auctionBCategory.Name));
                    }
                }

                // AH Category C
                foreach (var (catBKey, catBNode) in catB)
                {
                    var thisCatCResults = Data.Server.CompactSqlite.AuctionCCategories
                        .Where(x => x.ParentCategoryId == catBKey).OrderBy(x => x.Id);
                    foreach (var auctionCCategory in thisCatCResults)
                    {
                        catC.Add(auctionCCategory.Id ?? 0, catBNode.Nodes.Add(auctionCCategory.Name));
                    }
                }

                // Populate Grades
                cbGrade.Items.Clear();
                foreach (var itemGrade in Data.Server.CompactSqlite.ItemGrades.OrderBy(g => g.Id))
                {
                    var gradeId = itemGrade.Id ?? 0;
                    var gradeName = Data.Server.LocalizedText.GetValueOrDefault(("item_grades", "name", gradeId)) ??
                                    $"<grade {gradeId}>";
                    cbGrade.Items.Add((gradeName, gradeId));
                }

                if (cbGrade.Items.Count > 0)
                    cbGrade.SelectedIndex = cbGrade.Items.Count - 1;

                // Populate Items
                foreach (var item in Data.Server.CompactSqlite.Items.Where(x =>
                             (x.Price > 1) && (x.Refund > 1) && (x.AuctionACategoryId > 0)))
                {
                    if (item.BindId == 2) // Bind on Pickup item can technically not be sold on AH
                        continue;
                    // Localized name
                    var itemName = Data.Server.LocalizedText.GetValueOrDefault(("items", "name", item.Id)) ?? "";
                    // Not translated?
                    if (string.IsNullOrWhiteSpace(itemName) || itemName.StartsWith("DO NOT TRANSLATE"))
                        itemName = item.Name;
                    // No name?
                    if (string.IsNullOrWhiteSpace(itemName))
                        itemName = $"<item {item.Id}>";
                    if (catC.TryGetValue(item.AuctionCCategoryId ?? 0, out var catCNode))
                    {
                        var itemNode = catCNode.Nodes.Add(itemName);
                        itemNode.Tag = (item.Id ?? 0);
                        itemNode.ForeColor = SystemColors.GrayText;
                        ItemNodes.Add(item.Id ?? 0, itemNode);
                    }
                    else if (catB.TryGetValue(item.AuctionBCategoryId ?? 0, out var catBNode))
                    {
                        var itemNode = catBNode.Nodes.Add(itemName);
                        itemNode.Tag = (item.Id ?? 0);
                        itemNode.ForeColor = SystemColors.GrayText;
                        ItemNodes.Add(item.Id ?? 0, itemNode);
                    }
                    else if (catA.TryGetValue(item.AuctionACategoryId ?? 0, out var catANode))
                    {
                        var itemNode = catANode.Nodes.Add(itemName);
                        itemNode.Tag = (item.Id ?? 0);
                        itemNode.ForeColor = SystemColors.GrayText;
                        ItemNodes.Add(item.Id ?? 0, itemNode);
                    }
                }

                btnLoadConfig.PerformClick();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void AhBotForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        private static bool TryDeserializeObject<T>(string json, out T result, out Exception error)
        {
            result = default;

            if (string.IsNullOrWhiteSpace(json))
            {
                error = new ArgumentException("NullOrWhiteSpace", nameof(json));
                return false;
            }

            try
            {
                result = JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception e)
            {
                result = default;
                error = e;
                return false;
            }

            error = null;
            return result != null;
        }

        private void btnLoadConfig_Click(object sender, EventArgs e)
        {
            try
            {
                var settingsString = File.ReadAllText("ahbot.json");
                if (!TryDeserializeObject<AhBotSetting>(settingsString, out var res, out var ex))
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                Settings = res;
                UpdateFromSettings();
            }
            catch (Exception exception)
            {
                // MessageBox.Show(exception.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var res = JsonConvert.SerializeObject(Settings);
            try
            {
                File.WriteAllText("ahbot.json", res ?? string.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateFromSettings()
        {
            // Gray out everything first
            foreach (var (itemId, itemNode) in ItemNodes)
            {
                itemNode.ForeColor = SystemColors.GrayText;
            }

            foreach (var settingsItem in Settings.Items)
            {
                if (!ItemNodes.TryGetValue(settingsItem.ItemId, out var itemNode))
                    continue;
                itemNode.ForeColor = Color.Green;
            }
        }

        private void cbGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectGrade = Data.Server.CompactSqlite.ItemGrades.FirstOrDefault(x => x.Id == cbGrade.SelectedIndex);
            if (selectGrade != null)
            {
                lGrade.ForeColor = int.TryParse(selectGrade.ColorArgb, NumberStyles.HexNumber,
                    CultureInfo.InvariantCulture, out var hexColor)
                    ? Color.FromArgb(hexColor)
                    : SystemColors.GrayText;
            }
            else
            {
                lGrade.ForeColor = SystemColors.ControlText;
            }
        }

        private void tvAhList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectedAhBotItemEntry = null;
            SelectedItem = null;
            lbAhList.Items.Clear();
            if (e.Node?.Tag is long itemId)
            {
                btnUpdateAhItem.Enabled = false;
                btnRemoveItem.Enabled = false;
                var item = Data.Server.CompactSqlite.Items.FirstOrDefault(x => x.Id == itemId);
                if (item != null)
                {
                    SelectedItem = item;
                    lItemId.Text = item.Id.ToString();
                    lItemName.Text = Data.Server.LocalizedText.GetValueOrDefault(("items", "name", itemId)) ??
                                     "<error>";
                    if (item.FixedGrade >= 0)
                    {
                        cbGrade.SelectedIndex = (int)(item.FixedGrade ?? 0);
                        cbGrade.Enabled = false;
                    }
                    else
                    {
                        cbGrade.SelectedIndex = 0;
                        cbGrade.Enabled = true;
                    }
                    tSaleQuantity.Text = item.MaxStackSize.ToString();
                    tBuyOutPrice.Text = item.Price.ToString();
                    tStartBid.Text = item.Refund.ToString();
                    tListedCount.Text = @"1";
                    btnUpdateAhItem.Enabled = true;

                    var ahBotItems = Settings.Items.Where(x => x.ItemId == itemId).ToList();
                    if (ahBotItems.Any())
                    {
                        /*
                        cbGrade.SelectedIndex = ahBotItem.GradeId;
                        tSaleQuantity.Text = ahBotItem.Quantity.ToString(CultureInfo.InvariantCulture);
                        tSalePriceAll.Text = ahBotItem.Price.ToString(CultureInfo.InvariantCulture);
                        tStartBid.Text = ahBotItem.StartBid.ToString(CultureInfo.InvariantCulture);
                        tListedCount.Text = ahBotItem.ItemEntryCount.ToString(CultureInfo.InvariantCulture);
                        SelectedAhBotItemEntry = ahBotItem;
                        btnRemoveItem.Enabled = true;
                        */

                        lbAhList.Items.Clear();
                        foreach (var ahBotItem in ahBotItems)
                        {
                            lbAhList.Items.Add(ahBotItem);
                        }
                    }
                    else
                    {
                    }
                }
            }
        }

        private void btnUpdateAhItem_Click(object sender, EventArgs e)
        {
            /*
            if (SelectedAhBotItemEntry == null)
            {
                MessageBox.Show("No item selected");
                return;
            }
            */

            if (SelectedItem == null)
            {
                MessageBox.Show("No item selected");
                return;
            }
            var itemName = Data.Server.LocalizedText.GetValueOrDefault(("items", "name", SelectedItem.Id)) ?? "";
            // Not translated?
            if (string.IsNullOrWhiteSpace(itemName) || itemName.StartsWith("DO NOT TRANSLATE"))
                itemName = SelectedItem.Name;
            // No name?
            if (string.IsNullOrWhiteSpace(itemName))
                itemName = $"<item:{SelectedItem.Id}>";

            var newGrade = (byte)cbGrade.SelectedIndex;

            if (!int.TryParse(tSaleQuantity.Text, NumberStyles.Integer, CultureInfo.InvariantCulture,
                    out var newQuantity))
            {
                MessageBox.Show("Invalid Quantity field");
                return;
            }

            if (!long.TryParse(tBuyOutPrice.Text, NumberStyles.Integer, CultureInfo.InvariantCulture,
                    out var newBuyOutPrice))
            {
                MessageBox.Show("Invalid buy-out price");
                return;
            }

            if (!long.TryParse(tStartBid.Text, NumberStyles.Integer, CultureInfo.InvariantCulture, out var newStartBid))
            {
                MessageBox.Show("Invalid starting bid price");
                return;
            }

            if (!int.TryParse(tListedCount.Text, NumberStyles.Integer, CultureInfo.InvariantCulture,
                    out var newListedCount))
            {
                MessageBox.Show("Invalid listing count field");
                return;
            }

            if ((newQuantity < 1) || (newQuantity > SelectedItem.MaxStackSize))
            {
                MessageBox.Show($"Quantity for {itemName} must be between 1 and {SelectedItem.MaxStackSize}");
                return;
            }

            if (newBuyOutPrice < SelectedItem.Refund)
            {
                MessageBox.Show($"Buyout price for {itemName} must be at least the refund price of {SelectedItem.Refund}");
                return;
            }

            if (newStartBid >= newBuyOutPrice)
            {
                MessageBox.Show($"Starting bid must be lower than the buyout price");
                return;
            }

            if (newListedCount < 1)
            {
                MessageBox.Show($"You must at least put up one listing");
                return;
            }

            if (newListedCount > 9)
            {
                if (MessageBox.Show($"You really need {newListedCount} entries listed for this item (max recommended is 9)", "", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
            }

            var thisItem = (lbAhList.SelectedItem as AhBotEntry);
            var isNewEntry = thisItem == null;
            if (isNewEntry)
            {
                thisItem = new AhBotEntry();
            }

            thisItem.ItemId = (int)(SelectedItem.Id ?? 0);
            thisItem.GradeId = newGrade;
            thisItem.Quantity = newQuantity;
            thisItem.Price = newBuyOutPrice;
            thisItem.StartBid = newStartBid;
            thisItem.ItemEntryCount = newListedCount;

            if (isNewEntry)
            {
                Settings.Items.Add(thisItem);
            }

            tvAhList_AfterSelect(tvAhList, new TreeViewEventArgs(tvAhList.SelectedNode));
        }
    }
}
