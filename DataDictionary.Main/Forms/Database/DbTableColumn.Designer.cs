namespace DataDictionary.Main.Forms.Database
{
    partial class DbTableColumn
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
            TabControl columnDetailLayout;
            TabPage extendedPropertiesTab;
            TabPage columnsTab;
            TabControl dataTypeDetailTab;
            TableLayoutPanel columnFlagsLayout;
            extendedPropertiesData = new DataGridView();
            propertyNameData = new DataGridViewTextBoxColumn();
            propertyValueData = new DataGridViewTextBoxColumn();
            generalDataTab = new TabPage();
            columnGeneralDataLayout = new TableLayoutPanel();
            ordinalPositionData = new Controls.TextBoxData();
            dataTypeData = new Controls.TextBoxData();
            columnDefaultData = new Controls.TextBoxData();
            isNullableData = new CheckBox();
            isComputedData = new CheckBox();
            columnComputedData = new Controls.TextBoxData();
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
            otherDataTab = new TabPage();
            numericDataLayout = new TableLayoutPanel();
            isIdentityData = new CheckBox();
            isHiddenData = new CheckBox();
            numericPrecisionData = new Controls.TextBoxData();
            numericPrecisionRadixData = new Controls.TextBoxData();
            numericScaleData = new Controls.TextBoxData();
            dateTimePrecisionData = new Controls.TextBoxData();
            generatedAlwayTypeData = new Controls.TextBoxData();
            domainCatalogData = new Controls.TextBoxData();
            domainSchemaData = new Controls.TextBoxData();
            domainNameData = new Controls.TextBoxData();
            catalogNameData = new Controls.TextBoxData();
            schemaNameData = new Controls.TextBoxData();
            tableNameData = new Controls.TextBoxData();
            columnNameData = new Controls.TextBoxData();
            errorProvider = new ErrorProvider(components);
            dbTableLayout = new TableLayoutPanel();
            columnDetailLayout = new TabControl();
            extendedPropertiesTab = new TabPage();
            columnsTab = new TabPage();
            dataTypeDetailTab = new TabControl();
            columnFlagsLayout = new TableLayoutPanel();
            dbTableLayout.SuspendLayout();
            columnDetailLayout.SuspendLayout();
            extendedPropertiesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)extendedPropertiesData).BeginInit();
            columnsTab.SuspendLayout();
            dataTypeDetailTab.SuspendLayout();
            generalDataTab.SuspendLayout();
            columnGeneralDataLayout.SuspendLayout();
            columnFlagsLayout.SuspendLayout();
            characterDataTab.SuspendLayout();
            characterDataLayout.SuspendLayout();
            otherDataTab.SuspendLayout();
            numericDataLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // dbTableLayout
            // 
            dbTableLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            dbTableLayout.ColumnCount = 1;
            dbTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            dbTableLayout.Controls.Add(columnDetailLayout, 0, 4);
            dbTableLayout.Controls.Add(catalogNameData, 0, 0);
            dbTableLayout.Controls.Add(schemaNameData, 0, 1);
            dbTableLayout.Controls.Add(tableNameData, 0, 2);
            dbTableLayout.Controls.Add(columnNameData, 0, 3);
            dbTableLayout.Dock = DockStyle.Fill;
            dbTableLayout.Location = new Point(0, 0);
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
            dbTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            dbTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            dbTableLayout.Size = new Size(486, 645);
            dbTableLayout.TabIndex = 1;
            // 
            // columnDetailLayout
            // 
            columnDetailLayout.Controls.Add(columnsTab);
            columnDetailLayout.Controls.Add(extendedPropertiesTab);
            columnDetailLayout.Dock = DockStyle.Fill;
            columnDetailLayout.Location = new Point(3, 203);
            columnDetailLayout.Name = "columnDetailLayout";
            columnDetailLayout.SelectedIndex = 0;
            columnDetailLayout.Size = new Size(480, 439);
            columnDetailLayout.TabIndex = 9;
            // 
            // extendedPropertiesTab
            // 
            extendedPropertiesTab.Controls.Add(extendedPropertiesData);
            extendedPropertiesTab.Location = new Point(4, 24);
            extendedPropertiesTab.Name = "extendedPropertiesTab";
            extendedPropertiesTab.Padding = new Padding(3);
            extendedPropertiesTab.Size = new Size(472, 411);
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
            extendedPropertiesData.Size = new Size(466, 405);
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
            columnsTab.BackColor = SystemColors.Control;
            columnsTab.Controls.Add(dataTypeDetailTab);
            columnsTab.Location = new Point(4, 24);
            columnsTab.Name = "columnsTab";
            columnsTab.Padding = new Padding(3);
            columnsTab.Size = new Size(472, 411);
            columnsTab.TabIndex = 1;
            columnsTab.Text = "Column Detail";
            // 
            // dataTypeDetailTab
            // 
            dataTypeDetailTab.Controls.Add(generalDataTab);
            dataTypeDetailTab.Controls.Add(characterDataTab);
            dataTypeDetailTab.Controls.Add(otherDataTab);
            dataTypeDetailTab.Dock = DockStyle.Fill;
            dataTypeDetailTab.Location = new Point(3, 3);
            dataTypeDetailTab.Name = "dataTypeDetailTab";
            dataTypeDetailTab.SelectedIndex = 0;
            dataTypeDetailTab.Size = new Size(466, 405);
            dataTypeDetailTab.TabIndex = 1;
            // 
            // generalDataTab
            // 
            generalDataTab.BackColor = SystemColors.Control;
            generalDataTab.Controls.Add(columnGeneralDataLayout);
            generalDataTab.Location = new Point(4, 24);
            generalDataTab.Name = "generalDataTab";
            generalDataTab.Size = new Size(458, 377);
            generalDataTab.TabIndex = 3;
            generalDataTab.Text = "General";
            // 
            // columnGeneralDataLayout
            // 
            columnGeneralDataLayout.ColumnCount = 3;
            columnGeneralDataLayout.ColumnStyles.Add(new ColumnStyle());
            columnGeneralDataLayout.ColumnStyles.Add(new ColumnStyle());
            columnGeneralDataLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            columnGeneralDataLayout.Controls.Add(ordinalPositionData, 0, 0);
            columnGeneralDataLayout.Controls.Add(dataTypeData, 1, 0);
            columnGeneralDataLayout.Controls.Add(columnDefaultData, 0, 1);
            columnGeneralDataLayout.Controls.Add(columnFlagsLayout, 2, 0);
            columnGeneralDataLayout.Controls.Add(columnComputedData, 0, 2);
            columnGeneralDataLayout.Dock = DockStyle.Fill;
            columnGeneralDataLayout.Location = new Point(0, 0);
            columnGeneralDataLayout.Name = "columnGeneralDataLayout";
            columnGeneralDataLayout.RowCount = 3;
            columnGeneralDataLayout.RowStyles.Add(new RowStyle());
            columnGeneralDataLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            columnGeneralDataLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            columnGeneralDataLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            columnGeneralDataLayout.Size = new Size(458, 377);
            columnGeneralDataLayout.TabIndex = 1;
            // 
            // ordinalPositionData
            // 
            ordinalPositionData.AutoSize = true;
            ordinalPositionData.Dock = DockStyle.Fill;
            ordinalPositionData.HeaderText = "Ordinal Position";
            ordinalPositionData.Location = new Point(3, 3);
            ordinalPositionData.Multiline = false;
            ordinalPositionData.Name = "ordinalPositionData";
            ordinalPositionData.ReadOnly = true;
            ordinalPositionData.Size = new Size(120, 50);
            ordinalPositionData.TabIndex = 11;
            // 
            // dataTypeData
            // 
            dataTypeData.AutoSize = true;
            dataTypeData.Dock = DockStyle.Fill;
            dataTypeData.HeaderText = "Data Type";
            dataTypeData.Location = new Point(129, 3);
            dataTypeData.Multiline = false;
            dataTypeData.Name = "dataTypeData";
            dataTypeData.ReadOnly = true;
            dataTypeData.Size = new Size(120, 50);
            dataTypeData.TabIndex = 12;
            // 
            // columnDefaultData
            // 
            columnDefaultData.AutoSize = true;
            columnGeneralDataLayout.SetColumnSpan(columnDefaultData, 3);
            columnDefaultData.Dock = DockStyle.Fill;
            columnDefaultData.HeaderText = "Default";
            columnDefaultData.Location = new Point(3, 59);
            columnDefaultData.Multiline = true;
            columnDefaultData.Name = "columnDefaultData";
            columnDefaultData.ReadOnly = true;
            columnDefaultData.Size = new Size(452, 154);
            columnDefaultData.TabIndex = 13;
            // 
            // columnFlagsLayout
            // 
            columnFlagsLayout.AutoSize = true;
            columnFlagsLayout.ColumnCount = 1;
            columnFlagsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            columnFlagsLayout.Controls.Add(isNullableData, 0, 0);
            columnFlagsLayout.Controls.Add(isComputedData, 0, 1);
            columnFlagsLayout.Dock = DockStyle.Fill;
            columnFlagsLayout.Location = new Point(255, 3);
            columnFlagsLayout.Name = "columnFlagsLayout";
            columnFlagsLayout.RowCount = 2;
            columnFlagsLayout.RowStyles.Add(new RowStyle());
            columnFlagsLayout.RowStyles.Add(new RowStyle());
            columnFlagsLayout.Size = new Size(200, 50);
            columnFlagsLayout.TabIndex = 14;
            // 
            // isNullableData
            // 
            isNullableData.AutoCheck = false;
            isNullableData.AutoSize = true;
            isNullableData.Location = new Point(3, 3);
            isNullableData.Name = "isNullableData";
            isNullableData.Size = new Size(81, 19);
            isNullableData.TabIndex = 5;
            isNullableData.Text = "Is Nullable";
            isNullableData.UseVisualStyleBackColor = true;
            // 
            // isComputedData
            // 
            isComputedData.AutoSize = true;
            isComputedData.Location = new Point(3, 28);
            isComputedData.Name = "isComputedData";
            isComputedData.Size = new Size(94, 19);
            isComputedData.TabIndex = 10;
            isComputedData.Text = "Is Computed";
            isComputedData.UseVisualStyleBackColor = true;
            // 
            // columnComputedData
            // 
            columnComputedData.AutoSize = true;
            columnGeneralDataLayout.SetColumnSpan(columnComputedData, 3);
            columnComputedData.Dock = DockStyle.Fill;
            columnComputedData.HeaderText = "Computed";
            columnComputedData.Location = new Point(3, 219);
            columnComputedData.Multiline = true;
            columnComputedData.Name = "columnComputedData";
            columnComputedData.ReadOnly = true;
            columnComputedData.Size = new Size(452, 155);
            columnComputedData.TabIndex = 15;
            // 
            // characterDataTab
            // 
            characterDataTab.BackColor = SystemColors.Control;
            characterDataTab.Controls.Add(characterDataLayout);
            characterDataTab.Location = new Point(4, 24);
            characterDataTab.Name = "characterDataTab";
            characterDataTab.Padding = new Padding(3);
            characterDataTab.Size = new Size(458, 377);
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
            characterDataLayout.Size = new Size(452, 371);
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
            characterSetCatalogData.Size = new Size(446, 44);
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
            characterSetSchemaData.Size = new Size(446, 44);
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
            characterSetNameData.Size = new Size(446, 44);
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
            collationCatalogData.Size = new Size(446, 44);
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
            collationSchemaData.Size = new Size(446, 44);
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
            collationNameData.Size = new Size(446, 44);
            collationNameData.TabIndex = 21;
            // 
            // otherDataTab
            // 
            otherDataTab.BackColor = SystemColors.Control;
            otherDataTab.Controls.Add(numericDataLayout);
            otherDataTab.Location = new Point(4, 24);
            otherDataTab.Name = "otherDataTab";
            otherDataTab.Padding = new Padding(3);
            otherDataTab.Size = new Size(458, 377);
            otherDataTab.TabIndex = 1;
            otherDataTab.Text = "Other (Numeric, Date, ...)";
            // 
            // numericDataLayout
            // 
            numericDataLayout.ColumnCount = 3;
            numericDataLayout.ColumnStyles.Add(new ColumnStyle());
            numericDataLayout.ColumnStyles.Add(new ColumnStyle());
            numericDataLayout.ColumnStyles.Add(new ColumnStyle());
            numericDataLayout.Controls.Add(isIdentityData, 0, 0);
            numericDataLayout.Controls.Add(isHiddenData, 1, 0);
            numericDataLayout.Controls.Add(numericPrecisionData, 0, 1);
            numericDataLayout.Controls.Add(numericPrecisionRadixData, 1, 1);
            numericDataLayout.Controls.Add(numericScaleData, 2, 1);
            numericDataLayout.Controls.Add(dateTimePrecisionData, 0, 2);
            numericDataLayout.Controls.Add(generatedAlwayTypeData, 1, 2);
            numericDataLayout.Controls.Add(domainCatalogData, 0, 3);
            numericDataLayout.Controls.Add(domainSchemaData, 0, 4);
            numericDataLayout.Controls.Add(domainNameData, 0, 5);
            numericDataLayout.Dock = DockStyle.Fill;
            numericDataLayout.Location = new Point(3, 3);
            numericDataLayout.Name = "numericDataLayout";
            numericDataLayout.RowCount = 7;
            numericDataLayout.RowStyles.Add(new RowStyle());
            numericDataLayout.RowStyles.Add(new RowStyle());
            numericDataLayout.RowStyles.Add(new RowStyle());
            numericDataLayout.RowStyles.Add(new RowStyle());
            numericDataLayout.RowStyles.Add(new RowStyle());
            numericDataLayout.RowStyles.Add(new RowStyle());
            numericDataLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            numericDataLayout.Size = new Size(452, 371);
            numericDataLayout.TabIndex = 3;
            // 
            // isIdentityData
            // 
            isIdentityData.AutoCheck = false;
            isIdentityData.AutoSize = true;
            isIdentityData.Location = new Point(3, 3);
            isIdentityData.Name = "isIdentityData";
            isIdentityData.Size = new Size(77, 19);
            isIdentityData.TabIndex = 14;
            isIdentityData.Text = "Is Identity";
            isIdentityData.UseVisualStyleBackColor = true;
            // 
            // isHiddenData
            // 
            isHiddenData.AutoCheck = false;
            isHiddenData.AutoSize = true;
            isHiddenData.Location = new Point(145, 3);
            isHiddenData.Name = "isHiddenData";
            isHiddenData.Size = new Size(76, 19);
            isHiddenData.TabIndex = 15;
            isHiddenData.Text = "Is Hidden";
            isHiddenData.UseVisualStyleBackColor = true;
            // 
            // numericPrecisionData
            // 
            numericPrecisionData.AutoSize = true;
            numericPrecisionData.Dock = DockStyle.Fill;
            numericPrecisionData.HeaderText = "Numeric Precision";
            numericPrecisionData.Location = new Point(3, 28);
            numericPrecisionData.Multiline = false;
            numericPrecisionData.Name = "numericPrecisionData";
            numericPrecisionData.ReadOnly = true;
            numericPrecisionData.Size = new Size(136, 44);
            numericPrecisionData.TabIndex = 16;
            // 
            // numericPrecisionRadixData
            // 
            numericPrecisionRadixData.AutoSize = true;
            numericPrecisionRadixData.Dock = DockStyle.Fill;
            numericPrecisionRadixData.HeaderText = "Numeric Precision Radix";
            numericPrecisionRadixData.Location = new Point(145, 28);
            numericPrecisionRadixData.Multiline = false;
            numericPrecisionRadixData.Name = "numericPrecisionRadixData";
            numericPrecisionRadixData.ReadOnly = true;
            numericPrecisionRadixData.Size = new Size(161, 44);
            numericPrecisionRadixData.TabIndex = 17;
            // 
            // numericScaleData
            // 
            numericScaleData.AutoSize = true;
            numericScaleData.Dock = DockStyle.Fill;
            numericScaleData.HeaderText = "Numeric Scale";
            numericScaleData.Location = new Point(312, 28);
            numericScaleData.Multiline = false;
            numericScaleData.Name = "numericScaleData";
            numericScaleData.ReadOnly = true;
            numericScaleData.Size = new Size(137, 44);
            numericScaleData.TabIndex = 18;
            // 
            // dateTimePrecisionData
            // 
            dateTimePrecisionData.AutoSize = true;
            dateTimePrecisionData.Dock = DockStyle.Fill;
            dateTimePrecisionData.HeaderText = "Date Time Precision";
            dateTimePrecisionData.Location = new Point(3, 78);
            dateTimePrecisionData.Multiline = false;
            dateTimePrecisionData.Name = "dateTimePrecisionData";
            dateTimePrecisionData.ReadOnly = true;
            dateTimePrecisionData.Size = new Size(136, 44);
            dateTimePrecisionData.TabIndex = 19;
            // 
            // generatedAlwayTypeData
            // 
            generatedAlwayTypeData.AutoSize = true;
            generatedAlwayTypeData.Dock = DockStyle.Fill;
            generatedAlwayTypeData.HeaderText = "Generated Alway";
            generatedAlwayTypeData.Location = new Point(145, 78);
            generatedAlwayTypeData.Multiline = false;
            generatedAlwayTypeData.Name = "generatedAlwayTypeData";
            generatedAlwayTypeData.ReadOnly = true;
            generatedAlwayTypeData.Size = new Size(161, 44);
            generatedAlwayTypeData.TabIndex = 20;
            // 
            // domainCatalogData
            // 
            domainCatalogData.AutoSize = true;
            numericDataLayout.SetColumnSpan(domainCatalogData, 3);
            domainCatalogData.Dock = DockStyle.Fill;
            domainCatalogData.HeaderText = "Domain Catalog";
            domainCatalogData.Location = new Point(3, 128);
            domainCatalogData.Multiline = false;
            domainCatalogData.Name = "domainCatalogData";
            domainCatalogData.ReadOnly = true;
            domainCatalogData.Size = new Size(446, 44);
            domainCatalogData.TabIndex = 21;
            // 
            // domainSchemaData
            // 
            domainSchemaData.AutoSize = true;
            numericDataLayout.SetColumnSpan(domainSchemaData, 3);
            domainSchemaData.Dock = DockStyle.Fill;
            domainSchemaData.HeaderText = "Domain Schema";
            domainSchemaData.Location = new Point(3, 178);
            domainSchemaData.Multiline = false;
            domainSchemaData.Name = "domainSchemaData";
            domainSchemaData.ReadOnly = true;
            domainSchemaData.Size = new Size(446, 44);
            domainSchemaData.TabIndex = 22;
            // 
            // domainNameData
            // 
            domainNameData.AutoSize = true;
            numericDataLayout.SetColumnSpan(domainNameData, 3);
            domainNameData.Dock = DockStyle.Fill;
            domainNameData.HeaderText = "Domain (Data Type) Name";
            domainNameData.Location = new Point(3, 228);
            domainNameData.Multiline = false;
            domainNameData.Name = "domainNameData";
            domainNameData.ReadOnly = true;
            domainNameData.Size = new Size(446, 44);
            domainNameData.TabIndex = 23;
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
            catalogNameData.Size = new Size(480, 44);
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
            schemaNameData.Size = new Size(480, 44);
            schemaNameData.TabIndex = 11;
            // 
            // tableNameData
            // 
            tableNameData.AutoSize = true;
            tableNameData.Dock = DockStyle.Fill;
            tableNameData.HeaderText = "Table Name";
            tableNameData.Location = new Point(3, 103);
            tableNameData.Multiline = false;
            tableNameData.Name = "tableNameData";
            tableNameData.ReadOnly = true;
            tableNameData.Size = new Size(480, 44);
            tableNameData.TabIndex = 12;
            // 
            // columnNameData
            // 
            columnNameData.AutoSize = true;
            columnNameData.Dock = DockStyle.Fill;
            columnNameData.HeaderText = "Column Name";
            columnNameData.Location = new Point(3, 153);
            columnNameData.Multiline = false;
            columnNameData.Name = "columnNameData";
            columnNameData.ReadOnly = true;
            columnNameData.Size = new Size(480, 44);
            columnNameData.TabIndex = 13;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // DbColumn
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(486, 645);
            Controls.Add(dbTableLayout);
            Name = "DbColumn";
            Text = "Database Column";
            Load += DbColumn_Load;
            dbTableLayout.ResumeLayout(false);
            dbTableLayout.PerformLayout();
            columnDetailLayout.ResumeLayout(false);
            extendedPropertiesTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)extendedPropertiesData).EndInit();
            columnsTab.ResumeLayout(false);
            dataTypeDetailTab.ResumeLayout(false);
            generalDataTab.ResumeLayout(false);
            columnGeneralDataLayout.ResumeLayout(false);
            columnGeneralDataLayout.PerformLayout();
            columnFlagsLayout.ResumeLayout(false);
            columnFlagsLayout.PerformLayout();
            characterDataTab.ResumeLayout(false);
            characterDataLayout.ResumeLayout(false);
            characterDataLayout.PerformLayout();
            otherDataTab.ResumeLayout(false);
            numericDataLayout.ResumeLayout(false);
            numericDataLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DataGridView extendedPropertiesData;
        private DataGridViewTextBoxColumn propertyNameData;
        private DataGridViewTextBoxColumn propertyValueData;
        private TabPage characterDataTab;
        private TabPage otherDataTab;
        private TabPage generalDataTab;
        private TableLayoutPanel columnGeneralDataLayout;
        private CheckBox isNullableData;
        private TableLayoutPanel characterDataLayout;
        private TableLayoutPanel numericDataLayout;
        private CheckBox isComputedData;
        private CheckBox isIdentityData;
        private CheckBox isHiddenData;
        private Controls.TextBoxData catalogNameData;
        private Controls.TextBoxData schemaNameData;
        private Controls.TextBoxData tableNameData;
        private Controls.TextBoxData columnNameData;
        private Controls.TextBoxData ordinalPositionData;
        private Controls.TextBoxData dataTypeData;
        private Controls.TextBoxData columnDefaultData;
        private Controls.TextBoxData columnComputedData;
        private Controls.TextBoxData characterMaximumLengthData;
        private Controls.TextBoxData characterOctetLengthData;
        private Controls.TextBoxData characterSetCatalogData;
        private Controls.TextBoxData characterSetSchemaData;
        private Controls.TextBoxData characterSetNameData;
        private Controls.TextBoxData numericPrecisionData;
        private Controls.TextBoxData numericPrecisionRadixData;
        private Controls.TextBoxData numericScaleData;
        private Controls.TextBoxData dateTimePrecisionData;
        private Controls.TextBoxData generatedAlwayTypeData;
        private ErrorProvider errorProvider;
        private Controls.TextBoxData domainCatalogData;
        private Controls.TextBoxData domainSchemaData;
        private Controls.TextBoxData domainNameData;
        private Controls.TextBoxData collationCatalogData;
        private Controls.TextBoxData collationSchemaData;
        private Controls.TextBoxData collationNameData;
    }
}