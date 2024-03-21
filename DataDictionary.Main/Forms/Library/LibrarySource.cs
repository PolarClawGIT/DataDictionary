using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.LibraryData.Source;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Properties;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Library
{
    partial class LibrarySource : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingSource.Current is ILibrarySourceItem current && ReferenceEquals(current, item); }

        public LibrarySource() : base()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_Library;
        }

        public LibrarySource(ILibrarySourceItem librarySource) : this ()
        {
            LibrarySourceKeyName key = new LibrarySourceKeyName(librarySource);

            bindingSource.DataSource = new BindingView<LibrarySourceItem>(BusinessData.LibraryModel.LibrarySources, w => key.Equals(w));
            bindingSource.Position = 0;

            if (bindingSource.Current is ILibrarySourceItem current)
            {
                this.Icon = new ScopeKey(current).Scope.ToIcon();
                RowState = current.RowState();
                current.RowStateChanged += RowStateChanged;
                this.Text = current.ToString();
            }
        }

        private void LibrarySource_Load(object sender, EventArgs e)
        {
            ILibrarySourceItem bindingNames;

            libraryTitleData.DataBindings.Add(new Binding(nameof(libraryTitleData.Text), bindingSource, nameof(bindingNames.LibraryTitle)));
            libraryDescriptionData.DataBindings.Add(new Binding(nameof(libraryDescriptionData.Text), bindingSource, nameof(bindingNames.LibraryDescription)));
            asseblyNameData.DataBindings.Add(new Binding(nameof(asseblyNameData.Text), bindingSource, nameof(bindingNames.AssemblyName)));
            sourceFileNameData.DataBindings.Add(new Binding(nameof(sourceFileNameData.Text), bindingSource, nameof(bindingNames.SourceFile)));
            sourceFileDate.DataBindings.Add(new Binding(nameof(sourceFileDate.Text), bindingSource, nameof(bindingNames.SourceDate)));

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingSource.Current is not ILibrarySourceItem);
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
