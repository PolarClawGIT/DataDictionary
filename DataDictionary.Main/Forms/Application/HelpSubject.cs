using DataDictionary.BusinessLayer.NameSpace;
using DataDictionary.BusinessLayer.WorkFlows;
using DataDictionary.DataLayer.ApplicationData.Help;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Forms;
using DataDictionary.Main.Forms.Application;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Dialogs
{
    partial class HelpSubject : ApplicationBase
    {
        class ControlItem
        {
            public required Form SourceForm { get; init; }
            public required Control SourceControl { get; init; }
            public ListViewItem? ListItem { get; set; }

            public String ControlType
            {
                get
                {
                    if (SourceControl is Form) { return "Form"; }
                    else { return SourceControl.GetType().Name; }
                }
            }
            public String ControlName
            {
                get
                {
                    String fullName = SourceControl.ToFullControlName();
                    String formName = SourceForm.ToFullControlName();
                    if (SourceControl is Form) { return formName; }
                    else { return fullName.Substring(formName.Length + 1); }
                }
            }
            public Boolean IsForm { get { return SourceControl is Form; } }
            public String FullName { get { return SourceControl.ToFullControlName(); } }

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
        }

        public HelpSubject(Form targetForm) : this()
        {
            List<Control> values = targetForm.ToControlList()
                                             .OrderBy(o => o is not Form)
                                             .ThenBy(o => o.ToFullControlName())
                                             .ToList();

            controlList.Add(new ControlItem() { SourceForm = targetForm, SourceControl = targetForm });

            foreach (Control item in values)
            { controlList.Add(new ControlItem() { SourceForm = targetForm, SourceControl = item }); }

            nameSpaceBinding.DataSource = controlList;

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
            }
        }

        private void HelpSubject_Load(object sender, EventArgs e)
        {
            IHelpItem nameOfValues;
            helpSubjectData.DataBindings.Add(new Binding(nameof(helpSubjectData.Text), helpBinding, nameof(nameOfValues.HelpSubject)));
            helpNameSpaceData.DataBindings.Add(new Binding(nameof(helpNameSpaceData.Text), helpBinding, nameof(nameOfValues.NameSpace)));
            helpToolTipData.DataBindings.Add(new Binding(nameof(helpToolTipData.Text), helpBinding, nameof(nameOfValues.HelpToolTip)));
            helpTextData.DataBindings.Add(new Binding(nameof(helpTextData.Rtf), helpBinding, nameof(nameOfValues.HelpText)));

            // Setup the ListView control controlData
            if (controlList.FirstOrDefault(w => w.IsForm) is ControlItem baseForm)
            { controlsGroup.Text = String.Format("Controls for: {0}", baseForm.ControlName); }

            foreach (ControlItem item in controlList.OrderBy(o => o.IsForm == false).ThenBy(o => o.ControlName))
            {
                ListViewItem newItem = new ListViewItem(item.ControlName);
                newItem.SubItems.Add(item.ControlType);

                item.ListItem = newItem;

                controlData.Items.Add(newItem);
            }

            BuildHelpTree();
        }


        private void NewItemCommand_Click(object? sender, EventArgs e)
        {
            if (helpBinding.AddNew() is HelpItem newItem)
            {
                TreeNode newNode = CreateNode(newItem, helpContentImageIndex.HelpPage);
                helpContentNavigation.SelectedNode = newNode;
            }
        }

        private void DeleteItemCommand_Click(object? sender, EventArgs e)
        {

        }


        #region Help Content Tree
        Dictionary<TreeNode, Object> helpContentNodes = new Dictionary<TreeNode, Object>();
        enum helpContentImageIndex
        {
            HelpPage,
        }

        static Dictionary<helpContentImageIndex, (String imageKey, Image image)> helpContentImageItems = new Dictionary<helpContentImageIndex, (String imageKey, Image image)>()
        {
            {helpContentImageIndex.HelpPage,    ("HelpPage",   Resources.HelpIndexFile) },
        };

        void BuildHelpTree()
        {
            helpContentNavigation.Nodes.Clear();
            helpContentNodes.Clear();


            if (helpBinding.DataSource is IEnumerable<HelpItem> items)
            {




                foreach (HelpItem item in items.Where(w => w.HelpParentId is null))
                { TreeNode subjectNode = CreateNode(item, helpContentImageIndex.HelpPage); }
            }

        }

        private TreeNode CreateNode(HelpItem source, helpContentImageIndex imageIndex, TreeNode? parentNode = null)
        {
            TreeNode result = new TreeNode(source.HelpSubject);
            result.ImageKey = helpContentImageItems[imageIndex].imageKey;
            result.SelectedImageKey = helpContentImageItems[imageIndex].imageKey;

            if (parentNode is null)
            { helpContentNavigation.Nodes.Add(result); }
            else { parentNode.Nodes.Add(result); }

            helpContentNodes.Add(result, source);

            if (helpBinding.Current is HelpItem current && current == source)
            { helpContentNavigation.SelectedNode = result; }

            if (helpBinding.DataSource is IEnumerable<HelpItem> items)
            {
                foreach (HelpItem childItem in items.Where(w => w.HelpParentId == source.HelpId))
                { CreateNode(childItem, imageIndex, result); }
            }

            source.PropertyChanged += Source_PropertyChanged;

            return result;
        }

        private void Source_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is HelpItem item && helpContentNodes.FirstOrDefault(w => w.Value == item).Key is TreeNode node)
            {
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
                newItem.HelpSubject = root.SourceForm.Name;
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
                helpNameSpaceLayout.Enabled = false;

                if (controlList.FirstOrDefault(w => w.IsForm) is ControlItem root && current.NameSpace is String)
                {
                    ModelNameSpaceKeyMember currentNameSpace = new ModelNameSpaceKeyMember(current.NameSpace);
                    ModelNameSpaceKeyMember formNameSpace = new ModelNameSpaceKeyMember(root.FullName);
                    ModelNameSpaceKeyMember parentNameSpace = new ModelNameSpaceKeyMember(currentNameSpace.MemberPath);

                    if (formNameSpace.Equals(currentNameSpace))
                    {
                        helpNameSpaceLayout.Enabled = true;

                        if (controlList.FirstOrDefault(w => currentNameSpace.Equals(new ModelNameSpaceKeyMember(w.FullName))) is ControlItem formItem)
                        {
                            if (formItem.ListItem is not null && formItem.ListItem.Index >= 0)
                            { formItem.ListItem.Checked = true; }
                        }
                    }
                    else if (formNameSpace.Equals(parentNameSpace))
                    {
                        helpNameSpaceLayout.Enabled = true;

                        if (controlList.FirstOrDefault(w => currentNameSpace.Equals(new ModelNameSpaceKeyMember(String.Format("{0}.{1}", root.FullName, w.ControlName)))) is ControlItem item)
                        {
                            if (item.ListItem is not null && item.ListItem.Index >= 0)
                            { item.ListItem.Checked = true; }
                        }
                    }
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
        private void controlData_ItemChecked(object sender, ItemCheckedEventArgs e)
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
                    && controlList.FirstOrDefault(w => w.IsForm) is ControlItem root)
                {
                    current.NameSpace = selected.FullName;
                }

                currentItem = null;
            }
        }
    }
}
