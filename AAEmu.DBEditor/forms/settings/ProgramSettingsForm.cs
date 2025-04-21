using AAEmu.DBEditor.data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AAEmu.DBEditor.forms
{
    public partial class ProgramSettingsForm : Form
    {
        public ProgramSettingsForm()
        {
            InitializeComponent();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void OnSettingsChanged(object sender, EventArgs e)
        {
            BtnSave.Enabled = false;
            if (!ProgramSettings.Instance.ClientPak.Equals(LabelGamePakPath.Text))
                BtnSave.Enabled = true;
            if (!ProgramSettings.Instance.ClientDb.Equals(LabelClientDataDb.Text))
                BtnSave.Enabled = true;
            if (!ProgramSettings.Instance.ServerDb.Equals(LabelServerDataDb.Text))
                BtnSave.Enabled = true;

            if (!ProgramSettings.Instance.MySqlDb.Equals(TextBoxServerIP.Text) &&
                !string.IsNullOrWhiteSpace(TextBoxServerIP.Text))
                BtnSave.Enabled = true;


            if (!ProgramSettings.Instance.MySqlUser.Equals(TextBoxUsername.Text) &&
                !string.IsNullOrWhiteSpace(TextBoxUsername.Text))
                BtnSave.Enabled = true;

            if (!ProgramSettings.Instance.MySqlPassword.Equals(TextBoxPassword.Text))
                BtnSave.Enabled = true;

            if (!ProgramSettings.Instance.MySqlLogin.Equals(TextBoxLoginSchema.Text) &&
                !string.IsNullOrWhiteSpace(TextBoxLoginSchema.Text))
                BtnSave.Enabled = true;

            if (!ProgramSettings.Instance.MySqlGame.Equals(TextBoxGameSchema.Text) &&
                !string.IsNullOrWhiteSpace(TextBoxGameSchema.Text))
                BtnSave.Enabled = true;

            if (!ValidateFiles())
                BtnSave.Enabled = false;

            BtnRevert.Enabled = !BtnSave.Enabled;
        }

        private bool ValidateFiles()
        {
            return File.Exists(LabelGamePakPath.Text)
                   && (string.IsNullOrWhiteSpace(LabelClientDataDb.Text) || File.Exists(LabelClientDataDb.Text))
                   && (string.IsNullOrWhiteSpace(LabelServerDataDb.Text) || File.Exists(LabelServerDataDb.Text))
                ;
        }

        private void RevertSettings()
        {
            LabelGamePakPath.Text = ProgramSettings.Instance.ClientPak;
            LabelClientDataDb.Text = ProgramSettings.Instance.ClientDb;
            LabelServerDataDb.Text = ProgramSettings.Instance.ServerDb;
            TextBoxServerIP.Text = ProgramSettings.Instance.MySqlDb;
            TextBoxUsername.Text = ProgramSettings.Instance.MySqlUser;
            TextBoxPassword.Text = ProgramSettings.Instance.MySqlPassword;
            TextBoxLoginSchema.Text = ProgramSettings.Instance.MySqlLogin;
            TextBoxGameSchema.Text = ProgramSettings.Instance.MySqlGame;
        }

        private void BtnRevert_Click(object sender, EventArgs e)
        {
            RevertSettings();
        }

        private void BtnEditGamePak_Click(object sender, EventArgs e)
        {
            if (ofdClientPak.ShowDialog() != DialogResult.OK)
                return;
            LabelGamePakPath.Text = ofdClientPak.FileName;
            OnSettingsChanged(null, null);
        }

        private void BtnEditClientDb_Click(object sender, EventArgs e)
        {
            if (ofdClientDb.ShowDialog() != DialogResult.OK)
                return;
            LabelClientDataDb.Text = ofdClientDb.FileName;
            OnSettingsChanged(null, null);
        }

        private void BtnEditServerDb_Click(object sender, EventArgs e)
        {
            if (ofdServerDb.ShowDialog() != DialogResult.OK)
                return;
            LabelServerDataDb.Text = ofdServerDb.FileName;
            OnSettingsChanged(null, null);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void ProgramSettingsForm_Load(object sender, EventArgs e)
        {
            RevertSettings();
            BringToFront();
        }

        public ProgramSettings GenerateProgramSettingsFromDialog()
        {
            var res = new ProgramSettings();
            res.ClientLanguage = ProgramSettings.Instance.ClientLanguage;

            res.ClientPak = LabelGamePakPath.Text;
            res.ClientDb = LabelClientDataDb.Text;
            res.ServerDb = LabelServerDataDb.Text;
            res.MySqlDb = TextBoxServerIP.Text;
            res.MySqlUser = TextBoxUsername.Text;
            res.MySqlPassword = TextBoxPassword.Text;
            res.MySqlLogin = TextBoxLoginSchema.Text;
            res.MySqlGame = TextBoxGameSchema.Text;

            return res;
        }
    }
}
