using DataDictionary.BusinessLayer;
using DataDictionary.DataLayer;
using DataDictionary.DataLayer.DbMetaData;
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
        List<(String ServerName, String DatabaseName)> ServerDatabasePairs = new List<(String ServerName, String DatabaseName)>();

        public DbConnection()
        {
            InitializeComponent();
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
                        !ServerDatabasePairs.Contains((serverName, databaseName)))
                    { ServerDatabasePairs.Add((serverName, databaseName)); }
                }
            }

            // Setup Server list. 
            IEnumerable<String> serverNames = ServerDatabasePairs.OrderBy(o => o.ServerName).Select(s => s.ServerName).Distinct();
            serverNameData.Items.AddRange(serverNames.ToArray());
            if (serverNameData.Items.Count > 0) { serverNameData.SelectedIndex = 0; }

            // Sets up the Database list (needs to be repeated if the Server Name changes)
            IEnumerable<String> databaseNames = ServerDatabasePairs.Where(w => w.ServerName == serverNameData.Text).OrderBy(o => o.DatabaseName).Select(s => s.DatabaseName).Distinct();
            databaseNameData.Items.Clear();
            databaseNameData.Text = String.Empty;
            databaseNameData.Items.AddRange(databaseNames.ToArray());
            if (databaseNameData.Items.Count > 0) { databaseNameData.SelectedIndex = 0; }
            else { databaseNameData.SelectedIndex = -1; }

            // Setup Connection type
            authenticateWindows_CheckedChanged(this, EventArgs.Empty);
        }


        private void serverNameData_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void serverNameData_Validated(object sender, EventArgs e)
        {
            IEnumerable<String> databaseNames = ServerDatabasePairs.Where(w => w.ServerName == serverNameData.Text).OrderBy(o => o.DatabaseName).Select(s => s.DatabaseName).Distinct();
            databaseNameData.Items.Clear();
            databaseNameData.Text = String.Empty;
            databaseNameData.Items.AddRange(databaseNames.ToArray());
            if (databaseNameData.Items.Count > 0) { databaseNameData.SelectedIndex = 0; }
            else { databaseNameData.SelectedIndex = -1; }
        }

        private void connectCommand_Click(object sender, EventArgs e)
        {
            DbContext context = new DbContext() { ServerName = serverNameData.Text };
            this.UseWaitCursor = true;
            Program.WorkerQueue.Enqueue(Program.DbData.GetDatabases(context, onComplete));

            // TODO: Allow connection without specifying a Db, Db list needs to refresh from server.
            // TODO: On succesful Connection, add item to list of settings

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
            }
        }


        private void importCommand_Click(object sender, EventArgs e)
        {
            // TODO: Import the data into the data structure
        }

        #region IColleague
        public event EventHandler<MessageEventArgs>? OnSendMessage;

        public void RecieveMessage(object? sender, MessageEventArgs message)
        {
            throw new NotImplementedException();
        }
        #endregion



    }
}
