using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class SchemaDefinition
    {
        TreeBinding nodesTree;

        class TreeBinding
        {
            TreeView treeControl;

            Dictionary<TreeNode, XmlBuilderIndex> treeValues = new Dictionary<TreeNode, XmlBuilderIndex>();
            List<XmlBuilderIndex> expandedIndexes = new List<XmlBuilderIndex>();

            public TreeBinding(TreeView tree)
            {
                treeControl = tree;

                treeControl.ImageList = new ImageList();
                treeControl.ImageList.AddImages(Enum.GetValues<ScopeType>().ToList());
                treeControl.ImageList.AddImages(Enum.GetValues<ObjectValueType>().ToList());
            }

            public Boolean TryGetValue(TreeNode node, [NotNullWhen(true)] out XmlBuilderIndex? key)
            {
                if (treeValues.TryGetValue(node, out XmlBuilderIndex? value))
                { key = value; return true; }
                else { key = null; return false; }
            }

            public Boolean TryGetNode(XmlBuilderIndex key, [NotNullWhen(true)] out TreeNode? node)
            {
                var value = treeValues.Where(w => key.Equals(w.Value)).ToList();

                if (value.Count == 0)
                { node = null; return false; }
                else
                { node = value.First().Key; return true; }
            }

            public Boolean TryGetSelected([NotNullWhen(true)] out TreeNode? node)
            {
                node = treeControl.SelectedNode;
                return node is not null;
            }

            public Boolean TryGetSelected([NotNullWhen(true)] out XmlBuilderIndex? key)
            {
                key = null;
                return treeControl.SelectedNode is not null && treeValues.TryGetValue(treeControl.SelectedNode, out key);
            }

            public void LoadTree(IEnumerable<XmlBuilder> builders)
            {
                BeginUpdate();
                BuildNodes(builders);
                EndUpdate();


                void BeginUpdate()
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
                }

                void BuildNodes(IEnumerable<XmlBuilder> builders)
                {
                    ClearNodes(treeControl.Nodes);

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

                void ClearNodes(TreeNodeCollection nodes)
                {
                    while (nodes.Count > 0)
                    {
                        TreeNode item = nodes[0];
                        ClearNodes(item.Nodes);
                        item.Remove();
                    }
                }

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
                    node.ToolTipText = item.BuilderPath.MemberFullPath;

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

                    return node;
                }

                void EndUpdate()
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
                }
            }
        }

        #region Node TreeView
        Boolean? isTreeNodePlusMinus = null;
        private void SchemaNodeTree_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (isTreeNodePlusMinus == false) { e.Cancel = true; } // AfterCollapse does not fire
            else if (isTreeNodePlusMinus == true) { e.Cancel = false; }
            else { } // Was not triggered by Click event

            isTreeNodePlusMinus = null; // Reset to undetermined avoid calling above logic
        }

        private void SchemaNodeTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (isTreeNodePlusMinus == false) { e.Cancel = true; } // AfterExpanded does not fire
            else if (isTreeNodePlusMinus == true) { e.Cancel = false; }
            else { } // Was not triggered by Click event

            isTreeNodePlusMinus = null; // Reset to undetermined avoid calling above logic
        }

        private void SchemaNodeTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node is not null && e.Node.TreeView is not null)
            { isTreeNodePlusMinus = e.Node.TreeView.HitTest(e.Location).Location == TreeViewHitTestLocations.PlusMinus; }

            if (e.Clicks > 1) { throw new NotImplementedException(); } // This never occurs even on a double click.
        }

        private void SchemaNodeTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Need to get the Hit Location itself because the flag may have been reset.
            if (e.Node is not null
                && e.Node.TreeView is not null
                && e.Node.TreeView.HitTest(e.Location).Location != TreeViewHitTestLocations.PlusMinus
                && nodesTree.TryGetValue(e.Node, out XmlBuilderIndex? value))
            { formBinding.TrySetNode(value); }
        }
        #endregion
    }
}
