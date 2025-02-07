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
            attributeData = new DataGridView();
            attributeAliasColumn = new DataGridViewTextBoxColumn();
            attributeOrderColumn = new DataGridViewTextBoxColumn();
            detailDataTab = new TabControl();
            detailEntityPage = new TabPage();
            detailEntityLayout = new TableLayoutPanel();
            attributeAliasData = new DataDictionary.Main.Controls.TextBoxData();
            attributeSelectCommand = new Button();
            attributeOrderData = new DataDictionary.Main.Controls.TextBoxData();
            attributeNullable = new CheckBox();
            attributePrimaryKey = new CheckBox();
            detailAttributePage = new TabPage();
            detailAttributeLayout = new TableLayoutPanel();
            attributePathData = new DataDictionary.Main.Controls.TextBoxData();
            attributeInModelData = new CheckBox();
            attributeTitleData = new DataDictionary.Main.Controls.TextBoxData();
            attributeDescriptionData = new DataDictionary.Main.Controls.TextBoxData();
            attributeNavigation = new DataDictionary.Main.Controls.NavigatorData();
            propertyTab = new TabPage();
            propertiesData = new DataGridView();
            propertyIdColumn = new DataGridViewComboBoxColumn();
            propertyValueColumn = new DataGridViewTextBoxColumn();
            propertyControl = new Controls.Property();
            definitionTab = new TabPage();
            definitionData = new DataGridView();
            definitionColumn = new DataGridViewComboBoxColumn();
            definitionSummaryColumn = new DataGridViewTextBoxColumn();
            definitionControl = new Controls.Definition();
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
            subjectArea = new Controls.SubjectArea();
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
            ((System.ComponentModel.ISupportInitialize)attributeData).BeginInit();
            detailDataTab.SuspendLayout();
            detailEntityPage.SuspendLayout();
            detailEntityLayout.SuspendLayout();
            detailAttributePage.SuspendLayout();
            detailAttributeLayout.SuspendLayout();
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
            detailsLayout.Controls.Add(attributeData, 0, 0);
            detailsLayout.Controls.Add(detailDataTab, 0, 1);
            detailsLayout.Dock = DockStyle.Fill;
            detailsLayout.Location = new Point(3, 3);
            detailsLayout.Name = "detailsLayout";
            detailsLayout.RowCount = 2;
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            detailsLayout.Size = new Size(470, 437);
            detailsLayout.TabIndex = 0;
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
            attributeData.Size = new Size(464, 168);
            attributeData.TabIndex = 0;
            // 
            // attributeAliasColumn
            // 
            attributeAliasColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            attributeAliasColumn.DataPropertyName = "AttributeTitle";
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
            // detailDataTab
            // 
            detailDataTab.Controls.Add(detailEntityPage);
            detailDataTab.Controls.Add(detailAttributePage);
            detailDataTab.Dock = DockStyle.Fill;
            detailDataTab.Location = new Point(3, 177);
            detailDataTab.Name = "detailDataTab";
            detailDataTab.SelectedIndex = 0;
            detailDataTab.Size = new Size(464, 257);
            detailDataTab.TabIndex = 8;
            // 
            // detailEntityPage
            // 
            detailEntityPage.BackColor = SystemColors.Control;
            detailEntityPage.Controls.Add(detailEntityLayout);
            detailEntityPage.Location = new Point(4, 24);
            detailEntityPage.Name = "detailEntityPage";
            detailEntityPage.Padding = new Padding(3);
            detailEntityPage.Size = new Size(456, 229);
            detailEntityPage.TabIndex = 0;
            detailEntityPage.Text = "Entity";
            // 
            // detailEntityLayout
            // 
            detailEntityLayout.ColumnCount = 3;
            detailEntityLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            detailEntityLayout.ColumnStyles.Add(new ColumnStyle());
            detailEntityLayout.ColumnStyles.Add(new ColumnStyle());
            detailEntityLayout.Controls.Add(attributeAliasData, 0, 0);
            detailEntityLayout.Controls.Add(attributeSelectCommand, 2, 2);
            detailEntityLayout.Controls.Add(attributeOrderData, 2, 0);
            detailEntityLayout.Controls.Add(attributeNullable, 1, 1);
            detailEntityLayout.Controls.Add(attributePrimaryKey, 2, 1);
            detailEntityLayout.Dock = DockStyle.Fill;
            detailEntityLayout.Location = new Point(3, 3);
            detailEntityLayout.Name = "detailEntityLayout";
            detailEntityLayout.RowCount = 3;
            detailEntityLayout.RowStyles.Add(new RowStyle());
            detailEntityLayout.RowStyles.Add(new RowStyle());
            detailEntityLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            detailEntityLayout.Size = new Size(450, 223);
            detailEntityLayout.TabIndex = 0;
            // 
            // attributeAliasData
            // 
            attributeAliasData.AutoSize = true;
            detailEntityLayout.SetColumnSpan(attributeAliasData, 2);
            attributeAliasData.Dock = DockStyle.Fill;
            attributeAliasData.HeaderText = "Attribute Name (within Entity)";
            attributeAliasData.Location = new Point(3, 3);
            attributeAliasData.Multiline = false;
            attributeAliasData.Name = "attributeAliasData";
            attributeAliasData.ReadOnly = false;
            attributeAliasData.Size = new Size(318, 44);
            attributeAliasData.TabIndex = 5;
            attributeAliasData.WordWrap = true;
            // 
            // attributeSelectCommand
            // 
            attributeSelectCommand.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            attributeSelectCommand.AutoSize = true;
            attributeSelectCommand.Location = new Point(372, 195);
            attributeSelectCommand.Name = "attributeSelectCommand";
            attributeSelectCommand.Size = new Size(75, 25);
            attributeSelectCommand.TabIndex = 4;
            attributeSelectCommand.Text = "Select";
            attributeSelectCommand.TextImageRelation = TextImageRelation.ImageBeforeText;
            attributeSelectCommand.UseVisualStyleBackColor = true;
            attributeSelectCommand.Click += AttributeSelect_Click;
            // 
            // attributeOrderData
            // 
            attributeOrderData.AutoSize = true;
            attributeOrderData.HeaderText = "Order";
            attributeOrderData.Location = new Point(327, 3);
            attributeOrderData.Multiline = false;
            attributeOrderData.Name = "attributeOrderData";
            attributeOrderData.ReadOnly = false;
            attributeOrderData.Size = new Size(120, 44);
            attributeOrderData.TabIndex = 3;
            attributeOrderData.WordWrap = true;
            // 
            // attributeNullable
            // 
            attributeNullable.AutoSize = true;
            attributeNullable.Location = new Point(235, 53);
            attributeNullable.Name = "attributeNullable";
            attributeNullable.Size = new Size(86, 19);
            attributeNullable.TabIndex = 6;
            attributeNullable.Text = "Allow Nulls";
            attributeNullable.UseVisualStyleBackColor = true;
            // 
            // attributePrimaryKey
            // 
            attributePrimaryKey.AutoSize = true;
            attributePrimaryKey.Location = new Point(327, 53);
            attributePrimaryKey.Name = "attributePrimaryKey";
            attributePrimaryKey.Size = new Size(89, 19);
            attributePrimaryKey.TabIndex = 7;
            attributePrimaryKey.Text = "Primary Key";
            attributePrimaryKey.UseVisualStyleBackColor = true;
            // 
            // detailAttributePage
            // 
            detailAttributePage.BackColor = SystemColors.Control;
            detailAttributePage.Controls.Add(detailAttributeLayout);
            detailAttributePage.Location = new Point(4, 24);
            detailAttributePage.Name = "detailAttributePage";
            detailAttributePage.Padding = new Padding(3);
            detailAttributePage.Size = new Size(192, 72);
            detailAttributePage.TabIndex = 1;
            detailAttributePage.Text = "Attribute";
            // 
            // detailAttributeLayout
            // 
            detailAttributeLayout.ColumnCount = 2;
            detailAttributeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            detailAttributeLayout.ColumnStyles.Add(new ColumnStyle());
            detailAttributeLayout.Controls.Add(attributePathData, 0, 0);
            detailAttributeLayout.Controls.Add(attributeInModelData, 1, 0);
            detailAttributeLayout.Controls.Add(attributeTitleData, 0, 1);
            detailAttributeLayout.Controls.Add(attributeDescriptionData, 0, 2);
            detailAttributeLayout.Controls.Add(attributeNavigation, 0, 3);
            detailAttributeLayout.Dock = DockStyle.Fill;
            detailAttributeLayout.Location = new Point(3, 3);
            detailAttributeLayout.Name = "detailAttributeLayout";
            detailAttributeLayout.RowCount = 4;
            detailAttributeLayout.RowStyles.Add(new RowStyle());
            detailAttributeLayout.RowStyles.Add(new RowStyle());
            detailAttributeLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            detailAttributeLayout.RowStyles.Add(new RowStyle());
            detailAttributeLayout.Size = new Size(186, 66);
            detailAttributeLayout.TabIndex = 2;
            // 
            // attributePathData
            // 
            attributePathData.AutoSize = true;
            attributePathData.Dock = DockStyle.Fill;
            attributePathData.HeaderText = "Attribute Alias";
            attributePathData.Location = new Point(3, 3);
            attributePathData.Multiline = false;
            attributePathData.Name = "attributePathData";
            attributePathData.ReadOnly = false;
            attributePathData.Size = new Size(101, 44);
            attributePathData.TabIndex = 1;
            attributePathData.WordWrap = true;
            attributePathData.Validated += AttributeTitleData_Validated;
            // 
            // attributeInModelData
            // 
            attributeInModelData.AutoSize = true;
            attributeInModelData.Enabled = false;
            attributeInModelData.Location = new Point(110, 3);
            attributeInModelData.Name = "attributeInModelData";
            attributeInModelData.Size = new Size(73, 19);
            attributeInModelData.TabIndex = 2;
            attributeInModelData.Text = "in Model";
            attributeInModelData.UseVisualStyleBackColor = true;
            // 
            // attributeTitleData
            // 
            attributeTitleData.AutoSize = true;
            detailAttributeLayout.SetColumnSpan(attributeTitleData, 2);
            attributeTitleData.Dock = DockStyle.Fill;
            attributeTitleData.HeaderText = "Attribute Title";
            attributeTitleData.Location = new Point(3, 53);
            attributeTitleData.Multiline = false;
            attributeTitleData.Name = "attributeTitleData";
            attributeTitleData.ReadOnly = false;
            attributeTitleData.Size = new Size(180, 44);
            attributeTitleData.TabIndex = 3;
            attributeTitleData.WordWrap = true;
            // 
            // attributeDescriptionData
            // 
            attributeDescriptionData.AutoSize = true;
            detailAttributeLayout.SetColumnSpan(attributeDescriptionData, 2);
            attributeDescriptionData.Dock = DockStyle.Fill;
            attributeDescriptionData.HeaderText = "Attribute Description";
            attributeDescriptionData.Location = new Point(3, 103);
            attributeDescriptionData.Multiline = true;
            attributeDescriptionData.Name = "attributeDescriptionData";
            attributeDescriptionData.ReadOnly = false;
            attributeDescriptionData.Size = new Size(180, 1);
            attributeDescriptionData.TabIndex = 4;
            attributeDescriptionData.WordWrap = true;
            // 
            // attributeNavigation
            // 
            attributeNavigation.AutoSize = true;
            attributeNavigation.BindingSource = null;
            attributeNavigation.Dock = DockStyle.Fill;
            attributeNavigation.Location = new Point(3, 38);
            attributeNavigation.Name = "attributeNavigation";
            attributeNavigation.Size = new Size(101, 25);
            attributeNavigation.TabIndex = 5;
            // 
            // propertyTab
            // 
            propertyTab.BackColor = SystemColors.Control;
            propertyTab.Controls.Add(propertyLayout);
            propertyTab.Location = new Point(4, 24);
            propertyTab.Name = "propertyTab";
            propertyTab.Padding = new Padding(3);
            propertyTab.Size = new Size(476, 443);
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
            propertyLayout.Size = new Size(470, 437);
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
            propertiesData.Size = new Size(464, 168);
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
            propertyControl.Location = new Point(3, 177);
            propertyControl.Name = "propertyControl";
            propertyControl.Size = new Size(464, 257);
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
            bindingAlias.AddingNew += BindingAlias_AddingNew;
            bindingAlias.DataError += bindingAlias_DataError;
            bindingAlias.CurrentChanged += BindingAlias_CurrentChanged;
            // 
            // bindingProperty
            // 
            bindingProperty.AddingNew += BindingProperty_AddingNew;
            bindingProperty.CurrentChanged += BindingProperty_CurrentChanged;
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
            ((System.ComponentModel.ISupportInitialize)attributeData).EndInit();
            detailDataTab.ResumeLayout(false);
            detailEntityPage.ResumeLayout(false);
            detailEntityLayout.ResumeLayout(false);
            detailEntityLayout.PerformLayout();
            detailAttributePage.ResumeLayout(false);
            detailAttributeLayout.ResumeLayout(false);
            detailAttributeLayout.PerformLayout();
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
        private Button attributeSelectCommand;
        private DataDictionary.Main.Controls.TextBoxData attributeAliasData;
        private CheckBox attributeNullable;
        private CheckBox attributePrimaryKey;
        private DataGridViewTextBoxColumn attributeAliasColumn;
        private DataGridViewTextBoxColumn attributeOrderColumn;
        private DataGridViewComboBoxColumn aliaseScopeColumn;
        private DataGridViewTextBoxColumn aliasNameColumn;
        private TabControl detailDataTab;
        private TabPage detailEntityPage;
        private TableLayoutPanel detailEntityLayout;
        private TabPage detailAttributePage;
        private TableLayoutPanel detailAttributeLayout;
        private CheckBox attributeInModelData;
        private DataDictionary.Main.Controls.TextBoxData attributeTitleData;
        private DataDictionary.Main.Controls.TextBoxData attributeDescriptionData;
        private BindingSource bindingAttributeDetail;
        private DataDictionary.Main.Controls.NavigatorData attributeNavigation;
        private Controls.Property propertyControl;
        private Controls.Definition definitionControl;
    }
}