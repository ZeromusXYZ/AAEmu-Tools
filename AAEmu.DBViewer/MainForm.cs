using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Numerics;
using System.Reflection;
using AAPacker;
using AAEmu.Game.Utils.DB;
using AAEmu.ClipboardHelper;
using AAEmu.DBViewer.utils;
using System.Runtime;
using AAEmu.DBViewer.enums;
using System.Security.Cryptography;
using Newtonsoft.Json;
using AAEmu.DBViewer.DbDefs;
using System.Globalization;
using AAEmu.Commons.Utils;
using Newtonsoft.Json.Linq;

namespace AAEmu.DBViewer
{
    public partial class MainForm : Form
    {
        public static MainForm ThisForm { get; set; }
        private string DefaultTitle { get; set; } = string.Empty;
        public AAPak Pak { get; set; } = new("");
        private List<string> PossibleLanguageIDs { get; set; } = [];
        // TableName, SqliteFileName
        private Dictionary<string, string> AllTableNames { get; set; } = new();
        private List<TabPage> TabHistory { get; set; } = new();
        private bool SkipTabHistory { get; set; } = false;
        private int TabHistoryIndex { get; set; } = 0;
        private List<SavedProfile> Profiles { get; set; } = new();

        public MainForm()
        {
            InitializeComponent();
            ThisForm = this;
        }

        private static void LoadCustomReaders()
        {
            var customReadersFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ZeromusXYZ", "AAPakEditor");
            AAPak.ReaderPool.Clear();

            var jsonSettings = new JsonSerializerSettings
            {
                Culture = CultureInfo.InvariantCulture,
                Formatting = Formatting.Indented,
            };
            jsonSettings.Converters.Add(new ByteArrayHexConverter());

            var readerSettingsFileName = Path.Combine(customReadersFolder, "readers.json");
            try
            {
                if (File.Exists(readerSettingsFileName))
                {
                    var data = JsonConvert.DeserializeObject<List<AAPakFileFormatReader>>(File.ReadAllText(readerSettingsFileName), jsonSettings);
                    if (data?.Count > 0)
                        foreach (var r in data)
                            AAPak.ReaderPool.Add(r);
                }
            }
            catch
            {
                // Ignore
            }

            // Add only default in case of errors
            if (AAPak.ReaderPool.Count <= 0)
            {
                AAPak.ReaderPool.Add(new AAPakFileFormatReader(true));
                // Write default file to user's settings
                try
                {
                    Directory.CreateDirectory(customReadersFolder);
                    File.WriteAllText(readerSettingsFileName, JsonConvert.SerializeObject(AAPak.ReaderPool, jsonSettings));
                }
                catch
                {
                    // Ignore
                }
            }
        }

        private void TryLoadPakKeys(string fileName)
        {
            try
            {
                var keyFile = fileName + ".key";
                if (File.Exists(keyFile))
                {
                    var customKey = new byte[16];
                    using (var fs = new FileStream(keyFile, FileMode.Open, FileAccess.Read))
                    {
                        if (fs.Length != 16)
                        {
                            fs.Dispose();
                            return;
                        }

                        fs.Read(customKey, 0, 16);
                    }

                    Pak.SetCustomKey(customKey);
                }
                else
                    Pak.SetDefaultKey();
            }
            catch
            {
                // Reset key
                Pak.SetDefaultKey();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            MMVersion.Text = $"Version {Assembly.GetExecutingAssembly().GetName().Version}";
            lAppVersion.Text = MMVersion.Text;
            MM.Visible = false;
            tcViewer.ItemSize = new Size(0, 1);

            LoadCustomReaders();

            // Update settings if needed
            if (!Properties.Settings.Default.IsUpdated)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();
                Properties.Settings.Default.IsUpdated = true;
                Properties.Settings.Default.Save();
            }

            DefaultTitle = Text;
            PossibleLanguageIDs.Clear();
            PossibleLanguageIDs.Add("ko");
            PossibleLanguageIDs.Add("ru");
            PossibleLanguageIDs.Add("en_us");
            PossibleLanguageIDs.Add("zh_cn");
            PossibleLanguageIDs.Add("zh_tw");
            PossibleLanguageIDs.Add("de");
            PossibleLanguageIDs.Add("fr");
            PossibleLanguageIDs.Add("ja");
            cbItemSearchRange.SelectedIndex = 0;
            tcDoodads.SelectedTab = tpDoodadWorkflow;

            cbItemSearchType.Items.Clear();
            cbItemSearchType.Items.Add("Any");
            foreach (var implId in Enum.GetValues(typeof(GameItemImplId)))
                cbItemSearchType.Items.Add(((int)implId).ToString() + " - " + implId.ToString());
            cbItemSearchType.SelectedIndex = 0;

            LoadProfiles();
            SelectCurrentProfile();

            if (!LoadServerDB(false))
            {
                Close();
                return;
            }


            var gamePakFileName = Properties.Settings.Default.GamePakFileName;
            if (File.Exists(gamePakFileName))
            {
                using var loading = new LoadingForm();
                loading.Text = $@"Loading: {Path.GetFileName(gamePakFileName)}";
                loading.Show();
                loading.ShowInfo("Opening: " + Path.GetFileName(gamePakFileName));

                // TryLoadPakKeys(gamePakFileName);

                if (Pak.OpenPak(gamePakFileName, true))
                {
                    Properties.Settings.Default.GamePakFileName = gamePakFileName;
                    lCurrentPakFile.Text = Properties.Settings.Default.GamePakFileName;

                    loading.ShowInfo("Loading: World Data");
                    PrepareWorldXml(true);
                    loading.ShowInfo("Loading: Quest Sign Sphere Data");
                    LoadQuestSpheresFromPak();
                }
            }

            LoadHistory(Properties.Settings.Default.HistorySearchItem, cbItemSearch);
            LoadHistory(Properties.Settings.Default.HistorySearchNpc, cbSearchNPC);
            LoadHistory(Properties.Settings.Default.HistorySearchQuest, cbQuestSearch);
            LoadHistory(Properties.Settings.Default.HistorySearchSkill, cbSkillSearch);
            LoadHistory(Properties.Settings.Default.HistorySearchDoodad, cbSearchDoodads);
            LoadHistory(Properties.Settings.Default.HistorySearchBuff, cbSearchBuffs);
            LoadHistory(Properties.Settings.Default.HistorySearchSphere, CbSearchSpheres);
            LoadHistory(Properties.Settings.Default.HistorySearchSQL, cbSimpleSQL);

            tcViewer.SelectedTab = tpItems;
            AttachEventsToLabels();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.HistorySearchItem = CreateHistory(cbItemSearch);
            Properties.Settings.Default.HistorySearchNpc = CreateHistory(cbSearchNPC);
            Properties.Settings.Default.HistorySearchQuest = CreateHistory(cbQuestSearch);
            Properties.Settings.Default.HistorySearchSkill = CreateHistory(cbSkillSearch);
            Properties.Settings.Default.HistorySearchDoodad = CreateHistory(cbSearchDoodads);
            Properties.Settings.Default.HistorySearchBuff = CreateHistory(cbSearchBuffs);
            Properties.Settings.Default.HistorySearchSphere = CreateHistory(CbSearchSpheres);
            Properties.Settings.Default.HistorySearchSQL = CreateHistory(cbSimpleSQL);
            SaveProfiles();
            Properties.Settings.Default.Save();

            if (MapViewForm.ThisForm != null)
                MapViewForm.ThisForm.Close();

            if (Pak != null)
                Pak.ClosePak();
        }

        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                .Concat(controls)
                .Where(c => c.GetType() == type);
        }

        public static void CopyToClipBoard(string cliptext)
        {
            try
            {
                // Because nothing is ever as simple as the next line >.>
                // Clipboard.SetText(s);
                // Helper will (try to) prevent errors when copying to clipboard because of threading issues
                var cliphelp = new SetClipboardHelper(DataFormats.Text, cliptext);
                cliphelp.DontRetryWorkOnFailed = false;
                cliphelp.Go();
            }
            catch
            {
            }
        }

        private void LabelToCopy_Click(object sender, EventArgs e)
        {
            if (sender is Label)
            {
                string s = (sender as Label).Text;
                if ((s == "<none>") || (s == "???") || (s.Trim(' ') == string.Empty))
                    s = string.Empty;
                if (s != string.Empty)
                    CopyToClipBoard(s);
            }
        }

        private void AttachEventsToLabels()
        {
            var controls = GetAll(this, typeof(Label));
            foreach (Control c in controls)
            {
                if (c is Label)
                {
                    Label l = (c as Label);
                    if (!l.Name.ToLower().StartsWith("label"))
                    {
                        // l.ForeColor = Color.Red;
                        l.Cursor = Cursors.Hand;
                        l.Click += LabelToCopy_Click;
                    }
                    else
                    {
                        l.ForeColor = SystemColors.ControlDark;
                    }
                }
            }
        }

        private long GetInt64(SQLiteWrapperReader reader, string fieldname)
        {
            if (reader.IsDBNull(fieldname))
                return 0;
            else
                return reader.GetInt64(fieldname);
        }

        private string GetString(SQLiteWrapperReader reader, string fieldname)
        {
            if (reader.IsDBNull(fieldname))
                return string.Empty;
            else
                return reader.GetString(fieldname);
        }

        private bool GetBool(SQLiteWrapperReader reader, string fieldname)
        {
            if (reader.IsDBNull(fieldname))
                return false;
            else
            {
                var val = reader.GetString(fieldname);
                return ((val != null) && ((val == "t") || (val == "T") || (val == "1")));
            }
        }

        private float GetFloat(SQLiteWrapperReader reader, string fieldname)
        {
            if (reader.IsDBNull(fieldname))
                return 0;
            else
                return reader.GetFloat(fieldname);
        }

