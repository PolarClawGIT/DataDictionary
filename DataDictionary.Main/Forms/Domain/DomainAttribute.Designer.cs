using DataDictionary.Resource.Enumerations;

namespace DataDictionary.Main.Forms.Domain
{
    partial class DomainAttribute
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
            TableLayoutPanel mainLayout;
            TableLayoutPanel detailsLayout;
            TableLayoutPanel propertyLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DomainAttribute));
            TableLayoutPanel definitionLayout;
            TableLayoutPanel aliasCommandLayout;
            TableLayoutPanel subjectAreaLayout;
            titleData = new DataDictionary.Main.Controls.TextBoxData();
            descriptionData = new DataDictionary.Main.Controls.TextBoxData();
            detailTabLayout = new TabControl();
            detailTab = new TabPage();
            isSingleValueData = new CheckBox();
            isMultiValuedData = new CheckBox();
            isSimpleTypeData = new CheckBox();
            isCompositeTypeData = new CheckBox();
            isIntegralData = new CheckBox();
            isDerivedData = new CheckBox();
            isValuedData = new CheckBox();
            isNullableData = new CheckBox();
            isNonKeyData = new CheckBox();
            isKeyData = new CheckBox();
            propertyTab = new TabPage();
            propertiesData = new DataGridView();
            propertyIdColumn = new DataGridViewComboBoxColumn();
            propertyValueColumn = new DataGridViewTextBoxColumn();
            domainProperty = new Controls.DomainProperty();
            definitionTab = new TabPage();
            definitionData = new DataGridView();
            definitionColumn = new DataGridViewComboBoxColumn();
            definitionSummaryColumn = new DataGridViewTextBoxColumn();
            domainDefinition = new Controls.DomainDefinition();
            aliasTab = new TabPage();
            aliaseLayout = new TableLayoutPanel();
            aliasesData = new DataGridView();
            aliaseScopeColumn = new DataGridViewComboBoxColumn();
            aliasNameColumn = new DataGridViewTextBoxColumn();
            aliasNameData = new DataDictionary.Main.Controls.TextBoxData();
            aliasScopeData = new DataDictionary.Main.Controls.ComboBoxData();
            aliasSelectCommand = new Button();
            aliasAddCommand = new Button();
            isAliasInModelData = new CheckBox();
            subjectAreaTab = new TabPage();
            subjectArea = new Controls.SubjectArea();
            memberNameData = new DataDictionary.Main.Controls.TextBoxData();
            entityTab = new TabPage();
            bindingAttribute = new BindingSource(components);
            bindingProperty = new BindingSource(components);
            bindingAlias = new BindingSource(components);
            bindingSubjectArea = new BindingSource(components);
            bindingDefinition = new BindingSource(components);
            mainLayout = new TableLayoutPanel();
            detailsLayout = new TableLayoutPanel();
            propertyLayout = new TableLayoutPanel();
            definitionLayout = new TableLayoutPanel();
            aliasCommandLayout = new TableLayoutPanel();
            subjectAreaLayout = new TableLayoutPanel();
            mainLayout.SuspendLayout();
            detailTabLayout.SuspendLayout();
            detailTab.SuspendLayout();
            detailsLayout.SuspendLayout();
            propertyTab.SuspendLayout();
            propertyLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)propertiesData).BeginInit();
            definitionTab.SuspendLayout();
            definitionLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)definitionData).BeginInit();
            aliasTab.SuspendLayout();
            aliaseLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)aliasesData).BeginInit();
            aliasCommandLayout.SuspendLayout();
            subjectAreaTab.SuspendLayout();
            subjectAreaLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingAttribute).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingProperty).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingAlias).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSubjectArea).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingDefinition).BeginInit();
            SuspendLayout();
            // 
            // mainLayout
            // 
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.Controls.Add(titleData, 0, 0);
            mainLayout.Controls.Add(descriptionData, 0, 1);
            mainLayout.Controls.Add(detailTabLayout, 0, 2);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(0, 25);
            mainLayout.Name = "mainLayout";
            mainLayout.RowCount = 3;
            mainLayout.RowStyles.Add(new RowStyle());
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            mainLayout.Size = new Size(426, 521);
            mainLayout.TabIndex = 1;
            // 
            // titleData
            // 
            titleData.AutoSize = true;
            titleData.Dock = DockStyle.Fill;
            titleData.HeaderText = "Title";
            titleData.Location = new Point(3, 3);
            titleData.Multiline = false;
            titleData.Name = "titleData";
            titleData.ReadOnly = false;
            titleData.Size = new Size(420, 44);
            titleData.TabIndex = 0;
            titleData.WordWrap = true;
            // 
            // descriptionData
            // 
            descriptionData.AutoSize = true;
            descriptionData.Dock = DockStyle.Fill;
            descriptionData.HeaderText = "Description";
            descriptionData.Location = new Point(3, 53);
            descriptionData.Multiline = true;
            descriptionData.Name = "descriptionData";
            descriptionData.ReadOnly = false;
            descriptionData.Size = new Size(420, 88);
            descriptionData.TabIndex = 1;
            descriptionData.WordWrap = true;
            // 
            // detailTabLayout
            // 
            detailTabLayout.Controls.Add(detailTab);
            detailTabLayout.Controls.Add(propertyTab);
            detailTabLayout.Controls.Add(definitionTab);
            detailTabLayout.Controls.Add(aliasTab);
            detailTabLayout.Controls.Add(subjectAreaTab);
            detailTabLayout.Controls.Add(entityTab);
            detailTabLayout.Dock = DockStyle.Fill;
            detailTabLayout.Location = new Point(3, 147);
            detailTabLayout.Name = "detailTabLayout";
            detailTabLayout.SelectedIndex = 0;
            detailTabLayout.Size = new Size(420, 371);
            detailTabLayout.TabIndex = 2;
            // 
            // detailTab
            // 
            detailTab.BackColor = SystemColors.Control;
            detailTab.Controls.Add(detailsLayout);
            detailTab.Location = new Point(4, 24);
            detailTab.Name = "detailTab";
            detailTab.Padding = new Padding(3);
            detailTab.Size = new Size(412, 343);
            detailTab.TabIndex = 0;
            detailTab.Text = "Details";
            // 
            // detailsLayout
            // 
            detailsLayout.ColumnCount = 2;
            detailsLayout.ColumnStyles.Add(new ColumnStyle());
            detailsLayout.ColumnStyles.Add(new ColumnStyle());
            detailsLayout.Controls.Add(isSingleValueData, 0, 0);
            detailsLayout.Controls.Add(isMultiValuedData, 1, 0);
            detailsLayout.Controls.Add(isSimpleTypeData, 0, 1);
            detailsLayout.Controls.Add(isCompositeTypeData, 1, 1);
            detailsLayout.Controls.Add(isIntegralData, 0, 2);
            detailsLayout.Controls.Add(isDerivedData, 1, 2);
            detailsLayout.Controls.Add(isValuedData, 0, 3);
            detailsLayout.Controls.Add(isNullableData, 1, 3);
            detailsLayout.Controls.Add(isNonKeyData, 0, 4);
            detailsLayout.Controls.Add(isKeyData, 1, 4);
            detailsLayout.Dock = DockStyle.Fill;
            detailsLayout.Location = new Point(3, 3);
            detailsLayout.Name = "detailsLayout";
            detailsLayout.RowCount = 5;
            detailsLayout.RowStyles.Add(new RowStyle());
            detailsLayout.RowStyles.Add(new RowStyle());
            detailsLayout.RowStyles.Add(new RowStyle());
            detailsLayout.RowStyles.Add(new RowStyle());
            detailsLayout.RowStyles.Add(new RowStyle());
            detailsLayout.Size = new Size(406, 337);
            detailsLayout.TabIndex = 0;
            // 
            // isSingleValueData
            // 
            isSingleValueData.AutoSize = true;
            isSingleValueData.Location = new Point(3, 3);
            isSingleValueData.Name = "isSingleValueData";
            isSingleValueData.Size = new Size(98, 19);
            isSingleValueData.TabIndex = 0;
            isSingleValueData.Text = "Single-Valued";
            isSingleValueData.UseVisualStyleBackColor = true;
            // 
            // isMultiValuedData
            // 
            isMultiValuedData.AutoSize = true;
            isMultiValuedData.Location = new Point(107, 3);
            isMultiValuedData.Name = "isMultiValuedData";
            isMultiValuedData.Size = new Size(94, 19);
            isMultiValuedData.TabIndex = 1;
            isMultiValuedData.Text = "Multi-Valued";
            isMultiValuedData.UseVisualStyleBackColor = true;
            // 
            // isSimpleTypeData
            // 
            isSimpleTypeData.AutoSize = true;
            isSimpleTypeData.Location = new Point(3, 28);
            isSimpleTypeData.Name = "isSimpleTypeData";
            isSimpleTypeData.Size = new Size(89, 19);
            isSimpleTypeData.TabIndex = 2;
            isSimpleTypeData.Text = "Simple Type";
            isSimpleTypeData.UseVisualStyleBackColor = true;
            // 
            // isCompositeTypeData
            // 
            isCompositeTypeData.AutoSize = true;
            isCompositeTypeData.Location = new Point(107, 28);
            isCompositeTypeData.Name = "isCompositeTypeData";
            isCompositeTypeData.Size = new Size(111, 19);
            isCompositeTypeData.TabIndex = 3;
            isCompositeTypeData.Text = "Composite Type";
            isCompositeTypeData.UseVisualStyleBackColor = true;
            // 
            // isIntegralData
            // 
            isIntegralData.AutoSize = true;
            isIntegralData.Location = new Point(3, 53);
            isIntegralData.Name = "isIntegralData";
            isIntegralData.Size = new Size(77, 19);
            isIntegralData.TabIndex = 4;
            isIntegralData.Text = "is Integral";
            isIntegralData.UseVisualStyleBackColor = true;
            // 
            // isDerivedData
            // 
            isDerivedData.AutoSize = true;
            isDerivedData.Location = new Point(107, 53);
            isDerivedData.Name = "isDerivedData";
            isDerivedData.Size = new Size(77, 19);
            isDerivedData.TabIndex = 5;
            isDerivedData.Text = "is Derived";
            isDerivedData.UseVisualStyleBackColor = true;
            // 
            // isValuedData
            // 
            isValuedData.AutoSize = true;
            isValuedData.Location = new Point(3, 78);
            isValuedData.Name = "isValuedData";
            isValuedData.Size = new Size(72, 19);
            isValuedData.TabIndex = 6;
            isValuedData.Text = "is Valued";
            isValuedData.UseVisualStyleBackColor = true;
            // 
            // isNullableData
            // 
            isNullableData.AutoSize = true;
            isNullableData.Location = new Point(107, 78);
            isNullableData.Name = "isNullableData";
            isNullableData.Size = new Size(81, 19);
            isNullableData.TabIndex = 7;
            isNullableData.Text = "is Nullable";
            isNullableData.UseVisualStyleBackColor = true;
            // 
            // isNonKeyData
            // 
            isNonKeyData.AutoSize = true;
            isNonKeyData.Location = new Point(3, 103);
            isNonKeyData.Name = "isNonKeyData";
            isNonKeyData.Size = new Size(84, 19);
            isNonKeyData.TabIndex = 8;
            isNonKeyData.Text = "is Non-Key";
            isNonKeyData.UseVisualStyleBackColor = true;
            // 
            // isKeyData
            // 
            isKeyData.AutoSize = true;
            isKeyData.Location = new Point(107, 103);
            isKeyData.Name = "isKeyData";
            isKeyData.Size = new Size(56, 19);
            isKeyData.TabIndex = 9;
            isKeyData.Text = "is Key";
            isKeyData.UseVisualStyleBackColor = true;
            // 
            // propertyTab
            // 
            propertyTab.BackColor = SystemColors.Control;
            propertyTab.Controls.Add(propertyLayout);
            propertyTab.Location = new Point(4, 24);
            propertyTab.Name = "propertyTab";
            propertyTab.Padding = new Padding(3);
            propertyTab.Size = new Size(192, 72);
            propertyTab.TabIndex = 1;
            propertyTab.Text = "Properties";
            // 
            // propertyLayout
            // 
            propertyLayout.ColumnCount = 1;
            propertyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            propertyLayout.Controls.Add(propertiesData, 0, 0);
            propertyLayout.Controls.Add(domainProperty, 0, 1);
            propertyLayout.Dock = DockStyle.Fill;
            propertyLayout.Location = new Point(3, 3);
            propertyLayout.Name = "propertyLayout";
            propertyLayout.RowCount = 2;
            propertyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            propertyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            propertyLayout.Size = new Size(186, 66);
            propertyLayout.TabIndex = 0;
            // 
            // propertiesData
            // 
            propertiesData.AllowUserToAddRows = false;
            propertiesData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            propertiesData.Columns.AddRange(new DataGridViewColumn[] { propertyIdColumn, propertyValueColumn });
            propertiesData.Dock = DockStyle.Fill;
            propertiesData.Location = new Point(3, 3);
            propertiesData.Name = "propertiesData";
            propertiesData.ReadOnly = true;
            propertiesData.Size = new Size(180, 13);
            propertiesData.TabIndex = 1;
            // 
            // propertyIdColumn
            // 
            propertyIdColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            propertyIdColumn.DataPropertyName = "PropertyId";
            propertyIdColumn.FillWeight = 30F;
            propertyIdColumn.HeaderText = "Property";
            propertyIdColumn.Name = "propertyIdColumn";
            propertyIdColumn.ReadOnly = true;
            // 
            // propertyValueColumn
            // 
            propertyValueColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            propertyValueColumn.DataPropertyName = "PropertyValue";
            propertyValueColumn.FillWeight = 70F;
            propertyValueColumn.HeaderText = "Property Value";
            propertyValueColumn.Name = "propertyValueColumn";
            propertyValueColumn.ReadOnly = true;
            // 
            // domainProperty
            // 
            domainProperty.ApplyImage = (Image)resources.GetObject("domainProperty.ApplyImage");
            domainProperty.ApplyText = "apply";
            domainProperty.Dock = DockStyle.Fill;
            domainProperty.Location = new Point(3, 22);
            domainProperty.Name = "domainProperty";
            domainProperty.PropertyId = new Guid("00000000-0000-0000-0000-000000000000");
            domainProperty.PropertyValue = "";
            domainProperty.ReadOnly = false;
            domainProperty.Size = new Size(180, 41);
            domainProperty.TabIndex = 2;
            domainProperty.OnApply += DomainProperty_OnApply;
            // 
            // definitionTab
            // 
            definitionTab.BackColor = SystemColors.Control;
            definitionTab.Controls.Add(definitionLayout);
            definitionTab.Location = new Point(4, 24);
            definitionTab.Name = "definitionTab";
            definitionTab.Size = new Size(192, 72);
            definitionTab.TabIndex = 5;
            definitionTab.Text = "Definition";
            // 
            // definitionLayout
            // 
            definitionLayout.ColumnCount = 1;
            definitionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            definitionLayout.Controls.Add(definitionData, 0, 0);
            definitionLayout.Controls.Add(domainDefinition, 0, 1);
            definitionLayout.Dock = DockStyle.Fill;
            definitionLayout.Location = new Point(0, 0);
            definitionLayout.Name = "definitionLayout";
            definitionLayout.Padding = new Padding(3);
            definitionLayout.RowCount = 2;
            definitionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            definitionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            definitionLayout.Size = new Size(192, 72);
            definitionLayout.TabIndex = 0;
            // 
            // definitionData
            // 
            definitionData.AllowUserToAddRows = false;
            definitionData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            definitionData.Columns.AddRange(new DataGridViewColumn[] { definitionColumn, definitionSummaryColumn });
            definitionData.Dock = DockStyle.Fill;
            definitionData.Location = new Point(6, 6);
            definitionData.Name = "definitionData";
            definitionData.ReadOnly = true;
            definitionData.Size = new Size(180, 13);
            definitionData.TabIndex = 0;
            // 
            // definitionColumn
            // 
            definitionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            definitionColumn.DataPropertyName = "DefinitionId";
            definitionColumn.FillWeight = 50F;
            definitionColumn.HeaderText = "Definition";
            definitionColumn.Name = "definitionColumn";
            definitionColumn.ReadOnly = true;
            // 
            // definitionSummaryColumn
            // 
            definitionSummaryColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            definitionSummaryColumn.DataPropertyName = "DefinitionSummary";
            definitionSummaryColumn.HeaderText = "Definition Summary";
            definitionSummaryColumn.Name = "definitionSummaryColumn";
            definitionSummaryColumn.ReadOnly = true;
            // 
            // domainDefinition
            // 
            domainDefinition.ApplyImage = Properties.Resources.NewRichTextBox;
            domainDefinition.ApplyText = "apply";
            domainDefinition.DefinitionId = new Guid("00000000-0000-0000-0000-000000000000");
            domainDefinition.DefinitionSummary = "";
            domainDefinition.DefinitionText = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil Segoe UI;}}\r\n{\\*\\generator Riched20 10.0.19041}\\viewkind4\\uc1 \r\n\\pard\\f0\\fs18\\par\r\n}\r\n";
            domainDefinition.Dock = DockStyle.Fill;
            domainDefinition.Location = new Point(6, 25);
            domainDefinition.Name = "domainDefinition";
            domainDefinition.ReadOnly = false;
            domainDefinition.Size = new Size(180, 41);
            domainDefinition.TabIndex = 1;
            domainDefinition.OnApply += DomainDefinition_OnApply;
            // 
            // aliasTab
            // 
            aliasTab.BackColor = SystemColors.Control;
            aliasTab.Controls.Add(aliaseLayout);
            aliasTab.Location = new Point(4, 24);
            aliasTab.Name = "aliasTab";
            aliasTab.Padding = new Padding(3);
            aliasTab.Size = new Size(412, 343);
            aliasTab.TabIndex = 2;
            aliasTab.Text = "Aliases";
            // 
            // aliaseLayout
            // 
            aliaseLayout.ColumnCount = 2;
            aliaseLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            aliaseLayout.ColumnStyles.Add(new ColumnStyle());
            aliaseLayout.Controls.Add(aliasesData, 0, 0);
            aliaseLayout.Controls.Add(aliasNameData, 0, 2);
            aliaseLayout.Controls.Add(aliasScopeData, 0, 1);
            aliaseLayout.Controls.Add(aliasCommandLayout, 1, 1);
            aliaseLayout.Dock = DockStyle.Fill;
            aliaseLayout.Location = new Point(3, 3);
            aliaseLayout.Name = "aliaseLayout";
            aliaseLayout.RowCount = 3;
            aliaseLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            aliaseLayout.RowStyles.Add(new RowStyle());
            aliaseLayout.RowStyles.Add(new RowStyle());
            aliaseLayout.Size = new Size(406, 337);
            aliaseLayout.TabIndex = 2;
            // 
            // aliasesData
            // 
            aliasesData.AllowUserToAddRows = false;
            aliasesData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            aliasesData.Columns.AddRange(new DataGridViewColumn[] { aliaseScopeColumn, aliasNameColumn });
            aliaseLayout.SetColumnSpan(aliasesData, 2);
            aliasesData.Dock = DockStyle.Fill;
            aliasesData.Location = new Point(3, 3);
            aliasesData.Name = "aliasesData";
            aliasesData.ReadOnly = true;
            aliasesData.Size = new Size(400, 229);
            aliasesData.TabIndex = 0;
            // 
            // aliaseScopeColumn
            // 
            aliaseScopeColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            aliaseScopeColumn.DataPropertyName = "AliasScope";
            aliaseScopeColumn.FillWeight = 50F;
            aliaseScopeColumn.HeaderText = "Scope";
            aliaseScopeColumn.Name = "aliaseScopeColumn";
            aliaseScopeColumn.ReadOnly = true;
            // 
            // aliasNameColumn
            // 
            aliasNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            aliasNameColumn.DataPropertyName = "AliasName";
            aliasNameColumn.HeaderText = "Alias Name";
            aliasNameColumn.Name = "aliasNameColumn";
            aliasNameColumn.ReadOnly = true;
            // 
            // aliasNameData
            // 
            aliasNameData.AutoSize = true;
            aliasNameData.Dock = DockStyle.Fill;
            aliasNameData.HeaderText = "Alias Name";
            aliasNameData.Location = new Point(3, 290);
            aliasNameData.Multiline = false;
            aliasNameData.Name = "aliasNameData";
            aliasNameData.ReadOnly = true;
            aliasNameData.Size = new Size(313, 44);
            aliasNameData.TabIndex = 2;
            aliasNameData.WordWrap = true;
            aliasNameData.Validating += AliasNameData_Validating;
            // 
            // aliasScopeData
            // 
            aliasScopeData.AutoSize = true;
            aliasScopeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            aliasScopeData.Dock = DockStyle.Fill;
            aliasScopeData.DropDownStyle = ComboBoxStyle.DropDown;
            aliasScopeData.HeaderText = "Scope";
            aliasScopeData.Location = new Point(3, 238);
            aliasScopeData.Name = "aliasScopeData";
            aliasScopeData.ReadOnly = true;
            aliasScopeData.Size = new Size(313, 46);
            aliasScopeData.TabIndex = 1;
            // 
            // aliasCommandLayout
            // 
            aliasCommandLayout.AutoSize = true;
            aliasCommandLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            aliasCommandLayout.ColumnCount = 1;
            aliasCommandLayout.ColumnStyles.Add(new ColumnStyle());
            aliasCommandLayout.Controls.Add(aliasSelectCommand, 0, 1);
            aliasCommandLayout.Controls.Add(aliasAddCommand, 0, 2);
            aliasCommandLayout.Controls.Add(isAliasInModelData, 0, 0);
            aliasCommandLayout.Dock = DockStyle.Fill;
            aliasCommandLayout.Location = new Point(322, 238);
            aliasCommandLayout.Name = "aliasCommandLayout";
            aliasCommandLayout.RowCount = 3;
            aliaseLayout.SetRowSpan(aliasCommandLayout, 2);
            aliasCommandLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            aliasCommandLayout.RowStyles.Add(new RowStyle());
            aliasCommandLayout.RowStyles.Add(new RowStyle());
            aliasCommandLayout.Size = new Size(81, 96);
            aliasCommandLayout.TabIndex = 4;
            // 
            // aliasSelectCommand
            // 
            aliasSelectCommand.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            aliasSelectCommand.Location = new Point(3, 39);
            aliasSelectCommand.Name = "aliasSelectCommand";
            aliasSelectCommand.Size = new Size(75, 24);
            aliasSelectCommand.TabIndex = 1;
            aliasSelectCommand.Text = "Select";
            aliasSelectCommand.TextImageRelation = TextImageRelation.ImageBeforeText;
            aliasSelectCommand.UseVisualStyleBackColor = true;
            aliasSelectCommand.Click += AliasSelectCommand_Click;
            // 
            // aliasAddCommand
            // 
            aliasAddCommand.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            aliasAddCommand.Location = new Point(3, 69);
            aliasAddCommand.Name = "aliasAddCommand";
            aliasAddCommand.Size = new Size(75, 24);
            aliasAddCommand.TabIndex = 2;
            aliasAddCommand.Text = "New";
            aliasAddCommand.TextImageRelation = TextImageRelation.ImageBeforeText;
            aliasAddCommand.UseVisualStyleBackColor = true;
            aliasAddCommand.Click += AliasAddCommand_Click;
            // 
            // isAliasInModelData
            // 
            isAliasInModelData.AutoSize = true;
            isAliasInModelData.Enabled = false;
            isAliasInModelData.Location = new Point(3, 3);
            isAliasInModelData.Name = "isAliasInModelData";
            isAliasInModelData.Size = new Size(73, 19);
            isAliasInModelData.TabIndex = 0;
            isAliasInModelData.Text = "in Model";
            isAliasInModelData.UseVisualStyleBackColor = true;
            // 
            // subjectAreaTab
            // 
            subjectAreaTab.BackColor = SystemColors.Control;
            subjectAreaTab.Controls.Add(subjectAreaLayout);
            subjectAreaTab.Location = new Point(4, 24);
            subjectAreaTab.Name = "subjectAreaTab";
            subjectAreaTab.Size = new Size(192, 72);
            subjectAreaTab.TabIndex = 3;
            subjectAreaTab.Text = "Subject Area";
            // 
            // subjectAreaLayout
            // 
            subjectAreaLayout.ColumnCount = 1;
            subjectAreaLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            subjectAreaLayout.Controls.Add(subjectArea, 0, 1);
            subjectAreaLayout.Controls.Add(memberNameData, 0, 0);
            subjectAreaLayout.Dock = DockStyle.Fill;
            subjectAreaLayout.Location = new Point(0, 0);
            subjectAreaLayout.Name = "subjectAreaLayout";
            subjectAreaLayout.RowCount = 2;
            subjectAreaLayout.RowStyles.Add(new RowStyle());
            subjectAreaLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            subjectAreaLayout.Size = new Size(192, 72);
            subjectAreaLayout.TabIndex = 1;
            // 
            // subjectArea
            // 
            subjectArea.Dock = DockStyle.Fill;
            subjectArea.Location = new Point(3, 53);
            subjectArea.Name = "subjectArea";
            subjectArea.Size = new Size(186, 16);
            subjectArea.TabIndex = 0;
            subjectArea.OnSubjectAdd += SubjectArea_OnSubjectAdd;
            subjectArea.OnSubjectRemove += SubjectArea_OnSubjectRemove;
            // 
            // memberNameData
            // 
            memberNameData.AutoSize = true;
            memberNameData.Dock = DockStyle.Fill;
            memberNameData.HeaderText = "Subject Member Name";
            memberNameData.Location = new Point(3, 3);
            memberNameData.Multiline = false;
            memberNameData.Name = "memberNameData";
            memberNameData.ReadOnly = false;
            memberNameData.Size = new Size(186, 44);
            memberNameData.TabIndex = 1;
            memberNameData.WordWrap = true;
            memberNameData.Validating += MemberNameData_Validating;
            // 
            // entityTab
            // 
            entityTab.BackColor = SystemColors.Control;
            entityTab.Location = new Point(4, 24);
            entityTab.Name = "entityTab";
            entityTab.Size = new Size(192, 72);
            entityTab.TabIndex = 4;
            entityTab.Text = "Entities";
            // 
            // bindingProperty
            // 
            bindingProperty.AddingNew += BindingProperty_AddingNew;
            bindingProperty.CurrentChanged += BindingProperty_CurrentChanged;
            // 
            // bindingAlias
            // 
            bindingAlias.AddingNew += BindingAlias_AddingNew;
            bindingAlias.CurrentChanged += BindingAlias_CurrentChanged;
            // 
            // bindingSubjectArea
            // 
            bindingSubjectArea.AddingNew += BindingSubjectArea_AddingNew;
            // 
            // bindingDefinition
            // 
            bindingDefinition.AddingNew += BindingDefinition_AddingNew;
            bindingDefinition.CurrentChanged += BindingDefinition_CurrentChanged;
            // 
            // DomainAttribute
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(426, 546);
            Controls.Add(mainLayout);
            Name = "DomainAttribute";
            Text = "DomainAttribute";
            Load += Form_Load;
            Controls.SetChildIndex(mainLayout, 0);
            mainLayout.ResumeLayout(false);
            mainLayout.PerformLayout();
            detailTabLayout.ResumeLayout(false);
            detailTab.ResumeLayout(false);
            detailsLayout.ResumeLayout(false);
            detailsLayout.PerformLayout();
            propertyTab.ResumeLayout(false);
            propertyLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)propertiesData).EndInit();
            definitionTab.ResumeLayout(false);
            definitionLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)definitionData).EndInit();
            aliasTab.ResumeLayout(false);
            aliaseLayout.ResumeLayout(false);
            aliaseLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)aliasesData).EndInit();
            aliasCommandLayout.ResumeLayout(false);
            aliasCommandLayout.PerformLayout();
            subjectAreaTab.ResumeLayout(false);
            subjectAreaLayout.ResumeLayout(false);
            subjectAreaLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingAttribute).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingProperty).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingAlias).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSubjectArea).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingDefinition).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataDictionary.Main.Controls.TextBoxData titleData;
        private DataDictionary.Main.Controls.TextBoxData descriptionData;
        private TabControl detailTabLayout;
        private TabPage detailTab;
        private TabPage propertyTab;
        private BindingSource bindingAttribute;
        private TabPage aliasTab;
        private CheckBox isSingleValueData;
        private CheckBox isMultiValuedData;
        private CheckBox isSimpleTypeData;
        private CheckBox isCompositeTypeData;
        private CheckBox isIntegralData;
        private CheckBox isDerivedData;
        private CheckBox isValuedData;
        private CheckBox isNullableData;
        private CheckBox isNonKeyData;
        private CheckBox isKeyData;
        private BindingSource bindingProperty;
        private TabPage subjectAreaTab;
        private TabPage entityTab;
        private BindingSource bindingAlias;
        private DataGridView propertiesData;
        private DataGridViewComboBoxColumn propertyIdColumn;
        private DataGridViewTextBoxColumn propertyValueColumn;
        private Controls.DomainProperty domainProperty;
        private BindingSource bindingSubjectArea;
        private Controls.SubjectArea subjectArea;
        private TabPage definitionTab;
        private DataGridView definitionData;
        private BindingSource bindingDefinition;
        private DataGridViewComboBoxColumn definitionColumn;
        private DataGridViewTextBoxColumn definitionSummaryColumn;
        private Controls.DomainDefinition domainDefinition;
        private DataDictionary.Main.Controls.TextBoxData memberNameData;
        private TableLayoutPanel aliaseLayout;
        private DataGridView aliasesData;
        private DataGridViewComboBoxColumn aliaseScopeColumn;
        private DataGridViewTextBoxColumn aliasNameColumn;
        private DataDictionary.Main.Controls.TextBoxData aliasNameData;
        private DataDictionary.Main.Controls.ComboBoxData aliasScopeData;
        private Button aliasSelectCommand;
        private Button aliasAddCommand;
        private CheckBox isAliasInModelData;
    }
}