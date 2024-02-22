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
            components = new System.ComponentModel.Container();
            TableLayoutPanel dbTableLayout;
            TabControl domainDetailLayout;
            TabPage domainTab;
            TabControl dataTypeDetailTab;
            GroupBox characterGroup;
            TableLayoutPanel charaterGroupLayout;
            GroupBox numericGroup;
            GroupBox dateTimeGroup;
            TableLayoutPanel dateTimeGroupLayout;
            TableLayoutPanel defaultLayout;
            TabPage extendedPropertiesTab;
            generalDataTab = new TabPage();
            columnGeneralDataLayout = new TableLayoutPanel();
            dataTypeData = new Controls.TextBoxData();
            characterMaximumLengthData = new Controls.TextBoxData();
            characterOctetLengthData = new Controls.TextBoxData();
            tableLayoutPanel1 = new TableLayoutPanel();
            numericScaleData = new Controls.TextBoxData();
            numericPrecisionData = new Controls.TextBoxData();
            numericPrecisionRadixData = new Controls.TextBoxData();
            dateTimePrecisionData = new Controls.TextBoxData();
            defaultComputedTab = new TabPage();
            domainDefaultData = new Controls.TextBoxData();
            characterDataTab = new TabPage();
            characterDataLayout = new TableLayoutPanel();
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
            bindingDomain = new BindingSource(components);
            bindingProperties = new BindingSource(components);
            dbTableLayout = new TableLayoutPanel();
            domainDetailLayout = new TabControl();
            domainTab = new TabPage();
            dataTypeDetailTab = new TabControl();
            characterGroup = new GroupBox();
            charaterGroupLayout = new TableLayoutPanel();
            numericGroup = new GroupBox();
            dateTimeGroup = new GroupBox();
            dateTimeGroupLayout = new TableLayoutPanel();
            defaultLayout = new TableLayoutPanel();
            extendedPropertiesTab = new TabPage();
            dbTableLayout.SuspendLayout();
            domainDetailLayout.SuspendLayout();
            domainTab.SuspendLayout();
            dataTypeDetailTab.SuspendLayout();
            generalDataTab.SuspendLayout();
            columnGeneralDataLayout.SuspendLayout();
            characterGroup.SuspendLayout();
            charaterGroupLayout.SuspendLayout();
            numericGroup.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            dateTimeGroup.SuspendLayout();
            dateTimeGroupLayout.SuspendLayout();
            defaultComputedTab.SuspendLayout();
            defaultLayout.SuspendLayout();
            characterDataTab.SuspendLayout();
            characterDataLayout.SuspendLayout();
            extendedPropertiesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)extendedPropertiesData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingDomain).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingProperties).BeginInit();
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
            dbTableLayout.Size = new Size(610, 540);
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
            domainDetailLayout.Size = new Size(604, 384);
            domainDetailLayout.TabIndex = 9;
            // 
            // domainTab
            // 
            domainTab.BackColor = SystemColors.Control;
            domainTab.Controls.Add(dataTypeDetailTab);
            domainTab.Location = new Point(4, 24);
            domainTab.Name = "domainTab";
            domainTab.Padding = new Padding(3);
            domainTab.Size = new Size(596, 356);
            domainTab.TabIndex = 1;
            domainTab.Text = "Domain Detail";
            // 
            // dataTypeDetailTab
            // 
            dataTypeDetailTab.Controls.Add(generalDataTab);
            dataTypeDetailTab.Controls.Add(defaultComputedTab);
            dataTypeDetailTab.Controls.Add(characterDataTab);
            dataTypeDetailTab.Dock = DockStyle.Fill;
            dataTypeDetailTab.Location = new Point(3, 3);
            dataTypeDetailTab.Name = "dataTypeDetailTab";
            dataTypeDetailTab.SelectedIndex = 0;
            dataTypeDetailTab.Size = new Size(590, 350);
            dataTypeDetailTab.TabIndex = 1;
            // 
            // generalDataTab
            // 
            generalDataTab.BackColor = SystemColors.Control;
            generalDataTab.Controls.Add(columnGeneralDataLayout);
            generalDataTab.Location = new Point(4, 24);
            generalDataTab.Name = "generalDataTab";
            generalDataTab.Size = new Size(582, 322);
            generalDataTab.TabIndex = 3;
            generalDataTab.Text = "General";
            // 
            // columnGeneralDataLayout
            // 
            columnGeneralDataLayout.ColumnCount = 3;
            columnGeneralDataLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            columnGeneralDataLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            columnGeneralDataLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            columnGeneralDataLayout.Controls.Add(dataTypeData, 0, 0);
            columnGeneralDataLayout.Controls.Add(characterGroup, 0, 1);
            columnGeneralDataLayout.Controls.Add(numericGroup, 1, 1);
            columnGeneralDataLayout.Controls.Add(dateTimeGroup, 2, 1);
            columnGeneralDataLayout.Dock = DockStyle.Fill;
            columnGeneralDataLayout.Location = new Point(0, 0);
            columnGeneralDataLayout.Name = "columnGeneralDataLayout";
            columnGeneralDataLayout.RowCount = 3;
            columnGeneralDataLayout.RowStyles.Add(new RowStyle());
            columnGeneralDataLayout.RowStyles.Add(new RowStyle());
            columnGeneralDataLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            columnGeneralDataLayout.Size = new Size(582, 322);
            columnGeneralDataLayout.TabIndex = 1;
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
            dataTypeData.Size = new Size(382, 44);
            dataTypeData.TabIndex = 12;
            // 
            // characterGroup
            // 
            characterGroup.Controls.Add(charaterGroupLayout);
            characterGroup.Dock = DockStyle.Fill;
            characterGroup.Location = new Point(3, 53);
            characterGroup.Name = "characterGroup";
            characterGroup.Size = new Size(188, 173);
            characterGroup.TabIndex = 21;
            characterGroup.TabStop = false;
            characterGroup.Text = "Character data types";
            // 
            // charaterGroupLayout
            // 
            charaterGroupLayout.ColumnCount = 1;
            charaterGroupLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            charaterGroupLayout.Controls.Add(characterMaximumLengthData, 0, 0);
            charaterGroupLayout.Controls.Add(characterOctetLengthData, 0, 1);
            charaterGroupLayout.Dock = DockStyle.Fill;
            charaterGroupLayout.Location = new Point(3, 19);
            charaterGroupLayout.Name = "charaterGroupLayout";
            charaterGroupLayout.RowCount = 3;
            charaterGroupLayout.RowStyles.Add(new RowStyle());
            charaterGroupLayout.RowStyles.Add(new RowStyle());
            charaterGroupLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            charaterGroupLayout.Size = new Size(182, 151);
            charaterGroupLayout.TabIndex = 0;
            // 
            // characterMaximumLengthData
            // 
            characterMaximumLengthData.AutoSize = true;
            characterMaximumLengthData.Dock = DockStyle.Fill;
            characterMaximumLengthData.HeaderText = "Maximum Length";
            characterMaximumLengthData.Location = new Point(3, 3);
            characterMaximumLengthData.Multiline = false;
            characterMaximumLengthData.Name = "characterMaximumLengthData";
            characterMaximumLengthData.ReadOnly = true;
            characterMaximumLengthData.Size = new Size(176, 44);
            characterMaximumLengthData.TabIndex = 13;
            // 
            // characterOctetLengthData
            // 
            characterOctetLengthData.AutoSize = true;
            characterOctetLengthData.Dock = DockStyle.Fill;
            characterOctetLengthData.HeaderText = "Octet Length";
            characterOctetLengthData.Location = new Point(3, 53);
            characterOctetLengthData.Multiline = false;
            characterOctetLengthData.Name = "characterOctetLengthData";
            characterOctetLengthData.ReadOnly = true;
            characterOctetLengthData.Size = new Size(176, 44);
            characterOctetLengthData.TabIndex = 14;
            // 
            // numericGroup
            // 
            numericGroup.Controls.Add(tableLayoutPanel1);
            numericGroup.Location = new Point(197, 53);
            numericGroup.Name = "numericGroup";
            numericGroup.Size = new Size(188, 173);
            numericGroup.TabIndex = 22;
            numericGroup.TabStop = false;
            numericGroup.Text = "Numeric data types";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(numericScaleData, 0, 2);
            tableLayoutPanel1.Controls.Add(numericPrecisionData, 0, 0);
            tableLayoutPanel1.Controls.Add(numericPrecisionRadixData, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 19);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(182, 151);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // numericScaleData
            // 
            numericScaleData.AutoSize = true;
            numericScaleData.Dock = DockStyle.Fill;
            numericScaleData.HeaderText = "Numeric Scale";
            numericScaleData.Location = new Point(3, 103);
            numericScaleData.Multiline = false;
            numericScaleData.Name = "numericScaleData";
            numericScaleData.ReadOnly = true;
            numericScaleData.Size = new Size(176, 44);
            numericScaleData.TabIndex = 19;
            // 
            // numericPrecisionData
            // 
            numericPrecisionData.AutoSize = true;
            numericPrecisionData.Dock = DockStyle.Fill;
            numericPrecisionData.HeaderText = "Numeric Precision";
            numericPrecisionData.Location = new Point(3, 3);
            numericPrecisionData.Multiline = false;
            numericPrecisionData.Name = "numericPrecisionData";
            numericPrecisionData.ReadOnly = true;
            numericPrecisionData.Size = new Size(176, 44);
            numericPrecisionData.TabIndex = 17;
            // 
            // numericPrecisionRadixData
            // 
            numericPrecisionRadixData.AutoSize = true;
            numericPrecisionRadixData.Dock = DockStyle.Fill;
            numericPrecisionRadixData.HeaderText = "Numeric Precision Radix";
            numericPrecisionRadixData.Location = new Point(3, 53);
            numericPrecisionRadixData.Multiline = false;
            numericPrecisionRadixData.Name = "numericPrecisionRadixData";
            numericPrecisionRadixData.ReadOnly = true;
            numericPrecisionRadixData.Size = new Size(176, 44);
            numericPrecisionRadixData.TabIndex = 18;
            // 
            // dateTimeGroup
            // 
            dateTimeGroup.Controls.Add(dateTimeGroupLayout);
            dateTimeGroup.Dock = DockStyle.Fill;
            dateTimeGroup.Location = new Point(391, 53);
            dateTimeGroup.Name = "dateTimeGroup";
            dateTimeGroup.Size = new Size(188, 173);
            dateTimeGroup.TabIndex = 23;
            dateTimeGroup.TabStop = false;
            dateTimeGroup.Text = "Date Time data types";
            // 
            // dateTimeGroupLayout
            // 
            dateTimeGroupLayout.ColumnCount = 1;
            dateTimeGroupLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            dateTimeGroupLayout.Controls.Add(dateTimePrecisionData, 0, 0);
            dateTimeGroupLayout.Dock = DockStyle.Fill;
            dateTimeGroupLayout.Location = new Point(3, 19);
            dateTimeGroupLayout.Name = "dateTimeGroupLayout";
            dateTimeGroupLayout.RowCount = 2;
            dateTimeGroupLayout.RowStyles.Add(new RowStyle());
            dateTimeGroupLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            dateTimeGroupLayout.Size = new Size(182, 151);
            dateTimeGroupLayout.TabIndex = 0;
            // 
            // dateTimePrecisionData
            // 
            dateTimePrecisionData.AutoSize = true;
            dateTimePrecisionData.Dock = DockStyle.Fill;
            dateTimePrecisionData.HeaderText = "Date Time Precision";
            dateTimePrecisionData.Location = new Point(3, 3);
            dateTimePrecisionData.Multiline = false;
            dateTimePrecisionData.Name = "dateTimePrecisionData";
            dateTimePrecisionData.ReadOnly = true;
            dateTimePrecisionData.Size = new Size(176, 44);
            dateTimePrecisionData.TabIndex = 20;
            // 
            // defaultComputedTab
            // 
            defaultComputedTab.BackColor = SystemColors.Control;
            defaultComputedTab.Controls.Add(defaultLayout);
            defaultComputedTab.Location = new Point(4, 24);
            defaultComputedTab.Name = "defaultComputedTab";
            defaultComputedTab.Size = new Size(582, 322);
            defaultComputedTab.TabIndex = 4;
            defaultComputedTab.Text = "Default";
            // 
            // defaultLayout
            // 
            defaultLayout.ColumnCount = 1;
            defaultLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            defaultLayout.Controls.Add(domainDefaultData, 0, 0);
            defaultLayout.Dock = DockStyle.Fill;
            defaultLayout.Location = new Point(0, 0);
            defaultLayout.Name = "defaultLayout";
            defaultLayout.RowCount = 2;
            defaultLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            defaultLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            defaultLayout.Size = new Size(582, 322);
            defaultLayout.TabIndex = 0;
            // 
            // domainDefaultData
            // 
            domainDefaultData.AutoSize = true;
            defaultLayout.SetColumnSpan(domainDefaultData, 3);
            domainDefaultData.Dock = DockStyle.Fill;
            domainDefaultData.HeaderText = "Default";
            domainDefaultData.Location = new Point(3, 3);
            domainDefaultData.Multiline = true;
            domainDefaultData.Name = "domainDefaultData";
            domainDefaultData.ReadOnly = true;
            domainDefaultData.Size = new Size(576, 155);
            domainDefaultData.TabIndex = 16;
            // 
            // characterDataTab
            // 
            characterDataTab.BackColor = SystemColors.Control;
            characterDataTab.Controls.Add(characterDataLayout);
            characterDataTab.Location = new Point(4, 24);
            characterDataTab.Name = "characterDataTab";
            characterDataTab.Padding = new Padding(3);
            characterDataTab.Size = new Size(582, 322);
            characterDataTab.TabIndex = 0;
            characterDataTab.Text = "Character";
            // 
            // characterDataLayout
            // 
            characterDataLayout.ColumnCount = 1;
            characterDataLayout.ColumnStyles.Add(new ColumnStyle());
            characterDataLayout.Controls.Add(characterSetCatalogData, 0, 0);
            characterDataLayout.Controls.Add(characterSetSchemaData, 0, 1);
            characterDataLayout.Controls.Add(characterSetNameData, 0, 2);
            characterDataLayout.Controls.Add(collationCatalogData, 0, 3);
            characterDataLayout.Controls.Add(collationSchemaData, 0, 4);
            characterDataLayout.Controls.Add(collationNameData, 0, 5);
            characterDataLayout.Dock = DockStyle.Fill;
            characterDataLayout.Location = new Point(3, 3);
            characterDataLayout.Name = "characterDataLayout";
            characterDataLayout.RowCount = 7;
            characterDataLayout.RowStyles.Add(new RowStyle());
            characterDataLayout.RowStyles.Add(new RowStyle());
            characterDataLayout.RowStyles.Add(new RowStyle());
            characterDataLayout.RowStyles.Add(new RowStyle());
            characterDataLayout.RowStyles.Add(new RowStyle());
            characterDataLayout.RowStyles.Add(new RowStyle());
            characterDataLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            characterDataLayout.Size = new Size(576, 316);
            characterDataLayout.TabIndex = 3;
            // 
            // characterSetCatalogData
            // 
            characterSetCatalogData.AutoSize = true;
            characterSetCatalogData.Dock = DockStyle.Fill;
            characterSetCatalogData.HeaderText = "Character Set Catalog";
            characterSetCatalogData.Location = new Point(3, 3);
            characterSetCatalogData.Multiline = false;
            characterSetCatalogData.Name = "characterSetCatalogData";
            characterSetCatalogData.ReadOnly = true;
            characterSetCatalogData.Size = new Size(570, 44);
            characterSetCatalogData.TabIndex = 14;
            // 
            // characterSetSchemaData
            // 
            characterSetSchemaData.AutoSize = true;
            characterSetSchemaData.Dock = DockStyle.Fill;
            characterSetSchemaData.HeaderText = "Character Set Schema";
            characterSetSchemaData.Location = new Point(3, 53);
            characterSetSchemaData.Multiline = false;
            characterSetSchemaData.Name = "characterSetSchemaData";
            characterSetSchemaData.ReadOnly = true;
            characterSetSchemaData.Size = new Size(570, 44);
            characterSetSchemaData.TabIndex = 15;
            // 
            // characterSetNameData
            // 
            characterSetNameData.AutoSize = true;
            characterSetNameData.Dock = DockStyle.Fill;
            characterSetNameData.HeaderText = "Character Set Name";
            characterSetNameData.Location = new Point(3, 103);
            characterSetNameData.Multiline = false;
            characterSetNameData.Name = "characterSetNameData";
            characterSetNameData.ReadOnly = true;
            characterSetNameData.Size = new Size(570, 44);
            characterSetNameData.TabIndex = 18;
            // 
            // collationCatalogData
            // 
            collationCatalogData.AutoSize = true;
            collationCatalogData.Dock = DockStyle.Fill;
            collationCatalogData.HeaderText = "Collation Catalog";
            collationCatalogData.Location = new Point(3, 153);
            collationCatalogData.Multiline = false;
            collationCatalogData.Name = "collationCatalogData";
            collationCatalogData.ReadOnly = true;
            collationCatalogData.Size = new Size(570, 44);
            collationCatalogData.TabIndex = 19;
            // 
            // collationSchemaData
            // 
            collationSchemaData.AutoSize = true;
            collationSchemaData.Dock = DockStyle.Fill;
            collationSchemaData.HeaderText = "Collation Schema";
            collationSchemaData.Location = new Point(3, 203);
            collationSchemaData.Multiline = false;
            collationSchemaData.Name = "collationSchemaData";
            collationSchemaData.ReadOnly = true;
            collationSchemaData.Size = new Size(570, 44);
            collationSchemaData.TabIndex = 20;
            // 
            // collationNameData
            // 
            collationNameData.AutoSize = true;
            collationNameData.Dock = DockStyle.Fill;
            collationNameData.HeaderText = "Collation (sort) Name";
            collationNameData.Location = new Point(3, 253);
            collationNameData.Multiline = false;
            collationNameData.Name = "collationNameData";
            collationNameData.ReadOnly = true;
            collationNameData.Size = new Size(570, 44);
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
            catalogNameData.Size = new Size(604, 44);
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
            schemaNameData.Size = new Size(604, 44);
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
            domainNameData.Size = new Size(604, 44);
            domainNameData.TabIndex = 12;
            // 
            // DbDomain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(610, 565);
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
            characterGroup.ResumeLayout(false);
            charaterGroupLayout.ResumeLayout(false);
            charaterGroupLayout.PerformLayout();
            numericGroup.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            dateTimeGroup.ResumeLayout(false);
            dateTimeGroupLayout.ResumeLayout(false);
            dateTimeGroupLayout.PerformLayout();
            defaultComputedTab.ResumeLayout(false);
            defaultLayout.ResumeLayout(false);
            defaultLayout.PerformLayout();
            characterDataTab.ResumeLayout(false);
            characterDataLayout.ResumeLayout(false);
            characterDataLayout.PerformLayout();
            extendedPropertiesTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)extendedPropertiesData).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingDomain).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingProperties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TabPage generalDataTab;
        private TableLayoutPanel columnGeneralDataLayout;
        private Controls.TextBoxData dataTypeData;
        private TabPage characterDataTab;
        private TableLayoutPanel characterDataLayout;
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
        private Controls.TextBoxData dateTimePrecisionData;
        private Controls.TextBoxData numericScaleData;
        private Controls.TextBoxData numericPrecisionRadixData;
        private Controls.TextBoxData numericPrecisionData;
        private Controls.TextBoxData characterMaximumLengthData;
        private Controls.TextBoxData characterOctetLengthData;
        private TableLayoutPanel tableLayoutPanel1;
        private TabPage defaultComputedTab;
        private Controls.TextBoxData domainDefaultData;
        private BindingSource bindingDomain;
        private BindingSource bindingProperties;
    }
}