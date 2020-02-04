using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using AAPakEditor;

namespace AAEmu.ClientDataExporter
{
    public partial class MainForm : Form
    {
        private class QuestSphereEntry
        {
            public int questID = -1 ;
            public int componentID = -1;
            public double X = 0.0f;
            public double Y = 0.0f;
            public double Z = 0.0f;
            public double radius = 0.0f;
        }

        private AAPak pak = null ;
        
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
                pak = new AAPak(clientFolderDlg.SelectedPath + Path.DirectorySeparatorChar + "game_pak",true,false);
                btnQuestSphere.Enabled = (pak.isOpen);
                LClientLocation.Text = clientFolderDlg.SelectedPath;
            }
            Application.UseWaitCursor = false;
        }

        private void BtnQuestSphere_Click(object sender, EventArgs e)
        {
            if ((pak == null) || (!pak.isOpen))
                return;

            var qsd = new List<QuestSphereEntry>();
            var sl = new List<string>();

            // Find all related files and concat them into a giant stringlist
            foreach(var pfi in pak.files)
            {
                if (pfi.name.ToLower().EndsWith("quest_sphere_data.g"))
                {
                    var thisStream = pak.ExportFileAsStream(pfi);
                    using (var rs = new StreamReader(thisStream))
                    {
                        while(!rs.EndOfStream)
                        {
                            sl.Add(rs.ReadLine().Trim(' ').Trim('\t').ToLower());
                        }
                    }
                }
            }

            for(int i = 0; i < sl.Count - 3;i++)
            {
                var l1 = sl[i + 0];
                var l2 = sl[i + 1];
                var l3 = sl[i + 2];
                var l4 = sl[i + 3];
                Add parser here

            }


        }
    }
}
