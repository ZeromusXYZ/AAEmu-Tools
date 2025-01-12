using System;
using System.Windows.Forms;

namespace AAEmu.DBEditor.forms
{
    public partial class MapForm : Form
    {
        private static MapForm _instance;

        public static MapForm Instance
        {
            get { return _instance ??= new MapForm(); }
        }

        public MapForm()
        {
            InitializeComponent();
        }

        private void MapForm_Load(object sender, EventArgs e)
        {
            MainForm.Self.AddOwnedForm(this);
        }

        private void ViewPort_Paint(object sender, PaintEventArgs e)
        {
            var p = (sender as PictureBox);
            if (p != null)
            {
                return;
            }
            OnViewPaint(p, e);
        }

        private void OnViewPaint(PictureBox pictureBox, PaintEventArgs paintEventArgs)
        {
            throw new NotImplementedException();
        }
    }
}
