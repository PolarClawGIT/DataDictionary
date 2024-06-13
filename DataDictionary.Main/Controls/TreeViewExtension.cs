using DataDictionary.BusinessLayer;
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
        /// Used to hold the cross reference between the TreeNode and the NamedScope. Each tree has its own item.
        /// </summary>
        static Dictionary<TreeView, Dictionary<TreeNode, NamedScopeIndex>> treeNodeDictionary = new Dictionary<TreeView, Dictionary<TreeNode, NamedScopeIndex>>();

        /// <summary>
        /// Creates work items to load the target TreeView with the data from NameScope.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IEnumerable<WorkItem> Load(this TreeView target, INamedScopeData data)
        {
            List<WorkItem> result = new List<WorkItem>();
            List<DataLayerIndex> expandedNodes = new List<DataLayerIndex>();
            Dictionary<TreeNode, NamedScopeIndex> valueNodes = new Dictionary<TreeNode, NamedScopeIndex>();

            if (treeNodeDictionary.ContainsKey(target))
            { valueNodes = treeNodeDictionary[target]; }
            else
            {
                treeNodeDictionary.Add(target, valueNodes);
                target.Disposed += TreeViewDisposed;
            }

            // Store Expanded Nodes by Key
            result.Add(new WorkItem()
            {
                WorkName = "Store Expanded Nodes",
                DoWork = () =>
                {
                    target.Invoke(() =>
                    {
                        expandedNodes.AddRange(valueNodes.Where(w => 
                                    (w.Key.IsExpanded && data.ContainsKey(w.Value))
                                    || (w.Key.Nodes.Count == 0
                                        && w.Key.Parent is not null
                                        && w.Key.Parent.IsExpanded)
                                        && data.ContainsKey(w.Value)).
                                        Select(s => s.Value). // Get the NamedScope Index
                                        Distinct().
                                        Select(s => data.GetData(s).Index)); // Translate to DataLayer Index
                        valueNodes.Clear();
                    });
                }
            });

            // Clear the Tree
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
                        target.Nodes.RemoveAll();
                    });
                }
            });

            //Reload the NamedScope
            result.AddRange(BusinessData.LoadNamedScope());

            // Build the Tree
            result.Add(new WorkItem()
            {
                WorkName = "Build Tree",
                DoWork = () =>
                {
                    target.Invoke(() =>
                    { CreateNodes(target.Nodes, data.RootKeys()); });
                }
            });

            // Expanded the Nodes
            result.Add(new WorkItem()
            {
                WorkName = "Expand Tree Nodes",
                DoWork = () => target.Invoke(() =>
                {
                    foreach (KeyValuePair<TreeNode, NamedScopeIndex> node in expandedNodes.
                        SelectMany(item => valueNodes.
                            Where(w => data.ContainsKey(w.Value) && item.Equals(data.GetData(w.Value).Index)).
                            Where(node => node.Key is not null && !node.Key.IsExpanded)))
                    { node.Key.ExpandParent(); }
                })
            });

            // Enable the Tree
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

            void TreeViewDisposed(Object? sender, EventArgs e)
            {
                if (treeNodeDictionary.ContainsKey(target))
                { treeNodeDictionary.Remove(target); }

                target.Disposed -= TreeViewDisposed; // Only need to call it once
            }

            void CreateNodes(TreeNodeCollection targetNodes, IEnumerable<NamedScopeIndex> children)
            {
                foreach (IGrouping<ScopeType, NamedScopeIndex> scopeGroup in children.GroupBy(g => data.GetValue(g).Scope).OrderBy(o => o.Key))
                {
                    TreeNodeCollection nodes = targetNodes;

                    // Build Scope Groups
                    if (scopeGroup.Count() > 1 && scopeGroup.Key.Setting().GroupByScope)
                    {
                        TreeNode newNode = targetNodes.Add(scopeGroup.Key.ToName().Split(".").Last());
                        newNode.ImageKey = scopeGroup.Key.ToName();
                        newNode.SelectedImageKey = scopeGroup.Key.ToName();
                        newNode.NodeFont = new Font(newNode.TreeView.Font, FontStyle.Italic);
                        newNode.ToolTipText = String.Format("set of {0}", newNode.Text);

                        nodes = newNode.Nodes;
                    }

                    // Build Data Nodes
                    foreach (NamedScopeIndex item in scopeGroup.OrderBy(o => data.GetValue(o).OrdinalPosition).ThenBy(o => data.GetValue(o).Title))
                    {
                        TreeNode newNode = nodes.Add(data.GetValue(item).Title);
                        newNode.ImageKey = data.GetValue(item).Scope.ToName();
                        newNode.SelectedImageKey = data.GetValue(item).Scope.ToName();
                        newNode.ToolTipText = data.GetValue(item).NamedPath.MemberFullPath;
                        data.GetValue(item).OnTitleChanged += TreeViewExtension_OnTitleChanged; ;
                        valueNodes.Add(newNode, item);

                        // Handle 
                        void TreeViewExtension_OnTitleChanged(Object? sender, EventArgs e)
                        {
                            if (sender is INamedScopeValue value)
                            {

                                if (valueNodes.FirstOrDefault(w => value.Index.Equals(w.Value))
                                    is KeyValuePair<TreeNode, NamedScopeIndex> nodeItem
                                    && nodeItem.Key is not null)
                                {
                                    nodeItem.Key.Text = value.Title;
                                    nodeItem.Key.ToolTipText = value.NamedPath.MemberFullPath;
                                }
                            }
                        }

                        if (data.ChildrenKeys(item) is IList<NamedScopeIndex> values && values.Count > 0)
                        { CreateNodes(newNode.Nodes, values); }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the NameScope from the TreeNode
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static INamedScopeSourceValue? GetNamedScope(this TreeNode source)
        {
            if (treeNodeDictionary.ContainsKey(source.TreeView) && treeNodeDictionary[source.TreeView].ContainsKey(source))
            {
                NamedScopeIndex index = treeNodeDictionary[source.TreeView][source];
                INamedScopeSourceValue result = BusinessData.NamedScope.GetData(index);
                return result;
            }
            else { return null; }
        }
    }
}
