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
            BusinessLayer.NamedScope.NamedScopePath namedScopePath1 = new BusinessLayer.NamedScope.NamedScopePath();
            titleData = new DataDictionary.Main.Controls.TextBoxData();
            descriptionData = new DataDictionary.Main.Controls.TextBoxData();
            detailTabLayout = new TabControl();
            detailTab = new TabPage();
            isMultiValuedData = new CheckBox();
            isSingleValueData = new CheckBox();
            typeOfAttributeData = new DataDictionary.Main.Controls.ComboBoxData();
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
            namedScopeData = new DataDictionary.Main.Controls.NamedScopeData();
            subjectAreaTab = new TabPage();
            subjectArea = new Controls.SubjectArea();
            entityTab = new TabPage();
            bindingAttribute = new BindingSource(components);
            bindingProperty = new BindingSource(components);
            bindingAlias = new BindingSource(components);
            attributeToolStrip = new ContextMenuStrip(components);
            removeAttributeCommand = new ToolStripMenuItem();
            bindingSubjectArea = new BindingSource(components);
            bindingDefinition = new BindingSource(components);
            mainLayout = new TableLayoutPanel();
            detailsLayout = new TableLayoutPanel();
            propertyLayout = new TableLayoutPanel();
            definitionLayout = new TableLayoutPanel();
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
            subjectAreaTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingAttribute).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingProperty).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingAlias).BeginInit();
            attributeToolStrip.SuspendLayout();
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
            detailsLayout.Controls.Add(isMultiValuedData, 1, 1);
            detailsLayout.Controls.Add(isSingleValueData, 0, 1);
            detailsLayout.Controls.Add(typeOfAttributeData, 0, 0);
            detailsLayout.Controls.Add(isSimpleTypeData, 0, 2);
            detailsLayout.Controls.Add(isCompositeTypeData, 1, 2);
            detailsLayout.Controls.Add(isIntegralData, 0, 3);
            detailsLayout.Controls.Add(isDerivedData, 1, 3);
            detailsLayout.Controls.Add(isValuedData, 0, 4);
            detailsLayout.Controls.Add(isNullableData, 1, 4);
            detailsLayout.Controls.Add(isNonKeyData, 0, 5);
            detailsLayout.Controls.Add(isKeyData, 1, 5);
            detailsLayout.Dock = DockStyle.Fill;
            detailsLayout.Location = new Point(3, 3);
            detailsLayout.Name = "detailsLayout";
            detailsLayout.RowCount = 6;
            detailsLayout.RowStyles.Add(new RowStyle());
            detailsLayout.RowStyles.Add(new RowStyle());
            detailsLayout.RowStyles.Add(new RowStyle());
            detailsLayout.RowStyles.Add(new RowStyle());
            detailsLayout.RowStyles.Add(new RowStyle());
            detailsLayout.RowStyles.Add(new RowStyle());
            detailsLayout.Size = new Size(406, 337);
            detailsLayout.TabIndex = 0;
            // 
            // isMultiValuedData
            // 
            isMultiValuedData.AutoSize = true;
            isMultiValuedData.Location = new Point(107, 53);
            isMultiValuedData.Name = "isMultiValuedData";
            isMultiValuedData.Size = new Size(94, 19);
            isMultiValuedData.TabIndex = 1;
            isMultiValuedData.Text = "Multi-Valued";
            isMultiValuedData.UseVisualStyleBackColor = true;
            // 
            // isSingleValueData
            // 
            isSingleValueData.AutoSize = true;
            isSingleValueData.Location = new Point(3, 53);
            isSingleValueData.Name = "isSingleValueData";
            isSingleValueData.Size = new Size(98, 19);
            isSingleValueData.TabIndex = 0;
            isSingleValueData.Text = "Single-Valued";
            isSingleValueData.UseVisualStyleBackColor = true;
            // 
            // typeOfAttributeData
            // 
            typeOfAttributeData.AutoSize = true;
            typeOfAttributeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            detailsLayout.SetColumnSpan(typeOfAttributeData, 2);
            typeOfAttributeData.Dock = DockStyle.Fill;
            typeOfAttributeData.DropDownStyle = ComboBoxStyle.DropDown;
            typeOfAttributeData.HeaderText = "Type of Attribute";
            typeOfAttributeData.Location = new Point(3, 3);
            typeOfAttributeData.Name = "typeOfAttributeData";
            typeOfAttributeData.ReadOnly = false;
            typeOfAttributeData.Size = new Size(400, 44);
            typeOfAttributeData.TabIndex = 0;
            // 
            // isSimpleTypeData
            // 
            isSimpleTypeData.AutoSize = true;
            isSimpleTypeData.Location = new Point(3, 78);
            isSimpleTypeData.Name = "isSimpleTypeData";
            isSimpleTypeData.Size = new Size(89, 19);
            isSimpleTypeData.TabIndex = 2;
            isSimpleTypeData.Text = "Simple Type";
            isSimpleTypeData.UseVisualStyleBackColor = true;
            // 
            // isCompositeTypeData
            // 
            isCompositeTypeData.AutoSize = true;
            isCompositeTypeData.Location = new Point(107, 78);
            isCompositeTypeData.Name = "isCompositeTypeData";
            isCompositeTypeData.Size = new Size(111, 19);
            isCompositeTypeData.TabIndex = 3;
            isCompositeTypeData.Text = "Composite Type";
            isCompositeTypeData.UseVisualStyleBackColor = true;
            // 
            // isIntegralData
            // 
            isIntegralData.AutoSize = true;
            isIntegralData.Location = new Point(3, 103);
            isIntegralData.Name = "isIntegralData";
            isIntegralData.Size = new Size(77, 19);
            isIntegralData.TabIndex = 4;
            isIntegralData.Text = "is Integral";
            isIntegralData.UseVisualStyleBackColor = true;
            // 
            // isDerivedData
            // 
            isDerivedData.AutoSize = true;
            isDerivedData.Location = new Point(107, 103);
            isDerivedData.Name = "isDerivedData";
            isDerivedData.Size = new Size(77, 19);
            isDerivedData.TabIndex = 5;
            isDerivedData.Text = "is Derived";
            isDerivedData.UseVisualStyleBackColor = true;
            // 
            // isValuedData
            // 
            isValuedData.AutoSize = true;
            isValuedData.Location = new Point(3, 128);
            isValuedData.Name = "isValuedData";
            isValuedData.Size = new Size(72, 19);
            isValuedData.TabIndex = 6;
            isValuedData.Text = "is Valued";
            isValuedData.UseVisualStyleBackColor = true;
            // 
            // isNullableData
            // 
            isNullableData.AutoSize = true;
            isNullableData.Location = new Point(107, 128);
            isNullableData.Name = "isNullableData";
            isNullableData.Size = new Size(81, 19);
            isNullableData.TabIndex = 7;
            isNullableData.Text = "is Nullable";
            isNullableData.UseVisualStyleBackColor = true;
            // 
            // isNonKeyData
            // 
            isNonKeyData.AutoSize = true;
            isNonKeyData.Location = new Point(3, 153);
            isNonKeyData.Name = "isNonKeyData";
            isNonKeyData.Size = new Size(84, 19);
            isNonKeyData.TabIndex = 8;
            isNonKeyData.Text = "is Non-Key";
            isNonKeyData.UseVisualStyleBackColor = true;
            // 
            // isKeyData
            // 
            isKeyData.AutoSize = true;
            isKeyData.Location = new Point(107, 153);
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
            propertyTab.Size = new Size(412, 343);
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
            propertyLayout.Size = new Size(406, 337);
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
            propertiesData.Size = new Size(400, 95);
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
            domainProperty.Location = new Point(3, 104);
            domainProperty.Name = "domainProperty";
            domainProperty.PropertyId = new Guid("00000000-0000-0000-0000-000000000000");
            domainProperty.PropertyValue = "";
            domainProperty.ReadOnly = false;
            domainProperty.Size = new Size(400, 230);
            domainProperty.TabIndex = 2;
            domainProperty.OnApply += DomainProperty_OnApply;
            // 
            // definitionTab
            // 
            definitionTab.BackColor = SystemColors.Control;
            definitionTab.Controls.Add(definitionLayout);
            definitionTab.Location = new Point(4, 24);
            definitionTab.Name = "definitionTab";
            definitionTab.Size = new Size(412, 343);
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
            definitionLayout.Size = new Size(412, 343);
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
            definitionData.Size = new Size(400, 95);
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
            domainDefinition.Location = new Point(6, 107);
            domainDefinition.Name = "domainDefinition";
            domainDefinition.ReadOnly = false;
            domainDefinition.Size = new Size(400, 230);
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
            aliasTab.Size = new Size(192, 72);
            aliasTab.TabIndex = 2;
            aliasTab.Text = "Aliases";
            // 
            // aliaseLayout
            // 
            aliaseLayout.ColumnCount = 1;
            aliaseLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            aliaseLayout.Controls.Add(aliasesData, 0, 0);
            aliaseLayout.Controls.Add(namedScopeData, 0, 1);
            aliaseLayout.Dock = DockStyle.Fill;
            aliaseLayout.Location = new Point(3, 3);
            aliaseLayout.Name = "aliaseLayout";
            aliaseLayout.RowCount = 2;
            aliaseLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            aliaseLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            aliaseLayout.Size = new Size(186, 66);
            aliaseLayout.TabIndex = 1;
            // 
            // aliasesData
            // 
            aliasesData.AllowUserToAddRows = false;
            aliasesData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            aliasesData.Columns.AddRange(new DataGridViewColumn[] { aliaseScopeColumn, aliasNameColumn });
            aliasesData.Dock = DockStyle.Fill;
            aliasesData.Location = new Point(3, 3);
            aliasesData.Name = "aliasesData";
            aliasesData.ReadOnly = true;
            aliasesData.Size = new Size(180, 20);
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
            // namedScopeData
            // 
            namedScopeData.ApplyImage = Properties.Resources.NewSynonym;
            namedScopeData.ApplyText = "apply";
            namedScopeData.Dock = DockStyle.Fill;
            namedScopeData.HeaderText = "Alias";
            namedScopeData.Location = new Point(3, 29);
            namedScopeData.Name = "namedScopeData";
            namedScopeData.ReadOnly = false;
            namedScopeData.Scope = ScopeType.Null;
            namedScopeData.ScopePath = namedScopePath1;
            namedScopeData.Size = new Size(180, 34);
            namedScopeData.TabIndex = 1;
            namedScopeData.OnApply += NamedScopeData_OnApply;
            // 
            // subjectAreaTab
            // 
            subjectAreaTab.BackColor = SystemColors.Control;
            subjectAreaTab.Controls.Add(subjectArea);
            subjectAreaTab.Location = new Point(4, 24);
            subjectAreaTab.Name = "subjectAreaTab";
            subjectAreaTab.Size = new Size(192, 72);
            subjectAreaTab.TabIndex = 3;
            subjectAreaTab.Text = "Subject Area";
            // 
            // subjectArea
            // 
            subjectArea.Dock = DockStyle.Fill;
            subjectArea.Location = new Point(0, 0);
            subjectArea.Name = "subjectArea";
            subjectArea.Size = new Size(192, 72);
            subjectArea.TabIndex = 0;
            subjectArea.OnSubjectAdd += subjectArea_OnSubjectAdd;
            subjectArea.OnSubjectRemove += subjectArea_OnSubjectRemove;
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
            // attributeToolStrip
            // 
            attributeToolStrip.Items.AddRange(new ToolStripItem[] { removeAttributeCommand });
            attributeToolStrip.Name = "attributeContextMenu";
            attributeToolStrip.Size = new Size(165, 26);
            // 
            // removeAttributeCommand
            // 
            removeAttributeCommand.Image = Properties.Resources.DeleteAttribute;
            removeAttributeCommand.Name = "removeAttributeCommand";
            removeAttributeCommand.Size = new Size(164, 22);
            removeAttributeCommand.Text = "remove Attribute";
            removeAttributeCommand.Click += DeleteItemCommand_Click;
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
            ((System.ComponentModel.ISupportInitialize)aliasesData).EndInit();
            subjectAreaTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)bindingAttribute).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingProperty).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingAlias).EndInit();
            attributeToolStrip.ResumeLayout(false);
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
        private DataDictionary.Main.Controls.ComboBoxData typeOfAttributeData;
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
        private DataGridView aliasesData;
        private BindingSource bindingAlias;
        private DataGridView propertiesData;
        private DataGridViewComboBoxColumn propertyIdColumn;
        private DataGridViewTextBoxColumn propertyValueColumn;
        private Controls.DomainProperty domainProperty;
        private TableLayoutPanel aliaseLayout;
        private ContextMenuStrip attributeToolStrip;
        private ToolStripMenuItem removeAttributeCommand;
        private DataDictionary.Main.Controls.NamedScopeData namedScopeData;
        private BindingSource bindingSubjectArea;
        private Controls.SubjectArea subjectArea;
        private DataGridViewComboBoxColumn aliaseScopeColumn;
        private DataGridViewTextBoxColumn aliasNameColumn;
        private TabPage definitionTab;
        private DataGridView definitionData;
        private BindingSource bindingDefinition;
        private DataGridViewComboBoxColumn definitionColumn;
        private DataGridViewTextBoxColumn definitionSummaryColumn;
        private Controls.DomainDefinition domainDefinition;
    }
}