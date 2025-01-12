using AAEmu.DBEditor.utils;
using System;
using System.Text;
using System.Windows.Forms;

namespace AAEmu.DBEditor.forms.server
{
    public partial class AccountChangePasswordForm : Form
    {
        private Char PasswordChar { get; set; }
        public AccountChangePasswordForm()
        {
            InitializeComponent();
        }

        private void AccountChangePasswordForm_Load(object sender, EventArgs e)
        {
            PasswordChar = tNewPassword.PasswordChar;
        }

        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            tNewPassword.PasswordChar = cbShowPassword.Checked ? '\0' : PasswordChar;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ClipboardHelper.CopyToClipBoard(tNewPassword.Text);
            DialogResult = DialogResult.OK;
        }

        private void tNewPassword_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = !string.IsNullOrWhiteSpace(tNewPassword.Text);
        }

        private string CreatePassword(int length)
        {
            const string valid = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ1234567890-+=*/$%#?.!_";
            StringBuilder res = new StringBuilder();
            while (0 < length--)
            {
                res.Append(valid[Random.Shared.Next(valid.Length)]);
            }
            return res.ToString();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            tNewPassword.Text = CreatePassword(Random.Shared.Next(16, 24));
        }
    }
}
