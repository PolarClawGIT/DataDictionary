using DataDictionary.BusinessLayer.WorkFlows;
using DataDictionary.DataLayer.ApplicationData.Help;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Forms;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
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

namespace DataDictionary.Main.Dialogs
{
    partial class HelpSubject : ApplicationBase
    {
        HelpItem helpItem = new HelpItem();
        HelpKey helpKey;
        HelpKeyUnique initNameSpace;
        String initHelpSubject = String.Empty;

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

            helpKey = new HelpKey(helpItem);
            initNameSpace = new HelpKeyUnique(helpItem);

            // Setup Images for Tree Control
            SetImages(helpContentNavigation, helpContentImageItems.Values);
        }


        public HelpSubject(Object targetForm) : this()
        {
            initNameSpace = new HelpKeyUnique(targetForm);
            helpItem.NameSpace = initNameSpace.NameSpace;
            helpItem.HelpSubject = targetForm.GetType().Name;
        }

        public HelpSubject(String targetSubject) : this()
        {
            initHelpSubject = targetSubject;
            helpItem.HelpSubject = targetSubject;
        }

        private void NewItemCommand_Click(object? sender, EventArgs e)
        {
            UnBindData();
            helpItem = new HelpItem();
            helpKey = new HelpKey(helpItem);
            BindData();
        }

        private void DeleteItemCommand_Click(object? sender, EventArgs e)
        {
            if (Program.Data.HelpSubjects.FirstOrDefault(w => helpKey.Equals(w)) is HelpItem item)
            {
                UnBindData();
                Program.Data.HelpSubjects.Remove(item);
                helpItem = new HelpItem();
                helpKey = new HelpKey(helpItem);
                BuildHelpContentTree();
                BindData();
            }
        }


        private void HelpSubject_Load(object sender, EventArgs e)
        {
            BuildHelpContentTree();
            BindData();
        }

        void BindData()
        {
            errorProvider.Clear();

            if (Program.Data.HelpSubjects.FirstOrDefault(w => helpKey.Equals(w)) is HelpItem byKey)
            { helpItem = byKey; }
            else if (Program.Data.HelpSubjects.FirstOrDefault(w => initNameSpace.Equals(w)) is HelpItem byNameSpace)
            { helpItem = byNameSpace; helpKey = new HelpKey(byNameSpace); }
            else if (Program.Data.HelpSubjects.FirstOrDefault(w => String.Equals(w.HelpSubject, initHelpSubject, StringComparison.OrdinalIgnoreCase)) is HelpItem bySubject)
            { helpItem = bySubject; helpKey = new HelpKey(bySubject); }

            if (helpItem is not null)
            {
                helpSubjectData.DataBindings.Add(new Binding(nameof(helpSubjectData.Text), helpItem, nameof(helpItem.HelpSubject)));
                helpNameSpaceData.DataBindings.Add(new Binding(nameof(helpNameSpaceData.Text), helpItem, nameof(helpItem.NameSpace)));
                helpTextData.DataBindings.Add(new Binding(nameof(helpTextData.Rtf), helpItem, nameof(helpItem.HelpText)));
                ValidateChildren();
            }
        }

        void UnBindData()
        {
            helpSubjectData.DataBindings.Clear();
            helpNameSpaceData.DataBindings.Clear();
            helpTextData.DataBindings.Clear();
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

        void BuildHelpContentTree()
        {
            helpContentNavigation.Nodes.Clear();
            helpContentNodes.Clear();

            foreach (HelpItem item in Program.Data.HelpSubjects.Where(w => w.HelpParentId is null))
            {
                TreeNode subjectNode = CreateNode(item, helpContentImageIndex.HelpPage, null);
                helpContentNavigation.Nodes.Add(subjectNode);

                if (helpKey.Equals(item)) { helpContentNavigation.SelectedNode = subjectNode; }
            }

            TreeNode CreateNode(HelpItem source, helpContentImageIndex imageIndex, TreeNode? parentNode = null)
            {
                TreeNode result = new TreeNode(source.HelpSubject);
                result.ImageKey = helpContentImageItems[imageIndex].imageKey;
                result.SelectedImageKey = helpContentImageItems[imageIndex].imageKey;

                if (parentNode is not null) { parentNode.Nodes.Add(result); }
                helpContentNodes.Add(result, source);

                foreach (HelpItem childItem in Program.Data.HelpSubjects.Where(w => w.HelpParentId == source.HelpId))
                { CreateNode(childItem, imageIndex, result); }

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
            if (helpContentNodes.ContainsKey(e.Node) &&
                helpContentNodes[e.Node] is HelpItem item)
            {
                UnBindData();
                helpItem = item;
                helpKey = new HelpKey(item);
                BindData();
            }
        }
        #endregion

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            UnBindData();
            helpItem = new HelpItem();
            helpKey = new HelpKey(helpItem);
            BindData();
        }

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
        { UnBindData(); }

        protected override void HandleMessage(DbApplicationBatchCompleted message)
        { BuildHelpContentTree(); BindData();  }
        #endregion
    }
}
