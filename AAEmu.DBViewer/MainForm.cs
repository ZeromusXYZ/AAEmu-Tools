﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Numerics;
using System.Reflection;
using AAPacker;
using AAEmu.DBDefs;
using AAEmu.Game.Utils.DB;
using AAEmu.ClipboardHelper;
using AAEmu.DBViewer.utils;
using System.Runtime;
using AAEmu.DBViewer.enums;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace AAEmu.DBViewer
{
    public partial class MainForm : Form
    {
        public static MainForm ThisForm;
        private string defaultTitle;
        public AAPak pak = new("");
        private List<string> possibleLanguageIDs = new List<string>();
        private List<string> allTableNames = new List<string>();
        private readonly List<TabPage> tabHistory = new();
        private bool skipTabHistory = false;
        private int tabHistoryIndex = 0;

        public MainForm()
        {
            InitializeComponent();
            ThisForm = this;
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

                    pak.SetCustomKey(customKey);
                }
                else
                    pak.SetDefaultKey();
            }
            catch
            {
                // Reset key
                pak.SetDefaultKey();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            MMVersion.Text = $"Version {Assembly.GetExecutingAssembly().GetName().Version}";
            MM.Visible = false;
            tcViewer.ItemSize = new Size(0, 1);

            // Update settings if needed
            if (!Properties.Settings.Default.IsUpdated)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();
                Properties.Settings.Default.IsUpdated = true;
                Properties.Settings.Default.Save();
            }

            defaultTitle = Text;
            possibleLanguageIDs.Clear();
            possibleLanguageIDs.Add("ko");
            possibleLanguageIDs.Add("ru");
            possibleLanguageIDs.Add("en_us");
            possibleLanguageIDs.Add("zh_cn");
            possibleLanguageIDs.Add("zh_tw");
            possibleLanguageIDs.Add("de");
            possibleLanguageIDs.Add("fr");
            possibleLanguageIDs.Add("ja");
            cbItemSearchRange.SelectedIndex = 0;
            tcDoodads.SelectedTab = tpDoodadWorkflow;

            cbItemSearchType.Items.Clear();
            cbItemSearchType.Items.Add("Any");
            foreach (var implId in Enum.GetValues(typeof(GameItemImplId)))
                cbItemSearchType.Items.Add(((int)implId).ToString() + " - " + implId.ToString());
            cbItemSearchType.SelectedIndex = 0;
            cbNewGM.Checked = Properties.Settings.Default.NewGMCommands;

            if (!LoadServerDB(false))
            {
                Close();
                return;
            }


            string game_pakFileName = Properties.Settings.Default.GamePakFileName;
            if (File.Exists(game_pakFileName))
            {
                using (var loading = new LoadingForm())
                {
                    loading.Show();
                    loading.ShowInfo("Opening: " + Path.GetFileName(game_pakFileName));

                    TryLoadPakKeys(game_pakFileName);

                    if (pak.OpenPak(game_pakFileName, true))
                    {
                        Properties.Settings.Default.GamePakFileName = game_pakFileName;
                        lCurrentPakFile.Text = Properties.Settings.Default.GamePakFileName;

                        loading.ShowInfo("Loading: World Data");
                        PrepareWorldXml(true);
                        loading.ShowInfo("Loading: Quest Sign Sphere Data");
                        LoadQuestSpheresFromPak();
                    }
                }
            }

            LoadHistory(Properties.Settings.Default.HistorySearchItem, cbItemSearch);
            LoadHistory(Properties.Settings.Default.HistorySearchNpc, cbSearchNPC);
            LoadHistory(Properties.Settings.Default.HistorySearchQuest, cbQuestSearch);
            LoadHistory(Properties.Settings.Default.HistorySearchSkill, cbSkillSearch);
            LoadHistory(Properties.Settings.Default.HistorySearchDoodad, cbSearchDoodads);
            LoadHistory(Properties.Settings.Default.HistorySearchBuff, cbSearchBuffs);
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
            Properties.Settings.Default.HistorySearchSQL = CreateHistory(cbSimpleSQL);
            Properties.Settings.Default.Save();

            if (MapViewForm.ThisForm != null)
                MapViewForm.ThisForm.Close();

            if (pak != null)
                pak.ClosePak();
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
            string sql = "SELECT name FROM sqlite_master WHERE type = 'table' ORDER BY name ASC";

            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        allTableNames.Clear();
                        lbTableNames.Items.Clear();
                        while (reader.Read())
                        {
                            var tName = GetString(reader, "name");
                            allTableNames.Add(tName);
                            lbTableNames.Items.Add(tName);
                        }
                    }
                }
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
                                (AADB.DB_Items.TryGetValue(itemVal, out GameItem item)))
                            {
                                rt.AppendText(item.nameLocalized);
                            }
                            else if ((fieldNameStr == "NPC_NAME") &&
                                     (AADB.DB_NPCs.TryGetValue(itemVal, out GameNPC npc)))
                            {
                                rt.AppendText(npc.nameLocalized);
                            }
                            else if ((fieldNameStr == "QUEST_NAME") &&
                                     (AADB.DB_Quest_Contexts.TryGetValue(itemVal, out GameQuestContexts quest)))
                            {
                                rt.AppendText(quest.nameLocalized);
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
                    if (long.TryParse(valueText, out var value) && (AADB.DB_Items.TryGetValue(value, out var item)))
                        rt.AppendText(item.nameLocalized);
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
                        (AADB.DB_Doodad_Almighties.TryGetValue(value, out var doodad)))
                        rt.AppendText(doodad.nameLocalized);
                    else
                        rt.AppendText("@DOODAD_NAME(" + valueText + ")");
                    rt.SelectionColor = resetColor;
                    restText = restText.Substring(nextEndBracket + 1);
                }
                else if (restText.StartsWith("@NPC_NAME(") && (nextEndBracket > 10))
                {
                    rt.SelectionColor = Color.Yellow;
                    var valueText = restText.Substring(10, nextEndBracket - 10);
                    if (long.TryParse(valueText, out var value) && (AADB.DB_NPCs.TryGetValue(value, out var npc)))
                        rt.AppendText(npc.nameLocalized);
                    else
                        rt.AppendText("@NPC_NAME(" + valueText + ")");
                    rt.SelectionColor = resetColor;
                    restText = restText.Substring(nextEndBracket + 1);
                }
                else if (restText.StartsWith("@NPC_GROUP_NAME(") && (nextEndBracket > 16))
                {
                    rt.SelectionColor = Color.Yellow;
                    var valueText = restText.Substring(16, nextEndBracket - 16);
                    if (long.TryParse(valueText, out var value) && (AADB.DB_Quest_Monster_Groups.TryGetValue(value, out var npcGroup)))
                        rt.AppendText(npcGroup.nameLocalized);
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
                        (AADB.DB_Quest_Contexts.TryGetValue(value, out var quest)))
                        rt.AppendText(quest.nameLocalized);
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
        }

        private bool LoadServerDB(bool forceDlg)
        {
            string sqlfile = Properties.Settings.Default.DBFileName;

            while (forceDlg || (!File.Exists(sqlfile)))
            {
                forceDlg = false;
                if (openDBDlg.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }
                else
                {
                    sqlfile = openDBDlg.FileName;
                }
            }

            SQLite.SQLiteFileName = sqlfile;

            var i = cbItemSearchLanguage.Items.IndexOf(Properties.Settings.Default.DefaultGameLanguage);
            cbItemSearchLanguage.SelectedIndex = i;
            using (var loading = new LoadingForm())
            {
                loading.Show();
                loading.ShowInfo("Loading: Table names");
                // The table name loading is basically just to check if we can read the DB file
                LoadTableNames();
                Properties.Settings.Default.DBFileName = sqlfile;
                Text = $"{defaultTitle} [{tcViewer.SelectedTab?.Text}] - {Properties.Settings.Default.DBFileName} ({Properties.Settings.Default.DefaultGameLanguage})";
                // make sure translations are loaded first, other tables depend on it

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
                LoadZoneGroupBannedTags();
                loading.ShowInfo("Loading: Zones");
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
            }

            return true;
        }


        private void BtnFindGameClient_Click(object sender, EventArgs e)
        {
            if (openGamePakFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var loading = new LoadingForm())
                {
                    loading.Show();
                    if (pak.IsOpen)
                    {
                        loading.ShowInfo("Closing: " + pak.GpFilePath);
                        pak.ClosePak();

                        // TODO: HACK to try and free up as many memomry as possible - https://stackoverflow.com/questions/30622145/free-memory-of-byte
                        GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
                        GC.Collect();
                    }

                    loading.ShowInfo("Opening: " + Path.GetFileName(openGamePakFileDialog.FileName));

                    TryLoadPakKeys(openGamePakFileDialog.FileName);

                    if (pak.OpenPak(openGamePakFileDialog.FileName, true))
                    {
                        Properties.Settings.Default.GamePakFileName = openGamePakFileDialog.FileName;
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
                        MessageBox.Show("Failed to load: " + openGamePakFileDialog.FileName);
                    }
                }
            }
        }

        private void ShowSelectedData(string table, string whereStatement, string orderStatement)
        {
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
                                row.Cells[2].Value = AADB.GetTranslationByID(thisID, table, col, " ");
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
            cbSimpleSQL.Text = "SELECT * FROM " + tablename + " LIMIT 0, 50";

            // BtnSimpleSQL_Click(null, null);
        }

        private void BtnSimpleSQL_Click(object sender, EventArgs e)
        {
            if (cbSimpleSQL.Text == string.Empty)
                return;

            try
            {
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
                localizedValue = AADB.GetTranslationByID(rootTypeKey, rootTypeName, key, "");
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
            else if (key.EndsWith("next_phase") && (AADB.DB_Doodad_Func_Groups.TryGetValue(val, out var nextPhase)))
            {
                var s = string.IsNullOrWhiteSpace(nextPhase.nameLocalized) ? nextPhase.name : nextPhase.nameLocalized;
                if (!string.IsNullOrEmpty(s))
                    nodeText += " - " + s;
                res.ForeColor = Color.WhiteSmoke;
            }
            else if (key.EndsWith("skill_id") && (AADB.DB_Skills.TryGetValue(val, out var skill)))
            {
                res.targetTabPage = tpSkills;
                res.targetSearchBox = cbSkillSearch;
                // res.targetSearchText = skill.nameLocalized;
                res.targetSearchText = skill.id.ToString();
                res.targetSearchButton = btnSkillSearch;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + skill.nameLocalized;
                setCustomIcon = IconIdToLabel(skill.icon_id, null);
            }
            else if (key.EndsWith("item_id") && (AADB.DB_Items.TryGetValue(val, out var item)))
            {
                res.targetTabPage = tpItems;
                res.targetSearchBox = cbItemSearch;
                // res.targetSearchText = item.nameLocalized;
                res.targetSearchText = item.id.ToString();
                res.targetSearchButton = btnItemSearch;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + item.nameLocalized;
                setCustomIcon = IconIdToLabel(item.icon_id, null);
            }
            else if (key.EndsWith("doodad_id") && (AADB.DB_Doodad_Almighties.TryGetValue(val, out var doodad)))
            {
                res.targetTabPage = tpDoodads;
                res.targetSearchBox = cbSearchDoodads;
                // res.targetSearchText = doodad.nameLocalized;
                res.targetSearchText = doodad.id.ToString();
                res.targetSearchButton = btnSearchDoodads;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + doodad.nameLocalized;
            }
            else if (key.EndsWith("npc_id") && (AADB.DB_NPCs.TryGetValue(val, out var npc)))
            {
                res.targetTabPage = tpNPCs;
                res.targetSearchBox = cbSearchNPC;
                // res.targetSearchText = npc.nameLocalized;
                res.targetSearchText = npc.id.ToString();
                res.targetSearchButton = btnSearchNPC;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + npc.nameLocalized;
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
            else if (key.EndsWith("slave_id") && (AADB.DB_Slaves.TryGetValue(val, out var slave)))
            {
                res.targetTabPage = tpNPCs;
                // res.targetSearchBox = slave.id.ToString();
                res.targetSearchText = slave.id.ToString();
                res.targetSearchButton = btnSearchSlave;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + slave.nameLocalized;
            }
            else if (key.EndsWith("buff_id") && (AADB.DB_Buffs.TryGetValue(val, out var buff)))
            {
                res.targetTabPage = tpBuffs;
                res.targetSearchBox = cbSearchBuffs;
                // res.targetSearchText = buff.nameLocalized;
                res.targetSearchText = buff.id.ToString();
                res.targetSearchButton = btnSearchBuffs;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + buff.nameLocalized;
                setCustomIcon = IconIdToLabel(buff.icon_id, null);
            }
            else if (key.EndsWith("zone_id"))
            {
                if (AADB.DB_Zones.TryGetValue(val, out var zone))
                {
                    res.targetTabPage = tpZones;
                    res.targetTextBox = tZonesSearch;
                    res.targetSearchText = zone.display_textLocalized;
                    res.targetSearchButton = btnSearchZones;
                    res.ForeColor = Color.WhiteSmoke;
                    nodeText += " - " + zone.display_textLocalized + " (id)";
                }

                var zoneByKey = AADB.DB_Zones.Values.FirstOrDefault(z => z.zone_key == val);
                if (zoneByKey != null)
                {
                    res.targetTabPage = tpZones;
                    res.targetTextBox = tZonesSearch;
                    res.targetSearchText = zoneByKey.display_textLocalized;
                    res.targetSearchButton = btnSearchZones;
                    res.ForeColor = Color.WhiteSmoke;
                    if (zone != null)
                        nodeText += " or ";
                    else
                        nodeText += " - ";
                    nodeText += zoneByKey.display_textLocalized + " (key)";
                }
                // Some quest related entries use "zone_id" when they instead mean "zone_group_id"
                // So we add the 2nd part here.
                if (AADB.DB_Zone_Groups.TryGetValue(val, out var zoneGroup))
                {
                    res.targetTabPage = tpZones;
                    res.targetTextBox = tZonesSearch;
                    res.targetSearchText = zoneGroup.display_textLocalized;
                    res.targetSearchButton = btnSearchZones;
                    res.ForeColor = Color.WhiteSmoke;
                    if ((zone != null) || (zoneByKey != null))
                        nodeText += " or ";
                    else
                        nodeText += " - ";
                    nodeText += zoneGroup.display_textLocalized + " (group)";
                }
            }
            else if (key.EndsWith("zone_group_id") && (AADB.DB_Zone_Groups.TryGetValue(val, out var zoneGroup)))
            {
                res.targetTabPage = tpZones;
                res.targetTextBox = tZonesSearch;
                res.targetSearchText = zoneGroup.display_textLocalized;
                res.targetSearchButton = btnSearchZones;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + zoneGroup.display_textLocalized;
            }
            else if (key.EndsWith("item_category_id") &&
                     (AADB.DB_ItemsCategories.TryGetValue(val, out var itemCategory)))
            {
                res.targetTabPage = tpItems;
                res.targetSearchBox = cbItemSearch;
                res.targetSearchText = itemCategory.nameLocalized;
                res.targetSearchButton = btnItemSearch;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + itemCategory.nameLocalized;
            }
            else if (key.EndsWith("tag_id") && (AADB.DB_Tags.TryGetValue(val, out var tagId)))
            {
                res.targetTabPage = tpTags;
                res.targetTextBox = tSearchTags;
                // res.targetSearchText = tagId.nameLocalized;
                res.targetSearchText = tagId.id.ToString();
                res.targetSearchButton = btnSearchTags;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + tagId.nameLocalized;
            }
            else if (key.EndsWith("sphere_id") && (AADB.DB_Spheres.TryGetValue(val, out var sphere)))
            {
                res.targetTabPage = tpSpheres;
                res.targetSearchBox = CbSearchSpheres;
                // res.targetSearchText = npc.nameLocalized;
                res.targetSearchText = sphere.id.ToString();
                res.targetSearchButton = BtnSearchSpheres;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + sphere.name;
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
                var lootNode = new TreeNodeWithInfo();
                lootNode.Text = key + ": " + val;
                lootNode.ForeColor = Color.Yellow;

                var loots = AADB.DB_Loots.Where(l => l.Value.loot_pack_id == val).OrderBy(l => l.Value.group);
                foreach (var loot in loots)
                {
                    var itemNode = new TreeNodeWithInfo();
                    itemNode.Text = loot.Value.item_id.ToString();
                    if (AADB.DB_Items.TryGetValue(loot.Value.item_id, out var lItem))
                    {
                        itemNode.targetTabPage = tpItems;
                        itemNode.targetSearchBox = cbItemSearch;
                        itemNode.targetSearchText = lItem.nameLocalized;
                        itemNode.targetSearchButton = btnItemSearch;
                        itemNode.ForeColor = Color.WhiteSmoke;
                        itemNode.Text += " - " + lItem.nameLocalized;
                        itemNode.SelectedImageIndex = itemNode.ImageIndex = IconIdToLabel(lItem.icon_id, null);
                    }
                    else
                    {
                        itemNode.Text = " - ???";
                        itemNode.ForeColor = Color.Red;
                    }

                    lootNode.Nodes.Add(itemNode);
                }

                rootNode.Nodes.Add(lootNode);
                return lootNode;
            }
            else if (key.EndsWith("spawner_id"))
            {
                var spawnNode = new TreeNodeWithInfo();
                spawnNode.Text = key + ": " + val;
                spawnNode.ForeColor = Color.Yellow;
                rootNode.Nodes.Add(spawnNode);

                var spawns = AADB.DB_Npc_Spawner_Npcs.Values.Where(x => x.npc_spawner_id == val);
                foreach (var npcSpawnerNpc in spawns)
                {
                    if (npcSpawnerNpc.member_type == "Npc")
                        AddCustomPropertyNode("npc_id", npcSpawnerNpc.member_id.ToString(), false, spawnNode);
                    if (npcSpawnerNpc.member_type == "NpcGroup")
                        AddCustomPropertyNode("npc_group_id", npcSpawnerNpc.member_id.ToString(), false, spawnNode);
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
            else if ((key.EndsWith("icon_id") || key.EndsWith("icon1_id") || key.EndsWith("icon2_id")) && AADB.DB_Icons.TryGetValue(val, out var _))
            {
                setCustomIcon = IconIdToLabel(val, null);
            }
            else if (key.EndsWith("quest_id") && (AADB.DB_Quest_Contexts.TryGetValue(val, out var quest)))
            {
                res.targetTabPage = tpQuests;
                res.targetSearchBox = cbQuestSearch;
                // res.targetSearchText = buff.nameLocalized;
                res.targetSearchText = quest.id.ToString();
                res.targetSearchButton = btnQuestsSearch;
                res.ForeColor = Color.WhiteSmoke;
                nodeText += " - " + quest.nameLocalized;
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
            foreach (var i in AADB.DB_Translations)
            {
                var translation = i.Value;
                var tSearchString = "=" + translation.table.ToLower() + "=" + translation.field.ToLower() + "=" +
                                    translation.idx.ToString() + "=" + translation.value.ToLower() + "=";
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
                    row.Cells[0].Value = translation.table;
                    row.Cells[1].Value = translation.field;
                    var idxString = translation.idx.ToString();
                    if (translation.table == "ui_texts")
                    {
                        if (AADB.DB_UiTexts.TryGetValue(translation.idx, out var uiText))
                        {
                            idxString += $" ({uiText.key} : {uiText.category_id})";
                        }
                    }
                    row.Cells[2].Value = idxString;
                    row.Cells[3].Value = translation.value;
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
            if (tFilterTables.Text == string.Empty)
            {
                lbTableNames.Items.Clear();
                foreach (var s in allTableNames)
                    lbTableNames.Items.Add(s);
            }
            else
            {
                lbTableNames.Items.Clear();
                foreach (var s in allTableNames)
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
                    map.AddPoI(info.targetPosition.X, info.targetPosition.Y, info.targetPosition.Z, info.targetSearchText, Color.Aquamarine, info.targetRadius, "", 0, null);

                    map.FocusAll(true, false, false);
                    map.BringToFront();
                }
            }
        }

        private void cbNewGM_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.NewGMCommands = cbNewGM.Checked;
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
            foreach (var dbSphere in AADB.DB_Spheres.Values)
            {
                if (
                    (dbSphere.id != searchId) &&
                    (dbSphere.sphere_detail_id != searchId) &&
                    (!dbSphere.name.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)) &&
                    (!dbSphere.sphere_detail_type.Contains(searchText, StringComparison.InvariantCultureIgnoreCase))
                    )
                    continue;

                var line = DgvSpheres.Rows.Add();
                var row = DgvSpheres.Rows[line];

                row.Cells[0].Value = dbSphere.id.ToString();
                row.Cells[1].Value = dbSphere.name;
                row.Cells[2].Value = dbSphere.sphere_detail_type;
                row.Cells[3].Value = dbSphere.sphere_detail_id.ToString();
                row.Cells[4].Value = dbSphere.enter_or_leave.ToString();

                if (first)
                {
                    first = false;
                    ShowDbSphere(dbSphere.id);
                    ShowSelectedData("spheres", "id = " + dbSphere.id.ToString(), "id ASC");
                }

                if (DgvSpheres.Rows.Count > 250)
                {
                    MessageBox.Show("Results truncated to 250 results, please be more specific in the search");
                    break;
                }
            }

        }

        private void ShowDbSphere(long id)
        {
            TvSpheres.Nodes.Clear();
            if (!AADB.DB_Spheres.TryGetValue(id, out var sphere))
                return;

            var rootNode = TvSpheres.Nodes.Add($"{sphere.id} - {sphere.name}");
            AddCustomPropertyNode("enter_or_leave", sphere.enter_or_leave.ToString(), false, rootNode);
            AddCustomPropertyNode("trigger_condition_id", sphere.trigger_condition_id.ToString(), false, rootNode);
            AddCustomPropertyNode("trigger_condition_time", sphere.trigger_condition_time.ToString(), false, rootNode);
            AddCustomPropertyNode("team_msg", sphere.team_msg, false, rootNode);

            // category_id seems to be Quest Category Id?
            var cat = AADB.DB_Quest_Categories.GetValueOrDefault(sphere.category_id);
            var catName = cat?.nameLocalized ?? "???";
            rootNode.Nodes.Add($"category_id: {sphere.category_id} - {catName}");
            // AddCustomPropertyNode("category_id", sphere.category_id.ToString(), false, rootNode);
            // AddCustomPropertyNode("or_unit_reqs", sphere.or_unit_reqs.ToString(), false, rootNode);
            AddCustomPropertyNode("is_personal_msg", sphere.is_personal_msg.ToString(), false, rootNode);

            var requires = GetSphereRequirements(sphere.id);
            var reqNode = AddUnitRequirementNode(requires, sphere.or_unit_reqs, TvSpheres.Nodes);

            var questList = new List<long>();

            var detailNode = rootNode.Nodes.Add($"{sphere.sphere_detail_type} - {sphere.sphere_detail_id}");
            var detailsTableName = FunctionTypeToTableName(sphere.sphere_detail_type);
            var effectValuesList = GetCustomTableValues(detailsTableName, "id", sphere.sphere_detail_id.ToString());
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
                    var quests = AADB.PAK_QuestSignSpheres.Where(x => x.QuestId == questId).ToArray();
                    foreach (var questSphereEntry in quests)
                    {
                        var qNode = AddCustomPropertyNode("quest_id", questId.ToString(), false, questNode);
                        AddCustomPropertyNode("component_id", questSphereEntry.ComponentId.ToString(), false, qNode);
                        qNode.Nodes.Add($"world_id: {questSphereEntry.WorldId}");

                        // var zoneOffset = Vector3.Zero;
                        var zoneNode = new TreeNodeWithInfo();
                        qNode.Nodes.Add(zoneNode);
                        zoneNode.Text = $"zone_id: {questSphereEntry.ZoneKey}";
                        var zoneByKey = AADB.DB_Zones.Values.FirstOrDefault(z => z.zone_key == questSphereEntry.ZoneKey);
                        if (zoneByKey != null)
                        {
                            zoneNode.targetTabPage = tpZones;
                            zoneNode.targetTextBox = tZonesSearch;
                            zoneNode.targetSearchText = zoneByKey.display_textLocalized;
                            zoneNode.targetSearchButton = btnSearchZones;
                            zoneNode.ForeColor = Color.WhiteSmoke;
                            zoneNode.Text += " - " + zoneByKey.display_textLocalized + " (key)";

                            // var zoneGroup = AADB.DB_Zone_Groups.GetValueOrDefault(zoneByKey.group_id);
                            // if (zoneGroup != null)
                            //     zoneOffset = new Vector3(zoneGroup.PosAndSize.X, zoneGroup.PosAndSize.Y, 0f);
                        }

                        var zonePosNode = new TreeNodeWithInfo();
                        zonePosNode.Text = $"Position: {questSphereEntry.X} , {questSphereEntry.Y} , {questSphereEntry.Z}";
                        zonePosNode.targetPosition = new Vector3(questSphereEntry.X, questSphereEntry.Y, questSphereEntry.Z);
                        zonePosNode.targetRadius = questSphereEntry.radius;
                        zonePosNode.targetWorldName = questSphereEntry.WorldId;
                        zonePosNode.targetSearchText = rootNode.Text;
                        zonePosNode.ForeColor = Color.Aquamarine;
                        qNode.Nodes.Add(zonePosNode);

                        qNode.Nodes.Add($"Radius: {questSphereEntry.radius}");
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
                    reqNode.Nodes.Add($"kind_id: {req.kind_id}, value1: {req.value1}, value2: {req.value2}");
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
            Text = $"{defaultTitle} [{tcViewer.SelectedTab?.Text}] - {Properties.Settings.Default.DBFileName} ({Properties.Settings.Default.DefaultGameLanguage})";
            if (skipTabHistory == false && tcViewer.SelectedTab != null)
            {
                if (tabHistory.Count > tabHistoryIndex)
                    tabHistory.RemoveRange(tabHistoryIndex, tabHistory.Count - tabHistoryIndex - 1);

                tabHistory.Add(tcViewer.SelectedTab);
                tabHistoryIndex = tabHistory.Count - 1;
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
            skipTabHistory = true;
            if (tabHistoryIndex > 0)
            {
                tabHistoryIndex--;
                tcViewer.SelectedTab = tabHistory[tabHistoryIndex];
            }
            skipTabHistory = false;
        }

        private void MMForward_Click(object sender, EventArgs e)
        {
            skipTabHistory = true;
            if (tabHistoryIndex < tabHistory.Count - 1)
            {
                tabHistoryIndex++;
                tcViewer.SelectedTab = tabHistory[tabHistoryIndex];
            }
            skipTabHistory = false;
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
    }
}
