using DataDictionary.BusinessLayer.AppCatalog;
using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Database
{
    partial class DbCatalog : ApplicationData, IApplicationDataForm
    {

        public Boolean IsOpenItem(object? item)
        { return bindingSource.Current is ICatalogValue current && ReferenceEquals(current, item); }

        protected DbCatalog() : base()
        {
            InitializeComponent();

            SetRowState(bindingSource);
            SetTitle(bindingSource);
            SetCommand(ScopeType.Database, CommandImageType.Export);
        }

        public DbCatalog(ICatalogValue catalogItem) : this()
        {
            CatalogIndex key = new CatalogIndex(catalogItem);

            bindingSource.DataSource = new BindingView<CatalogValue>(BusinessData.CatalogModel.DbCatalogs, w => key.Equals(w));
            bindingSource.Position = 0;

            CommandButtons[CommandImageType.Export].Text = "to Model";
            CommandButtons[CommandImageType.Export].DropDown = exportOptions;
            exportAll.Image = NavigationEnumeration.GetImage(ScopeType.Model, CommandImageType.Add);
            exportAttributes.Image = NavigationEnumeration.GetImage(ScopeType.ModelAttribute, CommandImageType.Add);
            exportEntites.Image = NavigationEnumeration.GetImage(ScopeType.ModelEntity, CommandImageType.Add);

            exportProcesses.Visible = false; // Disabled until processes are supported
        }

        private void DbCatalog_Load(object sender, EventArgs e)
        {
            ICatalogValue bindingNames;
            catalogTitleData.DataBindings.Add(new Binding(nameof(catalogTitleData.Text), bindingSource, nameof(bindingNames.CatalogTitle)));
            catalogDescriptionData.DataBindings.Add(new Binding(nameof(catalogDescriptionData.Text), bindingSource, nameof(bindingNames.CatalogDescription)));
            sourceServerNameData.DataBindings.Add(new Binding(nameof(sourceServerNameData.Text), bindingSource, nameof(bindingNames.ServerName)));
            sourceDatabaseNameData.DataBindings.Add(new Binding(nameof(sourceDatabaseNameData.Text), bindingSource, nameof(bindingNames.DatabaseName)));
            sourceDateData.DataBindings.Add(new Binding(nameof(sourceDateData.Text), bindingSource, nameof(bindingNames.SourceDate)));

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingSource.Current is not ICatalogValue);
        }

        private void ExportOptionAll_Click(object sender, EventArgs e)
        {
            if (bindingSource.Current is ICatalogValue current)
            {
                throw new NotImplementedException(); // TODO needs to be fixed.
                var newModel = new ModelImport();
                List<WorkItem> work = new List<WorkItem>();
                work.AddRange(newModel.Load(BusinessData.CatalogModel, current));
                work.AddRange(newModel.Build(BusinessData.Model));

                DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            { SendMessage(new RefreshNavigation()); }
        }


        private void ExportEntites_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            //if (bindingSource.Current is ICatalogValue current)
            //{
            //    BusinessData.Model.Entities.Import(BusinessData.Catalog, BusinessData.ApplicationData.Properties, current);
            //    SendMessage(new RefreshNavigation());
            //}
        }

        private void ExportAttributes_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            //if (bindingSource.Current is ICatalogValue current)
            //{
            //    BusinessData.Model.Attributes.Import(BusinessData.Catalog, BusinessData.ApplicationData.Properties, current);
            //    SendMessage(new RefreshNavigation());
            //}
        }

        private void ExportProcesses_Click(object sender, EventArgs e)
        {
            //TODO: Add processes
            throw new NotImplementedException();
        }
    }
}
