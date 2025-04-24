using DataDictionary.Resource.Enumerations;

namespace DataDictionary.Main.Forms.Model
{
    partial class Entity
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
            TableLayoutPanel definitionLayout;
            TableLayoutPanel aliasCommandLayout;
            titleData = new DataDictionary.Main.Controls.TextBoxData();
            descriptionData = new DataDictionary.Main.Controls.TextBoxData();
            detailTabLayout = new TabControl();
            detailTab = new TabPage();
            attributeLayout = new TableLayoutPanel();
            attributeDescriptionData = new DataDictionary.Main.Controls.TextBoxData();
            attributeTitleData = new DataDictionary.Main.Controls.TextBoxData();
            attributePathData = new DataDictionary.Main.Controls.TextBoxData();
            attributeKnownAsData = new DataDictionary.Main.Controls.TextBoxData();
            attributeOptionsLayout = new TableLayoutPanel();
            attributeNullable = new CheckBox();
            attributePrimaryKey = new CheckBox();
            attributeOrderData = new DataDictionary.Main.Controls.TextBoxData();
            attributeInModelData = new CheckBox();
            attributeData = new DataGridView();
            attributeAliasColumn = new DataGridViewTextBoxColumn();
            attributeOrderColumn = new DataGridViewTextBoxColumn();
            attributeButtonLayout = new TableLayoutPanel();
            attributeSelectCommand = new Button();
            attributeNewCommand = new Button();
            propertyTab = new TabPage();
            propertiesData = new DataGridView();
            propertyIdColumn = new DataGridViewComboBoxColumn();
            propertyValueColumn = new DataGridViewTextBoxColumn();
            propertyControl = new DataDictionary.Main.Forms.Model.Controls.Property();
            definitionTab = new TabPage();
            definitionData = new DataGridView();
            definitionColumn = new DataGridViewComboBoxColumn();
            definitionSummaryColumn = new DataGridViewTextBoxColumn();
            definitionControl = new DataDictionary.Main.Forms.Model.Controls.Definition();
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
            subjectAreaLayout = new TableLayoutPanel();
            subjectArea = new DataDictionary.Main.Forms.Model.Controls.SubjectArea();
            memberNameData = new DataDictionary.Main.Controls.TextBoxData();
            bindingAlias = new BindingSource(components);
            bindingProperty = new BindingSource(components);
            bindingEntity = new BindingSource(components);
            bindingSubjectArea = new BindingSource(components);
            bindingDefinition = new BindingSource(components);
            bindingAttribute = new BindingSource(components);
            bindingAttributeDetail = new BindingSource(components);
            mainLayout = new TableLayoutPanel();
            detailsLayout = new TableLayoutPanel();
            propertyLayout = new TableLayoutPanel();
            definitionLayout = new TableLayoutPanel();
            aliasCommandLayout = new TableLayoutPanel();
            mainLayout.SuspendLayout();
            detailTabLayout.SuspendLayout();
            detailTab.SuspendLayout();
            detailsLayout.SuspendLayout();
            attributeLayout.SuspendLayout();
            attributeOptionsLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)attributeData).BeginInit();
            attributeButtonLayout.SuspendLayout();
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
            ((System.ComponentModel.ISupportInitialize)bindingAlias).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingProperty).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingEntity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSubjectArea).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingDefinition).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingAttribute).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingAttributeDetail).BeginInit();
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
            mainLayout.Size = new Size(490, 646);
            mainLayout.TabIndex = 2;
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
            titleData.Size = new Size(484, 44);
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
            descriptionData.Size = new Size(484, 113);
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
            detailTabLayout.Dock = DockStyle.Fill;
            detailTabLayout.Location = new Point(3, 172);
            detailTabLayout.Name = "detailTabLayout";
            detailTabLayout.SelectedIndex = 0;
            detailTabLayout.Size = new Size(484, 471);
            detailTabLayout.TabIndex = 2;
            // 
            // detailTab
            // 
            detailTab.BackColor = SystemColors.Control;
            detailTab.Controls.Add(detailsLayout);
            detailTab.Location = new Point(4, 24);
            detailTab.Name = "detailTab";
            detailTab.Padding = new Padding(3);
            detailTab.Size = new Size(476, 443);
            detailTab.TabIndex = 0;
            detailTab.Text = "Details";
            // 
            // detailsLayout
            // 
            detailsLayout.ColumnCount = 1;
            detailsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            detailsLayout.Controls.Add(attributeLayout, 0, 1);
            detailsLayout.Controls.Add(attributeData, 0, 0);
            detailsLayout.Controls.Add(attributeButtonLayout, 0, 2);
            detailsLayout.Dock = DockStyle.Fill;
            detailsLayout.Location = new Point(3, 3);
            detailsLayout.Name = "detailsLayout";
            detailsLayout.RowCount = 3;
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            detailsLayout.RowStyles.Add(new RowStyle());
            detailsLayout.Size = new Size(470, 437);
            detailsLayout.TabIndex = 0;
            // 
            // attributeLayout
            // 
            attributeLayout.ColumnCount = 2;
            attributeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            attributeLayout.ColumnStyles.Add(new ColumnStyle());
            attributeLayout.Controls.Add(attributeDescriptionData, 0, 3);
            attributeLayout.Controls.Add(attributeTitleData, 0, 2);
            attributeLayout.Controls.Add(attributePathData, 0, 1);
            attributeLayout.Controls.Add(attributeKnownAsData, 0, 0);
            attributeLayout.Controls.Add(attributeOptionsLayout, 1, 0);
            attributeLayout.Controls.Add(attributeInModelData, 1, 2);
            attributeLayout.Dock = DockStyle.Fill;
            attributeLayout.Location = new Point(3, 163);
            attributeLayout.Name = "attributeLayout";
            attributeLayout.RowCount = 4;
            attributeLayout.RowStyles.Add(new RowStyle());
            attributeLayout.RowStyles.Add(new RowStyle());
            attributeLayout.RowStyles.Add(new RowStyle());
            attributeLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            attributeLayout.Size = new Size(464, 234);
            attributeLayout.TabIndex = 0;
            // 
            // attributeDescriptionData
            // 
            attributeDescriptionData.AutoSize = true;
            attributeLayout.SetColumnSpan(attributeDescriptionData, 2);
            attributeDescriptionData.Dock = DockStyle.Fill;
            attributeDescriptionData.HeaderText = "Attribute Description";
            attributeDescriptionData.Location = new Point(3, 159);
            attributeDescriptionData.Multiline = true;
            attributeDescriptionData.Name = "attributeDescriptionData";
            attributeDescriptionData.ReadOnly = true;
            attributeDescriptionData.Size = new Size(458, 72);
            attributeDescriptionData.TabIndex = 4;
            attributeDescriptionData.WordWrap = true;
            // 
            // attributeTitleData
            // 
            attributeTitleData.AutoSize = true;
            attributeTitleData.Dock = DockStyle.Fill;
            attributeTitleData.HeaderText = "Attribute Title";
            attributeTitleData.Location = new Point(3, 109);
            attributeTitleData.Multiline = false;
            attributeTitleData.Name = "attributeTitleData";
            attributeTitleData.ReadOnly = true;
            attributeTitleData.Size = new Size(326, 44);
            attributeTitleData.TabIndex = 3;
            attributeTitleData.WordWrap = true;
            // 
            // attributePathData
            // 
            attributePathData.AutoSize = true;
            attributePathData.Dock = DockStyle.Fill;
            attributePathData.HeaderText = "Attribute Alias";
            attributePathData.Location = new Point(3, 53);
            attributePathData.Multiline = false;
            attributePathData.Name = "attributePathData";
            attributePathData.ReadOnly = false;
            attributePathData.Size = new Size(326, 50);
            attributePathData.TabIndex = 1;
            attributePathData.WordWrap = true;
            attributePathData.Validating += AttributePathData_Validating;
            // 
            // attributeKnownAsData
            // 
            attributeKnownAsData.AutoSize = true;
            attributeKnownAsData.Dock = DockStyle.Fill;
            attributeKnownAsData.HeaderText = "Attribute Known As";
            attributeKnownAsData.Location = new Point(3, 3);
            attributeKnownAsData.Multiline = false;
            attributeKnownAsData.Name = "attributeKnownAsData";
            attributeKnownAsData.ReadOnly = false;
            attributeKnownAsData.Size = new Size(326, 44);
            attributeKnownAsData.TabIndex = 5;
            attributeKnownAsData.WordWrap = true;
            // 
            // attributeOptionsLayout
            // 
            attributeOptionsLayout.AutoSize = true;
            attributeOptionsLayout.ColumnCount = 1;
            attributeOptionsLayout.ColumnStyles.Add(new ColumnStyle());
            attributeOptionsLayout.Controls.Add(attributeNullable, 0, 0);
            attributeOptionsLayout.Controls.Add(attributePrimaryKey, 0, 1);
            attributeOptionsLayout.Controls.Add(attributeOrderData, 0, 2);
            attributeOptionsLayout.Location = new Point(335, 3);
            attributeOptionsLayout.Name = "attributeOptionsLayout";
            attributeOptionsLayout.RowCount = 3;
            attributeLayout.SetRowSpan(attributeOptionsLayout, 2);
            attributeOptionsLayout.RowStyles.Add(new RowStyle());
            attributeOptionsLayout.RowStyles.Add(new RowStyle());
            attributeOptionsLayout.RowStyles.Add(new RowStyle());
            attributeOptionsLayout.Size = new Size(126, 100);
            attributeOptionsLayout.TabIndex = 8;
            // 
            // attributeNullable
            // 
            attributeNullable.AutoSize = true;
            attributeNullable.Location = new Point(3, 3);
            attributeNullable.Name = "attributeNullable";
            attributeNullable.Size = new Size(86, 19);
            attributeNullable.TabIndex = 6;
            attributeNullable.Text = "Allow Nulls";
            attributeNullable.UseVisualStyleBackColor = true;
            // 
            // attributePrimaryKey
            // 
            attributePrimaryKey.AutoSize = true;
            attributePrimaryKey.Location = new Point(3, 28);
            attributePrimaryKey.Name = "attributePrimaryKey";
            attributePrimaryKey.Size = new Size(89, 19);
            attributePrimaryKey.TabIndex = 7;
            attributePrimaryKey.Text = "Primary Key";
            attributePrimaryKey.UseVisualStyleBackColor = true;
            // 
            // attributeOrderData
            // 
            attributeOrderData.AutoSize = true;
            attributeOrderData.HeaderText = "Order";
            attributeOrderData.Location = new Point(3, 53);
            attributeOrderData.Multiline = false;
            attributeOrderData.Name = "attributeOrderData";
            attributeOrderData.ReadOnly = false;
            attributeOrderData.Size = new Size(120, 44);
            attributeOrderData.TabIndex = 3;
            attributeOrderData.WordWrap = true;
            // 
            // attributeInModelData
            // 
            attributeInModelData.AutoSize = true;
            attributeInModelData.Enabled = false;
            attributeInModelData.Location = new Point(335, 109);
            attributeInModelData.Name = "attributeInModelData";
            attributeInModelData.Size = new Size(73, 19);
            attributeInModelData.TabIndex = 2;
            attributeInModelData.Text = "in Model";
            attributeInModelData.UseVisualStyleBackColor = true;
            // 
            // attributeData
            // 
            attributeData.AllowUserToAddRows = false;
            attributeData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            attributeData.Columns.AddRange(new DataGridViewColumn[] { attributeAliasColumn, attributeOrderColumn });
            attributeData.Dock = DockStyle.Fill;
            attributeData.Location = new Point(3, 3);
            attributeData.Name = "attributeData";
            attributeData.ReadOnly = true;
            attributeData.Size = new Size(464, 154);
            attributeData.TabIndex = 0;
            // 
            // attributeAliasColumn
            // 
            attributeAliasColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            attributeAliasColumn.DataPropertyName = "AttributeKnownAs";
            attributeAliasColumn.FillWeight = 40F;
            attributeAliasColumn.HeaderText = "Attribute";
            attributeAliasColumn.Name = "attributeAliasColumn";
            attributeAliasColumn.ReadOnly = true;
            // 
            // attributeOrderColumn
            // 
            attributeOrderColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            attributeOrderColumn.DataPropertyName = "OrdinalPosition";
            attributeOrderColumn.FillWeight = 20F;
            attributeOrderColumn.HeaderText = "Order";
            attributeOrderColumn.Name = "attributeOrderColumn";
            attributeOrderColumn.ReadOnly = true;
            // 
            // attributeButtonLayout
            // 
            attributeButtonLayout.AutoSize = true;
            attributeButtonLayout.ColumnCount = 3;
            attributeButtonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            attributeButtonLayout.ColumnStyles.Add(new ColumnStyle());
            attributeButtonLayout.ColumnStyles.Add(new ColumnStyle());
            attributeButtonLayout.Controls.Add(attributeSelectCommand, 2, 0);
            attributeButtonLayout.Controls.Add(attributeNewCommand, 1, 0);
            attributeButtonLayout.Dock = DockStyle.Fill;
            attributeButtonLayout.Location = new Point(3, 403);
            attributeButtonLayout.Name = "attributeButtonLayout";
            attributeButtonLayout.RowCount = 1;
            attributeButtonLayout.RowStyles.Add(new RowStyle());
            attributeButtonLayout.Size = new Size(464, 31);
            attributeButtonLayout.TabIndex = 1;
            // 
            // attributeSelectCommand
            // 
            attributeSelectCommand.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            attributeSelectCommand.Location = new Point(386, 3);
            attributeSelectCommand.Name = "attributeSelectCommand";
            attributeSelectCommand.Size = new Size(75, 25);
            attributeSelectCommand.TabIndex = 4;
            attributeSelectCommand.Text = "Select";
            attributeSelectCommand.TextImageRelation = TextImageRelation.ImageBeforeText;
            attributeSelectCommand.UseVisualStyleBackColor = true;
            attributeSelectCommand.Click += AttributeSelect_Click;
            // 
            // attributeNewCommand
            // 
            attributeNewCommand.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            attributeNewCommand.Location = new Point(305, 5);
            attributeNewCommand.Name = "attributeNewCommand";
            attributeNewCommand.Size = new Size(75, 23);
            attributeNewCommand.TabIndex = 5;
            attributeNewCommand.Text = "New";
            attributeNewCommand.TextImageRelation = TextImageRelation.ImageBeforeText;
            attributeNewCommand.UseVisualStyleBackColor = true;
            attributeNewCommand.Click += AttributeNewCommand_Click;
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
            propertyLayout.Controls.Add(propertyControl, 0, 1);
            propertyLayout.Dock = DockStyle.Fill;
            propertyLayout.Location = new Point(3, 3);
            propertyLayout.Name = "propertyLayout";
            propertyLayout.RowCount = 2;
            propertyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            propertyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
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
            propertiesData.Size = new Size(180, 20);
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
            // propertyControl
            // 
            propertyControl.Dock = DockStyle.Fill;
            propertyControl.Location = new Point(3, 29);
            propertyControl.Name = "propertyControl";
            propertyControl.Size = new Size(180, 34);
            propertyControl.TabIndex = 2;
            // 
            // definitionTab
            // 
            definitionTab.BackColor = SystemColors.Control;
            definitionTab.Controls.Add(definitionLayout);
            definitionTab.Location = new Point(4, 24);
            definitionTab.Name = "definitionTab";
            definitionTab.Padding = new Padding(3);
            definitionTab.Size = new Size(192, 72);
            definitionTab.TabIndex = 4;
            definitionTab.Text = "Definitions";
            // 
            // definitionLayout
            // 
            definitionLayout.ColumnCount = 1;
            definitionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            definitionLayout.Controls.Add(definitionData, 0, 0);
            definitionLayout.Controls.Add(definitionControl, 0, 1);
            definitionLayout.Dock = DockStyle.Fill;
            definitionLayout.Location = new Point(3, 3);
            definitionLayout.Name = "definitionLayout";
            definitionLayout.Padding = new Padding(3);
            definitionLayout.RowCount = 2;
            definitionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            definitionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            definitionLayout.Size = new Size(186, 66);
            definitionLayout.TabIndex = 1;
            // 
            // definitionData
            // 
            definitionData.AllowUserToAddRows = false;
            definitionData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            definitionData.Columns.AddRange(new DataGridViewColumn[] { definitionColumn, definitionSummaryColumn });
            definitionData.Dock = DockStyle.Fill;
            definitionData.Location = new Point(3, 3);
            definitionData.Margin = new Padding(0);
            definitionData.Name = "definitionData";
            definitionData.ReadOnly = true;
            definitionData.Size = new Size(180, 18);
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
            // definitionControl
            // 
            definitionControl.Dock = DockStyle.Fill;
            definitionControl.Location = new Point(6, 24);
            definitionControl.Name = "definitionControl";
            definitionControl.Size = new Size(174, 36);
            definitionControl.TabIndex = 1;
            // 
            // aliasTab
            // 
            aliasTab.BackColor = SystemColors.Control;
            aliasTab.Controls.Add(aliaseLayout);
            aliasTab.Location = new Point(4, 24);
            aliasTab.Name = "aliasTab";
            aliasTab.Padding = new Padding(3);
            aliasTab.Size = new Size(192, 72);
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
            aliaseLayout.Size = new Size(186, 66);
            aliaseLayout.TabIndex = 1;
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
            aliasesData.Size = new Size(180, 1);
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
            aliasNameColumn.DataPropertyName = "AliasPath";
            aliasNameColumn.HeaderText = "Alias Name";
            aliasNameColumn.Name = "aliasNameColumn";
            aliasNameColumn.ReadOnly = true;
            // 
            // aliasNameData
            // 
            aliasNameData.AutoSize = true;
            aliasNameData.Dock = DockStyle.Fill;
            aliasNameData.HeaderText = "Alias Name";
            aliasNameData.Location = new Point(3, 19);
            aliasNameData.Multiline = false;
            aliasNameData.Name = "aliasNameData";
            aliasNameData.ReadOnly = true;
            aliasNameData.Size = new Size(93, 44);
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
            aliasScopeData.Location = new Point(3, -33);
            aliasScopeData.Name = "aliasScopeData";
            aliasScopeData.ReadOnly = true;
            aliasScopeData.Size = new Size(93, 46);
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
            aliasCommandLayout.Location = new Point(102, -33);
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
            // bindingAlias
            // 
            bindingAlias.CurrentChanged += BindingAlias_CurrentChanged;
            // 
            // bindingProperty
            // 
            bindingProperty.AddingNew += BindingProperty_AddingNew;
            bindingProperty.CurrentChanged += BindingProperty_CurrentChanged;
            // 
            // bindingDefinition
            // 
            bindingDefinition.AddingNew += BindingDefinition_AddingNew;
            bindingDefinition.CurrentChanged += BindingDefinition_CurrentChanged;
            // 
            // bindingAttribute
            // 
            bindingAttribute.AddingNew += BindingAttribute_AddingNew;
            bindingAttribute.CurrentChanged += BindingAttribute_CurrentChanged;
            // 
            // bindingAttributeDetail
            // 
            bindingAttributeDetail.AllowNew = false;
            // 
            // Entity
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(490, 671);
            Controls.Add(mainLayout);
            Name = "Entity";
            Text = "DomainEntity";
            Load += Form_Load;
            Controls.SetChildIndex(mainLayout, 0);
            mainLayout.ResumeLayout(false);
            mainLayout.PerformLayout();
            detailTabLayout.ResumeLayout(false);
            detailTab.ResumeLayout(false);
            detailsLayout.ResumeLayout(false);
            detailsLayout.PerformLayout();
            attributeLayout.ResumeLayout(false);
            attributeLayout.PerformLayout();
            attributeOptionsLayout.ResumeLayout(false);
            attributeOptionsLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)attributeData).EndInit();
            attributeButtonLayout.ResumeLayout(false);
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
            ((System.ComponentModel.ISupportInitialize)bindingAlias).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingProperty).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingEntity).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSubjectArea).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingDefinition).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingAttribute).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingAttributeDetail).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataDictionary.Main.Controls.TextBoxData titleData;
        private DataDictionary.Main.Controls.TextBoxData descriptionData;
        private TabControl detailTabLayout;
        private TabPage detailTab;
        private TabPage propertyTab;
        private DataGridView propertiesData;
        private DataGridViewComboBoxColumn propertyIdColumn;
        private DataGridViewTextBoxColumn propertyValueColumn;
        private TabPage aliasTab;
        private TableLayoutPanel aliaseLayout;
        private DataGridView aliasesData;
        private TabPage subjectAreaTab;
        private BindingSource bindingAlias;
        private BindingSource bindingProperty;
        private BindingSource bindingEntity;
        private BindingSource bindingSubjectArea;
        private Controls.SubjectArea subjectArea;
        private TabPage definitionTab;
        private BindingSource bindingDefinition;
        private DataGridView definitionData;
        private DataGridViewComboBoxColumn definitionColumn;
        private DataGridViewTextBoxColumn definitionSummaryColumn;
        private TableLayoutPanel subjectAreaLayout;
        private DataDictionary.Main.Controls.TextBoxData memberNameData;
        private DataGridView attributeData;
        private BindingSource bindingAttribute;
        private DataDictionary.Main.Controls.TextBoxData attributeOrderData;
        private DataDictionary.Main.Controls.TextBoxData attributePathData;
        private DataDictionary.Main.Controls.ComboBoxData aliasScopeData;
        private DataDictionary.Main.Controls.TextBoxData aliasNameData;
        private Button aliasSelectCommand;
        private CheckBox isAliasInModelData;
        private Button aliasAddCommand;
        private DataDictionary.Main.Controls.TextBoxData attributeKnownAsData;
        private CheckBox attributeNullable;
        private CheckBox attributePrimaryKey;
        private DataGridViewComboBoxColumn aliaseScopeColumn;
        private DataGridViewTextBoxColumn aliasNameColumn;
        private TableLayoutPanel attributeLayout;
        private CheckBox attributeInModelData;
        private DataDictionary.Main.Controls.TextBoxData attributeTitleData;
        private DataDictionary.Main.Controls.TextBoxData attributeDescriptionData;
        private BindingSource bindingAttributeDetail;
        private Controls.Property propertyControl;
        private Controls.Definition definitionControl;
        private TableLayoutPanel attributeOptionsLayout;
        private Button attributeSelectCommand;
        private DataGridViewTextBoxColumn attributeAliasColumn;
        private DataGridViewTextBoxColumn attributeOrderColumn;
        private TableLayoutPanel attributeButtonLayout;
        private Button attributeNewCommand;
    }
}