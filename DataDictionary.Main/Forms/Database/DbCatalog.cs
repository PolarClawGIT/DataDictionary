using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.Main.Properties;
using System.ComponentModel;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Database
{
    partial class DbCatalog : ApplicationBase, IApplicationDataForm<DbCatalogKey>
    {
        public required DbCatalogKey DataKey { get; init; }

        public bool IsOpenItem(object? item)
        { return DataKey.Equals(item); }

        public DbCatalog() : base()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_Database;

            importDataCommand.DropDown = importOptions;
            importDataCommand.Enabled = true;
            importDataCommand.ButtonClick += ImportDataCommand_Click;
            importDataCommand.ToolTipText = "Import the Database to the Domain Model";
        }

        private void DbCatalog_Load(object sender, EventArgs e)
        { (this as IApplicationDataBind).BindData(); }

        public bool BindDataCore()
        {
            if (Program.Data.DbCatalogs.FirstOrDefault(w => DataKey.Equals(w)) is DbCatalogItem data)
            {
                this.Text = new DbCatalogKey(data).ToString();

                catalogTitleData.DataBindings.Add(new Binding(nameof(catalogTitleData.Text), data, nameof(data.CatalogTitle)));
                catalogDescriptionData.DataBindings.Add(new Binding(nameof(catalogDescriptionData.Text), data, nameof(data.CatalogDescription)));
                sourceServerNameData.DataBindings.Add(new Binding(nameof(sourceServerNameData.Text), data, nameof(data.SourceServerName)));
                sourceDatabaseNameData.DataBindings.Add(new Binding(nameof(sourceDatabaseNameData.Text), data, nameof(data.SourceDatabaseName)));
                sourceDateData.DataBindings.Add(new Binding(nameof(sourceDateData.Text), data, nameof(data.SourceDate)));

                return true;
            }
            else { return false; }
        }

        public void UnbindDataCore()
        {
            catalogTitleData.DataBindings.Clear();
            catalogDescriptionData.DataBindings.Clear();
            sourceServerNameData.DataBindings.Clear();
            sourceDatabaseNameData.DataBindings.Clear();
            sourceDateData.DataBindings.Clear();
        }

        private void ImportDataCommand_Click(object? sender, EventArgs e)
        {

            List<WorkItem> work = new List<WorkItem>();

            if (Program.Data.DbCatalogs.FirstOrDefault(w => DataKey.Equals(w)) is DbCatalogItem data)
            {
                if (importOptionEntity.Checked) { work.AddRange(Program.Data.ImportEntity(data)); }
                if (importOptionAttribute.Checked) { work.AddRange(Program.Data.ImportAttribute(data)); }

                SendMessage(new Messages.DoUnbindData());
                this.DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            { SendMessage(new Messages.DoBindData()); }
        }
    }
}
