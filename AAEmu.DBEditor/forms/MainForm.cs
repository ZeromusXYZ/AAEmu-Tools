using AAEmu.DbEditor.data;
using AAEmu.DBEditor.forms;
using AAEmu.DBEditor.forms.server;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AAEmu.DbEditor
{
    public partial class MainForm : Form
    {
        public static MainForm Self;
        public Task ValidateFilesTask;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MMFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool CloseAllForms()
        {
            if (OwnedForms.Length > 0)
                if (MessageBox.Show("Reloading will first close all open forms!\r\nDo you want to continue ?", "Reload", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                    return false;

            for (var i = OwnedForms.Length - 1; i >= 0; i--)
                OwnedForms[i].Close();

            return true;
        }

        private bool DoReload()
        {
            if (!CloseAllForms())
                return false;

            if (ValidateFilesTask != null)
            {
                MessageBox.Show("Already busy loading data, cannot reload yet!", "Reload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            Application.UseWaitCursor = true;

            /*
            var res = false;
            ValidateFilesTask = new Task(new Action(delegate
            {
                res = ValidateFiles();
            }));
            ValidateFilesTask.Start();
            */
            var res = ValidateFiles();

            // Reload the DB stuff
            Application.UseWaitCursor = false;
            return res;
        }

        private void MMFileReload_Click(object sender, EventArgs e)
        {
            DoReload();
        }

        private bool OpenServerDbTask()
        {
            var res = true;
            if (!Data.Server.OpenDB(AAEmu.DBEditor.Properties.Settings.Default.ServerDB))
            {
                MainForm.Self.UpdateProgress("Opening ServerDB failed");
                res = false;
                UpdateLabel(lServerDB, AAEmu.DBEditor.Properties.Settings.Default.ServerDB + " <failed to load>");
            }
            else
            {
                MainForm.Self.UpdateProgress("ServerDB loaded");
                UpdateLabel(lServerDB, Data.Server.FileName + " <" + Data.Server.TableNames.Count.ToString() + " tables>");
                AAEmu.DBEditor.Properties.Settings.Default.Save();
            }
            return res;
        }

        private bool OpenClientPakTask()
        {
            var res = true;
            MainForm.Self.UpdateProgress("Loading Client Pak File ...");
            if (string.IsNullOrWhiteSpace(AAEmu.DBEditor.Properties.Settings.Default.ClientPak) || !File.Exists(AAEmu.DBEditor.Properties.Settings.Default.ClientPak))
            {
                UpdateLabel(lClientPak, "<not defined>");
                TestPanel.BackgroundImage = null;
            }
            else
            if (File.Exists(AAEmu.DBEditor.Properties.Settings.Default.ClientPak) && !Data.Client.Open(AAEmu.DBEditor.Properties.Settings.Default.ClientPak))
            {
                MainForm.Self.UpdateProgress("Loading Client Pak File failed");
                res = false;
                UpdateLabel(lClientPak, Data.Client.FileName + " <failed to load>");
                TestPanel.BackgroundImage = null;
            }
            else
            {
                MainForm.Self.UpdateProgress("Loaded Client Pak");
                UpdateLabel(lClientPak, Data.Client.FileName);
                TestPanel.BackgroundImage = Data.Client.GetIconByName(ClientPak.DefaultPakIcon);
            }

            return res;
        }

        private bool OpenMySQlTask()
        {
            MainForm.Self.UpdateProgress("Opening MySQL server ...");
            Data.MySqlDb.Initialize();
            if (Data.MySqlDb.IsValid)
            {
                UpdateLabel(lMySQLServer, AAEmu.DBEditor.Properties.Settings.Default.MySQLDB);
                AAEmu.DBEditor.Properties.Settings.Default.Save();
                MainForm.Self.UpdateProgress("MySQL loaded");
            }
            else
            {
                UpdateLabel(lMySQLServer, "<failed> " + Data.MySqlDb.LastError);
                MainForm.Self.UpdateProgress("MySQL failed to load");
            }
            return Data.MySqlDb.IsValid;
        }

        private bool ValidateFiles()
        {
            UpdateProgress("Loading data ...");
            UpdateLabel(lServerDB, "Loading ...");
            UpdateLabel(lClientPak, "Loading ...");
            UpdateLabel(lMySQLServer, "Loading ...");

            var res = true;

            var mySqlTask = OpenMySQlTask();
            var serverDbTask = OpenServerDbTask();
            var clientPakTask = OpenClientPakTask();

            if (!serverDbTask)
                res = false;
            if (!clientPakTask)
                res = false;
            if (!mySqlTask)
                res = false;

            if (res)
                UpdateProgress("Done");
            else
                UpdateProgress("Loading failed");

            ValidateFilesTask = null;
            return res;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            MainForm.Self = this;
            Data.Initialize();
            ValidateFilesTask = new Task(new Action(delegate
            {
                ValidateFiles();
            }));
            ValidateFilesTask.Start();
        }

        private void MMFileOpenServer_Click(object sender, EventArgs e)
        {
            if (!CloseAllForms())
                return;

            if (ofdServerDB.ShowDialog() == DialogResult.OK)
            {
                var oldServerDB = AAEmu.DBEditor.Properties.Settings.Default.ServerDB;
                AAEmu.DBEditor.Properties.Settings.Default.ServerDB = ofdServerDB.FileName;
                if (!OpenServerDbTask())
                {
                    AAEmu.DBEditor.Properties.Settings.Default.ServerDB = oldServerDB;
                    // AAEmu.DBEditor.Properties.Settings.Default.Save();
                }
                else
                {
                    AAEmu.DBEditor.Properties.Settings.Default.ServerDB = Data.Server.FileName;
                    AAEmu.DBEditor.Properties.Settings.Default.Save();
                }
            }
        }

        private void MMFileOpenClient_Click(object sender, EventArgs e)
        {
            if (!CloseAllForms())
                return;

            if (ofdClientPak.ShowDialog() == DialogResult.OK)
            {
                var oldPak = AAEmu.DBEditor.Properties.Settings.Default.ClientPak;
                AAEmu.DBEditor.Properties.Settings.Default.ClientPak = ofdClientPak.FileName;
                Data.Client.Initialize();
                if (!OpenClientPakTask())
                {
                    AAEmu.DBEditor.Properties.Settings.Default.ClientPak = oldPak;
                    // AAEmu.DBEditor.Properties.Settings.Default.Save();
                }
                else
                {
                    AAEmu.DBEditor.Properties.Settings.Default.ClientPak = Data.Client.FileName;
                    AAEmu.DBEditor.Properties.Settings.Default.Save();
                }
            }
        }

        public void UpdateProgress(string Name, int pos = 0, int max = 0)
        {
            var percent = (int)Math.Round((double)pos / (double)max * 100f);
            Invoke(new Action(delegate
            {
                if (max > 0)
                    sbL1.Text = Name + " " + percent.ToString() + "%";
                else
                    sbL1.Text = Name;
                sbL1.Invalidate();
            }));
        }

        public void UpdateLabel(Label label, string text)
        {
            Invoke(new Action(delegate
            {
                label.Text = text;
                label.Invalidate();
            }));
        }

        private void MMClientMap_Click(object sender, EventArgs e)
        {
            MapForm.Instance.Show();
        }

        private void MMClient_DropDownOpened(object sender, EventArgs e)
        {
            MMClientMap.Enabled = Data.Client?.Pak?.IsOpen ?? false;
        }

        private void serverToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            MMServerAccounts.Enabled = Data.MySqlDb.IsValid;
            MMServerICS.Enabled = Data.MySqlDb.IsValid;
        }

        private void MMServerAccounts_Click(object sender, EventArgs e)
        {
            var accountForm = new AccountsForm();
            accountForm.Show();
        }

        private void MMFileOpenMySQL_Click(object sender, EventArgs e)
        {
            if (!CloseAllForms())
                return;

            using var mysqlSettingsForm = new MySqlSettingsForm();
            if (mysqlSettingsForm.ShowDialog() == DialogResult.OK)
                OpenMySQlTask();
        }
    }
}
