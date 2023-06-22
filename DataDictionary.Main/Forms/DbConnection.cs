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
            DbContext currentContext = new DbContext();
            public DbContext CurrentContext
            {
                get { return currentContext; }
                set { currentContext = value; OnPropertyChanged(nameof(CurrentContext)); }
            }

            BindingList<DbContext> availableContexts = new BindingList<DbContext>();
            public BindingList<DbContext> AvailableContexts { get { return availableContexts; } }

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

            if (data.AvailableContexts.Count == 0) { data.CurrentContext = new DbContext(); }
            else { data.CurrentContext = data.AvailableContexts.First(); }
        }

        private void authenticateWindows_CheckedChanged(object sender, EventArgs e)
        {
            if (authenticateWindows.Checked)
            {
                userNameData.Enabled = false;
                userNameData.Text = String.Format("{0}\\{1}", SystemInformation.UserDomainName, SystemInformation.UserName);
                userPasswordData.Enabled = false;
                userPasswordData.Text = String.Empty;
            }
            else
            {
                userNameData.Enabled = true;
                userPasswordData.Enabled = true;
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

            // Hook up Database Connection list
            dbConnectionsData.AutoGenerateColumns = false;
            dbConnectionsData.DataSource = Program.DbData.DbConnections;

            // Data Bindings
            serverNameData.DataBindings.Add(new Binding(nameof(serverNameData.SelectedItem), data.CurrentContext, nameof(data.CurrentContext.ServerName), true, DataSourceUpdateMode.OnValidation));
            databaseNameData.DataBindings.Add(new Binding(nameof(databaseNameData.SelectedItem), data.CurrentContext, nameof(data.CurrentContext.DatabaseName), true, DataSourceUpdateMode.OnValidation));
        }

        private void serverNameData_SelectedIndexChanged(object sender, EventArgs e)
        { }

        private void serverNameData_Validated(object sender, EventArgs e)
        { }

        private void connectCommand_Click(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
            this.Enabled = false;

            Program.WorkerQueue.Enqueue(new DbVerifyConnection(data.CurrentContext) { WorkName = "Open Connection" });
            Program.WorkerQueue.Enqueue(Program.DbData.GetDatabases(data.CurrentContext, onComplete));

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

            Program.WorkerQueue.Enqueue(new DbVerifyConnection(data.CurrentContext) { WorkName = "Open Connection" });
            Program.WorkerQueue.Enqueue(Program.DbData.ImportDb(data.CurrentContext, onComplete));

            void onComplete()
            {
                String newValue = String.Format("{0}.{1}", data.CurrentContext.ServerName, data.CurrentContext.DatabaseName);

                // Handle the User Settings
                if (Settings.Default.UserServers.Contains(newValue))
                { Settings.Default.UserServers.Remove(newValue); }

                Settings.Default.UserServers.Insert(0, newValue);

                while (Settings.Default.UserServers.Count > 10)
                { Settings.Default.UserServers.RemoveAt(10); }

                Settings.Default.Save();

                // Done
                this.UseWaitCursor = false;
                this.Enabled = true;
            }
        }

        private void removeCommand_Click(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
            this.Enabled = false;

            Program.DbData.RemoveDb(data.CurrentContext);
            data.AvailableContexts.Remove(data.CurrentContext);

            if (data.AvailableContexts.Count == 0) { data.CurrentContext = new DbContext(); }
            else { data.CurrentContext = data.AvailableContexts.First(); }

            // Done
            this.UseWaitCursor = false;
            this.Enabled = true;
        }

        private void dbConnectionsData_SelectionChanged(object sender, EventArgs e)
        {
            if (dbConnectionsData.SelectedRows.Count > 0)
            {
                if (dbConnectionsData.SelectedRows[0].DataBoundItem is DbContext context)
                { data.CurrentContext = context; }
            }
        }

        #region IColleague
        public event EventHandler<MessageEventArgs>? OnSendMessage;

        public void RecieveMessage(object? sender, MessageEventArgs message)
        { }

        void SendMessage(MessageEventArgs message)
        {
            if (OnSendMessage is EventHandler<MessageEventArgs> handler)
            { handler(this, message); }
        }
        #endregion
    }
}
