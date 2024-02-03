using DataDictionary.BusinessLayer.NameSpace;
using DataDictionary.DataLayer.ApplicationData.Help;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using System.ComponentModel;
using System.Data;

namespace DataDictionary.Main.Forms.App
{
    partial class HelpSubject : ApplicationBase
    {
        class ControlItem
        {
            public ListViewItem? ListItem { get; set; }
            public String ControlType { get; private set; }
            public String ControlName { get; private set; }
            public Boolean IsForm { get; private set; }
            public String FullName { get; private set; }

            public ControlItem(Control source)
            {
                FullName = source.ToFullControlName();

                if (source is Form)
                {
                    if (source.GetType().BaseType is Type baseType)
                    { ControlType = baseType.Name; }
                    else { ControlType = source.GetType().Name; }

                    if (String.IsNullOrWhiteSpace(source.Name))
                    { ControlName = GetType().Name; }
                    else { ControlName = source.Name; }

                    IsForm = true;
                }
                else
                {
                    Control root = source;
                    while (root is not Form && root.Parent is not null)
                    { root = root.Parent; }

                    ControlName = source.ToFullControlName().Replace(String.Format("{0}.", root.ToFullControlName()), String.Empty);
                    ControlType = source.GetType().Name;
                    IsForm = false;
                }
            }

            public override string ToString()
            { return FullName; }
        }

        BindingList<ControlItem> controlList = new BindingList<ControlItem>();

        public HelpSubject() : base()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_HelpTableOfContent;
            controlData.Columns[0].Width = (Int32)(controlData.ClientSize.Width * 0.7);
            controlData.Columns[1].Width = (Int32)(controlData.ClientSize.Width * 0.3);

            helpToolStripButton.Enabled = false;
            newItemCommand.Enabled = true;
            newItemCommand.Click += NewItemCommand_Click;
            newItemCommand.Image = Resources.NewStatusHelp;

            deleteItemCommand.Enabled = true;
            deleteItemCommand.Click += DeleteItemCommand_Click;
            deleteItemCommand.Image = Resources.DeleteStatusHelp;

            helpBinding.DataSource = Program.Data.HelpSubjects;

            // Setup Images for Tree Control
            SetImages(helpContentNavigation, helpContentImageItems.Values);

            openFromDatabaseCommand.Enabled = false; // TODO: Not Ready
            openFromDatabaseCommand.Click += OpenFromDatabaseCommand_Click;

            saveToDatabaseCommand.Enabled = false; // TODO: Not Ready
            saveToDatabaseCommand.Click += SaveToDatabaseCommand_Click;

            deleteFromDatabaseCommand.Enabled = false; // TODO: Not Ready
            deleteFromDatabaseCommand.Click += DeleteFromDatabaseCommand_Click;

        }


        public HelpSubject(Form targetForm) : this()
        {
            List<Control> values = targetForm.ToControlList()
                .Where(w => !String.IsNullOrWhiteSpace(w.Name)
                            && !(w is Panel or ToolStrip or SplitContainer or Splitter or TabControl))
                .OrderBy(o => o is not Form)
                .ThenBy(o => o.ToFullControlName())
                .ToList();

            foreach (Control item in values)
            {
                ControlItem newControl = new ControlItem(item);
                ListViewItem newItem = new ListViewItem(newControl.ControlName);
                newItem.SubItems.Add(newControl.ControlType);
                newControl.ListItem = newItem;

                controlList.Add(newControl);
                controlData.Items.Add(newItem);
            }

            if (controlList.FirstOrDefault(w => w.IsForm) is ControlItem baseForm)
            { controlsGroup.Text = String.Format("Controls for: {0}", baseForm.ControlName); }

            if (helpBinding.DataSource is IList<HelpItem> subjects && targetForm.ToFullControlName() is String fullName)
            {
                if (subjects.FirstOrDefault(w => w.NameSpace is String && w.NameSpace == fullName) is HelpItem subject)
                { helpBinding.Position = subjects.IndexOf(subject); }
            }
        }

