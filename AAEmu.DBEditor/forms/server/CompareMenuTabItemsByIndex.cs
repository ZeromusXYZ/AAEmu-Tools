using System.Collections;
using System.Windows.Forms;

namespace AAEmu.DBEditor.forms.server
{
    public partial class ICSForm
    {
        class CompareMenuTabItemsByIndex : IComparer
        {
            private readonly System.Windows.Forms.ListView _listView;

            public CompareMenuTabItemsByIndex(System.Windows.Forms.ListView listView)
            {
                this._listView = listView;
            }
            public int Compare(object x, object y)
            {
                int i = this._listView.Items.IndexOf((ListViewItem)x);
                int j = this._listView.Items.IndexOf((ListViewItem)y);
                return i - j;
            }
        }
    }
}
