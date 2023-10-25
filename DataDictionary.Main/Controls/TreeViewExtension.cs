using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Controls
{
    static class TreeViewExtension
    {
        public static void ExpandParent(this TreeNode node)
        {
            node.Expand();

            if (node.Parent is TreeNode parentNode)
            {
                parentNode.Expand();
                parentNode.ExpandParent();
            }
        }
    }
}
