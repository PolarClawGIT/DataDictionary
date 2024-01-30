using DataDictionary.BusinessLayer.NameSpace;
using DataDictionary.BusinessLayer.WorkFlows;
using DataDictionary.DataLayer.ApplicationData.Help;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Forms;
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
        List<Control> controlList = new List<Control>();

        Boolean IsLocked
        {
            get { return !helpDetailLayout.Enabled; }
            set { helpDetailLayout.Enabled = !value; }
        }

        public HelpSubject() : base()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_HelpTableOfContent;

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
            if (helpBinding.DataSource is IList<HelpItem> subjects)
            {
                if (subjects.FirstOrDefault(w => w.NameSpace is String && w.NameSpace.Equals(targetForm.GetType().FullName, StringComparison.CurrentCultureIgnoreCase)) is HelpItem subject)
                { helpBinding.Position = subjects.IndexOf(subject); }
            }

            controlList.Add(targetForm);

            AddChild(targetForm);

            void AddChild(Control child)
            {
                foreach (Control item in child.Controls)
                {
                    if (!String.IsNullOrWhiteSpace(item.Name))
                    { controlList.Add(item); }

                    if (child.HasChildren && child is not UserControl)
                    { AddChild(item); }
                }
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

            BuildHelpTree();
            BuildNameSpace();
        }

        void BuildNameSpace()
        {
            nameSpaceBrowser.Items.Clear();

            if (controlList.Count > 0)
            {
                nameSpacesGroup.Text = String.Format("NameSpaces ({0})", controlList[0].GetType().FullName);

                foreach (Control item in controlList)
                {
                    ListViewItem newItem = new ListViewItem(item.Name); // Column 0, Name
                    if (item is not Form)
                    { newItem.SubItems.Add(item.GetType().Name); } // Column 1, Type

                    nameSpaceBrowser.Items.Add(newItem);
                }
            }
        }

        private void NewItemCommand_Click(object? sender, EventArgs e)
        { helpBinding.AddNew(); }

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
                {
                    TreeNode subjectNode = CreateNode(item, helpContentImageIndex.HelpPage, null);
                    helpContentNavigation.Nodes.Add(subjectNode);

                    if (helpBinding.Current is HelpItem current)
                    {
                        HelpKey key = new HelpKey(current);
                        if (key.Equals(item))
                        { helpContentNavigation.SelectedNode = subjectNode; }
                    }
                }
            }

            TreeNode CreateNode(HelpItem source, helpContentImageIndex imageIndex, TreeNode? parentNode = null)
            {
                TreeNode result = new TreeNode(source.HelpSubject);
                result.ImageKey = helpContentImageItems[imageIndex].imageKey;
                result.SelectedImageKey = helpContentImageItems[imageIndex].imageKey;

                if (parentNode is not null) { parentNode.Nodes.Add(result); }
                helpContentNodes.Add(result, source);

                if (helpBinding.DataSource is IEnumerable<HelpItem> items)
                {
                    foreach (HelpItem childItem in items.Where(w => w.HelpParentId == source.HelpId))
                    { CreateNode(childItem, imageIndex, result); }
                }

                return result;
            }

        }

        void SetImages(TreeView tree, IEnumerable<(String imageKey, Image image)> images)
        {
            if (tree.ImageList is null)
            { tree.ImageList = new ImageList(); }

            foreach ((string imageKey, Image image) image in images.Where(w => !tree.ImageList.Images.ContainsKey(w.imageKey)))
            { tree.ImageList.Images.Add(image.imageKey, image.image); }
        }

        private void helpContentNavigation_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
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

            if (controlList.FirstOrDefault() is Control root)
            {
                newItem.HelpSubject = root.Name;
                newItem.NameSpace = new ModelNameSpaceKeyMember(String.Format("{0}", root.GetType().FullName)).MemberFullName;
            }

            e.NewObject = newItem;
        }

        private void helpBinding_DataError(object sender, BindingManagerDataErrorEventArgs e)
        {

        }

        private void helpBinding_BindingComplete(object sender, BindingCompleteEventArgs e)
        {

        }

        private void helpBinding_CurrentChanged(object sender, EventArgs e)
        {
            if (helpBinding.Current is HelpItem current)
            {
                RowState = current.RowState();
                helpNameSpaceData.Enabled = false;
                nameSpacesGroup.Enabled = false;

                if (controlList.FirstOrDefault() is Control root)
                {
                    ModelNameSpaceKeyMember formNameSpace = new ModelNameSpaceKeyMember(String.Format("{0}", root.GetType().FullName));

                    if (current.NameSpace is String)
                    {
                        ModelNameSpaceKeyMember currentNameSpace = new ModelNameSpaceKeyMember(current.NameSpace);

                        if (currentNameSpace.MemberPath == formNameSpace.MemberPath)
                        {
                            helpNameSpaceData.Enabled = true;
                            nameSpacesGroup.Enabled = true;
                        }
                    }
                }
            }
        }

        private void nameSpaceBrowser_DoubleClick(object sender, EventArgs e)
        {
            if (nameSpaceBrowser.SelectedItems.Count > 0)
            {
                Control selected = controlList[nameSpaceBrowser.SelectedItems[0].Index];
                Control root = NameSpaceBrowserRoot(selected);
                ModelNameSpaceKeyMember nameSpace;

                if (selected is Form)
                { nameSpace = new ModelNameSpaceKeyMember(String.Format("{0}", root.GetType().FullName)); }
                else
                { nameSpace = new ModelNameSpaceKeyMember(String.Format("{0}.{1}", root.GetType().FullName, selected.Name)); }

                helpNameSpaceData.Text = nameSpace.MemberFullName;
            }
        }

        Control NameSpaceBrowserRoot(Control current)
        {
            if (current is Form)
            { return current; }
            else if (current.Parent is Control)
            { return NameSpaceBrowserRoot(current.Parent); }
            else { return current; }
        }
    }
}
