using DataDictionary.BusinessLayer.Database;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Database
{
    partial class DbCatalog : ApplicationData, IApplicationDataForm
    {

        public Boolean IsOpenItem(object? item)
        { return bindingSource.Current is ICatalogValue current && ReferenceEquals(current, item); }

        public DbCatalog() : base()
        {
            InitializeComponent();
            toolStrip.TransferItems(catalogToolStrip, 0);
        }

        public DbCatalog(ICatalogValue catalogItem) : this()
        {
            CatalogIndex key = new CatalogIndex(catalogItem);
            this.Icon = WinFormEnumeration.GetIcon(catalogItem.Scope);

            bindingSource.DataSource = new BindingView<CatalogValue>(BusinessData.DatabaseModel.DbCatalogs, w => key.Equals(w));
            bindingSource.Position = 0;

            Setup(bindingSource);
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

        private void exportCommand_Click(object sender, EventArgs e)
        {
            if (bindingSource.Current is ICatalogValue current)
            {
                BusinessData.DomainModel.Attributes.Import(BusinessData.DatabaseModel, BusinessData.ApplicationData.Properties, current);
                BusinessData.DomainModel.Entities.Import(BusinessData.DatabaseModel, BusinessData.ApplicationData.Properties, current);
            }
        }
    }
}
