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
            Label serverNameLayout;
            Label databaseNameLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
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
            unitTestToolStripMenuItem = new ToolStripMenuItem();
            testConnectionToolStripMenuItem = new ToolStripMenuItem();
            appExceptionToolStripMenuItem = new ToolStripMenuItem();
            getSchemaToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            contentsToolStripMenuItem = new ToolStripMenuItem();
            indexToolStripMenuItem = new ToolStripMenuItem();
            searchToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            navTabControl = new TabControl();
            navDomainTab = new TabPage();
            navDbSchemaTab = new TabPage();
            navMainPanel = new TableLayoutPanel();
            layoutConnectionPanel = new GroupBox();
            layoutConnectionTable = new TableLayoutPanel();
            serverNameData = new TextBox();
            databaseNameData = new TextBox();
            navSplitter = new Splitter();
            serverNameLayout = new Label();
            databaseNameLayout = new Label();
            statusStrip.SuspendLayout();
            menuStrip.SuspendLayout();
            navTabControl.SuspendLayout();
            navMainPanel.SuspendLayout();
            layoutConnectionPanel.SuspendLayout();
            layoutConnectionTable.SuspendLayout();
            SuspendLayout();
            // 
            // serverNameLayout
            // 
            serverNameLayout.AutoSize = true;
            serverNameLayout.Location = new Point(3, 0);
            serverNameLayout.Name = "serverNameLayout";
            serverNameLayout.Size = new Size(74, 15);
            serverNameLayout.TabIndex = 0;
            serverNameLayout.Text = "Server Name";
            // 
            // databaseNameLayout
            // 
            databaseNameLayout.AutoSize = true;
            databaseNameLayout.Location = new Point(3, 44);
            databaseNameLayout.Name = "databaseNameLayout";
            databaseNameLayout.Size = new Size(90, 15);
            databaseNameLayout.TabIndex = 1;
            databaseNameLayout.Text = "Database Name";
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripInfo, toolStripWhiteSpace, toolStripWorkerTask, toolStripProgressBar });
            statusStrip.Location = new Point(0, 428);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(800, 22);
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
            toolStripWhiteSpace.Size = new Size(507, 17);
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
            menuStrip.Size = new Size(800, 24);
            menuStrip.TabIndex = 1;
            menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, openToolStripMenuItem, toolStripSeparator, saveToolStripMenuItem, saveAsToolStripMenuItem, toolStripSeparator1, printToolStripMenuItem, printPreviewToolStripMenuItem, toolStripSeparator2, exitToolStripMenuItem });
            fileToolStripMenuItem.Enabled = false;
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Image = (Image)resources.GetObject("newToolStripMenuItem.Image");
            newToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            newToolStripMenuItem.Size = new Size(146, 22);
            newToolStripMenuItem.Text = "&New";
            // 
            // openToolStripMenuItem
            // 
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
            saveToolStripMenuItem.Image = (Image)resources.GetObject("saveToolStripMenuItem.Image");
            saveToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveToolStripMenuItem.Size = new Size(146, 22);
            saveToolStripMenuItem.Text = "&Save";
            // 
            // saveAsToolStripMenuItem
            // 
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
            printToolStripMenuItem.Image = (Image)resources.GetObject("printToolStripMenuItem.Image");
            printToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            printToolStripMenuItem.Name = "printToolStripMenuItem";
            printToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.P;
            printToolStripMenuItem.Size = new Size(146, 22);
            printToolStripMenuItem.Text = "&Print";
            // 
            // printPreviewToolStripMenuItem
            // 
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
            editToolStripMenuItem.Enabled = false;
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
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Image = (Image)resources.GetObject("copyToolStripMenuItem.Image");
            copyToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.C;
            copyToolStripMenuItem.Size = new Size(144, 22);
            copyToolStripMenuItem.Text = "&Copy";
            // 
            // pasteToolStripMenuItem
            // 
            pasteToolStripMenuItem.Image = (Image)resources.GetObject("pasteToolStripMenuItem.Image");
            pasteToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.V;
            pasteToolStripMenuItem.Size = new Size(144, 22);
            pasteToolStripMenuItem.Text = "&Paste";
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
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { customizeToolStripMenuItem, optionsToolStripMenuItem, unitTestToolStripMenuItem });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(46, 20);
            toolsToolStripMenuItem.Text = "&Tools";
            // 
            // customizeToolStripMenuItem
            // 
            customizeToolStripMenuItem.Enabled = false;
            customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
            customizeToolStripMenuItem.Size = new Size(130, 22);
            customizeToolStripMenuItem.Text = "&Customize";
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.Enabled = false;
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(130, 22);
            optionsToolStripMenuItem.Text = "&Options";
            // 
            // unitTestToolStripMenuItem
            // 
            unitTestToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { testConnectionToolStripMenuItem, appExceptionToolStripMenuItem, getSchemaToolStripMenuItem });
            unitTestToolStripMenuItem.Name = "unitTestToolStripMenuItem";
            unitTestToolStripMenuItem.Size = new Size(130, 22);
            unitTestToolStripMenuItem.Text = "Unit Test";
            // 
            // testConnectionToolStripMenuItem
            // 
            testConnectionToolStripMenuItem.Name = "testConnectionToolStripMenuItem";
            testConnectionToolStripMenuItem.Size = new Size(159, 22);
            testConnectionToolStripMenuItem.Text = "Test Connection";
            testConnectionToolStripMenuItem.Click += testConnectionToolStripMenuItem_Click;
            // 
            // appExceptionToolStripMenuItem
            // 
            appExceptionToolStripMenuItem.Name = "appExceptionToolStripMenuItem";
            appExceptionToolStripMenuItem.Size = new Size(159, 22);
            appExceptionToolStripMenuItem.Text = "App Exception";
            appExceptionToolStripMenuItem.Click += appExceptionToolStripMenuItem_Click;
            // 
            // getSchemaToolStripMenuItem
            // 
            getSchemaToolStripMenuItem.Name = "getSchemaToolStripMenuItem";
            getSchemaToolStripMenuItem.Size = new Size(159, 22);
            getSchemaToolStripMenuItem.Text = "Get Schema";
            getSchemaToolStripMenuItem.Click += getSchemaToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { contentsToolStripMenuItem, indexToolStripMenuItem, searchToolStripMenuItem, toolStripSeparator5, aboutToolStripMenuItem });
            helpToolStripMenuItem.Enabled = false;
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "&Help";
            // 
            // contentsToolStripMenuItem
            // 
            contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            contentsToolStripMenuItem.Size = new Size(122, 22);
            contentsToolStripMenuItem.Text = "&Contents";
            // 
            // indexToolStripMenuItem
            // 
            indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            indexToolStripMenuItem.Size = new Size(122, 22);
            indexToolStripMenuItem.Text = "&Index";
            // 
            // searchToolStripMenuItem
            // 
            searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            searchToolStripMenuItem.Size = new Size(122, 22);
            searchToolStripMenuItem.Text = "&Search";
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(119, 6);
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(122, 22);
            aboutToolStripMenuItem.Text = "&About...";
            // 
            // navTabControl
            // 
            navTabControl.Controls.Add(navDomainTab);
            navTabControl.Controls.Add(navDbSchemaTab);
            navTabControl.Dock = DockStyle.Fill;
            navTabControl.Location = new Point(3, 119);
            navTabControl.Name = "navTabControl";
            navTabControl.SelectedIndex = 0;
            navTabControl.Size = new Size(194, 282);
            navTabControl.TabIndex = 12;
            // 
            // navDomainTab
            // 
            navDomainTab.Location = new Point(4, 24);
            navDomainTab.Name = "navDomainTab";
            navDomainTab.Padding = new Padding(3);
            navDomainTab.Size = new Size(186, 254);
            navDomainTab.TabIndex = 1;
            navDomainTab.Text = "Domain";
            navDomainTab.UseVisualStyleBackColor = true;
            // 
            // navDbSchemaTab
            // 
            navDbSchemaTab.Location = new Point(4, 24);
            navDbSchemaTab.Name = "navDbSchemaTab";
            navDbSchemaTab.Padding = new Padding(3);
            navDbSchemaTab.Size = new Size(186, 254);
            navDbSchemaTab.TabIndex = 0;
            navDbSchemaTab.Text = "Db Schema";
            navDbSchemaTab.UseVisualStyleBackColor = true;
            // 
            // navMainPanel
            // 
            navMainPanel.ColumnCount = 1;
            navMainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            navMainPanel.Controls.Add(layoutConnectionPanel, 0, 0);
            navMainPanel.Controls.Add(navTabControl, 0, 1);
            navMainPanel.Dock = DockStyle.Left;
            navMainPanel.Location = new Point(0, 24);
            navMainPanel.Name = "navMainPanel";
            navMainPanel.RowCount = 2;
            navMainPanel.RowStyles.Add(new RowStyle());
            navMainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            navMainPanel.Size = new Size(200, 404);
            navMainPanel.TabIndex = 15;
            // 
            // layoutConnectionPanel
            // 
            layoutConnectionPanel.AutoSize = true;
            layoutConnectionPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutConnectionPanel.Controls.Add(layoutConnectionTable);
            layoutConnectionPanel.Dock = DockStyle.Fill;
            layoutConnectionPanel.Location = new Point(3, 3);
            layoutConnectionPanel.Name = "layoutConnectionPanel";
            layoutConnectionPanel.Size = new Size(194, 110);
            layoutConnectionPanel.TabIndex = 17;
            layoutConnectionPanel.TabStop = false;
            layoutConnectionPanel.Text = "Connection";
            // 
            // layoutConnectionTable
            // 
            layoutConnectionTable.AutoSize = true;
            layoutConnectionTable.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutConnectionTable.ColumnCount = 1;
            layoutConnectionTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutConnectionTable.Controls.Add(serverNameLayout, 0, 0);
            layoutConnectionTable.Controls.Add(databaseNameLayout, 0, 2);
            layoutConnectionTable.Controls.Add(serverNameData, 0, 1);
            layoutConnectionTable.Controls.Add(databaseNameData, 0, 3);
            layoutConnectionTable.Dock = DockStyle.Fill;
            layoutConnectionTable.Location = new Point(3, 19);
            layoutConnectionTable.Name = "layoutConnectionTable";
            layoutConnectionTable.RowCount = 4;
            layoutConnectionTable.RowStyles.Add(new RowStyle());
            layoutConnectionTable.RowStyles.Add(new RowStyle());
            layoutConnectionTable.RowStyles.Add(new RowStyle());
            layoutConnectionTable.RowStyles.Add(new RowStyle());
            layoutConnectionTable.Size = new Size(188, 88);
            layoutConnectionTable.TabIndex = 0;
            // 
            // serverNameData
            // 
            serverNameData.Dock = DockStyle.Fill;
            serverNameData.Location = new Point(3, 18);
            serverNameData.Name = "serverNameData";
            serverNameData.ReadOnly = true;
            serverNameData.Size = new Size(182, 23);
            serverNameData.TabIndex = 2;
            // 
            // databaseNameData
            // 
            databaseNameData.Dock = DockStyle.Fill;
            databaseNameData.Location = new Point(3, 62);
            databaseNameData.Name = "databaseNameData";
            databaseNameData.ReadOnly = true;
            databaseNameData.Size = new Size(182, 23);
            databaseNameData.TabIndex = 3;
            // 
            // navSplitter
            // 
            navSplitter.Location = new Point(200, 24);
            navSplitter.Name = "navSplitter";
            navSplitter.Size = new Size(3, 404);
            navSplitter.TabIndex = 16;
            navSplitter.TabStop = false;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(navSplitter);
            Controls.Add(navMainPanel);
            Controls.Add(statusStrip);
            Controls.Add(menuStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            IsMdiContainer = true;
            MainMenuStrip = menuStrip;
            Name = "Main";
            Text = "Data Dictionary manager";
            FormClosed += Main_FormClosed;
            Load += Main_Load;
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            navTabControl.ResumeLayout(false);
            navMainPanel.ResumeLayout(false);
            navMainPanel.PerformLayout();
            layoutConnectionPanel.ResumeLayout(false);
            layoutConnectionPanel.PerformLayout();
            layoutConnectionTable.ResumeLayout(false);
            layoutConnectionTable.PerformLayout();
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
        private ToolStripMenuItem contentsToolStripMenuItem;
        private ToolStripMenuItem indexToolStripMenuItem;
        private ToolStripMenuItem searchToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem unitTestToolStripMenuItem;
        private ToolStripMenuItem testConnectionToolStripMenuItem;
        private ToolStripMenuItem appExceptionToolStripMenuItem;
        private ToolStripMenuItem getSchemaToolStripMenuItem;
        private ToolStripStatusLabel toolStripWorkerTask;
        private ToolStripStatusLabel toolStripInfo;
        private ToolStripStatusLabel toolStripWhiteSpace;
        private ToolStripProgressBar toolStripProgressBar;
        private TabControl navTabControl;
        private TabPage navDbSchemaTab;
        private TabPage navDomainTab;
        private TableLayoutPanel navMainPanel;
        private Splitter navSplitter;
        private GroupBox layoutConnectionPanel;
        private TableLayoutPanel layoutConnectionTable;
        private TextBox serverNameData;
        private TextBox databaseNameData;
    }
}