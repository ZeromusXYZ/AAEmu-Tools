using AAEmu.DBEditor;
using AAEmu.DBEditor.data;
using AAEmu.DBEditor.data.aaemu.game;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace AAEmu.DBEditor.forms.server
{
    public partial class ICSForm : Form
    {
        private IcsSkus SelectedSKU { get; set; }
        private IcsShopItems SelectedShopItem { get; set; }
        private static string ShoppingCart { get; } = "🛒";
        private static string ShoppingBag { get; } = "🛍";
        private static string PresentBox { get; } = "🎁";
        private static string MoneyBag { get; } = "💰";

        public ICSForm()
        {
            InitializeComponent();
        }

        private void ICSForm_Load(object sender, EventArgs e)
        {
            MainForm.Self.AddOwnedForm(this);
            SelectedSKU = null;
            SelectedShopItem = null;

            // Populate Menu Dropdowns
            // Main Menu Names (4905 -> 4910)
            cbMainMenu.Items.Clear();
            for (var m = 1; m <= 6; m++)
                cbMainMenu.Items.Add(Data.Server.GetText("ui_texts", "text", 4904 + m, "main_menu_" + m.ToString()));
            cbMainMenu.SelectedIndex = 0;

            // Sub menu for main menu 1 (4911->4917)
            cbSubMenu.Items.Clear();
            for (var s = 1; s <= 7; s++)
            {
                var t = Data.Server.GetText("ui_texts", "text", 4910 + s, string.Empty);
                if (string.IsNullOrWhiteSpace(t))
                    t = "sub_menu_1_" + s.ToString();
                cbSubMenu.Items.Add(t);
            }

            cbSubMenu.SelectedIndex = 0;
            lvMenuItemsTab.ListViewItemSorter = new CompareMenuTabItemsByIndex(lvMenuItemsTab);
            FillSKUList();
            FillShopItemList();
            FillShopTabsPickList();
            FillShopTabsPage();
        }

        private void FillSKUList()
        {
            var skus = Data.MySqlDb.Game.IcsSkus.ToList();
            lvSKUs.Items.Clear();
            lvSKUs.SmallImageList = Data.Client.Icons32;
            SelectedSKU = null;
            foreach (var skuItem in skus)
            {
                if (IsSkuFilteredOut(tFilterSku.Text, skuItem))
                    continue;

                var skuIcon = lvSKUs.Items.Add(skuItem.Sku.ToString());
                skuIcon.Tag = skuItem;
                var itemEntry = Data.Server.CompactSqlite.Items.FirstOrDefault(x => x.Id == skuItem.ItemId);
                if (itemEntry != null)
                {
                    skuIcon.SubItems.Add(Data.Server.GetText("items", "name", (long)skuItem.ItemId, itemEntry.Name));
                    var iconEntry = Data.Server.CompactSqlite.Icons.FirstOrDefault(x => x.Id == itemEntry.IconId);
                    if (iconEntry != null)
                    {
                        skuIcon.ImageIndex = Data.Client.GetIconIndexByName(iconEntry.Filename);
                    }
                }
                else
                {
                    skuIcon.SubItems.Add(skuItem.ItemId.ToString());
                }
                skuIcon.SubItems.Add(skuItem.ItemCount.ToString());
            }
        }

        private bool IsSkuFilteredOut(string rawFilter, IcsSkus skuItem)
        {
            var filterWords = rawFilter.ToLower().Split(" ").ToList();
            if (filterWords.Count == 0)
                return false;

            var itemTexts = skuItem.Sku.ToString() +
                " " + skuItem.ShopId.ToString() +
                " " + skuItem.ItemId.ToString() +
                " " + Data.Server.GetText("items", "name", (long)skuItem.ItemId, "");

            if (skuItem.BonusItemId > 0)
            {
                itemTexts += " " + skuItem.BonusItemId.ToString() +
                    " " + Data.Server.GetText("items", "name", (long)skuItem.BonusItemId, "");
            }

            itemTexts = itemTexts.ToLower();

            foreach (var filterWord in filterWords)
                if (!itemTexts.Contains(filterWord)) return true;

            return false;
        }

        private void FillShopItemList()
        {
            var shopItems = Data.MySqlDb.Game.IcsShopItems.ToList();
            tvShopItems.Nodes.Clear();
            tvShopItems.ImageList = Data.Client.Icons32;
            cbSKUShopEntryId.Items.Clear();
            cbSKUShopEntryId.Items.Add("0");
            foreach (var shopItem in shopItems)
            {
                var subSKUs = Data.MySqlDb.Game.IcsSkus.Where(x => x.ShopId == shopItem.ShopId).OrderBy(x => x.Position).ToList();
                if (IsShopItemFilteredOut(tFilterShopItem.Text, shopItem, subSKUs))
                    continue;

                cbSKUShopEntryId.Items.Add(shopItem.ShopId);

                var itemNode = tvShopItems.Nodes.Add(shopItem.Name);
                itemNode.Tag = shopItem;
                long displayIconItemId = 0;
                var displayName = string.Empty;
                var minPrice = 0u;
                var maxPrice = 0u;
                var currencyName = string.Empty;
                foreach (var subSku in subSKUs)
                {
                    if ((minPrice == 0) && (maxPrice == 0) && (subSku.Price > 0))
                    {
                        minPrice = subSku.Price ?? 0;
                        maxPrice = minPrice;
                    }
                    switch (subSku.Currency)
                    {
                        case 0: currencyName = "Credits"; break;
                        case 1: currencyName = "AA Points"; break;
                        case 2: currencyName = "Loyalty"; break;
                        case 3: currencyName = "Coins"; break;
                    }
                    var itemEntry = Data.Server.CompactSqlite.Items.FirstOrDefault(x => x.Id == subSku.ItemId);
                    var subItemName = "<item:" + subSku.ItemId.ToString() + ">";
                    if (itemEntry != null)
                        subItemName = Data.Server.GetText("items", "name", (long)subSku.ItemId, itemEntry.Name);

                    if (string.IsNullOrEmpty(displayName))
                        displayName = subItemName;

                    var subNode = itemNode.Nodes.Add(subSku.ItemCount == 1 ? subItemName : subItemName + " x " + subSku.ItemCount.ToString());
                    subNode.Tag = subSku;
                    subNode.Text += Environment.NewLine + (subSku.DiscountPrice <= 0 ? $"Price: {subSku.Price} {currencyName}" : $"Discounted Price: {subSku.DiscountPrice} {currencyName}");
                    subNode.ImageIndex = Data.Client.GetIconIndexByItemTemplateId((long)subSku.ItemId);
                    subNode.SelectedImageIndex = subNode.ImageIndex;
                    if (displayIconItemId == 0)
                        displayIconItemId = (long)subSku.ItemId;

                    minPrice = Math.Min(minPrice, subSku.Price ?? minPrice);
                    maxPrice = Math.Max(maxPrice, subSku.Price ?? maxPrice);
                }

                if (string.IsNullOrEmpty(shopItem.Name) && (!string.IsNullOrEmpty(displayName)))
                    itemNode.Text = displayName;

                itemNode.Text += Environment.NewLine + $"Price: {minPrice} ~ {maxPrice} {currencyName}";

                itemNode.ImageIndex = Data.Client.GetIconIndexByItemTemplateId(shopItem.DisplayItemId > 0 ? shopItem.DisplayItemId : displayIconItemId);
                itemNode.SelectedImageIndex = itemNode.ImageIndex;
            }
        }

        private bool IsShopItemFilteredOut(string rawFilter, IcsShopItems shopItem, List<IcsSkus> subSKUs)
        {
            var filterWords = rawFilter.ToLower().Split(" ").ToList();
            if (filterWords.Count == 0)
                return false;

            // Main Shop Item entry
            var itemTexts = shopItem.ShopId.ToString() +
                " " + shopItem.Name;

            if (shopItem.DisplayItemId > 0)
            {
                itemTexts += " " + shopItem.DisplayItemId.ToString() +
                " " + Data.Server.GetText("items", "name", (long)shopItem.DisplayItemId, "");
            }

            if (shopItem.IsSale > 0)
                itemTexts += " sale";

            if (shopItem.IsHidden > 0)
                itemTexts += " hidden";

            // Also check it's sub-SKUs
            foreach (var skuItem in subSKUs)
            {
                itemTexts += skuItem.Sku.ToString() +
                " " + skuItem.ShopId.ToString() +
                " " + skuItem.ItemId.ToString() +
                " " + Data.Server.GetText("items", "name", (long)skuItem.ItemId, "");

                if (skuItem.BonusItemId > 0)
                {
                    itemTexts += " " + skuItem.BonusItemId.ToString() +
                        " " + Data.Server.GetText("items", "name", (long)skuItem.BonusItemId, "");
                }

                if (skuItem.EventType > 0)
                    itemTexts += " event";
            }

            itemTexts = itemTexts.ToLower();

            foreach (var filterWord in filterWords)
                if (!itemTexts.Contains(filterWord)) return true;

            return false;
        }

        private void FillShopTabsPickList()
        {
            // Filter Left Side
            var shopItems = Data.MySqlDb.Game.IcsShopItems.ToList();
            lvMenuShopItemList.Items.Clear();
            lvMenuShopItemList.SmallImageList = Data.Client.Icons32;
            lvMenuShopItemList.LargeImageList = Data.Client.Icons32;
            foreach (var shopItem in shopItems)
            {
                var subSKUs = Data.MySqlDb.Game.IcsSkus.Where(x => x.ShopId == shopItem.ShopId).OrderBy(x => x.Position).ToList();
                if (IsShopItemFilteredOut(tFilterMenuShopItemList.Text, shopItem, subSKUs))
                    continue;

                var itemNode = lvMenuShopItemList.Items.Add(shopItem.Name);
                itemNode.Tag = shopItem;
                long displayIconItemId = shopItem.DisplayItemId;
                var displayName = shopItem.Name;

                foreach (var subSku in subSKUs)
                {
                    var itemEntry = Data.Server.CompactSqlite.Items.FirstOrDefault(x => x.Id == subSku.ItemId);
                    var subItemName = "<item:" + subSku.ItemId.ToString() + ">";
                    if (itemEntry != null)
                        subItemName = Data.Server.GetText("items", "name", (long)subSku.ItemId, itemEntry.Name);

                    if ((displayIconItemId <= 0) && (subSku.ItemId > 0))
                        displayIconItemId = (long)subSku.ItemId;

                    if (string.IsNullOrWhiteSpace(displayName))
                    {
                        displayName = subItemName;
                        break;
                    }
                }

                itemNode.Text = displayName;
                itemNode.ImageIndex = Data.Client.GetIconIndexByItemTemplateId(displayIconItemId);
            }
        }

        private void RePageTabPage()
        {
            var itemPerPage = (cbMainMenu.SelectedIndex == 0 && cbSubMenu.SelectedIndex == 0) ? 4 : 8;

            var newPos = 0;
            foreach (ListViewItem item in lvMenuItemsTab.Items)
            {
                var thisPage = (int)Math.Ceiling((item.Index + 0.75f) / itemPerPage);
                while (lvMenuItemsTab.Groups.Count < thisPage)
                {
                    var newPage = (lvMenuItemsTab.Groups.Count + 1);
                    lvMenuItemsTab.Groups.Add(newPage.ToString(), "Page " + newPage.ToString());
                }
                // item.Group = lvMenuItemsTab.Groups[thisPage.ToString()];
                if (item.Tag is IcsMenu menuItem)
                {
                    menuItem.TabPos = newPos;
                }
                newPos++;
            }
        }

        private void FillShopTabsPage(IcsMenu newlyAddedItem = null)
        {
            // Fill Tab's page
            var mainMenu = cbMainMenu.SelectedIndex;
            var subMenu = cbSubMenu.SelectedIndex;
            lvMenuItemsTab.Items.Clear();
            lvMenuItemsTab.Groups.Clear();
            lvMenuItemsTab.LargeImageList = Data.Client.Icons;
            lvMenuItemsTab.SmallImageList = Data.Client.Icons;

            if (mainMenu < 0 || subMenu < 0)
                return;

            var itemPerPage = (mainMenu == 1 && subMenu == 1) ? 4 : 8;

            // Add one because of how it's used in the game and DB
            mainMenu++;
            subMenu++;

            // Grab tab's contents
            var tabItems = Data.MySqlDb.Game.IcsMenu.Where(x => (x.MainTab == mainMenu) && (x.SubTab == subMenu)).OrderBy(x => x.TabPos).ToList();
            lPageCount.Text = Math.Ceiling(tabItems.Count * 1f / itemPerPage).ToString() + " page(s)";
            foreach (var menuItem in tabItems)
            {
                // Grab the entry's shopItem
                var shopItem = Data.MySqlDb.Game.IcsShopItems.FirstOrDefault(x => x.ShopId == menuItem.ShopId);
                if (shopItem == null)
                    continue; // Not found? Ignore!

                var subSKUs = Data.MySqlDb.Game.IcsSkus.Where(x => x.ShopId == shopItem.ShopId).OrderBy(x => x.Position).ToList();

                var displayName = shopItem.Name;
                var displayItemId = shopItem.DisplayItemId;

                // Grab name to display
                if (string.IsNullOrEmpty(displayName))
                    foreach (var skuItem in subSKUs)
                    {
                        if (skuItem.ItemId <= 0)
                            continue;

                        displayName = Data.Server.GetText("items", "name", (long)skuItem.ItemId, string.Empty);

                        if (!string.IsNullOrEmpty(displayName))
                            break;
                    }

                // Grab itemId of what we want to use as a icon
                if (displayItemId <= 0)
                    foreach (var sku in subSKUs)
                    {
                        if (sku.ItemId <= 0)
                            continue;
                        displayItemId = (uint)sku.ItemId;
                        break;
                    }

                var buttonText = string.Empty;
                if ((shopItem.ShopButtons & 0x1) == 0)
                    buttonText += ShoppingCart;
                if ((shopItem.ShopButtons & 0x2) == 0)
                    buttonText += PresentBox;

                var newItem = new ListViewItem();
                newItem.Tag = menuItem;
                newItem.Text = string.IsNullOrWhiteSpace(buttonText) ? displayName : buttonText + " " + displayName;
                newItem.ImageIndex = Data.Client.GetIconIndexByItemTemplateId(displayItemId);

                lvMenuItemsTab.Items.Add(newItem);
                if (menuItem == newlyAddedItem)
                {
                    newItem.Selected = true;
                }
            }
            RePageTabPage();
            btnAutoCreateAllItemsTab.Enabled = ((mainMenu != 1) && (subMenu == 1)) || ((mainMenu == 1) && (subMenu == 4));
        }

        private void lvSKUs_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem selectedItem in lvSKUs.SelectedItems)
            {
                if (selectedItem.Tag is IcsSkus sku)
                {
                    SelectedSKU = sku;
                    break;
                }
            }

            // Nothing (valid) selected
            if (SelectedSKU == null)
                return;

            tSKUSKU.Text = SelectedSKU.Sku.ToString();
            cbSKUShopEntryId.Text = SelectedSKU.ShopId.ToString();
            tSKUShopEntryPosition.Text = SelectedSKU.Position.ToString();
            cbSKUIsDefault.Checked = SelectedSKU.IsDefault > 0;
            tSKUItemId.Text = SelectedSKU.ItemId.ToString();
            tSKUItemCount.Text = SelectedSKU.ItemCount.ToString();
            tSKUSelectType.Text = SelectedSKU.SelectType.ToString();
            tSKUEventType.Text = SelectedSKU.EventType.ToString();

            if ((SelectedSKU.EventEndDate == null) || (SelectedSKU.EventEndDate <= DateTime.MinValue))
            {
                cbSKUEventHasEnd.Checked = false;
                dtpSKUEventEndDate.Value = DateTime.Today.AddDays(14);
            }
            else
            {
                cbSKUEventHasEnd.Checked = true;
                dtpSKUEventEndDate.Value = SelectedSKU.EventEndDate ?? DateTime.MinValue;
            }
            tSKUPrice.Text = SelectedSKU.Price.ToString();
            rbSKUCurrencyCredits.Checked = SelectedSKU.Currency == 0;
            rbSKUCurrencyAAPoints.Checked = SelectedSKU.Currency == 1;
            rbSKUCurrencyLoyalty.Checked = SelectedSKU.Currency == 2;
            rbSKUCurrencyCoins.Checked = SelectedSKU.Currency == 3;
            tSKUDiscountPrice.Text = SelectedSKU.DiscountPrice.ToString();
            tSKUBonusItemId.Text = SelectedSKU.BonusItemId.ToString();
            tSKUBonusItemCount.Text = SelectedSKU.BonusItemCount.ToString();

            btnSKUUpdate.Enabled = false;
            btnSKUNew.Enabled = false;
        }

        private void tvShopItems_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            var brush = Brushes.Black;
            e.Graphics.DrawString(e.Node.Text, tvShopItems.Font, brush, Rectangle.Inflate(e.Bounds, 2, 0));
        }

        private void tSKU_Changed(object sender, EventArgs e)
        {
            if (SelectedSKU == null)
                return;

            var isNewSKU = false;
            var hasChanged = false;

            if (long.TryParse(tSKUSKU.Text, out var skuId))
                isNewSKU |= ((skuId > 0) && (skuId != SelectedSKU.Sku));

            if (long.TryParse(cbSKUShopEntryId.Text, out var shopId))
                hasChanged |= (shopId != SelectedSKU.ShopId);

            if (int.TryParse(tSKUShopEntryPosition.Text, out var shopPosition))
                hasChanged |= (shopPosition != SelectedSKU.Position);

            if (cbSKUIsDefault.Checked != (SelectedSKU.IsDefault != 0))
                hasChanged |= true;

            if (long.TryParse(tSKUItemId.Text, out var itemId))
                hasChanged |= (itemId != SelectedSKU.ItemId);

            if (long.TryParse(tSKUItemCount.Text, out var itemCount))
                hasChanged |= ((itemCount > 0) && (itemCount != SelectedSKU.ItemCount));

            if (byte.TryParse(tSKUSelectType.Text, out var selectType))
                hasChanged |= (selectType != SelectedSKU.SelectType);

            if (byte.TryParse(tSKUEventType.Text, out var eventType))
                hasChanged |= (eventType != SelectedSKU.EventType);

            if ((SelectedSKU.EventEndDate == null) || (SelectedSKU.EventEndDate <= DateTime.MinValue))
            {
                hasChanged |= cbSKUEventHasEnd.Checked;
            }
            else
            {
                hasChanged |= !cbSKUEventHasEnd.Checked || dtpSKUEventEndDate.Value != SelectedSKU.EventEndDate;
            }

            if (long.TryParse(tSKUPrice.Text, out var price))
                hasChanged |= (price != SelectedSKU.Price);

            hasChanged |= (rbSKUCurrencyCredits.Checked != (SelectedSKU.Currency == 0));
            hasChanged |= (rbSKUCurrencyAAPoints.Checked != (SelectedSKU.Currency == 1));
            hasChanged |= (rbSKUCurrencyLoyalty.Checked != (SelectedSKU.Currency == 2));
            hasChanged |= (rbSKUCurrencyCoins.Checked != (SelectedSKU.Currency == 3));

            if (long.TryParse(tSKUDiscountPrice.Text, out var discountedPrice))
                hasChanged |= (discountedPrice != SelectedSKU.DiscountPrice);

            if (long.TryParse(tSKUBonusItemId.Text, out var bonusItemId))
                hasChanged |= (bonusItemId != SelectedSKU.BonusItemId);

            if (long.TryParse(tSKUBonusItemCount.Text, out var bonusItemCount))
                hasChanged |= (bonusItemCount != SelectedSKU.BonusItemCount);

            if (isNewSKU)
                hasChanged = false;

            if ((discountedPrice > 0) && (price > 0))
            {
                var discountRate = ((100f / price) * discountedPrice) - 100f;
                if (discountRate != 0)
                {
                    lSKUDiscountCalculation.Text = discountRate.ToString("F1") + " %";
                }
                if (discountRate > 0)
                    lSKUDiscountCalculation.ForeColor = Color.Red;
                if (discountRate < 0)
                    lSKUDiscountCalculation.ForeColor = Color.Green;
                if (discountRate <= -45)
                    lSKUDiscountCalculation.ForeColor = Color.Purple;
            }
            else
            {
                lSKUDiscountCalculation.Text = "---";
                lSKUDiscountCalculation.ForeColor = SystemColors.ControlText;
            }

            btnSKUNew.Enabled = isNewSKU;
            btnSKUUpdate.Enabled = hasChanged;
        }

        private void btnSKUGetNewId_Click(object sender, EventArgs e)
        {
            var lastSku = Data.MySqlDb.Game.IcsSkus.OrderBy(x => x.Sku).Reverse().FirstOrDefault();
            if (lastSku == null)
            {
                tSKUSKU.Text = "1000000";
            }
            tSKUSKU.Text = (lastSku.Sku + 1).ToString();
        }

        private void btnSKUUpdate_Click(object sender, EventArgs e)
        {
            if (SelectedSKU == null)
                return;

            try
            {
                if (!uint.TryParse(cbSKUShopEntryId.Text, out var shopItemId))
                    shopItemId = 0;

                if (shopItemId == 0)
                {
                    // Create new default shop for this SKU
                    var lastShopItem = Data.MySqlDb.Game.IcsShopItems.OrderBy(x => x.ShopId).Reverse().FirstOrDefault();
                    if (lastShopItem == null)
                    {
                        shopItemId = 2000000;
                    }
                    else
                    {
                        shopItemId = lastShopItem.ShopId + 1;
                    }
                    var newShopItem = new IcsShopItems();
                    newShopItem.ShopId = shopItemId;
                    Data.MySqlDb.Game.IcsShopItems.Add(newShopItem);
                }

                SelectedSKU.Sku = uint.Parse(tSKUSKU.Text);
                SelectedSKU.ShopId = shopItemId;
                SelectedSKU.Position = int.Parse(tSKUShopEntryPosition.Text);
                SelectedSKU.IsDefault = (byte)(cbSKUIsDefault.Checked ? 1 : 0);
                SelectedSKU.ItemId = uint.Parse(tSKUItemId.Text);
                SelectedSKU.ItemCount = uint.Parse(tSKUItemCount.Text);
                SelectedSKU.SelectType = byte.Parse(tSKUSelectType.Text);
                SelectedSKU.EventType = byte.Parse(tSKUEventType.Text);
                if (cbSKUEventHasEnd.Checked)
                    SelectedSKU.EventEndDate = dtpSKUEventEndDate.Value;
                else
                    SelectedSKU.EventEndDate = DateTime.MinValue;
                SelectedSKU.Price = uint.Parse(tSKUPrice.Text);
                if (rbSKUCurrencyCredits.Checked)
                    SelectedSKU.Currency = 0;
                if (rbSKUCurrencyAAPoints.Checked)
                    SelectedSKU.Currency = 1;
                if (rbSKUCurrencyLoyalty.Checked)
                    SelectedSKU.Currency = 2;
                if (rbSKUCurrencyCoins.Checked)
                    SelectedSKU.Currency = 3;
                SelectedSKU.DiscountPrice = uint.Parse(tSKUDiscountPrice.Text);
                SelectedSKU.BonusItemId = uint.Parse(tSKUBonusItemId.Text);
                SelectedSKU.BonusItemCount = uint.Parse(tSKUBonusItemCount.Text);

                if (Data.MySqlDb.Game.SaveChanges() <= 0)
                {
                    MessageBox.Show("Failed to save SKU changes to DB");
                    return;
                }
                FillSKUList();
                FillShopItemList();
                FillShopTabsPickList();
                FillShopTabsPage();
            }
            catch (Exception ex)
            {
                // Error parsing or updating DB
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void btnSKUNew_Click(object sender, EventArgs e)
        {
            var newSku = new IcsSkus();
            newSku.Sku = uint.Parse(tSKUSKU.Text); // Set this so we won't have duplicate keys

            Data.MySqlDb.Game.IcsSkus.Add(newSku);
            SelectedSKU = newSku;
            btnSKUUpdate_Click(null, null); // Call our regular update function to fill and save the rest.
        }

        private void tvShopItems_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if ((tvShopItems.SelectedNode?.Tag is IcsSkus) && (tvShopItems.SelectedNode?.Parent?.Tag is IcsShopItems icsParent))
            {
                SelectedShopItem = icsParent;
            }
            else if (tvShopItems.SelectedNode?.Tag is not IcsShopItems ics)
            {
                return;
            }
            else
            {
                SelectedShopItem = ics;
            }

            // Nothing (valid) selected
            if (SelectedShopItem == null)
                return;

            tShopItemShopId.Text = SelectedShopItem.ShopId.ToString();
            tShopItemDisplayItemId.Text = SelectedShopItem.DisplayItemId.ToString();
            tShopItemName.Text = SelectedShopItem.Name?.ToString() ?? string.Empty;

            // Shop Buttons
            cbShopItemAllowCart.Checked = (SelectedShopItem.ShopButtons & 0x1) == 0;
            cbShopItemAllowGift.Checked = (SelectedShopItem.ShopButtons & 0x2) == 0;

            cbShopItemIsHidden.Checked = (SelectedShopItem.IsHidden != 0);

            cbShopItemLimitedType.Checked = (SelectedShopItem.LimitedType > 0);
            tShopItemLimitedStockMax.Text = SelectedShopItem.LimitedStockMax.ToString();
            tShopItemRemaining.Text = SelectedShopItem.Remaining.ToString();
            tShopItemMinLevel.Text = SelectedShopItem.LevelMin.ToString();
            tShopItemMaxLevel.Text = SelectedShopItem.LevelMax.ToString();

            rbShopItemRestrictNone.Checked = SelectedShopItem.BuyRestrictType == 0;
            rbShopItemRestrictLevel.Checked = SelectedShopItem.BuyRestrictType == 1;
            rbShopItemRestrictQuest.Checked = SelectedShopItem.BuyRestrictType == 2;
            tShopItemBuyRestrictId.Text = SelectedShopItem.BuyRestrictId.ToString();

            tShopItemIsSale.Text = SelectedShopItem.IsSale.ToString();


            if ((SelectedShopItem.SaleStart == null) || (SelectedShopItem.SaleStart <= DateTime.MinValue))
                dtpShopItemSaleStart.Value = DateTime.Today;
            else
                dtpShopItemSaleStart.Value = SelectedShopItem.SaleStart ?? DateTime.MinValue;

            if ((SelectedShopItem.SaleEnd == null) || (SelectedShopItem.SaleEnd <= DateTime.MinValue))
                dtpShopItemSaleEnd.Value = DateTime.Today.AddDays(14);
            else
                dtpShopItemSaleEnd.Value = SelectedShopItem.SaleEnd ?? DateTime.MinValue;
        }

        private void tShopItems_Changed(object sender, EventArgs e)
        {
            if (SelectedShopItem == null)
                return;

            var isNewShopItem = false;
            var hasChanged = false;

            if (long.TryParse(tShopItemShopId.Text, out var shopId))
                isNewShopItem |= ((shopId > 0) && (shopId != SelectedShopItem.ShopId));

            if (int.TryParse(tShopItemDisplayItemId.Text, out var displayItemId))
                hasChanged |= (displayItemId != SelectedShopItem.DisplayItemId);

            hasChanged |= (tShopItemName.Text != (SelectedShopItem.Name ?? string.Empty));

            hasChanged |= (cbShopItemAllowCart.Checked != ((SelectedShopItem.ShopButtons & 0x1) == 0));

            hasChanged |= (cbShopItemAllowGift.Checked != ((SelectedShopItem.ShopButtons & 0x2) == 0));

            hasChanged |= (cbShopItemIsHidden.Checked != (SelectedShopItem.IsHidden != 0));

            hasChanged |= (cbShopItemLimitedType.Checked != ((SelectedShopItem.LimitedType > 0)));

            if (uint.TryParse(tShopItemLimitedStockMax.Text, out var limitedStopMax))
                hasChanged |= (limitedStopMax != SelectedShopItem.LimitedStockMax);

            if (long.TryParse(tShopItemRemaining.Text, out var remaining))
                hasChanged |= (remaining != SelectedShopItem.Remaining);

            if (byte.TryParse(tShopItemMinLevel.Text, out var minimumLevel))
                hasChanged |= (minimumLevel != SelectedShopItem.LevelMin);

            if (byte.TryParse(tShopItemMaxLevel.Text, out var maximumLevel))
                hasChanged |= (maximumLevel != SelectedShopItem.LevelMax);

            if (uint.TryParse(tShopItemBuyRestrictId.Text, out var restrictId))
                hasChanged |= (restrictId != SelectedShopItem.BuyRestrictId);

            hasChanged |= (rbShopItemRestrictNone.Checked != (SelectedShopItem.BuyRestrictType == 0));
            hasChanged |= (rbShopItemRestrictLevel.Checked != (SelectedShopItem.BuyRestrictType == 1));
            hasChanged |= (rbShopItemRestrictQuest.Checked != (SelectedShopItem.BuyRestrictType == 2));

            if (byte.TryParse(tShopItemIsSale.Text, out var salesType))
                hasChanged |= (salesType != SelectedShopItem.IsSale);
            else
                salesType = 0;

            if ((SelectedShopItem.SaleStart == null) || (SelectedShopItem.SaleStart <= DateTime.MinValue))
            {
                hasChanged |= salesType != 0;
            }
            else
            {
                hasChanged |= salesType == 0 || dtpShopItemSaleStart.Value != SelectedShopItem.SaleStart;
            }

            if ((SelectedShopItem.SaleEnd == null) || (SelectedShopItem.SaleEnd <= DateTime.MinValue))
            {
                hasChanged |= salesType != 0;
            }
            else
            {
                hasChanged |= salesType == 0 || dtpShopItemSaleEnd.Value != SelectedShopItem.SaleEnd;
            }

            if (isNewShopItem)
                hasChanged = false;

            btnShopItemNew.Enabled = isNewShopItem;
            btnShopItemUpdate.Enabled = hasChanged;
        }

        private void btnNewShopItemId_Click(object sender, EventArgs e)
        {
            var lastShopItem = Data.MySqlDb.Game.IcsShopItems.OrderBy(x => x.ShopId).Reverse().FirstOrDefault();
            if (lastShopItem == null)
            {
                tShopItemShopId.Text = "2000000";
            }
            tShopItemShopId.Text = (lastShopItem.ShopId + 1).ToString();
        }

        private void btnShopItemUpdate_Click(object sender, EventArgs e)
        {
            if (SelectedShopItem == null)
                return;

            try
            {
                SelectedShopItem.ShopId = uint.Parse(tShopItemShopId.Text);
                SelectedShopItem.DisplayItemId = uint.Parse(tShopItemDisplayItemId.Text);
                SelectedShopItem.Name = tShopItemName.Text;
                SelectedShopItem.ShopButtons = (byte)((cbShopItemAllowCart.Checked ? 0x0 : 0x1) + (cbShopItemAllowGift.Checked ? 0x0 : 0x2));
                SelectedShopItem.IsHidden = (byte)(cbShopItemIsHidden.Checked ? 1 : 0);
                SelectedShopItem.LimitedType = (byte)(cbShopItemLimitedType.Checked ? 1 : 0);
                SelectedShopItem.LimitedStockMax = uint.Parse(tShopItemLimitedStockMax.Text);
                SelectedShopItem.Remaining = int.Parse(tShopItemRemaining.Text);
                SelectedShopItem.LevelMin = byte.Parse(tShopItemMinLevel.Text);
                SelectedShopItem.LevelMax = byte.Parse(tShopItemMaxLevel.Text);

                if (rbShopItemRestrictNone.Checked)
                    SelectedShopItem.BuyRestrictType = 0;
                else if (rbShopItemRestrictLevel.Checked)
                    SelectedShopItem.BuyRestrictType = 1;
                else if (rbShopItemRestrictQuest.Checked)
                    SelectedShopItem.BuyRestrictType = 2;
                else
                    SelectedShopItem.BuyRestrictType = 0;
                SelectedShopItem.BuyRestrictId = uint.Parse(tShopItemBuyRestrictId.Text);
                if (SelectedShopItem.BuyRestrictId <= 0)
                    SelectedShopItem.BuyRestrictType = 0;

                SelectedShopItem.IsSale = byte.Parse(tShopItemIsSale.Text);
                if (SelectedShopItem.IsSale > 0)
                {
                    SelectedShopItem.SaleStart = dtpShopItemSaleStart.Value;
                    SelectedShopItem.SaleEnd = dtpShopItemSaleEnd.Value;
                }
                else
                {
                    SelectedShopItem.SaleStart = DateTime.MinValue;
                    SelectedShopItem.SaleEnd = DateTime.MinValue;
                }


                if (Data.MySqlDb.Game.SaveChanges() <= 0)
                {
                    MessageBox.Show("Failed to save Shop Item changes to DB");
                    return;
                }
                FillSKUList();
                FillShopItemList();
                FillShopTabsPickList();
                FillShopTabsPage();
            }
            catch (Exception ex)
            {
                // Error parsing or updating DB
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void btnShopItemNew_Click(object sender, EventArgs e)
        {
            var newShopItem = new IcsShopItems();
            newShopItem.ShopId = uint.Parse(tShopItemShopId.Text); // Set this so we won't have duplicate keys

            Data.MySqlDb.Game.IcsShopItems.Add(newShopItem);
            SelectedShopItem = newShopItem;
            btnShopItemUpdate_Click(null, null); // Call our regular update function to fill and save the rest.
        }

        private void tFilterSku_TextChanged(object sender, EventArgs e)
        {
            FillSKUList();
        }

        private void tFilterShopItem_TextChanged(object sender, EventArgs e)
        {
            FillShopItemList();
        }

        private void lvMenuShopItemList_MouseDown(object sender, MouseEventArgs e)
        {
            var underCursor = lvMenuShopItemList.GetItemAt(e.X, e.Y);
            if (underCursor == null)
                return;
            underCursor.Selected = true;
            if (underCursor.Tag is IcsShopItems shopItem)
                lvMenuShopItemList.DoDragDrop(shopItem, DragDropEffects.Link);
        }

        private void tFilterMenuShopItemList_TextChanged(object sender, EventArgs e)
        {
            FillShopTabsPickList();
        }

        private void lvMenuItemsTab_DragEnter(object sender, DragEventArgs e)
        {
            var shopItem = e.Data.GetData(typeof(IcsShopItems)) as IcsShopItems;
            var menuItem = e.Data.GetData(typeof(IcsMenu)) as IcsMenu;
            if (shopItem != null)
            {
                e.Effect = DragDropEffects.Link;
            }
            else if (menuItem != null)
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void lvMenuItemsTab_DragLeave(object sender, EventArgs e)
        {
            // Nothing to do
        }

        private void lvMenuItemsTab_DragDrop(object sender, DragEventArgs e)
        {
            var shopItem = e.Data.GetData(typeof(IcsShopItems)) as IcsShopItems;
            var menuItem = e.Data.GetData(typeof(IcsMenu)) as IcsMenu;
            if ((shopItem == null) && (menuItem == null))
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            // Where did we drop it?
            var targetPoint = lvMenuItemsTab.PointToClient(new Point(e.X, e.Y));
            var targetItemIndex = lvMenuItemsTab.InsertionMark.NearestIndex(targetPoint);
            if (lvMenuItemsTab.InsertionMark.AppearsAfterItem)
                targetItemIndex++;

            // We're dropping a IcsMenu item, so we're moving things
            if (menuItem != null)
            {
                var oldIndex = -1;
                ListViewItem oldItem = null;
                foreach (ListViewItem item in lvMenuItemsTab.Items)
                {
                    if (item.Tag == menuItem)
                    {
                        oldIndex = item.Index;
                        oldItem = item;
                        break;
                    }
                }

                if ((oldIndex < 0) || (oldItem == null) || (oldIndex == targetItemIndex))
                    return; // couldn't find the old ListViewItem that's being dragged, or it wasn't dragged at all

                // If moved to the end of the list
                if (targetItemIndex >= lvMenuItemsTab.Items.Count)
                {
                    lvMenuItemsTab.Items.RemoveAt(oldIndex);
                    lvMenuItemsTab.Items.Add(oldItem);
                }
                else
                {
                    // Moving backwards/forwards
                    lvMenuItemsTab.Items.RemoveAt(oldIndex);
                    lvMenuItemsTab.Items.Insert(targetItemIndex, oldItem);
                }

                RePageTabPage();
                if (Data.MySqlDb.Game.SaveChanges() <= 0)
                {
                    MessageBox.Show("Failed to save menu move changes to DB");
                    return;
                }
                lvMenuItemsTab.Sort();
                return;
            }

            // Otherwise we're dragging in a IcsShopItem and need to make a new entry

            // New MenuItem
            var newIcsMenu = new IcsMenu();
            newIcsMenu.MainTab = (byte)(cbMainMenu.SelectedIndex + 1);
            newIcsMenu.SubTab = (byte)(cbSubMenu.SelectedIndex + 1);
            newIcsMenu.TabPos = 9999;
            newIcsMenu.ShopId = shopItem.ShopId;
            Data.MySqlDb.Game.IcsMenu.Add(newIcsMenu);

            var newItem = new ListViewItem();
            newItem.Text = shopItem.ShopId.ToString();
            newItem.Tag = newIcsMenu;
            newItem.ImageIndex = Data.Client.GetIconIndexByItemTemplateId(shopItem.DisplayItemId);
            newItem.Text = shopItem.Name + " - " + shopItem.ShopId.ToString();

            if ((targetItemIndex >= 0) && (targetItemIndex < lvMenuItemsTab.Items.Count))
            {
                lvMenuItemsTab.Items.Insert(targetItemIndex, newItem);
            }
            else
            {
                lvMenuItemsTab.Items.Add(newItem);
            }
            RePageTabPage();

            if (Data.MySqlDb.Game.SaveChanges() <= 0)
            {
                MessageBox.Show("Failed to save menu add changes to DB");
                return;
            }
            lvMenuItemsTab.Sort();
            FillShopTabsPage(newIcsMenu);
        }

        private void cbMenuTab_Changed(object sender, EventArgs e)
        {
            FillShopTabsPage();
        }

        private void cbMainMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update sub menu names
            // Sub menu for main menu X (4911-> )
            // cbSubMenu.Items.Clear();
            for (var s = 1; s <= 7; s++)
            {
                var t = Data.Server.GetText("ui_texts", "text", 4910 + s + (cbMainMenu.SelectedIndex * 7), string.Empty);
                if (string.IsNullOrWhiteSpace(t))
                    t = "sub_menu_" + (cbMainMenu.SelectedIndex + 1).ToString() + "_" + s.ToString();
                cbSubMenu.Items[s - 1] = t;
            }
            FillShopTabsPage();
        }

        private void cbSubMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillShopTabsPage();
        }

        private void lvMenuItemsTab_MouseDown(object sender, MouseEventArgs e)
        {
            var underCursor = lvMenuItemsTab.GetItemAt(e.X, e.Y);
            if (underCursor == null)
                return;
            underCursor.Selected = true;
            if (underCursor.Tag is IcsMenu menuItem)
                lvMenuShopItemList.DoDragDrop(menuItem, DragDropEffects.Move);
        }

        private void lvMenuItemsTab_DragOver(object sender, DragEventArgs e)
        {
            // https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.listviewinsertionmark.nearestindex?view=windowsdesktop-8.0&devlangs=csharp&f1url=%3FappId%3DDev16IDEF1%26l%3DEN-US%26k%3Dk(System.Windows.Forms.ListViewInsertionMark.NearestIndex)%3Bk(DevLang-csharp)%26rd%3Dtrue

            // Retrieve the client coordinates of the mouse pointer.
            Point targetPoint =
                lvMenuItemsTab.PointToClient(new Point(e.X, e.Y));

            // Retrieve the index of the item closest to the mouse pointer.
            int targetIndex = lvMenuItemsTab.InsertionMark.NearestIndex(targetPoint);

            // Confirm that the mouse pointer is not over the dragged item.
            if (targetIndex > -1)
            {
                // Determine whether the mouse pointer is to the left or
                // the right of the midpoint of the closest item and set
                // the InsertionMark.AppearsAfterItem property accordingly.
                Rectangle itemBounds = lvMenuItemsTab.GetItemRect(targetIndex);
                if (targetPoint.X > itemBounds.Left + (itemBounds.Width / 2))
                {
                    lvMenuItemsTab.InsertionMark.AppearsAfterItem = true;
                }
                else
                {
                    lvMenuItemsTab.InsertionMark.AppearsAfterItem = false;
                }
            }

            // Set the location of the insertion mark. If the mouse is
            // over the dragged item, the targetIndex value is -1 and
            // the insertion mark disappears.
            lvMenuItemsTab.InsertionMark.Index = targetIndex;
        }

        private void lvMenuItemsTab_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void pTrash_DragEnter(object sender, DragEventArgs e)
        {
            var menuItem = e.Data.GetData(typeof(IcsMenu)) as IcsMenu;
            if (menuItem != null)
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void pTrash_DragDrop(object sender, DragEventArgs e)
        {
            var menuItem = e.Data.GetData(typeof(IcsMenu)) as IcsMenu;
            if (menuItem == null)
                return;

            Data.MySqlDb.Game.IcsMenu.Remove(menuItem);
            Data.MySqlDb.Game.SaveChanges();
            FillShopTabsPage();
        }

        private void pTrash_DragLeave(object sender, EventArgs e)
        {
            // Do Nothing
        }

        private void btnAutoCreateAllItemsTab_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will remove all entries of this tab and poplate them with the contents of all other items in this sub-menu!" + Environment.NewLine +
                "Are you sure you want to continue?", "Auto-create items?", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            var mainMenu = (byte)(cbMainMenu.SelectedIndex + 1);
            var subMenu = (byte)(cbSubMenu.SelectedIndex + 1);

            // Delete all items on this sub menu
            Data.MySqlDb.Game.IcsMenu.Where(x => (x.MainTab == mainMenu) && (x.SubTab == subMenu)).ExecuteDelete();
            Data.MySqlDb.Game.SaveChanges();
            var allItemsOfThisMenu = Data.MySqlDb.Game.IcsMenu.Where(x => (x.MainTab == mainMenu)).OrderBy(x => x.SubTab).ThenBy(x => x.TabPos).ToList();
            var newPos = 0;
            foreach(var item in allItemsOfThisMenu)
            {
                var newItem = new IcsMenu();
                newItem.MainTab = mainMenu;
                newItem.SubTab = subMenu;
                newItem.TabPos = newPos;
                newItem.ShopId = item.ShopId;

                Data.MySqlDb.Game.IcsMenu.Add(newItem);
                newPos++;
            }
            Data.MySqlDb.Game.SaveChanges();
            FillShopTabsPage();
        }
    }
}

