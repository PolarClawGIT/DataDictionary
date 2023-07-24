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
            SplitContainer modelSpliter;
            TableLayoutPanel navigationModelLayout;
            Splitter navigationSpliter;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            modelNameData = new Controls.TextBoxData();
            modelDescriptionData = new Controls.TextBoxData();
            navigationTabs = new TabControl();
            navigationDbSchemaTab = new TabPage();
            dbMetaDataNavigation = new TreeView();
            dbSchemaContextMenu = new ContextMenuStrip(components);
            menuCatalogItem = new ToolStripMenuItem();
            menuSchemaItem = new ToolStripMenuItem();
            menuTableItem = new ToolStripMenuItem();
            menuColumnItem = new ToolStripMenuItem();
            menuPropertyItem = new ToolStripMenuItem();
            navigationDomainTab = new TabPage();
            domainModelNavigation = new TreeView();
            domainModelMenu = new ContextMenuStrip(components);
            menuImportDbSchema = new ToolStripMenuItem();
            menuAttributes = new ToolStripMenuItem();
            statusStrip = new StatusStrip();
            toolStripInfo = new ToolStripStatusLabel();
            toolStripWhiteSpace = new ToolStripStatusLabel();
            toolStripWorkerTask = new ToolStripStatusLabel();
            toolStripProgressBar = new ToolStripProgressBar();
            menuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
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
            toolStripSeparator4 = new ToolStripSeparator();
            selectAllToolStripMenuItem = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            customizeToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            dbSchemaToolStripMenuItem = new ToolStripMenuItem();
            domainModelToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            helpContentsMenuItem = new ToolStripMenuItem();
            helpIndexMenuItem = new ToolStripMenuItem();
            searchToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            helpAboutMenuItem = new ToolStripMenuItem();
            unitTestingToolStripMenuItem = new ToolStripMenuItem();
            gridViewToolStripMenuItem = new ToolStripMenuItem();
            navigationPanel = new Panel();
            modelSpliter = new SplitContainer();
            navigationModelLayout = new TableLayoutPanel();
            navigationSpliter = new Splitter();
            navigationPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)modelSpliter).BeginInit();
            modelSpliter.Panel1.SuspendLayout();
            modelSpliter.Panel2.SuspendLayout();
            modelSpliter.SuspendLayout();
            navigationModelLayout.SuspendLayout();
            navigationTabs.SuspendLayout();
            navigationDbSchemaTab.SuspendLayout();
            dbSchemaContextMenu.SuspendLayout();
            navigationDomainTab.SuspendLayout();
            domainModelMenu.SuspendLayout();
            statusStrip.SuspendLayout();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // navigationPanel
            // 
            navigationPanel.Controls.Add(modelSpliter);
            navigationPanel.Dock = DockStyle.Left;
            navigationPanel.Location = new Point(0, 24);
            navigationPanel.Name = "navigationPanel";
            navigationPanel.Size = new Size(220, 686);
            navigationPanel.TabIndex = 6;
            // 
            // modelSpliter
            // 
            modelSpliter.BorderStyle = BorderStyle.FixedSingle;
            modelSpliter.Dock = DockStyle.Fill;
            modelSpliter.Location = new Point(0, 0);
            modelSpliter.Name = "modelSpliter";
            modelSpliter.Orientation = Orientation.Horizontal;
            // 
            // modelSpliter.Panel1
            // 
            modelSpliter.Panel1.Controls.Add(navigationModelLayout);
            // 
            // modelSpliter.Panel2
            // 
            modelSpliter.Panel2.Controls.Add(navigationTabs);
            modelSpliter.Size = new Size(220, 686);
            modelSpliter.SplitterDistance = 110;
            modelSpliter.TabIndex = 2;
            // 
            // navigationModelLayout
            // 
            navigationModelLayout.AutoSize = true;
            navigationModelLayout.ColumnCount = 1;
            navigationModelLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            navigationModelLayout.Controls.Add(modelNameData, 0, 0);
            navigationModelLayout.Controls.Add(modelDescriptionData, 0, 1);
            navigationModelLayout.Dock = DockStyle.Fill;
            navigationModelLayout.Location = new Point(0, 0);
            navigationModelLayout.Name = "navigationModelLayout";
            navigationModelLayout.RowCount = 2;
            navigationModelLayout.RowStyles.Add(new RowStyle());
            navigationModelLayout.RowStyles.Add(new RowStyle());
            navigationModelLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            navigationModelLayout.Size = new Size(218, 108);
            navigationModelLayout.TabIndex = 2;
            // 
            // modelNameData
            // 
            modelNameData.AutoSize = true;
            modelNameData.Dock = DockStyle.Fill;
            modelNameData.HeaderText = "Model";
            modelNameData.Location = new Point(3, 3);
            modelNameData.Multiline = false;
            modelNameData.Name = "modelNameData";
            modelNameData.ReadOnly = false;
            modelNameData.Size = new Size(212, 44);
            modelNameData.TabIndex = 2;
            // 
            // modelDescriptionData
            // 
            modelDescriptionData.AutoSize = true;
            modelDescriptionData.Dock = DockStyle.Fill;
            modelDescriptionData.HeaderText = "Description";
            modelDescriptionData.Location = new Point(3, 53);
            modelDescriptionData.Multiline = true;
            modelDescriptionData.Name = "modelDescriptionData";
            modelDescriptionData.ReadOnly = false;
            modelDescriptionData.Size = new Size(212, 52);
            modelDescriptionData.TabIndex = 3;
            // 
            // navigationTabs
            // 
            navigationTabs.Controls.Add(navigationDbSchemaTab);
            navigationTabs.Controls.Add(navigationDomainTab);
            navigationTabs.Dock = DockStyle.Fill;
            navigationTabs.Location = new Point(0, 0);
            navigationTabs.Name = "navigationTabs";
            navigationTabs.SelectedIndex = 0;
            navigationTabs.Size = new Size(218, 570);
            navigationTabs.TabIndex = 1;
            // 
            // navigationDbSchemaTab
            // 
            navigationDbSchemaTab.Controls.Add(dbMetaDataNavigation);
            navigationDbSchemaTab.Location = new Point(4, 24);
            navigationDbSchemaTab.Name = "navigationDbSchemaTab";
            navigationDbSchemaTab.Padding = new Padding(3);
            navigationDbSchemaTab.Size = new Size(210, 542);
            navigationDbSchemaTab.TabIndex = 0;
            navigationDbSchemaTab.Text = "Db Schema";
            navigationDbSchemaTab.UseVisualStyleBackColor = true;
            // 
            // dbMetaDataNavigation
            // 
            dbMetaDataNavigation.ContextMenuStrip = dbSchemaContextMenu;
            dbMetaDataNavigation.Dock = DockStyle.Fill;
            dbMetaDataNavigation.Location = new Point(3, 3);
            dbMetaDataNavigation.Name = "dbMetaDataNavigation";
            dbMetaDataNavigation.Size = new Size(204, 536);
            dbMetaDataNavigation.TabIndex = 0;
            dbMetaDataNavigation.NodeMouseDoubleClick += dbMetaDataNavigation_NodeMouseDoubleClick;
            // 
            // dbSchemaContextMenu
            // 
            dbSchemaContextMenu.Items.AddRange(new ToolStripItem[] { menuCatalogItem, menuSchemaItem, menuTableItem, menuColumnItem, menuPropertyItem });
            dbSchemaContextMenu.Name = "dbSchemacontextMenu";
            dbSchemaContextMenu.Size = new Size(169, 114);
            // 
            // menuCatalogItem
            // 
            menuCatalogItem.Image = Properties.Resources.Database;
            menuCatalogItem.Name = "menuCatalogItem";
            menuCatalogItem.Size = new Size(168, 22);
            menuCatalogItem.Text = "Manage C&atalogs";
            menuCatalogItem.Click += menuCatalogItem_Click;
            // 
            // menuSchemaItem
            // 
            menuSchemaItem.Image = Properties.Resources.Schema;
            menuSchemaItem.Name = "menuSchemaItem";
            menuSchemaItem.Size = new Size(168, 22);
            menuSchemaItem.Text = "Browse &Schemas";
            menuSchemaItem.Click += menuSchemaItem_Click;
            // 
            // menuTableItem
            // 
            menuTableItem.Image = Properties.Resources.Table;
            menuTableItem.Name = "menuTableItem";
            menuTableItem.Size = new Size(168, 22);
            menuTableItem.Text = "Browse &Tables";
            menuTableItem.Click += menuTableItem_Click;
            // 
            // menuColumnItem
            // 
            menuColumnItem.Image = Properties.Resources.Column;
            menuColumnItem.Name = "menuColumnItem";
            menuColumnItem.Size = new Size(168, 22);
            menuColumnItem.Text = "Browse &Columns";
            menuColumnItem.Click += menuColumnItem_Click;
            // 
            // menuPropertyItem
            // 
            menuPropertyItem.Image = Properties.Resources.ExtendedProperty;
            menuPropertyItem.Name = "menuPropertyItem";
            menuPropertyItem.Size = new Size(168, 22);
            menuPropertyItem.Text = "Browse &Properties";
            menuPropertyItem.Click += menuPropertyItem_Click;
            // 
            // navigationDomainTab
            // 
            navigationDomainTab.Controls.Add(domainModelNavigation);
            navigationDomainTab.Location = new Point(4, 24);
            navigationDomainTab.Name = "navigationDomainTab";
            navigationDomainTab.Padding = new Padding(3);
            navigationDomainTab.Size = new Size(210, 542);
            navigationDomainTab.TabIndex = 1;
            navigationDomainTab.Text = "Domain Model";
            navigationDomainTab.UseVisualStyleBackColor = true;
            // 
            // domainModelNavigation
            // 
            domainModelNavigation.ContextMenuStrip = domainModelMenu;
            domainModelNavigation.Dock = DockStyle.Fill;
            domainModelNavigation.Location = new Point(3, 3);
            domainModelNavigation.Name = "domainModelNavigation";
            domainModelNavigation.Size = new Size(204, 536);
            domainModelNavigation.TabIndex = 0;
            domainModelNavigation.NodeMouseDoubleClick += domainModelNavigation_NodeMouseDoubleClick;
            // 
            // domainModelMenu
            // 
            domainModelMenu.Items.AddRange(new ToolStripItem[] { menuImportDbSchema, menuAttributes });
            domainModelMenu.Name = "domainModelMenu";
            domainModelMenu.Size = new Size(203, 48);
            // 
            // menuImportDbSchema
            // 
            menuImportDbSchema.Image = Properties.Resources.Dictionary;
            menuImportDbSchema.Name = "menuImportDbSchema";
            menuImportDbSchema.Size = new Size(202, 22);
            menuImportDbSchema.Text = "Import from &Db Schema";
            menuImportDbSchema.Click += menuImportDbSchema_Click;
            // 
            // menuAttributes
            // 
            menuAttributes.Image = Properties.Resources.Attribute;
            menuAttributes.Name = "menuAttributes";
            menuAttributes.Size = new Size(202, 22);
            menuAttributes.Text = "Browse &Attributes";
            menuAttributes.Click += menuAttributes_Click;
            // 
            // navigationSpliter
            // 
            navigationSpliter.Location = new Point(220, 24);
            navigationSpliter.Name = "navigationSpliter";
            navigationSpliter.Size = new Size(3, 686);
            navigationSpliter.TabIndex = 8;
            navigationSpliter.TabStop = false;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripInfo, toolStripWhiteSpace, toolStripWorkerTask, toolStripProgressBar });
            statusStrip.Location = new Point(0, 710);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1141, 22);
            statusStrip.TabIndex = 0;
            statusStrip.Text = "statusStrip1";
            // 
            // toolStripInfo
            // 
            toolStripInfo.Name = "toolStripInfo";
            toolStripInfo.Size = new Size(106, 17);
            toolStripInfo.Text = "(to be determined)";
            // 
            // toolStripWhiteSpace
            // 
            toolStripWhiteSpace.Name = "toolStripWhiteSpace";
            toolStripWhiteSpace.Size = new Size(848, 17);
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
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, toolsToolStripMenuItem, helpToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1141, 24);
            menuStrip.TabIndex = 1;
            menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, openToolStripMenuItem, toolStripSeparator, saveToolStripMenuItem, saveAsToolStripMenuItem, toolStripSeparator1, printToolStripMenuItem, printPreviewToolStripMenuItem, toolStripSeparator2, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Enabled = false;
            newToolStripMenuItem.Image = (Image)resources.GetObject("newToolStripMenuItem.Image");
            newToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            newToolStripMenuItem.Size = new Size(146, 22);
            newToolStripMenuItem.Text = "&New";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Enabled = false;
            openToolStripMenuItem.Image = (Image)resources.GetObject("openToolStripMenuItem.Image");
            openToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            openToolStripMenuItem.Size = new Size(146, 22);
            openToolStripMenuItem.Text = "&Open";
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new Size(143, 6);
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Enabled = false;
            saveToolStripMenuItem.Image = (Image)resources.GetObject("saveToolStripMenuItem.Image");
            saveToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveToolStripMenuItem.Size = new Size(146, 22);
            saveToolStripMenuItem.Text = "&Save";
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(146, 22);
            saveAsToolStripMenuItem.Text = "Save &As";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(143, 6);
            // 
            // printToolStripMenuItem
            // 
            printToolStripMenuItem.Enabled = false;
            printToolStripMenuItem.Image = (Image)resources.GetObject("printToolStripMenuItem.Image");
            printToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            printToolStripMenuItem.Name = "printToolStripMenuItem";
            printToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.P;
            printToolStripMenuItem.Size = new Size(146, 22);
            printToolStripMenuItem.Text = "&Print";
            // 
            // printPreviewToolStripMenuItem
            // 
            printPreviewToolStripMenuItem.Enabled = false;
            printPreviewToolStripMenuItem.Image = (Image)resources.GetObject("printPreviewToolStripMenuItem.Image");
            printPreviewToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            printPreviewToolStripMenuItem.Size = new Size(146, 22);
            printPreviewToolStripMenuItem.Text = "Print Pre&view";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(143, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(146, 22);
            exitToolStripMenuItem.Text = "E&xit";
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { undoToolStripMenuItem, redoToolStripMenuItem, toolStripSeparator3, cutToolStripMenuItem, copyToolStripMenuItem, pasteToolStripMenuItem, toolStripSeparator4, selectAllToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(39, 20);
            editToolStripMenuItem.Text = "&Edit";
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Z;
            undoToolStripMenuItem.Size = new Size(144, 22);
            undoToolStripMenuItem.Text = "&Undo";
            undoToolStripMenuItem.Click += undoToolStripMenuItem_Click;
            // 
            // redoToolStripMenuItem
            // 
            redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            redoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Y;
            redoToolStripMenuItem.Size = new Size(144, 22);
            redoToolStripMenuItem.Text = "&Redo";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(141, 6);
            // 
            // cutToolStripMenuItem
            // 
            cutToolStripMenuItem.Image = (Image)resources.GetObject("cutToolStripMenuItem.Image");
            cutToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            cutToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.X;
            cutToolStripMenuItem.Size = new Size(144, 22);
            cutToolStripMenuItem.Text = "Cu&t";
            cutToolStripMenuItem.Click += cutToolStripMenuItem_Click;
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Image = (Image)resources.GetObject("copyToolStripMenuItem.Image");
            copyToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.C;
            copyToolStripMenuItem.Size = new Size(144, 22);
            copyToolStripMenuItem.Text = "&Copy";
            copyToolStripMenuItem.Click += copyToolStripMenuItem_Click;
            // 
            // pasteToolStripMenuItem
            // 
            pasteToolStripMenuItem.Image = (Image)resources.GetObject("pasteToolStripMenuItem.Image");
            pasteToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.V;
            pasteToolStripMenuItem.Size = new Size(144, 22);
            pasteToolStripMenuItem.Text = "&Paste";
            pasteToolStripMenuItem.Click += pasteToolStripMenuItem_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(141, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            selectAllToolStripMenuItem.Size = new Size(144, 22);
            selectAllToolStripMenuItem.Text = "Select &All";
            selectAllToolStripMenuItem.Click += selectAllToolStripMenuItem_Click;
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { customizeToolStripMenuItem, optionsToolStripMenuItem, dbSchemaToolStripMenuItem, domainModelToolStripMenuItem, unitTestingToolStripMenuItem });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(46, 20);
            toolsToolStripMenuItem.Text = "&Tools";
            // 
            // customizeToolStripMenuItem
            // 
            customizeToolStripMenuItem.Enabled = false;
            customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
            customizeToolStripMenuItem.Size = new Size(180, 22);
            customizeToolStripMenuItem.Text = "&Customize";
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.Enabled = false;
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(180, 22);
            optionsToolStripMenuItem.Text = "&Options";
            // 
            // dbSchemaToolStripMenuItem
            // 
            dbSchemaToolStripMenuItem.Image = Properties.Resources.Database;
            dbSchemaToolStripMenuItem.Name = "dbSchemaToolStripMenuItem";
            dbSchemaToolStripMenuItem.Size = new Size(180, 22);
            dbSchemaToolStripMenuItem.Text = "Db &Schema";
            // 
            // domainModelToolStripMenuItem
            // 
            domainModelToolStripMenuItem.Image = Properties.Resources.Dictionary;
            domainModelToolStripMenuItem.Name = "domainModelToolStripMenuItem";
            domainModelToolStripMenuItem.Size = new Size(180, 22);
            domainModelToolStripMenuItem.Text = "Domain &Model";
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
            // unitTestingToolStripMenuItem
            // 
            unitTestingToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { gridViewToolStripMenuItem });
            unitTestingToolStripMenuItem.Name = "unitTestingToolStripMenuItem";
            unitTestingToolStripMenuItem.Size = new Size(180, 22);
            unitTestingToolStripMenuItem.Text = "Unit Testing";
            // 
            // gridViewToolStripMenuItem
            // 
            gridViewToolStripMenuItem.Name = "gridViewToolStripMenuItem";
            gridViewToolStripMenuItem.Size = new Size(180, 22);
            gridViewToolStripMenuItem.Text = "Grid View";
            gridViewToolStripMenuItem.Click += gridViewToolStripMenuItem_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1141, 732);
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
            FormClosed += Main_FormClosed;
            Load += Main_Load;
            HelpRequested += Main_HelpRequested;
            navigationPanel.ResumeLayout(false);
            modelSpliter.Panel1.ResumeLayout(false);
            modelSpliter.Panel1.PerformLayout();
            modelSpliter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)modelSpliter).EndInit();
            modelSpliter.ResumeLayout(false);
            navigationModelLayout.ResumeLayout(false);
            navigationModelLayout.PerformLayout();
            navigationTabs.ResumeLayout(false);
            navigationDbSchemaTab.ResumeLayout(false);
            dbSchemaContextMenu.ResumeLayout(false);
            navigationDomainTab.ResumeLayout(false);
            domainModelMenu.ResumeLayout(false);
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
        private ToolStripMenuItem newToolStripMenuItem;
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
        private ToolStripStatusLabel toolStripInfo;
        private ToolStripStatusLabel toolStripWhiteSpace;
        private ToolStripProgressBar toolStripProgressBar;
        private ContextMenuStrip dbSchemaContextMenu;
        private ContextMenuStrip domainModelMenu;
        private ToolStripMenuItem menuCatalogItem;
        private ToolStripMenuItem menuSchemaItem;
        private ToolStripMenuItem menuTableItem;
        private ToolStripMenuItem menuColumnItem;
        private ToolStripMenuItem menuPropertyItem;
        private ToolStripMenuItem menuImportDbSchema;
        private ToolStripMenuItem menuAttributes;
        private ToolStripMenuItem dbSchemaToolStripMenuItem;
        private ToolStripMenuItem domainModelToolStripMenuItem;
        private TabControl navigationTabs;
        private TabPage navigationDbSchemaTab;
        private TreeView dbMetaDataNavigation;
        private TabPage navigationDomainTab;
        private TreeView domainModelNavigation;
        private Controls.TextBoxData modelNameData;
        private Controls.TextBoxData modelDescriptionData;
        private ToolStripMenuItem unitTestingToolStripMenuItem;
        private ToolStripMenuItem gridViewToolStripMenuItem;
    }
}