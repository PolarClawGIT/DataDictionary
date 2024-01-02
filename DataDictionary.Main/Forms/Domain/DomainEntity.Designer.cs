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
            TableLayoutPanel propertyLayout;
            TabControl propertyTabLayout;
            TableLayoutPanel propertyValueLayout;
            TableLayoutPanel alaisLayout;
            entityTitleData = new Controls.TextBoxData();
            entityDescriptionData = new Controls.TextBoxData();
            subjectAreaData = new Controls.ComboBoxData();
            entityTabLayout = new TabControl();
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
            entityAlias = new TabPage();
            aliasData = new DataGridView();
            scopeNameColumn = new DataGridViewTextBoxColumn();
            aliasNameColumn = new DataGridViewTextBoxColumn();
            modelAliasNavigation = new Controls.ModelAliasNavigation();
            errorProvider = new ErrorProvider(components);
            bindingProperties = new BindingSource(components);
            bindingAlias = new BindingSource(components);
            bindingEntity = new BindingSource(components);
            entityLayout = new TableLayoutPanel();
            propertyLayout = new TableLayoutPanel();
            propertyTabLayout = new TabControl();
            propertyValueLayout = new TableLayoutPanel();
            alaisLayout = new TableLayoutPanel();
            entityLayout.SuspendLayout();
            entityTabLayout.SuspendLayout();
            entityPropertyTab.SuspendLayout();
            propertyLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)propertyNavigation).BeginInit();
            propertyTabLayout.SuspendLayout();
            propertyValueTab.SuspendLayout();
            propertyValueLayout.SuspendLayout();
            propertyDefinitionTab.SuspendLayout();
            entityAlias.SuspendLayout();
            alaisLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)aliasData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingProperties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingAlias).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingEntity).BeginInit();
            SuspendLayout();
            // 
            // entityLayout
            // 
            entityLayout.ColumnCount = 1;
            entityLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            entityLayout.Controls.Add(entityTitleData, 0, 0);
            entityLayout.Controls.Add(entityDescriptionData, 0, 2);
            entityLayout.Controls.Add(subjectAreaData, 0, 1);
            entityLayout.Controls.Add(entityTabLayout, 0, 3);
            entityLayout.Dock = DockStyle.Fill;
            entityLayout.Location = new Point(0, 25);
            entityLayout.Name = "entityLayout";
            entityLayout.RowCount = 4;
            entityLayout.RowStyles.Add(new RowStyle());
            entityLayout.RowStyles.Add(new RowStyle());
            entityLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            entityLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            entityLayout.Size = new Size(524, 610);
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
            entityTitleData.Validating += EntityTitleData_Validating;
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
            entityDescriptionData.Size = new Size(518, 96);
            entityDescriptionData.TabIndex = 2;
            // 
            // subjectAreaData
            // 
            subjectAreaData.AutoSize = true;
            subjectAreaData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            subjectAreaData.Dock = DockStyle.Fill;
            subjectAreaData.DropDownStyle = ComboBoxStyle.DropDown;
            subjectAreaData.HeaderText = "Subject Area";
            subjectAreaData.Location = new Point(3, 53);
            subjectAreaData.Name = "subjectAreaData";
            subjectAreaData.ReadOnly = false;
            subjectAreaData.Size = new Size(518, 44);
            subjectAreaData.TabIndex = 4;
            // 
            // entityTabLayout
            // 
            entityTabLayout.Controls.Add(entityPropertyTab);
            entityTabLayout.Controls.Add(entityAlias);
            entityTabLayout.Dock = DockStyle.Fill;
            entityTabLayout.Location = new Point(3, 205);
            entityTabLayout.Name = "entityTabLayout";
            entityTabLayout.SelectedIndex = 0;
            entityTabLayout.Size = new Size(518, 402);
            entityTabLayout.TabIndex = 3;
            entityTabLayout.SelectedIndexChanged += EntityTabLayout_SelectedIndexChanged;
            // 
            // entityPropertyTab
            // 
            entityPropertyTab.Controls.Add(propertyLayout);
            entityPropertyTab.Location = new Point(4, 24);
            entityPropertyTab.Name = "entityPropertyTab";
            entityPropertyTab.Padding = new Padding(3);
            entityPropertyTab.Size = new Size(510, 374);
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
            propertyLayout.Size = new Size(504, 368);
            propertyLayout.TabIndex = 1;
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
            propertyNavigation.Size = new Size(498, 141);
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
            propertyTabLayout.Location = new Point(3, 150);
            propertyTabLayout.Name = "propertyTabLayout";
            propertyTabLayout.SelectedIndex = 0;
            propertyTabLayout.Size = new Size(498, 215);
            propertyTabLayout.TabIndex = 1;
            // 
            // propertyValueTab
            // 
            propertyValueTab.BackColor = SystemColors.Control;
            propertyValueTab.Controls.Add(propertyValueLayout);
            propertyValueTab.Location = new Point(4, 24);
            propertyValueTab.Name = "propertyValueTab";
            propertyValueTab.Padding = new Padding(3);
            propertyValueTab.Size = new Size(490, 187);
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
            propertyValueLayout.Size = new Size(484, 181);
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
            propertyTypeData.Size = new Size(236, 44);
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
            propertyValueData.Size = new Size(236, 125);
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
            propertyChoiceData.Size = new Size(236, 175);
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
            // entityAlias
            // 
            entityAlias.BackColor = SystemColors.Control;
            entityAlias.Controls.Add(alaisLayout);
            entityAlias.Location = new Point(4, 24);
            entityAlias.Name = "entityAlias";
            entityAlias.Size = new Size(192, 72);
            entityAlias.TabIndex = 2;
            entityAlias.Text = "Alias";
            // 
            // alaisLayout
            // 
            alaisLayout.ColumnCount = 1;
            alaisLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            alaisLayout.Controls.Add(aliasData, 0, 0);
            alaisLayout.Controls.Add(modelAliasNavigation, 0, 1);
            alaisLayout.Dock = DockStyle.Fill;
            alaisLayout.Location = new Point(0, 0);
            alaisLayout.Name = "alaisLayout";
            alaisLayout.Padding = new Padding(3);
            alaisLayout.RowCount = 2;
            alaisLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            alaisLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            alaisLayout.Size = new Size(192, 72);
            alaisLayout.TabIndex = 0;
            // 
            // aliasData
            // 
            aliasData.AllowUserToAddRows = false;
            aliasData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            aliasData.Columns.AddRange(new DataGridViewColumn[] { scopeNameColumn, aliasNameColumn });
            aliasData.Dock = DockStyle.Fill;
            aliasData.Location = new Point(6, 6);
            aliasData.MultiSelect = false;
            aliasData.Name = "aliasData";
            aliasData.ReadOnly = true;
            aliasData.RowTemplate.Height = 25;
            aliasData.Size = new Size(180, 13);
            aliasData.TabIndex = 0;
            // 
            // scopeNameColumn
            // 
            scopeNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            scopeNameColumn.DataPropertyName = "ScopeName";
            scopeNameColumn.FillWeight = 30F;
            scopeNameColumn.HeaderText = "Scope";
            scopeNameColumn.MinimumWidth = 100;
            scopeNameColumn.Name = "scopeNameColumn";
            scopeNameColumn.ReadOnly = true;
            // 
            // aliasNameColumn
            // 
            aliasNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            aliasNameColumn.DataPropertyName = "AliasName";
            aliasNameColumn.FillWeight = 70F;
            aliasNameColumn.HeaderText = "Alias";
            aliasNameColumn.MinimumWidth = 250;
            aliasNameColumn.Name = "aliasNameColumn";
            aliasNameColumn.ReadOnly = true;
            // 
            // modelAliasNavigation
            // 
            modelAliasNavigation.Dock = DockStyle.Fill;
            modelAliasNavigation.Location = new Point(6, 25);
            modelAliasNavigation.Name = "modelAliasNavigation";
            modelAliasNavigation.Size = new Size(180, 41);
            modelAliasNavigation.TabIndex = 1;
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
            // bindingAlias
            // 
            bindingAlias.AddingNew += bindingAlias_AddingNew;
            bindingAlias.BindingComplete += BindingComplete;
            // 
            // DomainEntity
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(524, 635);
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
            entityAlias.ResumeLayout(false);
            alaisLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)aliasData).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingAlias).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingEntity).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData entityTitleData;
        private Controls.TextBoxData entityDescriptionData;
        private TabPage entityPropertyTab;
        private TabPage entityAlias;
        private DataGridView propertyNavigation;
        private DataGridViewComboBoxColumn propertyTypeColumn;
        private DataGridViewTextBoxColumn propertyValueColumn;
        private TabPage propertyValueTab;
        private Controls.ComboBoxData propertyTypeData;
        private Controls.TextBoxData propertyValueData;
        private TabPage propertyDefinitionTab;
        private Controls.RichTextBoxData propertyDefinitionData;
        private ErrorProvider errorProvider;
        private BindingSource bindingProperties;
        private Controls.CheckedListBoxData propertyChoiceData;
        private Controls.ComboBoxData subjectAreaData;
        private DataGridView aliasData;
        private BindingSource bindingAlias;
        private TabControl entityTabLayout;
        private Controls.ModelAliasNavigation modelAliasNavigation;
        private DataGridViewTextBoxColumn scopeNameColumn;
        private DataGridViewTextBoxColumn aliasNameColumn;
        private BindingSource bindingEntity;
    }
}