namespace DataDictionary.Main.Forms
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
            referenceSchemaNameData = new Controls.TextBoxData();
            referenceTableNameData = new Controls.TextBoxData();
            contraintTabs = new TabControl();
            columnsTab = new TabPage();
            constraintColumnsData = new DataGridView();
            extendedPropertiesTab = new TabPage();
            extendedPropertiesData = new DataGridView();
            propertyNameData = new DataGridViewTextBoxColumn();
            propertyValueData = new DataGridViewTextBoxColumn();
            referenceColumnNameValue = new DataGridViewTextBoxColumn();
            constraintLayout = new TableLayoutPanel();
            constraintLayout.SuspendLayout();
            contraintTabs.SuspendLayout();
            columnsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)constraintColumnsData).BeginInit();
            extendedPropertiesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)extendedPropertiesData).BeginInit();
            SuspendLayout();
            // 
            // constraintLayout
            // 
            constraintLayout.ColumnCount = 1;
            constraintLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            constraintLayout.Controls.Add(catalogNameData, 0, 0);
            constraintLayout.Controls.Add(schemaNameData, 0, 1);
            constraintLayout.Controls.Add(constraintNameData, 0, 2);
            constraintLayout.Controls.Add(constraintTypeData, 0, 3);
            constraintLayout.Controls.Add(referenceSchemaNameData, 0, 4);
            constraintLayout.Controls.Add(referenceTableNameData, 0, 5);
            constraintLayout.Controls.Add(contraintTabs, 0, 6);
            constraintLayout.Dock = DockStyle.Fill;
            constraintLayout.Location = new Point(0, 0);
            constraintLayout.Name = "constraintLayout";
            constraintLayout.RowCount = 7;
            constraintLayout.RowStyles.Add(new RowStyle());
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
            constraintTypeData.Location = new Point(3, 153);
            constraintTypeData.Multiline = false;
            constraintTypeData.Name = "constraintTypeData";
            constraintTypeData.ReadOnly = true;
            constraintTypeData.Size = new Size(510, 44);
            constraintTypeData.TabIndex = 3;
            // 
            // referenceSchemaNameData
            // 
            referenceSchemaNameData.AutoSize = true;
            referenceSchemaNameData.Dock = DockStyle.Fill;
            referenceSchemaNameData.HeaderText = "Reference SchemaN ame";
            referenceSchemaNameData.Location = new Point(3, 203);
            referenceSchemaNameData.Multiline = false;
            referenceSchemaNameData.Name = "referenceSchemaNameData";
            referenceSchemaNameData.ReadOnly = true;
            referenceSchemaNameData.Size = new Size(510, 44);
            referenceSchemaNameData.TabIndex = 4;
            // 
            // referenceTableNameData
            // 
            referenceTableNameData.AutoSize = true;
            referenceTableNameData.Dock = DockStyle.Fill;
            referenceTableNameData.HeaderText = "Reference Table Name";
            referenceTableNameData.Location = new Point(3, 253);
            referenceTableNameData.Multiline = false;
            referenceTableNameData.Name = "referenceTableNameData";
            referenceTableNameData.ReadOnly = true;
            referenceTableNameData.Size = new Size(510, 44);
            referenceTableNameData.TabIndex = 5;
            // 
            // contraintTabs
            // 
            contraintTabs.Controls.Add(columnsTab);
            contraintTabs.Controls.Add(extendedPropertiesTab);
            contraintTabs.Dock = DockStyle.Fill;
            contraintTabs.Location = new Point(3, 303);
            contraintTabs.Name = "contraintTabs";
            contraintTabs.SelectedIndex = 0;
            contraintTabs.Size = new Size(510, 237);
            contraintTabs.TabIndex = 6;
            // 
            // columnsTab
            // 
            columnsTab.Controls.Add(constraintColumnsData);
            columnsTab.Location = new Point(4, 24);
            columnsTab.Name = "columnsTab";
            columnsTab.Padding = new Padding(3);
            columnsTab.Size = new Size(502, 209);
            columnsTab.TabIndex = 1;
            columnsTab.Text = "Columns";
            columnsTab.UseVisualStyleBackColor = true;
            // 
            // constraintColumnsData
            // 
            constraintColumnsData.AllowUserToAddRows = false;
            constraintColumnsData.AllowUserToDeleteRows = false;
            constraintColumnsData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            constraintColumnsData.Columns.AddRange(new DataGridViewColumn[] { referenceColumnNameValue });
            constraintColumnsData.Dock = DockStyle.Fill;
            constraintColumnsData.Location = new Point(3, 3);
            constraintColumnsData.Name = "constraintColumnsData";
            constraintColumnsData.ReadOnly = true;
            constraintColumnsData.RowTemplate.Height = 25;
            constraintColumnsData.Size = new Size(496, 203);
            constraintColumnsData.TabIndex = 0;
            // 
            // extendedPropertiesTab
            // 
            extendedPropertiesTab.Controls.Add(extendedPropertiesData);
            extendedPropertiesTab.Location = new Point(4, 24);
            extendedPropertiesTab.Name = "extendedPropertiesTab";
            extendedPropertiesTab.Padding = new Padding(3);
            extendedPropertiesTab.Size = new Size(502, 209);
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
            extendedPropertiesData.Size = new Size(496, 203);
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
            // referenceColumnNameValue
            // 
            referenceColumnNameValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            referenceColumnNameValue.DataPropertyName = "ReferenceColumnName";
            referenceColumnNameValue.HeaderText = "Reference Column Name";
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
            contraintTabs.ResumeLayout(false);
            columnsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)constraintColumnsData).EndInit();
            extendedPropertiesTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)extendedPropertiesData).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel constraintLayout;
        private Controls.TextBoxData catalogNameData;
        private Controls.TextBoxData schemaNameData;
        private Controls.TextBoxData constraintNameData;
        private Controls.TextBoxData constraintTypeData;
        private Controls.TextBoxData referenceSchemaNameData;
        private Controls.TextBoxData referenceTableNameData;
        private TabControl contraintTabs;
        private TabPage extendedPropertiesTab;
        private TabPage columnsTab;
        private DataGridView constraintColumnsData;
        private DataGridView extendedPropertiesData;
        private DataGridViewTextBoxColumn propertyNameData;
        private DataGridViewTextBoxColumn propertyValueData;
        private DataGridViewTextBoxColumn referenceColumnNameValue;
    }
}