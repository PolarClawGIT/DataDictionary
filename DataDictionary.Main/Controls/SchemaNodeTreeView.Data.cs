using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using Toolbox.Threading;

namespace DataDictionary.Main.Controls
{
    class SchemaNodeTreeViewData
    {
        TreeView treeControl;

        Dictionary<TreeNode, XmlBuilderIndex> treeValues = new Dictionary<TreeNode, XmlBuilderIndex>();
        List<XmlBuilderIndex> expandedIndexes = new List<XmlBuilderIndex>();

        public SchemaNodeTreeViewData(TreeView tree)
        { treeControl = tree; }

        public XmlBuilderIndex? GetValue(TreeNode node)
        {
            if (treeValues.TryGetValue(node, out XmlBuilderIndex? value))
            { return value; }
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
                                    (w.Key.IsExpanded && treeValues.Any(a => w.Value.Equals(a.Value))
                                    || (w.Key.Nodes.Count == 0
                                        && w.Key.Parent is not null
                                        && w.Key.Parent.IsExpanded)
                                        && treeValues.Any(a => w.Value.Equals(a.Value)))).
                                        Select(s => s.Value). // Get the NamedScope Index
                                        Distinct());
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
                                node => node.Value,
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

        public IEnumerable<WorkItem> BuildNodes(IEnumerable<XmlBuilder> builders)
        {
            List<WorkItem> work = new List<WorkItem>();
            Int32 totalWork = builders.Count();
            Int32 completedWork = 0;
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
                    foreach (XmlBuilder item in builders.
                        Where(w => !builders.Any(a => a.BuilderPath.Equals(w.BuilderPath.ParentPath))).
                        OrderBy(o => o.RenderOrder).
                        ThenBy(o => o.BuilderPath))
                    {
                        treeControl.Invoke(() =>
                        {
                            TreeNode node = CreateNode(item);
                            treeControl.Nodes.Add(node);
                            BuildChildren(node, item.BuilderPath);
                        });
                    }
                }
            });

            return work;

            void BuildChildren(TreeNode parentNode, XmlBuilderIndex key)
            {
                foreach (XmlBuilder item in builders.
                    Where(w => key.Equals(w.BuilderPath.ParentPath)).
                    OrderBy(o => o.RenderOrder).
                    ThenBy(o => o.BuilderPath))
                {
                    TreeNode childNode = CreateNode(item);
                    parentNode.Nodes.Add(childNode);

                    BuildChildren(childNode, item.BuilderPath);
                }
            }

            TreeNode CreateNode(XmlBuilder item)
            {
                TreeNode node = new TreeNode(item.BuilderPath.Member);
                XmlBuilderIndex key = new XmlBuilderIndex(item);

                if (String.IsNullOrEmpty(item.ObjectProperty))
                {
                    node.ImageKey = item.ObjectScope.GetName();
                    node.SelectedImageKey = item.ObjectScope.GetName();
                }
                else
                {
                    if (item.ObjectType.TryGetImage(out Image? _))
                    {
                        node.ImageKey = item.ObjectType.GetName();
                        node.SelectedImageKey = item.ObjectType.GetName();
                    }
                    else
                    {
                        node.ImageKey = ObjectValueType.Null.GetName();
                        node.SelectedImageKey = ObjectValueType.Null.GetName();
                    }
                }

                if (!treeValues.ContainsValue(key))
                { treeValues.Add(node, key); }

                completedWork = completedWork + 1;
                return node;
            }

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
    }
}
