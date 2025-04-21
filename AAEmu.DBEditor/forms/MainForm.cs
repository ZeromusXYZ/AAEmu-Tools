using AAEmu.DBEditor.data;
using AAEmu.DBEditor.forms;
using AAEmu.DBEditor.forms.server;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using AAEmu.DBEditor.data.gamedb;
using AAEmu.DBEditor.forms.client;
using AAEmu.DBEditor.tools.ahbot;
using System.Diagnostics;

namespace AAEmu.DBEditor
{
    public partial class MainForm : Form
    {
        public const string SettingsFile = "settings.json";
        public const string NewSettingsFile = "new_settings.json";
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

        private void UpdateLocaleButtons()
        {
            var archeAgeText = Data.Server?.CompactSqlite?.LocalizedTexts?.FirstOrDefault(x => (x.TblName == "ui_texts") && (x.TblColumnName == "text") && (x.Idx == 1)); // 1944 = ArcheAge, 1 = Success!

            rbLocaleKo.Enabled = (archeAgeText != null) && !string.IsNullOrEmpty(archeAgeText.Ko);
            rbLocaleEnUs.Enabled = (archeAgeText != null) && !string.IsNullOrEmpty(archeAgeText.EnUs);
            rbLocaleRu.Enabled = (archeAgeText != null) && !string.IsNullOrEmpty(archeAgeText.Ru);
            rbLocaleZhCn.Enabled = (archeAgeText != null) && !string.IsNullOrEmpty(archeAgeText.ZhCn);
            rbLocaleZhTw.Enabled = (archeAgeText != null) && !string.IsNullOrEmpty(archeAgeText.ZhTw);
            rbLocaleJa.Enabled = (archeAgeText != null) && !string.IsNullOrEmpty(archeAgeText.Ja);
            rbLocaleDe.Enabled = (archeAgeText != null) && !string.IsNullOrEmpty(archeAgeText.De);
            rbLocaleFr.Enabled = (archeAgeText != null) && !string.IsNullOrEmpty(archeAgeText.Fr);
        }

        private bool OpenServerDbTask()
        {
            try
            {
                var res = true;
                if (!Data.Server.OpenDB(AAEmu.DBEditor.Properties.Settings.Default.ServerDB))
                {
                    UpdateProgress("Opening ServerDB failed");
                    res = false;
                    UpdateLabel(lServerDB, AAEmu.DBEditor.Properties.Settings.Default.ServerDB + " <failed to load>");
                }
                else
                {
                    UpdateProgress("ServerDB loaded");
                    UpdateLabel(lServerDB,
                        Data.Server.FileName + " <" + Data.Server.TableNames.Count.ToString() + " tables>");
                    AAEmu.DBEditor.Properties.Settings.Default.Save();
                    Data.Server.LoadDbCache();
                }
                return res;
            }
            catch (Exception e)
            {
                UpdateLabel(lServerDB, $"<failed to load> {e.Message}");
                return false;
            }
        }

        private bool OpenClientPakTask()
        {
            var res = true;
            UpdateProgress("Loading Client Pak File ...");
            if (string.IsNullOrWhiteSpace(AAEmu.DBEditor.Properties.Settings.Default.ClientPak) || !File.Exists(AAEmu.DBEditor.Properties.Settings.Default.ClientPak))
            {
                UpdateLabel(lClientPak, "<not defined>");
                TestPanel.BackgroundImage = null;
            }
            else
            if (File.Exists(AAEmu.DBEditor.Properties.Settings.Default.ClientPak) && !Data.Client.Open(AAEmu.DBEditor.Properties.Settings.Default.ClientPak))
            {
                UpdateProgress("Loading Client Pak File failed");
                res = false;
                UpdateLabel(lClientPak, Data.Client.FileName + " <failed to load>");
                TestPanel.BackgroundImage = null;
            }
            else
            {
                UpdateProgress("Loaded Client Pak");
                UpdateLabel(lClientPak, Data.Client.FileName);
                TestPanel.BackgroundImage = Data.Client.GetIconByName(ClientPak.DefaultPakIcon);
            }

            return res;
        }

