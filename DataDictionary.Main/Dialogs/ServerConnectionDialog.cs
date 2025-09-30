using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Properties;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Data;
using System.Drawing.Drawing2D;

namespace DataDictionary.Main.Dialogs
{
    partial class ServerConnectionDialog : Form
    {
        SqlConnectionStringBuilder connectionBuilder = new SqlConnectionStringBuilder()
        {
            ApplicationName = Application.ProductName,
            IntegratedSecurity = true,
            UserID = String.Format("{0}\\{1}", SystemInformation.UserDomainName, SystemInformation.UserName)
        };

        Image connectionOk = Resources.Icon_ServerDatabase.MergeImage(Resources.StatusOK, ContentAlignment.BottomRight, CompositingMode.SourceOver);
        Image connectionInfo = Resources.Icon_ServerDatabase.MergeImage(Resources.StatusInformation, ContentAlignment.BottomRight, CompositingMode.SourceOver);
        Image connectionInvalid = Resources.Icon_ServerDatabase.MergeImage(Resources.StatusInvalid, ContentAlignment.BottomRight, CompositingMode.SourceOver);

        public List<(String ServerName, String DatabaseName)> Servers { get; } = new List<(String ServerName, String DatabaseName)>();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public String ServerName { get { return connectionBuilder.DataSource; } set { connectionBuilder.DataSource = value; } }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public String DatabaseName { get { return connectionBuilder.InitialCatalog; } set { connectionBuilder.InitialCatalog = value; } }
        public String UserName { get { return connectionBuilder.UserID; } }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Action OpenHelp { get; set; } = () => { };

        public ServerConnectionDialog()
        {
            InitializeComponent();

            refreshDatabaseCommand.Image = Resources.Icon_ServerDatabase.MergeImage(Resources.ItemRefresh);

            this.Icon = Resources.Icon_ServerDatabase;
        }

        private void ServerConnectionDialog_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(ServerName) && Servers.FirstOrDefault() is (String, String) serverValue)
            {
                ServerName = serverValue.ServerName;

                if (String.IsNullOrWhiteSpace(DatabaseName) && Servers.FirstOrDefault(w => w.ServerName == ServerName) is (String, String) dbValue)
                { DatabaseName = dbValue.DatabaseName; }
            }

            serverNameData.DataSource = Servers.GroupBy(g => g.ServerName).Select(s => s.Key).ToList();
            databaseNameData.DataSource = Servers.Where(w => w.ServerName == ServerName).Select(s => s.DatabaseName).ToList();

            accountNameData.DataBindings.Add(new Binding(nameof(accountNameData.Text), this, nameof(this.UserName)));
            serverNameData.DataBindings.Add(new Binding(nameof(serverNameData.Text), this, nameof(this.ServerName)));
            databaseNameData.DataBindings.Add(new Binding(nameof(databaseNameData.Text), this, nameof(this.DatabaseName)));

            serverNameData.SelectedItem = ServerName;
            databaseNameData.SelectedItem = DatabaseName;
        }

        private void serverNameData_SelectedIndexChanged(object sender, EventArgs e)
        {
            validateCommand.Image = connectionInfo;
            databaseNameData.DataSource = Servers.Where(w => w.ServerName == ServerName).Select(s => s.DatabaseName).ToList();
        }

        Boolean ValidateConnection()
        {
            Boolean result = false;

            serverConnectionLayout.UseWaitCursor = true;
            serverConnectionLayout.Enabled = false;
            using (SqlConnection connect = new SqlConnection(connectionBuilder.ConnectionString))
            {
                errorProvider.SetError(accountNameData.ErrorControl, String.Empty);

                try
                {
                    connect.Open();
                    connect.Close();
                    result = true;
                    validateCommand.Image = connectionOk;
                }
                catch (Exception ex)
                {
                    errorProvider.SetError(accountNameData.ErrorControl, ex.Message);
                    validateCommand.Image = connectionInvalid;
                }
            }
            serverConnectionLayout.Enabled = true;
            serverConnectionLayout.UseWaitCursor = false;
            return result;
        }

        List<String> DatabaseList()
        {
            List<String> result = new List<String>();

            serverConnectionLayout.UseWaitCursor = true;
            serverConnectionLayout.Enabled = false;
            using (SqlConnection connect = new SqlConnection(connectionBuilder.ConnectionString))
            {
                errorProvider.SetError(accountNameData.ErrorControl, String.Empty);

                try
                {
                    connect.Open();
                    DataTable databases = connect.GetSchema("Databases");
                    connect.Close();

                    foreach (DataRow row in databases.Rows)
                    {
                        if (row["database_name"] is String dbName && !(dbName is "master" or "msdb" or "tempdb" or "model"))
                        { result.Add(dbName); }
                    }

                    validateCommand.Image = connectionOk;
                }
                catch (Exception ex)
                {
                    errorProvider.SetError(accountNameData.ErrorControl, ex.Message);
                    validateCommand.Image = connectionInvalid;
                }
            }

            serverConnectionLayout.Enabled = true;
            serverConnectionLayout.UseWaitCursor = false;

            return result;
        }

        private void refreshDatabaseCommand_Click(object sender, EventArgs e)
        {
            databaseNameData.DataSource = databaseNameData.DataSource =
                Servers.Where(w => w.ServerName == ServerName).
                Select(s => s.DatabaseName).
                Union(DatabaseList()).ToList();

            databaseNameData.SelectedItem = DatabaseName;
        }

        private void validateCommand_Click(object sender, EventArgs e)
        {
            if (ValidateConnection())
            {
                if (Servers.Count(w =>
                    w.ServerName.Equals(ServerName, StringComparison.CurrentCultureIgnoreCase)
                    && w.DatabaseName.Equals(DatabaseName, StringComparison.CurrentCultureIgnoreCase))
                    == 0)
                { Servers.Add((ServerName = ServerName, DatabaseName = DatabaseName)); }
            }
        }

        private void ServerConnectionDialog_HelpButtonClicked(object sender, CancelEventArgs e)
        { OpenHelp(); }
    }
}
