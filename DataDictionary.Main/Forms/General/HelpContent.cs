using DataDictionary.BusinessLayer.Application;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataDictionary.Main.Forms.General
{
    partial class HelpContent : ApplicationData
    {
        Form? helpForForm; // Form that requested the Help on

        public HelpContent() : base()
        {
            InitializeComponent();
            helpToolStripButton.Enabled = false;
            helpBinding.DataSource = BusinessData.ApplicationData.HelpSubjects;

            Setup(
                helpBinding,
                CommandImageType.Add,
                CommandImageType.Open,
                CommandImageType.HistoryDatabase);

            SetImages(helpContentNavigation);
        }

        public HelpContent(String targetSubject) : this()
        {
            if (helpBinding.DataSource is IList<HelpSubjectValue> subjects)
            {
                if (subjects.FirstOrDefault(w => w.HelpSubject is String
                    && w.HelpSubject.Equals(targetSubject, StringComparison.CurrentCultureIgnoreCase))
                    is HelpSubjectValue subject)
                { helpBinding.Position = subjects.IndexOf(subject); }
                else if (subjects.FirstOrDefault(w => w.NameSpace is not null
                    && w.NameSpace == targetSubject) is HelpSubjectValue nameSpaceSubject)
                { helpBinding.Position = subjects.IndexOf(nameSpaceSubject); }
            }
        }

        public HelpContent(Form targetForm) : this()
        {
            HelpSubjectIndexPath key = targetForm.ToNameSpaceKey();

            List<Control> values = targetForm.ToControlList()
                .Where(w => !String.IsNullOrWhiteSpace(w.Name)
                            && w is not Form
                            && !(w is Panel or ToolStrip or MenuStrip or SplitContainer or Splitter))
                .OrderBy(o => o is not Form)
                .ThenBy(o => o.ToNameSpaceKey())
                .ToList();

            if (helpBinding.DataSource is IList<HelpSubjectValue> subjects)
            {
                if (subjects.FirstOrDefault(w => key.Equals(new HelpSubjectIndexPath(w))) is HelpSubjectValue subject)
                { helpBinding.Position = subjects.IndexOf(subject); }
                helpForForm = targetForm;
            }
        }

        private void HelpContent_Load(object sender, EventArgs e)
        {
            helpSubjectData.DataBindings.Add(new Binding(nameof(helpSubjectData.Text), helpBinding, nameof(HelpSubjectValue.HelpSubject), false, DataSourceUpdateMode.OnPropertyChanged));
            helpTextData.DataBindings.Add(new Binding(nameof(helpTextData.Rtf), helpBinding, nameof(HelpSubjectValue.HelpText), false, DataSourceUpdateMode.OnPropertyChanged));
            BuildHelpTree();
        }

        protected override void AddCommand_Click(Object? sender, EventArgs e)
        {
            base.AddCommand_Click(sender, e);


        }

        protected override void OpenCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenCommand_Click(sender, e);

            if (helpBinding.Current is HelpSubjectValue current)
            {
                if (helpForForm is Form targetForm && new HelpSubjectIndexPath(current).Equals(targetForm.ToNameSpaceKey()))
                { Activate(() => new HelpSubject(current, targetForm)); }
                else { Activate(() => new HelpSubject(current)); }
            }
        }

        protected override void HistoryCommand_Click(Object sender, EventArgs e)
        {
            base.HistoryCommand_Click(sender, e);

            if (helpBinding.DataSource is ILoadHistoryData history)
            { Activate(() => new HistoryView<HelpSubjectValue>(history)); }
        }

        #region Help Content Tree
        Dictionary<TreeNode, HelpSubjectValue> helpContentNodes = new Dictionary<TreeNode, HelpSubjectValue>();
        enum helpContentImageIndex
        {
            HelpPage,
            HelpGroup
        }

        static Dictionary<helpContentImageIndex, ImageEnumeration> helpContentImageItems = new Dictionary<helpContentImageIndex, ImageEnumeration>()
        {
            {helpContentImageIndex.HelpPage, ImageEnumeration.Cast(ScopeType.ApplicationHelpPage) },
            {helpContentImageIndex.HelpGroup, ImageEnumeration.Cast(ScopeType.ApplicationHelpGroup) },
        };

        void BuildHelpTree()
        {
            helpContentNavigation.Nodes.Clear();
            helpContentNodes.Clear();

            if (helpBinding.DataSource is IEnumerable<HelpSubjectValue> items)
            { TreeGroup(helpContentNavigation.Nodes, items); }

            void TreeGroup(TreeNodeCollection target, IEnumerable<HelpSubjectValue> source, String? groupLevel = null)
            {
                List<IGrouping<String, HelpSubjectValue>> grouping = source.
                    OrderBy(o => o.NameSpace != Settings.Default.DefaultSubject). // Make About first in the list
                    ThenBy(o => new HelpSubjectIndexPath(o)).
                    GroupBy(g =>
                    {
                        if (String.IsNullOrWhiteSpace(g.NameSpace)) { return String.Empty; }
                        else
                        {
                            String remaining;
                            if (g.NameSpace is null) { remaining = string.Empty; }
                            else if (String.IsNullOrWhiteSpace(groupLevel)) { remaining = g.NameSpace; }
                            else { remaining = g.NameSpace.Replace(String.Format("{0}.", groupLevel), String.Empty); }

                            if (remaining.IndexOf('.') > 0)
                            { return remaining.Substring(0, remaining.IndexOf('.')); }
                            else { return remaining; }
                        }
                    }).ToList();

                TreeNodeCollection parent = target;

                foreach (IGrouping<String, HelpSubjectValue> group in grouping)
                {
                    List<HelpSubjectValue> items = group.Where(w => w.NameSpace == groupLevel)
                                                .OrderBy(o => o.NameSpace)
                                                .ThenBy(o => o.HelpSubject)
                                                .ToList();
                    List<HelpSubjectValue> subItems = group.Except(items).ToList();

                    if (items.Count == 1)
                    {
                        TreeNode newNode = CreateNode(items[0], helpContentImageIndex.HelpPage, parent);
                        parent = newNode.Nodes;
                    }
                    else if (items.Count > 1)
                    {
                        TreeNode newNode = CreateNode(group.Key, helpContentImageIndex.HelpGroup, parent);

                        foreach (HelpSubjectValue item in items)
                        { CreateNode(item, helpContentImageIndex.HelpPage, newNode.Nodes); }
                    }

                    String level;
                    if (String.IsNullOrWhiteSpace(groupLevel)) { level = group.Key; }
                    else { level = String.Format("{0}.{1}", groupLevel, group.Key); }

                    TreeGroup(parent, subItems, level);
                }

            }
        }

        private TreeNode CreateNode(HelpSubjectValue source, helpContentImageIndex imageIndex, TreeNodeCollection? parentNode = null)
        {
            TreeNode result = new TreeNode(source.HelpSubject);
            result.ImageKey = helpContentImageItems[imageIndex].Name;
            result.SelectedImageKey = helpContentImageItems[imageIndex].Name;

            if (parentNode is null)
            { helpContentNavigation.Nodes.Add(result); }
            else { parentNode.Add(result); }

            helpContentNodes.Add(result, source);

            if (helpBinding.Current is HelpSubjectValue current && current == source)
            { helpContentNavigation.SelectedNode = result; }

            source.PropertyChanged += Source_PropertyChanged;

            return result;
        }

        private TreeNode CreateNode(String nodeText, helpContentImageIndex imageIndex, TreeNodeCollection? parentNode = null)
        {
            TreeNode result = new TreeNode(nodeText);
            result.ImageKey = helpContentImageItems[imageIndex].Name;
            result.SelectedImageKey = helpContentImageItems[imageIndex].Name;

            if (parentNode is null)
            { helpContentNavigation.Nodes.Add(result); }
            else { parentNode.Add(result); }

            return result;
        }

        private void RemoveNode(HelpSubjectValue source)
        {
            HelpSubjectIndex key = new HelpSubjectIndex(source);

            KeyValuePair<TreeNode, HelpSubjectValue> currentValue = helpContentNodes.FirstOrDefault(w => key.Equals(w.Value));

            if (currentValue.Key is TreeNode && currentValue.Key.Nodes.Count == 0)
            {
                helpContentNodes.Remove(currentValue.Key);
                helpContentNavigation.Nodes.Remove(currentValue.Key);
            }

            if (currentValue.Key is TreeNode && currentValue.Key.Nodes.Count > 0)
            {
                TreeNodeCollection? parent = null;
                if (currentValue.Key.Parent is not null)
                { parent = currentValue.Key.Parent.Nodes; }

                String nodeText = "(unknown)";
                if (source.NameSpace is String)
                { nodeText = source.NameSpace.Split('.').Last(); }

                TreeNode newNode = CreateNode(nodeText, helpContentImageIndex.HelpGroup, parent);
                helpContentNavigation.Nodes.Remove(currentValue.Key);
                helpContentNodes.Remove(currentValue.Key);

                foreach (TreeNode item in currentValue.Key.Nodes)
                { newNode.Nodes.Add(item); }
            }

            if (helpBinding.DataSource is IList<HelpSubjectValue> subjects && subjects.Where(w => w.NameSpace == Settings.Default.DefaultSubject) is HelpSubjectValue subject)
            { helpBinding.Position = subjects.IndexOf(subject); }
        }

        private void Source_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is HelpSubjectValue item && helpContentNodes.FirstOrDefault(w => w.Value == item).Key is TreeNode node)
            {
                //TODO: Currently only updates the Subject title.
                // Can it update the tree based on NameSpace?
                // How do I delta the tree vs the NameSpace?

                if (node.Text != item.HelpSubject)
                { node.Text = item.HelpSubject; }
            }
        }

        void SetImages(TreeView tree)
        {
            if (tree.ImageList is null)
            { tree.ImageList = new ImageList(); }

            foreach (var image in helpContentImageItems.Values)
            { tree.ImageList.Images.Add(image.Name, image.GetImage()); }
        }

        private void HelpContentNavigation_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (helpContentNodes.ContainsKey(e.Node))
            {
                if (helpBinding.DataSource is IList<HelpSubjectValue> items && helpContentNodes[e.Node] is HelpSubjectValue target)
                {
                    if (items.Contains(target))
                    { helpBinding.Position = items.IndexOf(target); }
                }
            }
        }

        #endregion

    }
}
