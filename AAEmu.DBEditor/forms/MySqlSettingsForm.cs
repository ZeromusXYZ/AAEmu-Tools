﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AAEmu.DBEditor.forms
{
    public partial class MySqlSettingsForm : Form
    {
        public MySqlSettingsForm()
        {
            InitializeComponent();
        }

        private void MySqlSettingsForm_Load(object sender, EventArgs e)
        {
            tbServerIP.Text = AAEmu.DBEditor.Properties.Settings.Default.MySQLDB;
            tbUsername.Text = AAEmu.DBEditor.Properties.Settings.Default.MySQLUser;
            tbPassword.Text = AAEmu.DBEditor.Properties.Settings.Default.MySQLPassword;
            tbLoginSchema.Text = AAEmu.DBEditor.Properties.Settings.Default.MySQLLogin;
            tbGameSchema.Text = AAEmu.DBEditor.Properties.Settings.Default.MySQLGame;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AAEmu.DBEditor.Properties.Settings.Default.MySQLDB = tbServerIP.Text;
            AAEmu.DBEditor.Properties.Settings.Default.MySQLUser = tbUsername.Text;
            AAEmu.DBEditor.Properties.Settings.Default.MySQLPassword = tbPassword.Text;
            AAEmu.DBEditor.Properties.Settings.Default.MySQLLogin = tbLoginSchema.Text;
            AAEmu.DBEditor.Properties.Settings.Default.MySQLGame = tbGameSchema.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
