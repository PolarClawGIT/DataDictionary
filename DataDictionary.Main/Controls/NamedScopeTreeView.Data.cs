using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
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
            public PathIndex Path { get; init; }
            public DataIndex DataIndex { get { return NamedScope.Source.Index; } }
            public NamedScopeIndex ScopeIndex { get { return NamedScope.Index; } }
            public ScopeType Scope { get { return NamedScope.Scope; } }
            public String Title { get { return NamedScope.Title; } }
            public Int32 OrdinalPosition { get { return NamedScope.OrdinalPosition; } }

            public Boolean GroupBy
            {   
                get
                {
                    if (Scope.TryGetValue(out INavigationValue? value))
                    { return value.GroupBy; }
                    else { return false; }
                }
            }

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
        List<DataIndex> expandedIndexes = new List<DataIndex>();

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

        //TODO: Having performance issues building Nodes for a Model such as AdventureWorks
        // To Replicate, import the database from Information Schema.
        // Does not seem to have an impact when the database is loaded.
        // The Information Schema is being worked on so this has been put on hold.

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
            NamedScopeNode rootNode = new NamedScopeNode(treeData.GetValue(rootIndex));
            List<NamedScopeNode> values = BuildPath(rootNode, treeData).ToList();

            Dictionary<PathIndex, List<NamedScopeNode>> pathGroup = values.
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

            totalWork = pathGroup.Sum(v => v.Value.Count) + pathGroup.Count;

            BuildChildren(treeControl.Nodes, null);

            void BuildChildren(TreeNodeCollection treeNodes, PathIndex? path, ScopeType? scope = null)
            {
                foreach (var item in pathGroup.
                    Where(w =>
                        ((path is null && w.Key.ParentPath is null) ||
                         (path is not null && path.Equals(w.Key.ParentPath)) &&
                        ((scope is null || w.Value.Count == 0) ||
                         (scope is not null &&
                            w.Value.Count > 0 &&
                            w.Value.Any(a => a.Scope.Equals(scope)))
                        ))))
                {
                    TreeNode newNode;

                    if (item.Value.Count == 0)
                    {
                        newNode = CreateNode(item.Key);
                        treeNodes.Add(newNode);
                        completedWork = completedWork + 1;

                        BuildChildren(newNode.Nodes, item.Key);
                    }
                    else
                    {
                        foreach (var node in item.Value)
                        {
                            TreeNode groupNode = CreateNode(node);
                            treeNodes.Add(groupNode);
                            completedWork = completedWork + 1;

                            // TODO: Group By option may not be working as expected. Need more checking.
                            var scopes = pathGroup.
                               Where(w => node.Path.Equals(w.Key.ParentPath) && node.GroupBy).
                               SelectMany(s => s.Value).
                               GroupBy(g => g.Scope).
                               Select(s => s.Key).
                               ToList();

                            if (scopes.Count is 0 or 1)
                            { BuildChildren(groupNode.Nodes, node.Path); }
                            else
                            {
                                foreach (var scopeItem in scopes)
                                {
                                    TreeNode scopeNode = CreateNode(scopeItem);
                                    groupNode.Nodes.Add(scopeNode);

                                    BuildChildren(scopeNode.Nodes, node.Path, scopeItem);
                                }
                            }
                        }
                    }
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
            TreeNode result = new TreeNode(value.Title);

            result.ImageKey = value.Scope.GetEnumeration().Name;
            result.SelectedImageKey = value.Scope.GetEnumeration().Name;
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

        TreeNode CreateNode(PathIndex path)
        {
            TreeNode result = new TreeNode(path.Member);

            result.ImageKey = ScopeType.ModelNameSpace.GetEnumeration().Name;
            result.SelectedImageKey = ScopeType.ModelNameSpace.GetEnumeration().Name;
            result.NodeFont = new Font(treeControl.Font, FontStyle.Italic);
            result.ToolTipText = path.MemberFullPath;

            return result;
        }

        TreeNode CreateNode(ScopeType scope)
        {
            TreeNode result = new TreeNode(scope.GetEnumeration().Name.Split(".").Last());

            result.ImageKey = scope.GetEnumeration().Name;
            result.SelectedImageKey = scope.GetEnumeration().Name;
            result.NodeFont = new Font(treeControl.Font, FontStyle.Italic);
            result.ToolTipText = String.Format("set of {0}", scope.GetEnumeration().Name);

            return result;
        }

    }
}
