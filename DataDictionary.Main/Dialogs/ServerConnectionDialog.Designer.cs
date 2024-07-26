namespace DataDictionary.Main.Dialogs
{
    partial class ServerConnectionDialog
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
            components = new System.ComponentModel.Container();
            TableLayoutPanel serverConnectionButtonLayout;
            commandOk = new Button();
            validateCommand = new Button();
            cancelCommand = new Button();
            serverConnectionLayout = new TableLayoutPanel();
            databaseNameData = new Controls.ComboBoxData();
            serverNameData = new Controls.ComboBoxData();
            refreshDatabaseCommand = new Button();
            accountNameData = new Controls.TextBoxData();
            errorProvider = new ErrorProvider(components);
            serverConnectionButtonLayout = new TableLayoutPanel();
            serverConnectionButtonLayout.SuspendLayout();
            serverConnectionLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // serverConnectionButtonLayout
            // 
            serverConnectionButtonLayout.ColumnCount = 4;
            serverConnectionLayout.SetColumnSpan(serverConnectionButtonLayout, 2);
            serverConnectionButtonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            serverConnectionButtonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            serverConnectionButtonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            serverConnectionButtonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            serverConnectionButtonLayout.Controls.Add(commandOk, 0, 0);
            serverConnectionButtonLayout.Controls.Add(validateCommand, 1, 0);
            serverConnectionButtonLayout.Controls.Add(cancelCommand, 3, 0);
            serverConnectionButtonLayout.Dock = DockStyle.Fill;
            serverConnectionButtonLayout.Location = new Point(3, 157);
            serverConnectionButtonLayout.Name = "serverConnectionButtonLayout";
            serverConnectionButtonLayout.RowCount = 1;
            serverConnectionButtonLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            serverConnectionButtonLayout.Size = new Size(351, 31);
            serverConnectionButtonLayout.TabIndex = 4;
            // 
            // commandOk
            // 
            commandOk.DialogResult = DialogResult.OK;
            commandOk.Location = new Point(3, 3);
            commandOk.Name = "commandOk";
            commandOk.Size = new Size(75, 23);
            commandOk.TabIndex = 0;
            commandOk.Text = "Ok";
            commandOk.UseVisualStyleBackColor = true;
            // 
            // validateCommand
            // 
            validateCommand.Image = Properties.Resources.StatusInformation;
            validateCommand.Location = new Point(90, 3);
            validateCommand.Name = "validateCommand";
            validateCommand.Size = new Size(75, 23);
            validateCommand.TabIndex = 1;
            validateCommand.Text = "Validate";
            validateCommand.TextImageRelation = TextImageRelation.TextBeforeImage;
            validateCommand.UseVisualStyleBackColor = true;
            validateCommand.Click += validateCommand_Click;
            // 
            // cancelCommand
            // 
            cancelCommand.DialogResult = DialogResult.Cancel;
            cancelCommand.Location = new Point(264, 3);
            cancelCommand.Name = "cancelCommand";
            cancelCommand.Size = new Size(75, 23);
            cancelCommand.TabIndex = 2;
            cancelCommand.Text = "Cancel";
            cancelCommand.UseVisualStyleBackColor = true;
            // 
            // serverConnectionLayout
            // 
            serverConnectionLayout.ColumnCount = 2;
            serverConnectionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            serverConnectionLayout.ColumnStyles.Add(new ColumnStyle());
            serverConnectionLayout.Controls.Add(databaseNameData, 0, 2);
            serverConnectionLayout.Controls.Add(serverNameData, 0, 1);
            serverConnectionLayout.Controls.Add(refreshDatabaseCommand, 1, 2);
            serverConnectionLayout.Controls.Add(accountNameData, 0, 0);
            serverConnectionLayout.Controls.Add(serverConnectionButtonLayout, 0, 3);
            serverConnectionLayout.Dock = DockStyle.Fill;
            serverConnectionLayout.Location = new Point(0, 0);
            serverConnectionLayout.Name = "serverConnectionLayout";
            serverConnectionLayout.RowCount = 4;
            serverConnectionLayout.RowStyles.Add(new RowStyle());
            serverConnectionLayout.RowStyles.Add(new RowStyle());
            serverConnectionLayout.RowStyles.Add(new RowStyle());
            serverConnectionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            serverConnectionLayout.Size = new Size(357, 191);
            serverConnectionLayout.TabIndex = 0;
            // 
            // databaseNameData
            // 
            databaseNameData.AutoSize = true;
            databaseNameData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            databaseNameData.Dock = DockStyle.Fill;
            databaseNameData.DropDownStyle = ComboBoxStyle.DropDown;
            databaseNameData.HeaderText = "Database Name";
            databaseNameData.Location = new Point(3, 105);
            databaseNameData.Name = "databaseNameData";
            databaseNameData.ReadOnly = false;
            databaseNameData.Size = new Size(323, 46);
            databaseNameData.TabIndex = 1;
            // 
            // serverNameData
            // 
            serverNameData.AutoSize = true;
            serverNameData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            serverConnectionLayout.SetColumnSpan(serverNameData, 2);
            serverNameData.Dock = DockStyle.Fill;
            serverNameData.DropDownStyle = ComboBoxStyle.DropDown;
            serverNameData.HeaderText = "Server Name";
            serverNameData.Location = new Point(3, 53);
            serverNameData.Name = "serverNameData";
            serverNameData.ReadOnly = false;
            serverNameData.Size = new Size(351, 46);
            serverNameData.TabIndex = 0;
            serverNameData.SelectedIndexChanged += serverNameData_SelectedIndexChanged;
            // 
            // refreshDatabaseCommand
            // 
            refreshDatabaseCommand.AutoSize = true;
            refreshDatabaseCommand.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            refreshDatabaseCommand.Dock = DockStyle.Bottom;
            refreshDatabaseCommand.Image = Properties.Resources.Refresh;
            refreshDatabaseCommand.Location = new Point(332, 129);
            refreshDatabaseCommand.Name = "refreshDatabaseCommand";
            refreshDatabaseCommand.Size = new Size(22, 22);
            refreshDatabaseCommand.TabIndex = 2;
            refreshDatabaseCommand.TextImageRelation = TextImageRelation.ImageBeforeText;
            refreshDatabaseCommand.UseVisualStyleBackColor = true;
            refreshDatabaseCommand.Click += refreshDatabaseCommand_Click;
            // 
            // accountNameData
            // 
            accountNameData.AutoSize = true;
            serverConnectionLayout.SetColumnSpan(accountNameData, 2);
            accountNameData.Dock = DockStyle.Fill;
            accountNameData.HeaderText = "Account Name";
            accountNameData.Location = new Point(3, 3);
            accountNameData.Multiline = false;
            accountNameData.Name = "accountNameData";
            accountNameData.ReadOnly = true;
            accountNameData.Size = new Size(351, 44);
            accountNameData.TabIndex = 3;
            accountNameData.WordWrap = true;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // ServerConnectionDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(357, 191);
            Controls.Add(serverConnectionLayout);
            HelpButton = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ServerConnectionDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Server Connection";
            HelpButtonClicked += ServerConnectionDialog_HelpButtonClicked;
            Load += ServerConnectionDialog_Load;
            serverConnectionButtonLayout.ResumeLayout(false);
            serverConnectionLayout.ResumeLayout(false);
            serverConnectionLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Controls.ComboBoxData databaseNameData;
        private Controls.ComboBoxData serverNameData;
        private Button refreshDatabaseCommand;
        private Controls.TextBoxData accountNameData;
        private ErrorProvider errorProvider;
        private Button commandOk;
        private Button validateCommand;
        private Button cancelCommand;
        private TableLayoutPanel serverConnectionLayout;
    }
}