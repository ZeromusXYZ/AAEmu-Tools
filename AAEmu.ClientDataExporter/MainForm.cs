using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using AAPacker;
using System.Globalization;
using Newtonsoft.Json;

namespace AAEmu.ClientDataExporter
{
    public partial class MainForm : Form
    {
        private class QuestSphereEntry
        {
            public string worldID = string.Empty;
            public int zoneID = -1;
            public int questID = -1;
            public int componentID = -1;
            public double X = 0.0f;
            public double Y = 0.0f;
            public double Z = 0.0f;
            public double radius = 0.0f;
        }

        private AAPak pak = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {


        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (pak != null)
            {
                pak.ClosePak();
                pak = null;
            }
        }

        private void BtnFindClient_Click(object sender, EventArgs e)
        {
            if (clientFolderDlg.ShowDialog() == DialogResult.OK)
            {
                Application.UseWaitCursor = true;
                LClientLocation.Text = "<loading>";
                LClientLocation.Update();
                if (pak != null)
                    pak.ClosePak();
                pak = new AAPak(clientFolderDlg.SelectedPath + Path.DirectorySeparatorChar + "game_pak", true, false);
                btnQuestSphere.Enabled = (pak.IsOpen);
                LClientLocation.Text = clientFolderDlg.SelectedPath;
            }
            Application.UseWaitCursor = false;
        }

        private void BtnQuestSphere_Click(object sender, EventArgs e)
        {
            if ((pak == null) || (!pak.IsOpen))
                return;

            var qsd = new List<QuestSphereEntry>();
            var sl = new List<string>();

            LQuestSphereData.Text = "<searching for data>";
            LQuestSphereData.Update();

            // Find all related files and concat them into a giant stringlist
            foreach (var pfi in pak.Files)
            {
                var lowername = pfi.Name.ToLower();
                if (lowername.EndsWith("quest_sign_sphere.g"))
                {
                    var namesplited = lowername.Split('/');
                    var thisStream = pak.ExportFileAsStream(pfi);
                    using (var rs = new StreamReader(thisStream))
                    {
                        LQuestSphereData.Text = pfi.Name;
                        LQuestSphereData.Update();
                        sl.Clear();
                        while (!rs.EndOfStream)
                        {
                            sl.Add(rs.ReadLine().Trim(' ').Trim('\t').ToLower());
                        }
                    }
                    var worldname = "";
                    var zone = 0;

                    if (namesplited.Length > 6)
                    {
                        if ((namesplited[0] == "game") &&
                            (namesplited[1] == "worlds") &&
                            (namesplited[3] == "level_design") &&
                            (namesplited[4] == "zone") &&
                            (namesplited[6] == "client")
                            )
                        {
                            worldname = namesplited[2];
                            zone = int.Parse(namesplited[5]);
                        }
                    }

                    for (int i = 0; i < sl.Count - 4; i++)
                    {
                        var l0 = sl[i + 0]; // area
                        var l1 = sl[i + 1]; // qtype
                        var l2 = sl[i + 2]; // ctype
                        var l3 = sl[i + 3]; // pos
                        var l4 = sl[i + 4]; // radius


                        if (l0.StartsWith("area") && l1.StartsWith("qtype") && l2.StartsWith("ctype") && l3.StartsWith("pos") && l4.StartsWith("radius"))
                        {
                            try
                            {
                                var qse = new QuestSphereEntry();
                                qse.worldID = worldname;
                                qse.zoneID = zone;

                                qse.questID = int.Parse(l1.Substring(6));

                                qse.componentID = int.Parse(l2.Substring(6));

                                var subline = l3.Substring(4).Replace("(", "").Replace(")", "").Replace("x", "").Replace("y", "").Replace("z", "").Replace(" ", "");
                                var posstring = subline.Split(',');
                                if (posstring.Length == 3)
                                {
                                    // Parse the floats with NumberStyles.Float and CultureInfo.InvariantCulture or we get all sorts of
                                    // weird stuff with the decimal points depending on the user's language settings
                                    qse.X = double.Parse(posstring[0], NumberStyles.Float, CultureInfo.InvariantCulture);
                                    qse.Y = double.Parse(posstring[1], NumberStyles.Float, CultureInfo.InvariantCulture);
                                    qse.Z = double.Parse(posstring[2], NumberStyles.Float, CultureInfo.InvariantCulture);
                                }
                                qse.radius = double.Parse(l4.Substring(7));

                                qsd.Add(qse);
                                i += 5;
                            }
                            catch (Exception x)
                            {
                                MessageBox.Show("Exception: " + x.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    System.Threading.Thread.Sleep(5);
                }
            }

            var json = JsonConvert.SerializeObject(qsd.ToArray(), Formatting.Indented);
            LQuestSphereData.Text = "<added " + qsd.Count.ToString() + " entries>";
            LQuestSphereData.Update();

            exportFileDlg.FileName = "quest_sign_spheres.json";
            if (exportFileDlg.ShowDialog() == DialogResult.OK)
                File.WriteAllText(exportFileDlg.FileName, json);
        }

        private void btnMissionXml_Click(object sender, EventArgs e)
        {
            // example: game/worlds/main_world/zone/139/mission_mission0.xml
            if ((pak == null) || (!pak.IsOpen))
                return;

            lMissionXml.Text = "<searching for data>";
            lMissionXml.Update();

            // Find all related files and concat them into a giant stringlist
            foreach (var pfi in pak.Files)
            {
                var lowerName = pfi.Name.ToLower();
                if (lowerName.EndsWith("mission_mission0.xml"))
                {
                    // Read/Parse XML data here
                }
            }
            lMissionXml.Text = "<done>";
        }
    }
}
