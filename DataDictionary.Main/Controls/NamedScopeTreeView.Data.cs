using DataDictionary.BusinessLayer;
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
            Action<Int32, Int32> progressChanged = (completed, total) => { };

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

            work.Add(new WorkItem(ref progressChanged)
            {
                WorkName = "Building TreeView Nodes",
                DoWork = () =>
                {
                    foreach (NamedScopeIndex item in treeData.RootKeys())
                    { treeControl.Invoke(() => { BuildNodes(item, treeData, progressChanged); }); }
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

        void BuildNodes(NamedScopeIndex rootIndex, INamedScopeData treeData, Action<Int32, Int32> progressChanged)
        {
            Int32 totalWork = 0;
            Int32 completedWork = 0;
            NamedScopeNode rootnode = new NamedScopeNode(treeData.GetValue(rootIndex));
            List<NamedScopeNode> values = BuildPath(rootnode, treeData).ToList();

            Dictionary<NamedScopePath, List<NamedScopeNode>> pathGroup = values.
                SelectMany(s => s.Path.Group()).
                Distinct().
                GroupJoin(values,
                    path => path,
                    node => node.Path,
                    (path, nodes) => new
                    {
                        path,
                        nodes = nodes.ToList(),
                    }).
                ToDictionary(k => k.path, v => v.nodes);

            totalWork = pathGroup.Sum(v => v.Value.Count);


            foreach (var pathItem in pathGroup.Where(w => w.Key.ParentPath is null))
            { BuildNodes(treeControl.Nodes, pathItem.Key, pathItem.Value); }

            void BuildNodes(TreeNodeCollection treeNodes, NamedScopePath path, IReadOnlyList<NamedScopeNode> values)
            {
                if (values.Count == 0)
                {   // NameSpace node
                    // Occurs at top level and when a child node has an extra level.
                    TreeNode newNode = CreateNode(path);
                    treeNodes.Add(newNode);

                    var children = pathGroup.
                        Where(w => path.Equals(w.Key.ParentPath)).
                        ToDictionary(k => k.Key, v => v.Value);

                    foreach (var item in children)
                    { BuildNodes(newNode.Nodes, item.Key, item.Value); }
                }
                else if (values.Count == 1)
                {   // Single NameSpace, Single Node.
                    // NameSpace does uniquely identify the object
                    NamedScopeNode value = values.First();
                    TreeNode newNode = CreateNode(values.First());
                    treeNodes.Add(newNode);

                    BuildScopeNodes(newNode, value);
                }
                else
                {   // Multiple Object have the Same NameSpace
                    // An overloaded method in .Net falls into this category.
                    // Model Items (like Attributes) have the same NameSpace
                    foreach (var value in values)
                    {
                        TreeNode newNode = CreateNode(value);
                        treeNodes.Add(newNode);

                        BuildScopeNodes(newNode, value);
                    }
                }

                progressChanged(completedWork++, totalWork);
            }

            void BuildScopeNodes(TreeNode newNode, NamedScopeNode value)
            {
                // Children by Path
                var children = pathGroup.
                    Where(w => value.Path.Equals(w.Key.ParentPath)).
                    ToDictionary(k => k.Key, v => v.Value);

                // Children by DataIndex
                var childByIndex = children.
                    Select(s => new
                    {
                        s.Key,
                        Value = s.Value.
                            Where(w => w.Parent is not null
                                && value.DataIndex.Equals(w.Parent.DataIndex)).
                            ToList()
                    }).
                    Where(w => w.Value.Count > 0).
                    ToDictionary(k => k.Key, v => v.Value);

                if (childByIndex.Count > 0)
                { children = childByIndex; }

                // Children with Scopes to be nested
                // Mostly Database Objects
                var scopeGroups = children.
                    SelectMany(s => s.Value, (children, child) => new { child.Scope, Children = children }).
                    Distinct().
                    GroupBy(g => g.Scope).
                    ToList();

                if (scopeGroups.Count == 0)
                {   // No Scope grouping
                    foreach (var item in children)
                    { BuildNodes(newNode.Nodes, item.Key, item.Value); }
                }

                foreach (var scopeGroup in scopeGroups)
                {
                    ImageEnumeration scopeValue = ImageEnumeration.Cast(scopeGroup.Key);
                    TreeNode scopeNode = newNode;

                    if (scopeValue.GroupBy && scopeGroup.Count() > 1)
                    {   // Scope is to be grouped, make that group and use it.
                        scopeNode = CreateNode(scopeGroup.Key);
                        newNode.Nodes.Add(scopeNode);
                    }

                    foreach (var scopeItem in scopeGroup)
                    { BuildNodes(scopeNode.Nodes, scopeItem.Children.Key, scopeItem.Children.Value); }
                }
            }
        }

        IReadOnlyList<NamedScopeNode> BuildPath(NamedScopeNode parent, INamedScopeData treeData)
        {
            List<NamedScopeNode> result = new List<NamedScopeNode>();
            result.Add(parent);

            List<INamedScopeValue> nodes = treeData.
                ChildrenKeys(parent.NamedScope.Index).
                Select(s => treeData.GetValue(s)).
                OrderBy(o => o.Scope).
                ThenBy(o => o.OrdinalPosition).
                ThenBy(o => o.Title).
                ToList();

            foreach (var item in nodes)
            {
                NamedScopeNode newNode = new NamedScopeNode(parent, item);
                result.AddRange(BuildPath(newNode, treeData));
            }

            return result;
        }

        TreeNode CreateNode(NamedScopeNode value)
        {
            ImageEnumeration scopeImage = ImageEnumeration.Cast(value.Scope);
            TreeNode result = new TreeNode(value.Title);

            result.ImageKey = scopeImage.Name;
            result.SelectedImageKey = scopeImage.Name;
            result.ToolTipText = value.NamedScope.Path.MemberFullPath;
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
