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

            helpSubjectData.Focus();
        }

        public HelpContent(String targetSubject) : this()
        { formData.SetPosition(targetSubject); }

        public HelpContent(Form targetForm) : this()
        {
            formData.AddForm(targetForm);
            formData.SetPosition(targetForm);
        }

        private void HelpContent_Load(object sender, EventArgs e)
        {
            formTree.BuildTree(formData.HelpSubjects);

            helpSubjectData.DataBindings.Add(new Binding(nameof(helpSubjectData.Text), helpBinding, nameof(BindingSubject.Title), false, DataSourceUpdateMode.OnPropertyChanged));
            helpTextData.DataBindings.Add(new Binding(nameof(helpTextData.Rtf), helpBinding, nameof(BindingSubject.Description), false, DataSourceUpdateMode.OnValidation));
            //BindRtfHelpText();

            if (formData.TryCurrent(out BindingSubject? current))
            { formTree.SetNode(current); }
        }

        private void BindRtfHelpText()
        {
            try // If RTF, bind to the RTF property
            { helpTextData.DataBindings.Add(new Binding(nameof(helpTextData.Rtf), helpBinding, nameof(BindingSubject.Description), false, DataSourceUpdateMode.OnValidation)); }
            catch (Exception) // Else it is not RTF convert to RTF and bind.
            {
                if (helpBinding.Current is HelpSubjectValue subject)
                {
                    helpTextData.Text = subject.HelpText ?? String.Empty;
                    subject.HelpText = helpTextData.Rtf;
                    subject.AcceptChanges();
                }

                helpTextData.DataBindings.Add(new Binding(nameof(helpTextData.Rtf), helpBinding, nameof(BindingSubject.Description), false, DataSourceUpdateMode.OnValidation));
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

            //if (helpBinding.AddNew() is HelpSubjectValue newValue &&
            //    formData.CurrentForm is Form targetForm)
            //{
            //    HelpSubjectIndexPath newNameSpace = targetForm.ToNameSpaceKey();
            //    newValue.HelpSubject = String.Format("(new Subject: {0})", newNameSpace.Member);
            //    newValue.NameSpace = newNameSpace.MemberFullPath;

            //    Activate((data) => new HelpSubject(newValue, targetForm), newValue);
            //}
        }

        private void HelpBinding_ListChanged(object sender, ListChangedEventArgs e)
        {   // ISSUE: this fires multiple times. Be careful and remember prior state.
            // ISSUE: This can fire during InitializeComponent before formData or formTree is constructed.
        }

        protected override void OpenCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenCommand_Click(sender, e);

            //if (formData.TryGetCurrent(out HelpSubjectValue? current))
            //{
            //    if (formData.CurrentForm is Form targetForm
            //        && targetForm.ToNameSpaceKey().ParentOf(new HelpSubjectIndexPath(current)))
            //    { Activate((data) => new HelpSubject(current, targetForm), current); }
            //    else { Activate((data) => new HelpSubject(current), current); }
            //}
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

        private void HelpContentNavigation_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (formTree.GetSubject(e.Node, out BindingSubject? subject))
            { formData.SetPosition(subject.Path); }
        }

        private void HelpContentNavigation_MouseDoubleClick(object sender, MouseEventArgs e)
        { OpenCommand_Click(sender, EventArgs.Empty); }
    }
}
