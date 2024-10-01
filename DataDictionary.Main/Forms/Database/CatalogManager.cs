using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Database
{
    partial class CatalogManager : ApplicationData
    {
        CatalogSynchronize catalogs = new CatalogSynchronize(BusinessData.DatabaseModel);

        public CatalogManager() : base()
        {
            InitializeComponent();

            SetCommand(ScopeType.Database,
                CommandImageType.Add,
                CommandImageType.Delete,
                CommandImageType.OpenDatabase,
                CommandImageType.SaveDatabase,
                CommandImageType.DeleteDatabase);
        }

        private void CatalogManager_Load(object sender, EventArgs e)
        {
            if (Settings.Default.IsOnLineMode)
            {
                IsLocked(true);
                List<WorkItem> work = new List<WorkItem>();
                IDatabaseWork factory = BusinessData.GetDbFactory();
                work.Add(factory.OpenConnection());
                work.AddRange(catalogs.GetCatalogs(factory));
                DoWork(work, onCompleting);
            }
            else { BindData(); }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                catalogs.Refresh();
                BindData();
                IsLocked(false);
            }

            void BindData()
            {
                CatalogSynchronizeValue catalogNames;
                catalogBinding.DataSource = catalogs;
                Func<String, String> FormatName = (name) => { return String.Format("{0}.{1}", nameof(catalogNames.Source), name); };

                catalogNavigation.AutoGenerateColumns = false;
                catalogNavigation.DataSource = catalogBinding;

                catalogTitleData.DataBindings.Add(new Binding(nameof(catalogTitleData.Text), catalogBinding, FormatName(nameof(catalogNames.Source.CatalogTitle))));
                catalogDescriptionData.DataBindings.Add(new Binding(nameof(catalogDescriptionData.Text), catalogBinding, FormatName(nameof(catalogNames.Source.CatalogDescription)), false, DataSourceUpdateMode.OnPropertyChanged));
                sourceServerNameData.DataBindings.Add(new Binding(nameof(sourceServerNameData.Text), catalogBinding, FormatName(nameof(catalogNames.Source.SourceServerName))));
                sourceDatabaseNameData.DataBindings.Add(new Binding(nameof(sourceDatabaseNameData.Text), catalogBinding, FormatName(nameof(catalogNames.Source.SourceDatabaseName))));
                sourceDateData.DataBindings.Add(new Binding(nameof(sourceDateData.Text), catalogBinding, FormatName(nameof(catalogNames.Source.SourceDate))));
            }
        }

        protected override void AddCommand_Click(Object? sender, EventArgs e)
        {
            base.AddCommand_Click(sender, e);

            using (Dialogs.ServerConnectionDialog dialog = new Dialogs.ServerConnectionDialog())
            {
                if (catalogBinding.Current is ICatalogValue catalogItem
                    && catalogItem.SourceServerName is String
                    && catalogItem.SourceDatabaseName is String)
                {
                    dialog.ServerName = catalogItem.SourceServerName;
                    dialog.DatabaseName = catalogItem.SourceDatabaseName;
                }

                foreach (String? item in Settings.Default.UserServers)
                {
                    if (String.IsNullOrEmpty(item)) { continue; }
                    else
                    {
                        string[] dbServer = item.Split('.');

                        if (dbServer.Length > 1 &&
                            dbServer[0] is String serverName &&
                            dbServer[1] is String databaseName)
                        { dialog.Servers.Add((serverName, databaseName)); }
                    }
                }

                dialog.OpenHelp = () => Activate(() => new General.HelpContent(dialog));

                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    String newValue = String.Format("{0}.{1}", dialog.ServerName, dialog.DatabaseName);

                    // Handle the User Settings
                    if (Settings.Default.UserServers.Contains(newValue))
                    { Settings.Default.UserServers.Remove(newValue); }

                    Settings.Default.UserServers.Insert(0, newValue);

                    while (Settings.Default.UserServers.Count > 10)
                    { Settings.Default.UserServers.RemoveAt(10); }

                    Settings.Default.Save();

                    List<WorkItem> work = new List<WorkItem>();
                    DbSchemaContext source = new BusinessLayer.DbSchemaContext()
                    {
                        ServerName = dialog.ServerName,
                        DatabaseName = dialog.DatabaseName
                    };

                    IsLocked(true);
                    work.AddRange(catalogs.ImportFromSchema(source));
                    DoWork(work, onCompleting);
                }
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                catalogs.Refresh();
                SendMessage(new RefreshNavigation());
                IsLocked(false);
            }
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);

            catalogNavigation.EndEdit();
            List<WorkItem> work = new List<WorkItem>();

            if (catalogBinding.Current is CatalogSynchronizeValue value && value.Source is ICatalogValue item)
            {
                IsLocked(true);
                CatalogIndex catalogKey = new CatalogIndex(item);
                work.AddRange(BusinessData.DatabaseModel.Delete(catalogKey));

                DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                catalogs.Refresh();
                SendMessage(new RefreshNavigation());
                IsLocked(false);
            }
        }

        protected override void DeleteFromDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);
            catalogNavigation.EndEdit();

            if (catalogBinding.Current is CatalogSynchronizeValue value && value.Source is ICatalogValue item)
            {
                IsLocked(true);
                List<WorkItem> work = new List<WorkItem>();
                IDatabaseWork factory = BusinessData.GetDbFactory();
                CatalogIndex key = new CatalogIndex(item);

                work.Add(factory.OpenConnection());
                work.AddRange(catalogs.DeleteFromDb(factory, key));
                work.AddRange(catalogs.GetCatalogs(factory));
                DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                catalogs.Refresh();
                IsLocked(false);
            }
        }

        protected override void OpenFromDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);
            catalogNavigation.EndEdit();

            if (catalogBinding.Current is CatalogSynchronizeValue value && value.Source is ICatalogValue item)
            {
                IsLocked(true);
                List<WorkItem> work = new List<WorkItem>();
                IDatabaseWork factory = BusinessData.GetDbFactory();
                CatalogIndex key = new CatalogIndex(item);
                work.Add(factory.OpenConnection());
                work.AddRange(catalogs.OpenFromDb(factory, key));

                DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                catalogs.Refresh();
                SendMessage(new RefreshNavigation());
                IsLocked(false);
            }
        }

        protected override void SaveToDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);
            catalogNavigation.EndEdit();

            if (catalogBinding.Current is CatalogSynchronizeValue value && value.Source is ICatalogValue item)
            {
                IsLocked(true);
                List<WorkItem> work = new List<WorkItem>();
                IDatabaseWork factory = BusinessData.GetDbFactory();
                CatalogIndex key = new CatalogIndex(item);

                work.Add(factory.OpenConnection());

                if (GetInModel())
                {
                    work.AddRange(catalogs.SaveToDb(factory, key));
                    work.AddRange(catalogs.GetCatalogs(factory));
                }

                DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                catalogs.Refresh();
                IsLocked(false);
            }
        }

        private Boolean GetInModel()
        {
            if (catalogBinding.Current is CatalogSynchronizeValue item)
            { return item.InModel == true; }
            else { return false; }
        }

        private Boolean GetInDatabase()
        {
            if (catalogBinding.Current is CatalogSynchronizeValue item)
            { return item.InDatabase == true; }
            else { return false; }
        }

        private void CatalogBinding_CurrentChanged(object sender, EventArgs e)
        {
            CommandButtons[CommandImageType.Delete].IsEnabled = GetInModel();
            
            CommandButtons[CommandImageType.OpenDatabase].IsEnabled = GetInDatabase() && !GetInModel();
            CommandButtons[CommandImageType.SaveDatabase].IsEnabled = GetInModel();
            CommandButtons[CommandImageType.DeleteDatabase].IsEnabled = GetInDatabase();
        }
    }
}
