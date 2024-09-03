﻿using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.Main.Controls
{
    class NamedScopeTreeViewData
    {
        TreeView treeControl;

        class NamedScopeNode
        {
            public NamedScopeNode? Parent { get; init; } = null;
            public INamedScopeValue NamedScope { get; init; }
            public NamedScopePath Path { get; init; }
            public DataLayerIndex DataIndex { get { return NamedScope.Source.Index; } }
            public NamedScopeIndex ScopeIndex { get { return NamedScope.Index; } }
            public ScopeType Scope { get { return NamedScope.Scope; } }
            public String Title { get { return NamedScope.Title; } }
            public Int32 OrdinalPosition { get { return NamedScope.OrdinalPosition; } }

            public NamedScopeNode(INamedScopeValue value)
            {
                NamedScope = value;
                Path = value.Path;
            }

            public NamedScopeNode(NamedScopeNode parent, INamedScopeValue value) : this(value)
            {
                Parent = parent;
                Path = value.Path.Merge(parent.Path);
            }

            public override String ToString()
            { return Path.MemberFullPath; }

        }

        Dictionary<TreeNode, NamedScopeNode> treeValues = new Dictionary<TreeNode, NamedScopeNode>();
        List<DataLayerIndex> expandedIndexes = new List<DataLayerIndex>();

        public NamedScopeTreeViewData(TreeView tree)
        { treeControl = tree; }

        public INamedScopeSourceValue? GetValue(TreeNode node)
        {
            if (treeValues.TryGetValue(node, out NamedScopeNode? value))
            { return value.NamedScope.Source; }
            else { return null; }
        }

        public IEnumerable<WorkItem> BeginUpdate()
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem()
            {
                WorkName = "Begin TreeView Update",
                DoWork = () =>
                {
                    treeControl.Invoke(() =>
                    {
                        // Lock Tree
                        treeControl.Enabled = false;
                        treeControl.UseWaitCursor = true;
                        treeControl.BeginUpdate();

                        //Save Expended State
                        expandedIndexes.Clear();
                        expandedIndexes.AddRange(treeValues.Where(w =>
                                    (w.Key.IsExpanded && treeValues.Any(a => w.Value.NamedScope.Index.Equals(a.Value.ScopeIndex))
                                    || (w.Key.Nodes.Count == 0
                                        && w.Key.Parent is not null
                                        && w.Key.Parent.IsExpanded)
                                        && treeValues.Any(a => w.Value.ScopeIndex.Equals(a.Value.ScopeIndex)))).
                                        Select(s => s.Value). // Get the NamedScope Index
                                        Distinct().
                                        Select(s => s.DataIndex)); // Translate to DataLayer Index
                    });
                }
            });

            return work;
        }

        public IEnumerable<WorkItem> EndUpdate()
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem()
            {
                WorkName = "Begin TreeView Update",
                DoWork = () =>
                {
                    treeControl.Invoke(() =>
                    {
                        // Restore Expanded State
                        foreach (var item in expandedIndexes.
                            Join(treeValues.
                                Where(w => !w.Key.IsExpanded),
                                index => index,
                                node => node.Value.DataIndex,
                            (index, node) => node.Key))
                        { item.ExpandParent(); }

                        // Unlock Tree
                        treeControl.EndUpdate();
                        treeControl.UseWaitCursor = false;
                        treeControl.Enabled = true;
                    });
                }
            });

            return work;
        }

        public IEnumerable<WorkItem> BuildNodes(INamedScopeData treeData)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem()
            {
                WorkName = "Clear TreeView Nodes",
                DoWork = () =>
                {
                    treeControl.Invoke(() =>
                    {
                        ClearNodes(treeControl.Nodes);
                    });
                }
            });

            work.Add(new WorkItem()
            {
                WorkName = "Building TreeView Nodes",
                DoWork = () =>
                {
                    foreach (NamedScopeIndex item in treeData.RootKeys())
                    { treeControl.Invoke(() => { BuildNodes(item, treeData); }); }
                }
            });

            return work;

            void ClearNodes(TreeNodeCollection nodes)
            {
                while (nodes.Count > 0)
                {
                    TreeNode item = nodes[0];
                    ClearNodes(item.Nodes);
                    item.Remove();
                }
            }
        }



        void BuildNodes(NamedScopeIndex rootIndex, INamedScopeData treeData)
        {
            List<NamedScopeNode> nodePaths = new List<NamedScopeNode>();
            NamedScopeNode rootnode = new NamedScopeNode(treeData.GetValue(rootIndex));
            nodePaths.Add(rootnode);

            nodePaths.AddRange(BuildPath(rootnode, treeData));

            Dictionary<NamedScopePath, List<NamedScopeNode>> pathGroup = nodePaths.
                SelectMany(s => s.Path.Group()).
                Distinct().
                OrderBy(o => o).
                GroupJoin(nodePaths,
                    path => path,
                    node => node.Path,
                    (path, nodes) => new
                    {
                        path,
                        nodes = nodes.
                            OrderBy(o => o.OrdinalPosition).
                            ThenBy(o => o.Title).
                            ToList()
                    }).
                ToDictionary(k => k.path, e => e.nodes);



            foreach (var pathItem in pathGroup.Where(w => w.Key.ParentPath is null))
            {
                BuildTree(pathItem, treeControl.Nodes);
            }

            IReadOnlyList<NamedScopeNode> BuildPath(NamedScopeNode parent, INamedScopeData treeData)
            {
                List<NamedScopeNode> result = new List<NamedScopeNode>();

                List<INamedScopeValue> nodes = treeData.
                    ChildrenKeys(parent.NamedScope.Index).
                    Select(s => treeData.GetValue(s)).
                    OrderBy(o => o.OrdinalPosition).
                    ThenBy(o => o.Title).
                    ToList();

                foreach (var item in nodes)
                {
                    NamedScopeNode newNode = new NamedScopeNode(parent, item);
                    result.Add(newNode);
                    result.AddRange(BuildPath(newNode, treeData));
                }

                return result;
            }

            void BuildTree(KeyValuePair<NamedScopePath, List<NamedScopeNode>> pathItem, TreeNodeCollection nodes)
            {
                NamedScopePath path = pathItem.Key;
                List<NamedScopeNode> values = pathItem.Value;
                TreeNode newNode;

                if (values.Count == 0)
                {
                    newNode = CreateNode(path);
                    nodes.Add(newNode);
                }
                else if (values.Count == 1)
                {
                    newNode = CreateNode(values[0]);
                    nodes.Add(newNode);
                }
                else
                {
                    // Deal with Scopes?
                    newNode = CreateNode(path); 
                    nodes.Add(newNode);

                    foreach (var item in values)
                    { newNode.Nodes.Add(CreateNode(item)); }
                }

                foreach (var item in pathGroup.
                    Where(w => path.Equals(w.Key.ParentPath)))
                { BuildTree(item, newNode.Nodes); }
            }



            //TODO: Build logic. 

            //var x = values.
            //    SelectMany(s => s.Path.Group()).
            //    Distinct().
            //    GroupJoin(values,
            //        path => path,
            //        node => node.Path,
            //        (path, nodes) => new { path, nodes = nodes.Select(s => s).ToList() }).
            //    OrderBy(o => o.path).
            //    ToList();

            // Build list of all NameSpaces within rootIndex.
            // NameSpace by itself is not sufficient.
            // It also needs to be aware of parent/child.
            // Maybe build extended paths?
            //
            // Parent may contain the path equal to the child parent path.
            // Example: parent = [Database], child = [Database].[Schema]
            //
            // Or parent may not contain the path.
            // Example: parent = [Subject], child = [entity]



        }



        TreeNode CreateNode(NamedScopeNode value)
        {
            ImageEnumeration scopeImage = ImageEnumeration.Cast(value.Scope);
            TreeNode result = new TreeNode(value.Title);

            result.ImageKey = scopeImage.Name;
            result.SelectedImageKey = scopeImage.Name;
            result.ToolTipText = value.Path.MemberFullPath;
            value.NamedScope.OnTitleChanged += (source, eventArg) =>
            {
                if (source is INamedScopeValue item)
                {
                    result.Text = item.Title;
                    result.ToolTipText = item.Path.MemberFullPath;
                }
            };

            treeValues.Add(result, value);
            return result;
        }

        TreeNode CreateNode(NamedScopePath path)
        {
            ImageEnumeration scopeImage = ImageEnumeration.Cast(ScopeType.ModelNameSpace);
            TreeNode result = new TreeNode(path.Member);

            result.ImageKey = scopeImage.Name;
            result.SelectedImageKey = scopeImage.Name;
            result.NodeFont = new Font(treeControl.Font, FontStyle.Italic);
            result.ToolTipText = path.MemberFullPath;

            return result;
        }

        TreeNode CreateNode(ScopeType scope)
        {
            ImageEnumeration scopeImage = ImageEnumeration.Cast(scope);
            TreeNode result = new TreeNode(scopeImage.Name.Split(".").Last());

            result.ImageKey = scopeImage.Name;
            result.SelectedImageKey = scopeImage.Name;
            result.NodeFont = new Font(treeControl.Font, FontStyle.Italic);
            result.ToolTipText = String.Format("set of {0}", scopeImage.Name);

            return result;
        }

    }
}
