﻿using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
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
        /// <returns></returns>
        public static IEnumerable<WorkItem> Load(this TreeView target)
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
                                    (w.Key.IsExpanded && BusinessData.NamedScope.ContainsKey(w.Value))
                                    || (w.Key.Nodes.Count == 0
                                        && w.Key.Parent is not null
                                        && w.Key.Parent.IsExpanded)
                                        && BusinessData.NamedScope.ContainsKey(w.Value)).
                                        Select(s => s.Value). // Get the NamedScope Index
                                        Distinct().
                                        Select(s => BusinessData.NamedScope.GetData(s).Index)); // Translate to DataLayer Index
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
            //TODO: Can individual add/remove also update NamedScope?
            result.AddRange(BusinessData.LoadNamedScope());

            // Build the Tree
            result.Add(new WorkItem()
            {
                WorkName = "Build Tree",
                DoWork = () =>
                {
                    var x = BusinessData.NamedScope.RootKeys();
                    target.Invoke(() =>
                    { CreateNodes(target.Nodes, BusinessData.NamedScope.RootKeys()); });
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
                            Where(w => BusinessData.NamedScope.ContainsKey(w.Value)
                                && item.Equals(BusinessData.NamedScope.GetData(w.Value).Index)).
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


            void CreateNodes_Proto(TreeNodeCollection targetNodes, IEnumerable<NamedScopeIndex> children)
            {
                var childValues = children.
                    Select(s => BusinessData.NamedScope.GetValue(s)).
                    ToList();

                var paths = childValues.
                    SelectMany(s => s.Path.Group()).
                    Distinct().
                    ToList();

                var childPaths = paths.
                    GroupJoin(childValues,
                        path => path,
                        child => child.Path,
                        (path, children) => new
                        {
                            path,
                            scopes = children.
                                GroupBy(g => g.Scope).
                                Select(s => new
                                {
                                    scope = s.Key,
                                    values = s.Select(v => v).ToList()
                                }).
                                OrderBy(o => o.scope).
                                ToList()
                        }).
                    OrderBy(o => o.path).
                    ToList();

                TreeNodeCollection nodes = targetNodes;
                foreach (var pathNodes in childValues.Select(s => s.Source.Path).Distinct())
                {
                    nodes.Add(CreatePath(pathNodes));
                    // TODO: Duplicate paths are still showing up. Sometimes as an extra level.
                    // Partially caused by naming conventions. EI: [Database].[Schema].[Table].[Column]
                    // But this should be something that can be worked out.
                    // Maybe loop threw all the paths instead of just the ones for this node? (higher Level)

                }

                TreeNode CreatePath(NamedScopePath path)
                {
                    TreeNode? result = null;

                    var items = childValues.Where(w => path.Equals(w.Source.Path)).ToList();

                    if (path.ParentPath is NamedScopePath parent)
                    { result = CreatePath(parent); }

                    if (result is null && items.Count == 1)
                    {
                        result = CreateNodeByValue(items[0]);

                        if (BusinessData.NamedScope.ChildrenKeys(items[0].Index) is IList<NamedScopeIndex> values && values.Count > 0)
                        { CreateNodes(result.Nodes, values); }

                        return result;
                    }
                    else
                    {
                        if (result is null)
                        { result = CreateNodeByPath(path); }

                        foreach (var scopeGroup in items.
                            Select(s => s.Scope).
                            Distinct().
                            OrderBy(o => ImageEnumeration.Cast(o).DisplayName))
                        {
                            TreeNode scopeNode = result;
                            ImageEnumeration scopeValue = ImageEnumeration.Cast(scopeGroup);
                            if (scopeValue.GroupBy && items.Count(w => w.Scope == scopeGroup) > 1)
                            {
                                scopeNode = CreateNodeByScope(scopeGroup);
                                result.Nodes.Add(scopeNode);
                            }

                            foreach (var item in items.
                                Where(w => w.Scope == scopeGroup).
                                OrderBy(o => o.OrdinalPosition).
                                ThenBy(o => o.Title))
                            {
                                TreeNode newNode = CreateNodeByValue(item);
                                scopeNode.Nodes.Add(newNode);

                                if (BusinessData.NamedScope.ChildrenKeys(item.Index) is IList<NamedScopeIndex> values && values.Count > 0)
                                { CreateNodes(newNode.Nodes, values); }
                            }

                        }

                        return result;
                    }
                }




            }



            void CreateNodes(TreeNodeCollection targetNodes, IEnumerable<NamedScopeIndex> children)
            {



                foreach (IGrouping<ScopeType, NamedScopeIndex> scopeGroup in children.
                    GroupBy(g => BusinessData.NamedScope.GetValue(g).Scope).
                    OrderBy(o => o.Key))
                {
                    TreeNodeCollection nodes = targetNodes;
                    ImageEnumeration scopeValue = ImageEnumeration.Cast(scopeGroup.Key);

                    // Build Scope Groups
                    if (scopeGroup.Count() > 1 && scopeValue.GroupBy)
                    {
                        TreeNode newNode = targetNodes.Add(scopeValue.Name.Split(".").Last());
                        newNode.ImageKey = scopeValue.Name;
                        newNode.SelectedImageKey = scopeValue.Name;
                        newNode.NodeFont = new Font(newNode.TreeView.Font, FontStyle.Italic);
                        newNode.ToolTipText = String.Format("set of {0}", newNode.Text);

                        nodes = newNode.Nodes;
                    }

                    // Build Data Nodes
                    foreach (NamedScopeIndex item in scopeGroup.
                        OrderBy(o => BusinessData.NamedScope.GetValue(o).OrdinalPosition).
                        ThenBy(o => BusinessData.NamedScope.GetValue(o).Title))
                    {
                        INamedScopeValue value = BusinessData.NamedScope.GetValue(item);
                        ImageEnumeration valueScope = ImageEnumeration.Cast(value.Scope);

                        TreeNode newNode = nodes.Add(value.Title);
                        newNode.ImageKey = valueScope.Name;
                        newNode.SelectedImageKey = valueScope.Name;
                        newNode.ToolTipText = value.Path.MemberFullPath;
                        value.OnTitleChanged += TreeViewExtension_OnTitleChanged; ;
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
                                    nodeItem.Key.ToolTipText = value.Path.MemberFullPath;
                                }
                            }
                        }

                        if (BusinessData.NamedScope.ChildrenKeys(item) is IList<NamedScopeIndex> values && values.Count > 0)
                        { CreateNodes(newNode.Nodes, values); }
                    }
                }
            }


            TreeNode CreateNodeByValue(INamedScopeValue source)
            {
                TreeNode result = new TreeNode(source.Title);
                ImageEnumeration scopeImage = ImageEnumeration.Cast(source.Scope);

                result.ImageKey = scopeImage.Name;
                result.SelectedImageKey = scopeImage.Name;
                result.ToolTipText = source.Path.MemberFullPath;
                source.OnTitleChanged += (source, eventArg) =>
                {
                    if (source is INamedScopeValue item)
                    {
                        result.Text = item.Title;
                        result.ToolTipText = item.Path.MemberFullPath;
                    }
                };

                treeNodeDictionary[target].Add(result, source.Index);
                return result;
            }

            TreeNode CreateNodeByPath(NamedScopePath source)
            {
                TreeNode result = new TreeNode(source.Member);
                ImageEnumeration scopeImage = ImageEnumeration.Cast(ScopeType.ModelNameSpace);

                result.ImageKey = scopeImage.Name;
                result.SelectedImageKey = scopeImage.Name;
                result.ToolTipText = source.MemberFullPath;

                return result;
            }

            TreeNode CreateNodeByScope(ScopeType source)
            {
                ImageEnumeration scopeImage = ImageEnumeration.Cast(source);
                TreeNode result = new TreeNode(scopeImage.Name.Split(".").Last());

                result.ImageKey = scopeImage.Name;
                result.SelectedImageKey = scopeImage.Name;
                result.NodeFont = new Font(target.Font, FontStyle.Italic);
                result.ToolTipText = String.Format("set of {0}", scopeImage.Name);

                return result;
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
