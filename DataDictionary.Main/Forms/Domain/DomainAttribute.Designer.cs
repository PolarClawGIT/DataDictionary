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
            SplitContainer propertiesSplit;
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
            aliasTab = new TabPage();
            mainBinding = new BindingSource(components);
            toolTip = new ToolTip(components);
            propertyBinding = new BindingSource(components);
            subjectAreaTab = new TabPage();
            entityTab = new TabPage();
            mainLayout = new TableLayoutPanel();
            detailsLayout = new TableLayoutPanel();
            propertiesSplit = new SplitContainer();
            mainLayout.SuspendLayout();
            detailTabLayout.SuspendLayout();
            detailTab.SuspendLayout();
            detailsLayout.SuspendLayout();
            propertyTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)propertiesSplit).BeginInit();
            propertiesSplit.Panel1.SuspendLayout();
            propertiesSplit.Panel2.SuspendLayout();
            propertiesSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)propertiesData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mainBinding).BeginInit();
            ((System.ComponentModel.ISupportInitialize)propertyBinding).BeginInit();
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
            detailTabLayout.Controls.Add(aliasTab);
            detailTabLayout.Controls.Add(subjectAreaTab);
            detailTabLayout.Controls.Add(entityTab);
            detailTabLayout.Dock = DockStyle.Fill;
            detailTabLayout.Location = new Point(3, 147);
            detailTabLayout.Name = "detailTabLayout";
            detailTabLayout.SelectedIndex = 0;
            detailTabLayout.Size = new Size(420, 371);
            detailTabLayout.TabIndex = 2;
            detailTabLayout.SelectedIndexChanged += DetailTabLayout_SelectedIndexChanged;
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
            propertyTab.Controls.Add(propertiesSplit);
            propertyTab.Location = new Point(4, 24);
            propertyTab.Name = "propertyTab";
            propertyTab.Padding = new Padding(3);
            propertyTab.Size = new Size(412, 343);
            propertyTab.TabIndex = 1;
            propertyTab.Text = "Properties";
            // 
            // propertiesSplit
            // 
            propertiesSplit.Dock = DockStyle.Fill;
            propertiesSplit.Location = new Point(3, 3);
            propertiesSplit.Name = "propertiesSplit";
            propertiesSplit.Orientation = Orientation.Horizontal;
            // 
            // propertiesSplit.Panel1
            // 
            propertiesSplit.Panel1.Controls.Add(propertiesData);
            // 
            // propertiesSplit.Panel2
            // 
            propertiesSplit.Panel2.Controls.Add(domainProperty);
            propertiesSplit.Size = new Size(406, 337);
            propertiesSplit.SplitterDistance = 100;
            propertiesSplit.TabIndex = 1;
            // 
            // propertiesData
            // 
            propertiesData.AllowUserToAddRows = false;
            propertiesData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            propertiesData.Columns.AddRange(new DataGridViewColumn[] { propertyIdColumn, propertyValueColumn });
            propertiesData.Dock = DockStyle.Fill;
            propertiesData.Location = new Point(0, 0);
            propertiesData.Name = "propertiesData";
            propertiesData.ReadOnly = true;
            propertiesData.RowTemplate.Height = 25;
            propertiesData.Size = new Size(406, 100);
            propertiesData.TabIndex = 0;
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
            domainProperty.Dock = DockStyle.Fill;
            domainProperty.Location = new Point(0, 0);
            domainProperty.Name = "domainProperty";
            domainProperty.Size = new Size(406, 233);
            domainProperty.TabIndex = 1;
            // 
            // aliasTab
            // 
            aliasTab.BackColor = SystemColors.Control;
            aliasTab.Location = new Point(4, 24);
            aliasTab.Name = "aliasTab";
            aliasTab.Size = new Size(412, 343);
            aliasTab.TabIndex = 2;
            aliasTab.Text = "Aliases";
            // 
            // propertyBinding
            // 
            propertyBinding.AddingNew += PropertyBinding_AddingNew;
            // 
            // subjectAreaTab
            // 
            subjectAreaTab.BackColor = SystemColors.Control;
            subjectAreaTab.Location = new Point(4, 24);
            subjectAreaTab.Name = "subjectAreaTab";
            subjectAreaTab.Size = new Size(412, 343);
            subjectAreaTab.TabIndex = 3;
            subjectAreaTab.Text = "Subject Area";
            // 
            // entityTab
            // 
            entityTab.BackColor = SystemColors.Control;
            entityTab.Location = new Point(4, 24);
            entityTab.Name = "entityTab";
            entityTab.Size = new Size(412, 343);
            entityTab.TabIndex = 4;
            entityTab.Text = "Entities";
            // 
            // DomainAttribute
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(426, 546);
            Controls.Add(mainLayout);
            Name = "DomainAttribute";
            Text = "DomainAttribute_New";
            Load += Form_Load;
            Controls.SetChildIndex(mainLayout, 0);
            mainLayout.ResumeLayout(false);
            mainLayout.PerformLayout();
            detailTabLayout.ResumeLayout(false);
            detailTab.ResumeLayout(false);
            detailsLayout.ResumeLayout(false);
            detailsLayout.PerformLayout();
            propertyTab.ResumeLayout(false);
            propertiesSplit.Panel1.ResumeLayout(false);
            propertiesSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)propertiesSplit).EndInit();
            propertiesSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)propertiesData).EndInit();
            ((System.ComponentModel.ISupportInitialize)mainBinding).EndInit();
            ((System.ComponentModel.ISupportInitialize)propertyBinding).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataDictionary.Main.Controls.TextBoxData titleData;
        private DataDictionary.Main.Controls.TextBoxData descriptionData;
        private TabControl detailTabLayout;
        private TabPage detailTab;
        private TabPage propertyTab;
        private BindingSource mainBinding;
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
        private ToolTip toolTip;
        private DataGridView propertiesData;
        private Controls.DomainProperty domainProperty;
        private DataGridViewComboBoxColumn propertyIdColumn;
        private DataGridViewTextBoxColumn propertyValueColumn;
        private BindingSource propertyBinding;
        private SplitContainer propertiesSplit;
        private TabPage subjectAreaTab;
        private TabPage entityTab;
    }
}