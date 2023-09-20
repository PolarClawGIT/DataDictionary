namespace DataDictionary.Main.Forms.Domain
{
    partial class DomainEntity
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
            TableLayoutPanel entityLayout;
            TabControl entityTabLayout;
            TableLayoutPanel propertyLayout;
            TabControl propertyTabLayout;
            TableLayoutPanel propertyValueLayout;
            TableLayoutPanel databaseAliasLayout;
            entityTitleData = new Controls.TextBoxData();
            entityParentData = new Controls.TextBoxData();
            entityDescriptionData = new Controls.TextBoxData();
            entityPropertyTab = new TabPage();
            propertyNavigation = new DataGridView();
            propertyTypeColumn = new DataGridViewComboBoxColumn();
            propertyValueColumn = new DataGridViewTextBoxColumn();
            propertyValueTab = new TabPage();
            propertyTypeData = new Controls.ComboBoxData();
            propertyValueData = new Controls.TextBoxData();
            propertyChoiceData = new Controls.CheckedListBoxData();
            propertyDefinitionTab = new TabPage();
            propertyDefinitionData = new Controls.RichTextBoxData();
            entityDbAliasTab = new TabPage();
            aliasNavigation = new DataGridView();
            aliasCatalogNameData = new DataGridViewTextBoxColumn();
            aliasSchemaNameData = new DataGridViewTextBoxColumn();
            alaisObjectNameData = new DataGridViewTextBoxColumn();
            aliasElementNameData = new DataGridViewTextBoxColumn();
            catalogNameData = new Controls.ComboBoxData();
            schemaNameData = new Controls.ComboBoxData();
            objectNameData = new Controls.ComboBoxData();
            entityLibraryAlias = new TabPage();
            libraryAlaisToBeDetermined = new Label();
            attributeTab = new TabPage();
            attributeData = new DataGridView();
            attributeTitleColumn = new DataGridViewTextBoxColumn();
            attributeDescriptionTitle = new DataGridViewTextBoxColumn();
            errorProvider = new ErrorProvider(components);
            bindingProperties = new BindingSource(components);
            bindingDatabaseAlias = new BindingSource(components);
            entityLayout = new TableLayoutPanel();
            entityTabLayout = new TabControl();
            propertyLayout = new TableLayoutPanel();
            propertyTabLayout = new TabControl();
            propertyValueLayout = new TableLayoutPanel();
            databaseAliasLayout = new TableLayoutPanel();
            entityLayout.SuspendLayout();
            entityTabLayout.SuspendLayout();
            entityPropertyTab.SuspendLayout();
            propertyLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)propertyNavigation).BeginInit();
            propertyTabLayout.SuspendLayout();
            propertyValueTab.SuspendLayout();
            propertyValueLayout.SuspendLayout();
            propertyDefinitionTab.SuspendLayout();
            entityDbAliasTab.SuspendLayout();
            databaseAliasLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)aliasNavigation).BeginInit();
            entityLibraryAlias.SuspendLayout();
            attributeTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)attributeData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingProperties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingDatabaseAlias).BeginInit();
            SuspendLayout();
            // 
            // entityLayout
            // 
            entityLayout.ColumnCount = 1;
            entityLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            entityLayout.Controls.Add(entityTitleData, 0, 0);
            entityLayout.Controls.Add(entityParentData, 0, 1);
            entityLayout.Controls.Add(entityDescriptionData, 0, 2);
            entityLayout.Controls.Add(entityTabLayout, 0, 3);
            entityLayout.Dock = DockStyle.Fill;
            entityLayout.Location = new Point(0, 25);
            entityLayout.Name = "entityLayout";
            entityLayout.RowCount = 4;
            entityLayout.RowStyles.Add(new RowStyle());
            entityLayout.RowStyles.Add(new RowStyle());
            entityLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            entityLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            entityLayout.Size = new Size(524, 577);
            entityLayout.TabIndex = 1;
            // 
            // entityTitleData
            // 
            entityTitleData.AutoSize = true;
            entityTitleData.Dock = DockStyle.Fill;
            entityTitleData.HeaderText = "Entity Title";
            entityTitleData.Location = new Point(3, 3);
            entityTitleData.Multiline = false;
            entityTitleData.Name = "entityTitleData";
            entityTitleData.ReadOnly = false;
            entityTitleData.Size = new Size(518, 44);
            entityTitleData.TabIndex = 0;
            entityTitleData.Validating += entityTitleData_Validating;
            // 
            // entityParentData
            // 
            entityParentData.AutoSize = true;
            entityParentData.Dock = DockStyle.Fill;
            entityParentData.HeaderText = "Entity Parent";
            entityParentData.Location = new Point(3, 53);
            entityParentData.Multiline = false;
            entityParentData.Name = "entityParentData";
            entityParentData.ReadOnly = true;
            entityParentData.Size = new Size(518, 44);
            entityParentData.TabIndex = 1;
            // 
            // entityDescriptionData
            // 
            entityDescriptionData.AutoSize = true;
            entityDescriptionData.Dock = DockStyle.Fill;
            entityDescriptionData.HeaderText = "Description (Summary)";
            entityDescriptionData.Location = new Point(3, 103);
            entityDescriptionData.Multiline = true;
            entityDescriptionData.Name = "entityDescriptionData";
            entityDescriptionData.ReadOnly = false;
            entityDescriptionData.Size = new Size(518, 89);
            entityDescriptionData.TabIndex = 2;
            // 
            // entityTabLayout
            // 
            entityTabLayout.Controls.Add(entityPropertyTab);
            entityTabLayout.Controls.Add(entityDbAliasTab);
            entityTabLayout.Controls.Add(entityLibraryAlias);
            entityTabLayout.Controls.Add(attributeTab);
            entityTabLayout.Dock = DockStyle.Fill;
            entityTabLayout.Location = new Point(3, 198);
            entityTabLayout.Name = "entityTabLayout";
            entityTabLayout.SelectedIndex = 0;
            entityTabLayout.Size = new Size(518, 376);
            entityTabLayout.TabIndex = 3;
            // 
            // entityPropertyTab
            // 
            entityPropertyTab.Controls.Add(propertyLayout);
            entityPropertyTab.Location = new Point(4, 24);
            entityPropertyTab.Name = "entityPropertyTab";
            entityPropertyTab.Padding = new Padding(3);
            entityPropertyTab.Size = new Size(510, 348);
            entityPropertyTab.TabIndex = 0;
            entityPropertyTab.Text = "Properties";
            entityPropertyTab.UseVisualStyleBackColor = true;
            // 
            // propertyLayout
            // 
            propertyLayout.BackColor = SystemColors.Control;
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
            propertyLayout.Size = new Size(504, 342);
            propertyLayout.TabIndex = 1;
            // 
            // propertyNavigation
            // 
            propertyNavigation.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            propertyNavigation.Columns.AddRange(new DataGridViewColumn[] { propertyTypeColumn, propertyValueColumn });
            propertyNavigation.Dock = DockStyle.Fill;
            propertyNavigation.Location = new Point(3, 3);
            propertyNavigation.Name = "propertyNavigation";
            propertyNavigation.RowTemplate.Height = 25;
            propertyNavigation.Size = new Size(498, 130);
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
            propertyTabLayout.Controls.Add(propertyDefinitionTab);
            propertyTabLayout.Dock = DockStyle.Fill;
            propertyTabLayout.Location = new Point(3, 139);
            propertyTabLayout.Name = "propertyTabLayout";
            propertyTabLayout.SelectedIndex = 0;
            propertyTabLayout.Size = new Size(498, 200);
            propertyTabLayout.TabIndex = 1;
            // 
            // propertyValueTab
            // 
            propertyValueTab.BackColor = SystemColors.Control;
            propertyValueTab.Controls.Add(propertyValueLayout);
            propertyValueTab.Location = new Point(4, 24);
            propertyValueTab.Name = "propertyValueTab";
            propertyValueTab.Padding = new Padding(3);
            propertyValueTab.Size = new Size(490, 172);
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
            propertyValueLayout.Size = new Size(484, 166);
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
            propertyTypeData.Size = new Size(236, 44);
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
            propertyValueData.Size = new Size(236, 110);
            propertyValueData.TabIndex = 1;
            // 
            // propertyChoiceData
            // 
            propertyChoiceData.AutoSize = true;
            propertyChoiceData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            propertyChoiceData.CheckOnClick = true;
            propertyChoiceData.DataSource = null;
            propertyChoiceData.DisplayMember = "";
            propertyChoiceData.Dock = DockStyle.Fill;
            propertyChoiceData.HeaderText = "Property Choice";
            propertyChoiceData.Location = new Point(245, 3);
            propertyChoiceData.Name = "propertyChoiceData";
            propertyValueLayout.SetRowSpan(propertyChoiceData, 2);
            propertyChoiceData.Size = new Size(236, 160);
            propertyChoiceData.TabIndex = 2;
            propertyChoiceData.ItemCheck += propertyChoiceData_ItemCheck;
            // 
            // propertyDefinitionTab
            // 
            propertyDefinitionTab.BackColor = SystemColors.Control;
            propertyDefinitionTab.Controls.Add(propertyDefinitionData);
            propertyDefinitionTab.Location = new Point(4, 24);
            propertyDefinitionTab.Name = "propertyDefinitionTab";
            propertyDefinitionTab.Padding = new Padding(3);
            propertyDefinitionTab.Size = new Size(192, 72);
            propertyDefinitionTab.TabIndex = 1;
            propertyDefinitionTab.Text = "Defintion";
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
            propertyDefinitionData.Validated += propertyDefinitionData_Validated;
            // 
            // entityDbAliasTab
            // 
            entityDbAliasTab.Controls.Add(databaseAliasLayout);
            entityDbAliasTab.Location = new Point(4, 24);
            entityDbAliasTab.Name = "entityDbAliasTab";
            entityDbAliasTab.Padding = new Padding(3);
            entityDbAliasTab.Size = new Size(192, 72);
            entityDbAliasTab.TabIndex = 1;
            entityDbAliasTab.Text = "Db Alias";
            entityDbAliasTab.UseVisualStyleBackColor = true;
            // 
            // databaseAliasLayout
            // 
            databaseAliasLayout.BackColor = SystemColors.Control;
            databaseAliasLayout.ColumnCount = 1;
            databaseAliasLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            databaseAliasLayout.Controls.Add(aliasNavigation, 0, 0);
            databaseAliasLayout.Controls.Add(catalogNameData, 0, 1);
            databaseAliasLayout.Controls.Add(schemaNameData, 0, 2);
            databaseAliasLayout.Controls.Add(objectNameData, 0, 3);
            databaseAliasLayout.Dock = DockStyle.Fill;
            databaseAliasLayout.Location = new Point(3, 3);
            databaseAliasLayout.Name = "databaseAliasLayout";
            databaseAliasLayout.RowCount = 4;
            databaseAliasLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            databaseAliasLayout.RowStyles.Add(new RowStyle());
            databaseAliasLayout.RowStyles.Add(new RowStyle());
            databaseAliasLayout.RowStyles.Add(new RowStyle());
            databaseAliasLayout.Size = new Size(186, 66);
            databaseAliasLayout.TabIndex = 2;
            // 
            // aliasNavigation
            // 
            aliasNavigation.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            aliasNavigation.Columns.AddRange(new DataGridViewColumn[] { aliasCatalogNameData, aliasSchemaNameData, alaisObjectNameData, aliasElementNameData });
            aliasNavigation.Dock = DockStyle.Fill;
            aliasNavigation.Location = new Point(3, 3);
            aliasNavigation.Name = "aliasNavigation";
            aliasNavigation.RowTemplate.Height = 25;
            aliasNavigation.Size = new Size(180, 1);
            aliasNavigation.TabIndex = 0;
            aliasNavigation.Leave += aliasNavigation_Leave;
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
            catalogNameData.Location = new Point(3, -81);
            catalogNameData.Name = "catalogNameData";
            catalogNameData.ReadOnly = false;
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
            schemaNameData.Location = new Point(3, -31);
            schemaNameData.Name = "schemaNameData";
            schemaNameData.ReadOnly = false;
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
            objectNameData.Location = new Point(3, 19);
            objectNameData.Name = "objectNameData";
            objectNameData.ReadOnly = false;
            objectNameData.Size = new Size(180, 44);
            objectNameData.TabIndex = 3;
            objectNameData.ValueMember = "";
            objectNameData.Validated += objectNameData_Validated;
            objectNameData.Validating += objectNameData_Validating;
            // 
            // entityLibraryAlias
            // 
            entityLibraryAlias.BackColor = SystemColors.Control;
            entityLibraryAlias.Controls.Add(libraryAlaisToBeDetermined);
            entityLibraryAlias.Location = new Point(4, 24);
            entityLibraryAlias.Name = "entityLibraryAlias";
            entityLibraryAlias.Size = new Size(192, 72);
            entityLibraryAlias.TabIndex = 2;
            entityLibraryAlias.Text = "Library Alias";
            // 
            // libraryAlaisToBeDetermined
            // 
            libraryAlaisToBeDetermined.AutoSize = true;
            libraryAlaisToBeDetermined.Location = new Point(205, 179);
            libraryAlaisToBeDetermined.Name = "libraryAlaisToBeDetermined";
            libraryAlaisToBeDetermined.Size = new Size(100, 15);
            libraryAlaisToBeDetermined.TabIndex = 1;
            libraryAlaisToBeDetermined.Text = "To Be Determined";
            // 
            // attributeTab
            // 
            attributeTab.Controls.Add(attributeData);
            attributeTab.Location = new Point(4, 24);
            attributeTab.Name = "attributeTab";
            attributeTab.Size = new Size(192, 72);
            attributeTab.TabIndex = 3;
            attributeTab.Text = "Attributes";
            attributeTab.UseVisualStyleBackColor = true;
            // 
            // attributeData
            // 
            attributeData.AllowUserToAddRows = false;
            attributeData.AllowUserToDeleteRows = false;
            attributeData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            attributeData.Columns.AddRange(new DataGridViewColumn[] { attributeTitleColumn, attributeDescriptionTitle });
            attributeData.Dock = DockStyle.Fill;
            attributeData.Location = new Point(0, 0);
            attributeData.Name = "attributeData";
            attributeData.ReadOnly = true;
            attributeData.RowTemplate.Height = 25;
            attributeData.Size = new Size(192, 72);
            attributeData.TabIndex = 0;
            // 
            // attributeTitleColumn
            // 
            attributeTitleColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            attributeTitleColumn.DataPropertyName = "AttributeTitle";
            attributeTitleColumn.FillWeight = 30F;
            attributeTitleColumn.HeaderText = "Attribute Title";
            attributeTitleColumn.Name = "attributeTitleColumn";
            attributeTitleColumn.ReadOnly = true;
            // 
            // attributeDescriptionTitle
            // 
            attributeDescriptionTitle.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            attributeDescriptionTitle.DataPropertyName = "AttributeDescription";
            attributeDescriptionTitle.FillWeight = 70F;
            attributeDescriptionTitle.HeaderText = "Attribute Description";
            attributeDescriptionTitle.Name = "attributeDescriptionTitle";
            attributeDescriptionTitle.ReadOnly = true;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // bindingProperties
            // 
            bindingProperties.AddingNew += bindingProperties_AddingNew;
            bindingProperties.BindingComplete += BindingComplete;
            // 
            // bindingDatabaseAlias
            // 
            bindingDatabaseAlias.AddingNew += bindingDatabaseAlias_AddingNew;
            bindingDatabaseAlias.BindingComplete += BindingComplete;
            // 
            // DomainEntity
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(524, 602);
            Controls.Add(entityLayout);
            Name = "DomainEntity";
            Text = "DomainEntity";
            Load += DomainEntity_Load;
            Controls.SetChildIndex(entityLayout, 0);
            entityLayout.ResumeLayout(false);
            entityLayout.PerformLayout();
            entityTabLayout.ResumeLayout(false);
            entityPropertyTab.ResumeLayout(false);
            propertyLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)propertyNavigation).EndInit();
            propertyTabLayout.ResumeLayout(false);
            propertyValueTab.ResumeLayout(false);
            propertyValueLayout.ResumeLayout(false);
            propertyValueLayout.PerformLayout();
            propertyDefinitionTab.ResumeLayout(false);
            propertyDefinitionTab.PerformLayout();
            entityDbAliasTab.ResumeLayout(false);
            databaseAliasLayout.ResumeLayout(false);
            databaseAliasLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)aliasNavigation).EndInit();
            entityLibraryAlias.ResumeLayout(false);
            entityLibraryAlias.PerformLayout();
            attributeTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)attributeData).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingDatabaseAlias).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData entityTitleData;
        private Controls.TextBoxData entityParentData;
        private Controls.TextBoxData entityDescriptionData;
        private TabPage entityPropertyTab;
        private TabPage entityDbAliasTab;
        private TabPage entityLibraryAlias;
        private TabPage attributeTab;
        private DataGridView propertyNavigation;
        private DataGridViewComboBoxColumn propertyTypeColumn;
        private DataGridViewTextBoxColumn propertyValueColumn;
        private TabPage propertyValueTab;
        private Controls.ComboBoxData propertyTypeData;
        private Controls.TextBoxData propertyValueData;
        private TabPage propertyDefinitionTab;
        private Controls.RichTextBoxData propertyDefinitionData;
        private DataGridView aliasNavigation;
        private DataGridViewTextBoxColumn aliasCatalogNameData;
        private DataGridViewTextBoxColumn aliasSchemaNameData;
        private DataGridViewTextBoxColumn alaisObjectNameData;
        private DataGridViewTextBoxColumn aliasElementNameData;
        private Controls.ComboBoxData catalogNameData;
        private Controls.ComboBoxData schemaNameData;
        private Controls.ComboBoxData objectNameData;
        private Label libraryAlaisToBeDetermined;
        private ErrorProvider errorProvider;
        private BindingSource bindingProperties;
        private BindingSource bindingDatabaseAlias;
        private DataGridView attributeData;
        private DataGridViewTextBoxColumn attributeTitleColumn;
        private DataGridViewTextBoxColumn attributeDescriptionTitle;
        private Controls.CheckedListBoxData propertyChoiceData;
    }
}