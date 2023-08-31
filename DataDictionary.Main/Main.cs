using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.WorkFlows;
using DataDictionary.DataLayer.ApplicationData;
using DataDictionary.DataLayer.DbMetaData;
using DataDictionary.DataLayer.DomainData;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Windows.Forms;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main
{
    partial class Main : ApplicationBase
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
            toolStrip.Hide(); // Hide base ToolStrip
            menuStrip.Enabled = false;
            navigationTabs.Enabled = false;

            // Setup Images for Tree Control
            SetImages(dbMetaDataNavigation, dbDataImageItems.Values);
            SetImages(domainModelNavigation, domainModelImageItems.Values);

            // Setup Tabs
            navigationTabs.ImageList = new ImageList();
            foreach (navigationTabImageIndex item in Enum.GetValues(typeof(navigationTabImageIndex)))
            { navigationTabs.ImageList.Images.Add(item.ToString(), navigationTabImages[item]); }
            navigationDbSchemaTab.ImageKey = navigationTabImageIndex.Database.ToString();
            navigationDomainTab.ImageKey = navigationTabImageIndex.Domain.ToString();

            //Hook the WorkerQueue up to this forms UI thread for events.
            Program.Worker.InvokeUsing = this.Invoke;
        }

        #region Form
        private void Main_Load(object sender, EventArgs e)
        {
            Program.Worker.ProgressChanged += WorkerQueue_ProgressChanged;

            // TODO: Cannot get the Context menus to show. For now, add them to the Tools menu
            dbSchemaToolStripMenuItem.DropDownItems.AddRange(dbSchemaContextMenu.Items);
            domainModelToolStripMenuItem.DropDownItems.AddRange(domainModelMenu.Items);

            if (Settings.Default.IsOnLineMode)
            { this.DoWork(Program.Data.LoadApplicationData(), OnComplete); }
            else { this.DoWork(Program.Data.LoadApplicationData(new FileInfo(Settings.Default.AppDataFile)), OnComplete); }

            void OnComplete(RunWorkerCompletedEventArgs args)
            {
                if (args.Error is not null && Settings.Default.IsOnLineMode)
                { // Could not load the data from the database for whatever reason.
                  // Switch to off-line mode and load from file if possible.
                    Settings.Default.IsOnLineMode = false;
                    Settings.Default.Save();
                    this.DoWork(Program.Data.LoadApplicationData(new FileInfo(Settings.Default.AppDataFile)), OnComplete);
                }
                else if (args.Error is null) { BindData(); }
            }
        }

        void BindData()
        {
            modelNameData.DataBindings.Add(new Binding(nameof(modelNameData.Text), Program.Data.Model, nameof(Program.Data.Model.ModelTitle)));
            modelDescriptionData.DataBindings.Add(new Binding(nameof(modelDescriptionData.Text), Program.Data.Model, nameof(Program.Data.Model.ModelDescription)));

            BuildDbDataTree();
            BuildDomainModelTree();

            menuStrip.Enabled = true;
            navigationTabs.Enabled = true;
        }

        void UnBindData()
        {
            modelNameData.DataBindings.Clear();
            modelDescriptionData.DataBindings.Clear();
        }

        private void Main_FormClosing(object? sender, FormClosingEventArgs e)
        { }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        { Program.Worker.ProgressChanged -= WorkerQueue_ProgressChanged; }

        private void WorkerQueue_ProgressChanged(object? sender, WorkerProgressChangedEventArgs e)
        {
            toolStripProgressBar.Value = e.ProgressPercent;
            toolStripWorkerTask.Text = e.ProgressText;
        }
        #endregion

        #region Menu Events
        private void menuCatalogItem_Click(object sender, EventArgs e)
        { Activate(() => new Forms.DbCatalog()); }

        private void menuSchemaItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.DbSchema), Program.Data.DbSchemta); }

        private void menuTableItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.DbTable), Program.Data.DbTables); }

        private void menuColumnItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.DbColumn), Program.Data.DbColumns); }

        private void menuPropertyItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.DbExtendedProperty), Program.Data.DbExtendedProperties); }

        [Obsolete()]
        private void menuImportDbSchema_Click(object sender, EventArgs e)
        {
            //Program.Data.ImportDbSchemaToDomain();
            //BuildDomainModelTree();
        }

        private void menuAttributes_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.DomainAttribute), Program.Data.DomainAttributes); }


        private void HelpContentsMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild is Form currentForm)
            { Activate(() => new Dialogs.HelpSubject(currentForm)); }
        }

        private void HelpIndexMenuItem_Click(object sender, EventArgs e)
        { Activate(() => new Dialogs.HelpSubject()); }

        private void HelpAboutMenuItem_Click(object sender, EventArgs e)
        { Activate(() => new Dialogs.HelpSubject("About Application")); }

        private void Main_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            if (ActiveMdiChild is Form currentForm)
            { Activate(() => new Dialogs.HelpSubject(currentForm)); }
        }

        private void openSaveModelDatabaseMenuItem_Click(object sender, EventArgs e)
        { Activate(() => new Dialogs.OpenSaveModelDatabase()); }

        private void menuAttributeProperties_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.DomainProperty), Program.Data.DomainAttributeProperties); }

        private void menuAttributeAlaises_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.DomainAlias), Program.Data.DomainAttributeAliases); }

        private void menuConstraintItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.DbKey), Program.Data.DbConstraints); }

        private void menuConstraintColumnItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.DbKeyColumn), Program.Data.DbConstraintColumns); }

        private void menuDataTypeItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.DbDomainType), Program.Data.DbDomains); }

        private void menuRoutineItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.DbProcedure), Program.Data.DbRoutines); }

        private void menuRoutineParameterItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.DbParameter), Program.Data.DbRoutineParameters); }

        private void menuRoutineDependencyItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.DbDependancy), Program.Data.DbRoutineDependencies); }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate(() => new Dialogs.ApplicationProperty()); }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnBindData();
            Program.Data.Clear();
            Program.Data.NewModel();
            BindData();
        }

        private void cloneModelMenuItem_Click(object sender, EventArgs e)
        {
            UnBindData();
            Program.Data.NewModel();
            BindData();
        }

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

        private void navigationDbSchemaTab_MouseDoubleClick(object sender, MouseEventArgs e)
        { //TODO: Not Working. Does not show when the context menu is assigned to the control or rigged to an event.
            dbSchemaContextMenu.Show();
        }

        private void navigationDomainTab_MouseDoubleClick(object sender, MouseEventArgs e)
        { //TODO: Not Working. Does not show when the context menu is assigned to the control or rigged to an event.
            domainModelMenu.Show();
        }

        private void extendedPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate(() => new Dialogs.ViewTextTemplate()); }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate(() => new Dialogs.ApplicationOptions()); }

        private void definitionsToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate(() => new Dialogs.ApplicationDefinition()); }
        #endregion

        #region dbMetaDataNavigation
        Dictionary<TreeNode, Object> dbDataNodes = new Dictionary<TreeNode, Object>();
        enum dbDataImageIndex
        {
            Database,
            Schema,
            Tables,
            Table,
            TableKey,
            View,
            Columns,
            Column,
            ComputedColumn,
            Constraint,
            ConstraintColumn
        }

        static Dictionary<dbDataImageIndex, (String imageKey, Image image)> dbDataImageItems = new Dictionary<dbDataImageIndex, (String imageKey, Image image)>()
        {
            {dbDataImageIndex.Database,         ("Database",         Resources.Database) },
            {dbDataImageIndex.Schema,           ("Schema",           Resources.Schema) },
            {dbDataImageIndex.Tables,           ("Tables",           Resources.TableGroup) },
            {dbDataImageIndex.Table,            ("Table",            Resources.Table) },
            {dbDataImageIndex.TableKey,         ("TableKey",         Resources.TableKey) },
            {dbDataImageIndex.Columns,          ("Columns",          Resources.ColumnGroup) },
            {dbDataImageIndex.Column,           ("Column",           Resources.Column) },
            {dbDataImageIndex.ComputedColumn,   ("ComputedColumn",   Resources.ComputedColumn) },
            {dbDataImageIndex.Constraint,       ("Constraint",       Resources.Key) },
            {dbDataImageIndex.ConstraintColumn, ("ConstraintColumn", Resources.KeyColumn) },
            {dbDataImageIndex.View,             ("View",             Resources.View) }
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
                        w => w.IsSystem == false && new DbSchemaKey(w).Equals(new DbSchemaKey(schemaItem))))
                    {
                        TreeNode tableNode;
                        TreeNode? tableConstraintNode = null;
                        if (tableItem.TableType == "VIEW")
                        { tableNode = CreateNode(tableItem.TableName, dbDataImageIndex.View, tableItem, tablesNode); }
                        else { tableNode = CreateNode(tableItem.TableName, dbDataImageIndex.Table, tableItem, tablesNode); }

                        TreeNode columnsNode = CreateNode("Columns", dbDataImageIndex.Columns, null, tableNode);

                        foreach (IDbTableColumnItem columnItem in Program.Data.DbColumns.OrderBy(o => o.OrdinalPosition).Where(
                            w => new DbTableKey(w).Equals(new DbTableKey(tableItem))))
                        { TreeNode columnNode = CreateNode(columnItem.ColumnName, dbDataImageIndex.Column, columnItem, columnsNode); }

                        foreach (DbConstraintItem contraintItem in Program.Data.DbConstraints.Where(
                            w => new DbTableKey(w).Equals(new DbTableKey(tableItem))))
                        {
                            if (tableConstraintNode is null)
                            { tableConstraintNode = CreateNode("Constraints", dbDataImageIndex.TableKey, null, tableNode); }

                            TreeNode constraintNode = CreateNode(contraintItem.ConstraintName, dbDataImageIndex.Constraint, contraintItem, tableConstraintNode);

                            foreach (DbConstraintColumnItem contraintColumnItem in Program.Data.DbConstraintColumns.Where(
                                w => new DbConstraintKey(w).Equals(new DbConstraintKey(contraintItem))))
                            { TreeNode constraintColumnNode = CreateNode(contraintColumnItem.ColumnName, dbDataImageIndex.ConstraintColumn, contraintColumnItem, constraintNode); }
                        }
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

                if (dataNode is IDbSchemaItem schemaItem)
                { Activate((data) => new Forms.DbSchema(schemaItem), schemaItem); }

                if (dataNode is IDbTableItem tableItem)
                { Activate((data) => new Forms.DbTable(tableItem), tableItem); }

                if (dataNode is IDbTableColumnItem columnItem)
                { Activate((data) => new Forms.DbTableColumn(columnItem), columnItem); }

                if (dataNode is IDbConstraintItem constraintItem)
                { Activate((data) => new Forms.DbConstraint(constraintItem), constraintItem); }

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
            {domainModelImageIndex.Property,     ("Property",    Resources.Property) },
            {domainModelImageIndex.Alias,        ("Alias",       Resources.Synonym) },
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
                {
                    String propertyTitle = String.Empty;
                    if (Program.Data.Properties.FirstOrDefault(w => w.PropertyId == propertyItem.PropertyId) is PropertyItem property && property.PropertyTitle is not null)
                    { propertyTitle = property.PropertyTitle; }

                    CreateNode(propertyTitle, domainModelImageIndex.Property, propertyItem, attributeNode);
                }

                foreach (DomainAttributeAliasItem aliasItem in Program.Data.DomainAttributeAliases.Where(w => w.AttributeId == attributeItem.AttributeId))
                { CreateNode(aliasItem.ToString(), domainModelImageIndex.Alias, aliasItem, attributeNode); }

                foreach (DomainAttributeItem childAttributeItem in Program.Data.DomainAttributes.Where(w => w.AttributeId == attributeItem.ParentAttributeId))
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
                Object dataNode = domainModelNodes[e.Node];

                if (dataNode is IDomainAttributeItem item)
                { Activate((data) => new Forms.DomainAttribute(item), item); }
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
        {
            this.Controls[0].Focus();
            lastActive = ActiveMdiChild;
            UnBindData();
        }

        protected override void HandleMessage(DbDataBatchCompleted message)
        { BindData(); }
        #endregion

        private void gridViewToolStripMenuItem_Click(object sender, EventArgs e)
        { new Forms.UnitTestGridView().Show(); }


    }
}