using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            node.Expand();

            if (node.Parent is TreeNode parentNode)
            {
                parentNode.Expand();
                parentNode.ExpandParent();
            }
        }

        public static IDictionary<TreeNode, NamedScopeItem> Load(this TreeView target, NamedScopeDictionary data, Action<Int32, Int32>? progress = null)
        {
            Dictionary<TreeNode, NamedScopeItem> result = new Dictionary<TreeNode, NamedScopeItem>();

            target.Invoke(() =>
            {
                target.Enabled = false;
                target.UseWaitCursor = true;
                target.BeginUpdate();
            });

            // Progress tracker
            if (progress is null) { progress = (x, y) => { }; }
            Int32 totalWork = data.Count;
            Int32 completeWork = 0;
            progress(completeWork, totalWork);

            CreatNodes(target.Nodes, SelectItems(data.RootItem.Children));

            target.Invoke(() =>
            {
                target.EndUpdate();
                target.UseWaitCursor = false;
                target.Enabled = true;
            });

            return result;

            void CreatNodes(TreeNodeCollection targetNodes, IEnumerable<NamedScopeItem> children)
            {
                foreach (IGrouping<ScopeType, NamedScopeItem> scopeGroup in children.GroupBy(g => g.Scope).OrderBy(o => o.Key))
                {
                    TreeNodeCollection nodes = targetNodes;

                    if (scopeGroup.Count() > 1)
                    {
                        TreeNode scopeNode = target.Invoke<TreeNode>(() =>
                        {
                            TreeNode newNode = targetNodes.Add(scopeGroup.Key.ToScopeName().Split(".").Last());
                            newNode.ImageKey = scopeGroup.Key.ToScopeName();
                            newNode.SelectedImageKey = scopeGroup.Key.ToScopeName();
                            newNode.NodeFont = new Font(newNode.TreeView.Font, FontStyle.Italic);
                            newNode.ToolTipText = String.Format("set of {0}", newNode.Text);

                            nodes = newNode.Nodes;
                            return newNode;
                        });
                    }

                    foreach (NamedScopeItem item in scopeGroup.OrderBy(o => o.OrdinalPosition).ThenBy(o => o.MemberName))
                    {
                        TreeNode node = target.Invoke<TreeNode>(() =>
                        {
                            TreeNode newNode = nodes.Add(item.MemberTitle);
                            newNode.ImageKey = item.Scope.ToScopeName();
                            newNode.SelectedImageKey = item.Scope.ToScopeName();
                            result.Add(newNode, item);
                            newNode.ToolTipText = item.MemberFullName;
                            item.PropertyChanged += Item_PropertyChanged;

                            progress(completeWork++, totalWork);
                            return newNode;

                            void Item_PropertyChanged(object? sender, PropertyChangedEventArgs e)
                            { newNode.Text = item.MemberName; }
                        });

                        if (item.Children.Count > 0)
                        { CreatNodes(node.Nodes, SelectItems(item.Children)); }

                        //progress(completeWork++, totalWork);
                    }
                }
            }

            IEnumerable<NamedScopeItem> SelectItems(IEnumerable<NamedScopeKey> keys)
            {
                return keys.Select(s =>
                {
                    if (data.ContainsKey(s) && data[s] is not null) { return data[s]; }
                    else { return null; }
                }).OfType<NamedScopeItem>();
            }
        }

    }
}
