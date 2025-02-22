using System.Windows.Forms;

namespace AAEmu.DBViewer.utils;

public class TreeNodeWithPlotEventReference : TreeNode
{
    public long ThisEventId = 0;
    public long TargetEventId = 0;
}