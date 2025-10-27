using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;

namespace DataDictionary.Main.Controls
{
    static class TemplateNodeTree
    {
        static Dictionary<TreeView, Dictionary<TreeNode, TemplateNodeValue>> treeControls = new Dictionary<TreeView, Dictionary<TreeNode, TemplateNodeValue>>();

        /// <summary>
        /// Gets the Template Node from the Tree.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="templateNode"></param>
        /// <returns></returns>
        public static Boolean TryGetValue(this TreeNode node, [NotNullWhen(true)] out TemplateNodeValue? templateNode)
        {
            if (node.TreeView is TreeView tree && treeControls.ContainsKey(tree) 
                && treeControls[tree].TryGetValue(node, out TemplateNodeValue? result))
            { templateNode = result; return true; }
            else { templateNode = null; return false; }
        }

        /// <summary>
        /// Builds the Tree nodes for Template Nodes.
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="template"></param>
        /// <param name="nodes"></param>
        /// <param name="owners"></param>
        public static void BuildTree(this TreeView tree,
            ITemplateValue template,
            IEnumerable<TemplateNodeValue> nodes,
            IEnumerable<TemplateNodeOwnerValue> owners)
        {
            Dictionary<TreeNode, TemplateNodeValue> nodeDictionary;
            List<TemplateNodeIndex> expanded = new List<TemplateNodeIndex>();
            TemplateNodeIndex rootKey = new TemplateNodeIndex();

            tree.BeginUpdate();

            if (treeControls.ContainsKey(tree))
            { nodeDictionary = treeControls[tree]; }
            else
            {
                nodeDictionary = new Dictionary<TreeNode, TemplateNodeValue>();
                treeControls.Add(tree, nodeDictionary);
                tree.Disposed += Tree_Disposed;
            }

            // Get list of Expanded Tree Nodes
            foreach (TreeNode node in tree.Nodes.GetNodes(w => w.IsExpanded))
            {
                if (nodeDictionary.TryGetValue(node, out TemplateNodeValue? value))
                {
                    TemplateNodeIndex key = new TemplateNodeIndex(value);
                    if (!expanded.Contains(key))
                    { expanded.Add(key); }
                }
                else
                {
                    if (!expanded.Contains(rootKey))
                    { expanded.Add(rootKey); }
                }
            }

            // Clear and rebuild the Nodes
            tree.Nodes.Clear();
            if (tree.ImageList is null)
            { tree.SetImageList(); }

            TreeNode root = new TreeNode(template.Title);
            root.ImageKey = template.Scope.GetName();
            root.SelectedImageKey = template.Scope.GetName();
            tree.Nodes.Add(root);

            foreach (TemplateNodeValue item in nodes.
                Where(w => !owners.Any(a => new TemplateNodeIndex(w).Equals(a))).
                OrderBy(o => o.NodeOrder).
                ThenBy(o => o.NodeName).
                ToList())
            { BuildNode(root, item); }

            // Restore Expanded Tree Nodes
            if (expanded.Count > 0)
            {
                foreach (TreeNode item in tree.Nodes)
                { item.ExpandParent(); }
            }

            foreach (var item in nodeDictionary.Where(w => expanded.Any(a => a.Equals(w.Value))))
            { item.Key.ExpandParent(); }

            tree.EndUpdate();

            void BuildNode(TreeNode parent, TemplateNodeValue templateNode)
            {
                TreeNode result = new TreeNode(templateNode.NodeName);
                result.ImageKey = templateNode.Scope.GetName();
                result.SelectedImageKey = templateNode.Scope.GetName();
                parent.Nodes.Add(result);
                nodeDictionary.Add(result, templateNode);

                foreach (TemplateNodeOwnerValue owner in owners.
                    Where(w => new TemplateNodeOwnerIndex(templateNode).Equals(w)).
                    ToList())
                {
                    foreach (TemplateNodeValue child in nodes.
                        Where(w => new TemplateNodeIndex(owner).Equals(w)).
                        OrderBy(o => o.NodeOrder).
                        ThenBy(o => o.NodeName).
                        ToList())
                    { BuildNode(result, child); }
                }
            }

            void Tree_Disposed(Object? sender, EventArgs e)
            {
                if (sender is TreeView disposed && treeControls.ContainsKey(disposed))
                { treeControls.Remove(disposed); }
            }
        }

    }
}
