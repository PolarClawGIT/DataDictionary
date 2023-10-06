using DataDictionary.DataLayer.LibraryData.Source;
using DataDictionary.Main.Properties;
using System.ComponentModel;

namespace DataDictionary.Main.Forms.Library
{
    partial class LibrarySource : ApplicationBase, IApplicationDataForm<LibrarySourceKey>
    {
        public required LibrarySourceKey DataKey { get; init; }

        public bool IsOpenItem(object? item)
        { return DataKey.Equals(item); }

        public LibrarySource() : base()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_Library;
        }

        private void LibrarySource_Load(object sender, EventArgs e)
        { (this as IApplicationDataBind).BindData(); }

        public Boolean BindDataCore()
        {
            if (Program.Data.LibrarySources.FirstOrDefault(w => DataKey.Equals(w)) is LibrarySourceItem sourceItem)
            {
                libraryTitleData.DataBindings.Add(new Binding(nameof(libraryTitleData.Text), sourceItem, nameof(sourceItem.LibraryTitle)));
                libraryDescriptionData.DataBindings.Add(new Binding(nameof(libraryDescriptionData.Text), sourceItem, nameof(sourceItem.LibraryDescription)));
                asseblyNameData.DataBindings.Add(new Binding(nameof(asseblyNameData.Text), sourceItem, nameof(sourceItem.AssemblyName)));
                sourceFileNameData.DataBindings.Add(new Binding(nameof(sourceFileNameData.Text), sourceItem, nameof(sourceItem.SourceFile)));
                sourceFileDate.DataBindings.Add(new Binding(nameof(sourceFileDate.Text), sourceItem, nameof(sourceItem.SourceDate)));

                return true;
            }
            else { return false; }
        }

        public void UnbindDataCore()
        {
            libraryTitleData.DataBindings.Clear();
            libraryDescriptionData.DataBindings.Clear();
            asseblyNameData.DataBindings.Clear();
            sourceFileNameData.DataBindings.Clear();
            sourceFileDate.DataBindings.Clear();
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
