using DataDictionary.BusinessLayer;
using DataDictionary.DataLayer.DbMetaData;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using System.ComponentModel;
using Toolbox.BindingTable;
using Toolbox.Mediator;
using Toolbox.Threading;
using Toolbox.Threading.WorkItem;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace DataDictionary.Main
{
    partial class Main : Form, IColleague
    {
        #region Static Data
        enum navigationTabImageIndex
        {
            Database,
            Domain
        }
        Dictionary<navigationTabImageIndex, Image> navigationTabImages = new Dictionary<navigationTabImageIndex, Image>()
        {
            {navigationTabImageIndex.Database, Resources.Database },
            {navigationTabImageIndex.Domain, Resources.Dictionary }
        };
        #endregion

        public Main()
        {
            InitializeComponent();
            ChildFormOpening += Main_ChildFormOpening;

            navigationTabs.ImageList = new ImageList();
            foreach (navigationTabImageIndex item in Enum.GetValues(typeof(navigationTabImageIndex)))
            { navigationTabs.ImageList.Images.Add(item.ToString(), navigationTabImages[item]); }
            navigationDbSchemaTab.ImageKey = navigationTabImageIndex.Database.ToString();
            navigationDomainTab.ImageKey = navigationTabImageIndex.Domain.ToString();
        }



        private void Main_Load(object sender, EventArgs e)
        {
            Program.WorkerQueue.ProgressChanged += WorkerQueue_ProgressChanged;
            Program.Messenger.AddColleague(this);
            Program.DbData.ListChanged += DbData_ListChanged;

        }

        #region DbData
        Dictionary<TreeNode, Object> dbDataNodes = new Dictionary<TreeNode, Object>();
        enum dbDataImageIndex
        {
            Database,
            Schema,
            Tables,
            Table,
            Columns,
            Column
        }

        static Dictionary<dbDataImageIndex, (String imageKey, Image image)> dbDataImageItems = new Dictionary<dbDataImageIndex, (String imageKey, Image image)>()
        {
            {dbDataImageIndex.Database, ("Database",Resources.Database) },
            {dbDataImageIndex.Schema,   ("Schema",  Resources.Schema) },
            {dbDataImageIndex.Tables,   ("Tables",  Resources.TableGroup) },
            {dbDataImageIndex.Table,    ("Table",   Resources.Table) },
            {dbDataImageIndex.Columns,  ("Columns", Resources.ColumnGroup) },
            {dbDataImageIndex.Column,   ("Column",  Resources.Column) },
        };

        private void DbData_ListChanged(object? sender, ListChangedEventArgs e)
        {
            dbMetaDataNavigation.Nodes.Clear();
            dbDataNodes.Clear();

            if (dbMetaDataNavigation.ImageList is null)
            {
                dbMetaDataNavigation.ImageList = new ImageList();
                foreach (dbDataImageIndex item in Enum.GetValues(typeof(dbDataImageIndex)))
                { dbMetaDataNavigation.ImageList.Images.Add(dbDataImageItems[item].imageKey, dbDataImageItems[item].image); }
            }

            foreach (IDbCatalogItem catalogItem in Program.DbData.DbCatalogs.OrderBy(o => o.CatalogName))
            {
                TreeNode catalogNode = CreateNode(catalogItem.CatalogName, dbDataImageIndex.Database, catalogItem);
                dbMetaDataNavigation.Nodes.Add(catalogNode);

                foreach (IDbSchemaItem schemaItem in Program.DbData.DbSchemas.OrderBy(o => o.SchemaName).Where(
                    w => w.IsSystem == false &&
                    w.CatalogName == catalogItem.CatalogName))
                {
                    TreeNode schemaNode = CreateNode(schemaItem.SchemaName, dbDataImageIndex.Schema, schemaItem, catalogNode);
                    TreeNode tablesNode = CreateNode("Tables", dbDataImageIndex.Tables, null, schemaNode);

                    foreach (IDbTableItem tableItem in Program.DbData.DbTables.OrderBy(o => o.TableName).Where(
                        w => w.IsSystem == false &&
                        w.CatalogName == schemaItem.CatalogName &&
                        w.SchemaName == schemaItem.SchemaName))
                    {
                        TreeNode tableNode = CreateNode(tableItem.TableName, dbDataImageIndex.Table, tableItem, tablesNode);
                        TreeNode columnsNode = CreateNode("Columns", dbDataImageIndex.Columns, null, tableNode);

                        foreach (IDbColumnItem columnItem in Program.DbData.DbColumns.OrderBy(o => o.OrdinalPosition).Where(
                            w => w.IsSystem == false &&
                            w.CatalogName == tableItem.CatalogName &&
                            w.SchemaName == tableItem.SchemaName &&
                            w.TableName == tableItem.TableName))
                        { TreeNode columnNode = CreateNode(columnItem.ColumnName, dbDataImageIndex.Column, columnItem, columnsNode); }
                    }
                }
            }

            TreeNode CreateNode(String? nodeText, dbDataImageIndex imageIndex, Object? source = null, TreeNode? parentNode = null)
            {
                if (String.IsNullOrWhiteSpace(nodeText)) { throw new ArgumentNullException(nameof(nodeText)); }

                TreeNode result = new TreeNode(nodeText);
                result.ImageKey = dbDataImageItems[imageIndex].imageKey;
                result.SelectedImageKey = dbDataImageItems[imageIndex].imageKey;

                if (parentNode is not null) { parentNode.Nodes.Add(result); }
                if (source is not null) { dbDataNodes.Add(result, source); }

                return result;
            }
        }
        #endregion

        private void Main_FormClosing(object? sender, FormClosingEventArgs e)
        { }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.WorkerQueue.ProgressChanged -= WorkerQueue_ProgressChanged;
            Program.DbData.ListChanged -= DbData_ListChanged;
        }

        private void WorkerQueue_ProgressChanged(object? sender, WorkerProgressChangedEventArgs e)
        {
            toolStripProgressBar.Value = e.ProgressPercent;
            toolStripWorkerTask.Text = e.ProgressText;
        }


        private void importFromDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.FirstOrDefault(w => w.GetType() == typeof(Forms.DbConnection)) is Forms.DbConnection existingForm)
            { existingForm.Activate(); }
            else
            {
                Form newForm = new Forms.DbConnection();
                newForm.MdiParent = this;
                newForm.Show();
            }
        }


        #region IColleague
        event EventHandler<FormOpenMessage> ChildFormOpening;
        private void Main_ChildFormOpening(object? sender, FormOpenMessage e)
        { e.FormOpened.MdiParent = this; }

        public event EventHandler<MessageEventArgs>? OnSendMessage;

        public void RecieveMessage(object? sender, MessageEventArgs message)
        {
            if (message is FormOpenMessage openMessage && ChildFormOpening is EventHandler<FormOpenMessage> handler)
            { handler(sender, openMessage); }

        }

        void SendMessage(MessageEventArgs message)
        {
            if (OnSendMessage is EventHandler<MessageEventArgs> handler)
            { handler(this, message); }
        }
        #endregion

    }
}