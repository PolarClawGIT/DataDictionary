namespace DataDictionary.Main
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Panel navigationPanel;
            Splitter navigationSpliter;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            contextNameLayout = new TableLayoutPanel();
            dataSourceToolStrip = new ToolStrip();
            newAttributeCommand = new ToolStripSplitButton();
            attributeContextMenu = new ContextMenuStrip(components);
            menuAttributes = new ToolStripMenuItem();
            menuAttributeProperties = new ToolStripMenuItem();
            menuAttributeAlaises = new ToolStripMenuItem();
            newEntityCommand = new ToolStripSplitButton();
            entityContextMenu = new ContextMenuStrip(components);
            entitiesToolStripMenuItem = new ToolStripMenuItem();
            entityPropertiesToolStripMenuItem = new ToolStripMenuItem();
            entityAliasToolStripMenuItem = new ToolStripMenuItem();
            newSubjectAreaCommand = new ToolStripSplitButton();
            subjectAreaContextMenu = new ContextMenuStrip(components);
            subjectAreaToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator6 = new ToolStripSeparator();
            manageDatabasesCommand = new ToolStripSplitButton();
            catalogContextMenu = new ContextMenuStrip(components);
            menuCatalogItem = new ToolStripMenuItem();
            menuSchemaItem = new ToolStripMenuItem();
            menuTableItem = new ToolStripMenuItem();
            menuTableColumnItem = new ToolStripMenuItem();
            menuPropertyItem = new ToolStripMenuItem();
            menuConstraintItem = new ToolStripMenuItem();
            menuConstraintColumnItem = new ToolStripMenuItem();
            menuDataTypeItem = new ToolStripMenuItem();
            menuRoutineItem = new ToolStripMenuItem();
            menuRoutineParameterItem = new ToolStripMenuItem();
            menuRoutineDependencyItem = new ToolStripMenuItem();
            manageLibrariesCommand = new ToolStripSplitButton();
            libraryContextMenu = new ContextMenuStrip(components);
            viewLibrarySourceCommand = new ToolStripMenuItem();
            viewLibraryMemberCommand = new ToolStripMenuItem();
            toolStripSeparator7 = new ToolStripSeparator();
            refreshCommand = new ToolStripButton();
            contextNameNavigation = new TreeView();
            statusStrip = new StatusStrip();
            toolStripOnlineStatus = new ToolStripStatusLabel();
            toolStripWhiteSpace = new ToolStripStatusLabel();
            toolStripWorkerTask = new ToolStripStatusLabel();
            toolStripProgressBar = new ToolStripProgressBar();
            menuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newEmptyModelMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator = new ToolStripSeparator();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            printToolStripMenuItem = new ToolStripMenuItem();
            printPreviewToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            undoToolStripMenuItem = new ToolStripMenuItem();
            redoToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            cutToolStripMenuItem = new ToolStripMenuItem();
            copyToolStripMenuItem = new ToolStripMenuItem();
            pasteToolStripMenuItem = new ToolStripMenuItem();
            peekAtClipboardToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            selectAllToolStripMenuItem = new ToolStripMenuItem();
            scriptMenuItem = new ToolStripMenuItem();
            extendedPropertiesToolStripMenuItem = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            customizeToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            applicationToolStripMenuItem = new ToolStripMenuItem();
            browsePropertiesCommand = new ToolStripMenuItem();
            browseHelpCommand = new ToolStripMenuItem();
            unitTestingToolStripMenuItem = new ToolStripMenuItem();
            gridViewToolStripMenuItem = new ToolStripMenuItem();
            testFormToolStripMenuItem = new ToolStripMenuItem();
            textEditorToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            helpContentsMenuItem = new ToolStripMenuItem();
            helpIndexMenuItem = new ToolStripMenuItem();
            searchToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            helpAboutMenuItem = new ToolStripMenuItem();
            openFileDialog = new OpenFileDialog();
            saveFileDialog = new SaveFileDialog();
            manageModelCommand = new ToolStripSplitButton();
            navigationPanel = new Panel();
            navigationSpliter = new Splitter();
            navigationPanel.SuspendLayout();
            contextNameLayout.SuspendLayout();
            dataSourceToolStrip.SuspendLayout();
            attributeContextMenu.SuspendLayout();
            entityContextMenu.SuspendLayout();
            subjectAreaContextMenu.SuspendLayout();
            catalogContextMenu.SuspendLayout();
            libraryContextMenu.SuspendLayout();
            statusStrip.SuspendLayout();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // navigationPanel
            // 
            navigationPanel.Controls.Add(contextNameLayout);
            navigationPanel.Dock = DockStyle.Left;
            navigationPanel.Location = new Point(0, 49);
            navigationPanel.Name = "navigationPanel";
            navigationPanel.Size = new Size(300, 591);
            navigationPanel.TabIndex = 6;
            // 
            // nameSpaceLayout
            // 
            contextNameLayout.ColumnCount = 1;
            contextNameLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            contextNameLayout.Controls.Add(dataSourceToolStrip, 0, 0);
            contextNameLayout.Controls.Add(contextNameNavigation, 0, 1);
            contextNameLayout.Dock = DockStyle.Fill;
            contextNameLayout.Location = new Point(0, 0);
            contextNameLayout.Name = "nameSpaceLayout";
            contextNameLayout.RowCount = 2;
            contextNameLayout.RowStyles.Add(new RowStyle());
            contextNameLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            contextNameLayout.Size = new Size(300, 591);
            contextNameLayout.TabIndex = 2;
            // 
            // dataSourceToolStrip
            // 
            dataSourceToolStrip.Items.AddRange(new ToolStripItem[] { manageModelCommand, newAttributeCommand, newEntityCommand, newSubjectAreaCommand, toolStripSeparator6, manageDatabasesCommand, manageLibrariesCommand, toolStripSeparator7, refreshCommand });
            dataSourceToolStrip.Location = new Point(0, 0);
            dataSourceToolStrip.Name = "dataSourceToolStrip";
            dataSourceToolStrip.Size = new Size(300, 25);
            dataSourceToolStrip.TabIndex = 0;
            dataSourceToolStrip.Text = "toolStrip1";
            // 
            // newAttributeCommand
            // 
            newAttributeCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            newAttributeCommand.DropDown = attributeContextMenu;
            newAttributeCommand.Image = Properties.Resources.NewAttribute;
            newAttributeCommand.ImageTransparentColor = Color.Magenta;
            newAttributeCommand.Name = "newAttributeCommand";
            newAttributeCommand.Size = new Size(32, 22);
            newAttributeCommand.Text = "newAttributeCommand";
            newAttributeCommand.ToolTipText = "new Attribute";
            newAttributeCommand.ButtonClick += NewAttributeCommand_ButtonClick;
            // 
            // attributeContextMenu
            // 
            attributeContextMenu.Items.AddRange(new ToolStripItem[] { menuAttributes, menuAttributeProperties, menuAttributeAlaises });
            attributeContextMenu.Name = "attributeContextMenu";
            attributeContextMenu.OwnerItem = newAttributeCommand;
            attributeContextMenu.Size = new Size(219, 70);
            // 
            // menuAttributes
            // 
            menuAttributes.Image = Properties.Resources.Attribute;
            menuAttributes.Name = "menuAttributes";
            menuAttributes.Size = new Size(218, 22);
            menuAttributes.Text = "browse &Attributes";
            menuAttributes.Click += menuAttributes_Click;
            // 
            // menuAttributeProperties
            // 
            menuAttributeProperties.Image = Properties.Resources.Property;
            menuAttributeProperties.Name = "menuAttributeProperties";
            menuAttributeProperties.Size = new Size(218, 22);
            menuAttributeProperties.Text = "browse Attribute &Properties";
            menuAttributeProperties.Click += menuAttributeProperties_Click;
            // 
            // menuAttributeAlaises
            // 
            menuAttributeAlaises.Image = Properties.Resources.Synonym;
            menuAttributeAlaises.Name = "menuAttributeAlaises";
            menuAttributeAlaises.Size = new Size(218, 22);
            menuAttributeAlaises.Text = "browse Attribute A&laises";
            menuAttributeAlaises.Click += menuAttributeAlaises_Click;
            // 
            // newEntityCommand
            // 
            newEntityCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            newEntityCommand.DropDown = entityContextMenu;
            newEntityCommand.Image = Properties.Resources.NewEntity;
            newEntityCommand.ImageTransparentColor = Color.Magenta;
            newEntityCommand.Name = "newEntityCommand";
            newEntityCommand.Size = new Size(32, 22);
            newEntityCommand.Text = "new Entity";
            newEntityCommand.ButtonClick += NewEntityCommand_ButtonClick;
            // 
            // entityContextMenu
            // 
            entityContextMenu.Items.AddRange(new ToolStripItem[] { entitiesToolStripMenuItem, entityPropertiesToolStripMenuItem, entityAliasToolStripMenuItem });
            entityContextMenu.Name = "entityContextMenu";
            entityContextMenu.OwnerItem = newEntityCommand;
            entityContextMenu.Size = new Size(202, 70);
            // 
            // entitiesToolStripMenuItem
            // 
            entitiesToolStripMenuItem.Image = Properties.Resources.Entity;
            entitiesToolStripMenuItem.Name = "entitiesToolStripMenuItem";
            entitiesToolStripMenuItem.Size = new Size(201, 22);
            entitiesToolStripMenuItem.Text = "browse &Entities";
            entitiesToolStripMenuItem.Click += entitiesToolStripMenuItem_Click;
            // 
            // entityPropertiesToolStripMenuItem
            // 
            entityPropertiesToolStripMenuItem.Image = Properties.Resources.Property;
            entityPropertiesToolStripMenuItem.Name = "entityPropertiesToolStripMenuItem";
            entityPropertiesToolStripMenuItem.Size = new Size(201, 22);
            entityPropertiesToolStripMenuItem.Text = "browse Entity &Properties";
            entityPropertiesToolStripMenuItem.Click += entityPropertiesToolStripMenuItem_Click;
            // 
            // entityAliasToolStripMenuItem
            // 
            entityAliasToolStripMenuItem.Image = Properties.Resources.Synonym;
            entityAliasToolStripMenuItem.Name = "entityAliasToolStripMenuItem";
            entityAliasToolStripMenuItem.Size = new Size(201, 22);
            entityAliasToolStripMenuItem.Text = "browse Entity A&lias";
            entityAliasToolStripMenuItem.Click += entityAliasToolStripMenuItem_Click;
            // 
            // newSubjectAreaCommand
            // 
            newSubjectAreaCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            newSubjectAreaCommand.DropDown = subjectAreaContextMenu;
            newSubjectAreaCommand.Image = Properties.Resources.NewDiagram;
            newSubjectAreaCommand.ImageTransparentColor = Color.Magenta;
            newSubjectAreaCommand.Name = "newSubjectAreaCommand";
            newSubjectAreaCommand.Size = new Size(32, 22);
            newSubjectAreaCommand.Text = "new Subject Area";
            newSubjectAreaCommand.ButtonClick += NewSubjectAreaCommand_ButtonClick;
            // 
            // subjectAreaContextMenu
            // 
            subjectAreaContextMenu.Items.AddRange(new ToolStripItem[] { subjectAreaToolStripMenuItem });
            subjectAreaContextMenu.Name = "subjectAreaContextMenu";
            subjectAreaContextMenu.Size = new Size(187, 26);
            // 
            // subjectAreaToolStripMenuItem
            // 
            subjectAreaToolStripMenuItem.Image = Properties.Resources.Diagram;
            subjectAreaToolStripMenuItem.Name = "subjectAreaToolStripMenuItem";
            subjectAreaToolStripMenuItem.Size = new Size(186, 22);
            subjectAreaToolStripMenuItem.Text = "browse &Subject Areas";
            subjectAreaToolStripMenuItem.Click += subjectAreaToolStripMenuItem_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(6, 25);
            // 
            // manageDatabasesCommand
            // 
            manageDatabasesCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            manageDatabasesCommand.DropDown = catalogContextMenu;
            manageDatabasesCommand.Image = Properties.Resources.Database;
            manageDatabasesCommand.ImageTransparentColor = Color.Magenta;
            manageDatabasesCommand.Name = "manageDatabasesCommand";
            manageDatabasesCommand.Size = new Size(32, 22);
            manageDatabasesCommand.Text = "Manage Databases";
            manageDatabasesCommand.ButtonClick += manageDatabasesCommand_ButtonClick;
            // 
            // catalogContextMenu
            // 
            catalogContextMenu.Items.AddRange(new ToolStripItem[] { menuCatalogItem, menuSchemaItem, menuTableItem, menuTableColumnItem, menuPropertyItem, menuConstraintItem, menuConstraintColumnItem, menuDataTypeItem, menuRoutineItem, menuRoutineParameterItem, menuRoutineDependencyItem });
            catalogContextMenu.Name = "dbSchemacontextMenu";
            catalogContextMenu.OwnerItem = manageDatabasesCommand;
            catalogContextMenu.Size = new Size(234, 246);
            // 
            // menuCatalogItem
            // 
            menuCatalogItem.Image = Properties.Resources.Database;
            menuCatalogItem.Name = "menuCatalogItem";
            menuCatalogItem.Size = new Size(233, 22);
            menuCatalogItem.Text = "browse Catalogs";
            menuCatalogItem.Click += menuCatalogItem_Click;
            // 
            // menuSchemaItem
            // 
            menuSchemaItem.Image = Properties.Resources.Schema;
            menuSchemaItem.Name = "menuSchemaItem";
            menuSchemaItem.Size = new Size(233, 22);
            menuSchemaItem.Text = "browse Schemas";
            menuSchemaItem.Click += menuSchemaItem_Click;
            // 
            // menuTableItem
            // 
            menuTableItem.Image = Properties.Resources.Table;
            menuTableItem.Name = "menuTableItem";
            menuTableItem.Size = new Size(233, 22);
            menuTableItem.Text = "browse Tables";
            menuTableItem.Click += menuTableItem_Click;
            // 
            // menuTableColumnItem
            // 
            menuTableColumnItem.Image = Properties.Resources.Column;
            menuTableColumnItem.Name = "menuTableColumnItem";
            menuTableColumnItem.Size = new Size(233, 22);
            menuTableColumnItem.Text = "browse Table Columns";
            menuTableColumnItem.Click += menuColumnItem_Click;
            // 
            // menuPropertyItem
            // 
            menuPropertyItem.Image = Properties.Resources.ExtendedProperty;
            menuPropertyItem.Name = "menuPropertyItem";
            menuPropertyItem.Size = new Size(233, 22);
            menuPropertyItem.Text = "browse Properties";
            menuPropertyItem.Click += menuPropertyItem_Click;
            // 
            // menuConstraintItem
            // 
            menuConstraintItem.Image = Properties.Resources.Key;
            menuConstraintItem.Name = "menuConstraintItem";
            menuConstraintItem.Size = new Size(233, 22);
            menuConstraintItem.Text = "browse Constraints";
            menuConstraintItem.Click += menuConstraintItem_Click;
            // 
            // menuConstraintColumnItem
            // 
            menuConstraintColumnItem.Image = Properties.Resources.KeyColumn;
            menuConstraintColumnItem.Name = "menuConstraintColumnItem";
            menuConstraintColumnItem.Size = new Size(233, 22);
            menuConstraintColumnItem.Text = "browse Constraint Columns";
            menuConstraintColumnItem.Click += menuConstraintColumnItem_Click;
            // 
            // menuDataTypeItem
            // 
            menuDataTypeItem.Image = Properties.Resources.DomainType;
            menuDataTypeItem.Name = "menuDataTypeItem";
            menuDataTypeItem.Size = new Size(233, 22);
            menuDataTypeItem.Text = "browse Data Types";
            menuDataTypeItem.Click += menuDataTypeItem_Click;
            // 
            // menuRoutineItem
            // 
            menuRoutineItem.Image = Properties.Resources.Procedure;
            menuRoutineItem.Name = "menuRoutineItem";
            menuRoutineItem.Size = new Size(233, 22);
            menuRoutineItem.Text = "browse Routines";
            menuRoutineItem.Click += menuRoutineItem_Click;
            // 
            // menuRoutineParameterItem
            // 
            menuRoutineParameterItem.Image = Properties.Resources.Parameter;
            menuRoutineParameterItem.Name = "menuRoutineParameterItem";
            menuRoutineParameterItem.Size = new Size(233, 22);
            menuRoutineParameterItem.Text = "browse Routine Parameters";
            menuRoutineParameterItem.Click += menuRoutineParameterItem_Click;
            // 
            // menuRoutineDependencyItem
            // 
            menuRoutineDependencyItem.Image = Properties.Resources.Dependancy;
            menuRoutineDependencyItem.Name = "menuRoutineDependencyItem";
            menuRoutineDependencyItem.Size = new Size(233, 22);
            menuRoutineDependencyItem.Text = "browse Routine Dependencies";
            menuRoutineDependencyItem.Click += menuRoutineDependencyItem_Click;
            // 
            // manageLibrariesCommand
            // 
            manageLibrariesCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            manageLibrariesCommand.DropDown = libraryContextMenu;
            manageLibrariesCommand.Image = Properties.Resources.Library;
            manageLibrariesCommand.ImageTransparentColor = Color.Magenta;
            manageLibrariesCommand.Name = "manageLibrariesCommand";
            manageLibrariesCommand.Size = new Size(32, 22);
            manageLibrariesCommand.Text = "Manage Libraries";
            manageLibrariesCommand.ButtonClick += manageLibrariesCommand_ButtonClick;
            // 
            // libraryContextMenu
            // 
            libraryContextMenu.Items.AddRange(new ToolStripItem[] { viewLibrarySourceCommand, viewLibraryMemberCommand });
            libraryContextMenu.Name = "libraryContextMenu";
            libraryContextMenu.OwnerItem = manageLibrariesCommand;
            libraryContextMenu.Size = new Size(205, 48);
            // 
            // viewLibrarySourceCommand
            // 
            viewLibrarySourceCommand.Image = Properties.Resources.Library;
            viewLibrarySourceCommand.Name = "viewLibrarySourceCommand";
            viewLibrarySourceCommand.Size = new Size(204, 22);
            viewLibrarySourceCommand.Text = "browse Library Sources";
            viewLibrarySourceCommand.Click += viewLibrarySourceCommand_Click;
            // 
            // viewLibraryMemberCommand
            // 
            viewLibraryMemberCommand.Image = Properties.Resources.Class;
            viewLibraryMemberCommand.Name = "viewLibraryMemberCommand";
            viewLibraryMemberCommand.Size = new Size(204, 22);
            viewLibraryMemberCommand.Text = "browse Library Members";
            viewLibraryMemberCommand.Click += viewLibraryMemberCommand_Click;
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new Size(6, 25);
            // 
            // refreshCommand
            // 
            refreshCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            refreshCommand.Image = Properties.Resources.Refresh;
            refreshCommand.ImageTransparentColor = Color.Magenta;
            refreshCommand.Name = "refreshCommand";
            refreshCommand.Size = new Size(23, 22);
            refreshCommand.Text = "Refresh";
            refreshCommand.Click += RefreshCommand_Click;
            // 
            // nameSpaceNavigation
            // 
            contextNameNavigation.Dock = DockStyle.Fill;
            contextNameNavigation.HideSelection = false;
            contextNameNavigation.Location = new Point(3, 28);
            contextNameNavigation.Name = "nameSpaceNavigation";
            contextNameNavigation.Size = new Size(294, 560);
            contextNameNavigation.TabIndex = 0;
            contextNameNavigation.NodeMouseDoubleClick += DataSourceNavigation_NodeMouseDoubleClick;
            // 
            // navigationSpliter
            // 
            navigationSpliter.Location = new Point(300, 49);
            navigationSpliter.Name = "navigationSpliter";
            navigationSpliter.Size = new Size(3, 591);
            navigationSpliter.TabIndex = 8;
            navigationSpliter.TabStop = false;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripOnlineStatus, toolStripWhiteSpace, toolStripWorkerTask, toolStripProgressBar });
            statusStrip.Location = new Point(0, 640);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(917, 22);
            statusStrip.TabIndex = 0;
            statusStrip.Text = "statusStrip1";
            // 
            // toolStripOnlineStatus
            // 
            toolStripOnlineStatus.Name = "toolStripOnlineStatus";
            toolStripOnlineStatus.Size = new Size(59, 17);
            toolStripOnlineStatus.Text = "(pending)";
            toolStripOnlineStatus.ToolTipText = "Indicate the connection to the Application Database";
            // 
            // toolStripWhiteSpace
            // 
            toolStripWhiteSpace.Name = "toolStripWhiteSpace";
            toolStripWhiteSpace.Size = new Size(671, 17);
            toolStripWhiteSpace.Spring = true;
            // 
            // toolStripWorkerTask
            // 
            toolStripWorkerTask.Name = "toolStripWorkerTask";
            toolStripWorkerTask.Size = new Size(70, 17);
            toolStripWorkerTask.Text = "Worker Task";
            // 
            // toolStripProgressBar
            // 
            toolStripProgressBar.Name = "toolStripProgressBar";
            toolStripProgressBar.Size = new Size(100, 16);
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, scriptMenuItem, toolsToolStripMenuItem, helpToolStripMenuItem });
            menuStrip.Location = new Point(0, 25);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(917, 24);
            menuStrip.TabIndex = 1;
            menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newEmptyModelMenuItem, openToolStripMenuItem, toolStripSeparator, saveToolStripMenuItem, saveAsToolStripMenuItem, toolStripSeparator1, printToolStripMenuItem, printPreviewToolStripMenuItem, toolStripSeparator2, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // newEmptyModelMenuItem
            // 
            newEmptyModelMenuItem.Image = (Image)resources.GetObject("newEmptyModelMenuItem.Image");
            newEmptyModelMenuItem.ImageTransparentColor = Color.Magenta;
            newEmptyModelMenuItem.Name = "newEmptyModelMenuItem";
            newEmptyModelMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            newEmptyModelMenuItem.Size = new Size(178, 22);
            newEmptyModelMenuItem.Text = "&New Model";
            newEmptyModelMenuItem.ToolTipText = "Creates a New Empty Model";
            newEmptyModelMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Image = (Image)resources.GetObject("openToolStripMenuItem.Image");
            openToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            openToolStripMenuItem.Size = new Size(178, 22);
            openToolStripMenuItem.Text = "&Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new Size(175, 6);
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Enabled = false;
            saveToolStripMenuItem.Image = (Image)resources.GetObject("saveToolStripMenuItem.Image");
            saveToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveToolStripMenuItem.Size = new Size(178, 22);
            saveToolStripMenuItem.Text = "&Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Image = Properties.Resources.SaveAs;
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(178, 22);
            saveAsToolStripMenuItem.Text = "Save &As";
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(175, 6);
            // 
            // printToolStripMenuItem
            // 
            printToolStripMenuItem.Enabled = false;
            printToolStripMenuItem.Image = (Image)resources.GetObject("printToolStripMenuItem.Image");
            printToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            printToolStripMenuItem.Name = "printToolStripMenuItem";
            printToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.P;
            printToolStripMenuItem.Size = new Size(178, 22);
            printToolStripMenuItem.Text = "&Print";
            // 
            // printPreviewToolStripMenuItem
            // 
            printPreviewToolStripMenuItem.Enabled = false;
            printPreviewToolStripMenuItem.Image = (Image)resources.GetObject("printPreviewToolStripMenuItem.Image");
            printPreviewToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            printPreviewToolStripMenuItem.Size = new Size(178, 22);
            printPreviewToolStripMenuItem.Text = "Print Pre&view";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(175, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(178, 22);
            exitToolStripMenuItem.Text = "E&xit";
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { undoToolStripMenuItem, redoToolStripMenuItem, toolStripSeparator3, cutToolStripMenuItem, copyToolStripMenuItem, pasteToolStripMenuItem, peekAtClipboardToolStripMenuItem, toolStripSeparator4, selectAllToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(39, 20);
            editToolStripMenuItem.Text = "&Edit";
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Z;
            undoToolStripMenuItem.Size = new Size(167, 22);
            undoToolStripMenuItem.Text = "&Undo";
            undoToolStripMenuItem.Click += undoToolStripMenuItem_Click;
            // 
            // redoToolStripMenuItem
            // 
            redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            redoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Y;
            redoToolStripMenuItem.Size = new Size(167, 22);
            redoToolStripMenuItem.Text = "&Redo";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(164, 6);
            // 
            // cutToolStripMenuItem
            // 
            cutToolStripMenuItem.Image = (Image)resources.GetObject("cutToolStripMenuItem.Image");
            cutToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            cutToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.X;
            cutToolStripMenuItem.Size = new Size(167, 22);
            cutToolStripMenuItem.Text = "Cu&t";
            cutToolStripMenuItem.Click += cutToolStripMenuItem_Click;
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Image = (Image)resources.GetObject("copyToolStripMenuItem.Image");
            copyToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.C;
            copyToolStripMenuItem.Size = new Size(167, 22);
            copyToolStripMenuItem.Text = "&Copy";
            copyToolStripMenuItem.Click += copyToolStripMenuItem_Click;
            // 
            // pasteToolStripMenuItem
            // 
            pasteToolStripMenuItem.Image = (Image)resources.GetObject("pasteToolStripMenuItem.Image");
            pasteToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.V;
            pasteToolStripMenuItem.Size = new Size(167, 22);
            pasteToolStripMenuItem.Text = "&Paste";
            pasteToolStripMenuItem.Click += pasteToolStripMenuItem_Click;
            // 
            // peekAtClipboardToolStripMenuItem
            // 
            peekAtClipboardToolStripMenuItem.Name = "peekAtClipboardToolStripMenuItem";
            peekAtClipboardToolStripMenuItem.Size = new Size(167, 22);
            peekAtClipboardToolStripMenuItem.Text = "Peek at Clipboard";
            peekAtClipboardToolStripMenuItem.Click += peekAtClipboardToolStripMenuItem_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(164, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            selectAllToolStripMenuItem.Size = new Size(167, 22);
            selectAllToolStripMenuItem.Text = "Select &All";
            selectAllToolStripMenuItem.Click += selectAllToolStripMenuItem_Click;
            // 
            // scriptMenuItem
            // 
            scriptMenuItem.DropDownItems.AddRange(new ToolStripItem[] { extendedPropertiesToolStripMenuItem });
            scriptMenuItem.Name = "scriptMenuItem";
            scriptMenuItem.Size = new Size(66, 20);
            scriptMenuItem.Text = "Scripting";
            // 
            // extendedPropertiesToolStripMenuItem
            // 
            extendedPropertiesToolStripMenuItem.Name = "extendedPropertiesToolStripMenuItem";
            extendedPropertiesToolStripMenuItem.Size = new Size(179, 22);
            extendedPropertiesToolStripMenuItem.Text = "Extended Properties";
            extendedPropertiesToolStripMenuItem.Click += extendedPropertiesToolStripMenuItem_Click;
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { customizeToolStripMenuItem, optionsToolStripMenuItem, applicationToolStripMenuItem, unitTestingToolStripMenuItem });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(46, 20);
            toolsToolStripMenuItem.Text = "&Tools";
            // 
            // customizeToolStripMenuItem
            // 
            customizeToolStripMenuItem.Enabled = false;
            customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
            customizeToolStripMenuItem.Size = new Size(135, 22);
            customizeToolStripMenuItem.Text = "&Customize";
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(135, 22);
            optionsToolStripMenuItem.Text = "&Options";
            optionsToolStripMenuItem.Click += optionsToolStripMenuItem_Click;
            // 
            // applicationToolStripMenuItem
            // 
            applicationToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { browsePropertiesCommand, browseHelpCommand });
            applicationToolStripMenuItem.Name = "applicationToolStripMenuItem";
            applicationToolStripMenuItem.Size = new Size(135, 22);
            applicationToolStripMenuItem.Text = "Application";
            // 
            // browsePropertiesCommand
            // 
            browsePropertiesCommand.Image = Properties.Resources.Property;
            browsePropertiesCommand.Name = "browsePropertiesCommand";
            browsePropertiesCommand.Size = new Size(187, 22);
            browsePropertiesCommand.Text = "browse Properties";
            browsePropertiesCommand.Click += browsePropertiesCommand_Click;
            // 
            // browseHelpCommand
            // 
            browseHelpCommand.Image = Properties.Resources.StatusHelp;
            browseHelpCommand.Name = "browseHelpCommand";
            browseHelpCommand.Size = new Size(187, 22);
            browseHelpCommand.Text = "browse Help Subjects";
            browseHelpCommand.Click += browseHelpCommand_Click;
            // 
            // unitTestingToolStripMenuItem
            // 
            unitTestingToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { gridViewToolStripMenuItem, testFormToolStripMenuItem, textEditorToolStripMenuItem });
            unitTestingToolStripMenuItem.Name = "unitTestingToolStripMenuItem";
            unitTestingToolStripMenuItem.Size = new Size(135, 22);
            unitTestingToolStripMenuItem.Text = "Testing";
            // 
            // gridViewToolStripMenuItem
            // 
            gridViewToolStripMenuItem.Name = "gridViewToolStripMenuItem";
            gridViewToolStripMenuItem.Size = new Size(129, 22);
            gridViewToolStripMenuItem.Text = "Grid View";
            gridViewToolStripMenuItem.Click += gridViewToolStripMenuItem_Click;
            // 
            // testFormToolStripMenuItem
            // 
            testFormToolStripMenuItem.Name = "testFormToolStripMenuItem";
            testFormToolStripMenuItem.Size = new Size(129, 22);
            testFormToolStripMenuItem.Text = "Test Form";
            // 
            // textEditorToolStripMenuItem
            // 
            textEditorToolStripMenuItem.Name = "textEditorToolStripMenuItem";
            textEditorToolStripMenuItem.Size = new Size(129, 22);
            textEditorToolStripMenuItem.Text = "Text Editor";
            textEditorToolStripMenuItem.Click += textEditorToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { helpContentsMenuItem, helpIndexMenuItem, searchToolStripMenuItem, toolStripSeparator5, helpAboutMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "&Help";
            // 
            // helpContentsMenuItem
            // 
            helpContentsMenuItem.Name = "helpContentsMenuItem";
            helpContentsMenuItem.Size = new Size(122, 22);
            helpContentsMenuItem.Text = "&Contents";
            helpContentsMenuItem.Click += HelpContentsMenuItem_Click;
            // 
            // helpIndexMenuItem
            // 
            helpIndexMenuItem.Name = "helpIndexMenuItem";
            helpIndexMenuItem.Size = new Size(122, 22);
            helpIndexMenuItem.Text = "&Index";
            helpIndexMenuItem.Click += HelpIndexMenuItem_Click;
            // 
            // searchToolStripMenuItem
            // 
            searchToolStripMenuItem.Enabled = false;
            searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            searchToolStripMenuItem.Size = new Size(122, 22);
            searchToolStripMenuItem.Text = "&Search";
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(119, 6);
            // 
            // helpAboutMenuItem
            // 
            helpAboutMenuItem.Name = "helpAboutMenuItem";
            helpAboutMenuItem.Size = new Size(122, 22);
            helpAboutMenuItem.Text = "&About...";
            helpAboutMenuItem.Click += HelpAboutMenuItem_Click;
            // 
            // openFileDialog
            // 
            openFileDialog.FileName = "openFileDialog";
            // 
            // manageModelCommand
            // 
            manageModelCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            manageModelCommand.Image = Properties.Resources.SoftwareDefinitionModel;
            manageModelCommand.ImageTransparentColor = Color.Magenta;
            manageModelCommand.Name = "manageModelCommand";
            manageModelCommand.Size = new Size(32, 22);
            manageModelCommand.Text = "manageModelCommand";
            manageModelCommand.ButtonClick += ManageModelCommand_ButtonClick;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(917, 662);
            Controls.Add(navigationSpliter);
            Controls.Add(navigationPanel);
            Controls.Add(statusStrip);
            Controls.Add(menuStrip);
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            IsMdiContainer = true;
            MainMenuStrip = menuStrip;
            Name = "Main";
            Text = "Data Dictionary manager";
            WindowState = FormWindowState.Maximized;
            FormClosed += Main_FormClosed;
            Load += Main_Load;
            HelpRequested += Main_HelpRequested;
            Controls.SetChildIndex(menuStrip, 0);
            Controls.SetChildIndex(statusStrip, 0);
            Controls.SetChildIndex(navigationPanel, 0);
            Controls.SetChildIndex(navigationSpliter, 0);
            navigationPanel.ResumeLayout(false);
            contextNameLayout.ResumeLayout(false);
            contextNameLayout.PerformLayout();
            dataSourceToolStrip.ResumeLayout(false);
            dataSourceToolStrip.PerformLayout();
            attributeContextMenu.ResumeLayout(false);
            entityContextMenu.ResumeLayout(false);
            subjectAreaContextMenu.ResumeLayout(false);
            catalogContextMenu.ResumeLayout(false);
            libraryContextMenu.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip;
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newEmptyModelMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem printToolStripMenuItem;
        private ToolStripMenuItem printPreviewToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem cutToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem selectAllToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem customizeToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem helpContentsMenuItem;
        private ToolStripMenuItem helpIndexMenuItem;
        private ToolStripMenuItem searchToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem helpAboutMenuItem;
        private ToolStripStatusLabel toolStripWorkerTask;
        private ToolStripStatusLabel toolStripOnlineStatus;
        private ToolStripStatusLabel toolStripWhiteSpace;
        private ToolStripProgressBar toolStripProgressBar;
        private ContextMenuStrip catalogContextMenu;
        private ToolStripMenuItem menuCatalogItem;
        private ToolStripMenuItem menuSchemaItem;
        private ToolStripMenuItem menuTableItem;
        private ToolStripMenuItem menuTableColumnItem;
        private ToolStripMenuItem menuPropertyItem;
        private ToolStripMenuItem unitTestingToolStripMenuItem;
        private ToolStripMenuItem gridViewToolStripMenuItem;
        private ToolStripMenuItem menuConstraintItem;
        private ToolStripMenuItem menuConstraintColumnItem;
        private ToolStripMenuItem menuDataTypeItem;
        private ToolStripMenuItem menuRoutineItem;
        private ToolStripMenuItem menuRoutineParameterItem;
        private ToolStripMenuItem menuRoutineDependencyItem;
        private ToolStripMenuItem scriptMenuItem;
        private ToolStripMenuItem extendedPropertiesToolStripMenuItem;
        private ToolStripMenuItem applicationToolStripMenuItem;
        private ToolStripMenuItem browsePropertiesCommand;
        private ToolStripMenuItem testFormToolStripMenuItem;
        private ToolStripMenuItem peekAtClipboardToolStripMenuItem;
        private ToolStripMenuItem textEditorToolStripMenuItem;
        private ToolStripMenuItem browseHelpCommand;
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        private ToolStrip dataSourceToolStrip;
        private ToolStripSplitButton newAttributeCommand;
        private ToolStripSplitButton newEntityCommand;
        private ToolStripSplitButton newSubjectAreaCommand;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripSplitButton manageLibrariesCommand;
        private ToolStripSplitButton manageDatabasesCommand;
        private ToolStripButton refreshCommand;
        private TreeView contextNameNavigation;
        private ToolStripSeparator toolStripSeparator7;
        private TableLayoutPanel contextNameLayout;
        private ContextMenuStrip attributeContextMenu;
        private ToolStripMenuItem menuAttributes;
        private ToolStripMenuItem menuAttributeProperties;
        private ToolStripMenuItem menuAttributeAlaises;
        private ContextMenuStrip entityContextMenu;
        private ToolStripMenuItem entitiesToolStripMenuItem;
        private ToolStripMenuItem entityPropertiesToolStripMenuItem;
        private ToolStripMenuItem entityAliasToolStripMenuItem;
        private ContextMenuStrip subjectAreaContextMenu;
        private ToolStripMenuItem subjectAreaToolStripMenuItem;
        private ContextMenuStrip libraryContextMenu;
        private ToolStripMenuItem viewLibrarySourceCommand;
        private ToolStripMenuItem viewLibraryMemberCommand;
        private ToolStripSplitButton manageModelCommand;
    }
}