        public HelpSubject(String targetSubject) : this()
        {
            if (helpBinding.DataSource is IList<HelpItem> subjects)
            {
                if (subjects.FirstOrDefault(w => w.HelpSubject is String && w.HelpSubject.Equals(targetSubject, StringComparison.CurrentCultureIgnoreCase)) is HelpItem subject)
                { helpBinding.Position = subjects.IndexOf(subject); }
                else if (subjects.FirstOrDefault(w => w.NameSpace is String && w.NameSpace == targetSubject) is HelpItem nameSpaceSubject)
                { helpBinding.Position = subjects.IndexOf(nameSpaceSubject); }

            }
        }

        private void HelpSubject_Load(object sender, EventArgs e)
        {
            IHelpItem nameOfValues;
            helpSubjectData.DataBindings.Add(new Binding(nameof(helpSubjectData.Text), helpBinding, nameof(nameOfValues.HelpSubject)));
            helpNameSpaceData.DataBindings.Add(new Binding(nameof(helpNameSpaceData.Text), helpBinding, nameof(nameOfValues.NameSpace)));
            helpToolTipData.DataBindings.Add(new Binding(nameof(helpToolTipData.Text), helpBinding, nameof(nameOfValues.HelpToolTip)));
            helpTextData.DataBindings.Add(new Binding(nameof(helpTextData.Rtf), helpBinding, nameof(nameOfValues.HelpText)));

            BuildHelpTree();
        }


        private void NewItemCommand_Click(object? sender, EventArgs e)
        {
            if (helpBinding.AddNew() is HelpItem newItem)
            {
                //TODO: Always added at end of list. Can it be added based on Name Space?

                TreeNode newNode = CreateNode(newItem, helpContentImageIndex.HelpPage);
                helpContentNavigation.SelectedNode = newNode;
            }
        }

        private void DeleteItemCommand_Click(object? sender, EventArgs e)
        {
            if (helpBinding.Current is HelpItem current)
            {
                //TODO: Test/Debug. Appears to work but the tree after refresh does not match.

                foreach (KeyValuePair<TreeNode, HelpItem> item in helpContentNodes.Where(w => w.Value == current))
                {
                    if (helpContentNavigation.Nodes.Contains(item.Key))
                    {
                        helpContentNavigation.SelectedNode = item.Key.Parent;

                        foreach (TreeNode child in item.Key.Nodes)
                        {
                            helpContentNavigation.Nodes.Remove(child);
                            item.Key.Parent.Nodes.Add(child);
                        }

                        item.Key.Remove();
                    }
                }

                current.Remove();
            }
        }


        #region Help Content Tree
        Dictionary<TreeNode, HelpItem> helpContentNodes = new Dictionary<TreeNode, HelpItem>();
        enum helpContentImageIndex
        {
            HelpPage,
            HelpGroup
        }

        static Dictionary<helpContentImageIndex, (String imageKey, Image image)> helpContentImageItems = new Dictionary<helpContentImageIndex, (String imageKey, Image image)>()
        {
            {helpContentImageIndex.HelpPage,    ("HelpPage",   Resources.StatusHelp) },
            {helpContentImageIndex.HelpGroup,   ("HelpGroup",  Resources.HelpIndexFile) },
        };

        void BuildHelpTree()
        {
            helpContentNavigation.Nodes.Clear();
            helpContentNodes.Clear();

            if (helpBinding.DataSource is IEnumerable<HelpItem> items)
            { TreeGroup(helpContentNavigation.Nodes, items); }

            void TreeGroup(TreeNodeCollection target, IEnumerable<HelpItem> source, String? groupLevel = null)
            {
                List<IGrouping<String, HelpItem>> grouping = source.OrderBy(o => o.NameSpace).
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

                foreach (IGrouping<String, HelpItem> group in grouping)
                {
                    List<HelpItem> items = group.Where(w => w.NameSpace == groupLevel)
                                                .OrderBy(o => o.NameSpace)
                                                .ThenBy(o => o.HelpSubject)
                                                .ToList();
                    List<HelpItem> subItems = group.Except(items).ToList();

                    if (items.Count == 1)
                    {
                        TreeNode newNode = CreateNode(items[0], helpContentImageIndex.HelpPage, parent);
                        parent = newNode.Nodes;
                    }
                    else if (items.Count > 1)
                    {
                        TreeNode newNode;
                        if (String.IsNullOrWhiteSpace(group.Key))
                        { newNode = new TreeNode("General"); }
                        else { newNode = new TreeNode(group.Key); }

                        newNode.ImageKey = helpContentImageItems[helpContentImageIndex.HelpGroup].imageKey;
                        newNode.SelectedImageKey = helpContentImageItems[helpContentImageIndex.HelpGroup].imageKey;

                        parent.Add(newNode);

                        foreach (HelpItem item in items)
                        { CreateNode(item, helpContentImageIndex.HelpPage, newNode.Nodes); }
                    }

                    String level;
                    if (String.IsNullOrWhiteSpace(groupLevel)) { level = group.Key; }
                    else { level = String.Format("{0}.{1}", groupLevel, group.Key); }

                    TreeGroup(parent, subItems, level);
                }

            }
        }