        private void LoadTableNames()
        {
            var tableList = new List<string>();
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT name FROM sqlite_master WHERE type = 'table' ORDER BY name ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        while (reader.Read())
                        {
                            var tName = GetString(reader, "name");
                            tableList.Add(tName);
                        }
                    }
                }
            }

            foreach (var table in tableList)
            {
                if (AllTableNames.ContainsKey(table))
                    continue;

                AllTableNames.Add(table, SQLite.SQLiteFileName);
                lbTableNames.Items.Add(table);
            }
        }


        /// <summary>
        /// Convert a hex string to a .NET Color object.
        /// </summary>
        /// <param name="hexColor">a hex string: "FFFFFFFF", "00000000"</param>
        public static Color HexStringToARGBColor(string hexARGB)
        {
            string a;
            string r;
            string g;
            string b;
            if (hexARGB.Length == 8)
            {
                a = hexARGB.Substring(0, 2);
                r = hexARGB.Substring(2, 2);
                g = hexARGB.Substring(4, 2);
                b = hexARGB.Substring(6, 2);
            }
            else if (hexARGB.Length == 6)
            {
                a = hexARGB.Substring(0, 2);
                r = hexARGB.Substring(2, 2);
                g = hexARGB.Substring(4, 2);
                b = hexARGB.Substring(6, 2);
            }
            else
            {
                // you can choose whether to throw an exception
                //throw new ArgumentException("hexColor is not exactly 6 digits.");
                return Color.Empty;
            }

            Color color = Color.Empty;
            try
            {
                int ai = Int32.Parse(a, System.Globalization.NumberStyles.HexNumber);
                int ri = Int32.Parse(r, System.Globalization.NumberStyles.HexNumber);
                int gi = Int32.Parse(g, System.Globalization.NumberStyles.HexNumber);
                int bi = Int32.Parse(b, System.Globalization.NumberStyles.HexNumber);
                color = Color.FromArgb(ai, ri, gi, bi);
            }
            catch
            {
                // you can choose whether to throw an exception
                //throw new ArgumentException("Conversion failed.");
                return Color.Empty;
            }

            return color;
        }

        private void FormattedTextToRichtEditOld(string formattedText, RichTextBox rt)
        {
            rt.Text = string.Empty;
            rt.SelectionColor = rt.ForeColor;
            rt.SelectionBackColor = rt.BackColor;
            string restText = formattedText;
            restText = restText.Replace("\r\n\r\n", "\r");
            restText = restText.Replace("\r\n", "\r");
            restText = restText.Replace("\n", "\r");
            restText = restText.Replace("|nc;", "|cFFFFFFFF;");
            var colStartPos = -1;
            var colResetPos = -1;
            bool invalidPipe = false;
            while (restText.Length > 0)
            {
                var pipeStart = restText.IndexOf("|");
                var atStart = restText.IndexOf("@");

                if ((pipeStart >= 0) && (atStart >= 0) && (atStart < pipeStart))
                {
                    pipeStart = -1;
                }

                if ((atStart >= 0) && (restText.Length >= pipeStart + 12))
                {
                    var atStartBracket = restText.IndexOf("(", atStart);
                    var atEndBracket = restText.IndexOf(")", atStart);
                    if ((atStartBracket >= 0) && (atEndBracket >= 0) && (atEndBracket > atStartBracket))
                    {
                        var fieldNameStr = restText.Substring(atStart + 1, atStartBracket - atStart - 1);
                        var fieldValStr = restText.Substring(atStartBracket + 1, atEndBracket - atStartBracket - 1);
                        if (long.TryParse(fieldValStr, out long itemVal))
                        {
                            rt.AppendText(restText.Substring(0, atStart));
                            rt.SelectionColor = Color.Yellow;
                            if ((fieldNameStr == "ITEM_NAME") &&
                                (AaDb.DbItems.TryGetValue(itemVal, out GameItem item)))
                            {
                                rt.AppendText(item.NameLocalized);
                            }
                            else if ((fieldNameStr == "NPC_NAME") &&
                                     (AaDb.DbNpCs.TryGetValue(itemVal, out GameNpc npc)))
                            {
                                rt.AppendText(npc.NameLocalized);
                            }
                            else if ((fieldNameStr == "QUEST_NAME") &&
                                     (AaDb.DbQuestContexts.TryGetValue(itemVal, out GameQuestContexts quest)))
                            {
                                rt.AppendText(quest.NameLocalized);
                            }
                            else
                            {
                                rt.AppendText("@" + fieldNameStr + "(" + itemVal.ToString() + ")");
                            }

                            rt.SelectionColor = rt.ForeColor;

                            restText = restText.Substring(atEndBracket + 1);
                            pipeStart = -1;
                        }
                    }
                }


                if ((pipeStart >= 0) && (restText.Length >= pipeStart + 1))
                {
                    var pipeOption = restText.Substring(pipeStart + 1, 1);
                    colStartPos = -1;
                    colResetPos = -1;
                    invalidPipe = false;
                    switch (pipeOption)
                    {
                        case "c":
                            if (restText.Length >= pipeStart + 10)
                                colStartPos = pipeStart;
                            break;
                        case "r":
                            if (restText.Length >= pipeStart + 1)
                                colResetPos = pipeStart;
                            break;
                        default:
                            invalidPipe = true;
                            break;
                    }
                }
                else
                {
                    colStartPos = -1;
                    colResetPos = -1;
                }

                if ((invalidPipe) && (pipeStart >= 0))
                {
                    // We need to handle this in order to fix bugs related to text formatting
                    rt.AppendText(restText.Substring(0, pipeStart));
                    rt.SelectionColor = rt.ForeColor;
                    rt.AppendText("|");
                    restText = restText.Substring(pipeStart + 1);
                }
                else if ((colStartPos >= 0) && (restText.Length >= colStartPos + 10))
                {
                    var colText = restText.Substring(colStartPos + 2, 8);
                    rt.AppendText(restText.Substring(0, colStartPos));
                    var newCol = HexStringToARGBColor(colText);
                    rt.SelectionColor = newCol;
                    restText = restText.Substring(colStartPos + 10);
                }
                else if ((colResetPos >= 0) && (restText.Length >= colResetPos + 2))
                {
                    rt.AppendText(restText.Substring(0, colResetPos));
                    rt.SelectionColor = rt.ForeColor;
                    restText = restText.Substring(colResetPos + 2);
                }
                else if (atStart >= 0)
                {
                    // already handled
                }
                else
                {
                    rt.AppendText(restText);
                    restText = string.Empty;
                }
            }

        }

        private void FormattedTextToRichtEdit(string formattedText, RichTextBox rt)
        {
            rt.Text = string.Empty;
            rt.SelectionColor = rt.ForeColor;
            rt.SelectionBackColor = rt.BackColor;
            string restText = formattedText;
            //restText = restText.Replace("\r\n\r\n", "\r");
            restText = restText.Replace("\r\n", "\r");
            restText = restText.Replace("\n", "\r");
            var resetColor = rt.ForeColor;

            while (restText.Length > 0)
            {
                var nextEndBracket = restText.IndexOf(")");
                var nextEndAccolade = restText.IndexOf("}");
                if (restText.StartsWith("\r")) // linefeed
                {
                    var currentCol = rt.SelectionColor;
                    rt.AppendText("\r");
                    rt.SelectionColor = currentCol;
                    restText = restText.Substring(1);
                }
                else if (restText.StartsWith("|r")) // reset color
                {
                    rt.SelectionColor = resetColor;
                    restText = restText.Substring(2);
                }
                else if (restText.StartsWith("[START_DESCRIPTION]")) // reset color
                {
                    resetColor = Color.LimeGreen;
                    rt.SelectionColor = resetColor;
                    restText = restText.Substring(19);
                }
                else if (restText.StartsWith("|nc;")) // named color ?
                {
                    rt.SelectionColor = Color.White;
                    restText = restText.Substring(4);
                }
                else if (restText.StartsWith("|ni;")) // named color ?
                {
                    rt.SelectionColor = Color.GreenYellow;
                    restText = restText.Substring(4);
                }
                else if (restText.StartsWith("|nd;")) // named color ?
                {
                    rt.SelectionColor = Color.OrangeRed;
                    restText = restText.Substring(4);
                }
                else if (restText.StartsWith("|nr;")) // named color ?
                {
                    rt.SelectionColor = Color.Red;
                    restText = restText.Substring(4);
                }
                else if (restText.StartsWith("|c")) // hexcode color
                {
                    rt.SelectionColor = HexStringToARGBColor(restText.Substring(2, 8));
                    restText = restText.Substring(10);
                }
                else if (restText.StartsWith("@ITEM_NAME(") && (nextEndBracket > 11))
                {
                    rt.SelectionColor = Color.Yellow;
                    var valueText = restText.Substring(11, nextEndBracket - 11);
                    if (long.TryParse(valueText, out var value) && (AaDb.DbItems.TryGetValue(value, out var item)))
                        rt.AppendText(item.NameLocalized);
                    else
                        rt.AppendText("@ITEM_NAME(" + valueText + ")");
                    rt.SelectionColor = resetColor;
                    restText = restText.Substring(nextEndBracket + 1);
                }
                else if (restText.StartsWith("@DOODAD_NAME(") && (nextEndBracket > 13))
                {
                    rt.SelectionColor = Color.Yellow;
                    var valueText = restText.Substring(13, nextEndBracket - 13);
                    if (long.TryParse(valueText, out var value) &&
                        (AaDb.DbDoodadAlmighties.TryGetValue(value, out var doodad)))
                        rt.AppendText(doodad.NameLocalized);
                    else
                        rt.AppendText("@DOODAD_NAME(" + valueText + ")");
                    rt.SelectionColor = resetColor;
                    restText = restText.Substring(nextEndBracket + 1);
                }
                else if (restText.StartsWith("@NPC_NAME(") && (nextEndBracket > 10))
                {
                    rt.SelectionColor = Color.Yellow;
                    var valueText = restText.Substring(10, nextEndBracket - 10);
                    if (long.TryParse(valueText, out var value) && (AaDb.DbNpCs.TryGetValue(value, out var npc)))
                        rt.AppendText(npc.NameLocalized);
                    else
                        rt.AppendText("@NPC_NAME(" + valueText + ")");
                    rt.SelectionColor = resetColor;
                    restText = restText.Substring(nextEndBracket + 1);
                }
                else if (restText.StartsWith("@NPC_GROUP_NAME(") && (nextEndBracket > 16))
                {
                    rt.SelectionColor = Color.Yellow;
                    var valueText = restText.Substring(16, nextEndBracket - 16);
                    if (long.TryParse(valueText, out var value) && (AaDb.DbQuestMonsterGroups.TryGetValue(value, out var npcGroup)))
                        rt.AppendText(npcGroup.NameLocalized);
                    else
                        rt.AppendText("@NPC_GROUP_NAME(" + valueText + ")");
                    rt.SelectionColor = resetColor;
                    restText = restText.Substring(nextEndBracket + 1);
                }
                else if (restText.StartsWith("@QUEST_NAME(") && (nextEndBracket > 12))
                {
                    rt.SelectionColor = Color.Yellow;
                    var valueText = restText.Substring(12, nextEndBracket - 12);
                    if (long.TryParse(valueText, out var value) &&
                        (AaDb.DbQuestContexts.TryGetValue(value, out var quest)))
                        rt.AppendText(quest.NameLocalized);
                    else
                        rt.AppendText("@QUEST_NAME(" + valueText + ")");
                    rt.SelectionColor = resetColor;
                    restText = restText.Substring(nextEndBracket + 1);
                }
                else if (restText.StartsWith("#{") && (nextEndAccolade > 2))
                {
                    rt.SelectionColor = Color.Pink;
                    var valueText = restText.Substring(2, nextEndAccolade - 2);
                    rt.AppendText("#{" + valueText + "}");
                    rt.SelectionColor = resetColor;
                    restText = restText.Substring(nextEndAccolade + 1);
                }
                else
                {
                    rt.AppendText(restText.Substring(0, 1));
                    restText = restText.Substring(1);
                }
            }
        }

        private string MSToString(long msTime, bool dontShowMS = false)
        {
            string res = string.Empty;
            long ms = (msTime % 1000);
            msTime = msTime / 1000;
            long ss = (msTime % 60);
            msTime = msTime / 60;
            long mm = (msTime % 60);
            msTime = msTime / 60;
            long hh = (msTime % 24);
            msTime = msTime / 24;
            long dd = msTime;

            if (dd > 0)
                res += dd.ToString() + "d ";
            if (hh > 0)
                res += hh.ToString() + "h ";
            if (mm > 0)
                res += mm.ToString() + "m ";
            if (dontShowMS == false)
            {
                if (ss > 0)
                    res += ss.ToString() + "s ";
                if (ms > 0)
                    res += ms.ToString() + "ms";
            }
            else if ((ss > 0) || (ms > 0))
            {
                float s = ss + (1f / 1000f * ms);
                res += s.ToString() + "s";
            }

            if (res == string.Empty)
                res = "none";

            return res;
        }

        private string SecondsToString(long secTime)
        {
            string res = string.Empty;
            long ss = (secTime % 60);
            secTime = secTime / 60;
            long mm = (secTime % 60);
            secTime = secTime / 60;
            long hh = (secTime % 24);
            secTime = secTime / 24;
            long dd = secTime;

            if (dd > 0)
                res += dd.ToString() + "d ";
            if (hh > 0)
                res += hh.ToString() + "h ";
            if (mm > 0)
                res += mm.ToString() + "m ";
            if (ss > 0)
                res += ss.ToString() + "s ";

            if (res == string.Empty)
                res = "none";

            return res;
        }

        private string RangeToString(float range)
        {
            if (Math.Abs(range) < 150.0f)
            {
                return range.ToString("0") + " mm";
            }
            else if (Math.Abs(range) < 1000.0f)
            {
                return (range / 10).ToString("0") + " cm";
            }
            else if (Math.Abs(range) < 15000.0f)
            {
                return (range / 1000).ToString("0.0") + " m";
            }
            else
            {
                return (range / 1000).ToString("0") + " m";
            }
        }


        private string CopperToValuta(long copper)
        {
            var res = "";
            if (copper > 10000)
            {
                var gold = copper / 10000;
                res += gold.ToString() + "g ";
                copper -= gold * 10000;
            }
            if (copper > 100)
            {
                var silver = copper / 100;
                res += silver.ToString() + "s ";
                copper -= silver * 100;
            }
            if (copper > 0)
            {
                res += copper.ToString() + "c";
            }
            else
            {
                if (copper < 0)
                {
                    res = copper.ToString();
                }
            }
            return res.Trim();
        }

        private string VisualizeAmounts(long amountMin, long amountMax, long itemId)
        {
            var res = string.Empty;
            if (itemId != 500)
            {
                res = amountMin != amountMax ? amountMin.ToString() + "~" + amountMax : amountMin.ToString();
                return res;
            }
            res = amountMin != amountMax ? CopperToValuta(amountMin) + " ~ " + CopperToValuta(amountMax) : CopperToValuta(amountMin);
            return res;
        }

        private void BtnOpenServerDB_Click(object sender, EventArgs e)
        {
            LoadServerDB(true);
            UpdateTitleBar();
        }

        private void UpdateTitleBar()
        {
            var sqliteFiles = Properties.Settings.Default.DBFileName.Split(Path.PathSeparator);
            var titleSqliteFiles = string.Empty;

            var directories = new List<string>();
            foreach (var sqliteFile in sqliteFiles)
            {
                var dir = Path.GetDirectoryName(sqliteFile);
                if (!directories.Contains(dir))
                    directories.Add(dir);
            }

            // If only one directory, then truncate to the file names only, otherwise, the full path
            foreach (var sqliteFile in sqliteFiles)
            {
                if (!string.IsNullOrWhiteSpace(titleSqliteFiles))
                    titleSqliteFiles += " + ";
                else
                if (directories.Count == 1)
                    titleSqliteFiles += directories[0] + Path.DirectorySeparatorChar;
                titleSqliteFiles += (directories.Count == 1) ? Path.GetFileName(sqliteFile) : sqliteFile;
            }

            // Properties.Settings.Default.DBFileName = sqliteFileName;
            Text = $@"{DefaultTitle} [{tcViewer.SelectedTab?.Text}] - ({Properties.Settings.Default.DefaultGameLanguage}) {titleSqliteFiles}";
        }

        private bool LoadServerDB(bool forceDlg)
        {
            var sqliteFiles = Properties.Settings.Default.DBFileName.Split(Path.PathSeparator);
            var sqliteFile = sqliteFiles.Length <= 0 ? string.Empty : sqliteFiles[0];

            while (forceDlg || (!File.Exists(sqliteFile) && sqliteFiles.Length <= 1))
            {
                forceDlg = false;
                if (openDBDlg.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }
                else
                {
                    sqliteFile = openDBDlg.FileName;
                    sqliteFiles = new[] { sqliteFile };
                }
            }

            AaDb.Clear();
            AllTableNames.Clear();

            var res = true;
            if (sqliteFiles.Length == 1)
            {
                res = LoadServerDbFile(sqliteFile);
                if (res)
                    Properties.Settings.Default.DBFileName = sqliteFile;
                sqliteFiles = new[] { sqliteFile };
            }
            else
            {

                foreach (var file in sqliteFiles)
                {
                    res &= LoadServerDbFile(file);
                }

                if (res)
                    Properties.Settings.Default.DBFileName = string.Join(Path.PathSeparator, sqliteFiles);
            }

            CbSimpleSqlSourceDb.Items.Clear();
            foreach (var fileName in sqliteFiles)
            {
                CbSimpleSqlSourceDb.Items.Add(fileName);
            }
            if (CbSimpleSqlSourceDb.Items.Count > 0)
                CbSimpleSqlSourceDb.SelectedIndex = 0;

            tFilterTables_TextChanged(null, null);

            return res;
        }

        public bool LoadServerDbFile(string sqliteFileName)
        {
            SQLite.SQLiteFileName = sqliteFileName;

            var i = cbItemSearchLanguage.Items.IndexOf(Properties.Settings.Default.DefaultGameLanguage);
            cbItemSearchLanguage.SelectedIndex = i;

            using var loading = new LoadingForm();
            try
            {

                loading.Text = $@"Loading: {Path.GetFileName(sqliteFileName)}";
                loading.Show();

                // The table name loading is basically just to check if we can read the DB file
                loading.ShowInfo("Loading: Table names");
                LoadTableNames();

                UpdateTitleBar();

                // Make sure translations are loaded first, other tables depend on it
                loading.ShowInfo("Loading: Translation");
                LoadTranslations(Properties.Settings.Default.DefaultGameLanguage);
                loading.ShowInfo("Loading: Custom Translations");
                AddCustomTranslations();
                LoadUiTexts();

                loading.ShowInfo("Loading: Icon info");
                LoadIcons();

                loading.ShowInfo("Loading: Factions");
                LoadFactions();

                loading.ShowInfo("Loading: Plots");
                LoadPlots();

                loading.ShowInfo("Loading: Buffs");
                LoadBuffs();

                loading.ShowInfo("Loading: Transfers");
                LoadTransfers();
                LoadTransferPaths();

                loading.ShowInfo("Loading: Tags");
                LoadTags();

                loading.ShowInfo("Loading: Zones");
                LoadZoneGroupBannedTags();
                LoadZones();

                loading.ShowInfo("Loading: Doodads");
                LoadDoodads();

                loading.ShowInfo("Loading: Items");
                LoadItemCategories();
                LoadItems();
                LoadItemArmors();
                LoadItemWeapons();

                loading.ShowInfo("Loading: Skills");
                LoadSkills();
                LoadSkillReagents();
                LoadSkillProducts();
                LoadUnitReqs();
                LoadUnitMods();

                loading.ShowInfo("Loading: Models");
                LoadModels();

                loading.ShowInfo("Loading: NPCs");
                LoadNpcs();

                loading.ShowInfo("Loading: Vehicles");
                LoadSlaves();

                loading.ShowInfo("Loading: Quests");
                LoadQuests();

                loading.ShowInfo("Loading: Trades");
                LoadTrades();

                loading.ShowInfo("Loading: Loot");
                LoadLoots();

                loading.ShowInfo("Loading: Schedules");
                LoadSchedules();

                loading.ShowInfo("Loading: Spheres");
                LoadSpheresFromCompact();

                loading.ShowInfo("Loading: Achievements");
                LoadAchievements();

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, @"Exception loading DB", MessageBoxButtons.OK);
                return false;
            }
        }


        private void BtnFindGameClient_Click(object sender, EventArgs e)
        {
            _ = DoFindGameClient(true);
        }

        private bool DoFindGameClient(bool forceDialog)
        {
            var openFileName = Properties.Settings.Default.GamePakFileName;
            if (forceDialog)
            {
                if (openGamePakFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }

                openFileName = openGamePakFileDialog.FileName;
            }

            if (!File.Exists(openFileName))
                return false;

            using var loading = new LoadingForm();
            loading.Show();
            if (Pak.IsOpen)
            {
                loading.ShowInfo("Closing: " + Pak.GpFilePath);
                Pak.ClosePak();
                // LoadCustomReaders();

                // TODO: HACK to try and free up as many memory as possible - https://stackoverflow.com/questions/30622145/free-memory-of-byte
                GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
                GC.Collect();
            }

            loading.ShowInfo("Opening: " + Path.GetFileName(openFileName));

            // TryLoadPakKeys(openFileName);

            if (Pak.OpenPak(openFileName, true))
            {
                Properties.Settings.Default.GamePakFileName = openFileName;
                lCurrentPakFile.Text = Properties.Settings.Default.GamePakFileName;

                PrepareWorldXml(true);
                if (MapViewForm.ThisForm != null)
                {
                    MapViewForm.ThisForm.Close();
                    MapViewForm.ThisForm = null;
                }
            }
            else
            {
                MessageBox.Show($"Failed to load: {openFileName}\n{Pak.LastError}");
                return false;
            }

            return true;
        }

        private void ShowSelectedData(string table, string whereStatement, string orderStatement)
        {
            var dbFile = AllTableNames.GetValueOrDefault(table) ?? string.Empty;
            if (string.IsNullOrWhiteSpace(dbFile) || !File.Exists(dbFile))
            {
                labelCurrentDataInfo.Text = $@"{table} not found in {dbFile}, something went wrong";
                dgvCurrentData.Rows.Clear();
                return;
            }

            SQLite.SQLiteFileName = dbFile;
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM " + table + " WHERE " + whereStatement + " ORDER BY " +
                                          orderStatement + " LIMIT 1";
                    labelCurrentDataInfo.Text = command.CommandText;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;
                        dgvCurrentData.Rows.Clear();
                        var columnNames = reader.GetColumnNames();

                        while (reader.Read())
                        {
                            long thisID = -1;
                            if (columnNames.IndexOf("id") >= 0)
                                thisID = GetInt64(reader, "id");

                            foreach (var col in columnNames)
                            {
                                int line = dgvCurrentData.Rows.Add();
                                var row = dgvCurrentData.Rows[line];
                                row.Cells[0].Value = col;
                                row.Cells[1].Value = reader.GetValue(col).ToString();
                                ;
                                row.Cells[2].Value = AaDb.GetTranslationById(thisID, table, col, " ");
                            }
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }

        }

        private void LbTableNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbTableNames.SelectedItem == null)
                return;
            var tablename = lbTableNames.SelectedItem.ToString();
            CbSimpleSqlSourceDb.Text = AllTableNames.GetValueOrDefault(tablename) ?? string.Empty;
            cbSimpleSQL.Text = "SELECT * FROM " + tablename + " LIMIT 0, 50";

            // BtnSimpleSQL_Click(null, null);
        }

        private void BtnSimpleSQL_Click(object sender, EventArgs e)
        {
            if (cbSimpleSQL.Text == string.Empty)
                return;

            try
            {
                SQLite.SQLiteFileName = CbSimpleSqlSourceDb.Text;
                using (var connection = SQLite.CreateConnection())
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = cbSimpleSQL.Text;
                        command.Prepare();
                        using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                        {
                            Application.UseWaitCursor = true;
                            Cursor = Cursors.WaitCursor;
                            dgvSimple.Rows.Clear();
                            dgvSimple.Columns.Clear();
                            var columnNames = reader.GetColumnNames();

                            foreach (var col in columnNames)
                            {
                                dgvSimple.Columns.Add(col, col);
                            }

                            while (reader.Read())
                            {
                                int line = dgvSimple.Rows.Add();
                                var row = dgvSimple.Rows[line];
                                int c = 0;
                                foreach (var col in columnNames)
                                {
                                    row.Cells[c].Value = reader.GetValue(col).ToString();
                                    c++;
                                }

                                if (((line % 100) == 0) && (line > 0))
                                {
                                    if (MessageBox.Show($"Already added {line} records, continue?", "SQL Query", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                                        break;
                                }
                            }

                            Cursor = Cursors.Default;
                            Application.UseWaitCursor = false;
                        }
                    }
                }
                if (dgvSimple.Rows.Count > 0)
                    AddToSearchHistory(cbSimpleSQL, cbSimpleSQL.Text);
            }
            catch (Exception x)
            {
                Cursor = Cursors.Default;
                Application.UseWaitCursor = false;
                MessageBox.Show(x.Message, "Run MySQL Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void TSimpleSQL_TextChanged(object sender, EventArgs e)
        {
            btnSimpleSQL.Enabled = (cbSimpleSQL.Text != string.Empty);
        }

        private void TSimpleSQL_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && (btnSimpleSQL.Enabled))
                BtnSimpleSQL_Click(null, null);
        }

        private string FunctionTypeToTableName(string funcName)
        {
            if (funcName == "DoodadFuncNaviOpenMailbox")
                return "doodad_func_navi_open_mailboxes";
            var tableName = string.Empty;
            var rest = funcName;
            while (rest.Length > 0)
            {
                var c = rest.Substring(0, 1);
                if (c != c.ToLower()) // decaptialize the name, and put a underscore in front
                {
                    tableName += "_" + c.ToLower();
                }
                else
                {
                    tableName += c;
                }

                rest = rest.Substring(1);
            }

            tableName = tableName.Trim('_') + "s"; // remove excess underscores and add a "s"
            return tableName;
        }

        private List<Dictionary<string, string>> GetCustomTableValues(string tableName, string indexName, string searchId)
        {
            var res = new List<Dictionary<string, string>>();

            var dbFile = AllTableNames.GetValueOrDefault(tableName) ?? string.Empty;
            if (string.IsNullOrWhiteSpace(dbFile) || !File.Exists(dbFile))
            {
                return res;
            }

            SQLite.SQLiteFileName = dbFile;

            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    try
                    {
                        command.CommandText = "SELECT * FROM " + tableName + " WHERE " + indexName + "=" + searchId;
                        command.Prepare();
                        using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                        {
                            var columnNames = reader.GetColumnNames();

                            while (reader.Read())
                            {
                                var resRow = new Dictionary<string, string>();
                                int c = 0;
                                foreach (var col in columnNames)
                                {
                                    if (col != indexName) // skip the index field
                                    {
                                        if (reader.IsDBNull(col))
                                            resRow.Add(col, "<null>");
                                        else
                                            resRow.Add(col, reader.GetValue(col).ToString());
                                    }

                                    c++;
                                }

                                res.Add(resRow);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        var eRow = new Dictionary<string, string>();
                        eRow.Add("Exception", ex.Message);
                        res.Add(eRow);
                    }
                }
            }

            return res;
        }

        private bool IsCustomPropertyEmpty(string value)
        {
            return (string.IsNullOrWhiteSpace(value) || (value == "0") || (value == "<null>") || (value == "f"));
        }

        private TreeNode AddCustomPropertyNodeForLootPack(long packId, TreeNode rootNode)
        {
            var res = new TreeNodeWithInfo();
            var setCustomIcon = -1;
            var nodeText = $"loot_pack_id: {packId}";
            res.Text = nodeText;
            res.ForeColor = Color.Yellow;

            var loots = AaDb.DbLoots.Values.Where(l => l.LootPackId == packId).OrderBy(l => l.Group).ToList();
            if (!loots.Any())
            {
                res.Text += " missing";
                res.ForeColor = Color.Red;
                rootNode.Nodes.Add(res);
                return res;
            }
            var showGroups = loots.DistinctBy(x => x.Group).Count() > 1;
            var groupNodes = new Dictionary<long, TreeNode>();

            foreach (var loot in loots)
            {
                var groupDropRate = string.Empty;
                var isInGroup = AaDb.DbLootGroups.Values.FirstOrDefault(x => x.PackId == packId && x.GroupNo == loot.Group);
                var allLootsInGroup = loots.Where(l => l.Group == loot.Group);
                var totalWeightOfGroup = allLootsInGroup.Count() > 1 ? allLootsInGroup.Sum(x => x.DropRate) : 0;
                if (isInGroup != null)
                {
                    var groupRate = isInGroup.DropRate / 1_000_000f * 100f;
                    if (groupRate > 100f)
                    {
                        groupDropRate = $"Group {isInGroup.GroupNo} @ 100%+ ({isInGroup.DropRate})";
                    }
                    else if (isInGroup.DropRate == 1)
                    {
                        groupDropRate = $"Group {isInGroup.GroupNo} always ({isInGroup.DropRate})";
                    }
                    else if (groupRate < 5f)
                    {
                        groupDropRate = $"Group {isInGroup.GroupNo}@ {groupRate:F2}% ({isInGroup.DropRate})";
                    }
                    else
                    {
                        groupDropRate = $"Group {isInGroup.GroupNo}@ {groupRate:F0}% ({isInGroup.DropRate})";
                    }

                    var isVocationGroup = AaDb.DbLootActAbilityGroups.Values.FirstOrDefault(x =>
                        x.LootPackId == packId && x.LootGroupId == isInGroup.GroupNo);
                    if (isVocationGroup != null)
                    {
                        groupDropRate += $" VocationDice ({isVocationGroup.MinDice}~{isVocationGroup.MaxDice}) ";
                    }
                }

                TreeNode nodeForGroup = res;
                if (showGroups)
                {
                    if (groupNodes.TryGetValue(loot.Group, out var oldNode))
                    {
                        nodeForGroup = oldNode;
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(groupDropRate))
                            groupDropRate = "No group";
                        nodeForGroup = res.Nodes.Add(groupDropRate);
                        groupNodes.Add(loot.Group, nodeForGroup);
                    }
                }


                var dropRate = "??%";
                var itemNode = new TreeNodeWithInfo();
                itemNode.Text = loot.ItemId.ToString();
                if (AaDb.DbItems.TryGetValue(loot.ItemId, out var lItem))
                {
                    itemNode.targetTabPage = tpItems;
                    itemNode.targetSearchBox = cbItemSearch;
                    itemNode.targetSearchText = lItem.NameLocalized;
                    itemNode.targetSearchButton = btnItemSearch;
                    itemNode.ForeColor = Color.WhiteSmoke;
                    itemNode.Text += " - " + lItem.NameLocalized;
                    itemNode.SelectedImageIndex = itemNode.ImageIndex = IconIdToLabel(lItem.IconId, null);
                }
                else
                {
                    itemNode.Text = " - ???";
                    itemNode.ForeColor = Color.Red;
                }

                var itemRate = 0f;
                if (totalWeightOfGroup > 0)
                {
                    // Weighted
                    itemRate = (float)loot.DropRate / (float)totalWeightOfGroup * 100f;
                }
                else
                {
                    // Raw
                    itemRate = (float)loot.DropRate / 10_000_000f * 100f;
                }

                if (loot.AlwaysDrop)
                {
                    dropRate = "Always";
                }
                else if (itemRate > 100f)
                {
                    dropRate = "100%+";
                }
                else if (itemRate < 5f) 
                {
                    itemRate = MathF.Min(itemRate, 100f);
                    dropRate = $"{itemRate:F2}%";
                }
                else
                {
                    itemRate = MathF.Min(itemRate, 100f);
                    dropRate = $"{itemRate:F0}%";
                }

                var amount = string.Empty;
                if (loot.ItemId == 500)
                {
                    if (loot.MaxAmount > loot.MinAmount)
                    {
                        amount = $" x {CopperToValuta(loot.MinAmount)} ~ {CopperToValuta(loot.MaxAmount)}";
                    }
                    else if (loot.MaxAmount > 1)
                    {
                        amount = $" x {CopperToValuta(loot.MaxAmount)}";
                    }
                }
                else
                {
                    if (loot.MaxAmount > loot.MinAmount)
                    {
                        amount = $" x {loot.MinAmount}~{loot.MaxAmount}";
                    }
                    else if (loot.MaxAmount > 1)
                    {
                        amount = $" x {loot.MaxAmount}";
                    }
                }

                itemNode.Text = dropRate + " " + itemNode.Text + amount;

                nodeForGroup.Nodes.Add(itemNode);

                if (isInGroup != null && isInGroup.ItemGradeDistributionId > 0)
                {
                    if (AaDb.DbItemGradeDistributions.TryGetValue(isInGroup.ItemGradeDistributionId, out var gradeDistribution))
                    {
                        var totalWeight = gradeDistribution.Weights.Values.Sum();
                        foreach (var (gradeId, weight) in gradeDistribution.Weights)
                        {
                            if (weight <= 0)
                                continue;
                            var gradeRate = (float)weight / (float)totalWeight * 100f;
                            if (AaDb.DbItemGrades.TryGetValue(gradeId, out var grade))
                            {
                                var gradeNode = itemNode.Nodes.Add($"Grade: {grade.NameLocalized} ({gradeId}) @ {gradeRate:F0}%");
                                gradeNode.ForeColor = grade.ColorArgb;
                                gradeNode.SelectedImageIndex = gradeNode.ImageIndex = IconIdToLabel(grade.IconId, null);
                            }
                            else
                            {
                                itemNode.Nodes.Add($"Grade: Invalid ({gradeId}) ").ForeColor = Color.Red;
                            }

                        }
                        
                    }
                    else
                    {
                        itemNode.Nodes.Add("Unknown grade distribution").ForeColor = Color.Red;
                    }
                    
                }
                else if (loot.GradeId > 0)
                {
                    var grade = AaDb.DbItemGrades.GetValueOrDefault(loot.GradeId);

                    var gradeText = $"Grade: {grade?.NameLocalized ?? ""} ({loot.GradeId})";
                    if (grade != null)
                    {
                        var gradeNode = itemNode.Nodes.Add(gradeText);
                        gradeNode.ForeColor = grade.ColorArgb;
                        gradeNode.SelectedImageIndex = gradeNode.ImageIndex = IconIdToLabel(grade.IconId, null);
                    }
                    else
                    {
                        itemNode.Nodes.Add(gradeText);
                    }
                }
            }

            rootNode.Nodes.Add(res);
            if ((rootNode?.TreeView?.ImageList != null) && (setCustomIcon >= 0))
            {
                res.ImageIndex = setCustomIcon;
                res.SelectedImageIndex = setCustomIcon;
            }

            return res;
        }

        private TreeNode AddCustomPropertyNode(string key, string value, bool hideNull, TreeNode rootNode)
        {
            if (hideNull && IsCustomPropertyEmpty(value))
                return null;

            // First try to detect localizable entries
            var rootSplit = rootNode.Text.Split(' ');
            var rootTypeName = string.Empty;
            long rootTypeKey = 0;
            if ((rootSplit.Length >= 4) && (rootSplit[1] == "(") && (rootSplit[3] == ")")) // name ( id )
            {
                rootTypeName = FunctionTypeToTableName(rootSplit[0]);
                if (long.TryParse(rootSplit[2], out var v))
                    rootTypeKey = v;
            }

            var localizedValue = value;
            if (!string.IsNullOrWhiteSpace(rootTypeName) && (rootTypeKey > 0))
            {
                localizedValue = AaDb.GetTranslationById(rootTypeKey, rootTypeName, key, "");
            }

            if (!string.IsNullOrWhiteSpace(localizedValue) && (localizedValue != value))
            {
                var localizedNode = new TreeNodeWithInfo();
                localizedNode.targetTabPage = tpLocalizer;
                localizedNode.targetTextBox = tSearchLocalized;
                localizedNode.targetSearchText = rootTypeName + " " + key + " =" + rootTypeKey + "=";
                localizedNode.targetSearchButton = null;
                localizedNode.Text = key + ": " + localizedValue;
                localizedNode.ForeColor = Color.LightYellow;
                rootNode.Nodes.Add(localizedNode);
                return localizedNode;
            }

            var nodeText = key + ": " + value;
            if (!long.TryParse(value, out var val))
            {
                var normalNode = rootNode.Nodes.Add(nodeText);
                return normalNode;
            }

            var res = new TreeNodeWithInfo();
            var setCustomIcon = -1;

            if (key.EndsWith("delay") || key.EndsWith("_time") || key.EndsWith("duration") || key.EndsWith("custom_gcd"))
            {
                if (long.TryParse(value, out var delayVal))
                {
                    nodeText += " - " + MSToString(delayVal);
                    res.ForeColor = Color.LightSeaGreen;
                }
            }
            else if (key.EndsWith("next_phase") && (AaDb.DbDoodadFuncGroups.TryGetValue(val, out var nextPhase)))
            {
                var s = string.IsNullOrWhiteSpace(nextPhase.NameLocalized) ? nextPhase.Name : nextPhase.NameLocalized;
                if (!string.IsNullOrEmpty(s))
                    nodeText += " - " + s;
                res.ForeColor = Color.WhiteSmoke;
            }
            else if (key.EndsWith("skill_id") && (AaDb.DbSkills.TryGetValue(val, out var skill)))
            {
                res.targetTabPage = tpSkills;
                res.targetSearchBox = cbSkillSearch;
                // res.targetSearchText = skill.nameLocalized;
                res.targetSearchText = skill.Id.ToString();
                res.targetSearchButton = btnSkillSearch;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + skill.NameLocalized;
                setCustomIcon = IconIdToLabel(skill.IconId, null);
            }
            else if (key.EndsWith("item_id") && (AaDb.DbItems.TryGetValue(val, out var item)))
            {
                res.targetTabPage = tpItems;
                res.targetSearchBox = cbItemSearch;
                // res.targetSearchText = item.nameLocalized;
                res.targetSearchText = item.Id.ToString();
                res.targetSearchButton = btnItemSearch;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + item.NameLocalized;
                setCustomIcon = IconIdToLabel(item.IconId, null);
            }
            else if (key.EndsWith("doodad_id") && (AaDb.DbDoodadAlmighties.TryGetValue(val, out var doodad)))
            {
                res.targetTabPage = tpDoodads;
                res.targetSearchBox = cbSearchDoodads;
                // res.targetSearchText = doodad.nameLocalized;
                res.targetSearchText = doodad.Id.ToString();
                res.targetSearchButton = btnSearchDoodads;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + doodad.NameLocalized;
            }
            else if (key.EndsWith("npc_id") && (AaDb.DbNpCs.TryGetValue(val, out var npc)))
            {
                res.targetTabPage = tpNPCs;
                res.targetSearchBox = cbSearchNPC;
                // res.targetSearchText = npc.nameLocalized;
                res.targetSearchText = npc.Id.ToString();
                res.targetSearchButton = btnSearchNPC;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + npc.NameLocalized;
            }
            /*
            else if (key.EndsWith("npc_group_id"))
            {
                var npcs = AADB.DB_Quest_Monster_Npcs.Values.Where(x => x.quest_monster_group_id == val);
                if (npcs.Any())
                {
                    foreach (var npcFromGroup in npcs)
                    {
                        AddCustomPropertyNode("npc_id", npcFromGroup.npc_id.ToString(), false, res);
                    }
                }
                res.ForeColor = Color.Yellow;
            }
            */
            else if (key.EndsWith("slave_id") && (AaDb.DbSlaves.TryGetValue(val, out var slave)))
            {
                res.targetTabPage = tpNPCs;
                // res.targetSearchBox = slave.id.ToString();
                res.targetSearchText = slave.Id.ToString();
                res.targetSearchButton = btnSearchSlave;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + slave.NameLocalized;
            }
            else if (key.EndsWith("buff_id") && (AaDb.DbBuffs.TryGetValue(val, out var buff)))
            {
                res.targetTabPage = tpBuffs;
                res.targetSearchBox = cbSearchBuffs;
                // res.targetSearchText = buff.nameLocalized;
                res.targetSearchText = buff.Id.ToString();
                res.targetSearchButton = btnSearchBuffs;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + buff.NameLocalized;
                setCustomIcon = IconIdToLabel(buff.IconId, null);
            }
            else if (key.EndsWith("zone_id"))
            {
                if (AaDb.DbZones.TryGetValue(val, out var zone))
                {
                    res.targetTabPage = tpZones;
                    res.targetTextBox = tZonesSearch;
                    res.targetSearchText = zone.DisplayTextLocalized;
                    res.targetSearchButton = btnSearchZones;
                    res.ForeColor = Color.WhiteSmoke;
                    nodeText += " - " + zone.DisplayTextLocalized + " (id)";
                }

                var zoneByKey = AaDb.DbZones.Values.FirstOrDefault(z => z.ZoneKey == val);
                if (zoneByKey != null)
                {
                    res.targetTabPage = tpZones;
                    res.targetTextBox = tZonesSearch;
                    res.targetSearchText = zoneByKey.DisplayTextLocalized;
                    res.targetSearchButton = btnSearchZones;
                    res.ForeColor = Color.WhiteSmoke;
                    if (zone != null)
                        nodeText += " or ";
                    else
                        nodeText += " - ";
                    nodeText += zoneByKey.DisplayTextLocalized + " (key)";
                }
                // Some quest related entries use "zone_id" when they instead mean "zone_group_id"
                // So we add the 2nd part here.
                if (AaDb.DbZoneGroups.TryGetValue(val, out var zoneGroup))
                {
                    res.targetTabPage = tpZones;
                    res.targetTextBox = tZonesSearch;
                    res.targetSearchText = zoneGroup.DisplayTextLocalized;
                    res.targetSearchButton = btnSearchZones;
                    res.ForeColor = Color.WhiteSmoke;
                    if ((zone != null) || (zoneByKey != null))
                        nodeText += " or ";
                    else
                        nodeText += " - ";
                    nodeText += zoneGroup.DisplayTextLocalized + " (group)";
                }
            }
            else if (key.EndsWith("zone_group_id") && (AaDb.DbZoneGroups.TryGetValue(val, out var zoneGroup)))
            {
                res.targetTabPage = tpZones;
                res.targetTextBox = tZonesSearch;
                res.targetSearchText = zoneGroup.DisplayTextLocalized;
                res.targetSearchButton = btnSearchZones;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + zoneGroup.DisplayTextLocalized;
            }
            else if (key.EndsWith("item_category_id") &&
                     (AaDb.DbItemsCategories.TryGetValue(val, out var itemCategory)))
            {
                res.targetTabPage = tpItems;
                res.targetSearchBox = cbItemSearch;
                res.targetSearchText = itemCategory.NameLocalized;
                res.targetSearchButton = btnItemSearch;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + itemCategory.NameLocalized;
            }
            else if (key.EndsWith("tag_id") && (AaDb.DbTags.TryGetValue(val, out var tagId)))
            {
                res.targetTabPage = tpTags;
                res.targetTextBox = tSearchTags;
                // res.targetSearchText = tagId.nameLocalized;
                res.targetSearchText = tagId.Id.ToString();
                res.targetSearchButton = btnSearchTags;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + tagId.NameLocalized;
            }
            else if (key.EndsWith("sphere_id") && (AaDb.DbSpheres.TryGetValue(val, out var sphere)))
            {
                res.targetTabPage = tpSpheres;
                res.targetSearchBox = CbSearchSpheres;
                // res.targetSearchText = npc.nameLocalized;
                res.targetSearchText = sphere.Id.ToString();
                res.targetSearchButton = BtnSearchSpheres;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + sphere.Name;
            }
            else if (key.EndsWith("wi_id"))
            {
                try
                {
                    res.ForeColor = Color.LawnGreen;
                    nodeText += " - " + ((WorldInteractionType)Enum.Parse(typeof(WorldInteractionType), value)).ToString();
                }
                catch
                {
                    nodeText += " - ???";
                    res.ForeColor = Color.Red;
                }
            }
            else if (key.EndsWith("special_effect_type_id"))
            {
                try
                {
                    nodeText += " - " + ((SpecialType)Enum.Parse(typeof(SpecialType), value)).ToString();
                    res.ForeColor = Color.LawnGreen;
                }
                catch
                {
                    nodeText += " - ???";
                    res.ForeColor = Color.Red;
                }
            }
            else if (key.EndsWith("loot_pack_id"))
            {
                var lootNode = AddCustomPropertyNodeForLootPack(val, rootNode);
                /*
                var lootNode = new TreeNodeWithInfo();
                lootNode.Text = key + ": " + val;
                lootNode.ForeColor = Color.Yellow;

                var loots = AaDb.DbLoots.Where(l => l.Value.LootPackId == val).OrderBy(l => l.Value.Group);
                foreach (var loot in loots)
                {
                    var itemNode = new TreeNodeWithInfo();
                    itemNode.Text = loot.Value.ItemId.ToString();
                    if (AaDb.DbItems.TryGetValue(loot.Value.ItemId, out var lItem))
                    {
                        itemNode.targetTabPage = tpItems;
                        itemNode.targetSearchBox = cbItemSearch;
                        itemNode.targetSearchText = lItem.NameLocalized;
                        itemNode.targetSearchButton = btnItemSearch;
                        itemNode.ForeColor = Color.WhiteSmoke;
                        itemNode.Text += " - " + lItem.NameLocalized;
                        itemNode.SelectedImageIndex = itemNode.ImageIndex = IconIdToLabel(lItem.IconId, null);
                    }
                    else
                    {
                        itemNode.Text = " - ???";
                        itemNode.ForeColor = Color.Red;
                    }

                    lootNode.Nodes.Add(itemNode);
                }

                rootNode.Nodes.Add(lootNode);
                */
                return lootNode;
            }
            else if (key.EndsWith("spawner_id"))
            {
                var spawnNode = new TreeNodeWithInfo();
                spawnNode.Text = key + ": " + val;
                spawnNode.ForeColor = Color.Yellow;
                rootNode.Nodes.Add(spawnNode);

                var spawns = AaDb.DbNpcSpawnerNpcs.Values.Where(x => x.NpcSpawnerId == val);
                foreach (var npcSpawnerNpc in spawns)
                {
                    if (npcSpawnerNpc.MemberType == "Npc")
                        AddCustomPropertyNode("npc_id", npcSpawnerNpc.MemberId.ToString(), false, spawnNode);
                    if (npcSpawnerNpc.MemberType == "NpcGroup")
                        AddCustomPropertyNode("npc_group_id", npcSpawnerNpc.MemberId.ToString(), false, spawnNode);
                }

                return spawnNode;
            }
            else if (key.StartsWith("ability_id"))
            {
                try
                {
                    res.ForeColor = Color.LawnGreen;
                    nodeText += " - " + ((AbilityType)Enum.Parse(typeof(AbilityType), value)).ToString();
                }
                catch
                {
                    nodeText += " - ???";
                    res.ForeColor = Color.Red;
                }
            }
            else if ((key.EndsWith("icon_id") || key.EndsWith("icon1_id") || key.EndsWith("icon2_id")) && AaDb.DbIcons.TryGetValue(val, out var _))
            {
                setCustomIcon = IconIdToLabel(val, null);
            }
            else if (key.EndsWith("quest_id") && (AaDb.DbQuestContexts.TryGetValue(val, out var quest)))
            {
                res.targetTabPage = tpQuests;
                res.targetSearchBox = cbQuestSearch;
                // res.targetSearchText = buff.nameLocalized;
                res.targetSearchText = quest.Id.ToString();
                res.targetSearchButton = btnQuestsSearch;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + quest.NameLocalized;
            }

            res.Text = nodeText;

            rootNode.Nodes.Add(res);
            if ((rootNode?.TreeView?.ImageList != null) && (setCustomIcon >= 0))
            {
                res.ImageIndex = setCustomIcon;
                res.SelectedImageIndex = setCustomIcon;
            }

            return res;
        }

        private void tSearchLocalized_TextChanged(object sender, EventArgs e)
        {
            dgvLocalized.Rows.Clear();
            var sTexts = tSearchLocalized.Text.ToLower().Split(' ');
            if (sTexts.Length <= 0)
                return;

            var count = 0;
            foreach (var i in AaDb.DbTranslations)
            {
                var translation = i.Value;
                var tSearchString = "=" + translation.Table.ToLower() + "=" + translation.Field.ToLower() + "=" +
                                    translation.Idx.ToString() + "=" + translation.Value.ToLower() + "=";
                var searchOK = true;
                foreach (var s in sTexts)
                    if (!tSearchString.Contains(s))
                    {
                        searchOK = false;
                        break;
                    }

                if (searchOK)
                {
                    int line = dgvLocalized.Rows.Add();
                    var row = dgvLocalized.Rows[line];
                    row.Cells[0].Value = translation.Table;
                    row.Cells[1].Value = translation.Field;
                    var idxString = translation.Idx.ToString();
                    if (translation.Table == "ui_texts")
                    {
                        if (AaDb.DbUiTexts.TryGetValue(translation.Idx, out var uiText))
                        {
                            idxString += $" ({uiText.Key} : {uiText.CategoryId})";
                        }
                    }
                    row.Cells[2].Value = idxString;
                    row.Cells[3].Value = translation.Value;
                    count++;
                }

                if (count > 50)
                    break;
            }
        }

        private void TpLocalizerEnter(object sender, EventArgs e)
        {
            if (tSearchLocalized.Text == string.Empty)
                tSearchLocalized.Text = "doodad name =320=";
        }

        private void tFilterTables_TextChanged(object sender, EventArgs e)
        {
            var lastSelected = lbTableNames.SelectedIndex >= 0 ? lbTableNames.Text : "";
            var tableNames = AllTableNames.Keys.ToList();
            tableNames.Sort();
            if (tFilterTables.Text == string.Empty)
            {
                lbTableNames.Items.Clear();
                foreach (var s in tableNames)
                    lbTableNames.Items.Add(s);
            }
            else
            {
                lbTableNames.Items.Clear();
                foreach (var s in tableNames)
                {
                    if (s.ToLower().Contains(tFilterTables.Text.ToLower()))
                    {
                        lbTableNames.Items.Add(s);
                        if (s == lastSelected)
                            lbTableNames.SelectedIndex = lbTableNames.Items.Count - 1;
                    }
                }
            }
        }

        private void ProcessNodeInfoDoubleClick(TreeNode node)
        {
            if (node == null)
                return;
            CopyToClipBoard(node.Text);
            if (node is TreeNodeWithInfo info)
            {
                if (info.targetTabPage != null)
                    tcViewer.SelectedTab = info.targetTabPage;
                if (info.targetTextBox != null)
                    info.targetTextBox.Text = info.targetSearchText;
                if (info.targetSearchBox != null)
                    info.targetSearchBox.Text = info.targetSearchText;
                if (info.targetSearchButton != null)
                {
                    info.targetSearchButton.Enabled = true;
                    info.targetSearchButton.PerformClick();
                }

                if (!string.IsNullOrWhiteSpace(info.targetWorldName) && info.targetPosition != Vector3.Zero)
                {
                    PrepareWorldXml(false);
                    var map = MapViewForm.GetMap();
                    map.Show();
                    map.cbInstanceSelect.Text = info.targetWorldName;

                    if (map.GetPoICount() > 0 && MessageBox.Show("Keep PoI's ?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                        map.ClearPoI();
                    var tp = info.targetPosition;
                    map.AddPoI(tp.X, tp.Y, tp.Z, info.targetSearchText, Color.Aquamarine, info.targetRadius, "", 0, null);

                    map.FocusAll(true, false, false);
                    map.BringToFront();
                }
            }
        }

        private string TreeViewToString(TreeNodeCollection treeView, int depth = 0)
        {
            var res = string.Empty;
            foreach (TreeNode node in treeView)
            {
                var spacer = string.Empty.PadRight(depth * 2, ' ');
                if (depth > 0)
                    spacer += (depth % 2 == 0) ? "- " : "* ";
                res += spacer + $"{node.Text}\r\n";
                res += TreeViewToString(node.Nodes, depth + 1);
            }
            return res;
        }

        private void AddToSearchHistory(ComboBox searchBox, string searchString)
        {
            var oldText = searchBox.Text;
            var s = searchString.ToLower();
            for (int i = searchBox.Items.Count - 1; i >= 0; i--)
            {
                string item = (string)searchBox.Items[i];
                if (item.ToLower() == s)
                    searchBox.Items.RemoveAt(i);
            }
            searchBox.Items.Insert(0, oldText);

            // Put a artificial limit
            while (searchBox.Items.Count > 50)
                searchBox.Items.RemoveAt(searchBox.Items.Count - 1);

            searchBox.Text = oldText;
        }

        private void LoadHistory(string savedHistoryString, ComboBox comboBox)
        {
            var lines = savedHistoryString.Split(Environment.NewLine);
            comboBox.Items.Clear();
            foreach (var line in lines)
                if (!string.IsNullOrWhiteSpace(line))
                    comboBox.Items.Add(line);
        }

        private string CreateHistory(ComboBox comboBox)
        {
            var res = new List<string>();
            foreach (string line in comboBox.Items)
                if (!string.IsNullOrWhiteSpace(line))
                    res.Add(line);
            return string.Join(Environment.NewLine, res);
        }

        private void CbSearchSpheres_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSearchSpheres_Click(null, null);
            }
        }

        private void BtnSearchSpheres_Click(object sender, EventArgs e)
        {
            var searchText = CbSearchSpheres.Text.ToLower();
            if (searchText == string.Empty)
                return;
            if (!long.TryParse(searchText, out var searchId))
                searchId = -1;

            var first = true;
            DgvSpheres.Rows.Clear();
            foreach (var dbSphere in AaDb.DbSpheres.Values)
            {
                if (
                    (dbSphere.Id != searchId) &&
                    (dbSphere.SphereDetailId != searchId) &&
                    (!dbSphere.Name.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)) &&
                    (!dbSphere.SphereDetailType.Contains(searchText, StringComparison.InvariantCultureIgnoreCase))
                    )
                    continue;

                var line = DgvSpheres.Rows.Add();
                var row = DgvSpheres.Rows[line];

                row.Cells[0].Value = dbSphere.Id.ToString();
                row.Cells[1].Value = dbSphere.Name;
                row.Cells[2].Value = dbSphere.SphereDetailType;
                row.Cells[3].Value = dbSphere.SphereDetailId.ToString();
                row.Cells[4].Value = dbSphere.EnterOrLeave.ToString();

                if (first)
                {
                    first = false;
                    ShowDbSphere(dbSphere.Id);
                    ShowSelectedData("spheres", "id = " + dbSphere.Id.ToString(), "id ASC");
                }

                if (DgvSpheres.Rows.Count > 250)
                {
                    MessageBox.Show("Results truncated to 250 results, please be more specific in the search");
                    break;
                }
            }
            if (DgvSpheres.Rows.Count > 0)
                AddToSearchHistory(CbSearchSpheres, CbSearchSpheres.Text);
        }

        private void ShowDbSphere(long id)
        {
            TvSpheres.Nodes.Clear();
            if (!AaDb.DbSpheres.TryGetValue(id, out var sphere))
                return;

            var rootNode = TvSpheres.Nodes.Add($"{sphere.Id} - {sphere.Name}");
            AddCustomPropertyNode("enter_or_leave", sphere.EnterOrLeave.ToString(), false, rootNode);
            AddCustomPropertyNode("trigger_condition_id", sphere.TriggerConditionId.ToString(), false, rootNode);
            AddCustomPropertyNode("trigger_condition_time", sphere.TriggerConditionTime.ToString(), false, rootNode);
            AddCustomPropertyNode("team_msg", sphere.TeamMsg, false, rootNode);

            // category_id seems to be Quest Category Id?
            var cat = AaDb.DbQuestCategories.GetValueOrDefault(sphere.CategoryId);
            var catName = cat?.NameLocalized ?? "???";
            rootNode.Nodes.Add($"category_id: {sphere.CategoryId} - {catName}");
            // AddCustomPropertyNode("category_id", sphere.category_id.ToString(), false, rootNode);
            // AddCustomPropertyNode("or_unit_reqs", sphere.or_unit_reqs.ToString(), false, rootNode);
            AddCustomPropertyNode("is_personal_msg", sphere.IsPersonalMsg.ToString(), false, rootNode);

            var requires = GetSphereRequirements(sphere.Id);
            var reqNode = AddUnitRequirementNode(requires, sphere.OrUnitReqs, TvSpheres.Nodes);

            var questList = new List<long>();

            var detailNode = rootNode.Nodes.Add($"{sphere.SphereDetailType} - {sphere.SphereDetailId}");
            var detailsTableName = FunctionTypeToTableName(sphere.SphereDetailType);
            var effectValuesList = GetCustomTableValues(detailsTableName, "id", sphere.SphereDetailId.ToString());
            foreach (var effectValues in effectValuesList)
                foreach (var effectValue in effectValues)
                {
                    var thisNode = AddCustomPropertyNode(effectValue.Key, effectValue.Value, false, detailNode);
                    if (thisNode == null)
                        continue;

                    if (thisNode.ImageIndex <= 0) // override default blank icon with blue !
                    {
                        thisNode.ImageIndex = 4;
                        thisNode.SelectedImageIndex = 4;
                    }

                    if (detailsTableName == "sphere_quests" && effectValue.Key == "quest_id")
                    {
                        questList.Add(long.Parse(effectValue.Value));
                    }
                }

            if (questList.Count > 0)
            {
                var questNode = TvSpheres.Nodes.Add("Quests Sign Spheres");
                foreach (var questId in questList)
                {
                    var quests = AaDb.PakQuestSignSpheres.Where(x => x.QuestId == questId).ToArray();
                    foreach (var questSphereEntry in quests)
                    {
                        var qNode = AddCustomPropertyNode("quest_id", questId.ToString(), false, questNode);
                        AddCustomPropertyNode("component_id", questSphereEntry.ComponentId.ToString(), false, qNode);
                        qNode.Nodes.Add($"world_id: {questSphereEntry.WorldId}");

                        // var zoneOffset = Vector3.Zero;
                        var zoneNode = new TreeNodeWithInfo();
                        qNode.Nodes.Add(zoneNode);
                        zoneNode.Text = $"zone_id: {questSphereEntry.ZoneKey}";
                        var zoneByKey = AaDb.DbZones.Values.FirstOrDefault(z => z.ZoneKey == questSphereEntry.ZoneKey);
                        if (zoneByKey != null)
                        {
                            zoneNode.targetTabPage = tpZones;
                            zoneNode.targetTextBox = tZonesSearch;
                            zoneNode.targetSearchText = zoneByKey.DisplayTextLocalized;
                            zoneNode.targetSearchButton = btnSearchZones;
                            zoneNode.ForeColor = Color.WhiteSmoke;
                            zoneNode.Text += " - " + zoneByKey.DisplayTextLocalized + " (key)";

                            // var zoneGroup = AADB.DB_Zone_Groups.GetValueOrDefault(zoneByKey.group_id);
                            // if (zoneGroup != null)
                            //     zoneOffset = new Vector3(zoneGroup.PosAndSize.X, zoneGroup.PosAndSize.Y, 0f);
                        }

                        var zonePosNode = new TreeNodeWithInfo();
                        zonePosNode.Text = $"Position: {questSphereEntry.X} , {questSphereEntry.Y} , {questSphereEntry.Z}";
                        zonePosNode.targetPosition = new Vector3(questSphereEntry.X, questSphereEntry.Y, questSphereEntry.Z);
                        zonePosNode.targetRadius = questSphereEntry.Radius;
                        zonePosNode.targetWorldName = questSphereEntry.WorldId;
                        zonePosNode.targetSearchText = rootNode.Text;
                        zonePosNode.ForeColor = Color.Aquamarine;
                        qNode.Nodes.Add(zonePosNode);

                        qNode.Nodes.Add($"Radius: {questSphereEntry.Radius}");
                    }
                }

                if (questNode.Nodes.Count <= 0)
                    TvSpheres.Nodes.Remove(questNode);
            }

            TvSpheres.ExpandAll();
        }

        private void DgvSpheres_SelectionChanged(object sender, EventArgs e)
        {
            if (DgvSpheres.SelectedRows.Count <= 0)
                return;
            var row = DgvSpheres.SelectedRows[0];
            if (row.Cells.Count <= 0)
                return;

            var val = row.Cells[0].Value;
            if (val == null)
                return;

            var sid = long.Parse(val.ToString());
            ShowDbSphere(sid);
            ShowSelectedData("spheres", "id = " + sid.ToString(), "id ASC");
        }

        private void TvSpheres_DoubleClick(object sender, EventArgs e)
        {
            ProcessNodeInfoDoubleClick(TvSpheres.SelectedNode);
        }

        private void CbSearchSpheres_TextChanged(object sender, EventArgs e)
        {
            BtnSearchSpheres.Enabled = !string.IsNullOrWhiteSpace(CbSearchSpheres.Text);
        }

        public TreeNode AddUnitRequirementNode(List<GameUnitReqs> requirements, bool orUnitReqs, TreeNodeCollection root)
        {
            if (requirements.Any())
            {
                var reqNode = root.Add($"Requires {(orUnitReqs ? "Any" : "All")} of");
                reqNode.ForeColor = Color.Aqua;
                foreach (var req in requirements)
                {
                    reqNode.Nodes.Add($"kind_id: {req.KindId}, value1: {req.Value1}, value2: {req.Value2}");
                }
                reqNode.ExpandAll();
                return reqNode;
            }

            return null;
        }

        private void MMFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MMFileTables_Click(object sender, EventArgs e)
        {
            tcViewer.SelectedTab = tpTables;
        }

        private void MMSelectedData_Click(object sender, EventArgs e)
        {
            tcViewer.SelectedTab = tpCurrentRecord;
        }

        private void MMLocalizer_Click(object sender, EventArgs e)
        {
            tcViewer.SelectedTab = tpLocalizer;
        }

        private void MMGameObjectsNpcs_Click(object sender, EventArgs e)
        {
            tcViewer.SelectedTab = tpNPCs;
        }

        private void MMGameObjectsVehicles_Click(object sender, EventArgs e)
        {
            tcViewer.SelectedTab = tpSlaves;
        }

        private void MMSystemItems_Click(object sender, EventArgs e)
        {
            tcViewer.SelectedTab = tpItems;
        }

        private void MMSystemBuffs_Click(object sender, EventArgs e)
        {
            tcViewer.SelectedTab = tpBuffs;
        }

        private void MMSystemMaps_Click(object sender, EventArgs e)
        {
            tcViewer.SelectedTab = tpMap;
        }

        private void MMSystemLoot_Click(object sender, EventArgs e)
        {
            tcViewer.SelectedTab = tpLoot;
        }

        private void MMSystemSchedule_Click(object sender, EventArgs e)
        {
            tcViewer.SelectedTab = tpSchedules;
        }

        private void MMSystemTags_Click(object sender, EventArgs e)
        {
            tcViewer.SelectedTab = tpTags;
        }

        private void MMSystemTrades_Click(object sender, EventArgs e)
        {
            tcViewer.SelectedTab = tpTrade;
        }

        private void MMSystemZones_Click(object sender, EventArgs e)
        {
            tcViewer.SelectedTab = tpZones;
        }

        private void MMSystemSkills_Click(object sender, EventArgs e)
        {
            tcViewer.SelectedTab = tpSkills;
        }

        private void MMSystemFactions_Click(object sender, EventArgs e)
        {
            tcViewer.SelectedTab = tpFactions;
        }

        private void MMSplitter_Click(object sender, EventArgs e)
        {
            tcViewer.SelectedTab = tpV1;
        }

        private void tcViewer_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTitleBar();
            if (SkipTabHistory == false && tcViewer.SelectedTab != null)
            {
                if (TabHistory.Count > TabHistoryIndex)
                    TabHistory.RemoveRange(TabHistoryIndex, TabHistory.Count - TabHistoryIndex - 1);

                TabHistory.Add(tcViewer.SelectedTab);
                TabHistoryIndex = TabHistory.Count - 1;
            }

            foreach (ToolStripItem stripItem in TBMain.Items)
            {
                // stripItem.BackColor = (stripItem.Text == tcViewer.SelectedTab?.Text) ? Color.Red : SystemColors.Control ;
                stripItem.Enabled = (stripItem.Text != tcViewer.SelectedTab?.Text);
            }
        }

        private void MMSystemQuests_Click(object sender, EventArgs e)
        {
            tcViewer.SelectedTab = tpQuests;
        }

        private void MMGameObjectsDoodads_Click(object sender, EventArgs e)
        {
            tcViewer.SelectedTab = tpDoodads;
        }

        private void MMBack_Click(object sender, EventArgs e)
        {
            SkipTabHistory = true;
            if (TabHistoryIndex > 0)
            {
                TabHistoryIndex--;
                tcViewer.SelectedTab = TabHistory[TabHistoryIndex];
            }
            SkipTabHistory = false;
        }

        private void MMForward_Click(object sender, EventArgs e)
        {
            SkipTabHistory = true;
            if (TabHistoryIndex < TabHistory.Count - 1)
            {
                TabHistoryIndex++;
                tcViewer.SelectedTab = TabHistory[TabHistoryIndex];
            }
            SkipTabHistory = false;
        }

        private void MMSystemSpheres_Click(object sender, EventArgs e)
        {
            tcViewer.SelectedTab = tpSpheres;
        }

        private void TBFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TMBack_Click(object sender, EventArgs e)
        {
            MMBack_Click(null, null);
        }

        private void TBForward_Click(object sender, EventArgs e)
        {
            MMForward_Click(null, null);
        }

        private void TBSelectedData_Click(object sender, EventArgs e)
        {
            MMSelectedData_Click(null, null);
        }

        private void TBLocalizer_Click(object sender, EventArgs e)
        {
            MMLocalizer_Click(null, null);
        }

        private void TBMap_Click(object sender, EventArgs e)
        {
            MMSystemMaps_Click(null, null);
        }

        private void TBSplit_Click(object sender, EventArgs e)
        {
            MMSplitter_Click(null, null);
        }

        private void TBBuffs_Click(object sender, EventArgs e)
        {
            MMSystemBuffs_Click(null, null);
        }

        private void TBDoodads_Click(object sender, EventArgs e)
        {
            MMGameObjectsDoodads_Click(null, null);
        }

        private void TBFactions_Click(object sender, EventArgs e)
        {
            MMSystemFactions_Click(null, null);
        }

        private void TBItems_Click(object sender, EventArgs e)
        {
            MMSystemItems_Click(null, null);
        }

        private void TBLoot_Click(object sender, EventArgs e)
        {
            MMSystemLoot_Click(null, null);
        }

        private void TBNpc_Click(object sender, EventArgs e)
        {
            MMGameObjectsNpcs_Click(null, null);
        }

        private void TBQuests_Click(object sender, EventArgs e)
        {
            MMSystemQuests_Click(null, null);
        }

        private void TBSchedules_Click(object sender, EventArgs e)
        {
            MMSystemSchedule_Click(null, null);
        }

        private void TBSkills_Click(object sender, EventArgs e)
        {
            MMSystemSkills_Click(null, null);
        }

        private void TBSpheres_Click(object sender, EventArgs e)
        {
            MMSystemSpheres_Click(null, null);
        }

        private void TBTags_Click(object sender, EventArgs e)
        {
            MMSystemTags_Click(null, null);
        }

        private void TBTrades_Click(object sender, EventArgs e)
        {
            MMSystemTrades_Click(null, null);
        }

        private void TBSlaves_Click(object sender, EventArgs e)
        {
            MMGameObjectsVehicles_Click(null, null);
        }

        private void TBZones_Click(object sender, EventArgs e)
        {
            MMSystemZones_Click(null, null);
        }

        private void TBFileTables_Click(object sender, EventArgs e)
        {
            MMFileTables_Click(null, null);
        }

        private void MMFileSettings_Click(object sender, EventArgs e)
        {
            tcViewer.SelectedTab = tpSettings;
        }

        private void TBFileSettings_Click(object sender, EventArgs e)
        {
            tcViewer.SelectedTab = tpSettings;
        }

        private void BtnSearchNPC_Click(object sender, EventArgs e)
        {
            DoSearchNpc();
        }

        private void TSearchNPC_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && (btnSearchNPC.Enabled))
                BtnSearchNPC_Click(null, null);
        }

        private void TSearchNPC_TextChanged(object sender, EventArgs e)
        {
            btnSearchNPC.Enabled = (cbSearchNPC.Text != string.Empty);
        }

        private void DgvNpcs_SelectionChanged(object sender, EventArgs e)
        {
            DoNpcsSelectionChanged();
        }

        private void TSearchDoodads_TextChanged(object sender, EventArgs e)
        {
            btnSearchDoodads.Enabled = (cbSearchDoodads.Text != string.Empty);
        }

        private void TSearchDoodads_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnSearchDoodads_Click(null, null);
        }

        private void BtnSearchDoodads_Click(object sender, EventArgs e)
        {
            DoSearchDoodads();
        }

        private void DgvDoodads_SelectionChanged(object sender, EventArgs e)
        {
            DoDoodadsSelectionChanged();
        }

        private void DgvDoodadFuncGroups_SelectionChanged(object sender, EventArgs e)
        {
            DoDoodadFuncGroupsSelectionChanged();
        }

        private void TvDoodadDetails_DoubleClick(object sender, EventArgs e)
        {
            if ((sender is TreeView tv) && (tv.SelectedNode != null))
                ProcessNodeInfoDoubleClick(tv.SelectedNode);
        }

        private void CbDoodadWorkflowHideEmpty_CheckedChanged(object sender, EventArgs e)
        {
            if (long.TryParse(lDoodadID.Text, out var id))
                ShowDbDoodad(id);
        }

        private void BtnSearchSlave_Click(object sender, EventArgs e)
        {
            DoSearchSlave();
        }

        private void TSearchSlave_TextChanged(object sender, EventArgs e)
        {
            btnSearchSlave.Enabled = !string.IsNullOrEmpty(tSearchSlave.Text);
        }

        private void TSearchSlave_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && (btnSearchSlave.Enabled))
                BtnSearchSlave_Click(null, null);
        }

        private void TvNpcInfo_DoubleClick(object sender, EventArgs e)
        {
            // In properties the node tag is used internally, so only allow this node double-click if it's not set
            if ((sender is TreeView tv) && (tv.SelectedNode != null) && (tv.SelectedNode.Tag == null))
                ProcessNodeInfoDoubleClick(tv.SelectedNode);
        }

        private void DgvSlaves_SelectionChanged(object sender, EventArgs e)
        {
            DoSlavesSelectionChanged();
        }

        private void TvSlaveInfo_DoubleClick(object sender, EventArgs e)
        {
            if ((sender is TreeView tv) && (tv.SelectedNode != null) && (tv.SelectedNode.Tag == null))
                ProcessNodeInfoDoubleClick(tv.SelectedNode);
        }

        private void BtnItemSearch_Click(object sender, EventArgs e)
        {
            DoItemSearch();
        }

        private void TItemSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnItemSearch_Click(null, null);
            }
        }

        private void DgvItemSearch_SelectionChanged(object sender, EventArgs e)
        {
            DoItemSelectionChanged();
        }

        private void CbItemSearchLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbItemSearchLanguage.Enabled == false)
                return;
            cbItemSearchLanguage.Enabled = false;
            if (cbItemSearchLanguage.Text != Properties.Settings.Default.DefaultGameLanguage)
            {
                Properties.Settings.Default.DefaultGameLanguage = cbItemSearchLanguage.Text;
                LoadServerDB(false);
            }

            cbItemSearchLanguage.Enabled = true;
        }

        private void TItemSearch_TextChanged(object sender, EventArgs e)
        {
            btnItemSearch.Enabled = (cbItemSearch.Text != string.Empty);
        }

        private void BtnFindItemInLoot_Click(object sender, EventArgs e)
        {
            DoFindItemInLoot();
        }

        private void TLootSearch_TextChanged(object sender, EventArgs e)
        {
            btnLootSearch.Enabled = (tLootSearch.Text != string.Empty);
        }

        private void DgvLoot_SelectionChanged(object sender, EventArgs e)
        {
            DoLootSelectionChanged();
        }

        private void BtnLootSearch_Click(object sender, EventArgs e)
        {
            DoLootSearch();
        }

        private void TLootSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnLootSearch_Click(null, null);
            }
        }

        private void CbItemSearchFilterChanged(object sender, EventArgs e)
        {
            if (cbItemSearch.Text.Replace("*", "") == string.Empty)
            {

                if ((cbItemSearchItemArmorSlotTypeList.SelectedIndex > 0) ||
                    (cbItemSearchItemCategoryTypeList.SelectedIndex > 0) ||
                    (cbItemSearchRange.SelectedIndex > 0) ||
                    (cbItemSearchType.SelectedIndex > 0))
                    cbItemSearch.Text = "*";
                else
                    cbItemSearch.Text = string.Empty;
            }
        }

        private void BtnShowNpcLoot_Click(object sender, EventArgs e)
        {
            var searchId = (long)((sender as Button)?.Tag ?? 0);
            if (searchId <= 0)
                return;

            DoShowNpcLoot(searchId);
        }

        private void BtnFindLootNpc_Click(object sender, EventArgs e)
        {
            var searchId = (long)((sender as Button)?.Tag ?? 0);
            if (searchId <= 0)
                return;
            DoFindLootNpc(searchId);
        }

        private void TSearchFaction_TextChanged(object sender, EventArgs e)
        {
            btnSearchFaction.Enabled = (tSearchFaction.Text != string.Empty);
        }

        private void BtnSearchFaction_Click(object sender, EventArgs e)
        {
            DoSearchFaction();
        }

        private void TSearchFaction_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnSearchFaction_Click(null, null);
        }

        private void BtnFactionsAll_Click(object sender, EventArgs e)
        {
            DoShowAllFactions();
        }

        private void DgvFactions_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvFactions.SelectedRows.Count <= 0)
                return;
            var row = dgvFactions.SelectedRows[0];
            if (row.Cells.Count <= 0)
                return;

            var id = row.Cells[0].Value;
            if (id != null)
            {
                ShowDbFaction(long.Parse(id.ToString() ?? "0"));
                ShowSelectedData("system_factions", "(id = " + id.ToString() + ")", "id ASC");
            }
        }

        private void BtnMap_Click(object sender, EventArgs e)
        {
            var map = MapViewForm.GetMap();
            map.Show();
        }

        private void BtnShowNPCsOnMap_Click(object sender, EventArgs e)
        {
            var searchId = (long)((sender as Button)?.Tag ?? 0);
            if (searchId <= 0)
                return;
            DoShowNPCsOnMap(searchId);
        }

        private void btnFindTransferPathsInZone_Click(object sender, EventArgs e)
        {
            var searchId = (long)((sender as Button)?.Tag ?? 0);
            if (searchId <= 0)
                return;
            DoFindTransferPathsInZone(searchId);
        }

        private void BtnExportDataForVieweD_Click(object sender, EventArgs e)
        {
            DoExportDataForVieweD();
        }

        private void BtnExportNPCSpawnData_Click(object sender, EventArgs e)
        {
            DoExportNpcSpawnData();
        }

        private void BtnFindAllTransferPaths_Click(object sender, EventArgs e)
        {
            DoFindAllTransferPaths();
        }

        private void BtnFindAllHousing_Click(object sender, EventArgs e)
        {
            DoFindAllHousing();
        }

        private void BtnLoadCustomPaths_Click(object sender, EventArgs e)
        {
            DoLoadCustomPaths();
        }

        private void BtnLoadCustomAAEmuJson_Click(object sender, EventArgs e)
        {
            DoLoadCustomAAEmuJson();
        }

        private void BtnShowEntityAreaShape_Click(object sender, EventArgs e)
        {
            DoShowEntityAreaShape();
        }

        private void LbTradeSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            DoTradeSourceSelectedIndexChanged();
        }

        private void LbTradeDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            DoTradeDestinationSelectedIndexChanged();
        }

        private void BtnExportDoodadSpawnData_Click(object sender, EventArgs e)
        {
            DoExportDoodadSpawnData();
        }

        private void BtnSearchTags_Click(object sender, EventArgs e)
        {
            DoSearchTags();
        }

        private void TSearchTags_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSearchTags_Click(null, null);
            }
        }

        private void DgvTags_SelectionChanged(object sender, EventArgs e)
        {
            DoTagsSelectionChanged();
        }

        private void TSearchTags_TextChanged(object sender, EventArgs e)
        {
            btnSearchTags.Enabled = (tSearchTags.Text != string.Empty);
        }

        private void TvTagInfo_DoubleClick(object sender, EventArgs e)
        {
            if ((sender is TreeView tv) && (tv.SelectedNode != null) && (tv.SelectedNode.Tag == null))
                ProcessNodeInfoDoubleClick(tv.SelectedNode);
        }

        private void BtnLoadAAEmuWater_Click(object sender, EventArgs e)
        {
            DoLoadAAEmuWater();
        }

        private void BtnQuestsSearch_Click(object sender, EventArgs e)
        {
            DoQuestsSearch();
        }

        private void TQuestSearch_TextChanged(object sender, EventArgs e)
        {
            btnQuestsSearch.Enabled = (cbQuestSearch.Text != string.Empty);
        }

        private void TQuestSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnQuestsSearch_Click(null, null);
            }
        }

        private void DgvQuests_SelectionChanged(object sender, EventArgs e)
        {
            DoQuestsSelectionChanged();
        }

        private void BtnFindAllQuestSpheres_Click(object sender, EventArgs e)
        {
            DoFindAllQuestSpheres();
        }

        private void BtnQuestFindRelatedOnMap_Click(object sender, EventArgs e)
        {
            if ((sender as Button)?.Tag == null)
                return;
            var searchId = (long)(((Button)sender).Tag ?? 0);
            if (searchId <= 0)
                return;

            DoQuestFindRelatedOnMap(searchId);

        }

        private void TvQuestWorkflow_DoubleClick(object sender, EventArgs e)
        {
            if ((sender is TreeView tv) && (tv.SelectedNode != null))
                ProcessNodeInfoDoubleClick(tv.SelectedNode);
        }

        private void CbQuestWorkflowHideEmpty_CheckedChanged(object sender, EventArgs e)
        {
            if ((dgvQuests.CurrentRow != null) && (dgvQuests.CurrentRow.Cells.Count > 0))
            {
                if (long.TryParse(dgvQuests.CurrentRow.Cells[0].Value.ToString(), out var id))
                    ShowDbQuest(id);
            }
        }

        private void LbSchedules_SelectedIndexChanged(object sender, EventArgs e)
        {
            DoSchedulesSelectedIndexChanged();
        }

        private void LbSchedulesGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            DoSchedulesGameSelectedIndexChanged();
        }

        private void TvSchedule_DoubleClick(object sender, EventArgs e)
        {
            ProcessNodeInfoDoubleClick(tvSchedule.SelectedNode);
        }

        private void LbTowerDefs_SelectedIndexChanged(object sender, EventArgs e)
        {
            DoTowerDefsSelectedIndexChanged();
        }

        private void TSkillSearch_TextChanged(object sender, EventArgs e)
        {
            btnSkillSearch.Enabled = (cbSkillSearch.Text != string.Empty);
        }

        private void BtnSkillSearch_Click(object sender, EventArgs e)
        {
            DoSkillSearch();
        }

        private void TSkillSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSkillSearch_Click(null, null);
            }
        }

        private void DgvSkills_SelectionChanged(object sender, EventArgs e)
        {
            DoSkillsSelectionChanged();
        }

        private void BtnFindItemSkill_Click(object sender, EventArgs e)
        {
            DoFindItemSkill();
        }

        private void BtnSearchBuffs_Click(object sender, EventArgs e)
        {
            DoSearchBuffs();
        }

        private void TSearchBuffs_TextChanged(object sender, EventArgs e)
        {
            btnSearchBuffs.Enabled = (cbSearchBuffs.Text != string.Empty);
        }

        private void TSearchBuffs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnSearchBuffs_Click(null, null);
        }

        private void DgvBuffs_SelectionChanged(object sender, EventArgs e)
        {
            DoBuffsSelectionChanged();
        }

        private void TvSkill_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e?.Node != null)
                DoSkillAfterSelect(e.Node);
        }

        private void TvSkill_DoubleClick(object sender, EventArgs e)
        {
            // In properties the node tag is used internally, so only allow this node double-click if it's not set
            if ((sender is TreeView tv) && (tv.SelectedNode != null) && (tv.SelectedNode.Tag == null))
                ProcessNodeInfoDoubleClick(tv.SelectedNode);
        }

        private void TvBuffTriggers_DoubleClick(object sender, EventArgs e)
        {
            if ((sender is TreeView tv) && (tv.SelectedNode != null) && (tv.SelectedNode.Tag == null))
                ProcessNodeInfoDoubleClick(tv.SelectedNode);
        }

        private void CbBuffsHideEmpty_CheckedChanged(object sender, EventArgs e)
        {
            if (long.TryParse(lBuffId.Text, out var id))
                ShowDbBuff(id);
        }

        private void BtnCopySkillExecutionTree_Click(object sender, EventArgs e)
        {
            CopyToClipBoard($"Skill: {lSkillID.Text} - " + TreeViewToString(tvSkill.Nodes, 0));
        }

        private void BtnSkillTreeCollapse_Click(object sender, EventArgs e)
        {
            tvSkill.CollapseAll();
            foreach (TreeNode node in tvSkill.Nodes)
            {
                node.Expand();
            }
        }

        private void TBMenuFileSwitchSelect_Click(object sender, EventArgs e)
        {
            // Switch profile
            if ((sender is ToolStripMenuItem tsmi) && (tsmi.Tag is SavedProfile profile))
            {
                ApplyProfile(profile);
            }
        }

        private void LoadProfiles()
        {
            LbProfiles.Items.Clear();
            var profilesString = Properties.Settings.Default.Profiles;
            try
            {
                Profiles = JsonConvert.DeserializeObject<List<SavedProfile>>(profilesString);
            }
            catch
            {
                //
            }

            if (Profiles == null)
                Profiles = new();

            TBFileSwitchProfileMenu.DropDownItems.Clear();

            foreach (var savedProfile in Profiles)
            {
                LbProfiles.Items.Add(savedProfile);
                var newMenuItem = new ToolStripMenuItem(savedProfile.Name);
                newMenuItem.Tag = savedProfile;
                newMenuItem.Click += TBMenuFileSwitchSelect_Click;

                TBFileSwitchProfileMenu.DropDownItems.Add(newMenuItem);
            }
        }

        private void SaveProfiles()
        {
            try
            {
                var profilesString = JsonConvert.SerializeObject(Profiles);
                Properties.Settings.Default.Profiles = profilesString;
                Properties.Settings.Default.Save();
            }
            catch
            {
                // Failed to convert?
            }
        }

        private void BtnSaveProfileAs_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TSaveProfileName.Text))
            {
                MessageBox.Show("Name is required");
                return;
            }

            if (Profiles.Any(x => x.Name.Equals(TSaveProfileName.Text, StringComparison.InvariantCultureIgnoreCase)))
            {
                MessageBox.Show("Name already exists");
                return;
            }


            var newProfile = new SavedProfile()
            {
                Name = TSaveProfileName.Text,
                ClientDdFile = string.Empty,
                ServerDdFile = Properties.Settings.Default.DBFileName,
                GamePakFile = Properties.Settings.Default.GamePakFileName,
                Locale = Properties.Settings.Default.DefaultGameLanguage
            };

            Profiles.Add(newProfile);
            SaveProfiles();
            LoadProfiles();
            SelectCurrentProfile();
        }

        private void SelectCurrentProfile()
        {
            foreach (var lbProfilesItem in LbProfiles.Items)
            {
                if (lbProfilesItem is not SavedProfile savedProfile)
                    continue;
                if (savedProfile.GamePakFile != Properties.Settings.Default.GamePakFileName)
                    continue;
                if (savedProfile.ServerDdFile != Properties.Settings.Default.DBFileName)
                    continue;
                if (savedProfile.Locale != Properties.Settings.Default.DefaultGameLanguage)
                    continue;
                LbProfiles.SelectedItem = lbProfilesItem;
                break;
            }
        }

        private bool ApplyProfile(SavedProfile profile)
        {
            var oldServerDb = Properties.Settings.Default.DBFileName;
            var oldGamePakFile = Properties.Settings.Default.GamePakFileName;
            var oldLocale = Properties.Settings.Default.DefaultGameLanguage;

            Properties.Settings.Default.DBFileName = profile.ServerDdFile;
            Properties.Settings.Default.GamePakFileName = profile.GamePakFile;
            Properties.Settings.Default.DefaultGameLanguage = profile.Locale;

            bool failed = !LoadServerDB(false);

            if (!failed && !DoFindGameClient(false))
                failed = true;

            if (failed)
            {
                // revert settings
                Properties.Settings.Default.DBFileName = oldServerDb;
                Properties.Settings.Default.GamePakFileName = oldGamePakFile;
                Properties.Settings.Default.DefaultGameLanguage = oldLocale;
                LoadServerDB(false);
                DoFindGameClient(false);
                MessageBox.Show($"Failed to load profile {profile.Name}");
                return false;
            }

            Properties.Settings.Default.Save();
            return true;
        }

        private void BtnLoadProfile_Click(object sender, EventArgs e)
        {
            if (LbProfiles.SelectedItem is not SavedProfile profile)
            {
                MessageBox.Show("No profile selected");
                return;
            }

            _ = ApplyProfile(profile);
        }

        private void BtnDeleteProfile_Click(object sender, EventArgs e)
        {
            if (LbProfiles.SelectedItem is not SavedProfile profile)
            {
                MessageBox.Show("No profile selected");
                return;
            }

            if (MessageBox.Show($"Are you sure you want to remove {profile.Name} ?", "Delete profile", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            if (!Profiles.Remove(profile))
            {
                MessageBox.Show("Failed to remove profile");
                return;
            }

            SaveProfiles();
            LoadProfiles();
        }

        private void BtnLoadUntMovement_Click(object sender, EventArgs e)
        {
            DoLoadExportedUnitMovement();
        }

        private void BtnAdditionalServerDb_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to load an additional database file for this profile?", "Add DB", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            if (openDBDlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            var oldDbSetting = Properties.Settings.Default.DBFileName;
            var sqliteFiles = Properties.Settings.Default.DBFileName.Split(Path.PathSeparator).ToList();
            if (sqliteFiles.Contains(openDBDlg.FileName))
            {
                MessageBox.Show($"{openDBDlg.FileName} is already loaded");
                return;
            }
            sqliteFiles.Add(openDBDlg.FileName);
            Properties.Settings.Default.DBFileName = string.Join(Path.PathSeparator, sqliteFiles);
            if (LoadServerDB(false))
            {
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.DBFileName = oldDbSetting;
                LoadServerDB(false);
            }
            UpdateTitleBar();
        }

        private void MMAchievements_Click(object sender, EventArgs e)
        {
            tcViewer.SelectedTab = tpAchievements;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            MMAchievements_Click(null, null);
        }

        private void BtnAchievementFilter_Click(object sender, EventArgs e)
        {
            UpdateAchievementTree();
        }

        private void TSearchAchievements_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnAchievementFilter_Click(null, null);
        }

        private void TvAchievements_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ShowDbAchievement(e?.Node?.Tag as GameAchievements);
        }
    }
}
