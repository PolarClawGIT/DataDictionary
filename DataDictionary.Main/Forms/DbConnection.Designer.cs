namespace DataDictionary.Main.Forms
{
    partial class DbConnection
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            TableLayoutPanel dbConnectionLayout;
            GroupBox dbConnectionCurrentLayout;
            TableLayoutPanel dbConnectionCurrentGridLayout;
            Label serverNameHeader;
            GroupBox authencationLayout;
            Label authenticationTypeLayout;
            TableLayoutPanel authenticationChoiceLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbConnection));
            dbConnectionsData = new DataGridView();
            dbConnectionsServerNameData = new DataGridViewTextBoxColumn();
            dbConnectionsDatabaseData = new DataGridViewTextBoxColumn();
            databaseNameHeader = new Label();
            serverNameData = new ComboBox();
            databaseNameData = new ComboBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            connectCommand = new Button();
            importCommand = new Button();
            removeCommand = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            label2 = new Label();
            serverUserNameData = new TextBox();
            label3 = new Label();
            serverUserPasswordData = new TextBox();
            authenticateWindows = new RadioButton();
            authenticateDbServer = new RadioButton();
            dbConnectionLayout = new TableLayoutPanel();
            dbConnectionCurrentLayout = new GroupBox();
            dbConnectionCurrentGridLayout = new TableLayoutPanel();
            serverNameHeader = new Label();
            authencationLayout = new GroupBox();
            authenticationTypeLayout = new Label();
            authenticationChoiceLayout = new TableLayoutPanel();
            dbConnectionLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dbConnectionsData).BeginInit();
            dbConnectionCurrentLayout.SuspendLayout();
            dbConnectionCurrentGridLayout.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            authencationLayout.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            authenticationChoiceLayout.SuspendLayout();
            SuspendLayout();
            // 
            // dbConnectionLayout
            // 
            dbConnectionLayout.ColumnCount = 1;
            dbConnectionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            dbConnectionLayout.Controls.Add(dbConnectionsData, 0, 1);
            dbConnectionLayout.Controls.Add(dbConnectionCurrentLayout, 0, 0);
            dbConnectionLayout.Dock = DockStyle.Fill;
            dbConnectionLayout.Location = new Point(0, 0);
            dbConnectionLayout.Name = "dbConnectionLayout";
            dbConnectionLayout.RowCount = 2;
            dbConnectionLayout.RowStyles.Add(new RowStyle());
            dbConnectionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            dbConnectionLayout.Size = new Size(418, 430);
            dbConnectionLayout.TabIndex = 0;
            // 
            // dbConnectionsData
            // 
            dbConnectionsData.AllowUserToAddRows = false;
            dbConnectionsData.AllowUserToDeleteRows = false;
            dbConnectionsData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dbConnectionsData.Columns.AddRange(new DataGridViewColumn[] { dbConnectionsServerNameData, dbConnectionsDatabaseData });
            dbConnectionsData.Dock = DockStyle.Fill;
            dbConnectionsData.Location = new Point(3, 273);
            dbConnectionsData.MultiSelect = false;
            dbConnectionsData.Name = "dbConnectionsData";
            dbConnectionsData.ReadOnly = true;
            dbConnectionsData.RowTemplate.Height = 25;
            dbConnectionsData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dbConnectionsData.Size = new Size(412, 154);
            dbConnectionsData.TabIndex = 0;
            dbConnectionsData.SelectionChanged += dbConnectionsData_SelectionChanged;
            // 
            // dbConnectionsServerNameData
            // 
            dbConnectionsServerNameData.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dbConnectionsServerNameData.DataPropertyName = "ServerName";
            dbConnectionsServerNameData.HeaderText = "Server Name";
            dbConnectionsServerNameData.Name = "dbConnectionsServerNameData";
            dbConnectionsServerNameData.ReadOnly = true;
            // 
            // dbConnectionsDatabaseData
            // 
            dbConnectionsDatabaseData.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dbConnectionsDatabaseData.DataPropertyName = "DatabaseName";
            dbConnectionsDatabaseData.HeaderText = "Database Name";
            dbConnectionsDatabaseData.Name = "dbConnectionsDatabaseData";
            dbConnectionsDatabaseData.ReadOnly = true;
            // 
            // dbConnectionCurrentLayout
            // 
            dbConnectionCurrentLayout.AutoSize = true;
            dbConnectionCurrentLayout.Controls.Add(dbConnectionCurrentGridLayout);
            dbConnectionCurrentLayout.Dock = DockStyle.Fill;
            dbConnectionCurrentLayout.Location = new Point(3, 3);
            dbConnectionCurrentLayout.Name = "dbConnectionCurrentLayout";
            dbConnectionCurrentLayout.Size = new Size(412, 264);
            dbConnectionCurrentLayout.TabIndex = 1;
            dbConnectionCurrentLayout.TabStop = false;
            dbConnectionCurrentLayout.Text = "Connection";
            // 
            // dbConnectionCurrentGridLayout
            // 
            dbConnectionCurrentGridLayout.AutoSize = true;
            dbConnectionCurrentGridLayout.ColumnCount = 1;
            dbConnectionCurrentGridLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            dbConnectionCurrentGridLayout.Controls.Add(serverNameHeader, 0, 0);
            dbConnectionCurrentGridLayout.Controls.Add(databaseNameHeader, 0, 2);
            dbConnectionCurrentGridLayout.Controls.Add(serverNameData, 0, 1);
            dbConnectionCurrentGridLayout.Controls.Add(databaseNameData, 0, 3);
            dbConnectionCurrentGridLayout.Controls.Add(tableLayoutPanel1, 0, 5);
            dbConnectionCurrentGridLayout.Controls.Add(authencationLayout, 0, 4);
            dbConnectionCurrentGridLayout.Dock = DockStyle.Fill;
            dbConnectionCurrentGridLayout.Location = new Point(3, 19);
            dbConnectionCurrentGridLayout.Name = "dbConnectionCurrentGridLayout";
            dbConnectionCurrentGridLayout.RowCount = 6;
            dbConnectionCurrentGridLayout.RowStyles.Add(new RowStyle());
            dbConnectionCurrentGridLayout.RowStyles.Add(new RowStyle());
            dbConnectionCurrentGridLayout.RowStyles.Add(new RowStyle());
            dbConnectionCurrentGridLayout.RowStyles.Add(new RowStyle());
            dbConnectionCurrentGridLayout.RowStyles.Add(new RowStyle());
            dbConnectionCurrentGridLayout.RowStyles.Add(new RowStyle());
            dbConnectionCurrentGridLayout.Size = new Size(406, 242);
            dbConnectionCurrentGridLayout.TabIndex = 0;
            // 
            // serverNameHeader
            // 
            serverNameHeader.AutoSize = true;
            serverNameHeader.Location = new Point(3, 0);
            serverNameHeader.Name = "serverNameHeader";
            serverNameHeader.Size = new Size(74, 15);
            serverNameHeader.TabIndex = 0;
            serverNameHeader.Text = "Server Name";
            // 
            // databaseNameHeader
            // 
            databaseNameHeader.AutoSize = true;
            databaseNameHeader.Location = new Point(3, 44);
            databaseNameHeader.Name = "databaseNameHeader";
            databaseNameHeader.Size = new Size(90, 15);
            databaseNameHeader.TabIndex = 1;
            databaseNameHeader.Text = "Database Name";
            // 
            // serverNameData
            // 
            serverNameData.Dock = DockStyle.Fill;
            serverNameData.FormattingEnabled = true;
            serverNameData.Location = new Point(3, 18);
            serverNameData.Name = "serverNameData";
            serverNameData.Size = new Size(400, 23);
            serverNameData.TabIndex = 2;
            serverNameData.SelectedIndexChanged += serverNameData_SelectedIndexChanged;
            serverNameData.Validated += serverNameData_Validated;
            // 
            // databaseNameData
            // 
            databaseNameData.Dock = DockStyle.Fill;
            databaseNameData.FormattingEnabled = true;
            databaseNameData.Location = new Point(3, 62);
            databaseNameData.Name = "databaseNameData";
            databaseNameData.Size = new Size(400, 23);
            databaseNameData.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(connectCommand, 0, 0);
            tableLayoutPanel1.Controls.Add(importCommand, 1, 0);
            tableLayoutPanel1.Controls.Add(removeCommand, 3, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 208);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(400, 31);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // connectCommand
            // 
            connectCommand.AutoSize = true;
            connectCommand.Location = new Point(3, 3);
            connectCommand.Name = "connectCommand";
            connectCommand.Size = new Size(75, 25);
            connectCommand.TabIndex = 0;
            connectCommand.Text = "Connect";
            connectCommand.UseVisualStyleBackColor = true;
            connectCommand.Click += connectCommand_Click;
            // 
            // importCommand
            // 
            importCommand.AutoSize = true;
            importCommand.Location = new Point(84, 3);
            importCommand.Name = "importCommand";
            importCommand.Size = new Size(97, 25);
            importCommand.TabIndex = 1;
            importCommand.Text = "Refresh/Import";
            importCommand.UseVisualStyleBackColor = true;
            importCommand.Click += importCommand_Click;
            // 
            // removeCommand
            // 
            removeCommand.AutoSize = true;
            removeCommand.Location = new Point(322, 3);
            removeCommand.Name = "removeCommand";
            removeCommand.Size = new Size(75, 25);
            removeCommand.TabIndex = 3;
            removeCommand.Text = "Remove";
            removeCommand.UseVisualStyleBackColor = true;
            removeCommand.Click += removeCommand_Click;
            // 
            // authencationLayout
            // 
            authencationLayout.AutoSize = true;
            authencationLayout.Controls.Add(tableLayoutPanel2);
            authencationLayout.Dock = DockStyle.Fill;
            authencationLayout.Location = new Point(3, 91);
            authencationLayout.Name = "authencationLayout";
            authencationLayout.Size = new Size(400, 111);
            authencationLayout.TabIndex = 5;
            authencationLayout.TabStop = false;
            authencationLayout.Text = "Authentication";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(authenticationTypeLayout, 0, 0);
            tableLayoutPanel2.Controls.Add(label2, 0, 1);
            tableLayoutPanel2.Controls.Add(serverUserNameData, 1, 1);
            tableLayoutPanel2.Controls.Add(label3, 0, 2);
            tableLayoutPanel2.Controls.Add(serverUserPasswordData, 1, 2);
            tableLayoutPanel2.Controls.Add(authenticationChoiceLayout, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 19);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(394, 89);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // authenticationTypeLayout
            // 
            authenticationTypeLayout.AutoSize = true;
            authenticationTypeLayout.Location = new Point(3, 0);
            authenticationTypeLayout.Name = "authenticationTypeLayout";
            authenticationTypeLayout.Size = new Size(31, 15);
            authenticationTypeLayout.TabIndex = 0;
            authenticationTypeLayout.Text = "Type";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 31);
            label2.Name = "label2";
            label2.Size = new Size(65, 15);
            label2.TabIndex = 2;
            label2.Text = "User Name";
            // 
            // serverUserNameData
            // 
            serverUserNameData.Dock = DockStyle.Fill;
            serverUserNameData.Location = new Point(74, 34);
            serverUserNameData.Name = "serverUserNameData";
            serverUserNameData.Size = new Size(317, 23);
            serverUserNameData.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 60);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 4;
            label3.Text = "Password";
            // 
            // serverUserPasswordData
            // 
            serverUserPasswordData.Dock = DockStyle.Fill;
            serverUserPasswordData.Location = new Point(74, 63);
            serverUserPasswordData.Name = "serverUserPasswordData";
            serverUserPasswordData.PasswordChar = '*';
            serverUserPasswordData.Size = new Size(317, 23);
            serverUserPasswordData.TabIndex = 5;
            // 
            // authenticationChoiceLayout
            // 
            authenticationChoiceLayout.AutoSize = true;
            authenticationChoiceLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            authenticationChoiceLayout.ColumnCount = 2;
            authenticationChoiceLayout.ColumnStyles.Add(new ColumnStyle());
            authenticationChoiceLayout.ColumnStyles.Add(new ColumnStyle());
            authenticationChoiceLayout.Controls.Add(authenticateWindows, 0, 0);
            authenticationChoiceLayout.Controls.Add(authenticateDbServer, 1, 0);
            authenticationChoiceLayout.Dock = DockStyle.Fill;
            authenticationChoiceLayout.Location = new Point(74, 3);
            authenticationChoiceLayout.Name = "authenticationChoiceLayout";
            authenticationChoiceLayout.RowCount = 1;
            authenticationChoiceLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            authenticationChoiceLayout.Size = new Size(317, 25);
            authenticationChoiceLayout.TabIndex = 6;
            // 
            // authenticateWindows
            // 
            authenticateWindows.AutoSize = true;
            authenticateWindows.Checked = true;
            authenticateWindows.Location = new Point(3, 3);
            authenticateWindows.Name = "authenticateWindows";
            authenticateWindows.Size = new Size(74, 19);
            authenticateWindows.TabIndex = 0;
            authenticateWindows.TabStop = true;
            authenticateWindows.Text = "Windows";
            authenticateWindows.UseVisualStyleBackColor = true;
            authenticateWindows.CheckedChanged += authenticateWindows_CheckedChanged;
            // 
            // authenticateDbServer
            // 
            authenticateDbServer.AutoSize = true;
            authenticateDbServer.Location = new Point(83, 3);
            authenticateDbServer.Name = "authenticateDbServer";
            authenticateDbServer.Size = new Size(75, 19);
            authenticateDbServer.TabIndex = 1;
            authenticateDbServer.Text = "Db Server";
            authenticateDbServer.UseVisualStyleBackColor = true;
            // 
            // DbConnection
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(418, 430);
            Controls.Add(dbConnectionLayout);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "DbConnection";
            Text = "Database Connections";
            Load += DbConnection_Load;
            dbConnectionLayout.ResumeLayout(false);
            dbConnectionLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dbConnectionsData).EndInit();
            dbConnectionCurrentLayout.ResumeLayout(false);
            dbConnectionCurrentLayout.PerformLayout();
            dbConnectionCurrentGridLayout.ResumeLayout(false);
            dbConnectionCurrentGridLayout.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            authencationLayout.ResumeLayout(false);
            authencationLayout.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            authenticationChoiceLayout.ResumeLayout(false);
            authenticationChoiceLayout.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel dbConnectionLayout;
        private DataGridView dbConnectionsData;
        private Label databaseNameHeader;
        private ComboBox serverNameData;
        private ComboBox databaseNameData;
        private TableLayoutPanel tableLayoutPanel1;
        private Button connectCommand;
        private Button importCommand;
        private Button button3;
        private Button removeCommand;
        private GroupBox authencationLayout;
        private TableLayoutPanel tableLayoutPanel2;
        private Label authenticationTypeLayout;
        private Label label2;
        private TextBox serverUserNameData;
        private Label label3;
        private TextBox serverUserPasswordData;
        private TableLayoutPanel authenticationChoiceLayout;
        private RadioButton authenticateWindows;
        private RadioButton authenticateDbServer;
        private DataGridViewTextBoxColumn dbConnectionsServerNameData;
        private DataGridViewTextBoxColumn dbConnectionsDatabaseData;
    }
}