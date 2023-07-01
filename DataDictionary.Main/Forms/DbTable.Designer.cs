namespace DataDictionary.Main.Forms
{
    partial class DbTable
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
            TableLayoutPanel dbTableLayout;
            TableLayoutPanel dbTableDetailLayout;
            Label tableTypeLayout;
            Label schemaNameLayout;
            Label tableNameLayout;
            TabControl tableDetailLayout;
            TabPage extendedPropertiesTab;
            TabPage columnsTab;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbTable));
            tableTypeData = new TextBox();
            tableIsSystemData = new CheckBox();
            catalogNameLayout = new Label();
            catalogNameData = new TextBox();
            schemaNameData = new TextBox();
            tableNameData = new TextBox();
            extendedPropertiesData = new DataGridView();
            propertyNameData = new DataGridViewTextBoxColumn();
            propertyValueData = new DataGridViewTextBoxColumn();
            tableColumnsData = new DataGridView();
            errorProvider = new ErrorProvider(components);
            dbTableLayout = new TableLayoutPanel();
            dbTableDetailLayout = new TableLayoutPanel();
            tableTypeLayout = new Label();
            schemaNameLayout = new Label();
            tableNameLayout = new Label();
            tableDetailLayout = new TabControl();
            extendedPropertiesTab = new TabPage();
            columnsTab = new TabPage();
            dbTableLayout.SuspendLayout();
            dbTableDetailLayout.SuspendLayout();
            tableDetailLayout.SuspendLayout();
            extendedPropertiesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)extendedPropertiesData).BeginInit();
            columnsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tableColumnsData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // dbTableLayout
            // 
            dbTableLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            dbTableLayout.ColumnCount = 1;
            dbTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            dbTableLayout.Controls.Add(dbTableDetailLayout, 0, 6);
            dbTableLayout.Controls.Add(catalogNameLayout, 0, 0);
            dbTableLayout.Controls.Add(schemaNameLayout, 0, 2);
            dbTableLayout.Controls.Add(tableNameLayout, 0, 4);
            dbTableLayout.Controls.Add(catalogNameData, 0, 1);
            dbTableLayout.Controls.Add(schemaNameData, 0, 3);
            dbTableLayout.Controls.Add(tableNameData, 0, 5);
            dbTableLayout.Controls.Add(tableDetailLayout, 0, 7);
            dbTableLayout.Dock = DockStyle.Fill;
            dbTableLayout.Location = new Point(0, 0);
            dbTableLayout.Name = "dbTableLayout";
            dbTableLayout.RowCount = 8;
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            dbTableLayout.Size = new Size(433, 383);
            dbTableLayout.TabIndex = 0;
            // 
            // dbTableDetailLayout
            // 
            dbTableDetailLayout.AutoSize = true;
            dbTableDetailLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            dbTableDetailLayout.ColumnCount = 2;
            dbTableDetailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            dbTableDetailLayout.ColumnStyles.Add(new ColumnStyle());
            dbTableDetailLayout.Controls.Add(tableTypeData, 0, 1);
            dbTableDetailLayout.Controls.Add(tableTypeLayout, 0, 0);
            dbTableDetailLayout.Controls.Add(tableIsSystemData, 2, 1);
            dbTableDetailLayout.Dock = DockStyle.Fill;
            dbTableDetailLayout.Location = new Point(3, 135);
            dbTableDetailLayout.Name = "dbTableDetailLayout";
            dbTableDetailLayout.RowCount = 2;
            dbTableDetailLayout.RowStyles.Add(new RowStyle());
            dbTableDetailLayout.RowStyles.Add(new RowStyle());
            dbTableDetailLayout.Size = new Size(427, 44);
            dbTableDetailLayout.TabIndex = 1;
            // 
            // tableTypeData
            // 
            tableTypeData.Dock = DockStyle.Fill;
            tableTypeData.Location = new Point(3, 18);
            tableTypeData.Name = "tableTypeData";
            tableTypeData.ReadOnly = true;
            tableTypeData.Size = new Size(340, 23);
            tableTypeData.TabIndex = 0;
            // 
            // tableTypeLayout
            // 
            tableTypeLayout.AutoSize = true;
            tableTypeLayout.Location = new Point(3, 0);
            tableTypeLayout.Name = "tableTypeLayout";
            tableTypeLayout.Size = new Size(61, 15);
            tableTypeLayout.TabIndex = 1;
            tableTypeLayout.Text = "Table Type";
            // 
            // tableIsSystemData
            // 
            tableIsSystemData.AutoCheck = false;
            tableIsSystemData.AutoSize = true;
            tableIsSystemData.Location = new Point(349, 18);
            tableIsSystemData.Name = "tableIsSystemData";
            tableIsSystemData.Size = new Size(75, 19);
            tableIsSystemData.TabIndex = 3;
            tableIsSystemData.Text = "Is System";
            tableIsSystemData.UseVisualStyleBackColor = true;
            // 
            // catalogNameLayout
            // 
            catalogNameLayout.AutoSize = true;
            catalogNameLayout.Location = new Point(3, 0);
            catalogNameLayout.Name = "catalogNameLayout";
            catalogNameLayout.Size = new Size(83, 15);
            catalogNameLayout.TabIndex = 0;
            catalogNameLayout.Text = "Catalog Name";
            // 
            // schemaNameLayout
            // 
            schemaNameLayout.AutoSize = true;
            schemaNameLayout.Location = new Point(3, 44);
            schemaNameLayout.Name = "schemaNameLayout";
            schemaNameLayout.Size = new Size(84, 15);
            schemaNameLayout.TabIndex = 1;
            schemaNameLayout.Text = "Schema Name";
            // 
            // tableNameLayout
            // 
            tableNameLayout.AutoSize = true;
            tableNameLayout.Location = new Point(3, 88);
            tableNameLayout.Name = "tableNameLayout";
            tableNameLayout.Size = new Size(77, 15);
            tableNameLayout.TabIndex = 2;
            tableNameLayout.Text = "Object Name";
            // 
            // catalogNameData
            // 
            catalogNameData.Dock = DockStyle.Fill;
            catalogNameData.Location = new Point(3, 18);
            catalogNameData.Name = "catalogNameData";
            catalogNameData.ReadOnly = true;
            catalogNameData.Size = new Size(427, 23);
            catalogNameData.TabIndex = 4;
            // 
            // schemaNameData
            // 
            schemaNameData.Dock = DockStyle.Fill;
            schemaNameData.Location = new Point(3, 62);
            schemaNameData.Name = "schemaNameData";
            schemaNameData.ReadOnly = true;
            schemaNameData.Size = new Size(427, 23);
            schemaNameData.TabIndex = 5;
            // 
            // tableNameData
            // 
            tableNameData.Dock = DockStyle.Fill;
            tableNameData.Location = new Point(3, 106);
            tableNameData.Name = "tableNameData";
            tableNameData.ReadOnly = true;
            tableNameData.Size = new Size(427, 23);
            tableNameData.TabIndex = 6;
            // 
            // tableDetailLayout
            // 
            tableDetailLayout.Controls.Add(extendedPropertiesTab);
            tableDetailLayout.Controls.Add(columnsTab);
            tableDetailLayout.Dock = DockStyle.Fill;
            tableDetailLayout.Location = new Point(3, 185);
            tableDetailLayout.Name = "tableDetailLayout";
            tableDetailLayout.SelectedIndex = 0;
            tableDetailLayout.Size = new Size(427, 195);
            tableDetailLayout.TabIndex = 7;
            // 
            // extendedPropertiesTab
            // 
            extendedPropertiesTab.Controls.Add(extendedPropertiesData);
            extendedPropertiesTab.Location = new Point(4, 24);
            extendedPropertiesTab.Name = "extendedPropertiesTab";
            extendedPropertiesTab.Padding = new Padding(3);
            extendedPropertiesTab.Size = new Size(419, 167);
            extendedPropertiesTab.TabIndex = 0;
            extendedPropertiesTab.Text = "Extended Properties";
            extendedPropertiesTab.UseVisualStyleBackColor = true;
            // 
            // extendedPropertiesData
            // 
            extendedPropertiesData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            extendedPropertiesData.Columns.AddRange(new DataGridViewColumn[] { propertyNameData, propertyValueData });
            extendedPropertiesData.Dock = DockStyle.Fill;
            extendedPropertiesData.Location = new Point(3, 3);
            extendedPropertiesData.Name = "extendedPropertiesData";
            extendedPropertiesData.RowTemplate.Height = 25;
            extendedPropertiesData.Size = new Size(413, 161);
            extendedPropertiesData.TabIndex = 5;
            // 
            // propertyNameData
            // 
            propertyNameData.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            propertyNameData.DataPropertyName = "PropertyName";
            propertyNameData.HeaderText = "Property Name";
            propertyNameData.Name = "propertyNameData";
            propertyNameData.Width = 112;
            // 
            // propertyValueData
            // 
            propertyValueData.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            propertyValueData.DataPropertyName = "PropertyValue";
            propertyValueData.HeaderText = "PropertyValue";
            propertyValueData.Name = "propertyValueData";
            // 
            // columnsTab
            // 
            columnsTab.Controls.Add(tableColumnsData);
            columnsTab.Location = new Point(4, 24);
            columnsTab.Name = "columnsTab";
            columnsTab.Padding = new Padding(3);
            columnsTab.Size = new Size(419, 167);
            columnsTab.TabIndex = 1;
            columnsTab.Text = "Columns";
            columnsTab.UseVisualStyleBackColor = true;
            // 
            // tableColumnsData
            // 
            tableColumnsData.AllowUserToAddRows = false;
            tableColumnsData.AllowUserToDeleteRows = false;
            tableColumnsData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tableColumnsData.Dock = DockStyle.Fill;
            tableColumnsData.Location = new Point(3, 3);
            tableColumnsData.Name = "tableColumnsData";
            tableColumnsData.RowTemplate.Height = 25;
            tableColumnsData.Size = new Size(413, 161);
            tableColumnsData.TabIndex = 0;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // DbTable
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(433, 383);
            Controls.Add(dbTableLayout);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "DbTable";
            Text = "Database Table";
            Load += DbTable_Load;
            dbTableLayout.ResumeLayout(false);
            dbTableLayout.PerformLayout();
            dbTableDetailLayout.ResumeLayout(false);
            dbTableDetailLayout.PerformLayout();
            tableDetailLayout.ResumeLayout(false);
            extendedPropertiesTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)extendedPropertiesData).EndInit();
            columnsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)tableColumnsData).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel dbTableLayout;
        private Label catalogNameLayout;
        private Label schemaNameLayout;
        private Label tableNameLayout;
        private TextBox catalogNameData;
        private TextBox schemaNameData;
        private TextBox tableNameData;
        private ErrorProvider errorProvider;
        private TableLayoutPanel dbTableDetailLayout;
        private TextBox tableTypeData;
        private Label tableTypeLayout;
        private CheckBox tableIsSystemData;
        private DataGridView extendedPropertiesData;
        private DataGridViewTextBoxColumn propertyNameData;
        private DataGridViewTextBoxColumn propertyValueData;
        private DataGridView tableColumnsData;
    }
}