using System.Collections.Generic;
using System.Windows.Forms;
using MissionPlanner.Utilities;

namespace KMZImporter
{
    public static class TreeNodesExtension
    {
        public static IEnumerable<TreeNode> Flatten(this TreeNodeCollection nodes) {
            foreach (TreeNode node in nodes) {
                yield return node;
                foreach (var child in Flatten(node.Nodes))
                    yield return child;
            }
        }
    }
}
