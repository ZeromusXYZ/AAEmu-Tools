using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AAEmu.DBViewer.utils
{
    public class TreeNodeWithInfo : TreeNode
    {
        public TabPage targetTabPage;
        public TextBox targetTextBox;
        public ComboBox targetSearchBox;
        public string targetSearchText = string.Empty;
        public Button targetSearchButton;
        public string targetWorldName = string.Empty;
        public Vector3 targetPosition = Vector3.Zero;
        public float targetRadius = 0f;
    }
}
