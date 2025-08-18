using DataDictionary.BusinessLayer.AppLibrary;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Library
{
    partial class LibrarySource : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingSource.Current is ILibrarySourceValue current && ReferenceEquals(current, item); }

        protected LibrarySource() : base()
        {
            InitializeComponent();
            SetRowState(bindingSource);
        }

        public LibrarySource(ILibrarySourceValue librarySource) : this()
        {
            LibrarySourceIndex key = new LibrarySourceIndex(librarySource);

            bindingSource.DataSource = new BindingView<LibrarySourceValue>(BusinessData.LibraryModel.LibrarySources, w => key.Equals(w));
            bindingSource.Position = 0;
        }

        private void LibrarySource_Load(object sender, EventArgs e)
        {
            libraryTitleData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingSource, nameof(ILibrarySourceValue.LibraryTitle)));
            libraryDescriptionData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingSource, nameof(ILibrarySourceValue.LibraryDescription)));
            asseblyNameData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingSource, nameof(ILibrarySourceValue.AssemblyName)));
            sourceFileNameData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingSource, nameof(ILibrarySourceValue.SourceFile)));
            sourceFileDate.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingSource, nameof(ILibrarySourceValue.SourceDate)));

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingSource.Current is not ILibrarySourceValue);
        }

        private void libraryTitleData_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(libraryTitleData.Text))
            { errorProvider.SetError(libraryTitleData.ErrorControl, "Library Title is required"); }
            else { errorProvider.SetError(libraryTitleData.ErrorControl, String.Empty); }
        }

        private void asseblyNameData_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(asseblyNameData.Text))
            { errorProvider.SetError(asseblyNameData.ErrorControl, "Assembly Name is required"); }
            else { errorProvider.SetError(asseblyNameData.ErrorControl, String.Empty); }
        }
    }
}
