using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Forms.Scripting
{
    static class TemplateNodeTree
    {
        static Dictionary<TreeView, Dictionary<TreeNode, TemplateNodeValue>> treeControls = new Dictionary<TreeView, Dictionary<TreeNode, TemplateNodeValue>>();

        public static Boolean TryGetValue(this TreeNode node, out TemplateNodeValue? templateNode)
        {
            if (node.TreeView is TreeView tree && treeControls.ContainsKey(tree) && treeControls[tree].TryGetValue(node, out TemplateNodeValue? result))
            { templateNode = result; return true; }
            else { templateNode = null; return false; }
        }

        public static void BuildTree(this TreeView tree,
            ITemplateValue template,
            IEnumerable<TemplateNodeValue> nodes,
            IEnumerable<TemplateNodeOwnerValue> owners)
        {
            Dictionary<TreeNode, TemplateNodeValue> nodeDictionary;

            tree.BeginUpdate();
            tree.Disposed += Tree_Disposed;

            if (treeControls.ContainsKey(tree))
            { nodeDictionary = treeControls[tree]; }
            else
            {
                nodeDictionary = new Dictionary<TreeNode, TemplateNodeValue>();
                treeControls.Add(tree, nodeDictionary);
            }

            tree.Nodes.Clear();
            if (tree.ImageList is null)
            { tree.SetImageList(); }

            TreeNode root = new TreeNode(template.Title);
            root.ImageKey = template.Scope.GetName();
            root.SelectedImageKey = template.Scope.GetName();
            tree.Nodes.Add(root);

            foreach (TemplateNodeValue item in nodes.Where(w => !owners.Any(a => new TemplateNodeIndex(w).Equals(a))).ToList())
            { BuildNode(root, item); }

            tree.EndUpdate();

            void BuildNode(TreeNode parent, TemplateNodeValue templateNode)
            {
                TreeNode result = new TreeNode(templateNode.NodeName);
                result.ImageKey = templateNode.Scope.GetName();
                result.SelectedImageKey = templateNode.Scope.GetName();
                parent.Nodes.Add(result);
                nodeDictionary.Add(result, templateNode);

                foreach (TemplateNodeOwnerValue owner in owners.Where(w => new TemplateNodeOwnerIndex(templateNode).Equals(w)).ToList())
                {
                    foreach (TemplateNodeValue child in nodes.Where(w => new TemplateNodeIndex(owner).Equals(w)))
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
