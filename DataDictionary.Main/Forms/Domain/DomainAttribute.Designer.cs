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
            TabPage attributePropertyTab;
            TableLayoutPanel propertyLayout;
            TabControl propertyTabLayout;
            TableLayoutPanel propertyValueLayout;
            TabPage aliasTab;
            TableLayoutPanel alaisLayout;
            attributeDescriptionData = new Controls.TextBoxData();
            attributeTitleData = new Controls.TextBoxData();
            attributeTabLayout = new TabControl();
            propertyNavigation = new DataGridView();
            propertyTypeColumn = new DataGridViewComboBoxColumn();
            propertyValueColumn = new DataGridViewTextBoxColumn();
            propertyValueTab = new TabPage();
            propertyTypeData = new Controls.ComboBoxData();
            propertyValueData = new Controls.TextBoxData();
            propertyChoiceData = new Controls.CheckedListBoxData();
            propertyDefinitionTab = new TabPage();
            propertyDefinitionData = new Controls.RichTextBoxData();
            entityAliasData = new DataGridView();
            sourceNameColumn = new DataGridViewTextBoxColumn();
            scopeNameColumn = new DataGridViewTextBoxColumn();
            aliasNameColumn = new DataGridViewTextBoxColumn();
            modelAliasNavigation = new Controls.ModelAliasNavigation();
            subjectAreaData = new Controls.ComboBoxData();
            errorProvider = new ErrorProvider(components);
            bindingProperties = new BindingSource(components);
            bindingAlias = new BindingSource(components);
            attributeLayout = new TableLayoutPanel();
            attributePropertyTab = new TabPage();
            propertyLayout = new TableLayoutPanel();
            propertyTabLayout = new TabControl();
            propertyValueLayout = new TableLayoutPanel();
            aliasTab = new TabPage();
            alaisLayout = new TableLayoutPanel();
            attributeLayout.SuspendLayout();
            attributeTabLayout.SuspendLayout();
            attributePropertyTab.SuspendLayout();
            propertyLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)propertyNavigation).BeginInit();
            propertyTabLayout.SuspendLayout();
            propertyValueTab.SuspendLayout();
            propertyValueLayout.SuspendLayout();
            propertyDefinitionTab.SuspendLayout();
            aliasTab.SuspendLayout();
            alaisLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)entityAliasData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingProperties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingAlias).BeginInit();
            SuspendLayout();
            // 
            // attributeLayout
            // 
            attributeLayout.AutoSize = true;
            attributeLayout.ColumnCount = 1;
            attributeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            attributeLayout.Controls.Add(attributeDescriptionData, 0, 2);
            attributeLayout.Controls.Add(attributeTitleData, 0, 0);
            attributeLayout.Controls.Add(attributeTabLayout, 0, 3);
            attributeLayout.Controls.Add(subjectAreaData, 0, 1);
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
            attributeDescriptionData.Location = new Point(3, 97);
            attributeDescriptionData.Multiline = true;
            attributeDescriptionData.Name = "attributeDescriptionData";
            attributeDescriptionData.ReadOnly = false;
            attributeDescriptionData.Size = new Size(523, 98);
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
            attributeTitleData.Validating += attributeTitleData_Validating;
            // 
            // attributeTabLayout
            // 
            attributeTabLayout.Controls.Add(attributePropertyTab);
            attributeTabLayout.Controls.Add(aliasTab);
            attributeTabLayout.Dock = DockStyle.Fill;
            attributeTabLayout.Location = new Point(3, 201);
            attributeTabLayout.Name = "attributeTabLayout";
            attributeTabLayout.SelectedIndex = 0;
            attributeTabLayout.Size = new Size(523, 412);
            attributeTabLayout.TabIndex = 4;
            attributeTabLayout.SelectedIndexChanged += attributeTabLayout_SelectedIndexChanged;
            // 
            // attributePropertyTab
            // 
            attributePropertyTab.BackColor = SystemColors.Control;
            attributePropertyTab.Controls.Add(propertyLayout);
            attributePropertyTab.Location = new Point(4, 24);
            attributePropertyTab.Name = "attributePropertyTab";
            attributePropertyTab.Padding = new Padding(3);
            attributePropertyTab.Size = new Size(515, 384);
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
            propertyLayout.Size = new Size(509, 378);
            propertyLayout.TabIndex = 0;
            // 
            // propertyNavigation
            // 
            propertyNavigation.AllowUserToAddRows = false;
            propertyNavigation.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            propertyNavigation.Columns.AddRange(new DataGridViewColumn[] { propertyTypeColumn, propertyValueColumn });
            propertyNavigation.Dock = DockStyle.Fill;
            propertyNavigation.Location = new Point(3, 3);
            propertyNavigation.Name = "propertyNavigation";
            propertyNavigation.RowTemplate.Height = 25;
            propertyNavigation.Size = new Size(503, 145);
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
            propertyTabLayout.Location = new Point(3, 154);
            propertyTabLayout.Name = "propertyTabLayout";
            propertyTabLayout.SelectedIndex = 0;
            propertyTabLayout.Size = new Size(503, 221);
            propertyTabLayout.TabIndex = 1;
            // 
            // propertyValueTab
            // 
            propertyValueTab.BackColor = SystemColors.Control;
            propertyValueTab.Controls.Add(propertyValueLayout);
            propertyValueTab.Location = new Point(4, 24);
            propertyValueTab.Name = "propertyValueTab";
            propertyValueTab.Padding = new Padding(3);
            propertyValueTab.Size = new Size(495, 193);
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
            propertyValueLayout.Size = new Size(489, 187);
            propertyValueLayout.TabIndex = 0;
            // 
            // propertyTypeData
            // 
            propertyTypeData.AutoSize = true;
            propertyTypeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            propertyTypeData.Dock = DockStyle.Fill;
            propertyTypeData.DropDownStyle = ComboBoxStyle.DropDown;
            propertyTypeData.HeaderText = "Property Type";
            propertyTypeData.Location = new Point(3, 3);
            propertyTypeData.Name = "propertyTypeData";
            propertyTypeData.ReadOnly = false;
            propertyTypeData.Size = new Size(238, 44);
            propertyTypeData.TabIndex = 0;
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
            propertyValueData.Size = new Size(238, 131);
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
            propertyChoiceData.Location = new Point(247, 3);
            propertyChoiceData.Name = "propertyChoiceData";
            propertyValueLayout.SetRowSpan(propertyChoiceData, 2);
            propertyChoiceData.Size = new Size(239, 181);
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
            // aliasTab
            // 
            aliasTab.BackColor = SystemColors.Control;
            aliasTab.Controls.Add(alaisLayout);
            aliasTab.Location = new Point(4, 24);
            aliasTab.Name = "aliasTab";
            aliasTab.Padding = new Padding(3);
            aliasTab.Size = new Size(192, 72);
            aliasTab.TabIndex = 3;
            aliasTab.Text = "Alias";
            // 
            // alaisLayout
            // 
            alaisLayout.ColumnCount = 1;
            alaisLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            alaisLayout.Controls.Add(entityAliasData, 0, 0);
            alaisLayout.Controls.Add(modelAliasNavigation, 0, 1);
            alaisLayout.Dock = DockStyle.Fill;
            alaisLayout.Location = new Point(3, 3);
            alaisLayout.Name = "alaisLayout";
            alaisLayout.RowCount = 2;
            alaisLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            alaisLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            alaisLayout.Size = new Size(186, 66);
            alaisLayout.TabIndex = 0;
            // 
            // entityAliasData
            // 
            entityAliasData.AllowUserToAddRows = false;
            entityAliasData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            entityAliasData.Columns.AddRange(new DataGridViewColumn[] { sourceNameColumn, scopeNameColumn, aliasNameColumn });
            entityAliasData.Dock = DockStyle.Fill;
            entityAliasData.Location = new Point(3, 3);
            entityAliasData.MultiSelect = false;
            entityAliasData.Name = "entityAliasData";
            entityAliasData.ReadOnly = true;
            entityAliasData.RowTemplate.Height = 25;
            entityAliasData.Size = new Size(180, 13);
            entityAliasData.TabIndex = 1;
            // 
            // sourceNameColumn
            // 
            sourceNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            sourceNameColumn.DataPropertyName = "SourceName";
            sourceNameColumn.HeaderText = "Source";
            sourceNameColumn.MinimumWidth = 100;
            sourceNameColumn.Name = "sourceNameColumn";
            sourceNameColumn.ReadOnly = true;
            // 
            // scopeNameColumn
            // 
            scopeNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            scopeNameColumn.DataPropertyName = "ScopeName";
            scopeNameColumn.HeaderText = "Scope";
            scopeNameColumn.MinimumWidth = 100;
            scopeNameColumn.Name = "scopeNameColumn";
            scopeNameColumn.ReadOnly = true;
            // 
            // aliasNameColumn
            // 
            aliasNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            aliasNameColumn.DataPropertyName = "AliasName";
            aliasNameColumn.HeaderText = "Alias";
            aliasNameColumn.MinimumWidth = 250;
            aliasNameColumn.Name = "aliasNameColumn";
            aliasNameColumn.ReadOnly = true;
            // 
            // modelAliasNavigation
            // 
            modelAliasNavigation.Dock = DockStyle.Fill;
            modelAliasNavigation.Location = new Point(3, 22);
            modelAliasNavigation.Name = "modelAliasNavigation";
            modelAliasNavigation.Size = new Size(180, 41);
            modelAliasNavigation.TabIndex = 2;
            // 
            // subjectAreaData
            // 
            subjectAreaData.AutoSize = true;
            subjectAreaData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            subjectAreaData.Dock = DockStyle.Fill;
            subjectAreaData.DropDownStyle = ComboBoxStyle.DropDown;
            subjectAreaData.HeaderText = "Subject Area";
            subjectAreaData.Location = new Point(3, 47);
            subjectAreaData.Name = "subjectAreaData";
            subjectAreaData.ReadOnly = false;
            subjectAreaData.Size = new Size(523, 44);
            subjectAreaData.TabIndex = 5;
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
            // bindingAlias
            // 
            bindingAlias.AddingNew += bindingAlias_AddingNew;
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
            propertyDefinitionTab.ResumeLayout(false);
            propertyDefinitionTab.PerformLayout();
            aliasTab.ResumeLayout(false);
            alaisLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)entityAliasData).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingAlias).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData attributeTitleData;
        private Controls.TextBoxData attributeDescriptionData;
        private ErrorProvider errorProvider;
        private DataGridView propertyNavigation;
        private TabPage propertyValueTab;
        private TabPage propertyDefinitionTab;
        private Controls.ComboBoxData propertyTypeData;
        private Controls.TextBoxData propertyValueData;
        private Controls.RichTextBoxData propertyDefinitionData;
        private BindingSource bindingProperties;
        private DataGridViewComboBoxColumn propertyTypeColumn;
        private DataGridViewTextBoxColumn propertyValueColumn;
        private Controls.CheckedListBoxData propertyChoiceData;
        private Controls.ComboBoxData subjectAreaData;
        private DataGridView entityAliasData;
        private DataGridViewTextBoxColumn sourceNameColumn;
        private DataGridViewTextBoxColumn scopeNameColumn;
        private DataGridViewTextBoxColumn aliasNameColumn;
        private Controls.ModelAliasNavigation modelAliasNavigation;
        private BindingSource bindingAlias;
        private TabControl attributeTabLayout;
    }
}