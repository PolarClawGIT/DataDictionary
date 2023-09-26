namespace DataDictionary.Main.Forms.Database
{
    partial class DbDomain
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
            TableLayoutPanel dbTableLayout;
            TabControl domainDetailLayout;
            TabPage domainTab;
            TabControl dataTypeDetailTab;
            TabPage extendedPropertiesTab;
            generalDataTab = new TabPage();
            columnGeneralDataLayout = new TableLayoutPanel();
            dateTimePrecisionData = new Controls.TextBoxData();
            numericScaleData = new Controls.TextBoxData();
            numericPrecisionRadixData = new Controls.TextBoxData();
            numericPrecisionData = new Controls.TextBoxData();
            columnDefaultData = new Controls.TextBoxData();
            dataTypeData = new Controls.TextBoxData();
            characterDataTab = new TabPage();
            characterDataLayout = new TableLayoutPanel();
            characterMaximumLengthData = new Controls.TextBoxData();
            characterOctetLengthData = new Controls.TextBoxData();
            characterSetCatalogData = new Controls.TextBoxData();
            characterSetSchemaData = new Controls.TextBoxData();
            characterSetNameData = new Controls.TextBoxData();
            collationCatalogData = new Controls.TextBoxData();
            collationSchemaData = new Controls.TextBoxData();
            collationNameData = new Controls.TextBoxData();
            extendedPropertiesData = new DataGridView();
            propertyNameData = new DataGridViewTextBoxColumn();
            propertyValueData = new DataGridViewTextBoxColumn();
            catalogNameData = new Controls.TextBoxData();
            schemaNameData = new Controls.TextBoxData();
            domainNameData = new Controls.TextBoxData();
            dbTableLayout = new TableLayoutPanel();
            domainDetailLayout = new TabControl();
            domainTab = new TabPage();
            dataTypeDetailTab = new TabControl();
            extendedPropertiesTab = new TabPage();
            dbTableLayout.SuspendLayout();
            domainDetailLayout.SuspendLayout();
            domainTab.SuspendLayout();
            dataTypeDetailTab.SuspendLayout();
            generalDataTab.SuspendLayout();
            columnGeneralDataLayout.SuspendLayout();
            characterDataTab.SuspendLayout();
            characterDataLayout.SuspendLayout();
            extendedPropertiesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)extendedPropertiesData).BeginInit();
            SuspendLayout();
            // 
            // dbTableLayout
            // 
            dbTableLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            dbTableLayout.ColumnCount = 1;
            dbTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            dbTableLayout.Controls.Add(domainDetailLayout, 0, 3);
            dbTableLayout.Controls.Add(catalogNameData, 0, 0);
            dbTableLayout.Controls.Add(schemaNameData, 0, 1);
            dbTableLayout.Controls.Add(domainNameData, 0, 2);
            dbTableLayout.Dock = DockStyle.Fill;
            dbTableLayout.Location = new Point(0, 25);
            dbTableLayout.Name = "dbTableLayout";
            dbTableLayout.RowCount = 4;
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            dbTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            dbTableLayout.Size = new Size(501, 555);
            dbTableLayout.TabIndex = 2;
            // 
            // domainDetailLayout
            // 
            domainDetailLayout.Controls.Add(domainTab);
            domainDetailLayout.Controls.Add(extendedPropertiesTab);
            domainDetailLayout.Dock = DockStyle.Fill;
            domainDetailLayout.Location = new Point(3, 153);
            domainDetailLayout.Name = "domainDetailLayout";
            domainDetailLayout.SelectedIndex = 0;
            domainDetailLayout.Size = new Size(495, 399);
            domainDetailLayout.TabIndex = 9;
            // 
            // domainTab
            // 
            domainTab.BackColor = SystemColors.Control;
            domainTab.Controls.Add(dataTypeDetailTab);
            domainTab.Location = new Point(4, 24);
            domainTab.Name = "domainTab";
            domainTab.Padding = new Padding(3);
            domainTab.Size = new Size(487, 371);
            domainTab.TabIndex = 1;
            domainTab.Text = "Domain Detail";
            // 
            // dataTypeDetailTab
            // 
            dataTypeDetailTab.Controls.Add(generalDataTab);
            dataTypeDetailTab.Controls.Add(characterDataTab);
            dataTypeDetailTab.Dock = DockStyle.Fill;
            dataTypeDetailTab.Location = new Point(3, 3);
            dataTypeDetailTab.Name = "dataTypeDetailTab";
            dataTypeDetailTab.SelectedIndex = 0;
            dataTypeDetailTab.Size = new Size(481, 365);
            dataTypeDetailTab.TabIndex = 1;
            // 
            // generalDataTab
            // 
            generalDataTab.BackColor = SystemColors.Control;
            generalDataTab.Controls.Add(columnGeneralDataLayout);
            generalDataTab.Location = new Point(4, 24);
            generalDataTab.Name = "generalDataTab";
            generalDataTab.Size = new Size(473, 337);
            generalDataTab.TabIndex = 3;
            generalDataTab.Text = "General";
            // 
            // columnGeneralDataLayout
            // 
            columnGeneralDataLayout.ColumnCount = 3;
            columnGeneralDataLayout.ColumnStyles.Add(new ColumnStyle());
            columnGeneralDataLayout.ColumnStyles.Add(new ColumnStyle());
            columnGeneralDataLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            columnGeneralDataLayout.Controls.Add(numericScaleData, 2, 1);
            columnGeneralDataLayout.Controls.Add(numericPrecisionRadixData, 1, 1);
            columnGeneralDataLayout.Controls.Add(numericPrecisionData, 0, 1);
            columnGeneralDataLayout.Controls.Add(dataTypeData, 0, 0);
            columnGeneralDataLayout.Controls.Add(dateTimePrecisionData, 2, 0);
            columnGeneralDataLayout.Controls.Add(columnDefaultData, 0, 2);
            columnGeneralDataLayout.Dock = DockStyle.Fill;
            columnGeneralDataLayout.Location = new Point(0, 0);
            columnGeneralDataLayout.Name = "columnGeneralDataLayout";
            columnGeneralDataLayout.RowCount = 3;
            columnGeneralDataLayout.RowStyles.Add(new RowStyle());
            columnGeneralDataLayout.RowStyles.Add(new RowStyle());
            columnGeneralDataLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            columnGeneralDataLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            columnGeneralDataLayout.Size = new Size(473, 337);
            columnGeneralDataLayout.TabIndex = 1;
            // 
            // dateTimePrecisionData
            // 
            dateTimePrecisionData.AutoSize = true;
            dateTimePrecisionData.Dock = DockStyle.Fill;
            dateTimePrecisionData.HeaderText = "Date Time Precision";
            dateTimePrecisionData.Location = new Point(305, 3);
            dateTimePrecisionData.Multiline = false;
            dateTimePrecisionData.Name = "dateTimePrecisionData";
            dateTimePrecisionData.ReadOnly = true;
            dateTimePrecisionData.Size = new Size(165, 44);
            dateTimePrecisionData.TabIndex = 20;
            // 
            // numericScaleData
            // 
            numericScaleData.AutoSize = true;
            numericScaleData.Dock = DockStyle.Fill;
            numericScaleData.HeaderText = "Numeric Scale";
            numericScaleData.Location = new Point(305, 53);
            numericScaleData.Multiline = false;
            numericScaleData.Name = "numericScaleData";
            numericScaleData.ReadOnly = true;
            numericScaleData.Size = new Size(165, 44);
            numericScaleData.TabIndex = 19;
            // 
            // numericPrecisionRadixData
            // 
            numericPrecisionRadixData.AutoSize = true;
            numericPrecisionRadixData.Dock = DockStyle.Fill;
            numericPrecisionRadixData.HeaderText = "Numeric Precision Radix";
            numericPrecisionRadixData.Location = new Point(138, 53);
            numericPrecisionRadixData.Multiline = false;
            numericPrecisionRadixData.Name = "numericPrecisionRadixData";
            numericPrecisionRadixData.ReadOnly = true;
            numericPrecisionRadixData.Size = new Size(161, 44);
            numericPrecisionRadixData.TabIndex = 18;
            // 
            // numericPrecisionData
            // 
            numericPrecisionData.AutoSize = true;
            numericPrecisionData.Dock = DockStyle.Fill;
            numericPrecisionData.HeaderText = "Numeric Precision";
            numericPrecisionData.Location = new Point(3, 53);
            numericPrecisionData.Multiline = false;
            numericPrecisionData.Name = "numericPrecisionData";
            numericPrecisionData.ReadOnly = true;
            numericPrecisionData.Size = new Size(129, 44);
            numericPrecisionData.TabIndex = 17;
            // 
            // columnDefaultData
            // 
            columnDefaultData.AutoSize = true;
            columnGeneralDataLayout.SetColumnSpan(columnDefaultData, 3);
            columnDefaultData.Dock = DockStyle.Fill;
            columnDefaultData.HeaderText = "Default";
            columnDefaultData.Location = new Point(3, 103);
            columnDefaultData.Multiline = true;
            columnDefaultData.Name = "columnDefaultData";
            columnDefaultData.ReadOnly = true;
            columnDefaultData.Size = new Size(467, 231);
            columnDefaultData.TabIndex = 15;
            // 
            // dataTypeData
            // 
            dataTypeData.AutoSize = true;
            columnGeneralDataLayout.SetColumnSpan(dataTypeData, 2);
            dataTypeData.Dock = DockStyle.Fill;
            dataTypeData.HeaderText = "Data Type";
            dataTypeData.Location = new Point(3, 3);
            dataTypeData.Multiline = false;
            dataTypeData.Name = "dataTypeData";
            dataTypeData.ReadOnly = true;
            dataTypeData.Size = new Size(296, 44);
            dataTypeData.TabIndex = 12;
            // 
            // characterDataTab
            // 
            characterDataTab.BackColor = SystemColors.Control;
            characterDataTab.Controls.Add(characterDataLayout);
            characterDataTab.Location = new Point(4, 24);
            characterDataTab.Name = "characterDataTab";
            characterDataTab.Padding = new Padding(3);
            characterDataTab.Size = new Size(473, 337);
            characterDataTab.TabIndex = 0;
            characterDataTab.Text = "Character";
            // 
            // characterDataLayout
            // 
            characterDataLayout.ColumnCount = 2;
            characterDataLayout.ColumnStyles.Add(new ColumnStyle());
            characterDataLayout.ColumnStyles.Add(new ColumnStyle());
            characterDataLayout.Controls.Add(characterMaximumLengthData, 0, 0);
            characterDataLayout.Controls.Add(characterOctetLengthData, 1, 0);
            characterDataLayout.Controls.Add(characterSetCatalogData, 0, 1);
            characterDataLayout.Controls.Add(characterSetSchemaData, 0, 2);
            characterDataLayout.Controls.Add(characterSetNameData, 0, 3);
            characterDataLayout.Controls.Add(collationCatalogData, 0, 4);
            characterDataLayout.Controls.Add(collationSchemaData, 0, 5);
            characterDataLayout.Controls.Add(collationNameData, 0, 6);
            characterDataLayout.Dock = DockStyle.Fill;
            characterDataLayout.Location = new Point(3, 3);
            characterDataLayout.Name = "characterDataLayout";
            characterDataLayout.RowCount = 8;
            characterDataLayout.RowStyles.Add(new RowStyle());
            characterDataLayout.RowStyles.Add(new RowStyle());
            characterDataLayout.RowStyles.Add(new RowStyle());
            characterDataLayout.RowStyles.Add(new RowStyle());
            characterDataLayout.RowStyles.Add(new RowStyle());
            characterDataLayout.RowStyles.Add(new RowStyle());
            characterDataLayout.RowStyles.Add(new RowStyle());
            characterDataLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            characterDataLayout.Size = new Size(467, 331);
            characterDataLayout.TabIndex = 3;
            // 
            // characterMaximumLengthData
            // 
            characterMaximumLengthData.AutoSize = true;
            characterMaximumLengthData.HeaderText = "Maximum Length";
            characterMaximumLengthData.Location = new Point(3, 3);
            characterMaximumLengthData.Multiline = false;
            characterMaximumLengthData.Name = "characterMaximumLengthData";
            characterMaximumLengthData.ReadOnly = true;
            characterMaximumLengthData.Size = new Size(127, 44);
            characterMaximumLengthData.TabIndex = 12;
            // 
            // characterOctetLengthData
            // 
            characterOctetLengthData.AutoSize = true;
            characterOctetLengthData.HeaderText = "Octet Length";
            characterOctetLengthData.Location = new Point(136, 3);
            characterOctetLengthData.Multiline = false;
            characterOctetLengthData.Name = "characterOctetLengthData";
            characterOctetLengthData.ReadOnly = true;
            characterOctetLengthData.Size = new Size(120, 44);
            characterOctetLengthData.TabIndex = 13;
            // 
            // characterSetCatalogData
            // 
            characterSetCatalogData.AutoSize = true;
            characterDataLayout.SetColumnSpan(characterSetCatalogData, 2);
            characterSetCatalogData.Dock = DockStyle.Fill;
            characterSetCatalogData.HeaderText = "Character Set Catalog";
            characterSetCatalogData.Location = new Point(3, 53);
            characterSetCatalogData.Multiline = false;
            characterSetCatalogData.Name = "characterSetCatalogData";
            characterSetCatalogData.ReadOnly = true;
            characterSetCatalogData.Size = new Size(461, 44);
            characterSetCatalogData.TabIndex = 14;
            // 
            // characterSetSchemaData
            // 
            characterSetSchemaData.AutoSize = true;
            characterDataLayout.SetColumnSpan(characterSetSchemaData, 2);
            characterSetSchemaData.Dock = DockStyle.Fill;
            characterSetSchemaData.HeaderText = "Character Set Schema";
            characterSetSchemaData.Location = new Point(3, 103);
            characterSetSchemaData.Multiline = false;
            characterSetSchemaData.Name = "characterSetSchemaData";
            characterSetSchemaData.ReadOnly = true;
            characterSetSchemaData.Size = new Size(461, 44);
            characterSetSchemaData.TabIndex = 15;
            // 
            // characterSetNameData
            // 
            characterSetNameData.AutoSize = true;
            characterDataLayout.SetColumnSpan(characterSetNameData, 2);
            characterSetNameData.Dock = DockStyle.Fill;
            characterSetNameData.HeaderText = "Character Set Name";
            characterSetNameData.Location = new Point(3, 153);
            characterSetNameData.Multiline = false;
            characterSetNameData.Name = "characterSetNameData";
            characterSetNameData.ReadOnly = true;
            characterSetNameData.Size = new Size(461, 44);
            characterSetNameData.TabIndex = 18;
            // 
            // collationCatalogData
            // 
            collationCatalogData.AutoSize = true;
            characterDataLayout.SetColumnSpan(collationCatalogData, 2);
            collationCatalogData.Dock = DockStyle.Fill;
            collationCatalogData.HeaderText = "Collation Catalog";
            collationCatalogData.Location = new Point(3, 203);
            collationCatalogData.Multiline = false;
            collationCatalogData.Name = "collationCatalogData";
            collationCatalogData.ReadOnly = true;
            collationCatalogData.Size = new Size(461, 44);
            collationCatalogData.TabIndex = 19;
            // 
            // collationSchemaData
            // 
            collationSchemaData.AutoSize = true;
            characterDataLayout.SetColumnSpan(collationSchemaData, 2);
            collationSchemaData.Dock = DockStyle.Fill;
            collationSchemaData.HeaderText = "Collation Schema";
            collationSchemaData.Location = new Point(3, 253);
            collationSchemaData.Multiline = false;
            collationSchemaData.Name = "collationSchemaData";
            collationSchemaData.ReadOnly = true;
            collationSchemaData.Size = new Size(461, 44);
            collationSchemaData.TabIndex = 20;
            // 
            // collationNameData
            // 
            collationNameData.AutoSize = true;
            characterDataLayout.SetColumnSpan(collationNameData, 2);
            collationNameData.Dock = DockStyle.Fill;
            collationNameData.HeaderText = "Collation (sort) Name";
            collationNameData.Location = new Point(3, 303);
            collationNameData.Multiline = false;
            collationNameData.Name = "collationNameData";
            collationNameData.ReadOnly = true;
            collationNameData.Size = new Size(461, 44);
            collationNameData.TabIndex = 21;
            // 
            // extendedPropertiesTab
            // 
            extendedPropertiesTab.Controls.Add(extendedPropertiesData);
            extendedPropertiesTab.Location = new Point(4, 24);
            extendedPropertiesTab.Name = "extendedPropertiesTab";
            extendedPropertiesTab.Padding = new Padding(3);
            extendedPropertiesTab.Size = new Size(192, 72);
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
            extendedPropertiesData.Size = new Size(186, 66);
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
            // catalogNameData
            // 
            catalogNameData.AutoSize = true;
            catalogNameData.Dock = DockStyle.Fill;
            catalogNameData.HeaderText = "Catalog Name";
            catalogNameData.Location = new Point(3, 3);
            catalogNameData.Multiline = false;
            catalogNameData.Name = "catalogNameData";
            catalogNameData.ReadOnly = true;
            catalogNameData.Size = new Size(495, 44);
            catalogNameData.TabIndex = 10;
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
            schemaNameData.Size = new Size(495, 44);
            schemaNameData.TabIndex = 11;
            // 
            // domainNameData
            // 
            domainNameData.AutoSize = true;
            domainNameData.Dock = DockStyle.Fill;
            domainNameData.HeaderText = "Domain Name";
            domainNameData.Location = new Point(3, 103);
            domainNameData.Multiline = false;
            domainNameData.Name = "domainNameData";
            domainNameData.ReadOnly = true;
            domainNameData.Size = new Size(495, 44);
            domainNameData.TabIndex = 12;
            // 
            // DbDomain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(501, 580);
            Controls.Add(dbTableLayout);
            Name = "DbDomain";
            Text = "DbDomain";
            Load += DbDomain_Load;
            Controls.SetChildIndex(dbTableLayout, 0);
            dbTableLayout.ResumeLayout(false);
            dbTableLayout.PerformLayout();
            domainDetailLayout.ResumeLayout(false);
            domainTab.ResumeLayout(false);
            dataTypeDetailTab.ResumeLayout(false);
            generalDataTab.ResumeLayout(false);
            columnGeneralDataLayout.ResumeLayout(false);
            columnGeneralDataLayout.PerformLayout();
            characterDataTab.ResumeLayout(false);
            characterDataLayout.ResumeLayout(false);
            characterDataLayout.PerformLayout();
            extendedPropertiesTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)extendedPropertiesData).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TabPage generalDataTab;
        private TableLayoutPanel columnGeneralDataLayout;
        private Controls.TextBoxData dataTypeData;
        private TabPage characterDataTab;
        private TableLayoutPanel characterDataLayout;
        private Controls.TextBoxData characterMaximumLengthData;
        private Controls.TextBoxData characterOctetLengthData;
        private Controls.TextBoxData characterSetCatalogData;
        private Controls.TextBoxData characterSetSchemaData;
        private Controls.TextBoxData characterSetNameData;
        private Controls.TextBoxData collationCatalogData;
        private Controls.TextBoxData collationSchemaData;
        private Controls.TextBoxData collationNameData;
        private DataGridView extendedPropertiesData;
        private DataGridViewTextBoxColumn propertyNameData;
        private DataGridViewTextBoxColumn propertyValueData;
        private Controls.TextBoxData catalogNameData;
        private Controls.TextBoxData schemaNameData;
        private Controls.TextBoxData domainNameData;
        private Controls.TextBoxData columnDefaultData;
        private Controls.TextBoxData dateTimePrecisionData;
        private Controls.TextBoxData numericScaleData;
        private Controls.TextBoxData numericPrecisionRadixData;
        private Controls.TextBoxData numericPrecisionData;
    }
}