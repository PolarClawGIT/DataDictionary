using DataDictionary.BusinessLayer.AppGeneral;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Forms.ApplicationWide;
using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.General
{
    partial class HelpContent : ApplicationData
    {
        FormBinding formData;
        ContentTree formTree;

        //TODO: Continue removing references to helpBinding DataSource.

        public HelpContent() : base()
        {
            InitializeComponent();
            helpToolStripButton.Enabled = false;
            formData = new FormBinding(ref helpBinding);
            formTree = new ContentTree(helpContentNavigation);

            SetIcon(ScopeType.ApplicationHelp);
            SetCommand(
                ScopeType.ApplicationHelp,
                CommandImageType.Add,
                CommandImageType.Open,
                CommandImageType.Import,
                CommandImageType.HistoryDatabase);

            formTree.SetImages();

            CommandButtons[CommandImageType.Add].Text = "Add new Help Subject (blank)";
            CommandButtons[CommandImageType.Open].Text = "Open/Edit the Selected Help Subject Details";
            CommandButtons[CommandImageType.Import].IsEnabled = false;
            CommandButtons[CommandImageType.Import].Text = "Add new Help Subject using Form Data";

            OpenSubject(Settings.Default.DefaultSubject);
        }

        public void OpenSubject(String targetSubject)
        {
            formData.SetPosition(targetSubject);

            if (formData.TryCurrent(out BindingSubject? current))
            { formTree.SetNode(current); }
        }

        public void OpenSubject(IHelpSubjectIndex helpSubject)
        {
            formData.SetPosition(helpSubject);

            if (formData.TryCurrent(out BindingSubject? current))
            { formTree.SetNode(current); }
        }

        public void OpenSubject(Form targetForm)
        {
            formData.AddForm(targetForm);
            formData.SetPosition(targetForm);

            if (formData.TryCurrent(out BindingSubject? current))
            { formTree.SetNode(current); }
        }

        private void HelpContent_Load(object sender, EventArgs e)
        {
            formData.SubjectsChanged += FormData_SubjectsChanged;
            formTree.BuildTree(formData.HelpSubjects);

            helpSubjectData.DataBindings.Add(new Binding(nameof(helpSubjectData.Text), helpBinding, nameof(BindingSubject.Title), false, DataSourceUpdateMode.OnPropertyChanged));
            helpTextData.DataBindings.Add(new Binding(nameof(helpTextData.Rtf), helpBinding, nameof(BindingSubject.Description), false, DataSourceUpdateMode.OnValidation));

            if (formData.TryCurrent(out BindingSubject? current))
            { formTree.SetNode(current); }

            void FormData_SubjectsChanged(Object? sender, EventArgs e)
            {
                formTree.BuildTree(formData.HelpSubjects);

                if (formData.TryCurrent(out BindingSubject? current))
                { formTree.SetNode(current); }
            }
        }


        protected override void AddCommand_Click(Object? sender, EventArgs e)
        {
            base.AddCommand_Click(sender, e);

            HelpSubjectValue newValue = formData.NewSubject();
            OpenSubjectForm();
        }

        protected override void ImportCommand_Click(Object? sender, EventArgs e)
        {
            base.ImportCommand_Click(sender, e);

            if (formData.TryCurrent(out BindingSubject? current)
                && current.SubjectForm is not null)
            { HelpSubjectValue newValue = formData.NewSubject(current); }

            OpenSubjectForm();
        }

        protected override void OpenCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenCommand_Click(sender, e);

            if (formData.TryCurrent(out BindingSubject? current))
            {
                if (current.SubjectIndex is null && current.SubjectForm is not null)
                { HelpSubjectValue newValue = formData.NewSubject(current); }
            }

            OpenSubjectForm();
        }

        private void OpenSubjectForm()
        {
            if (formData.TryCurrent(out BindingSubject? current))
            {
                if (current.SubjectIndex is not null && current.SubjectForm is null)
                {
                    Activate(
                    () => new HelpSubject(current.SubjectIndex),
                    (form) => form.IsOpenItem(current.SubjectIndex));
                }
                else if (current.SubjectIndex is not null && current.SubjectForm is not null)
                {
                    Activate(
                    () => new HelpSubject(current.SubjectIndex, current.SubjectForm),
                    (form) => form.IsOpenItem(current.SubjectIndex));
                }
                else
                { throw new InvalidOperationException("Could not determine correct way to open Help Subject form"); }
            }

        }

        protected override void HistoryCommand_Click(Object sender, EventArgs e)
        {
            base.HistoryCommand_Click(sender, e);

            //if (helpBinding.DataSource is ILoadHistoryData history)
            //{
            //    Form form = Activate(() =>
            //    new HistoryView<HelpSubjectValue, HelpSubject>(ScopeType.ApplicationHelp, history)
            //    { SelectedForm = (subject) => new HelpSubject(subject) });

            //    if (history is IBindingTable table)
            //    { form.Text = String.Format("History: {0}", table.BindingName); }
            //}
        }

        private void HelpContentNavigation_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (formTree.GetSubject(e.Node, out BindingSubject? subject))
            { formData.SetPosition(subject.Path); }
        }

        private void HelpContentNavigation_MouseDoubleClick(object sender, MouseEventArgs e)
        { OpenCommand_Click(sender, EventArgs.Empty); }
    }
}
