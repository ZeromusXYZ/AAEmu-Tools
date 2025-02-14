using AAEmu.DBEditor.data;
using AAEmu.DBEditor.data.aaemu.game;
using AAEmu.DBEditor.data.aaemu.login;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using AAEmu.DBEditor.utils;
using AAEmu.DBEditor.data.enums;

#pragma warning disable WFO1000

namespace AAEmu.DBEditor.forms.server
{
    public partial class AccountsForm : Form
    {
        public Users SelectedAccount { get; set; }
        public Accounts SelectedGameAccount { get; set; }

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

            // Populate Ban Reasons ComboBox
            cbBanReason.Items.Clear();
            var reasons = Data.Server.CompactSqlite.UiTexts.Where(x => x.CategoryId == 2).OrderBy(x => x.Id);
            foreach (var reason in reasons)
            {
                cbBanReason.Items.Add($"{cbBanReason.Items.Count:D2}:{reason.Key} => {Data.Server.GetText("ui_texts", "text", (reason.Id ?? 0), $"?")}");
            }
            if (cbBanReason.Items.Count > 0)
                cbBanReason.SelectedIndex = 0;

            // Update list
            UpdateList("");
        }

        private void UpdateList(string selectUser)
        {
            SelectedAccount = null;
            SelectedGameAccount = null;
            btnSave.Enabled = false;

            var users = Data.MySqlDb.Login.Users.ToList();
            if (!string.IsNullOrWhiteSpace(tUserFilter.Text))
            {
                var selectList = new List<long>();
                var fLower = tUserFilter.Text.ToLower();
                var userNameList = Data.MySqlDb.Login.Users.Where(x => x.Username.ToLower().Contains(fLower)).ToList();
                foreach (var user in userNameList)
                {
                    if (selectList.Contains(user.Id))
                        continue;
                    selectList.Add(user.Id);
                }
                var characterNameList = Data.MySqlDb.Game.Characters.Where(x => x.Name.ToLower().Contains(fLower)).ToList();
                foreach (var character in characterNameList)
                {
                    if (selectList.Contains(character.AccountId))
                        continue;
                    selectList.Add(character.AccountId);
                }

                users = Data.MySqlDb.Login.Users.ToList().Where(x => selectList.Contains(x.Id)).ToList();
            }
            dgvUsers.DataSource = users;


            if (string.IsNullOrWhiteSpace(selectUser))
                return;

            dgvUsers.ClearSelection();

            for (var i = 0; i < users.Count; i++)
            {
                var user = users[i];
                if (user.Username == selectUser)
                {
                    dgvUsers.Rows[i].Selected = true;
                }
            }

            if (dgvUsers.Rows.Count > 0)
            {
                var scrollPos = dgvUsers.RowCount - dgvUsers.DisplayedRowCount(true) + 1;
                if (scrollPos < dgvUsers.Rows.Count)
                    dgvUsers.FirstDisplayedScrollingRowIndex = scrollPos;
            }
            dgvUsers_SelectionChanged(dgvUsers, null);
        }

        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            SelectedAccount = null;
            SelectedGameAccount = null;
            btnSave.Enabled = false;
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

            var account = Data.MySqlDb.Login.Users.FirstOrDefault(x => x.Id == accountId);
            if (account == null)
            {
                lvCharacters.Items.Add("Invalid Account Selected").ImageIndex = 0;
                return;
            }

            SelectedAccount = account;
            Data.MySqlDb.Login.Users.Entry(SelectedAccount).Reload();
            cbBanned.Checked = account.Banned > 0;
            cbBanReason.SelectedIndex = account.BanReason < cbBanReason.Items.Count ? (int)account.BanReason : 0;

