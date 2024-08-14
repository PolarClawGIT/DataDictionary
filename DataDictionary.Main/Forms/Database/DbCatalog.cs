using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.Domain;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Resource.Enumerations;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Database
{
    partial class DbCatalog : ApplicationData, IApplicationDataForm
    {

        public Boolean IsOpenItem(object? item)
        { return bindingSource.Current is ICatalogValue current && ReferenceEquals(current, item); }

        protected DbCatalog() : base()
        { InitializeComponent(); }

        public DbCatalog(ICatalogValue catalogItem) : this()
        {
            CatalogIndex key = new CatalogIndex(catalogItem);

            bindingSource.DataSource = new BindingView<CatalogValue>(BusinessData.DatabaseModel.DbCatalogs, w => key.Equals(w));
            bindingSource.Position = 0;

            Setup(bindingSource, CommandImageType.Export);
            CommandButtons[CommandImageType.Export].Text = "to Model";
            CommandButtons[CommandImageType.Export].DropDown = exportOptions;
            exportAll.Image = ImageEnumeration.GetImage(ScopeType.Model, CommandImageType.Add);
            exportAttributes.Image = ImageEnumeration.GetImage(ScopeType.ModelAttribute, CommandImageType.Add);
            exportEntites.Image = ImageEnumeration.GetImage(ScopeType.ModelEntity, CommandImageType.Add);

            exportProcesses.Visible = false; // Disabled until processes are supported
            //exportProcesses.Image = ImageEnumeration.GetImage(ScopeType.ModelProcess, CommandImageType.Add);
        }

        private void DbCatalog_Load(object sender, EventArgs e)
        {
            ICatalogValue bindingNames;
            catalogTitleData.DataBindings.Add(new Binding(nameof(catalogTitleData.Text), bindingSource, nameof(bindingNames.CatalogTitle)));
            catalogDescriptionData.DataBindings.Add(new Binding(nameof(catalogDescriptionData.Text), bindingSource, nameof(bindingNames.CatalogDescription)));
            sourceServerNameData.DataBindings.Add(new Binding(nameof(sourceServerNameData.Text), bindingSource, nameof(bindingNames.SourceServerName)));
            sourceDatabaseNameData.DataBindings.Add(new Binding(nameof(sourceDatabaseNameData.Text), bindingSource, nameof(bindingNames.SourceDatabaseName)));
            sourceDateData.DataBindings.Add(new Binding(nameof(sourceDateData.Text), bindingSource, nameof(bindingNames.SourceDate)));

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingSource.Current is not ICatalogValue);
        }

        private void ExportOptionAll_Click(object sender, EventArgs e)
        {
            if (bindingSource.Current is ICatalogValue current)
            {
                var import = new DatabaseImport(BusinessData.DatabaseModel, BusinessData.DomainModel);
                import.Import(current);
                //TODO: Validate both entities and attributes are working.
                //BusinessData.DomainModel.Entities.Import(BusinessData.DatabaseModel, BusinessData.ApplicationData.Properties, current);
                //BusinessData.DomainModel.Attributes.Import(BusinessData.DatabaseModel, BusinessData.ApplicationData.Properties, current);
                
                SendMessage(new RefreshNavigation());
            }
        }

        private void ExportEntites_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            //if (bindingSource.Current is ICatalogValue current)
            //{
            //    BusinessData.DomainModel.Entities.Import(BusinessData.DatabaseModel, BusinessData.ApplicationData.Properties, current);
            //    SendMessage(new RefreshNavigation());
            //}
        }

        private void ExportAttributes_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            //if (bindingSource.Current is ICatalogValue current)
            //{
            //    BusinessData.DomainModel.Attributes.Import(BusinessData.DatabaseModel, BusinessData.ApplicationData.Properties, current);
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
