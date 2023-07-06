using DataDictionary.BusinessLayer;
using DataDictionary.DataLayer.DbMetaData;
using DataDictionary.DataLayer.DomainData;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using System.ComponentModel;
using System.Windows.Forms;
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

        public Main() : base()
        {
            InitializeComponent();

            // Setup Images for Tree Control
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

            SetImages(dbMetaDataNavigation, dbDataImageItems.Values);
            SetImages(domainModelNavigation, domainModelImageItems.Values);

            dbSchemaToolStripMenuItem.DropDownItems.AddRange(dbSchemaContextMenu.Items);
            domainModelToolStripMenuItem.DropDownItems.AddRange(domainModelMenu.Items);
        }

        #region Form
        private void Main_FormClosing(object? sender, FormClosingEventArgs e)
        { }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        { Program.WorkerQueue.ProgressChanged -= WorkerQueue_ProgressChanged; }

        private void WorkerQueue_ProgressChanged(object? sender, WorkerProgressChangedEventArgs e)
        {
            toolStripProgressBar.Value = e.ProgressPercent;
            toolStripWorkerTask.Text = e.ProgressText;
        }
        #endregion

        #region Menu Events
        private void menuCatalogItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.FirstOrDefault(w => w.GetType() == typeof(Forms.DbCatalog)) is Forms.DbCatalog existingForm)
            { existingForm.Activate(); }
            else { new Forms.DbCatalog().Show(); }
        }

        private void menuSchemaItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.FirstOrDefault(w => w.GetType() == typeof(Forms.DbSchemaView)) is Forms.DbSchemaView existingForm)
            { existingForm.Activate(); }
            else { new Forms.DbSchemaView().Show(); }
        }

        private void menuTableItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.FirstOrDefault(w => w.GetType() == typeof(Forms.DbTableView)) is Forms.DbTableView existingForm)
            { existingForm.Activate(); }
            else { new Forms.DbTableView().Show(); }
        }

        private void menuColumnItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.FirstOrDefault(w => w.GetType() == typeof(Forms.DbColumnView)) is Forms.DbColumnView existingForm)
            { existingForm.Activate(); }
            else { new Forms.DbColumnView().Show(); }
        }

        private void menuPropertyItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.FirstOrDefault(w => w.GetType() == typeof(Forms.DbExtendedPropertyView)) is Forms.DbExtendedPropertyView existingForm)
            { existingForm.Activate(); }
            else { new Forms.DbExtendedPropertyView().Show(); }
        }

        private void menuImportDbSchema_Click(object sender, EventArgs e)
        {
            Program.DomainData.ImportAttributes(Program.DbData.DbColumns);
            BuildDomainModelTree();
        }

        private void menuAttributes_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.FirstOrDefault(w => w.GetType() == typeof(Forms.DomainAttributeView)) is Forms.DomainAttributeView existingForm)
            { existingForm.Activate(); }
            else { new Forms.DomainAttributeView().Show(); }
        }

        private void navigationDbSchemaTab_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            dbSchemaContextMenu.Show();
        }

        private void navigationDomainTab_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            domainModelMenu.Show();
        }

        #endregion

        #region dbMetaDataNavigation
        Dictionary<TreeNode, Object> dbDataNodes = new Dictionary<TreeNode, Object>();
        enum dbDataImageIndex
        {
            Database,
            Schema,
            Tables,
            Table,
            View,
            Columns,
            Column,
            ComputedColumn,
            KeyColumn
        }

        static Dictionary<dbDataImageIndex, (String imageKey, Image image)> dbDataImageItems = new Dictionary<dbDataImageIndex, (String imageKey, Image image)>()
        {
            {dbDataImageIndex.Database,         ("Database",        Resources.Database) },
            {dbDataImageIndex.Schema,           ("Schema",          Resources.Schema) },
            {dbDataImageIndex.Tables,           ("Tables",          Resources.TableGroup) },
            {dbDataImageIndex.Table,            ("Table",           Resources.Table) },
            {dbDataImageIndex.Columns,          ("Columns",         Resources.ColumnGroup) },
            {dbDataImageIndex.Column,           ("Column",          Resources.Column) },
            {dbDataImageIndex.ComputedColumn,   ("ComputedColumn",  Resources.ComputedColumn) },
            {dbDataImageIndex.KeyColumn,        ("KeyColumn",       Resources.KeyColumn) },
            {dbDataImageIndex.View,             ("View",            Resources.View) }
        };

        void SetImages(TreeView tree, IEnumerable<(String imageKey, Image image)> images)
        {
            if (tree.ImageList is null)
            { tree.ImageList = new ImageList(); }

            foreach ((string imageKey, Image image) image in images.Where(w => !tree.ImageList.Images.ContainsKey(w.imageKey)))
            { tree.ImageList.Images.Add(image.imageKey, image.image); }
        }

        void BuildDbDataTree()
        {
            dbMetaDataNavigation.Nodes.Clear();
            dbDataNodes.Clear();

            foreach (IDbCatalogItem catalogItem in Program.DbData.DbCatalogs.OrderBy(o => o.CatalogName))
            {
                if (String.IsNullOrWhiteSpace(catalogItem.CatalogName))
                {
                    //TODO: This event may fire when there is no data or the data is being changed. Cuased by the deleted row not being handled correctly.
                }

                TreeNode catalogNode = CreateNode(catalogItem.CatalogName, dbDataImageIndex.Database, catalogItem);
                dbMetaDataNavigation.Nodes.Add(catalogNode);

                foreach (IDbSchemaItem schemaItem in Program.DbData.DbSchemas.OrderBy(o => o.SchemaName).Where(
                    w => w.IsSystem == false &&
                    w.CatalogName == catalogItem.CatalogName))
                {
                    TreeNode schemaNode = CreateNode(schemaItem.SchemaName, dbDataImageIndex.Schema, schemaItem, catalogNode);
                    TreeNode tablesNode = CreateNode("Tables & Views", dbDataImageIndex.Tables, null, schemaNode);

                    foreach (IDbTableItem tableItem in Program.DbData.DbTables.OrderBy(o => o.TableName).Where(
                        w => w.IsSystem == false &&
                        w.CatalogName == schemaItem.CatalogName &&
                        w.SchemaName == schemaItem.SchemaName))
                    {
                        TreeNode tableNode;
                        if (tableItem.TableType == "VIEW")
                        { tableNode = CreateNode(tableItem.TableName, dbDataImageIndex.View, tableItem, tablesNode); }
                        else { tableNode = CreateNode(tableItem.TableName, dbDataImageIndex.Table, tableItem, tablesNode); }

                        TreeNode columnsNode = CreateNode("Columns", dbDataImageIndex.Columns, null, tableNode);

                        foreach (IDbColumnItem columnItem in Program.DbData.DbColumns.OrderBy(o => o.OrdinalPosition).Where(
                            w => w.CatalogName == tableItem.CatalogName &&
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

        private void dbMetaDataNavigation_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (dbDataNodes.ContainsKey(e.Node))
            {
                Object dataNode = dbDataNodes[e.Node];
                if (dataNode is IDbCatalogItem catalogItem) { }
                if (dataNode is IDbSchemaItem schemaItem) { new Forms.DbSchema(schemaItem).Show(); }
                if (dataNode is IDbTableItem tableItem) { new Forms.DbTable(tableItem).Show(); }
                if (dataNode is IDbColumnItem columnItem) { new Forms.DbColumn(columnItem).Show(); }
            }
        }
        #endregion

        #region domainModelNavigation
        Dictionary<TreeNode, Object> domainModelNodes = new Dictionary<TreeNode, Object>();
        enum domainModelImageIndex
        {
            Attribute,
            Attributes,
        }

        static Dictionary<domainModelImageIndex, (String imageKey, Image image)> domainModelImageItems = new Dictionary<domainModelImageIndex, (String imageKey, Image image)>()
        {
            {domainModelImageIndex.Attribute,    ("Attribute",   Resources.Attribute) },
            {domainModelImageIndex.Attributes,   ("Attributes",  Resources.Parameter) },
        };

        void BuildDomainModelTree()
        {
            domainModelNavigation.Nodes.Clear();
            domainModelNodes.Clear();

            foreach (IDomainAttributeItem attributeItem in
                Program.DomainData.DomainAttributes.
                Where(w => w.ParentAttributeId is null).
                OrderBy(o => o.AttributeTitle))
            {
                TreeNode attributeNode = CreateNode(attributeItem.AttributeTitle, domainModelImageIndex.Attribute, attributeItem);
                domainModelNavigation.Nodes.Add(attributeNode);

                //TODO: Need a recursive method to add children

            }

            TreeNode CreateNode(String? nodeText, domainModelImageIndex imageIndex, Object? source = null, TreeNode? parentNode = null)
            {
                if (String.IsNullOrWhiteSpace(nodeText)) { throw new ArgumentNullException(nameof(nodeText)); }

                TreeNode result = new TreeNode(nodeText);
                result.ImageKey = domainModelImageItems[imageIndex].imageKey;
                result.SelectedImageKey = domainModelImageItems[imageIndex].imageKey;

                if (parentNode is not null) { parentNode.Nodes.Add(result); }
                if (source is not null) { dbDataNodes.Add(result, source); }

                return result;
            }
        }

        private void domainModelNavigation_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (domainModelNodes.ContainsKey(e.Node))
            {
                Object domainNode = domainModelNodes[e.Node];
                if (domainNode is IDomainAttributeItem) { }
            }
        }


        #endregion
        #region IColleague
        public event EventHandler<MessageEventArgs>? OnSendMessage;

        public void ReceiveMessage(object? sender, MessageEventArgs message)
        { HandleMessage((dynamic)message); }

        void SendMessage(MessageEventArgs message)
        {
            if (OnSendMessage is EventHandler<MessageEventArgs> handler)
            { handler(this, message); }
        }

        void HandleMessage(MessageEventArgs message) { }
        void HandleMessage(FormAddMdiChild message)
        { if (message.ChildForm.MdiParent is null) { message.ChildForm.MdiParent = this; } }

        Form? lastActive;
        void HandleMessage(DbDataBatchStarting message)
        { lastActive = ActiveMdiChild; }

        void HandleMessage(DbDataBatchCompleted message)
        { BuildDbDataTree(); }

        #endregion


    }
}