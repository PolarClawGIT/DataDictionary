namespace DataDictionary.Main.Controls
{
    static class TreeViewExtension
    {
        /// <summary>
        /// Expands the node pass and all parent nodes of that node.
        /// </summary>
        /// <param name="node"></param>
        public static void ExpandParent(this TreeNode node)
        {
            if (node.Parent is TreeNode parentNode)
            {
                parentNode.ExpandParent();
                parentNode.Expand();
            }

            node.Expand();
        }

        /// <summary>
        /// Remove all nodes in the collection as well as the child nodes.
        /// </summary>
        /// <param name="target"></param>
        /// <remarks>
        /// This is an alternative to Clear().
        /// Unlike Clear(), it does not call Begin/End Update.
        /// If called inside a Being/End Update, the tree will not redraw until EndUpdate.
        /// If called by itself, the tree will redraw after each item is removed.
        /// </remarks>
        public static void RemoveAll(this TreeNodeCollection target)
        {
            while (target.Count > 0)
            {
                TreeNode item = target[0];
                RemoveAll(item.Nodes);
                item.Remove();
            }
        }

        /// <summary>
        /// Returns all nodes in the tree that meet the condition.
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static IEnumerable<TreeNode> GetNodes(this TreeNodeCollection nodes, Func<TreeNode, Boolean> condition)
        {
            List<TreeNode> result = new List<TreeNode>();

            foreach (var item in nodes.OfType<TreeNode>())
            {
                if (condition(item)) { result.Add(item); }
                if(item.Nodes.Count > 0) { result.AddRange(GetNodes(item.Nodes, condition)); }
            }

            return result;
        }
    }
}
