using DataDictionary.DataLayer.ApplicationData;
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

namespace DataDictionary.Main.Forms
{
    partial class HelpSubject : ApplicationFormBase
    {
        class FormData
        {
            public Guid HelpId { get; set; } = Guid.Empty;
            public HelpItem HelpItem { get; set; } = new HelpItem();
        }

        FormData data = new FormData();

        public HelpSubject() : base()
        {
            InitializeComponent();
            this.Icon = Resources.HelpTableOfContent;

            // Setup Images for Tree Control
            SetImages(helpContentNavigation, helpContentImageItems.Values);
        }

        public HelpSubject(String? nameSpace) : this()
        { NavigateTo(nameSpace); }

        public HelpSubject(Form target) : this()
        { NavigateTo(target.GetType().FullName); }

        public void NavigateTo(Form target)
        { NavigateTo(target.GetType().FullName); }

        public void NavigateTo(String? nameSpace)
        {
            if (Program.Data.HelpSubjects.FirstOrDefault(w => w.NameSpace == nameSpace) is HelpItem item)
            { data.HelpItem = item; }
            else
            { if (!String.IsNullOrWhiteSpace(nameSpace)) { data.HelpItem.NameSpace = nameSpace; } }

            if (data.HelpItem.HelpId is Guid helpGuid) { data.HelpId = helpGuid; }
        }

        private void HelpSubject_Load(object sender, EventArgs e)
        {
            this.ValidateChildren();

            openToolStripButton_Click(this, EventArgs.Empty);
        }

        void BindData()
        {
            if (Program.Data.HelpSubjects.FirstOrDefault(w => w.HelpId == data.HelpId) is HelpItem item)
            { data.HelpItem = item; }

            if (data.HelpItem is not null)
            {
                helpSubjectData.DataBindings.Add(new Binding(nameof(helpSubjectData.Text), data.HelpItem, nameof(data.HelpItem.HelpSubject)));
                helpNameSpaceData.DataBindings.Add(new Binding(nameof(helpNameSpaceData.Text), data.HelpItem, nameof(data.HelpItem.NameSpace)));
                helpTextData.DataBindings.Add(new Binding(nameof(helpTextData.Text), data.HelpItem, nameof(data.HelpItem.HelpText)));
            }

            BuildHelpContentTree();
        }

        void UnBindData()
        {
            helpSubjectData.DataBindings.Clear();
            helpNameSpaceData.DataBindings.Clear();
            helpTextData.DataBindings.Clear();
        }

        #region 
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
        #endregion


        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
            this.Enabled = false;
            UnBindData();

            Program.Worker.Enqueue(Program.Data.LoadHelp(), OnComplete);

            void OnComplete(RunWorkerCompletedEventArgs args)
            {
                this.UseWaitCursor = false;
                this.Enabled = true;
                BindData();
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            if (errorProvider.HasErrors)
            { return; } //TODO: Cause the errors to flash?

            this.UseWaitCursor = true;
            this.Enabled = false;
            UnBindData();

            if (data.HelpItem is HelpItem && !Program.Data.HelpSubjects.Contains(data.HelpItem))
            { Program.Data.HelpSubjects.Add(data.HelpItem); }

            Program.Worker.Enqueue(Program.Data.SaveHelp(), OnComplete);

            void OnComplete(RunWorkerCompletedEventArgs args)
            {
                this.UseWaitCursor = false;
                this.Enabled = true;
                BindData();
            }
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            data.HelpItem = new HelpItem();
            if (data.HelpItem.HelpId is Guid helpGuid) { data.HelpId = helpGuid; }
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
    }
}
