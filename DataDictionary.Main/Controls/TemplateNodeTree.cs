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

        public static Boolean TryGetNode(this TreeView tree, ITemplateNodeIndex templateNode, [NotNullWhen(true)] out TreeNode? result)
        {
            if (treeControls.ContainsKey(tree))
            {
                TemplateNodeIndex key = new TemplateNodeIndex(templateNode);

                if (treeControls[tree].Where(w => key.Equals(w.Value)).Select(s => s.Key).FirstOrDefault() is TreeNode treeNode)
                { result = treeNode; return true; }
                else { result = null; return false; }
            }
            else { result = null; return false; }
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
            TemplateIndex templateKey = new TemplateIndex(template);
            Dictionary<TreeNode, TemplateNodeValue> nodeDictionary;
            List<TemplateNodeIndex> expanded = new List<TemplateNodeIndex>();
            TemplateNodeIndex? selected = null;
            TemplateNodeIndex rootKey = new TemplateNodeIndex();

            tree.BeginUpdate();

            if (treeControls.ContainsKey(tree))
            { nodeDictionary = treeControls[tree]; }
            else
            {
                nodeDictionary = new Dictionary<TreeNode, TemplateNodeValue>();
                treeControls.Add(tree, nodeDictionary);
                tree.ImageList = NavigationExtention.CreateImageList();

                tree.Disposed += Tree_Disposed;
            }

            // Get the Selected Tree Node
            if (tree.SelectedNode is not null
                && nodeDictionary.ContainsKey(tree.SelectedNode))
            { selected = new TemplateNodeIndex(nodeDictionary[tree.SelectedNode]); }

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

            // Clear Nodes
            tree.Nodes.Clear();
            nodeDictionary.Clear();

            // Create Tree Root Node
            TreeNode root = new TreeNode(template.Title);
            root.ImageKey = template.Scope.GetName();
            root.SelectedImageKey = template.Scope.GetName();
            tree.Nodes.Add(root);

            // Add TemplateNodes to Tree Root Node
            List<TemplateNodeValue> templateRoots = nodes.
                Where(w => templateKey.Equals(w) && !owners.Any(a => new TemplateNodeIndex(w).Equals(a))).
                OrderBy(o => o.NodeOrder).
                ThenBy(o => o.NodeName).
                ToList();

            foreach (TemplateNodeValue item in templateRoots)
            { BuildNode(root, item); }

            // Handle Missing Nodes (orphans & recursive)
            List<TemplateNodeValue> templateOrphans = nodes.
                Where(w => templateKey.Equals(w) && !nodeDictionary.Any(a => new TemplateNodeIndex(w).Equals(a.Value))).
                OrderBy(o => o.NodeOrder).
                ThenBy(o => o.NodeName).
                ToList();

            foreach (TemplateNodeValue item in templateOrphans)
            {
                TreeNode newTreeNode = new TreeNode(item.NodeName);
                newTreeNode.ImageKey = item.Scope.GetName();
                newTreeNode.SelectedImageKey = item.Scope.GetName();
                newTreeNode.ForeColor = SystemColors.Highlight; // Mark node has having an issue.
                root.Nodes.Add(newTreeNode);
                nodeDictionary.Add(newTreeNode, item);
            }

            // Restore selected
            if (selected is not null
                && nodeDictionary.Where(w => selected.Equals(w.Value)).
                    Select(s => s.Key).
                    FirstOrDefault() is TreeNode treeNode)
            { tree.SelectedNode = treeNode; }

            // Restore Expanded Tree Nodes
            if (expanded.Count > 0)
            { root.ExpandParent(); }

            foreach (var item in nodeDictionary.Where(w => expanded.Any(a => a.Equals(w.Value))))
            { item.Key.ExpandParent(); }

            tree.EndUpdate();

            void BuildNode(TreeNode parent, TemplateNodeValue parentNode)
            {
                TreeNode newTreeNode = new TreeNode(parentNode.NodeName);
                newTreeNode.ImageKey = parentNode.Scope.GetName();
                newTreeNode.SelectedImageKey = parentNode.Scope.GetName();
                parent.Nodes.Add(newTreeNode);
                nodeDictionary.Add(newTreeNode, parentNode);

                foreach (TemplateNodeOwnerValue owner in owners.
                    Where(w => new TemplateNodeOwnerIndex(parentNode).Equals(w)).
                    ToList())
                {
                    foreach (TemplateNodeValue childNode in nodes.
                        Where(w => new TemplateNodeIndex((ITemplateNodeIndex)owner).Equals(w)).
                        OrderBy(o => o.NodeOrder).
                        ThenBy(o => o.NodeName).
                        ToList())
                    { BuildNode(newTreeNode, childNode); }
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
