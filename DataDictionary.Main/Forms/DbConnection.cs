using DataDictionary.BusinessLayer;
using DataDictionary.DataLayer;
using DataDictionary.DataLayer.DbMetaData;
using DataDictionary.DataLayer.WorkDbItem;
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
using Toolbox.Mediator;

namespace DataDictionary.Main.Forms
{
    public partial class DbConnection : Form, IColleague
    {
        class FormData : INotifyPropertyChanged
        {
            public BindingList<DbContext> AvailableContexts { get; } = new BindingList<DbContext>();

            private string? serverName;
            public String? ServerName { get { return serverName; } set { serverName = value; OnPropertyChanged(nameof(ServerName)); } }

            public String? databaseName;
            public String? DatabaseName { get { return databaseName; } set { databaseName = value; OnPropertyChanged(nameof(DatabaseName)); } }

            public String? serverUserName;
            public String? ServerUserName { get { return serverUserName; } set { serverUserName = value; OnPropertyChanged(nameof(ServerUserName)); } }

            public String? serverUserPassword;
            public String? ServerUserPassword { get { return serverUserPassword; } set { serverUserPassword = value; OnPropertyChanged(nameof(ServerUserPassword)); } }

            public DbContext? DbContext
            {
                get
                {
                    if (String.IsNullOrEmpty(ServerName) || String.IsNullOrEmpty(DatabaseName)) { return null; }
                    return new DbContext()
                    {
                        ServerName = ServerName,
                        DatabaseName = DatabaseName,
                        //TODO: User Name, Password, and authentcation option are not yet supported
                        //ServerUserName = serverUserName,
                        //ServerUserPassword = ServerUserPassword 
                    };
                }
                set
                {
                    if (value is DbContext)
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

        FormData data = new FormData();

        public DbConnection()
        {
            InitializeComponent();
            Program.Messenger.AddColleague(this);
            SendMessage(new Messages.FormAddMdiChild() { ChildForm = this });

            // Create list of all known Server/Database Name Pairs
            foreach (String? item in Settings.Default.UserServers)
            {
                if (String.IsNullOrEmpty(item)) { continue; }
                else
                {
                    string[] dbServer = item.Split('.');

                    if (dbServer.Length > 1 &&
                        dbServer[0] is String serverName &&
                        dbServer[1] is String databaseName &&
                        data.AvailableContexts.FirstOrDefault(
                            w => w.ServerName == serverName &&
                            w.DatabaseName == databaseName) is null)
                    { data.AvailableContexts.Add(new DbContext() { ServerName = serverName, DatabaseName = databaseName }); }
                }
            }
        }


        private void DbConnection_Load(object sender, EventArgs e)
        {

            // Setup Server list. 
            IEnumerable<String> serverNames = data.AvailableContexts.OrderBy(o => o.ServerName).Select(s => s.ServerName).Distinct();
            serverNameData.Items.AddRange(serverNames.ToArray());

            // Sets up the Database list (needs to be repeated if the Server Name changes)
            IEnumerable<String> databaseNames = data.AvailableContexts.OrderBy(o => o.DatabaseName).Select(s => s.DatabaseName).Distinct();
            databaseNameData.Items.Clear();
            databaseNameData.Text = String.Empty;
            databaseNameData.Items.AddRange(databaseNames.ToArray());

            // Setup Connection type
            authenticateWindows_CheckedChanged(this, EventArgs.Empty);

            // Data Binding
            if (data.AvailableContexts.Count > 0) { data.DbContext = data.AvailableContexts[0]; }
            BindData();
        }

        void BindData()
        {
            dbConnectionsData.AutoGenerateColumns = false;
            dbConnectionsData.DataSource = Program.DbData.DbConnections;

            serverNameData.DataBindings.Add(new Binding(nameof(serverNameData.SelectedItem), data, nameof(data.ServerName), true, DataSourceUpdateMode.OnValidation));
            databaseNameData.DataBindings.Add(new Binding(nameof(databaseNameData.SelectedItem), data, nameof(data.DatabaseName), true, DataSourceUpdateMode.OnValidation));
            serverUserNameData.DataBindings.Add(new Binding(nameof(serverUserNameData.Text), data, nameof(data.serverUserName), true, DataSourceUpdateMode.OnValidation));
            serverUserPasswordData.DataBindings.Add(new Binding(nameof(serverUserPasswordData.Text), data, nameof(data.serverUserPassword), true, DataSourceUpdateMode.OnValidation));

            // Select the connection
            dbConnectionsData.ClearSelection();

            if (dbConnectionsData.Rows.Cast<DataGridViewRow>().Where(
                w => w.DataBoundItem is DbContext context &&
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
            this.UseWaitCursor = true;
            this.Enabled = false;

            if (data.DbContext is DbContext)
            {
                Program.WorkerQueue.Enqueue(new DbVerifyConnection(data.DbContext) { WorkName = "Open Connection" });
                Program.WorkerQueue.Enqueue(Program.DbData.GetDatabases(data.DbContext, onComplete));
            }

            void onComplete(IEnumerable<IDbCatalogItem> catalogs)
            {
                IEnumerable<String?> databaseNames = catalogs.Where(w => w.IsSystem == false).OrderBy(o => o.CatalogName).Select(s => s.CatalogName).Distinct();

                if (databaseNames.FirstOrDefault(w => w == databaseNameData.Text) is String currentDb)
                {
                    databaseNameData.Items.Clear();
                    databaseNameData.Text = String.Empty;
                    databaseNameData.Items.AddRange(databaseNames.ToArray());
                    databaseNameData.SelectedIndex = databaseNameData.Items.IndexOf(currentDb);
                }
                else
                {
                    databaseNameData.Items.Clear();
                    databaseNameData.Text = String.Empty;
                    databaseNameData.Items.AddRange(databaseNames.ToArray());
                    if (databaseNameData.Items.Count > 0) { databaseNameData.SelectedIndex = 0; }
                    else { databaseNameData.SelectedIndex = -1; }
                }

                this.UseWaitCursor = false;
                this.Enabled = true;
            }
        }

        private void importCommand_Click(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
            this.Enabled = false;
            UnBindData();
            SendMessage(new DbDataBatchStarting());

            if (data.DbContext is DbContext)
            {
                if (data.DbContext is DbContext) { Program.WorkerQueue.Enqueue(Program.DbData.RemoveDb(data.DbContext)); }
                Program.WorkerQueue.Enqueue(new DbVerifyConnection(data.DbContext) { WorkName = "Open Connection" });
                Program.WorkerQueue.Enqueue(Program.DbData.ImportDb(data.DbContext, onComplete));
            }

            void onComplete()
            {
                String newValue = String.Format("{0}.{1}", data.DbContext.ServerName, data.DbContext.DatabaseName);

                // Handle the User Settings
                if (Settings.Default.UserServers.Contains(newValue))
                { Settings.Default.UserServers.Remove(newValue); }

                Settings.Default.UserServers.Insert(0, newValue);

                while (Settings.Default.UserServers.Count > 10)
                { Settings.Default.UserServers.RemoveAt(10); }

                Settings.Default.Save();


                // Done
                SendMessage(new DbDataBatchCompleted());
                BindData();

                this.UseWaitCursor = false;
                this.Enabled = true;
            }
        }

        private void removeCommand_Click(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
            this.Enabled = false;
            UnBindData();
            SendMessage(new DbDataBatchStarting());


            if (data.DbContext is DbContext)
            { Program.WorkerQueue.Enqueue(Program.DbData.RemoveDb(data.DbContext, onComplete)); }

            void onComplete()
            {
                SendMessage(new DbDataBatchCompleted());
                BindData();
                this.UseWaitCursor = false;
                this.Enabled = true;
            }
        }

        private void dbConnectionsData_SelectionChanged(object sender, EventArgs e)
        {
            if (dbConnectionsData.SelectedRows.Count > 0)
            {
                if (dbConnectionsData.SelectedRows[0].DataBoundItem is DbContext context)
                { data.DbContext = context; }
            }
        }

        #region IColleague
        public event EventHandler<MessageEventArgs>? OnSendMessage;

        public void RecieveMessage(object? sender, MessageEventArgs message)
        { HandleMessage((dynamic)message); }

        void SendMessage(MessageEventArgs message)
        {
            if (OnSendMessage is EventHandler<MessageEventArgs> handler)
            { handler(this, message); }
        }

        void HandleMessage(MessageEventArgs message) { }
        #endregion
    }
}
