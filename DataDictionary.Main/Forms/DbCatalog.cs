using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.WorkFlows;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms
{
    partial class DbCatalog : ApplicationBase
    {
        class FormData : INotifyPropertyChanged
        {
            private string? serverName;
            public String? ServerName { get { return serverName; } set { serverName = value; OnPropertyChanged(nameof(ServerName)); } }

            public String? databaseName;
            public String? DatabaseName { get { return databaseName; } set { databaseName = value; OnPropertyChanged(nameof(DatabaseName)); } }

            public String? serverUserName;
            public String? ServerUserName { get { return serverUserName; } set { serverUserName = value; OnPropertyChanged(nameof(ServerUserName)); } }

            public String? serverUserPassword;
            public String? ServerUserPassword { get { return serverUserPassword; } set { serverUserPassword = value; OnPropertyChanged(nameof(ServerUserPassword)); } }

            public DbSchemaContext? DbContext
            {
                get
                {
                    if (String.IsNullOrEmpty(ServerName) || String.IsNullOrEmpty(DatabaseName)) { return null; }
                    return new DbSchemaContext()
                    {
                        ServerName = ServerName,
                        DatabaseName = DatabaseName,
                        //TODO: User Name, Password, and authentication option are not yet supported
                        //ServerUserName = serverUserName,
                        //ServerUserPassword = ServerUserPassword 
                    };
                }
                set
                {
                    if (value is DbSchemaContext)
                    {
                        ServerName = value.ServerName;
                        DatabaseName = value.DatabaseName;
                        ServerUserName = value.ServerUserName;
                        ServerUserPassword = value.ServerUserPassword;

                    }
                    else
                    {
                        ServerName = String.Empty;
                        DatabaseName = String.Empty;
                        ServerUserName = String.Empty;
                        ServerUserPassword = String.Empty;
                    }
                }
            }

            public event PropertyChangedEventHandler? PropertyChanged;
            public virtual void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged is PropertyChangedEventHandler handler)
                { handler(this, new PropertyChangedEventArgs(propertyName)); }
            }
        }

        /// <summary>
        /// Persists a list of all the connections this screen used for this session.
        /// </summary>
        static BindingList<DbSchemaContext> availableContexts = new BindingList<DbSchemaContext>();

        FormData data = new FormData();

        public DbCatalog()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_Database;

            // Create list of all known Server/Database Name Pairs to add to AvailableContexts
            foreach (String? item in Settings.Default.UserServers)
            {
                if (String.IsNullOrEmpty(item)) { continue; }
                else
                {
                    string[] dbServer = item.Split('.');

                    if (dbServer.Length > 1 &&
                        dbServer[0] is String serverName &&
                        dbServer[1] is String databaseName &&
                        availableContexts.FirstOrDefault(
                            w => w.ServerName == serverName &&
                            w.DatabaseName == databaseName) is null)
                    { availableContexts.Add(new DbSchemaContext() { ServerName = serverName, DatabaseName = databaseName }); }
                }
            }

            // Load any items in the Catalog not yet in the AvailableContexts
            foreach (DbCatalogItem catalogItem in Program.Data.DbCatalogs)
            {
                if (availableContexts.FirstOrDefault(
                    w => w.ServerName.Equals(catalogItem.SourceServerName, StringComparison.CurrentCultureIgnoreCase) &&
                    w.DatabaseName.Equals(catalogItem.CatalogName, StringComparison.CurrentCultureIgnoreCase)) is null &&
                    catalogItem.SourceServerName is String &&
                    catalogItem.CatalogName is String)
                { availableContexts.Add(new DbSchemaContext() { ServerName = catalogItem.SourceServerName, DatabaseName = catalogItem.CatalogName }); }
            }
        }


        private void DbConnection_Load(object sender, EventArgs e)
        {

            // Setup Server list. 
            IEnumerable<String> serverNames = availableContexts.OrderBy(o => o.ServerName).Select(s => s.ServerName).Distinct();
            serverNameData.Items.AddRange(serverNames.ToArray());

            // Sets up the Database list (needs to be repeated if the Server Name changes)
            IEnumerable<String> databaseNames = availableContexts.OrderBy(o => o.DatabaseName).Select(s => s.DatabaseName).Distinct();
            databaseNameData.Items.Clear();
            databaseNameData.Text = String.Empty;
            databaseNameData.Items.AddRange(databaseNames.ToArray());

            // Setup Connection type
            authenticateWindows_CheckedChanged(this, EventArgs.Empty);

            // Data Binding
            if (availableContexts.Count > 0) { data.DbContext = availableContexts[0]; }
            BindData();
        }

        void BindData()
        {
            dbConnectionsData.AutoGenerateColumns = false;
            dbConnectionsData.DataSource = Program.Data.DbCatalogs;

            serverNameData.DataBindings.Add(new Binding(nameof(serverNameData.SelectedItem), data, nameof(data.ServerName), true, DataSourceUpdateMode.OnValidation));
            databaseNameData.DataBindings.Add(new Binding(nameof(databaseNameData.SelectedItem), data, nameof(data.DatabaseName), true, DataSourceUpdateMode.OnValidation));
            serverUserNameData.DataBindings.Add(new Binding(nameof(serverUserNameData.Text), data, nameof(data.serverUserName), true, DataSourceUpdateMode.OnValidation));
            serverUserPasswordData.DataBindings.Add(new Binding(nameof(serverUserPasswordData.Text), data, nameof(data.serverUserPassword), true, DataSourceUpdateMode.OnValidation));

            // Select the connection
            dbConnectionsData.ClearSelection();

            if (dbConnectionsData.Rows.Cast<DataGridViewRow>().Where(
                w => w.DataBoundItem is DbSchemaContext context &&
                context.ServerName == data.ServerName &&
                context.DatabaseName == data.DatabaseName).FirstOrDefault() is DataGridViewRow row)
            { row.Selected = true; }
        }

        void UnBindData()
        {
            dbConnectionsData.DataSource = null;

            serverNameData.DataBindings.Clear();
            databaseNameData.DataBindings.Clear();
            serverUserNameData.DataBindings.Clear();
            serverUserPasswordData.DataBindings.Clear();
        }

        private void serverNameData_SelectedIndexChanged(object sender, EventArgs e)
        { }

        private void serverNameData_Validated(object sender, EventArgs e)
        { }

        private void authenticateWindows_CheckedChanged(object sender, EventArgs e)
        {
            if (authenticateWindows.Checked)
            {
                serverUserNameData.Enabled = false;
                serverUserNameData.Text = String.Format("{0}\\{1}", SystemInformation.UserDomainName, SystemInformation.UserName);
                serverUserPasswordData.Enabled = false;
                serverUserPasswordData.Text = String.Empty;
            }
            else
            {
                serverUserNameData.Enabled = true;
                serverUserPasswordData.Enabled = true;
            }
        }

        private void connectCommand_Click(object sender, EventArgs e)
        {
            BindingTable<DbDatabaseItem> databaseNames = new BindingTable<DbDatabaseItem>();

            if (data.DbContext is DbSchemaContext)
            { this.DoWork(data.DbContext.GetDatabaseSchema(databaseNames), onComplete) ; }

            void onComplete(RunWorkerCompletedEventArgs result)
            {
                if (!result.Cancelled)
                {

                    if (databaseNames.FirstOrDefault(w => w.CatalogName == databaseNameData.Text) is DbDatabaseItem currentDb)
                    {
                        databaseNameData.Items.Clear();
                        databaseNameData.Text = String.Empty;
                        databaseNameData.Items.AddRange(databaseNames.Select(s => s.CatalogName).ToArray());
                        databaseNameData.SelectedIndex = databaseNameData.Items.IndexOf(currentDb.CatalogName);
                    }
                    else
                    {
                        databaseNameData.Items.Clear();
                        databaseNameData.Text = String.Empty;
                        databaseNameData.Items.AddRange(databaseNames.Select(s => s.CatalogName).ToArray());
                        if (databaseNameData.Items.Count > 0) { databaseNameData.SelectedIndex = 0; }
                        else { databaseNameData.SelectedIndex = -1; }
                    }
                }
            }
        }

        private void importCommand_Click(object sender, EventArgs e)
        {
            UnBindData();
            SendMessage(new DoUnbindData());

            if (data.DbContext is DbSchemaContext)
            {
                if (data.DbContext is DbSchemaContext)
                { this.DoWork(Program.Data.LoadDbSchema(data.DbContext), onCompleting); }
            }

            void onCompleting(RunWorkerCompletedEventArgs result)
            {
                if (!result.Cancelled)
                {
                    String newValue = String.Format("{0}.{1}", data.DbContext.ServerName, data.DbContext.DatabaseName);

                    // Handle the User Settings
                    if (Settings.Default.UserServers.Contains(newValue))
                    { Settings.Default.UserServers.Remove(newValue); }

                    Settings.Default.UserServers.Insert(0, newValue);

                    while (Settings.Default.UserServers.Count > 10)
                    { Settings.Default.UserServers.RemoveAt(10); }

                    Settings.Default.Save();
                }

                // Done
                SendMessage(new DoBindData());
                BindData();

                // Because other forms can sometimes get focus during data binding
                if (ParentForm is Form parent && ParentForm.ActiveMdiChild != this) { this.Activate(); }
            }
        }

        private void removeCommand_Click(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
            this.Enabled = false;
            UnBindData();
            SendMessage(new DoUnbindData());


            if (data.DbContext is DbSchemaContext)
            { this.DoWork(Program.Data.RemoveCatalog(data.DbContext), onComplete); }

            void onComplete(RunWorkerCompletedEventArgs result)
            {
                SendMessage(new DoBindData());
                BindData();
                this.UseWaitCursor = false;
                this.Enabled = true;
            }
        }

        private void dbConnectionsData_SelectionChanged(object sender, EventArgs e)
        {
            if (dbConnectionsData.SelectedRows.Count > 0)
            {
                if (dbConnectionsData.SelectedRows[0].DataBoundItem is DbCatalogItem catalogItem &&
                    availableContexts.FirstOrDefault(
                        w => w.ServerName.Equals(catalogItem.SourceServerName, StringComparison.CurrentCultureIgnoreCase) &&
                        w.DatabaseName.Equals(catalogItem.CatalogName, StringComparison.OrdinalIgnoreCase)) is DbSchemaContext newContext)
                { data.DbContext = newContext; }
                else
                { // Not suppose to happen
                    Exception ex = new InvalidOperationException();
                    ex.Data.Add("BoundItem", dbConnectionsData.SelectedRows[0].DataBoundItem);
                    throw ex;
                }
            }
        }

        #region IColleague
        #endregion
    }
}
