using System.Collections.Generic;
using System.Windows.Forms;

namespace AAEmu.DBViewer.utils;

public static class TreeViewExtension
{
    public static IEnumerable<TreeNode> Collect(TreeNodeCollection nodes)
    {
        foreach (TreeNode node in nodes)
        {
            yield return node;

            foreach (var child in Collect(node.Nodes))
                yield return child;
        }
    }
}