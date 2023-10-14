using DataDictionary.BusinessLayer.WorkFlows;
using DataDictionary.DataLayer.DatabaseData.Catalog;
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
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Database
{
    partial class CatalogManager : ApplicationBase, IApplicationDataBind
    {
        DbCatalogCollection dbData = new DbCatalogCollection();
        CatalogManagerCollection bindingData = new CatalogManagerCollection();

        public CatalogManager()
        {
            InitializeComponent();
            openToolStripButton.Enabled = true;
            saveToolStripButton.Enabled = Settings.Default.IsOnLineMode;
            openToolStripButton.Click += OpenToolStripButton_Click;
            saveToolStripButton.Click += SaveToolStripButton_Click;

            openToolStripButton.ToolTipText = "Open and add a Database from the Db Schema";
            saveToolStripButton.ToolTipText = "Save the changes back to the application database";
            this.Icon = Resources.Icon_Database;
        }

        private void CatalogManager_Load(object sender, EventArgs e)
        {
            if (Settings.Default.IsOnLineMode)
            { this.DoWork(dbData.LoadCatalog(), onCompleting); }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                bindingData.Build(Program.Data.DbCatalogs, dbData);
                this.BindData();
            }
        }

        public Boolean BindDataCore()
        {
            bindingData.Build(Program.Data.DbCatalogs, dbData);

            catalogBinding.DataSource = bindingData;

            catalogNavigation.AutoGenerateColumns = false;
            catalogNavigation.DataSource = catalogBinding;

            CatalogManagerItem? nameOfValues;
            catalogTitleData.DataBindings.Add(new Binding(nameof(catalogTitleData.Text), catalogBinding, nameof(nameOfValues.CatalogTitle)));
            catalogDescriptionData.DataBindings.Add(new Binding(nameof(catalogDescriptionData.Text), catalogBinding, nameof(nameOfValues.CatalogDescription)));
            sourceServerNameData.DataBindings.Add(new Binding(nameof(sourceServerNameData.Text), catalogBinding, nameof(nameOfValues.SourceServerName)));
            sourceDatabaseNameData.DataBindings.Add(new Binding(nameof(sourceDatabaseNameData.Text), catalogBinding, nameof(nameOfValues.SourceDatabaseName)));
            sourceDateData.DataBindings.Add(new Binding(nameof(sourceDateData.Text), catalogBinding, nameof(nameOfValues.SourceDate)));
            inModelData.DataBindings.Add(new Binding(nameof(inModelData.Checked), catalogBinding, nameof(nameOfValues.InModel), true));
            inDatabaseData.DataBindings.Add(new Binding(nameof(inDatabaseData.Checked), catalogBinding, nameof(nameOfValues.InDatabase), true));

            return true;
        }

        public void UnbindDataCore()
        {
            catalogTitleData.DataBindings.Clear();
            catalogDescriptionData.DataBindings.Clear();
            sourceServerNameData.DataBindings.Clear();
            sourceDatabaseNameData.DataBindings.Clear();
            sourceDateData.DataBindings.Clear();
            inModelData.DataBindings.Clear();
            inDatabaseData.DataBindings.Clear();

            catalogNavigation.DataSource = null;
            catalogBinding.DataSource = null;
        }

        private void SaveToolStripButton_Click(object? sender, EventArgs e)
        {
            catalogNavigation.EndEdit();
            List<WorkItem> work = new List<WorkItem>();

            foreach (CatalogManagerItem item in bindingData.ToList())
            {
                DbCatalogKey key = new DbCatalogKey(item);
                Boolean inDbList = (dbData.FirstOrDefault(w => key.Equals(w)) is DbCatalogItem);
                Boolean inModelList = (Program.Data.DbCatalogs.FirstOrDefault(w => key.Equals(w)) is DbCatalogItem);

                if (item.InModel && !inDbList)
                { work.AddRange(Program.Data.SaveCatalog(key)); }
                else if (item.InModel && item.InDatabase && inModelList)
                { work.AddRange(Program.Data.SaveCatalog(key)); }
                else if (item.InModel && item.InDatabase && !inModelList)
                { work.AddRange(Program.Data.LoadCatalog(key)); }
                else if (!item.InModel && item.InDatabase && inModelList)
                { work.AddRange(Program.Data.RemoveCatalog(key)); }
                else if (!item.InModel && !item.InDatabase && inModelList && !inDbList)
                { work.AddRange(Program.Data.RemoveCatalog(key)); }
                else if (!item.InModel && !item.InDatabase && inModelList && inDbList)
                {
                    bindingData.Remove(item);
                    work.AddRange(Program.Data.RemoveCatalog(key));
                    work.AddRange(Program.Data.DeleteCatalog(key));
                }
                else if (!item.InModel && !item.InDatabase && !inModelList && inDbList)
                {
                    bindingData.Remove(item);
                    work.AddRange(Program.Data.DeleteCatalog(key));
                }
            }
            DoLocalWork(work);
        }

        private void DoLocalWork(List<WorkItem> work)
        {
            SendMessage(new Messages.DoUnbindData());

            dbData.Clear();
            work.AddRange(dbData.LoadCatalog());

            this.DoWork(work, onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            { SendMessage(new Messages.DoBindData()); }
        }

        private void OpenToolStripButton_Click(object? sender, EventArgs e)
        {
            using (Dialogs.ServerConnectionDialog dialog = new Dialogs.ServerConnectionDialog())
            {
                if (catalogBinding.Current is CatalogManagerItem catalogItem
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

                dialog.OpenHelp = () => Activate(() => new Dialogs.HelpSubject(dialog));

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

                    DbCatalogKey? catalogKey = null;
                    if (dbData.FirstOrDefault(w =>
                        w.SourceServerName is String serverName &&
                        w.SourceDatabaseName is String databaseName &&
                        serverName.Equals(dialog.ServerName, StringComparison.CurrentCultureIgnoreCase) &&
                        databaseName.Equals(dialog.DatabaseName, StringComparison.CurrentCultureIgnoreCase))
                        is DbCatalogItem existing)
                    { catalogKey = new DbCatalogKey(existing); }

                    SendMessage(new DoUnbindData());

                    DoWork(Program.Data.LoadDbSchema(
                        new BusinessLayer.DbSchemaContext()
                        {
                            ServerName = dialog.ServerName,
                            DatabaseName = dialog.DatabaseName
                        }, catalogKey), onCompleting);
                }
            }

            void onCompleting(RunWorkerCompletedEventArgs result)
            {
                bindingData.Build(Program.Data.DbCatalogs, dbData);
                SendMessage(new DoBindData());
            }
        }

        protected override void HandleMessage(OnlineStatusChanged message)
        {
            base.HandleMessage(message);
            saveToolStripButton.Enabled = Settings.Default.IsOnLineMode;
        }
    }
}