        private bool OpenMySQlTask()
        {
            UpdateProgress("Opening MySQL server ...");
            Data.MySqlDb.Initialize();
            if (Data.MySqlDb.IsValid)
            {
                UpdateLabel(lMySQLServer, AAEmu.DBEditor.Properties.Settings.Default.MySQLDB + " - " + AAEmu.DBEditor.Properties.Settings.Default.MySQLLogin + " - " + AAEmu.DBEditor.Properties.Settings.Default.MySQLGame);
                AAEmu.DBEditor.Properties.Settings.Default.Save();
                UpdateProgress("MySQL loaded");
            }
            else
            {
                UpdateLabel(lMySQLServer, "<failed> " + Data.MySqlDb.LastError);
                UpdateProgress("MySQL failed to load");
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

            Invoke(new Action(delegate { UpdateLocaleButtons(); }));

            if (res)
            {
                UpdateProgress("Done");
            }
            else
            {
                UpdateProgress("Loading failed");
            }

            ValidateFilesTask = null;

            return res;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Update settings if needed
            if (!Properties.Settings.Default.IsUpdated)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();
                Properties.Settings.Default.IsUpdated = true;
                Properties.Settings.Default.Save();
            }

            var newSettingsFile = Path.Combine(ProgramSettings.GetSettingsFolder(), NewSettingsFile);
            var settingsFile = Path.Combine(ProgramSettings.GetSettingsFolder(), SettingsFile);
            var settingsToLoad = settingsFile;
            // Load new settings if it exists
            if (File.Exists(newSettingsFile))
            {
                settingsToLoad = newSettingsFile;
            }

            var loadedSettings = ProgramSettings.LoadFromFile(settingsToLoad);
            if (loadedSettings != null)
            {
                ProgramSettings.Instance = loadedSettings;
            }

            MainForm.Self = this;
            MMVersion.Text = "Version " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            Data.Initialize();
            ValidateFilesTask = new Task(new Action(delegate
            {
                var res = ValidateFiles();
                // If valid copy the new settings file and delete it
                if (res && File.Exists(newSettingsFile))
                {
                    ProgramSettings.SaveToFile(ProgramSettings.Instance, settingsFile);
                    File.Delete(newSettingsFile);
                }
            }));
            ValidateFilesTask.Start();
            UpdateLocaleButtons();
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
            // Not implemented yet, so nothing enabled
            // MMClientMap.Enabled = Data.Client?.Pak?.IsOpen ?? false;
            MMClientItems.Enabled = Data.Server.TableNames.Count > 0;
        }

        private void serverToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            MMServerAccounts.Enabled = Data.MySqlDb.IsValid;
            MMServerCharacters.Enabled = Data.MySqlDb.IsValid;
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

        private void rbLocale_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is not RadioButton rbLocale)
                return;

            if (rbLocale.Checked)
            {
                AAEmu.DBEditor.Properties.Settings.Default.ClientLanguage = rbLocale.Text;
                AAEmu.DBEditor.Properties.Settings.Default.Save();
                UpdateProgress("Updated Locale, reloading from DB ...");
                if (Data.Server.ReloadLocale(Data.Server.CompactSqlite))
                    UpdateProgress($"Locale updated to {rbLocale.Text}.");
                else
                    UpdateProgress($"Failed to update locale to {rbLocale.Text}!");
            }
        }

        private void MMServerICS_Click(object sender, EventArgs e)
        {
            var icsForm = new ICSForm();
            icsForm.Show();
        }

        private void MMClientItems_Click(object sender, EventArgs e)
        {
            var newItemForm = new ItemsForm();
            newItemForm.Show();
        }

        private void MMServerCharacters_Click(object sender, EventArgs e)
        {
            var characterForm = new CharacterForm();
            characterForm.Show();
        }

        private void MMToolsAhBot_Click(object sender, EventArgs e)
        {
            AhBotForm.Instance.Show();
        }

        private void MMTools_DropDownOpened(object sender, EventArgs e)
        {
            MMToolsAhBot.Enabled = Data.MySqlDb.IsValid && Data.Server.TableNames.Count > 0;
        }

        private void MMFileSettings_Click(object sender, EventArgs e)
        {
            using var settingsForm = new ProgramSettingsForm();
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                var newSettings = settingsForm.GenerateProgramSettingsFromDialog();
                if (!ProgramSettings.SaveToFile(newSettings,
                        Path.Combine(ProgramSettings.GetSettingsFolder(), NewSettingsFile)))
                {
                    MessageBox.Show("Failed to save new setttings");
                    return;
                }
                if (MessageBox.Show($"Settings have changed, a restart is required.{Environment.NewLine}" +
                                    $"Do you want to restart now?", "Settings", MessageBoxButtons.YesNo) ==
                    DialogResult.Yes)
                {

                    if (RestartProgram())
                        Close();
                }
            }
        }

        private bool RestartProgram()
        {
            if (!CloseAllForms())
                return false;

            var exeFile = Environment.ProcessPath ?? string.Empty;
            if (!File.Exists(exeFile))
            {
                MessageBox.Show($"I no longer exist ?{Environment.NewLine}{exeFile}");
                return false;
            }

            var newProcess = new ProcessStartInfo(exeFile, string.Empty);
            newProcess.UseShellExecute = true;
            // newProcess.Verb = "runas";
            bool startOK = false;
            try
            {
                Process.Start(newProcess);
                startOK = true;
            }
            catch
            {
                startOK = false;
            }
            return startOK;
        }
    }
}
