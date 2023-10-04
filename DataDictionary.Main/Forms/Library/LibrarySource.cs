using DataDictionary.DataLayer.LibraryData.Source;
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

namespace DataDictionary.Main.Forms.Library
{
    partial class LibrarySource : ApplicationBase, IApplicationDataForm
    {
        public LibrarySourceKey DataKey { get; private set; }

        public LibrarySource() : base()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_Library;
            DataKey = new LibrarySourceKey(new LibrarySourceItem());
        }

        public LibrarySource(ILibrarySourceKey librarySourceItem) : this()
        { DataKey = new LibrarySourceKey(librarySourceItem); }

        public Boolean IsOpenItem(Object? item)
        { return DataKey.Equals(item); }

        private void LibrarySource_Load(object sender, EventArgs e)
        { BindData(); }

        void BindData()
        {
            if (Program.Data.LibrarySources.FirstOrDefault(w => DataKey.Equals(w)) is LibrarySourceItem sourceItem)
            {
                libraryTitleData.DataBindings.Add(new Binding(nameof(libraryTitleData.Text), sourceItem, nameof(sourceItem.LibraryTitle)));
                libraryDescriptionData.DataBindings.Add(new Binding(nameof(libraryDescriptionData.Text), sourceItem, nameof(sourceItem.LibraryDescription)));
                asseblyNameData.DataBindings.Add(new Binding(nameof(asseblyNameData.Text), sourceItem, nameof(sourceItem.AssemblyName)));
                sourceFileNameData.DataBindings.Add(new Binding(nameof(sourceFileNameData.Text), sourceItem, nameof(sourceItem.SourceFile)));
                sourceFileDate.DataBindings.Add(new Binding(nameof(sourceFileDate.Text), sourceItem, nameof(sourceItem.SourceDate)));
            }
        }

        void UnBindData()
        {
            libraryTitleData.DataBindings.Clear();
            libraryDescriptionData.DataBindings.Clear();
            asseblyNameData.DataBindings.Clear();
            sourceFileNameData.DataBindings.Clear();
            sourceFileDate.DataBindings.Clear();
        }

        #region IColleague
        protected override void HandleMessage(DbDataBatchStarting message)
        { UnBindData(); }

        protected override void HandleMessage(DbDataBatchCompleted message)
        { BindData(); }
        #endregion

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
