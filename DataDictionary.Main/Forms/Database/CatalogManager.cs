﻿using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
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
    partial class CatalogManager : ApplicationData
    {
        CatalogSynchronize catalogs = new CatalogSynchronize(BusinessData.DatabaseModel);

        Boolean inModelList
        {
            get
            {
                if (catalogBinding.Current is CatalogSynchronizeValue item)
                { return item.InModel == true; }
                else { return false; }
            }
        }

        Boolean inDatabaseList
        {
            get
            {
                if (catalogBinding.Current is CatalogSynchronizeValue item)
                { return item.InDatabase == true; }
                else { return false; }
            }
        }

        public CatalogManager() : base()
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
                IsLocked(true);
                List<WorkItem> work = new List<WorkItem>();
                IDatabaseWork factory = BusinessData.GetDbFactory();
                work.Add(factory.OpenConnection());
                work.AddRange(catalogs.GetCatalogs(factory));
                DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                CatalogSynchronizeValue catalogNames;
                catalogBinding.DataSource = catalogs;
                Func<String, String> FormatName = (name) => { return String.Format("{0}.{1}", nameof(catalogNames.Source), name); };

                catalogNavigation.AutoGenerateColumns = false;
                catalogNavigation.DataSource = catalogBinding;

                catalogTitleData.DataBindings.Add(new Binding(nameof(catalogTitleData.Text), catalogBinding, FormatName(nameof(catalogNames.Source.CatalogTitle))));
                catalogDescriptionData.DataBindings.Add(new Binding(nameof(catalogDescriptionData.Text), catalogBinding, FormatName(nameof(catalogNames.Source.CatalogDescription))));
                sourceServerNameData.DataBindings.Add(new Binding(nameof(sourceServerNameData.Text), catalogBinding, FormatName(nameof(catalogNames.Source.SourceServerName))));
                sourceDatabaseNameData.DataBindings.Add(new Binding(nameof(sourceDatabaseNameData.Text), catalogBinding, FormatName(nameof(catalogNames.Source.SourceDatabaseName))));
                sourceDateData.DataBindings.Add(new Binding(nameof(sourceDateData.Text), catalogBinding, FormatName(nameof(catalogNames.Source.SourceDate))));

                IsLocked(false);
            }
        }

        private void addDatabaseCommand_Click(object? sender, EventArgs e)
        {
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
                IsLocked(false);
                SendMessage(new RefreshNavigation());
            }
        }


        private void removeDatabaseCommand_Click(object? sender, EventArgs e)
        {
            catalogNavigation.EndEdit();
            List<WorkItem> work = new List<WorkItem>();

            if (catalogBinding.Current is CatalogSynchronizeValue value && value.Source is ICatalogValue item)
            {
                IsLocked(true);
                CatalogKey catalogKey = new CatalogKey(item);
                work.AddRange(BusinessData.DatabaseModel.Remove(catalogKey));

                DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                IsLocked(false);
                SendMessage(new RefreshNavigation());
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
                CatalogKey key = new CatalogKey(item);

                work.Add(factory.OpenConnection());
                work.AddRange(catalogs.DeleteFromDb(factory, key));

                DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            { IsLocked(false); }
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
                CatalogKey key = new CatalogKey(item);
                work.Add(factory.OpenConnection());
                work.AddRange(catalogs.OpenFromDb(factory, key));

                DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                IsLocked(false);
                SendMessage(new RefreshNavigation());
            }
        }

        protected override void SaveToDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);
            catalogNavigation.EndEdit();

            if (catalogBinding.Current is ICatalogValue item)
            {
                IsLocked(true);
                List<WorkItem> work = new List<WorkItem>();
                IDatabaseWork factory = BusinessData.GetDbFactory();
                CatalogKey key = new CatalogKey(item);

                work.Add(factory.OpenConnection());

                if (inModelList)
                { work.AddRange(BusinessData.DatabaseModel.Save(factory, key)); }

                DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            { IsLocked(false); }
        }

        private void catalogBinding_CurrentChanged(object sender, EventArgs e)
        {
            removeDatabaseCommand.Enabled = inModelList;
            IsOpenDatabase = inDatabaseList && !inModelList;
            IsSaveDatabase = inModelList;
            IsDeleteDatabase = inDatabaseList;
        }
    }
}
