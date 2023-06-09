namespace DataDictionary.Main.Control
{
    partial class MainNavigation
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Label serverNameLayout;
            Label databaseNameLayout;
            navMainPanel = new TableLayoutPanel();
            layoutConnectionPanel = new GroupBox();
            layoutConnectionTable = new TableLayoutPanel();
            serverNameData = new TextBox();
            databaseNameData = new TextBox();
            navTabControl = new TabControl();
            navDomainTab = new TabPage();
            navDbSchemaTab = new TabPage();
            serverNameLayout = new Label();
            databaseNameLayout = new Label();
            navMainPanel.SuspendLayout();
            layoutConnectionPanel.SuspendLayout();
            layoutConnectionTable.SuspendLayout();
            navTabControl.SuspendLayout();
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
            // navMainPanel
            // 
            navMainPanel.ColumnCount = 1;
            navMainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            navMainPanel.Controls.Add(layoutConnectionPanel, 0, 0);
            navMainPanel.Controls.Add(navTabControl, 0, 1);
            navMainPanel.Dock = DockStyle.Fill;
            navMainPanel.Location = new Point(0, 0);
            navMainPanel.Name = "navMainPanel";
            navMainPanel.RowCount = 2;
            navMainPanel.RowStyles.Add(new RowStyle());
            navMainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            navMainPanel.Size = new Size(242, 401);
            navMainPanel.TabIndex = 16;
            // 
            // layoutConnectionPanel
            // 
            layoutConnectionPanel.AutoSize = true;
            layoutConnectionPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutConnectionPanel.Controls.Add(layoutConnectionTable);
            layoutConnectionPanel.Dock = DockStyle.Fill;
            layoutConnectionPanel.Location = new Point(3, 3);
            layoutConnectionPanel.Name = "layoutConnectionPanel";
            layoutConnectionPanel.Size = new Size(236, 110);
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
            layoutConnectionTable.Size = new Size(230, 88);
            layoutConnectionTable.TabIndex = 0;
            // 
            // serverNameData
            // 
            serverNameData.Dock = DockStyle.Fill;
            serverNameData.Location = new Point(3, 18);
            serverNameData.Name = "serverNameData";
            serverNameData.ReadOnly = true;
            serverNameData.Size = new Size(224, 23);
            serverNameData.TabIndex = 2;
            // 
            // databaseNameData
            // 
            databaseNameData.Dock = DockStyle.Fill;
            databaseNameData.Location = new Point(3, 62);
            databaseNameData.Name = "databaseNameData";
            databaseNameData.ReadOnly = true;
            databaseNameData.Size = new Size(224, 23);
            databaseNameData.TabIndex = 3;
            // 
            // navTabControl
            // 
            navTabControl.Controls.Add(navDomainTab);
            navTabControl.Controls.Add(navDbSchemaTab);
            navTabControl.Dock = DockStyle.Fill;
            navTabControl.Location = new Point(3, 119);
            navTabControl.Name = "navTabControl";
            navTabControl.SelectedIndex = 0;
            navTabControl.Size = new Size(236, 279);
            navTabControl.TabIndex = 12;
            // 
            // navDomainTab
            // 
            navDomainTab.Location = new Point(4, 24);
            navDomainTab.Name = "navDomainTab";
            navDomainTab.Padding = new Padding(3);
            navDomainTab.Size = new Size(228, 251);
            navDomainTab.TabIndex = 1;
            navDomainTab.Text = "Domain";
            navDomainTab.UseVisualStyleBackColor = true;
            // 
            // navDbSchemaTab
            // 
            navDbSchemaTab.Location = new Point(4, 24);
            navDbSchemaTab.Name = "navDbSchemaTab";
            navDbSchemaTab.Padding = new Padding(3);
            navDbSchemaTab.Size = new Size(228, 251);
            navDbSchemaTab.TabIndex = 0;
            navDbSchemaTab.Text = "Db Schema";
            navDbSchemaTab.UseVisualStyleBackColor = true;
            // 
            // MainNavigation
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(navMainPanel);
            Name = "MainNavigation";
            Size = new Size(242, 401);
            Load += MainNavigation_Load;
            navMainPanel.ResumeLayout(false);
            navMainPanel.PerformLayout();
            layoutConnectionPanel.ResumeLayout(false);
            layoutConnectionPanel.PerformLayout();
            layoutConnectionTable.ResumeLayout(false);
            layoutConnectionTable.PerformLayout();
            navTabControl.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel navMainPanel;
        private GroupBox layoutConnectionPanel;
        private TableLayoutPanel layoutConnectionTable;
        private TextBox serverNameData;
        private TextBox databaseNameData;
        private TabControl navTabControl;
        private TabPage navDomainTab;
        private TabPage navDbSchemaTab;
    }
}
