using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AAEmu.DBViewer
{
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            InitializeComponent();
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {

        }

        public void ShowInfo(string info)
        {
            lInfo.Text = info;
            Refresh();
            System.Threading.Thread.Sleep(1);
        }
    }
}