        private TreeNode CreateNode(HelpItem source, helpContentImageIndex imageIndex, TreeNodeCollection? parentNode = null)
        {
            TreeNode result = new TreeNode(source.HelpSubject);
            result.ImageKey = helpContentImageItems[imageIndex].imageKey;
            result.SelectedImageKey = helpContentImageItems[imageIndex].imageKey;

            if (parentNode is null)
            { helpContentNavigation.Nodes.Add(result); }
            else { parentNode.Add(result); }

            helpContentNodes.Add(result, source);

            if (helpBinding.Current is HelpItem current && current == source)
            { helpContentNavigation.SelectedNode = result; }

            if (helpBinding.DataSource is IEnumerable<HelpItem> items)
            {
                //foreach (HelpItem childItem in items.Where(w => w.HelpParentId == source.HelpId))
                //{ CreateNode(childItem, imageIndex, result); }
            }

            source.PropertyChanged += Source_PropertyChanged;

            return result;
        }

        private void Source_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is HelpItem item && helpContentNodes.FirstOrDefault(w => w.Value == item).Key is TreeNode node)
            {
                //TODO: Currently only updates the Subject title.
                // Can it update the tree based on NameSpace?
                // How do I delta the tree vs the NameSpace?

                if (node.Text != item.HelpSubject)
                { node.Text = item.HelpSubject; }
            }
        }

        void SetImages(TreeView tree, IEnumerable<(String imageKey, Image image)> images)
        {
            if (tree.ImageList is null)
            { tree.ImageList = new ImageList(); }

            foreach ((string imageKey, Image image) image in images.Where(w => !tree.ImageList.Images.ContainsKey(w.imageKey)))
            { tree.ImageList.Images.Add(image.imageKey, image.image); }
        }


        private void HelpContentNavigation_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (helpContentNodes.ContainsKey(e.Node))
            {
                if (helpBinding.DataSource is IList<HelpItem> items && helpContentNodes[e.Node] is HelpItem target)
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
            HelpItem newItem = new HelpItem();

            if (controlList.FirstOrDefault(w => w.IsForm) is ControlItem root)
            {
                newItem.HelpSubject = root.ControlName;
                newItem.NameSpace = root.FullName;
            }
            else
            { newItem.HelpSubject = "(new Help Subject)"; }

            e.NewObject = newItem;
        }

        private void helpBinding_CurrentChanged(object sender, EventArgs e)
        {
            if (helpBinding.Current is HelpItem current)
            {
                RowState = current.RowState();
                controlData.Enabled = false;

                // So checked event does not fire while the data is being worked
                controlData.ItemChecked -= ControlData_ItemChecked;

                while (controlData.CheckedItems.Count > 0)
                { controlData.CheckedItems[0].Checked = false; }

                if (current.NameSpace is String currentNameSpace
                    && controlList.FirstOrDefault(w => w.FullName == currentNameSpace) is ControlItem control
                    && control.ListItem is ListViewItem listItem)
                {
                    listItem.Checked = true;
                    controlData.Enabled = true;
                    controlData.ItemChecked += ControlData_ItemChecked;
                }
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

                if (helpBinding.Current is HelpItem current
                    && current.NameSpace is String
                    && controlList.FirstOrDefault(w => w.ListItem == e.Item) is ControlItem selected
                    && controlList.FirstOrDefault(w => w.IsForm) is ControlItem root
                    && current.NameSpace != selected.FullName)
                { current.NameSpace = selected.FullName; }

                currentItem = null;
            }
        }


        private void DeleteFromDatabaseCommand_Click(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SaveToDatabaseCommand_Click(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OpenFromDatabaseCommand_Click(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}
