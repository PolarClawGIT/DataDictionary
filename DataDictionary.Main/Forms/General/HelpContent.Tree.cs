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
                {ImageKey.HelpPage, ScopeType.ApplicationHelpPage.GetImage(CommandType.Default)},
                {ImageKey.HelpGroup, ScopeType.ApplicationHelpGroup.GetImage(CommandType.Default) },
                {ImageKey.HelpForm, ScopeType.ApplicationHelpForm.GetImage(CommandType.Default) },
            };

            Dictionary<TreeNode, BindingSubject> subjectNodes = new Dictionary<TreeNode, BindingSubject>();
            Dictionary<TreeNode, PathIndex> pathNodes = new Dictionary<TreeNode, PathIndex>();

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
                if (treeControl.SelectedNode is TreeNode selected && subjectNodes.TryGetValue(selected, out BindingSubject? value))
                { selectedNode = value; }

                List<BindingSubject> expandedSubjects = new List<BindingSubject>();
                List<PathIndex> expandedPaths = new List<PathIndex>();
                foreach (TreeNode item in treeControl.Nodes.GetNodes(w => w.IsExpanded))
                {
                    if (subjectNodes.TryGetValue(item, out BindingSubject? subject)
                        && !expandedSubjects.Contains(subject))
                    { expandedSubjects.Add(subject); }

                    if (pathNodes.TryGetValue(item, out PathIndex? path)
                        && !expandedPaths.Contains(path))
                    { expandedPaths.Add(path); }
                }

                // Clear the Tree
                treeControl.BeginUpdate();
                subjectNodes.Clear();
                pathNodes.Clear();
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
                foreach (TreeNode item in subjectNodes.
                        Where(w =>
                            selectedNode is not null
                            && selectedNode.Equals(w.Value)).
                        Select(s => s.Key))
                { treeControl.SelectedNode = item; }

                foreach (BindingSubject item in expandedSubjects)
                {   // Normally only one but multiples are possible
                    foreach (TreeNode node in subjectNodes.Where(w => item.Equals(w.Value)).Select(s => s.Key))
                    { if (!node.IsExpanded) { node.ExpandParent(); } }
                }

                foreach (PathIndex item in expandedPaths)
                {   // Normally only one but multiples are possible
                    foreach (TreeNode node in pathNodes.Where(w => item.Equals(w.Value)).Select(s => s.Key))
                    { if (!node.IsExpanded) { node.ExpandParent(); } }
                }

                // Done
                treeControl.EndUpdate();

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
                        pathNodes.Add(groupNode, path);
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

                    subjectNodes.Add(newNode, subject);
                    currentNodes.Add(newNode);
                    currentNodes = newNode.Nodes;
                    return currentNodes;
                }
            }


            public Boolean SetNode(BindingSubject helpSubject)
            {
                var node = subjectNodes.FirstOrDefault(w => helpSubject.Path.Equals(w.Value.Path));
                if (node.Key is not null)
                { treeControl.SelectedNode = node.Key; return true; }
                else { return false; }
            }

            public Boolean GetSubject(TreeNode treeNode, [NotNullWhen(true)] out BindingSubject? value)
            {
                if (subjectNodes.TryGetValue(treeNode, out BindingSubject? result))
                { value = result; return true; }
                else { value = null; return false; }
            }

            public Boolean GetSubject([NotNullWhen(true)] out BindingSubject? value)
            {
                if (treeControl.SelectedNode is not null
                    && subjectNodes.TryGetValue(treeControl.SelectedNode, out BindingSubject? result))
                { value = result; return true; }
                else { value = null; return false; }
            }
        }
    }
}
