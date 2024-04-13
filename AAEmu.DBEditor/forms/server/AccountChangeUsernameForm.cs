using AAEmu.DBEditor.utils;
using MySqlX.XDevAPI.Common;
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
    public partial class AccountChangeUsernameForm : Form
    {
        private Char PasswordChar { get; set; }
        public AccountChangeUsernameForm()
        {
            InitializeComponent();
        }

        private void AccountChangePasswordForm_Load(object sender, EventArgs e)
        {
            PasswordChar = tNewUsername.PasswordChar;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ClipboardHelper.CopyToClipBoard(tNewUsername.Text);
            DialogResult = DialogResult.OK;
        }

        private void tNewPassword_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = !string.IsNullOrWhiteSpace(tNewUsername.Text);
        }
    }
}
