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
            TableLayoutPanel attributeLayout;
            TabControl attributeTabLayout;
            TabPage attributePropertyTab;
            TableLayoutPanel propertyLayout;
            TabControl propertyTabLayout;
            TableLayoutPanel propertyValueLayout;
            TabPage attributeDbAlaisTab;
            TableLayoutPanel databaseAliasLayout;
            TabPage libraryAliasTab;
            attributeDescriptionData = new Controls.TextBoxData();
            attributeTitleData = new Controls.TextBoxData();
            attributeParentTitleData = new Controls.TextBoxData();
            propertyNavigation = new DataGridView();
            propertyTypeColumn = new DataGridViewComboBoxColumn();
            propertyValueColumn = new DataGridViewTextBoxColumn();
            propertyValueTab = new TabPage();
            propertyTypeData = new Controls.ComboBoxData();
            propertyValueData = new Controls.TextBoxData();
            propertyChoiceData = new CheckedListBox();
            prropertyDefinitionTab = new TabPage();
            propertyDefinitionData = new Controls.RichTextBoxData();
            attributeAlaisData = new DataGridView();
            aliasCatalogNameData = new DataGridViewTextBoxColumn();
            aliasSchemaNameData = new DataGridViewTextBoxColumn();
            alaisObjectNameData = new DataGridViewTextBoxColumn();
            aliasElementNameData = new DataGridViewTextBoxColumn();
            catalogNameData = new Controls.ComboBoxData();
            schemaNameData = new Controls.ComboBoxData();
            objectNameData = new Controls.ComboBoxData();
            elementNameData = new Controls.ComboBoxData();
            libraryAlaisToBeDetermined = new Label();
            bindingDefinition = new BindingSource(components);
            errorProvider = new ErrorProvider(components);
            bindingProperties = new BindingSource(components);
            bindingDatabaseAlias = new BindingSource(components);
            attributeLayout = new TableLayoutPanel();
            attributeTabLayout = new TabControl();
            attributePropertyTab = new TabPage();
            propertyLayout = new TableLayoutPanel();
            propertyTabLayout = new TabControl();
            propertyValueLayout = new TableLayoutPanel();
            attributeDbAlaisTab = new TabPage();
            databaseAliasLayout = new TableLayoutPanel();
            libraryAliasTab = new TabPage();
            attributeLayout.SuspendLayout();
            attributeTabLayout.SuspendLayout();
            attributePropertyTab.SuspendLayout();
            propertyLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)propertyNavigation).BeginInit();
            propertyTabLayout.SuspendLayout();
            propertyValueTab.SuspendLayout();
            propertyValueLayout.SuspendLayout();
            prropertyDefinitionTab.SuspendLayout();
            attributeDbAlaisTab.SuspendLayout();
            databaseAliasLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)attributeAlaisData).BeginInit();
            libraryAliasTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingDefinition).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingProperties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingDatabaseAlias).BeginInit();
            SuspendLayout();
            // 
            // attributeLayout
            // 
            attributeLayout.AutoSize = true;
            attributeLayout.ColumnCount = 1;
            attributeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            attributeLayout.Controls.Add(attributeDescriptionData, 0, 2);
            attributeLayout.Controls.Add(attributeTitleData, 0, 0);
            attributeLayout.Controls.Add(attributeParentTitleData, 0, 1);
            attributeLayout.Controls.Add(attributeTabLayout, 0, 3);
            attributeLayout.Dock = DockStyle.Fill;
            attributeLayout.Location = new Point(0, 25);
            attributeLayout.Name = "attributeLayout";
            attributeLayout.RowCount = 4;
            attributeLayout.RowStyles.Add(new RowStyle());
            attributeLayout.RowStyles.Add(new RowStyle());
            attributeLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            attributeLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            attributeLayout.Size = new Size(529, 616);
            attributeLayout.TabIndex = 0;
            // 
            // attributeDescriptionData
            // 
            attributeDescriptionData.AutoSize = true;
            attributeDescriptionData.Dock = DockStyle.Fill;
            attributeDescriptionData.HeaderText = "Description (Summary)";
            attributeDescriptionData.Location = new Point(3, 91);
            attributeDescriptionData.Multiline = true;
            attributeDescriptionData.Name = "attributeDescriptionData";
            attributeDescriptionData.ReadOnly = false;
            attributeDescriptionData.Size = new Size(523, 99);
            attributeDescriptionData.TabIndex = 0;
            // 
            // attributeTitleData
            // 
            attributeTitleData.AutoSize = true;
            attributeTitleData.Dock = DockStyle.Fill;
            attributeTitleData.HeaderText = "Attribute Title";
            attributeTitleData.Location = new Point(0, 0);
            attributeTitleData.Margin = new Padding(0);
            attributeTitleData.Multiline = false;
            attributeTitleData.Name = "attributeTitleData";
            attributeTitleData.ReadOnly = false;
            attributeTitleData.Size = new Size(529, 44);
            attributeTitleData.TabIndex = 0;
            // 
            // attributeParentTitleData
            // 
            attributeParentTitleData.AutoSize = true;
            attributeParentTitleData.Dock = DockStyle.Fill;
            attributeParentTitleData.HeaderText = "Parent Attribute";
            attributeParentTitleData.Location = new Point(0, 44);
            attributeParentTitleData.Margin = new Padding(0);
            attributeParentTitleData.Multiline = false;
            attributeParentTitleData.Name = "attributeParentTitleData";
            attributeParentTitleData.ReadOnly = true;
            attributeParentTitleData.Size = new Size(529, 44);
            attributeParentTitleData.TabIndex = 1;
            // 
            // attributeTabLayout
            // 
            attributeTabLayout.Controls.Add(attributePropertyTab);
            attributeTabLayout.Controls.Add(attributeDbAlaisTab);
            attributeTabLayout.Controls.Add(libraryAliasTab);
            attributeTabLayout.Dock = DockStyle.Fill;
            attributeTabLayout.Location = new Point(3, 196);
            attributeTabLayout.Name = "attributeTabLayout";
            attributeTabLayout.SelectedIndex = 0;
            attributeTabLayout.Size = new Size(523, 417);
            attributeTabLayout.TabIndex = 4;
            // 
            // attributePropertyTab
            // 
            attributePropertyTab.BackColor = SystemColors.Control;
            attributePropertyTab.Controls.Add(propertyLayout);
            attributePropertyTab.Location = new Point(4, 24);
            attributePropertyTab.Name = "attributePropertyTab";
            attributePropertyTab.Padding = new Padding(3);
            attributePropertyTab.Size = new Size(515, 389);
            attributePropertyTab.TabIndex = 1;
            attributePropertyTab.Text = "Properties";
            // 
            // propertyLayout
            // 
            propertyLayout.ColumnCount = 1;
            propertyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            propertyLayout.Controls.Add(propertyNavigation, 0, 0);
            propertyLayout.Controls.Add(propertyTabLayout, 0, 1);
            propertyLayout.Dock = DockStyle.Fill;
            propertyLayout.Location = new Point(3, 3);
            propertyLayout.Name = "propertyLayout";
            propertyLayout.RowCount = 2;
            propertyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            propertyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            propertyLayout.Size = new Size(509, 383);
            propertyLayout.TabIndex = 0;
            // 
            // propertyNavigation
            // 
            propertyNavigation.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            propertyNavigation.Columns.AddRange(new DataGridViewColumn[] { propertyTypeColumn, propertyValueColumn });
            propertyNavigation.Dock = DockStyle.Fill;
            propertyNavigation.Location = new Point(3, 3);
            propertyNavigation.Name = "propertyNavigation";
            propertyNavigation.RowTemplate.Height = 25;
            propertyNavigation.Size = new Size(503, 147);
            propertyNavigation.TabIndex = 0;
            propertyNavigation.Leave += propertyNavigation_Leave;
            // 
            // propertyTypeColumn
            // 
            propertyTypeColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            propertyTypeColumn.DataPropertyName = "PropertyId";
            propertyTypeColumn.FillWeight = 30F;
            propertyTypeColumn.HeaderText = "Property Type";
            propertyTypeColumn.Name = "propertyTypeColumn";
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
            // propertyTabLayout
            // 
            propertyTabLayout.Controls.Add(propertyValueTab);
            propertyTabLayout.Controls.Add(prropertyDefinitionTab);
            propertyTabLayout.Dock = DockStyle.Fill;
            propertyTabLayout.Location = new Point(3, 156);
            propertyTabLayout.Name = "propertyTabLayout";
            propertyTabLayout.SelectedIndex = 0;
            propertyTabLayout.Size = new Size(503, 224);
            propertyTabLayout.TabIndex = 1;
            // 
            // propertyValueTab
            // 
            propertyValueTab.BackColor = SystemColors.Control;
            propertyValueTab.Controls.Add(propertyValueLayout);
            propertyValueTab.Location = new Point(4, 24);
            propertyValueTab.Name = "propertyValueTab";
            propertyValueTab.Padding = new Padding(3);
            propertyValueTab.Size = new Size(495, 196);
            propertyValueTab.TabIndex = 0;
            propertyValueTab.Text = "Value";
            // 
            // propertyValueLayout
            // 
            propertyValueLayout.ColumnCount = 2;
            propertyValueLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            propertyValueLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            propertyValueLayout.Controls.Add(propertyTypeData, 0, 0);
            propertyValueLayout.Controls.Add(propertyValueData, 0, 1);
            propertyValueLayout.Controls.Add(propertyChoiceData, 1, 0);
            propertyValueLayout.Dock = DockStyle.Fill;
            propertyValueLayout.Location = new Point(3, 3);
            propertyValueLayout.Name = "propertyValueLayout";
            propertyValueLayout.RowCount = 2;
            propertyValueLayout.RowStyles.Add(new RowStyle());
            propertyValueLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            propertyValueLayout.Size = new Size(489, 190);
            propertyValueLayout.TabIndex = 0;
            // 
            // propertyTypeData
            // 
            propertyTypeData.AutoSize = true;
            propertyTypeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            propertyTypeData.DataSource = null;
            propertyTypeData.DisplayMember = "";
            propertyTypeData.Dock = DockStyle.Fill;
            propertyTypeData.HeaderText = "Property Type";
            propertyTypeData.Location = new Point(3, 3);
            propertyTypeData.Name = "propertyTypeData";
            propertyTypeData.ReadOnly = false;
            propertyTypeData.SelectedIndex = -1;
            propertyTypeData.SelectedItem = null;
            propertyTypeData.SelectedValue = null;
            propertyTypeData.Size = new Size(238, 44);
            propertyTypeData.TabIndex = 0;
            propertyTypeData.ValueMember = "";
            propertyTypeData.SelectedIndexChanged += propertyTypeData_SelectedIndexChanged;
            // 
            // propertyValueData
            // 
            propertyValueData.AutoSize = true;
            propertyValueData.Dock = DockStyle.Fill;
            propertyValueData.HeaderText = "Property Value";
            propertyValueData.Location = new Point(3, 53);
            propertyValueData.Multiline = true;
            propertyValueData.Name = "propertyValueData";
            propertyValueData.ReadOnly = false;
            propertyValueData.Size = new Size(238, 134);
            propertyValueData.TabIndex = 1;
            // 
            // propertyChoiceData
            // 
            propertyChoiceData.CheckOnClick = true;
            propertyChoiceData.Dock = DockStyle.Fill;
            propertyChoiceData.FormattingEnabled = true;
            propertyChoiceData.Location = new Point(247, 3);
            propertyChoiceData.Name = "propertyChoiceData";
            propertyValueLayout.SetRowSpan(propertyChoiceData, 2);
            propertyChoiceData.Size = new Size(239, 184);
            propertyChoiceData.TabIndex = 2;
            propertyChoiceData.ItemCheck += propertyChoiceData_ItemCheck;
            propertyChoiceData.EnabledChanged += propertyChoiceData_EnabledChanged;
            // 
            // prropertyDefinitionTab
            // 
            prropertyDefinitionTab.BackColor = SystemColors.Control;
            prropertyDefinitionTab.Controls.Add(propertyDefinitionData);
            prropertyDefinitionTab.Location = new Point(4, 24);
            prropertyDefinitionTab.Name = "prropertyDefinitionTab";
            prropertyDefinitionTab.Padding = new Padding(3);
            prropertyDefinitionTab.Size = new Size(192, 72);
            prropertyDefinitionTab.TabIndex = 1;
            prropertyDefinitionTab.Text = "Defintion";
            // 
            // propertyDefinitionData
            // 
            propertyDefinitionData.AutoSize = true;
            propertyDefinitionData.Dock = DockStyle.Fill;
            propertyDefinitionData.HeaderText = "Definition";
            propertyDefinitionData.Location = new Point(3, 3);
            propertyDefinitionData.Name = "propertyDefinitionData";
            propertyDefinitionData.ReadOnly = false;
            propertyDefinitionData.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil Segoe UI;}}\r\n{\\*\\generator Riched20 10.0.19041}\\viewkind4\\uc1 \r\n\\pard\\f0\\fs18\\par\r\n}\r\n";
            propertyDefinitionData.Size = new Size(186, 66);
            propertyDefinitionData.TabIndex = 0;
            // 
            // attributeDbAlaisTab
            // 
            attributeDbAlaisTab.BackColor = SystemColors.Control;
            attributeDbAlaisTab.Controls.Add(databaseAliasLayout);
            attributeDbAlaisTab.Location = new Point(4, 24);
            attributeDbAlaisTab.Name = "attributeDbAlaisTab";
            attributeDbAlaisTab.Padding = new Padding(3);
            attributeDbAlaisTab.Size = new Size(192, 72);
            attributeDbAlaisTab.TabIndex = 2;
            attributeDbAlaisTab.Text = "Db Alias";
            // 
            // databaseAliasLayout
            // 
            databaseAliasLayout.ColumnCount = 1;
            databaseAliasLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            databaseAliasLayout.Controls.Add(attributeAlaisData, 0, 0);
            databaseAliasLayout.Controls.Add(catalogNameData, 0, 1);
            databaseAliasLayout.Controls.Add(schemaNameData, 0, 2);
            databaseAliasLayout.Controls.Add(objectNameData, 0, 3);
            databaseAliasLayout.Controls.Add(elementNameData, 0, 4);
            databaseAliasLayout.Dock = DockStyle.Fill;
            databaseAliasLayout.Location = new Point(3, 3);
            databaseAliasLayout.Name = "databaseAliasLayout";
            databaseAliasLayout.RowCount = 5;
            databaseAliasLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            databaseAliasLayout.RowStyles.Add(new RowStyle());
            databaseAliasLayout.RowStyles.Add(new RowStyle());
            databaseAliasLayout.RowStyles.Add(new RowStyle());
            databaseAliasLayout.RowStyles.Add(new RowStyle());
            databaseAliasLayout.Size = new Size(186, 66);
            databaseAliasLayout.TabIndex = 1;
            // 
            // attributeAlaisData
            // 
            attributeAlaisData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            attributeAlaisData.Columns.AddRange(new DataGridViewColumn[] { aliasCatalogNameData, aliasSchemaNameData, alaisObjectNameData, aliasElementNameData });
            attributeAlaisData.Dock = DockStyle.Fill;
            attributeAlaisData.Location = new Point(3, 3);
            attributeAlaisData.Name = "attributeAlaisData";
            attributeAlaisData.RowTemplate.Height = 25;
            attributeAlaisData.Size = new Size(180, 1);
            attributeAlaisData.TabIndex = 0;
            attributeAlaisData.Leave += attributeAlaisData_Leave;
            // 
            // aliasCatalogNameData
            // 
            aliasCatalogNameData.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            aliasCatalogNameData.DataPropertyName = "CatalogName";
            aliasCatalogNameData.HeaderText = "Catalog Name";
            aliasCatalogNameData.Name = "aliasCatalogNameData";
            // 
            // aliasSchemaNameData
            // 
            aliasSchemaNameData.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            aliasSchemaNameData.DataPropertyName = "SchemaName";
            aliasSchemaNameData.HeaderText = "Schema Name";
            aliasSchemaNameData.Name = "aliasSchemaNameData";
            // 
            // alaisObjectNameData
            // 
            alaisObjectNameData.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            alaisObjectNameData.DataPropertyName = "ObjectName";
            alaisObjectNameData.HeaderText = "Object Name";
            alaisObjectNameData.Name = "alaisObjectNameData";
            // 
            // aliasElementNameData
            // 
            aliasElementNameData.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            aliasElementNameData.DataPropertyName = "ElementName";
            aliasElementNameData.HeaderText = "Element Name";
            aliasElementNameData.Name = "aliasElementNameData";
            // 
            // catalogNameData
            // 
            catalogNameData.AutoSize = true;
            catalogNameData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            catalogNameData.DataSource = null;
            catalogNameData.DisplayMember = "";
            catalogNameData.Dock = DockStyle.Fill;
            catalogNameData.HeaderText = "Catalog Name";
            catalogNameData.Location = new Point(3, -131);
            catalogNameData.Name = "catalogNameData";
            catalogNameData.ReadOnly = false;
            catalogNameData.SelectedIndex = -1;
            catalogNameData.SelectedItem = null;
            catalogNameData.SelectedValue = null;
            catalogNameData.Size = new Size(180, 44);
            catalogNameData.TabIndex = 1;
            catalogNameData.ValueMember = "";
            catalogNameData.Validated += catalogNameData_Validated;
            catalogNameData.Validating += catalogNameData_Validating;
            // 
            // schemaNameData
            // 
            schemaNameData.AutoSize = true;
            schemaNameData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            schemaNameData.DataSource = null;
            schemaNameData.DisplayMember = "";
            schemaNameData.Dock = DockStyle.Fill;
            schemaNameData.HeaderText = "Schema Name";
            schemaNameData.Location = new Point(3, -81);
            schemaNameData.Name = "schemaNameData";
            schemaNameData.ReadOnly = false;
            schemaNameData.SelectedIndex = -1;
            schemaNameData.SelectedItem = null;
            schemaNameData.SelectedValue = null;
            schemaNameData.Size = new Size(180, 44);
            schemaNameData.TabIndex = 2;
            schemaNameData.ValueMember = "";
            schemaNameData.Validated += schemaNameData_Validated;
            schemaNameData.Validating += schemaNameData_Validating;
            // 
            // objectNameData
            // 
            objectNameData.AutoSize = true;
            objectNameData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            objectNameData.DataSource = null;
            objectNameData.DisplayMember = "";
            objectNameData.Dock = DockStyle.Fill;
            objectNameData.HeaderText = "Object Name";
            objectNameData.Location = new Point(3, -31);
            objectNameData.Name = "objectNameData";
            objectNameData.ReadOnly = false;
            objectNameData.SelectedIndex = -1;
            objectNameData.SelectedItem = null;
            objectNameData.SelectedValue = null;
            objectNameData.Size = new Size(180, 44);
            objectNameData.TabIndex = 3;
            objectNameData.ValueMember = "";
            objectNameData.Validated += objectNameData_Validated;
            objectNameData.Validating += objectNameData_Validating;
            // 
            // elementNameData
            // 
            elementNameData.AutoSize = true;
            elementNameData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            elementNameData.DataSource = null;
            elementNameData.DisplayMember = "";
            elementNameData.Dock = DockStyle.Fill;
            elementNameData.HeaderText = "Element Name";
            elementNameData.Location = new Point(3, 19);
            elementNameData.Name = "elementNameData";
            elementNameData.ReadOnly = false;
            elementNameData.SelectedIndex = -1;
            elementNameData.SelectedItem = null;
            elementNameData.SelectedValue = null;
            elementNameData.Size = new Size(180, 44);
            elementNameData.TabIndex = 4;
            elementNameData.ValueMember = "";
            elementNameData.Validated += elementNameData_Validated;
            elementNameData.Validating += elementNameData_Validating;
            // 
            // libraryAliasTab
            // 
            libraryAliasTab.BackColor = SystemColors.Control;
            libraryAliasTab.Controls.Add(libraryAlaisToBeDetermined);
            libraryAliasTab.Location = new Point(4, 24);
            libraryAliasTab.Name = "libraryAliasTab";
            libraryAliasTab.Padding = new Padding(3);
            libraryAliasTab.Size = new Size(192, 72);
            libraryAliasTab.TabIndex = 3;
            libraryAliasTab.Text = "Library Alais";
            // 
            // libraryAlaisToBeDetermined
            // 
            libraryAlaisToBeDetermined.AutoSize = true;
            libraryAlaisToBeDetermined.Location = new Point(166, 75);
            libraryAlaisToBeDetermined.Name = "libraryAlaisToBeDetermined";
            libraryAlaisToBeDetermined.Size = new Size(100, 15);
            libraryAlaisToBeDetermined.TabIndex = 0;
            libraryAlaisToBeDetermined.Text = "To Be Determined";
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // bindingProperties
            // 
            bindingProperties.AddingNew += bindingProperties_AddingNew;
            bindingProperties.BindingComplete += BindingComplete;
            bindingProperties.CurrentChanged += bindingProperties_CurrentChanged;
            // 
            // bindingDatabaseAlias
            // 
            bindingDatabaseAlias.AddingNew += bindingDatabaseAlias_AddingNew;
            bindingDatabaseAlias.BindingComplete += BindingComplete;
            // 
            // DomainAttribute
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(529, 641);
            Controls.Add(attributeLayout);
            Name = "DomainAttribute";
            Text = "Domain Attribute";
            Load += DomainAttribute_Load;
            Controls.SetChildIndex(attributeLayout, 0);
            attributeLayout.ResumeLayout(false);
            attributeLayout.PerformLayout();
            attributeTabLayout.ResumeLayout(false);
            attributePropertyTab.ResumeLayout(false);
            propertyLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)propertyNavigation).EndInit();
            propertyTabLayout.ResumeLayout(false);
            propertyValueTab.ResumeLayout(false);
            propertyValueLayout.ResumeLayout(false);
            propertyValueLayout.PerformLayout();
            prropertyDefinitionTab.ResumeLayout(false);
            prropertyDefinitionTab.PerformLayout();
            attributeDbAlaisTab.ResumeLayout(false);
            databaseAliasLayout.ResumeLayout(false);
            databaseAliasLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)attributeAlaisData).EndInit();
            libraryAliasTab.ResumeLayout(false);
            libraryAliasTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingDefinition).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingDatabaseAlias).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData attributeTitleData;
        private Controls.TextBoxData attributeParentTitleData;
        private DataGridView attributeAlaisData;
        private DataGridViewTextBoxColumn aliasCatalogNameData;
        private DataGridViewTextBoxColumn aliasSchemaNameData;
        private DataGridViewTextBoxColumn alaisObjectNameData;
        private DataGridViewTextBoxColumn aliasElementNameData;
        private Controls.TextBoxData attributeDescriptionData;
        private BindingSource bindingDefinition;
        private ErrorProvider errorProvider;
        private DataGridView propertyNavigation;
        private TabControl propertyTabLayout;
        private TabPage propertyValueTab;
        private TabPage prropertyDefinitionTab;
        private Controls.ComboBoxData propertyTypeData;
        private Controls.TextBoxData propertyValueData;
        private CheckedListBox propertyChoiceData;
        private Controls.RichTextBoxData propertyDefinitionData;
        private TableLayoutPanel databaseAliasLayout;
        private Controls.ComboBoxData catalogNameData;
        private Controls.ComboBoxData schemaNameData;
        private Controls.ComboBoxData objectNameData;
        private Controls.ComboBoxData elementNameData;
        private Label libraryAlaisToBeDetermined;
        private BindingSource bindingProperties;
        private BindingSource bindingDatabaseAlias;
        private DataGridViewComboBoxColumn propertyTypeColumn;
        private DataGridViewTextBoxColumn propertyValueColumn;
    }
}