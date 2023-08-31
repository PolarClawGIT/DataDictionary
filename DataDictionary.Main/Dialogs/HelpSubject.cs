using DataDictionary.BusinessLayer.WorkFlows;
using DataDictionary.DataLayer.ApplicationData;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataDictionary.Main.Dialogs
{
    partial class HelpSubject : ApplicationBase
    {
        class FormData
        {
            public Guid HelpId { get; set; } = Guid.Empty;
            public String TargetNameSpace { get; set; } = String.Empty;
            public HelpItem HelpItem { get; set; } = new HelpItem();
        }

        FormData data = new FormData();

        public HelpSubject() : base()
        {
            InitializeComponent();
            this.Icon = Resources.HelpTableOfContent;
            helpToolStripButton.Enabled = false;

            // Setup Images for Tree Control
            SetImages(helpContentNavigation, helpContentImageItems.Values);
        }

        public HelpSubject(String targetNameSpace) : this()
        { data.TargetNameSpace = targetNameSpace; }

        public HelpSubject(Form targetForm) : this()
        { if (targetForm.GetType().FullName is String value) { data.TargetNameSpace = value; } }

        private void HelpSubject_Load(object sender, EventArgs e)
        {
            saveToolStripButton.Enabled = Settings.Default.IsOnLineMode;
            newToolStripButton.Enabled = Settings.Default.IsOnLineMode;

            if (data.HelpId == Guid.Empty && !String.IsNullOrWhiteSpace(data.TargetNameSpace))
            {
                if (Program.Data.HelpSubjects.FirstOrDefault(w => w.NameSpace == data.TargetNameSpace) is HelpItem item && item.HelpId is Guid itemGuid)
                {
                    data.HelpItem = item;
                    data.HelpId = itemGuid;
                }
            }

            NavigateTo(data.TargetNameSpace);
            BindData();
        }

        public void NavigateTo(Form targetForm)
        {
            if (targetForm.GetType().FullName is String value)
            { NavigateTo(value); }
        }

        public void NavigateTo(String targetName)
        {
            data.TargetNameSpace = targetName;
            data.HelpId = Guid.Empty;

            if (Program.Data.HelpSubjects.FirstOrDefault(w => w.NameSpace == data.TargetNameSpace) is HelpItem item && item.HelpId is Guid itemGuid)
            {
                data.HelpItem = item;
                data.HelpId = itemGuid;
                if (!String.IsNullOrWhiteSpace(item.NameSpace)) { data.TargetNameSpace = item.NameSpace; }
            }
            else
            {
                data.HelpItem = new HelpItem();
                if (!String.IsNullOrWhiteSpace(data.TargetNameSpace)) { data.HelpItem.NameSpace = data.TargetNameSpace; }
                if (data.HelpItem.HelpId is Guid newId) { data.HelpId = newId; }
            }
        }


        void BindData()
        {
            errorProvider.Clear();

            if (Program.Data.HelpSubjects.FirstOrDefault(w => w.HelpId == data.HelpId) is HelpItem item)
            {
                data.HelpItem = item;
                if (!String.IsNullOrWhiteSpace(item.NameSpace)) { data.TargetNameSpace = item.NameSpace; }
            }

            if (data.HelpItem is not null)
            {
                helpSubjectData.DataBindings.Add(new Binding(nameof(helpSubjectData.Text), data.HelpItem, nameof(data.HelpItem.HelpSubject)));
                helpNameSpaceData.DataBindings.Add(new Binding(nameof(helpNameSpaceData.Text), data.HelpItem, nameof(data.HelpItem.NameSpace)));
                helpTextData.DataBindings.Add(new Binding(nameof(helpTextData.Rtf), data.HelpItem, nameof(data.HelpItem.HelpText), true));
                ValidateChildren();
            }

            BuildHelpContentTree();
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
                helpContentNodes[e.Node] is HelpItem helpItem &&
                helpItem.HelpId is Guid helpId)
            {
                UnBindData();
                data.HelpId = helpId;
                BindData();
            }
        }
        #endregion


        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
            this.Enabled = false;
            UnBindData();

            this.DoWork(Program.Data.LoadApplicationData(), OnComplete);

            void OnComplete(RunWorkerCompletedEventArgs args)
            {
                if (args.Error is not null) { Program.ShowException(args.Error); }

                NavigateTo("About");
                BindData();
            }
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            UnBindData();
            data.HelpItem = new HelpItem();
            data.TargetNameSpace = String.Empty;
            if (data.HelpItem.HelpId is Guid helpGuid) { data.HelpId = helpGuid; }
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
    }
}
