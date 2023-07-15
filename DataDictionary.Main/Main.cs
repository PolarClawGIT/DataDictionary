using DataDictionary.BusinessLayer;
using DataDictionary.DataLayer.DbMetaData;
using DataDictionary.DataLayer.DomainData;
using DataDictionary.Main.Forms;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using System.ComponentModel;
using System.Windows.Forms;
using Toolbox.BindingTable;
using Toolbox.Mediator;
using Toolbox.Threading;

namespace DataDictionary.Main
{
    partial class Main : ApplicationFormBase
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

            // TODO: Cannot get the Context menus to show. For now, add them to the Tools menu
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
            Program.Data.ImportDbSchemaToDomain();
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

            foreach (IDbCatalogItem catalogItem in Program.Data.DbCatalogs.OrderBy(o => o.CatalogName))
            {
                if (String.IsNullOrWhiteSpace(catalogItem.CatalogName))
                {
                    //TODO: This event may fire when there is no data or the data is being changed. Cuased by the deleted row not being handled correctly.
                }

                TreeNode catalogNode = CreateNode(catalogItem.CatalogName, dbDataImageIndex.Database, catalogItem);
                dbMetaDataNavigation.Nodes.Add(catalogNode);

                foreach (IDbSchemaItem schemaItem in Program.Data.DbSchemta.OrderBy(o => o.SchemaName).Where(
                    w => w.IsSystem == false &&
                    w.CatalogName == catalogItem.CatalogName))
                {
                    TreeNode schemaNode = CreateNode(schemaItem.SchemaName, dbDataImageIndex.Schema, schemaItem, catalogNode);
                    TreeNode tablesNode = CreateNode("Tables & Views", dbDataImageIndex.Tables, null, schemaNode);

                    foreach (IDbTableItem tableItem in Program.Data.DbTables.OrderBy(o => o.TableName).Where(
                        w => w.IsSystem == false &&
                        w.CatalogName == schemaItem.CatalogName &&
                        w.SchemaName == schemaItem.SchemaName))
                    {
                        TreeNode tableNode;
                        if (tableItem.TableType == "VIEW")
                        { tableNode = CreateNode(tableItem.TableName, dbDataImageIndex.View, tableItem, tablesNode); }
                        else { tableNode = CreateNode(tableItem.TableName, dbDataImageIndex.Table, tableItem, tablesNode); }

                        TreeNode columnsNode = CreateNode("Columns", dbDataImageIndex.Columns, null, tableNode);

                        foreach (IDbColumnItem columnItem in Program.Data.DbColumns.OrderBy(o => o.OrdinalPosition).Where(
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
            Property,
            Alias
        }

        static Dictionary<domainModelImageIndex, (String imageKey, Image image)> domainModelImageItems = new Dictionary<domainModelImageIndex, (String imageKey, Image image)>()
        {
            {domainModelImageIndex.Attribute,    ("Attribute",   Resources.Attribute) },
            {domainModelImageIndex.Attributes,   ("Attributes",  Resources.Parameter) },
            {domainModelImageIndex.Property,     ("Property",    Resources.ExtendedProperty) },
            {domainModelImageIndex.Alias,        ("Alias",       Resources.Column) },
        };

        void BuildDomainModelTree()
        {
            domainModelNavigation.Nodes.Clear();
            domainModelNodes.Clear();

            foreach (IDomainAttributeItem attributeItem in
                Program.Data.DomainAttributes.
                Where(w => w.ParentAttributeId is null).
                OrderBy(o => o.AttributeTitle))
            {
                TreeNode attributeNode = CreateAttribute(attributeItem, null);
                domainModelNavigation.Nodes.Add(attributeNode);
            }

            TreeNode CreateAttribute(IDomainAttributeItem attributeItem, TreeNode? parent)
            {
                TreeNode attributeNode = CreateNode(attributeItem.AttributeTitle, domainModelImageIndex.Attribute, attributeItem);

                foreach (DomainAttributePropertyItem propertyItem in Program.Data.DomainAttributeProperties.Where(w => w.AttributeId == attributeItem.AttributeId))
                { CreateNode(propertyItem.PropertyName, domainModelImageIndex.Property, propertyItem, attributeNode); }

                foreach (DomainAttributeAliasItem aliasItem in Program.Data.DomainAttributeAliases.Where(w => w.AttributeId == attributeItem.AttributeId))
                { CreateNode(aliasItem.ToString(), domainModelImageIndex.Alias, aliasItem, attributeNode); }

                foreach (DomainAttributeItem childAttributeItem in Program.Data.DomainAttributes.Where(w => w.ParentAttributeId == attributeItem.AttributeId))
                { attributeNode.Nodes.Add(CreateAttribute(childAttributeItem, attributeNode)); }

                if (parent is not null) { parent.Nodes.Add(attributeNode); }
                return attributeNode;
            }

            TreeNode CreateNode(String? nodeText, domainModelImageIndex imageIndex, Object? source = null, TreeNode? parentNode = null)
            {
                if (String.IsNullOrWhiteSpace(nodeText)) { throw new ArgumentNullException(nameof(nodeText)); }

                TreeNode result = new TreeNode(nodeText);
                result.ImageKey = domainModelImageItems[imageIndex].imageKey;
                result.SelectedImageKey = domainModelImageItems[imageIndex].imageKey;

                if (parentNode is not null) { parentNode.Nodes.Add(result); }
                if (source is not null) { domainModelNodes.Add(result, source); }

                return result;
            }
        }

        private void domainModelNavigation_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (domainModelNodes.ContainsKey(e.Node))
            {
                Object domainNode = domainModelNodes[e.Node];
                if (domainNode is IDomainAttributeItem attributeItem) { new Forms.DomainAttribute(attributeItem).Show(); }
            }
        }

        #endregion

        #region IColleague

        protected override void HandleMessage(FormAddMdiChild message)
        {
            if (!ReferenceEquals(this, message.ChildForm) && message.ChildForm.MdiParent is null)
            { message.ChildForm.MdiParent = this; }
        }

        Form? lastActive;
        protected override void HandleMessage(DbDataBatchStarting message)
        { lastActive = ActiveMdiChild; }

        protected override void HandleMessage(DbDataBatchCompleted message)
        {
            Program.Data.ImportDbSchemaToDomain();
            BuildDbDataTree();
            BuildDomainModelTree();
        }

        #endregion

        //TODO: apparently when the ToolStrip contains the Cut/Copy/Paste options, the keystroke is not sent to the control that has focus.
        // I think I will have to have each form/control handle the cut/copy/paste. Put in the Base Class?

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        { SendMessage(new WindowsCutCommand() { HandledBy = this.ActiveMdiChild }); }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        { SendMessage(new WindowsCopyCommand() { HandledBy = this.ActiveMdiChild }); }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        { SendMessage(new WindowsPasteCommand() { HandledBy = this.ActiveMdiChild }); }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        { SendMessage(new WindowsUndoCommand() { HandledBy = this.ActiveMdiChild }); }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        { SendMessage(new WindowsSelectAllCommand() { HandledBy = this.ActiveMdiChild }); }
    }
}