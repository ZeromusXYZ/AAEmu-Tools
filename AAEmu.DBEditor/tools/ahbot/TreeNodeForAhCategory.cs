using System.Windows.Forms;

namespace AAEmu.DBEditor.tools.ahbot;

public class TreeNodeForAhCategory : TreeNode
{
    public long ThisKey => MakeKey(CategoryA, CategoryB, CategoryC);
    public TreeNodeForAhCategory ParentCategory { get; set; }
    public long CategoryA { get; set; }
    public long CategoryB { get; set; }
    public long CategoryC { get; set; }

    public static long MakeKey(long? a, long? b, long? c)
    {
        return (a ?? 0) + (0x100 * b ?? 0) + (0x10000 * c ?? 0);
    }
}