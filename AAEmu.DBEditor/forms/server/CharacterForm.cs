using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AAEmu.DBEditor.data;
using AAEmu.DBEditor.data.aaemu.game;
using AAEmu.DBEditor.data.enums;
using AAEmu.DBEditor.data.gamedb;
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

        public Characters GetSelectCharacter()
        {
            return SelectedCharacter;
        }

        private void CharacterForm_Load(object sender, EventArgs e)
        {
            MainForm.Self.AddOwnedForm(this);

            lvItems.Items.Clear();
            lvItems.SmallImageList = Data.Client.Icons16;
            lvItems.LargeImageList = Data.Client.Icons32;
            tcCharacter.SelectedTab = tpServerStats;
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

        private void SelectContainer(uint characterId, int slotType)
        {
            if ((slotType < 0) && Enum.TryParse<SlotType>(cbItemContainerTypeSelect.Text, out var n) && (n >= SlotType.None))
            {
                slotType = (int)n;
            }
            var itemContainer = Data.MySqlDb.Game.ItemContainers.FirstOrDefault(x => x.OwnerId == characterId && x.SlotType == slotType);
            lvItems.Items.Clear();
            if (itemContainer == null)
            {
                lContainer.Text = $"Container not found {slotType}";
                return;
            }
            lContainer.Text = $@"{itemContainer.SlotType}:{itemContainer.ContainerType} ({itemContainer.ContainerId}), Size: {itemContainer.ContainerSize}";

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
                if (itemContainer.SlotType == (int)SlotType.Equipment)
                {
                    var equipSlot = (EquipmentSlot)item.Slot;
                    newItem.SubItems.Add($"{equipSlot}:{item.Slot}");
                }
                else
                {
                    newItem.SubItems.Add(item.Slot.ToString());
                }

                newItem.SubItems.Add($"{item.SlotType}");

                if ((lastSlot == item.Slot) && (itemContainer.SlotType != (int)SlotType.Mail))
                    newItem.ForeColor = Color.Red;
                lastSlot = item.Slot;

                lvItems.Items.Add(newItem);
            }
            lvItems.View = View.Details;

            lContainer.Text += $@", Count:{lvItems.Items.Count}";
        }

        private void rbContainers_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender is not RadioButton { Tag: string containerTypeString }))
                return;
            if (SelectedCharacter == null)
                return;
            if (!int.TryParse(containerTypeString, out var containerType))
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
                PopulateItemsTab(SelectedCharacter);
                PopulateStatsTab(SelectedCharacter);
                PopulateOwnedTab(SelectedCharacter);
                break;
            }
        }

        private void PopulateItemsTab(Characters character)
        {
            rbEquipement.Checked = true;
            gbContainerSelect.Text = $"Containers of {character.Name} ({character.Id})";
            rbContainers_CheckedChanged(rbEquipement, null);
        }

        private byte GetLevel(long exp)
        {
            var level = Data.Server.CompactSqlite.Levels.Where(x => x.TotalExp <= exp).OrderByDescending(x => x.Id).FirstOrDefault();
            return (byte)(level?.Id ?? 0);
        }

        private void PopulateStatsTab(Characters character)
        {
            tvStats.Nodes.Clear();
            var rootNode = tvStats.Nodes.Add($"{character.Name} ({character.Id})");
            rootNode.Nodes.Add($"{Data.Server.GetText("system_factions", "name", character.FactionId, "<" + character.FactionId + ">")} ({character.FactionId})");

            // Class
            var classNode = rootNode.Nodes.Add($"Lv {character.Level} - {AbilityNames.GetClassName(character.Ability1, character.Ability2, character.Ability3)} ({character.GetClassName()})");
            var abilities = Data.MySqlDb.Game.Abilities.Where(x => x.Owner == character.Id);
            foreach (var ability in abilities)
            {
                var abilityName = Data.Server.GetText("ui_texts", "text", 1110 + ability.Id, "<" + ability.Id + ">");
                var abilityNode = classNode.Nodes.Add($"Lv {GetLevel(ability.Exp)} {abilityName} ({ability.Id}), Exp {ability.Exp}");
                if ((ability.Id == character.Ability1) || (ability.Id == character.Ability2) || (ability.Id == character.Ability3))
                    abilityNode.NodeFont = new Font(tvStats.Font, FontStyle.Bold);
            }

            // Inventory
            var inventoryNode = rootNode.Nodes.Add("Inventory");
            inventoryNode.Nodes.Add($"Size: {character.NumInvSlot}");
            inventoryNode.Nodes.Add($"Money: {AaTextHelper.CopperToString(character.Money)}");

            // Warehouse
            var warehouseNode = rootNode.Nodes.Add("Warehouse"); // Bank
            warehouseNode.Nodes.Add($"Size: {character.NumBankSlot}");
            warehouseNode.Nodes.Add($"Money: {AaTextHelper.CopperToString(character.Money2)}");

            // Vocation Skills
            var vocationNode = rootNode.Nodes.Add($"Vocation ({character.VocationPoint} badges)");
            var actAbilities = Data.MySqlDb.Game.Actabilities.Where(x => x.Owner == character.Id).OrderBy(x => x.Id);
            foreach (var actAbility in actAbilities)
            {
                if (actAbility.Point <= 0)
                    continue;

                var abilityName = Data.Server.GetText("actability_groups", "name", actAbility.Id, "<" + actAbility.Id + ">");
                var abilityNode = vocationNode.Nodes.Add($"{abilityName}, {actAbility.Point} (Rank {actAbility.Step})");
            }

            // Honor
            rootNode.Nodes.Add($"Honor {character.HonorPoint}, PvP {character.PvpHonor}");

            // Default expand layout
            tvStats.CollapseAll();
            rootNode.Expand();
            inventoryNode.ExpandAll();
            warehouseNode.ExpandAll();
        }

        private void PopulateOwnedTab(Characters character)
        {
            tvOwned.Nodes.Clear();

            // Housing
            #region houses
            var housingNode = tvOwned.Nodes.Add($"Houses and Farms");
            List<Housings> houses;
            if (cbIncludeAccountHouses.Checked)
                houses = Data.MySqlDb.Game.Housings.Where(x => x.AccountId == character.AccountId).ToList();
            else
                houses = Data.MySqlDb.Game.Housings.Where(x => x.Owner == character.Id).ToList();
            foreach (var house in houses)
            {
                var houseNode = housingNode.Nodes.Add($"{house.Name} ({house.Id})");
                if (cbIncludeAccountHouses.Checked && house.Owner == character.Id)
                {
                    houseNode.NodeFont = new Font(tvOwned.Font, FontStyle.Bold);
                }
                else
                {
                    var ownerCharacter = Data.MySqlDb.Game.Characters.FirstOrDefault(x => x.Id == house.Owner);
                    houseNode.Nodes.Add($"Owner: {ownerCharacter?.Name ?? "<none>"} ({ownerCharacter?.Id ?? 0})");
                }

                if (house.CoOwner != house.Owner)
                {
                    var coOwnerCharacter = Data.MySqlDb.Game.Characters.FirstOrDefault(x => x.Id == house.CoOwner);
                    houseNode.Nodes.Add($"Co-Owner: {coOwnerCharacter?.Name ?? "<none>"} ({coOwnerCharacter?.Id ?? 0})");
                }

                houseNode.Nodes.Add($"Pos: {house.X:F0}, {house.Y:F0}, {house.Z:F0}");
            }
            #endregion

            // Pets
            #region pets
            var petsNode = tvOwned.Nodes.Add("Pets");
            var pets = Data.MySqlDb.Game.Mates.Where(x => x.Owner == character.Id).ToList();
            foreach (var pet in pets)
            {
                var petNode = petsNode.Nodes.Add($"{pet.Name} ({pet.Id}) Lv {pet.Level} ");

                var petItem = Data.MySqlDb.Game.Items.FirstOrDefault(x => x.Id == pet.ItemId);
                if (petItem == null)
                {
                    petNode.Nodes.Add($"Item {pet.ItemId} - Orphaned").ForeColor = Color.Red;
                    petNode.ForeColor = Color.Red;
                }
                else
                {
                    var item = Data.Server.GetItem(petItem.TemplateId);
                    petNode.Nodes.Add($"({petItem.Id}) {Data.Server.GetText("items", "name", (long)item.Id, item.Name)} ({item.Id})");
                }

                petNode.Nodes.Add($"Exp {pet.Xp}");

                var gearNode = petNode.Nodes.Add($"Equipment");
                var petGearContainer = Data.MySqlDb.Game.ItemContainers.FirstOrDefault(x => x.MateId == pet.Id);
                if (petGearContainer != null)
                {
                    var petGears = Data.MySqlDb.Game.Items.Where(x => x.ContainerId == petGearContainer.ContainerId);
                    if (petGears.Any())
                    {
                        foreach (var petGear in petGears)
                        {
                            var item = Data.Server.GetItem(petGear.TemplateId);
                            gearNode.Nodes.Add($"Item {Data.Server.GetText("items", "name", (long)item.Id, item.Name)} ({petGear.Id})");
                        }
                    }
                    else
                    {
                        gearNode.Text = "No equipment";
                    }
                }
                else
                {
                    gearNode.Text = "No equipment container";
                }

                gearNode.Collapse();
                petNode.Collapse();
            }
            #endregion

            // Vehicles
            #region vehicles
            var vehiclesNode = tvOwned.Nodes.Add($"Vehicles");
            var vehicles = Data.MySqlDb.Game.Slaves.Where(x => x.OwnerId == character.Id && x.OwnerType == 0 && x.Summoner == character.Id).ToList();
            foreach (var vehicle in vehicles)
            {
                var vehicleNode = vehiclesNode.Nodes.Add($"{vehicle.Name} ({vehicle.Id})");
                var vehicleItem = Data.MySqlDb.Game.Items.FirstOrDefault(x => x.Id == vehicle.ItemId);
                if (vehicleItem == null)
                {
                    vehicleNode.Nodes.Add($"Item {vehicle.ItemId} - Orphaned").ForeColor = Color.Red;
                    vehicleNode.ForeColor = Color.Red;
                }
                else
                {
                    var item = Data.Server.GetItem(vehicleItem.TemplateId);
                    vehicleNode.Nodes.Add($"({vehicleItem.Id}) Item {Data.Server.GetText("items", "name", (long)item.Id, item.Name)} ({item.Id})");
                    if (vehicleItem.Ucc > 0)
                        vehicleNode.Nodes.Add($"UCC: {vehicleItem.Ucc}");
                }
                vehicleNode.Nodes.Add($"Pos: {vehicle.X:F0}, {vehicle.Y:F0}, {vehicle.Z:F0}");
                vehicleNode.Nodes.Add($"{vehicle.Hp} HP, {vehicle.Mp} MP");
                var childrenNode = vehicleNode.Nodes.Add($"Persistent Children");
                var doodads = Data.MySqlDb.Game.Doodads.Where(x => (x.OwnerType == 2) && (x.HouseId == vehicle.Id));
                foreach (var doodad in doodads)
                {
                    var doodadNode = childrenNode.Nodes.Add($"Doodad {doodad.Id} {Data.Server.GetText("doodad_almighties", "name", doodad.TemplateId, $"<{doodad.TemplateId}>", true)} ({doodad.TemplateId})");
                    if (doodad.ParentDoodad > 0)
                        doodadNode.Nodes.Add($"Parent: {doodad.ParentDoodad}");
                    doodadNode.Nodes.Add($"AttachPoint: {doodad.AttachPoint}");
                    doodadNode.Nodes.Add($"Pos: {doodad.X:F1}, {doodad.Y:F1}, {doodad.Z:F1}");
                    if (doodad.ItemId > 0)
                        doodadNode.Nodes.Add($"Item: {doodad.ItemId}");
                    if (doodad.ItemTemplateId > 0)
                        doodadNode.Nodes.Add($"ItemTemplate: ({doodad.ItemTemplateId}) {Data.Server.GetText("items", "name", (long)doodad.ItemTemplateId, "???")}");
                }

                var slaves = Data.MySqlDb.Game.Slaves.Where(x => (x.OwnerType == 2) && (x.OwnerId == vehicle.Id));
                foreach (var slave in slaves)
                {
                    var slaveNode = childrenNode.Nodes.Add($"Slave {slave.Id} {slave.Name} ({slave.TemplateId})");
                    slaveNode.Nodes.Add($"AttachPoint: {slave.AttachPoint}");
                    slaveNode.Nodes.Add($"Pos: {slave.X:F1}, {slave.Y:F1}, {slave.Z:F1}");
                    slaveNode.Nodes.Add($"{slave.Hp} HP, {slave.Mp} MP");
                }
            }
            #endregion

            // Default expand layout
            // tvOwned.ExpandAll();
            housingNode.Expand();
            petsNode.Expand();
            vehiclesNode.Expand();
        }

        private void cbIncludeAccountHouses_CheckedChanged(object sender, EventArgs e)
        {
            if (SelectedCharacter != null)
                PopulateOwnedTab(SelectedCharacter);
        }

        private void tpItems_Enter(object sender, EventArgs e)
        {
            // Populate the custom combo list box
            cbItemContainerTypeSelect.Items.Clear();
            if (SelectedCharacter == null)
                return;

            var itemContainerTypes = Data.MySqlDb.Game.ItemContainers.Where(x => x.OwnerId == SelectedCharacter.Id).ToList().DistinctBy(x => x.SlotType).OrderBy(x => x.SlotType);
            foreach (var itemContainerType in itemContainerTypes)
            {
                var slotTypeName = ((SlotType)itemContainerType.SlotType).ToString();
                cbItemContainerTypeSelect.Items.Add(slotTypeName);
            }

            if (cbItemContainerTypeSelect.Items.Count > 0)
                cbItemContainerTypeSelect.SelectedIndex = 0;
        }

        private void cbItemContainerTypeSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectContainer(SelectedCharacter.Id, -1);
            rbSystem.Checked = true;
        }

        private void btnSelectionOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
