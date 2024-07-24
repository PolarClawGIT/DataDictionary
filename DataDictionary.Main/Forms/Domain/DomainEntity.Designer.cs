﻿using DataDictionary.Resource.Enumerations;

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
            TableLayoutPanel mainLayout;
            TableLayoutPanel detailsLayout;
            TableLayoutPanel attributeDetailLayout;
            TableLayoutPanel propertyLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DomainEntity));
            TableLayoutPanel definitionLayout;
            BusinessLayer.NamedScope.NamedScopePath namedScopePath1 = new BusinessLayer.NamedScope.NamedScopePath();
            titleData = new DataDictionary.Main.Controls.TextBoxData();
            descriptionData = new DataDictionary.Main.Controls.TextBoxData();
            detailTabLayout = new TabControl();
            detailTab = new TabPage();
            attributeData = new DataGridView();
            attributeColumn = new DataGridViewComboBoxColumn();
            attributeOrderColumn = new DataGridViewTextBoxColumn();
            attributeOrderData = new DataDictionary.Main.Controls.TextBoxData();
            attributeTitleData = new DataDictionary.Main.Controls.TextBoxData();
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
            attributeSelect = new Button();
            mainLayout = new TableLayoutPanel();
            detailsLayout = new TableLayoutPanel();
            attributeDetailLayout = new TableLayoutPanel();
            propertyLayout = new TableLayoutPanel();
            definitionLayout = new TableLayoutPanel();
            mainLayout.SuspendLayout();
            detailTabLayout.SuspendLayout();
            detailTab.SuspendLayout();
            detailsLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)attributeData).BeginInit();
            attributeDetailLayout.SuspendLayout();
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
            mainLayout.Size = new Size(426, 521);
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
            detailsLayout.ColumnCount = 1;
            detailsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            detailsLayout.Controls.Add(attributeData, 0, 0);
            detailsLayout.Controls.Add(attributeDetailLayout, 0, 1);
            detailsLayout.Dock = DockStyle.Fill;
            detailsLayout.Location = new Point(3, 3);
            detailsLayout.Name = "detailsLayout";
            detailsLayout.RowCount = 2;
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            detailsLayout.Size = new Size(406, 337);
            detailsLayout.TabIndex = 0;
            // 
            // attributeData
            // 
            attributeData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            attributeData.Columns.AddRange(new DataGridViewColumn[] { attributeColumn, attributeOrderColumn });
            attributeData.Dock = DockStyle.Fill;
            attributeData.Location = new Point(3, 3);
            attributeData.Name = "attributeData";
            attributeData.Size = new Size(400, 229);
            attributeData.TabIndex = 0;
            attributeData.DataError += AttributeData_DataError;
            // 
            // attributeColumn
            // 
            attributeColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            attributeColumn.DataPropertyName = "AttributeId";
            attributeColumn.HeaderText = "Attribute";
            attributeColumn.Name = "attributeColumn";
            // 
            // attributeOrderColumn
            // 
            attributeOrderColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            attributeOrderColumn.DataPropertyName = "OrdinalPosition";
            attributeOrderColumn.FillWeight = 30F;
            attributeOrderColumn.HeaderText = "Order";
            attributeOrderColumn.Name = "attributeOrderColumn";
            // 
            // attributeDetailLayout
            // 
            attributeDetailLayout.ColumnCount = 2;
            attributeDetailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            attributeDetailLayout.ColumnStyles.Add(new ColumnStyle());
            attributeDetailLayout.Controls.Add(attributeOrderData, 1, 1);
            attributeDetailLayout.Controls.Add(attributeTitleData, 0, 0);
            attributeDetailLayout.Controls.Add(attributeSelect, 1, 0);
            attributeDetailLayout.Dock = DockStyle.Fill;
            attributeDetailLayout.Location = new Point(3, 238);
            attributeDetailLayout.Name = "attributeDetailLayout";
            attributeDetailLayout.RowCount = 2;
            attributeDetailLayout.RowStyles.Add(new RowStyle());
            attributeDetailLayout.RowStyles.Add(new RowStyle());
            attributeDetailLayout.Size = new Size(400, 96);
            attributeDetailLayout.TabIndex = 1;
            // 
            // attributeOrderData
            // 
            attributeOrderData.AutoSize = true;
            attributeOrderData.Dock = DockStyle.Fill;
            attributeOrderData.HeaderText = "Order";
            attributeOrderData.Location = new Point(277, 53);
            attributeOrderData.Multiline = false;
            attributeOrderData.Name = "attributeOrderData";
            attributeOrderData.ReadOnly = false;
            attributeOrderData.Size = new Size(120, 44);
            attributeOrderData.TabIndex = 2;
            attributeOrderData.WordWrap = true;
            // 
            // attributeTitleData
            // 
            attributeTitleData.AutoSize = true;
            attributeTitleData.Dock = DockStyle.Fill;
            attributeTitleData.HeaderText = "Attribute Title";
            attributeTitleData.Location = new Point(3, 3);
            attributeTitleData.Multiline = false;
            attributeTitleData.Name = "attributeTitleData";
            attributeTitleData.ReadOnly = false;
            attributeTitleData.Size = new Size(268, 44);
            attributeTitleData.TabIndex = 3;
            attributeTitleData.WordWrap = true;
            attributeTitleData.Validated += AttributeTitleData_Validated;
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
            // domainProperty
            // 
            domainProperty.ApplyImage = (Image)resources.GetObject("domainProperty.ApplyImage");
            domainProperty.ApplyText = "apply";
            domainProperty.Dock = DockStyle.Fill;
            domainProperty.Location = new Point(3, 29);
            domainProperty.Name = "domainProperty";
            domainProperty.PropertyId = new Guid("00000000-0000-0000-0000-000000000000");
            domainProperty.PropertyValue = "";
            domainProperty.ReadOnly = false;
            domainProperty.Size = new Size(180, 34);
            domainProperty.TabIndex = 2;
            domainProperty.OnApply += DomainProperty_OnApply;
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
            definitionLayout.Controls.Add(domainDefinition, 0, 1);
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
            definitionData.Location = new Point(6, 6);
            definitionData.Name = "definitionData";
            definitionData.ReadOnly = true;
            definitionData.Size = new Size(174, 12);
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
            domainDefinition.Location = new Point(6, 24);
            domainDefinition.Name = "domainDefinition";
            domainDefinition.ReadOnly = false;
            domainDefinition.Size = new Size(174, 36);
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
            bindingAttribute.CurrentItemChanged += BindingAttribute_CurrentItemChanged;
            // 
            // bindingAttributeDetail
            // 
            bindingAttributeDetail.AddingNew += BindingAttributeDetail_AddingNew;
            // 
            // attributeSelect
            // 
            attributeSelect.Location = new Point(277, 3);
            attributeSelect.Name = "attributeSelect";
            attributeSelect.Size = new Size(75, 23);
            attributeSelect.TabIndex = 4;
            attributeSelect.Text = "Select";
            attributeSelect.TextImageRelation = TextImageRelation.ImageBeforeText;
            attributeSelect.UseVisualStyleBackColor = true;
            attributeSelect.Click += attributeSelect_Click;
            // 
            // DomainEntity
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(426, 546);
            Controls.Add(mainLayout);
            Name = "DomainEntity";
            Text = "DomainEntity";
            Load += Form_Load;
            Controls.SetChildIndex(mainLayout, 0);
            mainLayout.ResumeLayout(false);
            mainLayout.PerformLayout();
            detailTabLayout.ResumeLayout(false);
            detailTab.ResumeLayout(false);
            detailsLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)attributeData).EndInit();
            attributeDetailLayout.ResumeLayout(false);
            attributeDetailLayout.PerformLayout();
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
        private Controls.DomainProperty domainProperty;
        private TabPage aliasTab;
        private TableLayoutPanel aliaseLayout;
        private DataGridView aliasesData;
        private TabPage subjectAreaTab;
        private BindingSource bindingAlias;
        private BindingSource bindingProperty;
        private BindingSource bindingEntity;
        private DataDictionary.Main.Controls.NamedScopeData namedScopeData;
        private BindingSource bindingSubjectArea;
        private Controls.SubjectArea subjectArea;
        private DataGridViewComboBoxColumn aliaseScopeColumn;
        private DataGridViewTextBoxColumn aliasNameColumn;
        private TabPage definitionTab;
        private BindingSource bindingDefinition;
        private DataGridView definitionData;
        private DataGridViewComboBoxColumn definitionColumn;
        private DataGridViewTextBoxColumn definitionSummaryColumn;
        private Controls.DomainDefinition domainDefinition;
        private TableLayoutPanel subjectAreaLayout;
        private DataDictionary.Main.Controls.TextBoxData memberNameData;
        private DataGridView attributeData;
        private BindingSource bindingAttribute;
        private DataGridViewComboBoxColumn attributeColumn;
        private DataGridViewTextBoxColumn attributeOrderColumn;
        private BindingSource bindingAttributeDetail;
        private DataDictionary.Main.Controls.TextBoxData attributeOrderData;
        private DataDictionary.Main.Controls.TextBoxData attributeTitleData;
        private Button attributeSelect;
    }
}