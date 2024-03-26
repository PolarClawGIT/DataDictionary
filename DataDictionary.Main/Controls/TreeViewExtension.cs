using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

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

        static Dictionary<TreeView, Dictionary<TreeNode, NamedScopeItem>> treeNodes = new Dictionary<TreeView, Dictionary<TreeNode, NamedScopeItem>>();

        public static IEnumerable<WorkItem> Load(this TreeView target, NamedScopeDictionary data)
        {
            List<WorkItem> result = new List<WorkItem>();
            List<NamedScopeKey> expandedNodes = new List<NamedScopeKey>();
            Action<Int32, Int32> progress = (x, y) => { };
            Int32 totalWork = data.Count;
            Int32 completeWork = 0;

            if (!treeNodes.ContainsKey(target))
            { treeNodes.Add(target, new Dictionary<TreeNode, NamedScopeItem>()); }

            result.Add(new WorkItem()
            {
                WorkName = "Store Expanded Nodes",
                DoWork = () =>
                {
                    target.Invoke(() =>
                    {
                        expandedNodes.AddRange(treeNodes[target]
                        .Where(w => w.Key.IsExpanded
                            || (w.Key.Nodes.Count == 0
                                && w.Key.Parent is not null
                                && w.Key.Parent.IsExpanded))
                        .Select(s => new NamedScopeKey(s.Value)));

                        treeNodes[target].Clear();
                    });
                }
            });

            result.Add(new WorkItem()
            {
                WorkName = "Disable and Clear the Tree",
                DoWork = () =>
                {
                    target.Invoke(() =>
                    {
                        target.Enabled = false;
                        target.UseWaitCursor = true;
                        target.BeginUpdate();
                        target.Nodes.Clear();
                    });
                }
            });

            WorkItem buildTree = new WorkItem()
            {
                WorkName = "Build Tree",
                DoWork = () =>
                { CreatNodes(target.Nodes, SelectItems(data.RootItem.Children)); }
            };
            progress = buildTree.OnProgressChanged;
            result.Add(buildTree);

            result.Add(new WorkItem()
            {
                WorkName = "Expand Tree Nodes",
                DoWork = () => target.Invoke(() =>
                {
                    foreach (NamedScopeKey item in expandedNodes)
                    {
                        var node = treeNodes[target].FirstOrDefault(w => item.Equals(w.Value));
                        if (node.Key is not null && !node.Key.IsExpanded)
                        { node.Key.ExpandParent(); }
                    }
                })
            });

            result.Add(new WorkItem()
            {
                WorkName = "Enable Tree",
                DoWork = () =>
                {
                    target.Invoke(() =>
                    {
                        target.EndUpdate();
                        target.UseWaitCursor = false;
                        target.Enabled = true;
                    });
                }
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
                            newNode.ToolTipText = item.MemberFullName;
                            item.PropertyChanged += Item_PropertyChanged;
                            treeNodes[target].Add(newNode, item);

                            progress(completeWork++, totalWork);
                            return newNode;

                            void Item_PropertyChanged(object? sender, PropertyChangedEventArgs e)
                            { newNode.Text = item.MemberName; }
                        });


                        if (item.Children.Count > 0)
                        { CreatNodes(node.Nodes, SelectItems(item.Children)); }
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

        public static NamedScopeItem? GetItem(this TreeNode source)
        {
            if (treeNodes.ContainsKey(source.TreeView) && treeNodes[source.TreeView].ContainsKey(source))
            { return treeNodes[source.TreeView][source]; }
            else { return null; }
        }
    }
}