            var gameAccount = Data.MySqlDb.Game.Accounts.FirstOrDefault(x => x.AccountId == accountId);
            SelectedGameAccount = gameAccount;
            if (gameAccount != null)
            {
                Data.MySqlDb.Game.Accounts.Entry(SelectedGameAccount).Reload();
                tLabor.Text = gameAccount.Labor.ToString(CultureInfo.InvariantCulture);
                tCredits.Text = gameAccount.Credits.ToString(CultureInfo.InvariantCulture);
                tLoyalty.Text = gameAccount.Loyalty.ToString(CultureInfo.InvariantCulture);
                cbAccessLevel.Text = gameAccount.AccessLevel.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                tLabor.Text = "";
                tCredits.Text = "";
                tLoyalty.Text = "";
                cbAccessLevel.Text = "";
            }
            tLabor.Enabled = (gameAccount != null);
            tCredits.Enabled = (gameAccount != null);
            tLoyalty.Enabled = (gameAccount != null);
            cbAccessLevel.Enabled = (gameAccount != null);

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
                if (character.Deleted > 0)
                {
                    icon.ImageIndex = 0;
                    icon.ForeColor = Color.Red;
                }
                else
                if (character.DeleteTime > DateTime.MinValue)
                {
                    icon.ForeColor = Color.Purple;
                }
            }
            btnSave.Enabled = false;
        }

        private void lvCharacters_SelectedIndexChanged(object sender, EventArgs e)
        {
            Characters c = null;
            var iconId = 0;
            foreach (ListViewItem s in lvCharacters.SelectedItems)
                if (s.Tag is Characters tag)
                {
                    c = tag;
                    iconId = s.ImageIndex;
                    break;
                }

            if (c == null)
            {
                lCharacterName.Text = @"<none>";
                lLevel.Text = "";
                lClass.Text = "";
                lMoney.Text = "";
                pbCharacter.Image = ilRaces.Images[0];
                return;
            }

            pbCharacter.Image = ilRaces.Images[iconId];
            lCharacterName.Text = c.Name + (c.Deleted > 0 ? " (deleted)" : "");
            // ReSharper disable once LocalizableElement
            lLevel.Text = $"{Data.Server.GetText("ui_texts", "text", 1097, "<level>")} {c.Level}  {c.GetRaceName()} {c.GetGenderName()}";
            // ReSharper disable once LocalizableElement
            lMoney.Text = $"{AaTextHelper.CopperToString(c.Money)} on player, {AaTextHelper.CopperToString(c.Money2)} in warehouse";
            // ReSharper disable once LocalizableElement
            lClass.Text = $"{AbilityNames.GetClassName(c.Ability1, c.Ability2, c.Ability3)} ({c.GetClassName()})";
        }

        private void MenuAccountNew_Click(object sender, EventArgs e)
        {
            // New new user name
            var newName = "User";
            for (int i = 1; ; i++)
            {
                var s = newName + i.ToString();
                if (Data.MySqlDb.Login.Users.FirstOrDefault(x => x.Username.ToLower() == s.ToLower()) == null)
                {
                    newName = s;
                    break;
                }
            }
            var newAccount = new Users()
            {
                // Id = 0,
                Username = newName,
                Password = "",
                Email = "",
                LastLogin = 0,
                LastIp = "0.0.0.0",
                CreatedAt = (ulong)DateTime.UtcNow.ToFileTime(),
                UpdatedAt = (ulong)DateTime.UtcNow.ToFileTime(),
            };
            try
            {
                Data.MySqlDb.Login.Users.Add(newAccount);
                Data.MySqlDb.Login.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            UpdateList(newName);
        }

        private void MenuAccount_DropDownOpening(object sender, EventArgs e)
        {
            AccountPassword.Enabled = SelectedAccount != null;
        }

        private void AccountPassword_Click(object sender, EventArgs e)
        {
            if (SelectedAccount == null)
                return;

            using (var passwordForm = new AccountChangePasswordForm())
            {
                passwordForm.Text += $" - {SelectedAccount.Username}";
                if (passwordForm.ShowDialog() != DialogResult.OK)
                    return;

                byte[] passBytes = Encoding.UTF8.GetBytes(passwordForm.tNewPassword.Text);
                using (var sha = SHA256.Create())
                {
                    var hash = sha.ComputeHash(passBytes);
                    var passHash = Convert.ToBase64String(hash);
                    Data.MySqlDb.Login.Users.Entry(SelectedAccount).Reload();
                    SelectedAccount.Password = passHash;
                    SelectedAccount.UpdatedAt = (ulong)DateTime.UtcNow.ToFileTime();
                    try
                    {
                        Data.MySqlDb.Login.Users.Update(SelectedAccount);
                        Data.MySqlDb.Login.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            UpdateList(SelectedAccount.Username);
        }

        private void AccountDelete_Click(object sender, EventArgs e)
        {
            if (SelectedAccount == null)
                return;

            if (Data.MySqlDb.Game.Characters.Any(x => x.AccountId == SelectedAccount.Id))
            {
                MessageBox.Show($"It's not allowed to delete accounts that still have characters attached!");
                return;
            }

            if (MessageBox.Show($"Are you sure you want to delete user {SelectedAccount.Username} ({SelectedAccount.Id})", "Delete user", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            try
            {
                Data.MySqlDb.Login.Users.Remove(SelectedAccount);
                Data.MySqlDb.Login.SaveChanges();
                SelectedAccount = null;
                SelectedGameAccount = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            UpdateList("");
        }

        private void AccountUsername_Click(object sender, EventArgs e)
        {
            if (SelectedAccount == null)
                return;

            using (var userNameForm = new AccountChangeUsernameForm())
            {
                userNameForm.Text += $" - {SelectedAccount.Username}";
                if (userNameForm.ShowDialog() != DialogResult.OK)
                    return;

                var newName = userNameForm.tNewUsername.Text.Trim();

                var duplicates = Data.MySqlDb.Login.Users.FirstOrDefault(x => x.Username.ToLower() == newName.ToLower());
                if (duplicates != null)
                {
                    MessageBox.Show($"Username {newName} is already taken by user {duplicates.Id}");
                    return;
                }

                Data.MySqlDb.Login.Users.Entry(SelectedAccount).Reload();
                SelectedAccount.Username = newName;
                SelectedAccount.UpdatedAt = (ulong)DateTime.UtcNow.ToFileTime();
                try
                {
                    Data.MySqlDb.Login.Users.Update(SelectedAccount);
                    Data.MySqlDb.Login.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            UpdateList(SelectedAccount.Username);
        }

        private void tUserFilter_TextChanged(object sender, EventArgs e)
        {
            UpdateList(SelectedAccount?.Username ?? "");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Game specific settings
            if (SelectedGameAccount != null)
            {
                var valid = true;
                if (!int.TryParse(tLabor.Text, CultureInfo.InvariantCulture, out int newLabor))
                    valid = false;
                if (!int.TryParse(tCredits.Text, CultureInfo.InvariantCulture, out int newCredits))
                    valid = false;
                if (!int.TryParse(tLoyalty.Text, CultureInfo.InvariantCulture, out int newLoyalty))
                    valid = false;
                if (!int.TryParse(cbAccessLevel.Text, CultureInfo.InvariantCulture, out int newAccountAccessLevel))
                    valid = false;

                if (valid)
                {
                    try
                    {
                        // Game Account
                        Data.MySqlDb.Game.Accounts.Entry(SelectedGameAccount).Reload();
                        SelectedGameAccount.Labor = newLabor;
                        SelectedGameAccount.Credits = newCredits;
                        SelectedGameAccount.Loyalty = newLoyalty;
                        SelectedGameAccount.AccessLevel = newAccountAccessLevel;
                        Data.MySqlDb.Game.Accounts.Update(SelectedGameAccount);
                        Data.MySqlDb.Game.SaveChanges();

                        // Login Account
                        Data.MySqlDb.Login.Users.Entry(SelectedAccount).Reload();
                        SelectedAccount.Banned = cbBanned.Checked ? 1u : 0;
                        SelectedAccount.BanReason = cbBanReason.SelectedIndex >= 0 ? (uint)cbBanReason.SelectedIndex : 0;
                        Data.MySqlDb.Login.Users.Update(SelectedAccount);
                        Data.MySqlDb.Login.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }
            }
        }

        private void AccountValueChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }
    }
}
