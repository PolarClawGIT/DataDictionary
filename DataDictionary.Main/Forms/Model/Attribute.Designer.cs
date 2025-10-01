using DataDictionary.Resource.Enumerations;

namespace DataDictionary.Main.Forms.Model
{
    partial class Attribute
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
            TableLayoutPanel subjectAreaLayout;
            TableLayoutPanel xElementLayout;
            titleData = new DataDictionary.Main.Controls.TextBoxData();
            descriptionData = new DataDictionary.Main.Controls.TextBoxData();
            detailTabLayout = new TabControl();
            detailTab = new TabPage();
            dataLengthData = new DataDictionary.Main.Controls.TextBoxData();
            dataPrecisionData = new DataDictionary.Main.Controls.TextBoxData();
            isSingleValueData = new CheckBox();
            isMultiValuedData = new CheckBox();
            isSimpleTypeData = new CheckBox();
            isCompositeTypeData = new CheckBox();
            isDerivedData = new CheckBox();
            isValuedData = new CheckBox();
            isIntegralData = new CheckBox();
            isNullableData = new CheckBox();
            isNonKeyData = new CheckBox();
            isKeyData = new CheckBox();
            dataTypeData = new DataDictionary.Main.Controls.ComboBoxData();
            propertyTab = new TabPage();
            propertyData = new DataDictionary.Main.Forms.Model.Controls.PropertyData();
            definitionTab = new TabPage();
            definitionData = new DataDictionary.Main.Forms.Model.Controls.DefinitionData();
            aliasTab = new TabPage();
            aliasData = new DataDictionary.Main.Forms.Model.Controls.AliasData();
            subjectAreaTab = new TabPage();
            subjectArea = new DataDictionary.Main.Forms.Model.Controls.SubjectAreaData();
            memberNameData = new DataDictionary.Main.Controls.TextBoxData();
            xElementTab = new TabPage();
            xElementData = new DataDictionary.Main.Controls.TextBoxData();
            xElementToolStrip = new ToolStrip();
            xElementRenderCommand = new ToolStripButton();
            bindingAttribute = new BindingSource(components);
            bindingProperty = new BindingSource(components);
            bindingAlias = new BindingSource(components);
            bindingSubjectArea = new BindingSource(components);
            bindingDefinition = new BindingSource(components);
            mainLayout = new TableLayoutPanel();
            detailsLayout = new TableLayoutPanel();
            subjectAreaLayout = new TableLayoutPanel();
            xElementLayout = new TableLayoutPanel();
            mainLayout.SuspendLayout();
            detailTabLayout.SuspendLayout();
            detailTab.SuspendLayout();
            detailsLayout.SuspendLayout();
            propertyTab.SuspendLayout();
            definitionTab.SuspendLayout();
            aliasTab.SuspendLayout();
            subjectAreaTab.SuspendLayout();
            subjectAreaLayout.SuspendLayout();
            xElementTab.SuspendLayout();
            xElementLayout.SuspendLayout();
            xElementToolStrip.SuspendLayout();
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
            detailTabLayout.Controls.Add(xElementTab);
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
            detailsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            detailsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            detailsLayout.Controls.Add(dataLengthData, 0, 1);
            detailsLayout.Controls.Add(dataPrecisionData, 1, 1);
            detailsLayout.Controls.Add(isSingleValueData, 0, 2);
            detailsLayout.Controls.Add(isMultiValuedData, 1, 2);
            detailsLayout.Controls.Add(isSimpleTypeData, 0, 3);
            detailsLayout.Controls.Add(isCompositeTypeData, 1, 3);
            detailsLayout.Controls.Add(isDerivedData, 1, 4);
            detailsLayout.Controls.Add(isValuedData, 0, 4);
            detailsLayout.Controls.Add(isIntegralData, 0, 5);
            detailsLayout.Controls.Add(isNullableData, 1, 5);
            detailsLayout.Controls.Add(isNonKeyData, 0, 6);
            detailsLayout.Controls.Add(isKeyData, 1, 6);
            detailsLayout.Controls.Add(dataTypeData, 0, 0);
            detailsLayout.Dock = DockStyle.Fill;
            detailsLayout.Location = new Point(3, 3);
            detailsLayout.Name = "detailsLayout";
            detailsLayout.RowCount = 8;
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            detailsLayout.RowStyles.Add(new RowStyle());
            detailsLayout.RowStyles.Add(new RowStyle());
            detailsLayout.RowStyles.Add(new RowStyle());
            detailsLayout.RowStyles.Add(new RowStyle());
            detailsLayout.RowStyles.Add(new RowStyle());
            detailsLayout.RowStyles.Add(new RowStyle());
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 66.6666641F));
            detailsLayout.Size = new Size(406, 337);
            detailsLayout.TabIndex = 0;
            // 
            // dataLengthData
            // 
            dataLengthData.AutoSize = true;
            dataLengthData.Dock = DockStyle.Fill;
            dataLengthData.HeaderText = "Data Length";
            dataLengthData.Location = new Point(3, 57);
            dataLengthData.Multiline = false;
            dataLengthData.Name = "dataLengthData";
            dataLengthData.ReadOnly = false;
            dataLengthData.Size = new Size(197, 44);
            dataLengthData.TabIndex = 1;
            dataLengthData.WordWrap = true;
            // 
            // dataPrecisionData
            // 
            dataPrecisionData.AutoSize = true;
            dataPrecisionData.Dock = DockStyle.Fill;
            dataPrecisionData.HeaderText = "Data Precision";
            dataPrecisionData.Location = new Point(206, 57);
            dataPrecisionData.Multiline = false;
            dataPrecisionData.Name = "dataPrecisionData";
            dataPrecisionData.ReadOnly = false;
            dataPrecisionData.Size = new Size(197, 44);
            dataPrecisionData.TabIndex = 2;
            dataPrecisionData.WordWrap = true;
            // 
            // isSingleValueData
            // 
            isSingleValueData.AutoSize = true;
            isSingleValueData.Location = new Point(3, 107);
            isSingleValueData.Name = "isSingleValueData";
            isSingleValueData.Size = new Size(98, 19);
            isSingleValueData.TabIndex = 3;
            isSingleValueData.Text = "Single-Valued";
            isSingleValueData.UseVisualStyleBackColor = true;
            // 
            // isMultiValuedData
            // 
            isMultiValuedData.AutoSize = true;
            isMultiValuedData.Location = new Point(206, 107);
            isMultiValuedData.Name = "isMultiValuedData";
            isMultiValuedData.Size = new Size(94, 19);
            isMultiValuedData.TabIndex = 4;
            isMultiValuedData.Text = "Multi-Valued";
            isMultiValuedData.UseVisualStyleBackColor = true;
            // 
            // isSimpleTypeData
            // 
            isSimpleTypeData.AutoSize = true;
            isSimpleTypeData.Location = new Point(3, 132);
            isSimpleTypeData.Name = "isSimpleTypeData";
            isSimpleTypeData.Size = new Size(90, 19);
            isSimpleTypeData.TabIndex = 5;
            isSimpleTypeData.Text = "Simple Type";
            isSimpleTypeData.UseVisualStyleBackColor = true;
            // 
            // isCompositeTypeData
            // 
            isCompositeTypeData.AutoSize = true;
            isCompositeTypeData.Location = new Point(206, 132);
            isCompositeTypeData.Name = "isCompositeTypeData";
            isCompositeTypeData.Size = new Size(112, 19);
            isCompositeTypeData.TabIndex = 6;
            isCompositeTypeData.Text = "Composite Type";
            isCompositeTypeData.UseVisualStyleBackColor = true;
            // 
            // isDerivedData
            // 
            isDerivedData.AutoSize = true;
            isDerivedData.Location = new Point(206, 157);
            isDerivedData.Name = "isDerivedData";
            isDerivedData.Size = new Size(77, 19);
            isDerivedData.TabIndex = 8;
            isDerivedData.Text = "is Derived";
            isDerivedData.UseVisualStyleBackColor = true;
            // 
            // isValuedData
            // 
            isValuedData.AutoSize = true;
            isValuedData.Location = new Point(3, 157);
            isValuedData.Name = "isValuedData";
            isValuedData.Size = new Size(72, 19);
            isValuedData.TabIndex = 7;
            isValuedData.Text = "is Valued";
            isValuedData.UseVisualStyleBackColor = true;
            // 
            // isIntegralData
            // 
            isIntegralData.AutoSize = true;
            isIntegralData.Location = new Point(3, 182);
            isIntegralData.Name = "isIntegralData";
            isIntegralData.Size = new Size(77, 19);
            isIntegralData.TabIndex = 9;
            isIntegralData.Text = "is Integral";
            isIntegralData.UseVisualStyleBackColor = true;
            // 
            // isNullableData
            // 
            isNullableData.AutoSize = true;
            isNullableData.Location = new Point(206, 182);
            isNullableData.Name = "isNullableData";
            isNullableData.Size = new Size(81, 19);
            isNullableData.TabIndex = 10;
            isNullableData.Text = "is Nullable";
            isNullableData.UseVisualStyleBackColor = true;
            // 
            // isNonKeyData
            // 
            isNonKeyData.AutoSize = true;
            isNonKeyData.Location = new Point(3, 207);
            isNonKeyData.Name = "isNonKeyData";
            isNonKeyData.Size = new Size(84, 19);
            isNonKeyData.TabIndex = 11;
            isNonKeyData.Text = "is Non-Key";
            isNonKeyData.UseVisualStyleBackColor = true;
            // 
            // isKeyData
            // 
            isKeyData.AutoSize = true;
            isKeyData.Location = new Point(206, 207);
            isKeyData.Name = "isKeyData";
            isKeyData.Size = new Size(56, 19);
            isKeyData.TabIndex = 12;
            isKeyData.Text = "is Key";
            isKeyData.UseVisualStyleBackColor = true;
            // 
            // dataTypeData
            // 
            dataTypeData.AutoSize = true;
            dataTypeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            detailsLayout.SetColumnSpan(dataTypeData, 2);
            dataTypeData.Dock = DockStyle.Fill;
            dataTypeData.DropDownStyle = ComboBoxStyle.DropDown;
            dataTypeData.HeaderText = "Data Type";
            dataTypeData.Location = new Point(3, 3);
            dataTypeData.Name = "dataTypeData";
            dataTypeData.ReadOnly = false;
            dataTypeData.Size = new Size(400, 48);
            dataTypeData.TabIndex = 13;
            // 
            // propertyTab
            // 
            propertyTab.BackColor = SystemColors.Control;
            propertyTab.Controls.Add(propertyData);
            propertyTab.Location = new Point(4, 24);
            propertyTab.Name = "propertyTab";
            propertyTab.Padding = new Padding(3);
            propertyTab.Size = new Size(192, 72);
            propertyTab.TabIndex = 1;
            propertyTab.Text = "Properties";
            // 
            // propertyData
            // 
            propertyData.Dock = DockStyle.Fill;
            propertyData.Location = new Point(3, 3);
            propertyData.Name = "propertyData";
            propertyData.Size = new Size(186, 66);
            propertyData.TabIndex = 0;
            // 
            // definitionTab
            // 
            definitionTab.BackColor = SystemColors.Control;
            definitionTab.Controls.Add(definitionData);
            definitionTab.Location = new Point(4, 24);
            definitionTab.Name = "definitionTab";
            definitionTab.Padding = new Padding(3);
            definitionTab.Size = new Size(192, 72);
            definitionTab.TabIndex = 5;
            definitionTab.Text = "Definition";
            // 
            // definitionData
            // 
            definitionData.Dock = DockStyle.Fill;
            definitionData.Location = new Point(3, 3);
            definitionData.Name = "definitionData";
            definitionData.Size = new Size(186, 66);
            definitionData.TabIndex = 0;
            // 
            // aliasTab
            // 
            aliasTab.BackColor = SystemColors.Control;
            aliasTab.Controls.Add(aliasData);
            aliasTab.Location = new Point(4, 24);
            aliasTab.Name = "aliasTab";
            aliasTab.Padding = new Padding(3);
            aliasTab.Size = new Size(192, 72);
            aliasTab.TabIndex = 2;
            aliasTab.Text = "Aliases";
            // 
            // aliasData
            // 
            aliasData.Dock = DockStyle.Fill;
            aliasData.Location = new Point(3, 3);
            aliasData.Name = "aliasData";
            aliasData.Size = new Size(186, 66);
            aliasData.TabIndex = 0;
            // 
            // subjectAreaTab
            // 
            subjectAreaTab.BackColor = SystemColors.Control;
            subjectAreaTab.Controls.Add(subjectAreaLayout);
            subjectAreaTab.Location = new Point(4, 24);
            subjectAreaTab.Name = "subjectAreaTab";
            subjectAreaTab.Padding = new Padding(3);
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
            subjectAreaLayout.Location = new Point(3, 3);
            subjectAreaLayout.Name = "subjectAreaLayout";
            subjectAreaLayout.RowCount = 2;
            subjectAreaLayout.RowStyles.Add(new RowStyle());
            subjectAreaLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            subjectAreaLayout.Size = new Size(186, 66);
            subjectAreaLayout.TabIndex = 1;
            // 
            // subjectArea
            // 
            subjectArea.Dock = DockStyle.Fill;
            subjectArea.Location = new Point(3, 53);
            subjectArea.Name = "subjectArea";
            subjectArea.Size = new Size(180, 10);
            subjectArea.TabIndex = 0;
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
            memberNameData.Size = new Size(180, 44);
            memberNameData.TabIndex = 1;
            memberNameData.WordWrap = true;
            memberNameData.Validating += MemberNameData_Validating;
            // 
            // xElementTab
            // 
            xElementTab.BackColor = SystemColors.Control;
            xElementTab.Controls.Add(xElementLayout);
            xElementTab.Location = new Point(4, 24);
            xElementTab.Name = "xElementTab";
            xElementTab.Padding = new Padding(3);
            xElementTab.Size = new Size(412, 343);
            xElementTab.TabIndex = 4;
            xElementTab.Text = "XML";
            // 
            // xElementLayout
            // 
            xElementLayout.ColumnCount = 1;
            xElementLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            xElementLayout.Controls.Add(xElementData, 0, 1);
            xElementLayout.Controls.Add(xElementToolStrip, 0, 0);
            xElementLayout.Dock = DockStyle.Fill;
            xElementLayout.Location = new Point(3, 3);
            xElementLayout.Name = "xElementLayout";
            xElementLayout.RowCount = 2;
            xElementLayout.RowStyles.Add(new RowStyle());
            xElementLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            xElementLayout.Size = new Size(406, 337);
            xElementLayout.TabIndex = 1;
            // 
            // xElementData
            // 
            xElementData.AutoSize = true;
            xElementData.Dock = DockStyle.Fill;
            xElementData.HeaderText = "XML fragment";
            xElementData.Location = new Point(3, 28);
            xElementData.Multiline = true;
            xElementData.Name = "xElementData";
            xElementData.ReadOnly = true;
            xElementData.Size = new Size(400, 306);
            xElementData.TabIndex = 0;
            xElementData.WordWrap = false;
            // 
            // xElementToolStrip
            // 
            xElementToolStrip.Items.AddRange(new ToolStripItem[] { xElementRenderCommand });
            xElementToolStrip.Location = new Point(0, 0);
            xElementToolStrip.Name = "xElementToolStrip";
            xElementToolStrip.Size = new Size(406, 25);
            xElementToolStrip.TabIndex = 1;
            xElementToolStrip.Text = "toolStrip1";
            // 
            // xElementRenderCommand
            // 
            xElementRenderCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            xElementRenderCommand.ImageTransparentColor = Color.Magenta;
            xElementRenderCommand.Name = "xElementRenderCommand";
            xElementRenderCommand.Size = new Size(23, 22);
            xElementRenderCommand.Text = "render XML fragement";
            xElementRenderCommand.Click += XElementRenderCommand_Click;
            // 
            // Attribute
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(426, 546);
            Controls.Add(mainLayout);
            Name = "Attribute";
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
            definitionTab.ResumeLayout(false);
            aliasTab.ResumeLayout(false);
            subjectAreaTab.ResumeLayout(false);
            subjectAreaLayout.ResumeLayout(false);
            subjectAreaLayout.PerformLayout();
            xElementTab.ResumeLayout(false);
            xElementLayout.ResumeLayout(false);
            xElementLayout.PerformLayout();
            xElementToolStrip.ResumeLayout(false);
            xElementToolStrip.PerformLayout();
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
        private TabPage xElementTab;
        private BindingSource bindingAlias;
        private BindingSource bindingSubjectArea;
        private Controls.SubjectAreaData subjectArea;
        private TabPage definitionTab;
        private BindingSource bindingDefinition;
        private DataDictionary.Main.Controls.TextBoxData memberNameData;
        private DataDictionary.Main.Controls.TextBoxData dataLengthData;
        private DataDictionary.Main.Controls.TextBoxData dataPrecisionData;
        private DataDictionary.Main.Controls.ComboBoxData dataTypeData;
        private Controls.PropertyData propertyData;
        private Controls.DefinitionData definitionData;
        private Controls.AliasData aliasData;
        private DataDictionary.Main.Controls.TextBoxData xElementData;
        private ToolStrip xElementToolStrip;
        private ToolStripButton xElementRenderCommand;
    }
}