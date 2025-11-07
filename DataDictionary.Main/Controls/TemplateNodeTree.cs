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
            Dictionary<TreeNode, TemplateNodeValue> nodeDictionary;
            List<TemplateNodeIndex> expanded = new List<TemplateNodeIndex>();
            TemplateNodeIndex? selected = null;
            TemplateNodeIndex rootKey = new TemplateNodeIndex();
            List<(TemplateNodeIndex parent, TemplateNodeIndex child)> parentChild = new List<(TemplateNodeIndex parent, TemplateNodeIndex child)>();

            tree.BeginUpdate();

            if (treeControls.ContainsKey(tree))
            { nodeDictionary = treeControls[tree]; }
            else
            {
                nodeDictionary = new Dictionary<TreeNode, TemplateNodeValue>();
                treeControls.Add(tree, nodeDictionary);
                tree.Disposed += Tree_Disposed;
            }

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
            if (tree.ImageList is null)
            { tree.SetImageList(); }

            // Create Tree Root Node
            TreeNode root = new TreeNode(template.Title);
            root.ImageKey = template.Scope.GetName();
            root.SelectedImageKey = template.Scope.GetName();
            tree.Nodes.Add(root);

            //var x = nodes.Where(w => !owners.Any(a => new TemplateNodeIndex(w).Equals(a))).ToList();
            //var y = nodes.Where(w => !owners.Any(a => new TemplateNodeOwnerIndex(w).Equals(a))).ToList();

            // Add TemplateNodes to Tree Root Node
            foreach (TemplateNodeValue item in nodes.
                Where(w => !owners.Any(a => new TemplateNodeIndex(w).Equals(a))).
                OrderBy(o => o.NodeOrder).
                ThenBy(o => o.NodeName).
                ToList())
            { BuildNode(root, item); }

            //TODO: Recusrive relations not working. These are lost.
            var x = nodes.Where(w => !nodeDictionary.Any(a => new TemplateNodeIndex(w).Equals(a.Value))).ToList();

            // restore selected
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
                TreeNode result = new TreeNode(parentNode.NodeName);
                result.ImageKey = parentNode.Scope.GetName();
                result.SelectedImageKey = parentNode.Scope.GetName();
                parent.Nodes.Add(result);
                nodeDictionary.Add(result, parentNode);

                foreach (TemplateNodeOwnerValue owner in owners.
                    Where(w => new TemplateNodeOwnerIndex(parentNode).Equals(w)).
                    ToList())
                {
                    //TODO: Detect recursive relationship?
                    foreach (TemplateNodeValue childNode in nodes.
                        Where(w => new TemplateNodeIndex((ITemplateNodeIndex)owner).Equals(w)).
                        OrderBy(o => o.NodeOrder).
                        ThenBy(o => o.NodeName).
                        ToList())
                    {
                        var x = parentChild.Where(w => w.parent.Equals(parentNode) && w.child.Equals(childNode)).ToList();


                        parentChild.Add(new(new TemplateNodeIndex(parentNode), new TemplateNodeIndex(childNode)));
                        BuildNode(result, childNode);
                    }
                }
            }

            void Tree_Disposed(Object? sender, EventArgs e)
            {
                if (sender is TreeView disposed && treeControls.ContainsKey(disposed))
                { treeControls.Remove(disposed); }
            }
        }


        //TODO: Not Working. Tried to build from child up.
        public static void BuildTree_New(this TreeView tree,
            ITemplateValue template,
            IEnumerable<TemplateNodeValue> nodes,
            IEnumerable<TemplateNodeOwnerValue> owners)
        {
            Dictionary<TreeNode, TemplateNodeValue> nodeDictionary;
            List<TemplateNodeIndex> expanded = new List<TemplateNodeIndex>();
            TemplateNodeIndex? selected = null;
            TemplateNodeIndex rootKey = new TemplateNodeIndex();
            List<(TemplateNodeValue Node, TemplateNodeOwnerIndex? Owner)> templateTree = new List<(TemplateNodeValue Node, TemplateNodeOwnerIndex? Owner)>();

            // Build Tree Structure (child node up)
            foreach (var item in nodes)
            { BuildTemplateItem(new TemplateNodeIndex(item)); }

            // Start Update
            tree.BeginUpdate();

            // Add missing Tree Control and list of Nodes
            if (treeControls.ContainsKey(tree))
            { nodeDictionary = treeControls[tree]; }
            else
            {
                nodeDictionary = new Dictionary<TreeNode, TemplateNodeValue>();
                treeControls.Add(tree, nodeDictionary);
                tree.Disposed += Tree_Disposed;
            }

            // Get the Selected Template Node
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
            if (tree.ImageList is null)
            { tree.SetImageList(); }

            // Create Root Node
            TreeNode root = new TreeNode(template.Title);
            root.ImageKey = template.Scope.GetName();
            root.SelectedImageKey = template.Scope.GetName();
            tree.Nodes.Add(root);

            // Build the TreeNodes
            foreach (var templateNode in templateTree.Where(w => w.Owner is null))
            { BuildTreeNode(root, templateNode.Node, templateNode.Owner); }

            // restore selected
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

            // Complete Update
            tree.EndUpdate();

            // Builds the Template Tree, Recursive
            void BuildTemplateItem(TemplateNodeIndex nodeIndex)
            {
                List<TemplateNodeOwnerIndex> nodeOwners = owners.
                     Where(w => nodeIndex.Equals(w)).
                     Select(s => new TemplateNodeOwnerIndex((ITemplateNodeOwnerIndex)s)).
                     ToList();

                TemplateNodeValue currentNode = nodes.First(w => nodeIndex.Equals(w));

                if (nodeOwners.Count == 0)
                { templateTree.Add(new (currentNode, null)); }

                foreach (TemplateNodeOwnerIndex nodeOwner in nodeOwners)
                {
                    templateTree.Add(new (currentNode, nodeOwner));

                    if (templateTree.Count(w => nodeOwner.Equals(w.Owner) && nodeIndex.Equals(w.Node)) == 0)
                    { BuildTemplateItem(new TemplateNodeIndex(nodeOwner)); }
                }
            }

            // Builds the TreeView Nodes, Recursive
            void BuildTreeNode(TreeNode parent, TemplateNodeValue TemplateNode, TemplateNodeOwnerIndex? Owner)
            {
                TreeNode newTreeNode = new TreeNode(TemplateNode.NodeName);
                newTreeNode.ImageKey = TemplateNode.Scope.GetName();
                newTreeNode.SelectedImageKey = TemplateNode.Scope.GetName();
                parent.Nodes.Add(newTreeNode);
                nodeDictionary.Add(newTreeNode, TemplateNode);

                
            }

            void Tree_Disposed(Object? sender, EventArgs e)
            {
                if (sender is TreeView disposed && treeControls.ContainsKey(disposed))
                { treeControls.Remove(disposed); }
            }
        }

    }
}
