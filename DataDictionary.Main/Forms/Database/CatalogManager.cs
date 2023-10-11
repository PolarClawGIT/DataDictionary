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

namespace DataDictionary.Main.Forms.Database
{
    partial class CatalogManager : ApplicationBase
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
            this.DoWork(dbData.LoadCatalog(), onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                bindingData.Build(Program.Data.DbCatalogs, dbData);
                BindData();
            }
        }

        void BindData()
        {
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
        }

        void UnbindData()
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

        }

        private void OpenToolStripButton_Click(object? sender, EventArgs e)
        {

        }


    }
}
