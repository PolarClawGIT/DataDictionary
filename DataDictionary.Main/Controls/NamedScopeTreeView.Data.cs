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

        Dictionary<TreeNode, INamedScopeValue> treeValues = new Dictionary<TreeNode, INamedScopeValue>();
        List<DataLayerIndex> expandedIndexes = new List<DataLayerIndex>();

        public NamedScopeTreeViewData(TreeView tree)
        { treeControl = tree; }

        public INamedScopeSourceValue? GetValue(TreeNode node)
        {
            if (treeValues.TryGetValue(node, out INamedScopeValue? value))
            { return value.Source; }
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
                                    (w.Key.IsExpanded && treeValues.Any(a => w.Value.Index.Equals(a.Value.Index))
                                    || (w.Key.Nodes.Count == 0
                                        && w.Key.Parent is not null
                                        && w.Key.Parent.IsExpanded)
                                        && treeValues.Any(a => w.Value.Index.Equals(a.Value.Index)))).
                                        Select(s => s.Value). // Get the NamedScope Index
                                        Distinct().
                                        Select(s => s.Source.Index)); // Translate to DataLayer Index
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
                                node => node.Value.Source.Index,
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
            List<INamedScopeValue> values = new List<INamedScopeValue>();
            values.AddRange(GetValues(rootIndex));

            IReadOnlyList<INamedScopeValue> GetValues(NamedScopeIndex index)
            {
                List<INamedScopeValue> result = new List<INamedScopeValue>();
                result.Add(treeData.GetValue(index));

                foreach (var item in treeData.ChildrenKeys(index))
                { result.Add(treeData.GetValue(item)); }
                return result;
            }
            //TODO: Build out rest of logic.

        }

        TreeNode CreateNode(INamedScopeValue value)
        {
            ImageEnumeration scopeImage = ImageEnumeration.Cast(value.Scope);
            TreeNode result = new TreeNode(value.Title);

            result.ImageKey = scopeImage.Name;
            result.SelectedImageKey = scopeImage.Name;
            result.ToolTipText = value.Path.MemberFullPath;
            value.OnTitleChanged += (source, eventArg) =>
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
