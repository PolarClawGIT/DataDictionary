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
            TableLayoutPanel constraintLayout;
            catalogNameData = new Controls.TextBoxData();
            schemaNameData = new Controls.TextBoxData();
            constraintNameData = new Controls.TextBoxData();
            constraintTypeData = new Controls.TextBoxData();
            tableNameData = new Controls.TextBoxData();
            extendedPropertiesTab = new TabPage();
            extendedPropertiesData = new DataGridView();
            propertyValueData = new DataGridViewTextBoxColumn();
            propertyNameData = new DataGridViewTextBoxColumn();
            columnsTab = new TabPage();
            constraintColumnsData = new DataGridView();
            contraintTabs = new TabControl();
            referenceSchemaNameValue = new DataGridViewTextBoxColumn();
            referenceTableNameValue = new DataGridViewTextBoxColumn();
            referenceColumnNameValue = new DataGridViewTextBoxColumn();
            constraintLayout = new TableLayoutPanel();
            constraintLayout.SuspendLayout();
            extendedPropertiesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)extendedPropertiesData).BeginInit();
            columnsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)constraintColumnsData).BeginInit();
            contraintTabs.SuspendLayout();
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
            constraintLayout.Location = new Point(0, 0);
            constraintLayout.Name = "constraintLayout";
            constraintLayout.RowCount = 6;
            constraintLayout.RowStyles.Add(new RowStyle());
            constraintLayout.RowStyles.Add(new RowStyle());
            constraintLayout.RowStyles.Add(new RowStyle());
            constraintLayout.RowStyles.Add(new RowStyle());
            constraintLayout.RowStyles.Add(new RowStyle());
            constraintLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            constraintLayout.Size = new Size(516, 543);
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
            // 
            // extendedPropertiesTab
            // 
            extendedPropertiesTab.Controls.Add(extendedPropertiesData);
            extendedPropertiesTab.Location = new Point(4, 24);
            extendedPropertiesTab.Name = "extendedPropertiesTab";
            extendedPropertiesTab.Padding = new Padding(3);
            extendedPropertiesTab.Size = new Size(502, 127);
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
            extendedPropertiesData.RowTemplate.Height = 25;
            extendedPropertiesData.Size = new Size(496, 121);
            extendedPropertiesData.TabIndex = 6;
            // 
            // propertyValueData
            // 
            propertyValueData.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            propertyValueData.DataPropertyName = "PropertyValue";
            propertyValueData.HeaderText = "PropertyValue";
            propertyValueData.Name = "propertyValueData";
            propertyValueData.ReadOnly = true;
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
            // columnsTab
            // 
            columnsTab.Controls.Add(constraintColumnsData);
            columnsTab.Location = new Point(4, 24);
            columnsTab.Name = "columnsTab";
            columnsTab.Padding = new Padding(3);
            columnsTab.Size = new Size(502, 259);
            columnsTab.TabIndex = 1;
            columnsTab.Text = "Columns";
            columnsTab.UseVisualStyleBackColor = true;
            // 
            // constraintColumnsData
            // 
            constraintColumnsData.AllowUserToAddRows = false;
            constraintColumnsData.AllowUserToDeleteRows = false;
            constraintColumnsData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            constraintColumnsData.Columns.AddRange(new DataGridViewColumn[] { referenceSchemaNameValue, referenceTableNameValue, referenceColumnNameValue });
            constraintColumnsData.Dock = DockStyle.Fill;
            constraintColumnsData.Location = new Point(3, 3);
            constraintColumnsData.Name = "constraintColumnsData";
            constraintColumnsData.ReadOnly = true;
            constraintColumnsData.RowTemplate.Height = 25;
            constraintColumnsData.Size = new Size(496, 253);
            constraintColumnsData.TabIndex = 0;
            // 
            // contraintTabs
            // 
            contraintTabs.Controls.Add(columnsTab);
            contraintTabs.Controls.Add(extendedPropertiesTab);
            contraintTabs.Dock = DockStyle.Fill;
            contraintTabs.Location = new Point(3, 253);
            contraintTabs.Name = "contraintTabs";
            contraintTabs.SelectedIndex = 0;
            contraintTabs.Size = new Size(510, 287);
            contraintTabs.TabIndex = 6;
            // 
            // referenceSchemaNameValue
            // 
            referenceSchemaNameValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            referenceSchemaNameValue.DataPropertyName = "ReferenceSchemaName";
            referenceSchemaNameValue.HeaderText = "Schema Name";
            referenceSchemaNameValue.Name = "referenceSchemaNameValue";
            referenceSchemaNameValue.ReadOnly = true;
            // 
            // referenceTableNameValue
            // 
            referenceTableNameValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            referenceTableNameValue.DataPropertyName = "ReferenceTableName";
            referenceTableNameValue.HeaderText = "Table Name";
            referenceTableNameValue.Name = "referenceTableNameValue";
            referenceTableNameValue.ReadOnly = true;
            // 
            // referenceColumnNameValue
            // 
            referenceColumnNameValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            referenceColumnNameValue.DataPropertyName = "ReferenceColumnName";
            referenceColumnNameValue.HeaderText = "Column Name";
            referenceColumnNameValue.Name = "referenceColumnNameValue";
            referenceColumnNameValue.ReadOnly = true;
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
            constraintLayout.ResumeLayout(false);
            constraintLayout.PerformLayout();
            extendedPropertiesTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)extendedPropertiesData).EndInit();
            columnsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)constraintColumnsData).EndInit();
            contraintTabs.ResumeLayout(false);
            ResumeLayout(false);
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
        private DataGridViewTextBoxColumn referenceSchemaNameValue;
        private DataGridViewTextBoxColumn referenceTableNameValue;
        private DataGridViewTextBoxColumn referenceColumnNameValue;
    }
}