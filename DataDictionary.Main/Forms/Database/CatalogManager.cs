﻿using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.WorkFlows;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DomainData.Alias;
using DataDictionary.Main.Controls;
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
    partial class CatalogManager : ApplicationData, IApplicationDataBind
    {
        DbCatalogCollection dbData = new DbCatalogCollection();
        CatalogManagerCollection bindingData = new CatalogManagerCollection();

        Boolean inModelList
        {
            get
            {
                if (catalogBinding.Current is CatalogManagerItem item)
                {
                    DbCatalogKey key = new DbCatalogKey(item);
                    return (BusinessData.DatabaseModel.DbCatalogs.FirstOrDefault(w => key.Equals(w)) is DbCatalogItem);
                }
                else { return false; }
            }
        }

        Boolean inDatabaseList
        {
            get
            {
                if (catalogBinding.Current is CatalogManagerItem item)
                {
                    DbCatalogKey key = new DbCatalogKey(item);
                    return (dbData.FirstOrDefault(w => key.Equals(w)) is DbCatalogItem);
                }
                else { return false; }
            }
        }

        public CatalogManager() :base()
        {
            InitializeComponent();
            toolStrip.TransferItems(catalogContextMenu, 0);

            addDatabaseCommand.Enabled = true;
            removeDatabaseCommand.Enabled = false;

            this.Icon = Resources.Icon_Database;
        }

        private void CatalogManager_Load(object sender, EventArgs e)
        {
            if (Settings.Default.IsOnLineMode)
            {
                List<WorkItem> work = new List<WorkItem>();
                IDatabaseWork factory = BusinessData.GetDbFactory();
                work.Add(factory.OpenConnection());
                work.AddRange(LoadLocalData(factory));
                this.DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            { this.BindData(); }
        }

        public Boolean BindDataCore()
        {
            bindingData.Build(BusinessData.DatabaseModel.DbCatalogs, dbData);

            catalogBinding.DataSource = bindingData;

            catalogNavigation.AutoGenerateColumns = false;
            catalogNavigation.DataSource = catalogBinding;

            CatalogManagerItem? nameOfValues;
            catalogTitleData.DataBindings.Add(new Binding(nameof(catalogTitleData.Text), catalogBinding, nameof(nameOfValues.CatalogTitle)));
            catalogDescriptionData.DataBindings.Add(new Binding(nameof(catalogDescriptionData.Text), catalogBinding, nameof(nameOfValues.CatalogDescription)));
            sourceServerNameData.DataBindings.Add(new Binding(nameof(sourceServerNameData.Text), catalogBinding, nameof(nameOfValues.SourceServerName)));
            sourceDatabaseNameData.DataBindings.Add(new Binding(nameof(sourceDatabaseNameData.Text), catalogBinding, nameof(nameOfValues.SourceDatabaseName)));
            sourceDateData.DataBindings.Add(new Binding(nameof(sourceDateData.Text), catalogBinding, nameof(nameOfValues.SourceDate)));

            return true;
        }

        public void UnbindDataCore()
        {
            catalogTitleData.DataBindings.Clear();
            catalogDescriptionData.DataBindings.Clear();
            sourceServerNameData.DataBindings.Clear();
            sourceDatabaseNameData.DataBindings.Clear();
            sourceDateData.DataBindings.Clear();

            catalogNavigation.DataSource = null;
            catalogBinding.DataSource = null;
        }

        private IReadOnlyList<WorkItem> LoadLocalData(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem() { WorkName = "Clear local data", DoWork = dbData.Clear });
            work.AddRange(factory.CreateLoad(dbData).ToList());

            return work;
        }

        private void DoLocalWork(List<WorkItem> work)
        {
            SendMessage(new Messages.DoUnbindData());

            this.DoWork(work, onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            { 
                SendMessage(new Messages.DoBindData());
                SendMessage(new Messages.RefreshNavigation());
            }
        }

        private void addDatabaseCommand_Click(object? sender, EventArgs e)
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

                dialog.OpenHelp = () => Activate(() => new ApplicationWide.HelpSubject(dialog));

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

                    List<WorkItem> work = new List<WorkItem>();
                    List<NamedScopeItem> names = new List<NamedScopeItem>();
                    DbSchemaContext source = new BusinessLayer.DbSchemaContext()
                    {
                        ServerName = dialog.ServerName,
                        DatabaseName = dialog.DatabaseName
                    };

                    work.AddRange(BusinessData.DatabaseModel.Import(source));
                    work.AddRange(BusinessData.NamedScope.Build());
                    DoLocalWork(work);
                }
            }
        }

        private void removeDatabaseCommand_Click(object? sender, EventArgs e)
        {
            catalogNavigation.EndEdit();
            List<WorkItem> work = new List<WorkItem>();

            if (catalogBinding.Current is CatalogManagerItem item)
            {
                NamedScopeKey scopeKey = new NamedScopeKey(item);
                DbCatalogKey catalogKey = new DbCatalogKey(item);
                work.AddRange(BusinessData.DatabaseModel.Remove(catalogKey));
                work.Add(
                    new WorkItem()
                    {
                        WorkName = "Remove NameScope",
                        DoWork = () => { BusinessData.NamedScope.Remove(scopeKey); }
                    });
            }

            DoLocalWork(work);
        }

        protected override void DeleteFromDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);
            catalogNavigation.EndEdit();

            if (catalogBinding.Current is CatalogManagerItem item)
            {
                List<WorkItem> work = new List<WorkItem>();
                IDatabaseWork factory = BusinessData.GetDbFactory();
                IDbCatalogKey key = new DbCatalogKey(item);

                work.Add(factory.OpenConnection());

                if (inModelList)
                {
                    work.AddRange(BusinessData.DatabaseModel.Remove(key));
                    work.AddRange(BusinessData.DatabaseModel.Save(factory, key));
                }
                else
                {
                    work.Add(new WorkItem() { DoWork = () => { dbData.Remove(key); } });
                    work.Add(factory.CreateSave(dbData, key));
                }

                work.AddRange(LoadLocalData(factory));
                DoLocalWork(work);
            }
        }

        protected override void OpenFromDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);
            catalogNavigation.EndEdit();

            if (catalogBinding.Current is CatalogManagerItem item)
            {
                List<WorkItem> work = new List<WorkItem>();
                IDatabaseWork factory = BusinessData.GetDbFactory();

                DbCatalogKey key = new DbCatalogKey(item);
                work.Add(factory.OpenConnection());
                work.AddRange(BusinessData.DatabaseModel.Load(factory, key));
                work.AddRange(BusinessData.NamedScope.Build());

                DoLocalWork(work);
            }
        }

        protected override void SaveToDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);
            catalogNavigation.EndEdit();

            if (catalogBinding.Current is CatalogManagerItem item)
            {
                List<WorkItem> work = new List<WorkItem>();
                IDatabaseWork factory = BusinessData.GetDbFactory();
                IDbCatalogKey key = new DbCatalogKey(item);

                work.Add(factory.OpenConnection());

                if (inModelList)
                { work.AddRange(BusinessData.DatabaseModel.Save(factory, key)); }
                else
                { work.Add(factory.CreateSave(dbData, key)); }

                work.AddRange(LoadLocalData(factory));

                DoLocalWork(work);
            }
        }

        private void catalogBinding_CurrentChanged(object sender, EventArgs e)
        {
            removeDatabaseCommand.Enabled = inModelList;

            IsOpenDatabase = inDatabaseList && !inModelList;
            IsSaveDatabase = true;
            IsDeleteDatabase = inDatabaseList;
        }
    }
}
