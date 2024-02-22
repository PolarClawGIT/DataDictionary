namespace DataDictionary.Main.Forms.Database
{
    partial class DbRoutineParameter
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
            TabPage columnsTab;
            TabControl dataTypeDetailTab;
            TableLayoutPanel characterGroupLayout;
            TableLayoutPanel numericGroupLayout;
            GroupBox datetimeGroup;
            TableLayoutPanel dateTimeGroupLayout;
            TabPage extendedPropertiesTab;
            generalDataTab = new TabPage();
            columnGeneralDataLayout = new TableLayoutPanel();
            dataTypeData = new Controls.TextBoxData();
            characterGroup = new GroupBox();
            characterMaximumLengthData = new Controls.TextBoxData();
            characterOctetLengthData = new Controls.TextBoxData();
            numericGroup = new GroupBox();
            numericPrecisionData = new Controls.TextBoxData();
            numericPrecisionRadixData = new Controls.TextBoxData();
            numericScaleData = new Controls.TextBoxData();
            ordinalPositionData = new Controls.TextBoxData();
            dateTimePrecisionData = new Controls.TextBoxData();
            characterDataTab = new TabPage();
            characterDataLayout = new TableLayoutPanel();
            characterSetCatalogData = new Controls.TextBoxData();
            characterSetSchemaData = new Controls.TextBoxData();
            characterSetNameData = new Controls.TextBoxData();
            collationCatalogData = new Controls.TextBoxData();
            collationSchemaData = new Controls.TextBoxData();
            collationNameData = new Controls.TextBoxData();
            otherDataTab = new TabPage();
            numericDataLayout = new TableLayoutPanel();
            domainCatalogData = new Controls.TextBoxData();
            domainSchemaData = new Controls.TextBoxData();
            domainNameData = new Controls.TextBoxData();
            extendedPropertiesData = new DataGridView();
            propertyNameData = new DataGridViewTextBoxColumn();
            propertyValueData = new DataGridViewTextBoxColumn();
            catalogNameData = new Controls.TextBoxData();
            schemaNameData = new Controls.TextBoxData();
            routineNameData = new Controls.TextBoxData();
            parameterNameData = new Controls.TextBoxData();
            bindingParameter = new BindingSource(components);
            bindingProperties = new BindingSource(components);
            dbTableLayout = new TableLayoutPanel();
            columnDetailLayout = new TabControl();
            columnsTab = new TabPage();
            dataTypeDetailTab = new TabControl();
            characterGroupLayout = new TableLayoutPanel();
            numericGroupLayout = new TableLayoutPanel();
            datetimeGroup = new GroupBox();
            dateTimeGroupLayout = new TableLayoutPanel();
            extendedPropertiesTab = new TabPage();
            dbTableLayout.SuspendLayout();
            columnDetailLayout.SuspendLayout();
            columnsTab.SuspendLayout();
            dataTypeDetailTab.SuspendLayout();
            generalDataTab.SuspendLayout();
            columnGeneralDataLayout.SuspendLayout();
            characterGroup.SuspendLayout();
            characterGroupLayout.SuspendLayout();
            numericGroup.SuspendLayout();
            numericGroupLayout.SuspendLayout();
            datetimeGroup.SuspendLayout();
            dateTimeGroupLayout.SuspendLayout();
            characterDataTab.SuspendLayout();
            characterDataLayout.SuspendLayout();
            otherDataTab.SuspendLayout();
            numericDataLayout.SuspendLayout();
            extendedPropertiesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)extendedPropertiesData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingParameter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingProperties).BeginInit();
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
            dbTableLayout.Controls.Add(routineNameData, 0, 2);
            dbTableLayout.Controls.Add(parameterNameData, 0, 3);
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
            dbTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            dbTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            dbTableLayout.Size = new Size(657, 581);
            dbTableLayout.TabIndex = 2;
            // 
            // columnDetailLayout
            // 
            columnDetailLayout.Controls.Add(columnsTab);
            columnDetailLayout.Controls.Add(extendedPropertiesTab);
            columnDetailLayout.Dock = DockStyle.Fill;
            columnDetailLayout.Location = new Point(3, 203);
            columnDetailLayout.Name = "columnDetailLayout";
            columnDetailLayout.SelectedIndex = 0;
            columnDetailLayout.Size = new Size(651, 375);
            columnDetailLayout.TabIndex = 9;
            // 
            // columnsTab
            // 
            columnsTab.BackColor = SystemColors.Control;
            columnsTab.Controls.Add(dataTypeDetailTab);
            columnsTab.Location = new Point(4, 24);
            columnsTab.Name = "columnsTab";
            columnsTab.Padding = new Padding(3);
            columnsTab.Size = new Size(643, 347);
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
            dataTypeDetailTab.Size = new Size(637, 341);
            dataTypeDetailTab.TabIndex = 1;
            // 
            // generalDataTab
            // 
            generalDataTab.BackColor = SystemColors.Control;
            generalDataTab.Controls.Add(columnGeneralDataLayout);
            generalDataTab.Location = new Point(4, 24);
            generalDataTab.Name = "generalDataTab";
            generalDataTab.Size = new Size(629, 313);
            generalDataTab.TabIndex = 3;
            generalDataTab.Text = "General";
            // 
            // columnGeneralDataLayout
            // 
            columnGeneralDataLayout.ColumnCount = 3;
            columnGeneralDataLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            columnGeneralDataLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            columnGeneralDataLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            columnGeneralDataLayout.Controls.Add(dataTypeData, 0, 0);
            columnGeneralDataLayout.Controls.Add(characterGroup, 0, 1);
            columnGeneralDataLayout.Controls.Add(numericGroup, 1, 1);
            columnGeneralDataLayout.Controls.Add(ordinalPositionData, 2, 0);
            columnGeneralDataLayout.Controls.Add(datetimeGroup, 2, 1);
            columnGeneralDataLayout.Dock = DockStyle.Fill;
            columnGeneralDataLayout.Location = new Point(0, 0);
            columnGeneralDataLayout.Name = "columnGeneralDataLayout";
            columnGeneralDataLayout.RowCount = 3;
            columnGeneralDataLayout.RowStyles.Add(new RowStyle());
            columnGeneralDataLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            columnGeneralDataLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            columnGeneralDataLayout.Size = new Size(629, 313);
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
            dataTypeData.Size = new Size(412, 44);
            dataTypeData.TabIndex = 12;
            // 
            // characterGroup
            // 
            characterGroup.Controls.Add(characterGroupLayout);
            characterGroup.Dock = DockStyle.Fill;
            characterGroup.Location = new Point(3, 53);
            characterGroup.Name = "characterGroup";
            characterGroup.Size = new Size(203, 197);
            characterGroup.TabIndex = 15;
            characterGroup.TabStop = false;
            characterGroup.Text = "Character data types";
            // 
            // characterGroupLayout
            // 
            characterGroupLayout.ColumnCount = 1;
            characterGroupLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            characterGroupLayout.Controls.Add(characterMaximumLengthData, 0, 0);
            characterGroupLayout.Controls.Add(characterOctetLengthData, 0, 1);
            characterGroupLayout.Dock = DockStyle.Fill;
            characterGroupLayout.Location = new Point(3, 19);
            characterGroupLayout.Name = "characterGroupLayout";
            characterGroupLayout.RowCount = 3;
            characterGroupLayout.RowStyles.Add(new RowStyle());
            characterGroupLayout.RowStyles.Add(new RowStyle());
            characterGroupLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            characterGroupLayout.Size = new Size(197, 175);
            characterGroupLayout.TabIndex = 0;
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
            characterMaximumLengthData.Size = new Size(191, 44);
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
            characterOctetLengthData.Size = new Size(191, 44);
            characterOctetLengthData.TabIndex = 14;
            // 
            // numericGroup
            // 
            numericGroup.Controls.Add(numericGroupLayout);
            numericGroup.Dock = DockStyle.Fill;
            numericGroup.Location = new Point(212, 53);
            numericGroup.Name = "numericGroup";
            numericGroup.Size = new Size(203, 197);
            numericGroup.TabIndex = 16;
            numericGroup.TabStop = false;
            numericGroup.Text = "Numeric data types";
            // 
            // numericGroupLayout
            // 
            numericGroupLayout.ColumnCount = 1;
            numericGroupLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            numericGroupLayout.Controls.Add(numericPrecisionData, 0, 0);
            numericGroupLayout.Controls.Add(numericPrecisionRadixData, 0, 1);
            numericGroupLayout.Controls.Add(numericScaleData, 0, 2);
            numericGroupLayout.Dock = DockStyle.Fill;
            numericGroupLayout.Location = new Point(3, 19);
            numericGroupLayout.Name = "numericGroupLayout";
            numericGroupLayout.RowCount = 3;
            numericGroupLayout.RowStyles.Add(new RowStyle());
            numericGroupLayout.RowStyles.Add(new RowStyle());
            numericGroupLayout.RowStyles.Add(new RowStyle());
            numericGroupLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            numericGroupLayout.Size = new Size(197, 175);
            numericGroupLayout.TabIndex = 0;
            // 
            // numericPrecisionData
            // 
            numericPrecisionData.AutoSize = true;
            numericPrecisionData.Dock = DockStyle.Fill;
            numericPrecisionData.HeaderText = "Precision";
            numericPrecisionData.Location = new Point(3, 3);
            numericPrecisionData.Multiline = false;
            numericPrecisionData.Name = "numericPrecisionData";
            numericPrecisionData.ReadOnly = true;
            numericPrecisionData.Size = new Size(191, 44);
            numericPrecisionData.TabIndex = 17;
            // 
            // numericPrecisionRadixData
            // 
            numericPrecisionRadixData.AutoSize = true;
            numericPrecisionRadixData.Dock = DockStyle.Fill;
            numericPrecisionRadixData.HeaderText = "Precision Radix";
            numericPrecisionRadixData.Location = new Point(3, 53);
            numericPrecisionRadixData.Multiline = false;
            numericPrecisionRadixData.Name = "numericPrecisionRadixData";
            numericPrecisionRadixData.ReadOnly = true;
            numericPrecisionRadixData.Size = new Size(191, 44);
            numericPrecisionRadixData.TabIndex = 18;
            // 
            // numericScaleData
            // 
            numericScaleData.AutoSize = true;
            numericScaleData.Dock = DockStyle.Fill;
            numericScaleData.HeaderText = "Scale";
            numericScaleData.Location = new Point(3, 103);
            numericScaleData.Multiline = false;
            numericScaleData.Name = "numericScaleData";
            numericScaleData.ReadOnly = true;
            numericScaleData.Size = new Size(191, 69);
            numericScaleData.TabIndex = 19;
            // 
            // ordinalPositionData
            // 
            ordinalPositionData.AutoSize = true;
            ordinalPositionData.Dock = DockStyle.Fill;
            ordinalPositionData.HeaderText = "Ordinal Position";
            ordinalPositionData.Location = new Point(421, 3);
            ordinalPositionData.Multiline = false;
            ordinalPositionData.Name = "ordinalPositionData";
            ordinalPositionData.ReadOnly = true;
            ordinalPositionData.Size = new Size(205, 44);
            ordinalPositionData.TabIndex = 11;
            // 
            // datetimeGroup
            // 
            datetimeGroup.Controls.Add(dateTimeGroupLayout);
            datetimeGroup.Dock = DockStyle.Fill;
            datetimeGroup.Location = new Point(421, 53);
            datetimeGroup.Name = "datetimeGroup";
            datetimeGroup.Size = new Size(205, 197);
            datetimeGroup.TabIndex = 17;
            datetimeGroup.TabStop = false;
            datetimeGroup.Text = "Date Time data types";
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
            dateTimeGroupLayout.Size = new Size(199, 175);
            dateTimeGroupLayout.TabIndex = 0;
            // 
            // dateTimePrecisionData
            // 
            dateTimePrecisionData.AutoSize = true;
            dateTimePrecisionData.Dock = DockStyle.Fill;
            dateTimePrecisionData.HeaderText = "Precision";
            dateTimePrecisionData.Location = new Point(3, 3);
            dateTimePrecisionData.Multiline = false;
            dateTimePrecisionData.Name = "dateTimePrecisionData";
            dateTimePrecisionData.ReadOnly = true;
            dateTimePrecisionData.Size = new Size(193, 44);
            dateTimePrecisionData.TabIndex = 20;
            // 
            // characterDataTab
            // 
            characterDataTab.BackColor = SystemColors.Control;
            characterDataTab.Controls.Add(characterDataLayout);
            characterDataTab.Location = new Point(4, 24);
            characterDataTab.Name = "characterDataTab";
            characterDataTab.Padding = new Padding(3);
            characterDataTab.Size = new Size(629, 313);
            characterDataTab.TabIndex = 0;
            characterDataTab.Text = "Character";
            // 
            // characterDataLayout
            // 
            characterDataLayout.ColumnCount = 1;
            characterDataLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            characterDataLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
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
            characterDataLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            characterDataLayout.Size = new Size(623, 307);
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
            characterSetCatalogData.Size = new Size(617, 44);
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
            characterSetSchemaData.Size = new Size(617, 44);
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
            characterSetNameData.Size = new Size(617, 44);
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
            collationCatalogData.Size = new Size(617, 44);
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
            collationSchemaData.Size = new Size(617, 44);
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
            collationNameData.Size = new Size(617, 44);
            collationNameData.TabIndex = 21;
            // 
            // otherDataTab
            // 
            otherDataTab.BackColor = SystemColors.Control;
            otherDataTab.Controls.Add(numericDataLayout);
            otherDataTab.Location = new Point(4, 24);
            otherDataTab.Name = "otherDataTab";
            otherDataTab.Padding = new Padding(3);
            otherDataTab.Size = new Size(629, 313);
            otherDataTab.TabIndex = 1;
            otherDataTab.Text = "Other";
            // 
            // numericDataLayout
            // 
            numericDataLayout.ColumnCount = 1;
            numericDataLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            numericDataLayout.Controls.Add(domainCatalogData, 0, 0);
            numericDataLayout.Controls.Add(domainSchemaData, 0, 1);
            numericDataLayout.Controls.Add(domainNameData, 0, 2);
            numericDataLayout.Dock = DockStyle.Fill;
            numericDataLayout.Location = new Point(3, 3);
            numericDataLayout.Name = "numericDataLayout";
            numericDataLayout.RowCount = 4;
            numericDataLayout.RowStyles.Add(new RowStyle());
            numericDataLayout.RowStyles.Add(new RowStyle());
            numericDataLayout.RowStyles.Add(new RowStyle());
            numericDataLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            numericDataLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            numericDataLayout.Size = new Size(623, 307);
            numericDataLayout.TabIndex = 3;
            // 
            // domainCatalogData
            // 
            domainCatalogData.AutoSize = true;
            domainCatalogData.Dock = DockStyle.Fill;
            domainCatalogData.HeaderText = "Domain Catalog";
            domainCatalogData.Location = new Point(3, 3);
            domainCatalogData.Multiline = false;
            domainCatalogData.Name = "domainCatalogData";
            domainCatalogData.ReadOnly = true;
            domainCatalogData.Size = new Size(617, 44);
            domainCatalogData.TabIndex = 21;
            // 
            // domainSchemaData
            // 
            domainSchemaData.AutoSize = true;
            domainSchemaData.Dock = DockStyle.Fill;
            domainSchemaData.HeaderText = "Domain Schema";
            domainSchemaData.Location = new Point(3, 53);
            domainSchemaData.Multiline = false;
            domainSchemaData.Name = "domainSchemaData";
            domainSchemaData.ReadOnly = true;
            domainSchemaData.Size = new Size(617, 44);
            domainSchemaData.TabIndex = 22;
            // 
            // domainNameData
            // 
            domainNameData.AutoSize = true;
            domainNameData.Dock = DockStyle.Fill;
            domainNameData.HeaderText = "Domain (Data Type) Name";
            domainNameData.Location = new Point(3, 103);
            domainNameData.Multiline = false;
            domainNameData.Name = "domainNameData";
            domainNameData.ReadOnly = true;
            domainNameData.Size = new Size(617, 44);
            domainNameData.TabIndex = 23;
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
            catalogNameData.Size = new Size(651, 44);
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
            schemaNameData.Size = new Size(651, 44);
            schemaNameData.TabIndex = 11;
            // 
            // routineNameData
            // 
            routineNameData.AutoSize = true;
            routineNameData.Dock = DockStyle.Fill;
            routineNameData.HeaderText = "Routine Name";
            routineNameData.Location = new Point(3, 103);
            routineNameData.Multiline = false;
            routineNameData.Name = "routineNameData";
            routineNameData.ReadOnly = true;
            routineNameData.Size = new Size(651, 44);
            routineNameData.TabIndex = 12;
            // 
            // parameterNameData
            // 
            parameterNameData.AutoSize = true;
            parameterNameData.Dock = DockStyle.Fill;
            parameterNameData.HeaderText = "Parameter Name";
            parameterNameData.Location = new Point(3, 153);
            parameterNameData.Multiline = false;
            parameterNameData.Name = "parameterNameData";
            parameterNameData.ReadOnly = true;
            parameterNameData.Size = new Size(651, 44);
            parameterNameData.TabIndex = 13;
            // 
            // DbRoutineParameter
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(657, 606);
            Controls.Add(dbTableLayout);
            Name = "DbRoutineParameter";
            Text = "DbRoutineParameter";
            Load += DbRoutineParameter_Load;
            Controls.SetChildIndex(dbTableLayout, 0);
            dbTableLayout.ResumeLayout(false);
            dbTableLayout.PerformLayout();
            columnDetailLayout.ResumeLayout(false);
            columnsTab.ResumeLayout(false);
            dataTypeDetailTab.ResumeLayout(false);
            generalDataTab.ResumeLayout(false);
            columnGeneralDataLayout.ResumeLayout(false);
            columnGeneralDataLayout.PerformLayout();
            characterGroup.ResumeLayout(false);
            characterGroupLayout.ResumeLayout(false);
            characterGroupLayout.PerformLayout();
            numericGroup.ResumeLayout(false);
            numericGroupLayout.ResumeLayout(false);
            numericGroupLayout.PerformLayout();
            datetimeGroup.ResumeLayout(false);
            dateTimeGroupLayout.ResumeLayout(false);
            dateTimeGroupLayout.PerformLayout();
            characterDataTab.ResumeLayout(false);
            characterDataLayout.ResumeLayout(false);
            characterDataLayout.PerformLayout();
            otherDataTab.ResumeLayout(false);
            numericDataLayout.ResumeLayout(false);
            numericDataLayout.PerformLayout();
            extendedPropertiesTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)extendedPropertiesData).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingParameter).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingProperties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TabPage generalDataTab;
        private TableLayoutPanel columnGeneralDataLayout;
        private Controls.TextBoxData dataTypeData;
        private GroupBox characterGroup;
        private Controls.TextBoxData characterMaximumLengthData;
        private Controls.TextBoxData characterOctetLengthData;
        private GroupBox numericGroup;
        private Controls.TextBoxData numericPrecisionData;
        private Controls.TextBoxData numericPrecisionRadixData;
        private Controls.TextBoxData numericScaleData;
        private Controls.TextBoxData ordinalPositionData;
        private Controls.TextBoxData dateTimePrecisionData;
        private TabPage characterDataTab;
        private TableLayoutPanel characterDataLayout;
        private Controls.TextBoxData characterSetCatalogData;
        private Controls.TextBoxData characterSetSchemaData;
        private Controls.TextBoxData characterSetNameData;
        private Controls.TextBoxData collationCatalogData;
        private Controls.TextBoxData collationSchemaData;
        private Controls.TextBoxData collationNameData;
        private TabPage otherDataTab;
        private TableLayoutPanel numericDataLayout;
        private DataGridView extendedPropertiesData;
        private DataGridViewTextBoxColumn propertyNameData;
        private DataGridViewTextBoxColumn propertyValueData;
        private Controls.TextBoxData catalogNameData;
        private Controls.TextBoxData schemaNameData;
        private Controls.TextBoxData routineNameData;
        private Controls.TextBoxData parameterNameData;
        private Controls.TextBoxData domainCatalogData;
        private Controls.TextBoxData domainSchemaData;
        private Controls.TextBoxData domainNameData;
        private BindingSource bindingParameter;
        private BindingSource bindingProperties;
    }
}