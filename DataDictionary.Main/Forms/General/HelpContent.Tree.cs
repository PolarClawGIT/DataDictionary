using DataDictionary.BusinessLayer.AppGeneral;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
                {ImageKey.HelpPage, NavigationEnumeration.GetImage(ScopeType.ApplicationHelpPage) },
                {ImageKey.HelpGroup, NavigationEnumeration.GetImage(ScopeType.ApplicationHelpGroup) },
                {ImageKey.HelpForm, NavigationEnumeration.GetImage(ScopeType.ApplicationHelpForm) },
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

                // Build Nodes
                var paths = source.
                    SelectMany(s => s.Path.Group()).
                    Distinct().
                    OrderBy(o => o).
                    ToList();

                TreeNodeCollection rootNodes = treeControl.Nodes;

                foreach (PathIndex root in paths.
                        Where(w =>
                            w.ParentPath is null
                            || (w.ParentPath is not null
                                && !paths.Any(a => w.ParentPath.Equals(a)))))
                { BuildNodes(rootNodes, root); }

                // Restore State
                foreach (TreeNode item in nodes.
                        Where(w =>
                            selectedNode is not null 
                            && selectedNode.Equals(w.Value)).
                        Select(s => s.Key))
                { treeControl.SelectedNode = item; }

                foreach (BindingSubject item in expendedNodes)
                {
                    foreach (TreeNode node in nodes.Where(w => item.Equals(w.Value)).Select(s => s.Key))
                    { if (!node.IsExpanded) { node.ExpandParent(); } }
                }

                void BuildNodes(TreeNodeCollection treeNodes, PathIndex path)
                {
                    var subjects = source.Where(w => path.Equals(w.Path)).ToList();
                    var childPaths = paths.Where(w => path.Equals(w.ParentPath)).ToList();

                    TreeNodeCollection currentNodes = treeNodes;

                    if (subjects.Count == 1)
                    { currentNodes = BuildNode(currentNodes, subjects.First()); }
                    else
                    {
                        TreeNode groupNode = new TreeNode(path.Member);
                        groupNode.ImageKey = nameof(ImageKey.HelpGroup);
                        groupNode.SelectedImageKey = nameof(ImageKey.HelpGroup);
                        currentNodes.Add(groupNode);
                        currentNodes = groupNode.Nodes;

                        foreach (var subject in subjects)
                        { currentNodes = BuildNode(currentNodes, subject); }
                    }

                    foreach (var childPath in childPaths)
                    { BuildNodes(currentNodes, childPath); }
                }

                TreeNodeCollection BuildNode(TreeNodeCollection currentNodes, BindingSubject subject)
                {
                    TreeNode newNode = new TreeNode(subject.Title);
                    if (subject.SubjectForm is null)
                    {
                        newNode.ImageKey = nameof(ImageKey.HelpPage);
                        newNode.SelectedImageKey = nameof(ImageKey.HelpPage);
                    }
                    else
                    {
                        newNode.ImageKey = nameof(ImageKey.HelpForm);
                        newNode.SelectedImageKey = nameof(ImageKey.HelpForm);
                    }

                    nodes.Add(newNode, subject);
                    currentNodes.Add(newNode);
                    currentNodes = newNode.Nodes;
                    return currentNodes;
                }
            }


            public Boolean SetNode(BindingSubject helpSubject)
            {
                var node = nodes.FirstOrDefault(w => helpSubject.Path.Equals(w.Value.Path));
                if (node.Key is not null)
                { treeControl.SelectedNode = node.Key; return true; }
                else { return false; }
            }

            public Boolean GetSubject(TreeNode treeNode, [NotNullWhen(true)] out BindingSubject? value)
            {
                if (nodes.TryGetValue(treeNode, out BindingSubject? result))
                { value = result; return true; }
                else { value = null; return false; }
            }

            public Boolean GetSubject([NotNullWhen(true)] out BindingSubject? value)
            {
                var x = treeControl.Nodes.OfType<TreeNode>().Where(w => w.IsSelected);

                if (nodes.TryGetValue(treeControl.SelectedNode, out BindingSubject? result))
                { value = result; return true; }
                else { value = null; return false; }
            }
        }
    }
}
