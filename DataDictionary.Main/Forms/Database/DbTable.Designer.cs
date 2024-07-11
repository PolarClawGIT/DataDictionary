namespace DataDictionary.Main.Forms.Database
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
            TabControl tableDetailLayout;
            TabPage extendedPropertiesTab;
            TabPage columnsTab;
            TableLayoutPanel constraintLayout;
            isSystemData = new CheckBox();
            extendedPropertiesData = new DataGridView();
            propertyNameData = new DataGridViewTextBoxColumn();
            propertyValueData = new DataGridViewTextBoxColumn();
            tableColumnsData = new DataGridView();
            ColumnNameValue = new DataGridViewTextBoxColumn();
            DataTypeValue = new DataGridViewTextBoxColumn();
            IsNullableValue = new DataGridViewCheckBoxColumn();
            constraintTab = new TabPage();
            tableConstraintData = new DataGridView();
            ConstraintNameValue = new DataGridViewTextBoxColumn();
            ConstraintTypeValue = new DataGridViewTextBoxColumn();
            catalogNameData = new Controls.TextBoxData();
            schemaNameData = new Controls.TextBoxData();
            tableNameData = new Controls.TextBoxData();
            tableTypeData = new Controls.TextBoxData();
            errorProvider = new ErrorProvider(components);
            bindingTable = new BindingSource(components);
            bindingProperties = new BindingSource(components);
            bindingColumns = new BindingSource(components);
            bindingConstraints = new BindingSource(components);
            exportOptions = new ContextMenuStrip(components);
            exportAll = new ToolStripMenuItem();
            exportEntites = new ToolStripMenuItem();
            exportAttributes = new ToolStripMenuItem();
            dbTableLayout = new TableLayoutPanel();
            tableDetailLayout = new TabControl();
            extendedPropertiesTab = new TabPage();
            columnsTab = new TabPage();
            constraintLayout = new TableLayoutPanel();
            dbTableLayout.SuspendLayout();
            tableDetailLayout.SuspendLayout();
            extendedPropertiesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)extendedPropertiesData).BeginInit();
            columnsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tableColumnsData).BeginInit();
            constraintTab.SuspendLayout();
            constraintLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tableConstraintData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingProperties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingColumns).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingConstraints).BeginInit();
            exportOptions.SuspendLayout();
            SuspendLayout();
            // 
            // dbTableLayout
            // 
            dbTableLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            dbTableLayout.ColumnCount = 2;
            dbTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            dbTableLayout.ColumnStyles.Add(new ColumnStyle());
            dbTableLayout.Controls.Add(isSystemData, 1, 3);
            dbTableLayout.Controls.Add(tableDetailLayout, 0, 4);
            dbTableLayout.Controls.Add(catalogNameData, 0, 0);
            dbTableLayout.Controls.Add(schemaNameData, 0, 1);
            dbTableLayout.Controls.Add(tableNameData, 0, 2);
            dbTableLayout.Controls.Add(tableTypeData, 0, 3);
            dbTableLayout.Dock = DockStyle.Fill;
            dbTableLayout.Location = new Point(0, 25);
            dbTableLayout.Name = "dbTableLayout";
            dbTableLayout.RowCount = 5;
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            dbTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            dbTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            dbTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            dbTableLayout.Size = new Size(433, 358);
            dbTableLayout.TabIndex = 0;
            // 
            // isSystemData
            // 
            isSystemData.AutoCheck = false;
            isSystemData.AutoSize = true;
            isSystemData.Location = new Point(355, 153);
            isSystemData.Name = "isSystemData";
            isSystemData.Size = new Size(75, 19);
            isSystemData.TabIndex = 3;
            isSystemData.Text = "Is System";
            isSystemData.UseVisualStyleBackColor = true;
            // 
            // tableDetailLayout
            // 
            dbTableLayout.SetColumnSpan(tableDetailLayout, 2);
            tableDetailLayout.Controls.Add(extendedPropertiesTab);
            tableDetailLayout.Controls.Add(columnsTab);
            tableDetailLayout.Controls.Add(constraintTab);
            tableDetailLayout.Dock = DockStyle.Fill;
            tableDetailLayout.Location = new Point(3, 203);
            tableDetailLayout.Name = "tableDetailLayout";
            tableDetailLayout.SelectedIndex = 0;
            tableDetailLayout.Size = new Size(427, 152);
            tableDetailLayout.TabIndex = 7;
            // 
            // extendedPropertiesTab
            // 
            extendedPropertiesTab.Controls.Add(extendedPropertiesData);
            extendedPropertiesTab.Location = new Point(4, 24);
            extendedPropertiesTab.Name = "extendedPropertiesTab";
            extendedPropertiesTab.Padding = new Padding(3);
            extendedPropertiesTab.Size = new Size(419, 124);
            extendedPropertiesTab.TabIndex = 0;
            extendedPropertiesTab.Text = "Extended Properties";
            extendedPropertiesTab.UseVisualStyleBackColor = true;
            // 
            // extendedPropertiesData
            // 
            extendedPropertiesData.AllowUserToAddRows = false;
            extendedPropertiesData.AllowUserToDeleteRows = false;
            extendedPropertiesData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            extendedPropertiesData.Columns.AddRange(new DataGridViewColumn[] { propertyNameData, propertyValueData });
            extendedPropertiesData.Dock = DockStyle.Fill;
            extendedPropertiesData.Location = new Point(3, 3);
            extendedPropertiesData.Name = "extendedPropertiesData";
            extendedPropertiesData.ReadOnly = true;
            extendedPropertiesData.Size = new Size(413, 118);
            extendedPropertiesData.TabIndex = 5;
            // 
            // propertyNameData
            // 
            propertyNameData.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            propertyNameData.DataPropertyName = "PropertyName";
            propertyNameData.HeaderText = "Property Name";
            propertyNameData.Name = "propertyNameData";
            propertyNameData.ReadOnly = true;
            propertyNameData.Width = 112;
            // 
            // propertyValueData
            // 
            propertyValueData.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            propertyValueData.DataPropertyName = "PropertyValue";
            propertyValueData.HeaderText = "PropertyValue";
            propertyValueData.Name = "propertyValueData";
            propertyValueData.ReadOnly = true;
            // 
            // columnsTab
            // 
            columnsTab.Controls.Add(tableColumnsData);
            columnsTab.Location = new Point(4, 24);
            columnsTab.Name = "columnsTab";
            columnsTab.Padding = new Padding(3);
            columnsTab.Size = new Size(419, 124);
            columnsTab.TabIndex = 1;
            columnsTab.Text = "Columns";
            columnsTab.UseVisualStyleBackColor = true;
            // 
            // tableColumnsData
            // 
            tableColumnsData.AllowUserToAddRows = false;
            tableColumnsData.AllowUserToDeleteRows = false;
            tableColumnsData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tableColumnsData.Columns.AddRange(new DataGridViewColumn[] { ColumnNameValue, DataTypeValue, IsNullableValue });
            tableColumnsData.Dock = DockStyle.Fill;
            tableColumnsData.Location = new Point(3, 3);
            tableColumnsData.Name = "tableColumnsData";
            tableColumnsData.Size = new Size(413, 118);
            tableColumnsData.TabIndex = 0;
            // 
            // ColumnNameValue
            // 
            ColumnNameValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ColumnNameValue.DataPropertyName = "ColumnName";
            ColumnNameValue.HeaderText = "Column Name";
            ColumnNameValue.Name = "ColumnNameValue";
            // 
            // DataTypeValue
            // 
            DataTypeValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataTypeValue.DataPropertyName = "DataType";
            DataTypeValue.HeaderText = "Data Type";
            DataTypeValue.Name = "DataTypeValue";
            // 
            // IsNullableValue
            // 
            IsNullableValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            IsNullableValue.DataPropertyName = "IsNullable";
            IsNullableValue.HeaderText = "Is Nullable";
            IsNullableValue.Name = "IsNullableValue";
            IsNullableValue.Width = 68;
            // 
            // constraintTab
            // 
            constraintTab.Controls.Add(constraintLayout);
            constraintTab.Location = new Point(4, 24);
            constraintTab.Name = "constraintTab";
            constraintTab.Size = new Size(419, 124);
            constraintTab.TabIndex = 2;
            constraintTab.Text = "Constraints";
            constraintTab.UseVisualStyleBackColor = true;
            // 
            // constraintLayout
            // 
            constraintLayout.ColumnCount = 1;
            constraintLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            constraintLayout.Controls.Add(tableConstraintData, 0, 0);
            constraintLayout.Dock = DockStyle.Fill;
            constraintLayout.Location = new Point(0, 0);
            constraintLayout.Name = "constraintLayout";
            constraintLayout.RowCount = 1;
            constraintLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            constraintLayout.Size = new Size(419, 124);
            constraintLayout.TabIndex = 1;
            // 
            // tableConstraintData
            // 
            tableConstraintData.AllowUserToAddRows = false;
            tableConstraintData.AllowUserToDeleteRows = false;
            tableConstraintData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tableConstraintData.Columns.AddRange(new DataGridViewColumn[] { ConstraintNameValue, ConstraintTypeValue });
            tableConstraintData.Dock = DockStyle.Fill;
            tableConstraintData.Location = new Point(3, 3);
            tableConstraintData.Name = "tableConstraintData";
            tableConstraintData.ReadOnly = true;
            tableConstraintData.Size = new Size(413, 118);
            tableConstraintData.TabIndex = 0;
            // 
            // ConstraintNameValue
            // 
            ConstraintNameValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ConstraintNameValue.DataPropertyName = "ConstraintName";
            ConstraintNameValue.HeaderText = "Constraint Name";
            ConstraintNameValue.Name = "ConstraintNameValue";
            ConstraintNameValue.ReadOnly = true;
            // 
            // ConstraintTypeValue
            // 
            ConstraintTypeValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            ConstraintTypeValue.DataPropertyName = "ConstraintType";
            ConstraintTypeValue.HeaderText = "Constraint Type";
            ConstraintTypeValue.Name = "ConstraintTypeValue";
            ConstraintTypeValue.ReadOnly = true;
            ConstraintTypeValue.Width = 105;
            // 
            // catalogNameData
            // 
            catalogNameData.AutoSize = true;
            dbTableLayout.SetColumnSpan(catalogNameData, 2);
            catalogNameData.Dock = DockStyle.Fill;
            catalogNameData.HeaderText = "Catalog Name";
            catalogNameData.Location = new Point(3, 3);
            catalogNameData.Multiline = false;
            catalogNameData.Name = "catalogNameData";
            catalogNameData.ReadOnly = true;
            catalogNameData.Size = new Size(427, 44);
            catalogNameData.TabIndex = 8;
            catalogNameData.WordWrap = true;
            // 
            // schemaNameData
            // 
            schemaNameData.AutoSize = true;
            dbTableLayout.SetColumnSpan(schemaNameData, 2);
            schemaNameData.Dock = DockStyle.Fill;
            schemaNameData.HeaderText = "Schema Name";
            schemaNameData.Location = new Point(3, 53);
            schemaNameData.Multiline = false;
            schemaNameData.Name = "schemaNameData";
            schemaNameData.ReadOnly = true;
            schemaNameData.Size = new Size(427, 44);
            schemaNameData.TabIndex = 9;
            schemaNameData.WordWrap = true;
            // 
            // tableNameData
            // 
            tableNameData.AutoSize = true;
            dbTableLayout.SetColumnSpan(tableNameData, 2);
            tableNameData.Dock = DockStyle.Fill;
            tableNameData.HeaderText = "Table Name";
            tableNameData.Location = new Point(3, 103);
            tableNameData.Multiline = false;
            tableNameData.Name = "tableNameData";
            tableNameData.ReadOnly = true;
            tableNameData.Size = new Size(427, 44);
            tableNameData.TabIndex = 10;
            tableNameData.WordWrap = true;
            // 
            // tableTypeData
            // 
            tableTypeData.AutoSize = true;
            tableTypeData.Dock = DockStyle.Fill;
            tableTypeData.HeaderText = "Table Type";
            tableTypeData.Location = new Point(3, 153);
            tableTypeData.Multiline = false;
            tableTypeData.Name = "tableTypeData";
            tableTypeData.ReadOnly = true;
            tableTypeData.Size = new Size(346, 44);
            tableTypeData.TabIndex = 11;
            tableTypeData.WordWrap = true;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // exportOptions
            // 
            exportOptions.Items.AddRange(new ToolStripItem[] { exportAll, exportEntites, exportAttributes });
            exportOptions.Name = "contextMenuStrip1";
            exportOptions.Size = new Size(181, 92);
            // 
            // exportAll
            // 
            exportAll.Name = "exportAll";
            exportAll.Size = new Size(180, 22);
            exportAll.Text = "All items";
            exportAll.ToolTipText = "Create all domain model items from database model";
            exportAll.Click += ExportAll_Click;
            // 
            // exportEntites
            // 
            exportEntites.Name = "exportEntites";
            exportEntites.Size = new Size(180, 22);
            exportEntites.Text = "Entity only";
            exportEntites.ToolTipText = "Create domain Entity from database Tables or View";
            exportEntites.Click += ExportEntites_Click;
            // 
            // exportAttributes
            // 
            exportAttributes.Name = "exportAttributes";
            exportAttributes.Size = new Size(180, 22);
            exportAttributes.Text = "Attributes only";
            exportAttributes.ToolTipText = "Create domain Attributes from database Columns";
            exportAttributes.Click += ExportAttributes_Click;
            // 
            // DbTable
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(433, 383);
            Controls.Add(dbTableLayout);
            Name = "DbTable";
            Text = "Database Table";
            Load += DbTable_Load;
            Controls.SetChildIndex(dbTableLayout, 0);
            dbTableLayout.ResumeLayout(false);
            dbTableLayout.PerformLayout();
            tableDetailLayout.ResumeLayout(false);
            extendedPropertiesTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)extendedPropertiesData).EndInit();
            columnsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)tableColumnsData).EndInit();
            constraintTab.ResumeLayout(false);
            constraintLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)tableConstraintData).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingTable).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingColumns).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingConstraints).EndInit();
            exportOptions.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ErrorProvider errorProvider;
        private CheckBox isSystemData;
        private DataGridView extendedPropertiesData;
        private DataGridViewTextBoxColumn propertyNameData;
        private DataGridViewTextBoxColumn propertyValueData;
        private DataGridView tableColumnsData;
        private Controls.TextBoxData catalogNameData;
        private Controls.TextBoxData schemaNameData;
        private Controls.TextBoxData tableNameData;
        private Controls.TextBoxData tableTypeData;
        private DataGridViewTextBoxColumn ColumnNameValue;
        private DataGridViewTextBoxColumn DataTypeValue;
        private DataGridViewCheckBoxColumn IsNullableValue;
        private TabPage constraintTab;
        private DataGridView tableConstraintData;
        private DataGridViewTextBoxColumn ConstraintNameValue;
        private DataGridViewTextBoxColumn ConstraintTypeValue;
        private BindingSource bindingTable;
        private BindingSource bindingProperties;
        private BindingSource bindingColumns;
        private BindingSource bindingConstraints;
        private ContextMenuStrip exportOptions;
        private ToolStripMenuItem exportAll;
        private ToolStripMenuItem exportEntites;
        private ToolStripMenuItem exportAttributes;
    }
}