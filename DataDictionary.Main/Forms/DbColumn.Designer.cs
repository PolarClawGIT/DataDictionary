namespace DataDictionary.Main.Forms
{
    partial class DbColumn
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
            Label ordinalPositionLayout;
            Label dataTypeLayout;
            Label columnDefaultLayout;
            Label characterMaximumLengthLayout;
            Label characterOctetLengthLayout;
            Label characterSetCatalogLayout;
            Label characterSetSchemaLayout;
            Label characterSetNameLayout;
            Label numericPrecisionLayout;
            Label numericPrecisionRadixLayout;
            Label numericScaleLayout;
            Label dateTimePrecisionLayout;
            Label schemaNameLayout;
            Label tableNameLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbColumn));
            extendedPropertiesData = new DataGridView();
            propertyNameData = new DataGridViewTextBoxColumn();
            propertyValueData = new DataGridViewTextBoxColumn();
            generalDataTab = new TabPage();
            columnGeneralDataLayout = new TableLayoutPanel();
            ordinalPositionData = new TextBox();
            dataTypeData = new TextBox();
            isNullableData = new CheckBox();
            columnDefaultData = new TextBox();
            characterDataTab = new TabPage();
            characterDataLayout = new TableLayoutPanel();
            characterOctetLengthData = new TextBox();
            characterMaximumLengthData = new TextBox();
            characterSetCatalogData = new TextBox();
            characterSetSchemaData = new TextBox();
            characterSetNameData = new TextBox();
            collationCatalogLayout = new Label();
            collationCatalogData = new TextBox();
            otherDataTab = new TabPage();
            numericDataLayout = new TableLayoutPanel();
            numericPrecisionData = new TextBox();
            numericPrecisionRadixData = new TextBox();
            numericScaleData = new TextBox();
            dateTimePrecisionData = new TextBox();
            isSparseData = new CheckBox();
            isColumnSetData = new CheckBox();
            isFileStreamData = new CheckBox();
            isSystemData = new CheckBox();
            catalogNameLayout = new Label();
            catalogNameData = new TextBox();
            schemaNameData = new TextBox();
            tableNameData = new TextBox();
            columnNameLayout = new Label();
            columnNameData = new TextBox();
            errorProvider = new ErrorProvider(components);
            dbTableLayout = new TableLayoutPanel();
            columnDetailLayout = new TabControl();
            extendedPropertiesTab = new TabPage();
            columnsTab = new TabPage();
            dataTypeDetailTab = new TabControl();
            ordinalPositionLayout = new Label();
            dataTypeLayout = new Label();
            columnDefaultLayout = new Label();
            characterMaximumLengthLayout = new Label();
            characterOctetLengthLayout = new Label();
            characterSetCatalogLayout = new Label();
            characterSetSchemaLayout = new Label();
            characterSetNameLayout = new Label();
            numericPrecisionLayout = new Label();
            numericPrecisionRadixLayout = new Label();
            numericScaleLayout = new Label();
            dateTimePrecisionLayout = new Label();
            schemaNameLayout = new Label();
            tableNameLayout = new Label();
            dbTableLayout.SuspendLayout();
            columnDetailLayout.SuspendLayout();
            extendedPropertiesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)extendedPropertiesData).BeginInit();
            columnsTab.SuspendLayout();
            dataTypeDetailTab.SuspendLayout();
            generalDataTab.SuspendLayout();
            columnGeneralDataLayout.SuspendLayout();
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
            dbTableLayout.Controls.Add(columnDetailLayout, 0, 8);
            dbTableLayout.Controls.Add(catalogNameLayout, 0, 0);
            dbTableLayout.Controls.Add(schemaNameLayout, 0, 2);
            dbTableLayout.Controls.Add(tableNameLayout, 0, 4);
            dbTableLayout.Controls.Add(catalogNameData, 0, 1);
            dbTableLayout.Controls.Add(schemaNameData, 0, 3);
            dbTableLayout.Controls.Add(tableNameData, 0, 5);
            dbTableLayout.Controls.Add(columnNameLayout, 0, 6);
            dbTableLayout.Controls.Add(columnNameData, 0, 7);
            dbTableLayout.Dock = DockStyle.Fill;
            dbTableLayout.Location = new Point(0, 0);
            dbTableLayout.Name = "dbTableLayout";
            dbTableLayout.RowCount = 9;
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            dbTableLayout.Size = new Size(383, 491);
            dbTableLayout.TabIndex = 1;
            // 
            // columnDetailLayout
            // 
            columnDetailLayout.Controls.Add(extendedPropertiesTab);
            columnDetailLayout.Controls.Add(columnsTab);
            columnDetailLayout.Dock = DockStyle.Fill;
            columnDetailLayout.Location = new Point(3, 179);
            columnDetailLayout.Name = "columnDetailLayout";
            columnDetailLayout.SelectedIndex = 0;
            columnDetailLayout.Size = new Size(377, 309);
            columnDetailLayout.TabIndex = 9;
            // 
            // extendedPropertiesTab
            // 
            extendedPropertiesTab.Controls.Add(extendedPropertiesData);
            extendedPropertiesTab.Location = new Point(4, 24);
            extendedPropertiesTab.Name = "extendedPropertiesTab";
            extendedPropertiesTab.Padding = new Padding(3);
            extendedPropertiesTab.Size = new Size(369, 281);
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
            extendedPropertiesData.Size = new Size(363, 275);
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
            columnsTab.BackColor = SystemColors.Control;
            columnsTab.Controls.Add(dataTypeDetailTab);
            columnsTab.Location = new Point(4, 24);
            columnsTab.Name = "columnsTab";
            columnsTab.Padding = new Padding(3);
            columnsTab.Size = new Size(369, 281);
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
            dataTypeDetailTab.Size = new Size(363, 275);
            dataTypeDetailTab.TabIndex = 1;
            // 
            // generalDataTab
            // 
            generalDataTab.BackColor = SystemColors.Control;
            generalDataTab.Controls.Add(columnGeneralDataLayout);
            generalDataTab.Location = new Point(4, 24);
            generalDataTab.Name = "generalDataTab";
            generalDataTab.Size = new Size(355, 247);
            generalDataTab.TabIndex = 3;
            generalDataTab.Text = "General";
            // 
            // columnGeneralDataLayout
            // 
            columnGeneralDataLayout.ColumnCount = 3;
            columnGeneralDataLayout.ColumnStyles.Add(new ColumnStyle());
            columnGeneralDataLayout.ColumnStyles.Add(new ColumnStyle());
            columnGeneralDataLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            columnGeneralDataLayout.Controls.Add(ordinalPositionLayout, 0, 0);
            columnGeneralDataLayout.Controls.Add(dataTypeLayout, 1, 0);
            columnGeneralDataLayout.Controls.Add(ordinalPositionData, 0, 1);
            columnGeneralDataLayout.Controls.Add(dataTypeData, 1, 1);
            columnGeneralDataLayout.Controls.Add(isNullableData, 2, 1);
            columnGeneralDataLayout.Controls.Add(columnDefaultData, 0, 3);
            columnGeneralDataLayout.Controls.Add(columnDefaultLayout, 0, 2);
            columnGeneralDataLayout.Dock = DockStyle.Fill;
            columnGeneralDataLayout.Location = new Point(0, 0);
            columnGeneralDataLayout.Name = "columnGeneralDataLayout";
            columnGeneralDataLayout.RowCount = 4;
            columnGeneralDataLayout.RowStyles.Add(new RowStyle());
            columnGeneralDataLayout.RowStyles.Add(new RowStyle());
            columnGeneralDataLayout.RowStyles.Add(new RowStyle());
            columnGeneralDataLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            columnGeneralDataLayout.Size = new Size(355, 247);
            columnGeneralDataLayout.TabIndex = 1;
            // 
            // ordinalPositionLayout
            // 
            ordinalPositionLayout.AutoSize = true;
            ordinalPositionLayout.Location = new Point(3, 0);
            ordinalPositionLayout.Name = "ordinalPositionLayout";
            ordinalPositionLayout.Size = new Size(92, 15);
            ordinalPositionLayout.TabIndex = 0;
            ordinalPositionLayout.Text = "Ordinal Position";
            // 
            // dataTypeLayout
            // 
            dataTypeLayout.AutoSize = true;
            dataTypeLayout.Location = new Point(109, 0);
            dataTypeLayout.Name = "dataTypeLayout";
            dataTypeLayout.Size = new Size(58, 15);
            dataTypeLayout.TabIndex = 1;
            dataTypeLayout.Text = "Data Type";
            // 
            // ordinalPositionData
            // 
            ordinalPositionData.Location = new Point(3, 18);
            ordinalPositionData.Name = "ordinalPositionData";
            ordinalPositionData.ReadOnly = true;
            ordinalPositionData.Size = new Size(100, 23);
            ordinalPositionData.TabIndex = 3;
            // 
            // dataTypeData
            // 
            dataTypeData.Location = new Point(109, 18);
            dataTypeData.Name = "dataTypeData";
            dataTypeData.ReadOnly = true;
            dataTypeData.Size = new Size(100, 23);
            dataTypeData.TabIndex = 4;
            // 
            // isNullableData
            // 
            isNullableData.AutoCheck = false;
            isNullableData.AutoSize = true;
            isNullableData.Location = new Point(215, 18);
            isNullableData.Name = "isNullableData";
            isNullableData.Size = new Size(81, 19);
            isNullableData.TabIndex = 5;
            isNullableData.Text = "Is Nullable";
            isNullableData.UseVisualStyleBackColor = true;
            // 
            // columnDefaultData
            // 
            columnGeneralDataLayout.SetColumnSpan(columnDefaultData, 3);
            columnDefaultData.Dock = DockStyle.Fill;
            columnDefaultData.Location = new Point(3, 62);
            columnDefaultData.Multiline = true;
            columnDefaultData.Name = "columnDefaultData";
            columnDefaultData.ReadOnly = true;
            columnDefaultData.Size = new Size(349, 182);
            columnDefaultData.TabIndex = 6;
            // 
            // columnDefaultLayout
            // 
            columnDefaultLayout.AutoSize = true;
            columnDefaultLayout.Location = new Point(3, 44);
            columnDefaultLayout.Name = "columnDefaultLayout";
            columnDefaultLayout.Size = new Size(45, 15);
            columnDefaultLayout.TabIndex = 7;
            columnDefaultLayout.Text = "Default";
            // 
            // characterDataTab
            // 
            characterDataTab.BackColor = SystemColors.Control;
            characterDataTab.Controls.Add(characterDataLayout);
            characterDataTab.Location = new Point(4, 24);
            characterDataTab.Name = "characterDataTab";
            characterDataTab.Padding = new Padding(3);
            characterDataTab.Size = new Size(355, 247);
            characterDataTab.TabIndex = 0;
            characterDataTab.Text = "Character";
            // 
            // characterDataLayout
            // 
            characterDataLayout.ColumnCount = 2;
            characterDataLayout.ColumnStyles.Add(new ColumnStyle());
            characterDataLayout.ColumnStyles.Add(new ColumnStyle());
            characterDataLayout.Controls.Add(characterMaximumLengthLayout, 0, 0);
            characterDataLayout.Controls.Add(characterOctetLengthLayout, 1, 0);
            characterDataLayout.Controls.Add(characterOctetLengthData, 1, 1);
            characterDataLayout.Controls.Add(characterMaximumLengthData, 0, 1);
            characterDataLayout.Controls.Add(characterSetCatalogLayout, 0, 2);
            characterDataLayout.Controls.Add(characterSetCatalogData, 0, 3);
            characterDataLayout.Controls.Add(characterSetSchemaData, 0, 5);
            characterDataLayout.Controls.Add(characterSetSchemaLayout, 0, 4);
            characterDataLayout.Controls.Add(characterSetNameLayout, 0, 6);
            characterDataLayout.Controls.Add(characterSetNameData, 0, 7);
            characterDataLayout.Controls.Add(collationCatalogLayout, 0, 8);
            characterDataLayout.Controls.Add(collationCatalogData, 0, 9);
            characterDataLayout.Dock = DockStyle.Fill;
            characterDataLayout.Location = new Point(3, 3);
            characterDataLayout.Name = "characterDataLayout";
            characterDataLayout.RowCount = 11;
            characterDataLayout.RowStyles.Add(new RowStyle());
            characterDataLayout.RowStyles.Add(new RowStyle());
            characterDataLayout.RowStyles.Add(new RowStyle());
            characterDataLayout.RowStyles.Add(new RowStyle());
            characterDataLayout.RowStyles.Add(new RowStyle());
            characterDataLayout.RowStyles.Add(new RowStyle());
            characterDataLayout.RowStyles.Add(new RowStyle());
            characterDataLayout.RowStyles.Add(new RowStyle());
            characterDataLayout.RowStyles.Add(new RowStyle());
            characterDataLayout.RowStyles.Add(new RowStyle());
            characterDataLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            characterDataLayout.Size = new Size(349, 241);
            characterDataLayout.TabIndex = 3;
            // 
            // characterMaximumLengthLayout
            // 
            characterMaximumLengthLayout.AutoSize = true;
            characterMaximumLengthLayout.Location = new Point(3, 0);
            characterMaximumLengthLayout.Name = "characterMaximumLengthLayout";
            characterMaximumLengthLayout.Size = new Size(102, 15);
            characterMaximumLengthLayout.TabIndex = 0;
            characterMaximumLengthLayout.Text = "Maximum Length";
            // 
            // characterOctetLengthLayout
            // 
            characterOctetLengthLayout.AutoSize = true;
            characterOctetLengthLayout.Location = new Point(131, 0);
            characterOctetLengthLayout.Name = "characterOctetLengthLayout";
            characterOctetLengthLayout.Size = new Size(76, 15);
            characterOctetLengthLayout.TabIndex = 1;
            characterOctetLengthLayout.Text = "Octet Length";
            // 
            // characterOctetLengthData
            // 
            characterOctetLengthData.Location = new Point(131, 18);
            characterOctetLengthData.Name = "characterOctetLengthData";
            characterOctetLengthData.ReadOnly = true;
            characterOctetLengthData.Size = new Size(100, 23);
            characterOctetLengthData.TabIndex = 2;
            // 
            // characterMaximumLengthData
            // 
            characterMaximumLengthData.Location = new Point(3, 18);
            characterMaximumLengthData.Name = "characterMaximumLengthData";
            characterMaximumLengthData.ReadOnly = true;
            characterMaximumLengthData.Size = new Size(100, 23);
            characterMaximumLengthData.TabIndex = 3;
            // 
            // characterSetCatalogLayout
            // 
            characterSetCatalogLayout.AutoSize = true;
            characterSetCatalogLayout.Location = new Point(3, 44);
            characterSetCatalogLayout.Name = "characterSetCatalogLayout";
            characterSetCatalogLayout.Size = new Size(121, 15);
            characterSetCatalogLayout.TabIndex = 4;
            characterSetCatalogLayout.Text = "Character Set Catalog";
            // 
            // characterSetCatalogData
            // 
            characterDataLayout.SetColumnSpan(characterSetCatalogData, 2);
            characterSetCatalogData.Dock = DockStyle.Fill;
            characterSetCatalogData.Location = new Point(3, 62);
            characterSetCatalogData.Name = "characterSetCatalogData";
            characterSetCatalogData.ReadOnly = true;
            characterSetCatalogData.Size = new Size(343, 23);
            characterSetCatalogData.TabIndex = 5;
            // 
            // characterSetSchemaData
            // 
            characterDataLayout.SetColumnSpan(characterSetSchemaData, 2);
            characterSetSchemaData.Dock = DockStyle.Fill;
            characterSetSchemaData.Location = new Point(3, 106);
            characterSetSchemaData.Name = "characterSetSchemaData";
            characterSetSchemaData.ReadOnly = true;
            characterSetSchemaData.Size = new Size(343, 23);
            characterSetSchemaData.TabIndex = 6;
            // 
            // characterSetSchemaLayout
            // 
            characterSetSchemaLayout.AutoSize = true;
            characterSetSchemaLayout.Location = new Point(3, 88);
            characterSetSchemaLayout.Name = "characterSetSchemaLayout";
            characterSetSchemaLayout.Size = new Size(122, 15);
            characterSetSchemaLayout.TabIndex = 7;
            characterSetSchemaLayout.Text = "Character Set Schema";
            // 
            // characterSetNameLayout
            // 
            characterSetNameLayout.AutoSize = true;
            characterSetNameLayout.Location = new Point(3, 132);
            characterSetNameLayout.Name = "characterSetNameLayout";
            characterSetNameLayout.Size = new Size(112, 15);
            characterSetNameLayout.TabIndex = 8;
            characterSetNameLayout.Text = "Character Set Name";
            // 
            // characterSetNameData
            // 
            characterDataLayout.SetColumnSpan(characterSetNameData, 2);
            characterSetNameData.Dock = DockStyle.Fill;
            characterSetNameData.Location = new Point(3, 150);
            characterSetNameData.Name = "characterSetNameData";
            characterSetNameData.ReadOnly = true;
            characterSetNameData.Size = new Size(343, 23);
            characterSetNameData.TabIndex = 9;
            // 
            // collationCatalogLayout
            // 
            collationCatalogLayout.AutoSize = true;
            collationCatalogLayout.Location = new Point(3, 176);
            collationCatalogLayout.Name = "collationCatalogLayout";
            collationCatalogLayout.Size = new Size(99, 15);
            collationCatalogLayout.TabIndex = 10;
            collationCatalogLayout.Text = "Collation Catalog";
            // 
            // collationCatalogData
            // 
            characterDataLayout.SetColumnSpan(collationCatalogData, 2);
            collationCatalogData.Dock = DockStyle.Fill;
            collationCatalogData.Location = new Point(3, 194);
            collationCatalogData.Name = "collationCatalogData";
            collationCatalogData.ReadOnly = true;
            collationCatalogData.Size = new Size(343, 23);
            collationCatalogData.TabIndex = 11;
            // 
            // otherDataTab
            // 
            otherDataTab.BackColor = SystemColors.Control;
            otherDataTab.Controls.Add(numericDataLayout);
            otherDataTab.Location = new Point(4, 24);
            otherDataTab.Name = "otherDataTab";
            otherDataTab.Padding = new Padding(3);
            otherDataTab.Size = new Size(355, 247);
            otherDataTab.TabIndex = 1;
            otherDataTab.Text = "Other (Numeric, Date, ...)";
            // 
            // numericDataLayout
            // 
            numericDataLayout.ColumnCount = 3;
            numericDataLayout.ColumnStyles.Add(new ColumnStyle());
            numericDataLayout.ColumnStyles.Add(new ColumnStyle());
            numericDataLayout.ColumnStyles.Add(new ColumnStyle());
            numericDataLayout.Controls.Add(numericPrecisionLayout, 0, 0);
            numericDataLayout.Controls.Add(numericPrecisionRadixLayout, 1, 0);
            numericDataLayout.Controls.Add(numericScaleLayout, 2, 0);
            numericDataLayout.Controls.Add(numericPrecisionData, 0, 1);
            numericDataLayout.Controls.Add(numericPrecisionRadixData, 1, 1);
            numericDataLayout.Controls.Add(numericScaleData, 2, 1);
            numericDataLayout.Controls.Add(dateTimePrecisionLayout, 0, 2);
            numericDataLayout.Controls.Add(dateTimePrecisionData, 0, 3);
            numericDataLayout.Controls.Add(isSparseData, 1, 2);
            numericDataLayout.Controls.Add(isColumnSetData, 2, 2);
            numericDataLayout.Controls.Add(isFileStreamData, 1, 3);
            numericDataLayout.Controls.Add(isSystemData, 2, 3);
            numericDataLayout.Dock = DockStyle.Fill;
            numericDataLayout.Location = new Point(3, 3);
            numericDataLayout.Name = "numericDataLayout";
            numericDataLayout.RowCount = 5;
            numericDataLayout.RowStyles.Add(new RowStyle());
            numericDataLayout.RowStyles.Add(new RowStyle());
            numericDataLayout.RowStyles.Add(new RowStyle());
            numericDataLayout.RowStyles.Add(new RowStyle());
            numericDataLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            numericDataLayout.Size = new Size(349, 241);
            numericDataLayout.TabIndex = 3;
            // 
            // numericPrecisionLayout
            // 
            numericPrecisionLayout.AutoSize = true;
            numericPrecisionLayout.Location = new Point(3, 0);
            numericPrecisionLayout.Name = "numericPrecisionLayout";
            numericPrecisionLayout.Size = new Size(55, 30);
            numericPrecisionLayout.TabIndex = 0;
            numericPrecisionLayout.Text = "Numeric\r\nPrecision";
            // 
            // numericPrecisionRadixLayout
            // 
            numericPrecisionRadixLayout.AutoSize = true;
            numericPrecisionRadixLayout.Location = new Point(109, 0);
            numericPrecisionRadixLayout.Name = "numericPrecisionRadixLayout";
            numericPrecisionRadixLayout.Size = new Size(87, 30);
            numericPrecisionRadixLayout.TabIndex = 1;
            numericPrecisionRadixLayout.Text = "Numeric\r\nPrecision Radix";
            // 
            // numericScaleLayout
            // 
            numericScaleLayout.AutoSize = true;
            numericScaleLayout.Location = new Point(215, 0);
            numericScaleLayout.Name = "numericScaleLayout";
            numericScaleLayout.Size = new Size(53, 30);
            numericScaleLayout.TabIndex = 2;
            numericScaleLayout.Text = "Numeric\r\nScale";
            // 
            // numericPrecisionData
            // 
            numericPrecisionData.Location = new Point(3, 33);
            numericPrecisionData.Name = "numericPrecisionData";
            numericPrecisionData.ReadOnly = true;
            numericPrecisionData.Size = new Size(100, 23);
            numericPrecisionData.TabIndex = 3;
            // 
            // numericPrecisionRadixData
            // 
            numericPrecisionRadixData.Location = new Point(109, 33);
            numericPrecisionRadixData.Name = "numericPrecisionRadixData";
            numericPrecisionRadixData.ReadOnly = true;
            numericPrecisionRadixData.Size = new Size(100, 23);
            numericPrecisionRadixData.TabIndex = 4;
            // 
            // numericScaleData
            // 
            numericScaleData.Location = new Point(215, 33);
            numericScaleData.Name = "numericScaleData";
            numericScaleData.ReadOnly = true;
            numericScaleData.Size = new Size(100, 23);
            numericScaleData.TabIndex = 5;
            // 
            // dateTimePrecisionLayout
            // 
            dateTimePrecisionLayout.AutoSize = true;
            dateTimePrecisionLayout.Location = new Point(3, 59);
            dateTimePrecisionLayout.Name = "dateTimePrecisionLayout";
            dateTimePrecisionLayout.Size = new Size(60, 30);
            dateTimePrecisionLayout.TabIndex = 6;
            dateTimePrecisionLayout.Text = "Date Time\r\nPrecision";
            // 
            // dateTimePrecisionData
            // 
            dateTimePrecisionData.Location = new Point(3, 92);
            dateTimePrecisionData.Name = "dateTimePrecisionData";
            dateTimePrecisionData.ReadOnly = true;
            dateTimePrecisionData.Size = new Size(100, 23);
            dateTimePrecisionData.TabIndex = 7;
            // 
            // isSparseData
            // 
            isSparseData.AutoCheck = false;
            isSparseData.AutoSize = true;
            isSparseData.Location = new Point(109, 62);
            isSparseData.Name = "isSparseData";
            isSparseData.Size = new Size(71, 19);
            isSparseData.TabIndex = 8;
            isSparseData.Text = "Is Sparse";
            isSparseData.UseVisualStyleBackColor = true;
            // 
            // isColumnSetData
            // 
            isColumnSetData.AutoCheck = false;
            isColumnSetData.AutoSize = true;
            isColumnSetData.Location = new Point(215, 62);
            isColumnSetData.Name = "isColumnSetData";
            isColumnSetData.Size = new Size(99, 19);
            isColumnSetData.TabIndex = 9;
            isColumnSetData.Text = "Is Column Set";
            isColumnSetData.UseVisualStyleBackColor = true;
            // 
            // isFileStreamData
            // 
            isFileStreamData.AutoCheck = false;
            isFileStreamData.AutoSize = true;
            isFileStreamData.Location = new Point(109, 92);
            isFileStreamData.Name = "isFileStreamData";
            isFileStreamData.Size = new Size(95, 19);
            isFileStreamData.TabIndex = 10;
            isFileStreamData.Text = "Is File Stream";
            isFileStreamData.UseVisualStyleBackColor = true;
            // 
            // isSystemData
            // 
            isSystemData.AutoCheck = false;
            isSystemData.AutoSize = true;
            isSystemData.Location = new Point(215, 92);
            isSystemData.Name = "isSystemData";
            isSystemData.Size = new Size(75, 19);
            isSystemData.TabIndex = 11;
            isSystemData.Text = "Is System";
            isSystemData.UseVisualStyleBackColor = true;
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
            tableNameLayout.Size = new Size(69, 15);
            tableNameLayout.TabIndex = 2;
            tableNameLayout.Text = "Table Name";
            // 
            // catalogNameData
            // 
            catalogNameData.Dock = DockStyle.Fill;
            catalogNameData.Location = new Point(3, 18);
            catalogNameData.Name = "catalogNameData";
            catalogNameData.ReadOnly = true;
            catalogNameData.Size = new Size(377, 23);
            catalogNameData.TabIndex = 4;
            // 
            // schemaNameData
            // 
            schemaNameData.Dock = DockStyle.Fill;
            schemaNameData.Location = new Point(3, 62);
            schemaNameData.Name = "schemaNameData";
            schemaNameData.ReadOnly = true;
            schemaNameData.Size = new Size(377, 23);
            schemaNameData.TabIndex = 5;
            // 
            // tableNameData
            // 
            tableNameData.Dock = DockStyle.Fill;
            tableNameData.Location = new Point(3, 106);
            tableNameData.Name = "tableNameData";
            tableNameData.ReadOnly = true;
            tableNameData.Size = new Size(377, 23);
            tableNameData.TabIndex = 6;
            // 
            // columnNameLayout
            // 
            columnNameLayout.AutoSize = true;
            columnNameLayout.Location = new Point(3, 132);
            columnNameLayout.Name = "columnNameLayout";
            columnNameLayout.Size = new Size(85, 15);
            columnNameLayout.TabIndex = 7;
            columnNameLayout.Text = "Column Name";
            // 
            // columnNameData
            // 
            columnNameData.Dock = DockStyle.Fill;
            columnNameData.Location = new Point(3, 150);
            columnNameData.Name = "columnNameData";
            columnNameData.ReadOnly = true;
            columnNameData.Size = new Size(377, 23);
            columnNameData.TabIndex = 8;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // DbColumn
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(383, 491);
            Controls.Add(dbTableLayout);
            Icon = (Icon)resources.GetObject("$this.Icon");
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

        private Label catalogNameLayout;
        private TextBox catalogNameData;
        private TextBox schemaNameData;
        private TextBox tableNameData;
        private Label columnNameLayout;
        private TextBox columnNameData;
        private DataGridView extendedPropertiesData;
        private DataGridViewTextBoxColumn propertyNameData;
        private DataGridViewTextBoxColumn propertyValueData;
        private ErrorProvider errorProvider;
        private TabPage characterDataTab;
        private TabPage otherDataTab;
        private TabPage generalDataTab;
        private TableLayoutPanel columnGeneralDataLayout;
        private TextBox ordinalPositionData;
        private TextBox dataTypeData;
        private CheckBox isNullableData;
        private TextBox columnDefaultData;
        private TableLayoutPanel characterDataLayout;
        private TextBox characterOctetLengthData;
        private TextBox characterMaximumLengthData;
        private TextBox characterSetCatalogData;
        private TextBox characterSetSchemaData;
        private TextBox characterSetNameData;
        private Label collationCatalogLayout;
        private TextBox collationCatalogData;
        private TableLayoutPanel numericDataLayout;
        private TextBox numericPrecisionData;
        private TextBox numericPrecisionRadixData;
        private TextBox numericScaleData;
        private TextBox dateTimePrecisionData;
        private CheckBox isSparseData;
        private CheckBox isColumnSetData;
        private CheckBox isFileStreamData;
        private CheckBox isSystemData;
    }
}