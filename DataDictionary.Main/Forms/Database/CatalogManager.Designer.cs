namespace DataDictionary.Main.Forms.Database
{
    partial class CatalogManager
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
            TableLayoutPanel catalogManagerLayout;
            TableLayoutPanel libraryManagerLayoutCheckBoxs;
            inModelData = new CheckBox();
            inDatabaseData = new CheckBox();
            catalogNavigation = new DataGridView();
            catalogTitleData = new Controls.TextBoxData();
            catalogDescriptionData = new Controls.TextBoxData();
            sourceDateData = new Controls.TextBoxData();
            sourceServerNameData = new Controls.TextBoxData();
            sourceDatabaseNameData = new Controls.TextBoxData();
            catalogBinding = new BindingSource(components);
            catalogTitleColumn = new DataGridViewTextBoxColumn();
            databaseNameColumn = new DataGridViewTextBoxColumn();
            inModelColumn = new DataGridViewCheckBoxColumn();
            inDatabase = new DataGridViewCheckBoxColumn();
            catalogManagerLayout = new TableLayoutPanel();
            libraryManagerLayoutCheckBoxs = new TableLayoutPanel();
            catalogManagerLayout.SuspendLayout();
            libraryManagerLayoutCheckBoxs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)catalogNavigation).BeginInit();
            ((System.ComponentModel.ISupportInitialize)catalogBinding).BeginInit();
            SuspendLayout();
            // 
            // catalogManagerLayout
            // 
            catalogManagerLayout.ColumnCount = 2;
            catalogManagerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.6666641F));
            catalogManagerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            catalogManagerLayout.Controls.Add(libraryManagerLayoutCheckBoxs, 1, 3);
            catalogManagerLayout.Controls.Add(catalogNavigation, 0, 0);
            catalogManagerLayout.Controls.Add(catalogTitleData, 0, 1);
            catalogManagerLayout.Controls.Add(catalogDescriptionData, 0, 2);
            catalogManagerLayout.Controls.Add(sourceDateData, 1, 4);
            catalogManagerLayout.Controls.Add(sourceServerNameData, 0, 3);
            catalogManagerLayout.Controls.Add(sourceDatabaseNameData, 0, 4);
            catalogManagerLayout.Dock = DockStyle.Fill;
            catalogManagerLayout.Location = new Point(0, 25);
            catalogManagerLayout.Name = "catalogManagerLayout";
            catalogManagerLayout.RowCount = 5;
            catalogManagerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            catalogManagerLayout.RowStyles.Add(new RowStyle());
            catalogManagerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            catalogManagerLayout.RowStyles.Add(new RowStyle());
            catalogManagerLayout.RowStyles.Add(new RowStyle());
            catalogManagerLayout.Size = new Size(509, 490);
            catalogManagerLayout.TabIndex = 1;
            // 
            // libraryManagerLayoutCheckBoxs
            // 
            libraryManagerLayoutCheckBoxs.AutoSize = true;
            libraryManagerLayoutCheckBoxs.ColumnCount = 1;
            libraryManagerLayoutCheckBoxs.ColumnStyles.Add(new ColumnStyle());
            libraryManagerLayoutCheckBoxs.Controls.Add(inModelData, 0, 0);
            libraryManagerLayoutCheckBoxs.Controls.Add(inDatabaseData, 0, 1);
            libraryManagerLayoutCheckBoxs.Dock = DockStyle.Fill;
            libraryManagerLayoutCheckBoxs.Location = new Point(342, 386);
            libraryManagerLayoutCheckBoxs.Name = "libraryManagerLayoutCheckBoxs";
            libraryManagerLayoutCheckBoxs.RowCount = 2;
            libraryManagerLayoutCheckBoxs.RowStyles.Add(new RowStyle());
            libraryManagerLayoutCheckBoxs.RowStyles.Add(new RowStyle());
            libraryManagerLayoutCheckBoxs.Size = new Size(164, 50);
            libraryManagerLayoutCheckBoxs.TabIndex = 9;
            // 
            // inModelData
            // 
            inModelData.AutoSize = true;
            inModelData.Location = new Point(3, 3);
            inModelData.Name = "inModelData";
            inModelData.Size = new Size(73, 19);
            inModelData.TabIndex = 0;
            inModelData.Text = "In Model";
            inModelData.UseVisualStyleBackColor = true;
            // 
            // inDatabaseData
            // 
            inDatabaseData.AutoSize = true;
            inDatabaseData.Location = new Point(3, 28);
            inDatabaseData.Name = "inDatabaseData";
            inDatabaseData.Size = new Size(87, 19);
            inDatabaseData.TabIndex = 1;
            inDatabaseData.Text = "In Database";
            inDatabaseData.UseVisualStyleBackColor = true;
            // 
            // catalogNavigation
            // 
            catalogNavigation.AllowUserToAddRows = false;
            catalogNavigation.AllowUserToDeleteRows = false;
            catalogNavigation.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            catalogNavigation.Columns.AddRange(new DataGridViewColumn[] { catalogTitleColumn, databaseNameColumn, inModelColumn, inDatabase });
            catalogManagerLayout.SetColumnSpan(catalogNavigation, 2);
            catalogNavigation.Dock = DockStyle.Fill;
            catalogNavigation.Location = new Point(3, 3);
            catalogNavigation.Name = "catalogNavigation";
            catalogNavigation.RowTemplate.Height = 25;
            catalogNavigation.Size = new Size(503, 227);
            catalogNavigation.TabIndex = 0;
            // 
            // catalogTitleData
            // 
            catalogTitleData.AutoSize = true;
            catalogManagerLayout.SetColumnSpan(catalogTitleData, 2);
            catalogTitleData.Dock = DockStyle.Fill;
            catalogTitleData.HeaderText = "Catalog Title";
            catalogTitleData.Location = new Point(3, 236);
            catalogTitleData.Multiline = false;
            catalogTitleData.Name = "catalogTitleData";
            catalogTitleData.ReadOnly = false;
            catalogTitleData.Size = new Size(503, 44);
            catalogTitleData.TabIndex = 1;
            // 
            // catalogDescriptionData
            // 
            catalogDescriptionData.AutoSize = true;
            catalogManagerLayout.SetColumnSpan(catalogDescriptionData, 2);
            catalogDescriptionData.Dock = DockStyle.Fill;
            catalogDescriptionData.HeaderText = "Catalog Description";
            catalogDescriptionData.Location = new Point(3, 286);
            catalogDescriptionData.Multiline = true;
            catalogDescriptionData.Name = "catalogDescriptionData";
            catalogDescriptionData.ReadOnly = false;
            catalogDescriptionData.Size = new Size(503, 94);
            catalogDescriptionData.TabIndex = 2;
            // 
            // sourceDateData
            // 
            sourceDateData.AutoSize = true;
            sourceDateData.Dock = DockStyle.Fill;
            sourceDateData.HeaderText = "Source Date";
            sourceDateData.Location = new Point(342, 442);
            sourceDateData.Multiline = false;
            sourceDateData.Name = "sourceDateData";
            sourceDateData.ReadOnly = true;
            sourceDateData.Size = new Size(164, 45);
            sourceDateData.TabIndex = 10;
            // 
            // sourceServerNameData
            // 
            sourceServerNameData.AutoSize = true;
            sourceServerNameData.Dock = DockStyle.Fill;
            sourceServerNameData.HeaderText = "Source Server Name";
            sourceServerNameData.Location = new Point(3, 386);
            sourceServerNameData.Multiline = false;
            sourceServerNameData.Name = "sourceServerNameData";
            sourceServerNameData.ReadOnly = true;
            sourceServerNameData.Size = new Size(333, 50);
            sourceServerNameData.TabIndex = 11;
            // 
            // sourceDatabaseNameData
            // 
            sourceDatabaseNameData.AutoSize = true;
            sourceDatabaseNameData.Dock = DockStyle.Fill;
            sourceDatabaseNameData.HeaderText = "Source Database Name";
            sourceDatabaseNameData.Location = new Point(3, 442);
            sourceDatabaseNameData.Multiline = false;
            sourceDatabaseNameData.Name = "sourceDatabaseNameData";
            sourceDatabaseNameData.ReadOnly = true;
            sourceDatabaseNameData.Size = new Size(333, 45);
            sourceDatabaseNameData.TabIndex = 12;
            // 
            // catalogTitleColumn
            // 
            catalogTitleColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            catalogTitleColumn.DataPropertyName = "CatalogTitle";
            catalogTitleColumn.HeaderText = "Catalog Title";
            catalogTitleColumn.MinimumWidth = 150;
            catalogTitleColumn.Name = "catalogTitleColumn";
            // 
            // databaseNameColumn
            // 
            databaseNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            databaseNameColumn.DataPropertyName = "SourceDatabaseName";
            databaseNameColumn.HeaderText = "Database Name";
            databaseNameColumn.MinimumWidth = 150;
            databaseNameColumn.Name = "databaseNameColumn";
            databaseNameColumn.ReadOnly = true;
            // 
            // inModelColumn
            // 
            inModelColumn.DataPropertyName = "InModel";
            inModelColumn.HeaderText = "In Model";
            inModelColumn.Name = "inModelColumn";
            // 
            // inDatabase
            // 
            inDatabase.DataPropertyName = "InDatabase";
            inDatabase.HeaderText = "In Database";
            inDatabase.Name = "inDatabase";
            // 
            // CatalogManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(509, 515);
            Controls.Add(catalogManagerLayout);
            Name = "CatalogManager";
            Text = "Catalog Manager";
            Load += CatalogManager_Load;
            Controls.SetChildIndex(catalogManagerLayout, 0);
            catalogManagerLayout.ResumeLayout(false);
            catalogManagerLayout.PerformLayout();
            libraryManagerLayoutCheckBoxs.ResumeLayout(false);
            libraryManagerLayoutCheckBoxs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)catalogNavigation).EndInit();
            ((System.ComponentModel.ISupportInitialize)catalogBinding).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView catalogNavigation;
        private BindingSource catalogBinding;
        private Controls.TextBoxData catalogTitleData;
        private Controls.TextBoxData catalogDescriptionData;
        private CheckBox inModelData;
        private CheckBox inDatabaseData;
        private Controls.TextBoxData sourceDateData;
        private Controls.TextBoxData sourceServerNameData;
        private Controls.TextBoxData sourceDatabaseNameData;
        private DataGridViewTextBoxColumn catalogTitleColumn;
        private DataGridViewTextBoxColumn databaseNameColumn;
        private DataGridViewCheckBoxColumn inModelColumn;
        private DataGridViewCheckBoxColumn inDatabase;
    }
}