using DataDictionary.BusinessLayer.AppGeneral;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Forms.ApplicationWide;
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
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.General
{
    partial class HelpContent : ApplicationData
    {
        FormBinding formData;

        //TODO: Continue removing references to helpBinding DataSource.

        public HelpContent() : base()
        {
            InitializeComponent();
            helpToolStripButton.Enabled = false;
            formData = new FormBinding(ref helpBinding);
            formData.SetSubject(Settings.Default.DefaultSubject);

            SetIcon(ScopeType.ApplicationHelp);
            SetCommand(
                ScopeType.ApplicationHelp,
                CommandImageType.Add,
                CommandImageType.Open,
                CommandImageType.Import,
                CommandImageType.HistoryDatabase);

            SetImages(helpContentNavigation);

            CommandButtons[CommandImageType.Add].Text = "Add new Help Subject (blank)";
            CommandButtons[CommandImageType.Open].Text = "Open/Edit the Selected Help Subject Details";
            CommandButtons[CommandImageType.Import].IsEnabled = false;
            CommandButtons[CommandImageType.Import].Text = "Add new Help Subject using Form Data";

            helpSubjectData.Focus();
        }

        public HelpContent(String targetSubject) : this()
        { formData.SetSubject(targetSubject); }

        public HelpContent(Form targetForm) : this()
        {
            formData.SetSubject(targetForm);
            formData.SetForm(targetForm);
        }

        private void HelpContent_Load(object sender, EventArgs e)
        {
            CommandButtons[CommandImageType.Import].IsEnabled = formData.CurrentForm is not null;

            formData.Bind(BusinessData.ApplicationData.HelpSubjects);

            if (formData.SetPosition() &&
                formData.TryGetCurrent(out HelpSubjectValue? value))
            {
                SetNode(value);
                //helpContentNavigation.HideSelection = false;
                helpBinding.ResumeBinding();
            }
            else
            {
                //TODO: Control keeps focus so HideSelection does not work as desired.
                // Node shows as selected even when SelectedNode is null.
                //helpContentNavigation.SelectedNode = null;
                //helpContentNavigation.HideSelection = true;

                helpBinding.SuspendBinding();
                helpDetailLayout.Enabled = false;

                if (formData.CurrentForm is Form currentForm)
                { helpSubjectData.Text = String.Format("(new Subject: {0})", currentForm.ToNameSpaceKey().Member); }
            }

            helpSubjectData.DataBindings.Add(new Binding(nameof(helpSubjectData.Text), helpBinding, nameof(HelpSubjectValue.HelpSubject), false, DataSourceUpdateMode.OnPropertyChanged));

            BindRtfHelpText();
        }

        private void BindRtfHelpText()
        {
            try // If RTF, bind to the RTF property
            { helpTextData.DataBindings.Add(new Binding(nameof(helpTextData.Rtf), helpBinding, nameof(HelpSubjectValue.HelpText), false, DataSourceUpdateMode.OnValidation)); }
            catch (Exception) // Else it is not RTF, bind to the property 
            {
                if (helpBinding.Current is HelpSubjectValue subject)
                {
                    helpTextData.Text = subject.HelpText ?? String.Empty;
                    subject.HelpText = helpTextData.Rtf;
                    subject.AcceptChanges();
                }

                helpTextData.DataBindings.Add(new Binding(nameof(helpTextData.Rtf), helpBinding, nameof(HelpSubjectValue.HelpText), false, DataSourceUpdateMode.OnValidation));
            }
        }

        protected override void AddCommand_Click(Object? sender, EventArgs e)
        {
            base.AddCommand_Click(sender, e);

            if (helpBinding.AddNew() is HelpSubjectValue newValue)
            { Activate((data) => new HelpSubject(newValue), newValue); }
        }

        protected override void ImportCommand_Click(Object? sender, EventArgs e)
        {
            base.ImportCommand_Click(sender, e);

            if (helpBinding.AddNew() is HelpSubjectValue newValue &&
                formData.CurrentForm is Form targetForm)
            {
                HelpSubjectIndexPath newNameSpace = targetForm.ToNameSpaceKey();
                newValue.HelpSubject = String.Format("(new Subject: {0})", newNameSpace.Member);
                newValue.NameSpace = newNameSpace.MemberFullPath;

                Activate((data) => new HelpSubject(newValue, targetForm), newValue);
            }
        }


        private void HelpBinding_ListChanged(object sender, ListChangedEventArgs e)
        { // ISSUE: this fires multiple times. Be careful and remember prior state.
            BuildHelpTree();
        }

        protected override void OpenCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenCommand_Click(sender, e);

            if (formData.TryGetCurrent(out HelpSubjectValue? current))
            {
                if (formData.CurrentForm is Form targetForm
                    && targetForm.ToNameSpaceKey().ParentOf(new HelpSubjectIndexPath(current)))
                { Activate((data) => new HelpSubject(current, targetForm), current); }
                else { Activate((data) => new HelpSubject(current), current); }
            }
        }


        protected override void HistoryCommand_Click(Object sender, EventArgs e)
        {
            base.HistoryCommand_Click(sender, e);

            if (helpBinding.DataSource is ILoadHistoryData history)
            {
                Form form = Activate(() =>
                new HistoryView<HelpSubjectValue, HelpSubject>(ScopeType.ApplicationHelp, history)
                { SelectedForm = (subject) => new HelpSubject(subject) });

                if (history is IBindingTable table)
                { form.Text = String.Format("History: {0}", table.BindingName); }
            }
        }

        #region Help Content Tree
        Dictionary<TreeNode, HelpSubjectValue> helpContentNodes = new Dictionary<TreeNode, HelpSubjectValue>();
        enum helpContentImageIndex
        {
            HelpPage,
            HelpGroup
        }

        static Dictionary<helpContentImageIndex, NavigationEnumeration> helpContentImageItems = new Dictionary<helpContentImageIndex, NavigationEnumeration>()
        {
            {helpContentImageIndex.HelpPage, NavigationEnumeration.Cast(ScopeType.ApplicationHelpPage) },
            {helpContentImageIndex.HelpGroup, NavigationEnumeration.Cast(ScopeType.ApplicationHelpGroup) },
        };

        void BuildHelpTree()
        {
            List<HelpSubjectIndex> expanded = new List<HelpSubjectIndex>();

            expanded.AddRange(helpContentNodes.Where(w =>
            (w.Key.IsExpanded
            || (w.Key.Nodes.Count == 0
                && w.Key.Parent is not null
                && w.Key.Parent.IsExpanded))).
                Select(s => new HelpSubjectIndex(s.Value)). // Get the HelpSubjectIndex
                Distinct());

            HelpSubjectValue? selectSubject = null;
            if (helpContentNavigation.SelectedNode is TreeNode selectedNode
                && helpContentNodes.ContainsKey(selectedNode))
            { selectSubject = helpContentNodes[selectedNode]; }

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

                        if (expanded.Any(w => w.Equals(items[0])))
                        { newNode.ExpandParent(); }
                    }
                    else if (items.Count > 1)
                    {
                        TreeNode newNode = CreateNode(group.Key, helpContentImageIndex.HelpGroup, parent);

                        foreach (HelpSubjectValue item in items)
                        {
                            TreeNode newChild = CreateNode(item, helpContentImageIndex.HelpPage, newNode.Nodes);
                            if (expanded.Any(w => w.Equals(item)))
                            { newChild.ExpandParent(); }
                        }
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

        [Obsolete("does not work")]
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

        private void SetNode(HelpSubjectValue target)
        {
            if (helpContentNodes.Where(w => w.Value.Equals(target)).Select(s => s.Key).FirstOrDefault() is TreeNode node)
            { helpContentNavigation.SelectedNode = node; }
            else
            {
                // ISSUE: TreeView has no way of un-selecting nodes.
                helpContentNavigation.SelectedNode = null;
            }
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
            if (helpContentNodes.ContainsKey(e.Node)
                && helpContentNodes[e.Node] is HelpSubjectValue target)
            {
                if (formData.SetPosition(target))
                {
                    helpBinding.ResumeBinding();
                    helpDetailLayout.Enabled = true;
                }
                else
                {
                    helpBinding.SuspendBinding();
                    helpDetailLayout.Enabled = false;
                }

                if (formData.CurrentForm is Form targetForm
                    && targetForm.ToNameSpaceKey().ParentOf(new HelpSubjectIndexPath(target)))
                { CommandButtons[CommandImageType.Import].IsEnabled = true; }
                else { CommandButtons[CommandImageType.Import].IsEnabled = false; }
            }
        }

        private void HelpContentNavigation_MouseDoubleClick(object sender, MouseEventArgs e)
        { OpenCommand_Click(sender, EventArgs.Empty); }

        #endregion
    }
}
