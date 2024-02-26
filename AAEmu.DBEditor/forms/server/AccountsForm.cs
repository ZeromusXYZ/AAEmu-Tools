using AAEmu.DBEditor;
using AAEmu.DBEditor.data;
using AAEmu.DBEditor.data.aaemu;
using Microsoft.Extensions.Logging.Abstractions;
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
    public partial class AccountsForm : Form
    {
        public AccountsForm()
        {
            InitializeComponent();
        }

        private void AccountsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }

        private void AccountsForm_Load(object sender, EventArgs e)
        {
            MainForm.Self.AddOwnedForm(this);
            dgvUsers.DataSource = Data.MySqlDb.Login.Users.ToList();
        }

        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            lvCharacters.Clear();
            if (dgvUsers.SelectedCells.Count < 1)
                return;
            var selectedCells = dgvUsers.SelectedCells;
            var accountId = 0;

            foreach (DataGridViewCell cell in selectedCells)
            {
                var idString = dgvUsers.Rows[cell.RowIndex].Cells[0].Value.ToString();
                if (!int.TryParse(idString, out accountId))
                    accountId = 0;
                // lvCharacters.Items.Add(idString);
            }

            if (accountId == 0)
            {
                lvCharacters.Items.Add("No Account Selected").ImageIndex = 0;
                return;
            }

            var characters = Data.MySqlDb.Game.Characters.Where(x => x.AccountId == accountId).ToList();

            if (characters.Count <= 0)
            {
                lvCharacters.Items.Add("No Characters").ImageIndex = 0;
                return;
            }

            foreach (var character in characters)
            {
                var icon = lvCharacters.Items.Add(character.Name);
                icon.ImageIndex = character.Race + (character.Gender == 2 ? 9 : 0); // I'm too lazy to optimize the icons in the list
                icon.Tag = character;
            }

        }

        private void lvCharacters_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.aaemu.game.Characters c = null;
            var iconId = 0;
            foreach (ListViewItem s in lvCharacters.SelectedItems)
                if (s.Tag is data.aaemu.game.Characters)
                {
                    c = s.Tag as data.aaemu.game.Characters;
                    iconId = s.ImageIndex;
                    break;
                }

            if (c == null)
            {
                lCharacterName.Text = "<none>";
                lLevel.Text = "<none>";
                lClass.Text = "<none>";
                lMoney.Text = "0c";
                pbCharacter.Image = ilRaces.Images[0];
                return;
            }

            pbCharacter.Image = ilRaces.Images[iconId];
            lCharacterName.Text = c.Name;
            lLevel.Text = $"{Data.Server.GetText("ui_texts", "text", 1097, "<level>")} {c.Level}  {c.GetRaceName()} {c.GetGenderName()}" ;
            lMoney.Text = $"{c.GetMoney(c.Money)} on player, {c.GetMoney(c.Money2)} in warehouse";
            lClass.Text = c.GetClassName();
        }
    }
}
