using AAEmu.DBEditor;
using AAEmu.DBEditor.data;
using AAEmu.DBEditor.data.aaemu.game;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AAEmu.DBEditor.forms.server
{
    public partial class ICSForm : Form
    {
        private IcsSkus SelectedSKU { get; set; }
        private IcsShopItems SelectedShopItem { get; set; }

        public ICSForm()
        {
            InitializeComponent();
        }

        private void ICSForm_Load(object sender, EventArgs e)
        {
            MainForm.Self.AddOwnedForm(this);
            SelectedSKU = null;
            SelectedShopItem = null;
            FillSKUList();
            FillShopItemList();
            FillShopTabs();
        }

        private void FillSKUList()
        {
            var skus = Data.MySqlDb.Game.IcsSkus.ToList();
            lvSKUs.Items.Clear();
            lvSKUs.SmallImageList = Data.Client.Icons;
            SelectedSKU = null;
            foreach (var skuItem in skus)
            {
                if (IsSkuFilteredOut(skuItem))
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

        private bool IsSkuFilteredOut(IcsSkus skuItem)
        {
            var filterWords = tFilterSku.Text.ToLower().Split(" ").ToList();
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
            tvShopItems.ImageList = Data.Client.Icons;
            cbSKUShopEntryId.Items.Clear();
            cbSKUShopEntryId.Items.Add("0");
            foreach (var shopItem in shopItems)
            {
                var subSKUs = Data.MySqlDb.Game.IcsSkus.Where(x => x.ShopId == shopItem.ShopId).OrderBy(x => x.Position);
                if (IsShopItemFilteredOut(shopItem, subSKUs))
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

        private bool IsShopItemFilteredOut(IcsShopItems shopItem, IOrderedQueryable<IcsSkus> subSKUs)
        {
            var filterWords = tFilterShopItem.Text.ToLower().Split(" ").ToList();
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

        private void FillShopTabs()
        {
            // Do nothing
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
            var lastSku = Data.MySqlDb.Game.IcsSkus.OrderBy(x => x.Sku).Reverse().First();
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
                SelectedSKU.Sku = uint.Parse(tSKUSKU.Text);
                SelectedSKU.ShopId = uint.Parse(cbSKUShopEntryId.Text);
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
                    MessageBox.Show("Failed to save changes to DB");
                    return;
                }
                FillSKUList();
                FillShopItemList();
                FillShopTabs();
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
            var lastShopItem = Data.MySqlDb.Game.IcsShopItems.OrderBy(x => x.ShopId).Reverse().First();
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
                    MessageBox.Show("Failed to save changes to DB");
                    return;
                }
                FillSKUList();
                FillShopItemList();
                FillShopTabs();
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

        private void lvMenuItemsTab_ItemDrag(object sender, ItemDragEventArgs e)
        {
            
        }
    }
}
