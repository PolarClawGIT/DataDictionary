﻿using DataDictionary.BusinessLayer.Application;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.ApplicationWide
{
    [Obsolete("Replaced with HelpContent and a detailed version of HelpSubject")]
    partial class HelpSubject : ApplicationData
    {
        class ControlItem
        {
            public ListViewItem? ListItem { get; set; }
            public String ControlType { get; private set; }
            public HelpSubjectIndexPath ControlName { get; private set; }
            public Boolean IsForm { get; private set; }

            public ControlItem(Control source)
            {
                ControlName = source.ToNameSpaceKey();

                if (source is Form)
                {
                    if (source.GetType().BaseType is Type baseType)
                    { ControlType = baseType.Name; }
                    else { ControlType = source.GetType().Name; }

                    IsForm = true;
                }
                else
                {
                    Control root = source;
                    while (root is not Form && root.Parent is not null)
                    { root = root.Parent; }

                    ControlType = source.GetType().Name;
                    IsForm = false;
                }
            }

            public override string ToString()
            { return ControlName.MemberFullPath; }
        }

        BindingList<ControlItem> controlList = new BindingList<ControlItem>();

        public HelpSubject() : base()
        {
            InitializeComponent();

            controlData.Columns[0].Width = (Int32)(controlData.ClientSize.Width * 0.7);
            controlData.Columns[1].Width = (Int32)(controlData.ClientSize.Width * 0.3);

            helpToolStripButton.Enabled = false;

            helpBinding.DataSource = BusinessData.ApplicationData.HelpSubjects;

            if (helpBinding.DataSource is IList<HelpSubjectValue> subjects
                && subjects.FirstOrDefault(w => w.NameSpace == Settings.Default.DefaultSubject) is HelpSubjectValue subject)
            { helpBinding.Position = subjects.IndexOf(subject); }

            Setup(
                helpBinding,
                CommandImageType.Add,
                CommandImageType.Delete,
                CommandImageType.OpenDatabase,
                CommandImageType.SaveDatabase,
                CommandImageType.DeleteDatabase,
                CommandImageType.HistoryDatabase);
            SetImages(helpContentNavigation);
        }

        public HelpSubject(Form targetForm) : this()
        {
            HelpSubjectIndexPath key = targetForm.ToNameSpaceKey();

            List<Control> values = targetForm.ToControlList()
                .Where(w => !String.IsNullOrWhiteSpace(w.Name)
                            && w is not Form
                            && !(w is Panel or ToolStrip or MenuStrip or SplitContainer or Splitter))
                .OrderBy(o => o is not Form)
                .ThenBy(o => o.ToNameSpaceKey())
                .ToList();

            // Take care of current form
            ControlItem baseForm = new ControlItem(targetForm);
            ListViewItem baseItem = new ListViewItem(baseForm.ControlName.Member);
            baseForm.ListItem = baseItem;
            controlList.Add(baseForm);
            controlData.Items.Add(baseItem);
            controlsGroup.Text = String.Format("Controls for: {0}", baseForm.ControlName.Format("{0}"));

            // Add all the forms controls
            foreach (Control item in values)
            {
                ControlItem newControl = new ControlItem(item);
                String itemName = newControl.ControlName.Format("{0}")
                    .Replace(String.Format("{0}.", baseForm.ControlName.Format("{0}")), String.Empty);
                ListViewItem newItem = new ListViewItem(itemName);
                newItem.SubItems.Add(newControl.ControlType);
                newControl.ListItem = newItem;

                controlList.Add(newControl);
                controlData.Items.Add(newItem);
            }

            if (helpBinding.DataSource is IList<HelpSubjectValue> subjects)
            {
                if (subjects.FirstOrDefault(w => key.Equals(new HelpSubjectIndexPath(w))) is HelpSubjectValue subject)
                { helpBinding.Position = subjects.IndexOf(subject); }
            }
        }

        public HelpSubject(String targetSubject) : this()
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

        private void HelpSubject_Load(object sender, EventArgs e)
        {
            IHelpSubjectValue nameOfValues;
            helpSubjectData.DataBindings.Add(new Binding(nameof(helpSubjectData.Text), helpBinding, nameof(nameOfValues.HelpSubject), false, DataSourceUpdateMode.OnPropertyChanged));
            helpNameSpaceData.DataBindings.Add(new Binding(nameof(helpNameSpaceData.Text), helpBinding, nameof(nameOfValues.NameSpace), false, DataSourceUpdateMode.OnPropertyChanged));
            helpToolTipData.DataBindings.Add(new Binding(nameof(helpToolTipData.Text), helpBinding, nameof(nameOfValues.HelpToolTip), false, DataSourceUpdateMode.OnPropertyChanged));
            helpTextData.DataBindings.Add(new Binding(nameof(helpTextData.Rtf), helpBinding, nameof(nameOfValues.HelpText), false, DataSourceUpdateMode.OnPropertyChanged));

            BuildHelpTree();
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

        private void helpSubjectData_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(helpSubjectData.Text))
            {
                errorProvider.SetError(helpSubjectData.ErrorControl, "Help Subject must have a value");
                e.Cancel = true;
            }
        }

        private void helpSubjectData_Validated(object sender, EventArgs e)
        { errorProvider.SetError(helpSubjectData.ErrorControl, String.Empty); }


        private void helpTextData_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(helpTextData.Text))
            {
                errorProvider.SetError(helpTextData.ErrorControl, "Help Text must have a value");
                e.Cancel = true;
            }
        }

        private void helpTextData_Validated(object sender, EventArgs e)
        { errorProvider.SetError(helpTextData.ErrorControl, String.Empty); }

        private void HelpSubject_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If the forms closing, ignore errors.
            // Error Provider will set this to e.Cancel to true, blocking the closing of the form.
            if (errorProvider.GetAllErrors(this).Count() > 0 && e.Cancel) { e.Cancel = false; }
        }

        #region IColleague

        protected override void HandleMessage(DbApplicationBatchStarting message)
        { }

        protected override void HandleMessage(DbApplicationBatchCompleted message)
        { }
        #endregion

        private void helpBinding_AddingNew(object sender, AddingNewEventArgs e)
        {
            HelpSubjectValue newItem = new HelpSubjectValue();

            if (controlList.FirstOrDefault(w => w.IsForm) is ControlItem root)
            {
                newItem.HelpSubject = root.ControlName.Member;
                newItem.NameSpace = root.ControlName.MemberFullPath;
            }
            else
            { newItem.HelpSubject = "(new Help Subject)"; }

            e.NewObject = newItem;
        }

        HelpSubjectValue? lastHelpItem; // Used exclusively by CurrentChanged to get the prior current value.
        private void helpBinding_CurrentChanged(object sender, EventArgs e)
        {
            if (helpBinding.Current is HelpSubjectValue current)
            {
                HelpSubjectIndexPath key = new HelpSubjectIndexPath(current);

                // The item(s) with the Default NameSpace cannot change NameSpaces. This is the About subject.
                helpNameSpaceData.Enabled = !(current.NameSpace is String && current.NameSpace == Settings.Default.DefaultSubject);

                if (lastHelpItem is not null)
                { lastHelpItem.RowStateChanged -= Current_RowStateChanged; }

                current.RowStateChanged += Current_RowStateChanged;
                lastHelpItem = current;
                controlData.Enabled = false;

                // So checked event does not fire while the data is being worked
                controlData.ItemChecked -= ControlData_ItemChecked;

                while (controlData.CheckedItems.Count > 0)
                { controlData.CheckedItems[0].Checked = false; }

                if (controlList.FirstOrDefault(w => key.Equals(w.ControlName)) is ControlItem control
                    && control.ListItem is ListViewItem listItem)
                {
                    listItem.Checked = true;
                    controlData.Enabled = true;
                    controlData.ItemChecked += ControlData_ItemChecked;
                }
            }

            void Current_RowStateChanged(object? sender, EventArgs e)
            {
                if (helpBinding.Current is HelpSubjectValue current)
                { RowState = current.RowState(); }
            }
        }

        private void helpBinding_DataError(object sender, BindingManagerDataErrorEventArgs e)
        { } // Helps in Debugging binding

        private void helpBinding_BindingComplete(object sender, BindingCompleteEventArgs e)
        { }  // Helps in Debugging binding

        private void controlData_Resize(object sender, EventArgs e)
        {
            controlData.Columns[0].Width = (Int32)(controlData.ClientSize.Width * 0.7);
            controlData.Columns[1].Width = (Int32)(controlData.ClientSize.Width * 0.3);
        }

        ListViewItem? currentItem = null; // To prevent recursive calls
        private void ControlData_ItemChecked(object? sender, ItemCheckedEventArgs e)
        {
            if (currentItem is null && e.Item.Checked)
            {
                // ItemChecked event is called each time the Checked state changes for any item, even in code.
                // The currentItem is used to track the item currently being worked on.
                // This prevents the recursive call fired when the Check state is set by the following loop.
                currentItem = e.Item;

                foreach (ControlItem item in
                controlList.Where(w => w.ListItem != e.Item
                    && w.ListItem is not null
                    && w.ListItem.Index >= 0
                    && w.ListItem.Checked))
                {
                    if (item.ListItem is ListViewItem viewItem && viewItem.Index >= 0)
                    { viewItem.Checked = false; }
                }

                if (helpBinding.Current is HelpSubjectValue current)
                {
                    HelpSubjectIndexPath key = new HelpSubjectIndexPath(current);
                    if (controlList.FirstOrDefault(w => w.ListItem == e.Item) is ControlItem selected
                        && !key.Equals(selected.ControlName))
                    { current.NameSpace = selected.ControlName.MemberFullPath; }

                }

                currentItem = null;
            }
        }

        protected override void AddCommand_Click(Object? sender, EventArgs e)
        {
            if (helpBinding.AddNew() is HelpSubjectValue newItem)
            {
                //TODO: Always added at end of list. Can it be added based on Name Space?

                TreeNode newNode = CreateNode(newItem, helpContentImageIndex.HelpPage);
                helpContentNavigation.SelectedNode = newNode;
            }
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            if (helpBinding.Current is HelpSubjectValue current)
            {
                RemoveNode(current);
                helpBinding.RemoveCurrent();
            }
        }

        protected override void DeleteFromDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);

            if (helpBinding.Current is HelpSubjectValue current
                && current.NameSpace is String
                && current.NameSpace != Settings.Default.DefaultSubject) // Cannot Delete the Default Subject
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                RemoveNode(current);
                current.Remove();

                work.Add(factory.OpenConnection());
                work.AddRange(BusinessData.ApplicationData.HelpSubjects.Save(factory, current));

                IsLocked(true);
                IsWaitCursor(true);
                DoWork(work, onCompleting);

                void onCompleting(RunWorkerCompletedEventArgs args)
                {
                    IsWaitCursor(false);

                    if (args.Error is null)
                    { IsLocked(false); }
                }
            }
        }

        protected override void SaveToDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);

            if (helpBinding.Current is HelpSubjectValue current)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.AddRange(BusinessData.ApplicationData.HelpSubjects.Save(factory, current));

                IsLocked(true);
                IsWaitCursor(true);
                DoWork(work, onCompleting);

                void onCompleting(RunWorkerCompletedEventArgs args)
                {
                    IsWaitCursor(false);

                    if (args.Error is null)
                    {
                        current.AcceptChanges();
                        RowState = current.RowState();
                        IsLocked(false);
                    }
                }
            }
        }

        protected override void OpenFromDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);

            if (helpBinding.Current is HelpSubjectValue current)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();
                HelpSubjectIndex key = new HelpSubjectIndex(current);
                current.Remove();

                work.Add(factory.OpenConnection());
                work.AddRange(BusinessData.ApplicationData.HelpSubjects.Load(factory, current));

                // Unbind the RTF control to avoid errors generated by background thread
                helpTextData.DataBindings.Clear();

                IsLocked(true);
                IsWaitCursor(true);
                DoWork(work, onCompleting);

                void onCompleting(RunWorkerCompletedEventArgs args)
                {
                    IsWaitCursor(false);

                    if (args.Error is null)
                    { IsLocked(false); }

                    if (helpBinding.DataSource is IList<HelpSubjectValue> subjects)
                    {
                        if (subjects.FirstOrDefault(w => key.Equals(w)) is HelpSubjectValue subject)
                        {
                            helpTextData.DataBindings.Add(new Binding(nameof(helpTextData.Rtf), helpBinding, nameof(subject.HelpText), false, DataSourceUpdateMode.OnPropertyChanged));
                            helpBinding.Position = subjects.IndexOf(subject);
                        }
                    }
                }
            }
        }
    }
}
