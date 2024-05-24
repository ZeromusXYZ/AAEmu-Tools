using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AAEmu.DBEditor.data;
using AAEmu.DBEditor.data.aaemu.game;
using AAEmu.DBEditor.data.enums;
using AAEmu.DBEditor.utils;

namespace AAEmu.DBEditor.forms.server
{
    public partial class CharacterForm : Form
    {

        private Characters SelectedCharacter { get; set; }

        public CharacterForm()
        {
            InitializeComponent();
        }

        private void CharacterForm_Load(object sender, EventArgs e)
        {
            lvItems.Items.Clear();
            lvItems.SmallImageList = Data.Client.Icons16;
            lvItems.LargeImageList = Data.Client.Icons32;
            tcCharacter.SelectedTab = tpItems;
            tFilter_TextChanged(null, null);
        }

        private void CharacterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }

        private void tFilter_TextChanged(object sender, EventArgs e)
        {
            var filterLowerCase = tFilter.Text.ToLower();
            var characters = Data.MySqlDb.Game.Characters.Where(x => (x.Name.ToLower() + x.Id.ToString()).Contains(filterLowerCase));
            lvCharacterList.Clear();
            foreach (var character in characters)
            {
                if (lvCharacterList.Items.Count > 25)
                {
                    var endOfList = lvCharacterList.Items.Add($"Too many results");
                    endOfList.ImageIndex = 0;
                    endOfList.ForeColor = Color.DarkRed;
                    break;
                }
                var characterEntry = lvCharacterList.Items.Add(character.Name);
                characterEntry.ImageIndex = character.Race + (character.Gender == 2 ? 9 : 0);
                characterEntry.Tag = character;
                if (character.Deleted > 0)
                {
                    characterEntry.ImageIndex = 0;
                    characterEntry.ForeColor = Color.Red;
                }
                else
                if (character.DeleteTime > DateTime.MinValue)
                {
                    characterEntry.ForeColor = Color.Purple;
                }
            }
        }

        private void SelectContainer(uint characterId, string slotType)
        {
            var itemContainer = Data.MySqlDb.Game.ItemContainers.FirstOrDefault(x => x.OwnerId == characterId && x.SlotType == slotType);
            lvItems.Items.Clear();
            if (itemContainer == null)
            {
                lContainer.Text = $"Container not found {slotType}";
                return;
            }
            lContainer.Text = $"{itemContainer.SlotType}:{itemContainer.ContainerType} ({itemContainer.ContainerId}), Size: {itemContainer.ContainerSize}";

            var containerItems = Data.MySqlDb.Game.Items.Where(x => x.ContainerId == itemContainer.ContainerId).OrderBy(x => x.Slot);
            var lastSlot = -2;
            foreach (var item in containerItems)
            {
                var itemEntry = Data.Server.GetItem((long)item.TemplateId);
                var newItem = new ListViewItem();
                newItem.Tag = itemEntry;
                // TemplateId
                newItem.Text = itemEntry.Id.ToString();
                newItem.ImageIndex = Data.Client.GetIconIndexByItemTemplateId((long)itemEntry.Id);
                // Count
                newItem.SubItems.Add(item.Count == 1 ? string.Empty : item.Count.ToString());
                // Name
                newItem.SubItems.Add(Data.Server.GetText("items", "name", (long)itemEntry.Id, itemEntry.Name));
                // Category
                newItem.SubItems.Add(Data.Server.GetText("item_categories", "name", (long)itemEntry.CategoryId, itemEntry.CategoryId.ToString()));
                // DbId
                newItem.SubItems.Add(item.Id.ToString());
                // Slot
                if (itemContainer.SlotType == "Equipment")
                {
                    var equipSlot = (EquipmentSlot)item.Slot;
                    newItem.SubItems.Add($"{equipSlot}:{item.Slot}");
                }
                else
                {
                    newItem.SubItems.Add(item.Slot.ToString());
                }
                
                if ((lastSlot == item.Slot) && (itemContainer.SlotType != "Mail"))
                    newItem.ForeColor = Color.Red;
                lastSlot = item.Slot;

                lvItems.Items.Add(newItem);
            }
            lvItems.View = View.Details;
        }

        private void rbContainers_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender is not RadioButton radioButton) || (radioButton.Tag is not string containerType))
                return;
            if (SelectedCharacter == null)
                return;

            SelectContainer(SelectedCharacter.Id, containerType);
        }

        private void lvCharacterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedCharacter = null;
            foreach (ListViewItem selectedItem in lvCharacterList.SelectedItems)
            {
                if (selectedItem.Tag is not Characters character)
                    continue;
                SelectedCharacter = character;
                rbEquipement.Checked = true;
                gbContainerSelect.Text = $"Containers of {character.Name} ({character.Id})";
                lInventoryGold.Text = AaTextHelper.CopperToString(character.Money);
                lBankGold.Text = AaTextHelper.CopperToString(character.Money2);
                rbContainers_CheckedChanged(rbEquipement, null);
                break;
            }
        }
    }
}
