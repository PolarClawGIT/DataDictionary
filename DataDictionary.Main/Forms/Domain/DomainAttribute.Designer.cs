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
            titleData = new Controls.TextBoxData();
            descriptionData = new Controls.TextBoxData();
            detailTabLayout = new TabControl();
            detailTab = new TabPage();
            isMultiValuedData = new CheckBox();
            isSingleValueData = new CheckBox();
            typeOfAttributeData = new Controls.ComboBoxData();
            isSimpleTypeData = new CheckBox();
            isCompositeTypeData = new CheckBox();
            isIntegralData = new CheckBox();
            isDerivedData = new CheckBox();
            isValuedData = new CheckBox();
            isNullableData = new CheckBox();
            isNonKeyData = new CheckBox();
            isKeyData = new CheckBox();
            propertyTab = new TabPage();
            aliasTab = new TabPage();
            bindingSource = new BindingSource(components);
            toolTip = new ToolTip(components);
            mainLayout = new TableLayoutPanel();
            detailsLayout = new TableLayoutPanel();
            mainLayout.SuspendLayout();
            detailTabLayout.SuspendLayout();
            detailTab.SuspendLayout();
            detailsLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource).BeginInit();
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
            mainLayout.Size = new Size(353, 450);
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
            titleData.Size = new Size(347, 44);
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
            descriptionData.Size = new Size(347, 74);
            descriptionData.TabIndex = 1;
            // 
            // detailTabLayout
            // 
            detailTabLayout.Controls.Add(detailTab);
            detailTabLayout.Controls.Add(propertyTab);
            detailTabLayout.Controls.Add(aliasTab);
            detailTabLayout.Dock = DockStyle.Fill;
            detailTabLayout.Location = new Point(3, 133);
            detailTabLayout.Name = "detailTabLayout";
            detailTabLayout.SelectedIndex = 0;
            detailTabLayout.Size = new Size(347, 314);
            detailTabLayout.TabIndex = 2;
            // 
            // detailTab
            // 
            detailTab.BackColor = SystemColors.Control;
            detailTab.Controls.Add(detailsLayout);
            detailTab.Location = new Point(4, 24);
            detailTab.Name = "detailTab";
            detailTab.Padding = new Padding(3);
            detailTab.Size = new Size(339, 286);
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
            detailsLayout.Size = new Size(333, 280);
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
            typeOfAttributeData.Size = new Size(327, 44);
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
            propertyTab.Location = new Point(4, 24);
            propertyTab.Name = "propertyTab";
            propertyTab.Padding = new Padding(3);
            propertyTab.Size = new Size(192, 72);
            propertyTab.TabIndex = 1;
            propertyTab.Text = "Properties";
            // 
            // aliasTab
            // 
            aliasTab.BackColor = SystemColors.Control;
            aliasTab.Location = new Point(4, 24);
            aliasTab.Name = "aliasTab";
            aliasTab.Size = new Size(192, 72);
            aliasTab.TabIndex = 2;
            aliasTab.Text = "Aliases";
            // 
            // DomainAttribute_New
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(353, 475);
            Controls.Add(mainLayout);
            Name = "DomainAttribute_New";
            Text = "DomainAttribute_New";
            Load += Form_Load;
            Controls.SetChildIndex(mainLayout, 0);
            mainLayout.ResumeLayout(false);
            mainLayout.PerformLayout();
            detailTabLayout.ResumeLayout(false);
            detailTab.ResumeLayout(false);
            detailsLayout.ResumeLayout(false);
            detailsLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel mainLayout;
        private Controls.TextBoxData titleData;
        private Controls.TextBoxData descriptionData;
        private TabControl detailTabLayout;
        private TabPage detailTab;
        private TabPage propertyTab;
        private BindingSource bindingSource;
        private TableLayoutPanel detailsLayout;
        private TabPage aliasTab;
        private Controls.ComboBoxData typeOfAttributeData;
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
    }
}