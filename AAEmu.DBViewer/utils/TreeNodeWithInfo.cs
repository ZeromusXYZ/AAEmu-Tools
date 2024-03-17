using System;
using System.Collections.Generic;
using System.Linq;
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
        public string targetSearchText = String.Empty;
        public Button targetSearchButton;
    }
}
