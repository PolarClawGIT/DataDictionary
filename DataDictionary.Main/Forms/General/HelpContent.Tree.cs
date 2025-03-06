using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Forms.General
{
    partial class HelpContent
    {
        class ContentTree
        {
            TreeView treeControl;

            enum ImageKey
            {
                HelpPage,
                HelpGroup,
                HelpForm
            }

            static Dictionary<ImageKey, Image> imageList = new Dictionary<ImageKey, Image>()
            {
                {ImageKey.HelpPage, NavigationEnumeration.Cast(ScopeType.ApplicationHelpPage).GetImage() },
                {ImageKey.HelpGroup, NavigationEnumeration.Cast(ScopeType.ApplicationHelpGroup).GetImage() },
                {ImageKey.HelpForm, NavigationEnumeration.Cast(ScopeType.ApplicationHelpForm).GetImage() },
            };

            Dictionary<TreeNode, BindingSubject> nodes = new Dictionary<TreeNode, BindingSubject>();

            public ContentTree(TreeView target)
            { treeControl = target; }


            public void SetImages()
            {
                ImageList images = new ImageList();

                foreach (var item in imageList)
                { images.Images.Add(item.Key.ToString(), item.Value); }

                treeControl.ImageList = images;
            }

            public void BuildTree(IEnumerable<BindingSubject> source)
            {
                // Existing State
                BindingSubject? selectedNode = null;
                if (treeControl.SelectedNode is TreeNode selected && nodes.TryGetValue(selected, out BindingSubject? value))
                { selectedNode = value; }

                List<BindingSubject> expendedNodes = new List<BindingSubject>();
                expendedNodes.AddRange(nodes.Where(w =>
                (w.Key.IsExpanded
                || (w.Key.Nodes.Count == 0
                    && w.Key.Parent is not null
                    && w.Key.Parent.IsExpanded))).
                    Select(s => s.Value).
                    Distinct());

                nodes.Clear();
                treeControl.Nodes.Clear();

                // TODO: Build Nodes
                var roots = source.
                    Where(w => w.Path.ParentPath is null
                        || !source.Any(a => a.Path.Equals(w.Path.ParentPath))).
                    ToList();

                // Restore State
                foreach (TreeNode item in nodes.Where(w => selectedNode is not null && selectedNode.Equals(w.Value)).Select(s => s.Key))
                { treeControl.SelectedNode = item; }

                foreach (BindingSubject item in expendedNodes)
                {
                    foreach (TreeNode node in nodes.Where(w => item.Equals(w.Value)).Select(s => s.Key))
                    { if (!node.IsExpanded) { node.ExpandParent(); } }
                }
            }
        }
    }
}
