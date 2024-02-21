using DataDictionary.BusinessLayer.Database;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.Main.Properties;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Database
{
    partial class DbCatalog : ApplicationBase, IApplicationDataForm
    {

        public Boolean IsOpenItem(object? item)
        { return bindingSource.Current is IDbCatalogItem current && ReferenceEquals(current, item); }

        public DbCatalog() : base()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_Database;

            importDataCommand.DropDown = importOptions;
            importDataCommand.Enabled = true;
            importDataCommand.ButtonClick += ImportDataCommand_Click;
            importDataCommand.ToolTipText = "Import the Database to the Domain Model";
        }

        public DbCatalog(IDbCatalogItem catalogItem) : this()
        {
            bindingSource.DataSource = new BindingList<IDbCatalogItem> { catalogItem };
            RowState = catalogItem.RowState();
            catalogItem.RowStateChanged += CatalogItem_RowStateChanged;
            bindingSource.Position = 0;
            this.Text = catalogItem.ToString();
        }

        private void CatalogItem_RowStateChanged(object? sender, EventArgs e)
        {
            if (sender is IBindingRowState data)
            {
                RowState = data.RowState();
                if (IsHandleCreated)
                { this.Invoke(() => { this.IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted); }); }
                else { this.IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted); }
            }
        }

        private void DbCatalog_Load(object sender, EventArgs e)
        {
            IDbCatalogItem bindingNames;
            catalogTitleData.DataBindings.Add(new Binding(nameof(catalogTitleData.Text), bindingSource, nameof(bindingNames.CatalogTitle)));
            catalogDescriptionData.DataBindings.Add(new Binding(nameof(catalogDescriptionData.Text), bindingSource, nameof(bindingNames.CatalogDescription)));
            sourceServerNameData.DataBindings.Add(new Binding(nameof(sourceServerNameData.Text), bindingSource, nameof(bindingNames.SourceServerName)));
            sourceDatabaseNameData.DataBindings.Add(new Binding(nameof(sourceDatabaseNameData.Text), bindingSource, nameof(bindingNames.SourceDatabaseName)));
            sourceDateData.DataBindings.Add(new Binding(nameof(sourceDateData.Text), bindingSource, nameof(bindingNames.SourceDate)));
        }

        private void ImportDataCommand_Click(object? sender, EventArgs e)
        {
            if (bindingSource.Current is IDbCatalogItem current)
            {
                BusinessData.DomainData.DomainAttributes.Import(BusinessData.DatabaseData, current);
                BusinessData.DomainData.DomainEntities.Import(BusinessData.DatabaseData, current);
            }
        }
    }
}
