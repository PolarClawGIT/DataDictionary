namespace DataDictionary.Main.Forms.Database
{
    partial class DbConstraint
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
            TableLayoutPanel constraintLayout;
            catalogNameData = new Controls.TextBoxData();
            schemaNameData = new Controls.TextBoxData();
            constraintNameData = new Controls.TextBoxData();
            contraintTabs = new TabControl();
            columnsTab = new TabPage();
            constraintColumnsData = new DataGridView();
            columnName = new DataGridViewTextBoxColumn();
            referencedSchemaNameColumn = new DataGridViewTextBoxColumn();
            referencedTableNameColumn = new DataGridViewTextBoxColumn();
            referencedColumnNameColumn = new DataGridViewTextBoxColumn();
            extendedPropertiesTab = new TabPage();
            extendedPropertiesData = new DataGridView();
            propertyNameData = new DataGridViewTextBoxColumn();
            propertyValueData = new DataGridViewTextBoxColumn();
            constraintTypeData = new Controls.TextBoxData();
            tableNameData = new Controls.TextBoxData();
            bindingConstraint = new BindingSource(components);
            bindingColumn = new BindingSource(components);
            bindingProperties = new BindingSource(components);
            constraintLayout = new TableLayoutPanel();
            constraintLayout.SuspendLayout();
            contraintTabs.SuspendLayout();
            columnsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)constraintColumnsData).BeginInit();
            extendedPropertiesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)extendedPropertiesData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingConstraint).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingColumn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingProperties).BeginInit();
            SuspendLayout();
            // 
            // constraintLayout
            // 
            constraintLayout.ColumnCount = 1;
            constraintLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            constraintLayout.Controls.Add(catalogNameData, 0, 0);
            constraintLayout.Controls.Add(schemaNameData, 0, 1);
            constraintLayout.Controls.Add(constraintNameData, 0, 2);
            constraintLayout.Controls.Add(contraintTabs, 0, 5);
            constraintLayout.Controls.Add(constraintTypeData, 0, 4);
            constraintLayout.Controls.Add(tableNameData, 0, 3);
            constraintLayout.Dock = DockStyle.Fill;
            constraintLayout.Location = new Point(0, 25);
            constraintLayout.Name = "constraintLayout";
            constraintLayout.RowCount = 6;
            constraintLayout.RowStyles.Add(new RowStyle());
            constraintLayout.RowStyles.Add(new RowStyle());
            constraintLayout.RowStyles.Add(new RowStyle());
            constraintLayout.RowStyles.Add(new RowStyle());
            constraintLayout.RowStyles.Add(new RowStyle());
            constraintLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            constraintLayout.Size = new Size(516, 518);
            constraintLayout.TabIndex = 0;
            // 
            // catalogNameData
            // 
            catalogNameData.AutoSize = true;
            catalogNameData.Dock = DockStyle.Fill;
            catalogNameData.HeaderText = "Catalog Name";
            catalogNameData.Location = new Point(3, 3);
            catalogNameData.Multiline = false;
            catalogNameData.Name = "catalogNameData";
            catalogNameData.ReadOnly = true;
            catalogNameData.Size = new Size(510, 44);
            catalogNameData.TabIndex = 0;
            catalogNameData.WordWrap = true;
            // 
            // schemaNameData
            // 
            schemaNameData.AutoSize = true;
            schemaNameData.Dock = DockStyle.Fill;
            schemaNameData.HeaderText = "Schema Name";
            schemaNameData.Location = new Point(3, 53);
            schemaNameData.Multiline = false;
            schemaNameData.Name = "schemaNameData";
            schemaNameData.ReadOnly = true;
            schemaNameData.Size = new Size(510, 44);
            schemaNameData.TabIndex = 1;
            schemaNameData.WordWrap = true;
            // 
            // constraintNameData
            // 
            constraintNameData.AutoSize = true;
            constraintNameData.Dock = DockStyle.Fill;
            constraintNameData.HeaderText = "Constraint Name";
            constraintNameData.Location = new Point(3, 103);
            constraintNameData.Multiline = false;
            constraintNameData.Name = "constraintNameData";
            constraintNameData.ReadOnly = true;
            constraintNameData.Size = new Size(510, 44);
            constraintNameData.TabIndex = 2;
            constraintNameData.WordWrap = true;
            // 
            // contraintTabs
            // 
            contraintTabs.Controls.Add(columnsTab);
            contraintTabs.Controls.Add(extendedPropertiesTab);
            contraintTabs.Dock = DockStyle.Fill;
            contraintTabs.Location = new Point(3, 253);
            contraintTabs.Name = "contraintTabs";
            contraintTabs.SelectedIndex = 0;
            contraintTabs.Size = new Size(510, 262);
            contraintTabs.TabIndex = 6;
            // 
            // columnsTab
            // 
            columnsTab.Controls.Add(constraintColumnsData);
            columnsTab.Location = new Point(4, 24);
            columnsTab.Name = "columnsTab";
            columnsTab.Padding = new Padding(3);
            columnsTab.Size = new Size(502, 234);
            columnsTab.TabIndex = 1;
            columnsTab.Text = "Columns";
            columnsTab.UseVisualStyleBackColor = true;
            // 
            // constraintColumnsData
            // 
            constraintColumnsData.AllowUserToAddRows = false;
            constraintColumnsData.AllowUserToDeleteRows = false;
            constraintColumnsData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            constraintColumnsData.Columns.AddRange(new DataGridViewColumn[] { columnName, referencedSchemaNameColumn, referencedTableNameColumn, referencedColumnNameColumn });
            constraintColumnsData.Dock = DockStyle.Fill;
            constraintColumnsData.Location = new Point(3, 3);
            constraintColumnsData.Name = "constraintColumnsData";
            constraintColumnsData.ReadOnly = true;
            constraintColumnsData.Size = new Size(496, 228);
            constraintColumnsData.TabIndex = 0;
            // 
            // columnName
            // 
            columnName.DataPropertyName = "ColumnName";
            columnName.HeaderText = "Column";
            columnName.Name = "columnName";
            columnName.ReadOnly = true;
            // 
            // referencedSchemaNameColumn
            // 
            referencedSchemaNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            referencedSchemaNameColumn.DataPropertyName = "ReferencedSchemaName";
            referencedSchemaNameColumn.HeaderText = "Ref. Schema";
            referencedSchemaNameColumn.Name = "referencedSchemaNameColumn";
            referencedSchemaNameColumn.ReadOnly = true;
            // 
            // referencedTableNameColumn
            // 
            referencedTableNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            referencedTableNameColumn.DataPropertyName = "ReferencedTableName";
            referencedTableNameColumn.HeaderText = "Ref. Table";
            referencedTableNameColumn.Name = "referencedTableNameColumn";
            referencedTableNameColumn.ReadOnly = true;
            // 
            // referencedColumnNameColumn
            // 
            referencedColumnNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            referencedColumnNameColumn.DataPropertyName = "ReferencedColumnName";
            referencedColumnNameColumn.HeaderText = "Ref. Column";
            referencedColumnNameColumn.Name = "referencedColumnNameColumn";
            referencedColumnNameColumn.ReadOnly = true;
            // 
            // extendedPropertiesTab
            // 
            extendedPropertiesTab.Controls.Add(extendedPropertiesData);
            extendedPropertiesTab.Location = new Point(4, 24);
            extendedPropertiesTab.Name = "extendedPropertiesTab";
            extendedPropertiesTab.Padding = new Padding(3);
            extendedPropertiesTab.Size = new Size(502, 234);
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
            extendedPropertiesData.Size = new Size(496, 228);
            extendedPropertiesData.TabIndex = 6;
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
            // constraintTypeData
            // 
            constraintTypeData.AutoSize = true;
            constraintTypeData.Dock = DockStyle.Fill;
            constraintTypeData.HeaderText = "Constraint Type";
            constraintTypeData.Location = new Point(3, 203);
            constraintTypeData.Multiline = false;
            constraintTypeData.Name = "constraintTypeData";
            constraintTypeData.ReadOnly = true;
            constraintTypeData.Size = new Size(510, 44);
            constraintTypeData.TabIndex = 3;
            constraintTypeData.WordWrap = true;
            // 
            // tableNameData
            // 
            tableNameData.AutoSize = true;
            tableNameData.Dock = DockStyle.Fill;
            tableNameData.HeaderText = "Table Name";
            tableNameData.Location = new Point(3, 153);
            tableNameData.Multiline = false;
            tableNameData.Name = "tableNameData";
            tableNameData.ReadOnly = true;
            tableNameData.Size = new Size(510, 44);
            tableNameData.TabIndex = 5;
            tableNameData.WordWrap = true;
            // 
            // DbConstraint
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(516, 543);
            Controls.Add(constraintLayout);
            Name = "DbConstraint";
            Text = "Db Constraint";
            Load += DbConstraint_Load;
            Controls.SetChildIndex(constraintLayout, 0);
            constraintLayout.ResumeLayout(false);
            constraintLayout.PerformLayout();
            contraintTabs.ResumeLayout(false);
            columnsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)constraintColumnsData).EndInit();
            extendedPropertiesTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)extendedPropertiesData).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingConstraint).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingColumn).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingProperties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData catalogNameData;
        private Controls.TextBoxData schemaNameData;
        private Controls.TextBoxData constraintNameData;
        private Controls.TextBoxData constraintTypeData;
        private Controls.TextBoxData tableNameData;
        private TabControl contraintTabs;
        private TabPage columnsTab;
        private DataGridView constraintColumnsData;
        private TabPage extendedPropertiesTab;
        private DataGridView extendedPropertiesData;
        private DataGridViewTextBoxColumn propertyNameData;
        private DataGridViewTextBoxColumn propertyValueData;
        private BindingSource bindingConstraint;
        private BindingSource bindingColumn;
        private BindingSource bindingProperties;
        private DataGridViewTextBoxColumn columnName;
        private DataGridViewTextBoxColumn referencedSchemaNameColumn;
        private DataGridViewTextBoxColumn referencedTableNameColumn;
        private DataGridViewTextBoxColumn referencedColumnNameColumn;
    }
}