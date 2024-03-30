namespace DataDictionary.Main.Forms.ApplicationWide
{
    partial class Property
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
            TableLayoutPanel applicationPropertyLayout;
            TableLayoutPanel subTypeFlagsLayout;
            propertyNavigation = new DataGridView();
            propertyTitleColumn = new DataGridViewTextBoxColumn();
            propertyDescriptionColum = new DataGridViewTextBoxColumn();
            propertyTitleLayout = new TableLayoutPanel();
            propertyTitleData = new Controls.TextBoxData();
            isChoiceData = new CheckBox();
            isDefinitionData = new CheckBox();
            isExtendedPropertyData = new CheckBox();
            isFrameworkSummaryData = new CheckBox();
            choicesHeader = new Label();
            choiceData = new DataGridView();
            choiceColumn = new DataGridViewTextBoxColumn();
            extendedPropertyData = new Controls.TextBoxData();
            propertyDescriptionData = new Controls.TextBoxData();
            errorProvider = new ErrorProvider(components);
            bindingSource = new BindingSource(components);
            propertyToolStrip = new ContextMenuStrip(components);
            addPropertyComand = new ToolStripMenuItem();
            applicationPropertyLayout = new TableLayoutPanel();
            subTypeFlagsLayout = new TableLayoutPanel();
            applicationPropertyLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)propertyNavigation).BeginInit();
            propertyTitleLayout.SuspendLayout();
            subTypeFlagsLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)choiceData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource).BeginInit();
            propertyToolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // applicationPropertyLayout
            // 
            applicationPropertyLayout.ColumnCount = 1;
            applicationPropertyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            applicationPropertyLayout.Controls.Add(propertyNavigation, 0, 0);
            applicationPropertyLayout.Controls.Add(propertyTitleLayout, 0, 1);
            applicationPropertyLayout.Dock = DockStyle.Fill;
            applicationPropertyLayout.Location = new Point(0, 25);
            applicationPropertyLayout.Name = "applicationPropertyLayout";
            applicationPropertyLayout.RowCount = 2;
            applicationPropertyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            applicationPropertyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            applicationPropertyLayout.Size = new Size(703, 510);
            applicationPropertyLayout.TabIndex = 1;
            // 
            // propertyNavigation
            // 
            propertyNavigation.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            propertyNavigation.Columns.AddRange(new DataGridViewColumn[] { propertyTitleColumn, propertyDescriptionColum });
            propertyNavigation.Dock = DockStyle.Fill;
            propertyNavigation.Location = new Point(3, 3);
            propertyNavigation.Name = "propertyNavigation";
            propertyNavigation.Size = new Size(697, 198);
            propertyNavigation.TabIndex = 3;
            propertyNavigation.TabStop = false;
            propertyNavigation.DataError += PropertyNavigation_DataError;
            propertyNavigation.RowValidating += PropertyNavigation_RowValidating;
            propertyNavigation.Leave += PropertyNavigation_Leave;
            // 
            // propertyTitleColumn
            // 
            propertyTitleColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            propertyTitleColumn.DataPropertyName = "PropertyTitle";
            propertyTitleColumn.FillWeight = 30F;
            propertyTitleColumn.HeaderText = "Property Title";
            propertyTitleColumn.Name = "propertyTitleColumn";
            // 
            // propertyDescriptionColum
            // 
            propertyDescriptionColum.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            propertyDescriptionColum.DataPropertyName = "PropertyDescription";
            propertyDescriptionColum.FillWeight = 70F;
            propertyDescriptionColum.HeaderText = "Property Description";
            propertyDescriptionColum.Name = "propertyDescriptionColum";
            // 
            // propertyTitleLayout
            // 
            propertyTitleLayout.AutoSize = true;
            propertyTitleLayout.ColumnCount = 2;
            propertyTitleLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            propertyTitleLayout.ColumnStyles.Add(new ColumnStyle());
            propertyTitleLayout.Controls.Add(propertyTitleData, 0, 0);
            propertyTitleLayout.Controls.Add(subTypeFlagsLayout, 1, 0);
            propertyTitleLayout.Controls.Add(extendedPropertyData, 0, 2);
            propertyTitleLayout.Controls.Add(propertyDescriptionData, 0, 1);
            propertyTitleLayout.Dock = DockStyle.Fill;
            propertyTitleLayout.Location = new Point(3, 207);
            propertyTitleLayout.Name = "propertyTitleLayout";
            propertyTitleLayout.RowCount = 3;
            propertyTitleLayout.RowStyles.Add(new RowStyle());
            propertyTitleLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            propertyTitleLayout.RowStyles.Add(new RowStyle());
            propertyTitleLayout.Size = new Size(697, 300);
            propertyTitleLayout.TabIndex = 0;
            // 
            // propertyTitleData
            // 
            propertyTitleData.AutoSize = true;
            propertyTitleData.Dock = DockStyle.Fill;
            propertyTitleData.HeaderText = "Property Title";
            propertyTitleData.Location = new Point(3, 3);
            propertyTitleData.Multiline = false;
            propertyTitleData.Name = "propertyTitleData";
            propertyTitleData.ReadOnly = false;
            propertyTitleData.Size = new Size(439, 44);
            propertyTitleData.TabIndex = 0;
            propertyTitleData.Validated += PropertyTitleData_Validated;
            propertyTitleData.Validating += PropertyTitleData_Validating;
            // 
            // subTypeFlagsLayout
            // 
            subTypeFlagsLayout.AutoSize = true;
            subTypeFlagsLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            subTypeFlagsLayout.ColumnCount = 1;
            subTypeFlagsLayout.ColumnStyles.Add(new ColumnStyle());
            subTypeFlagsLayout.Controls.Add(isChoiceData, 0, 3);
            subTypeFlagsLayout.Controls.Add(isDefinitionData, 0, 0);
            subTypeFlagsLayout.Controls.Add(isExtendedPropertyData, 0, 1);
            subTypeFlagsLayout.Controls.Add(isFrameworkSummaryData, 0, 2);
            subTypeFlagsLayout.Controls.Add(choicesHeader, 0, 5);
            subTypeFlagsLayout.Controls.Add(choiceData, 0, 6);
            subTypeFlagsLayout.Dock = DockStyle.Fill;
            subTypeFlagsLayout.Location = new Point(448, 3);
            subTypeFlagsLayout.Name = "subTypeFlagsLayout";
            subTypeFlagsLayout.RowCount = 7;
            propertyTitleLayout.SetRowSpan(subTypeFlagsLayout, 3);
            subTypeFlagsLayout.RowStyles.Add(new RowStyle());
            subTypeFlagsLayout.RowStyles.Add(new RowStyle());
            subTypeFlagsLayout.RowStyles.Add(new RowStyle());
            subTypeFlagsLayout.RowStyles.Add(new RowStyle());
            subTypeFlagsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            subTypeFlagsLayout.RowStyles.Add(new RowStyle());
            subTypeFlagsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            subTypeFlagsLayout.Size = new Size(246, 294);
            subTypeFlagsLayout.TabIndex = 4;
            // 
            // isChoiceData
            // 
            isChoiceData.AutoSize = true;
            isChoiceData.Location = new Point(3, 78);
            isChoiceData.Name = "isChoiceData";
            isChoiceData.Size = new Size(63, 19);
            isChoiceData.TabIndex = 3;
            isChoiceData.Text = "Choice";
            isChoiceData.UseVisualStyleBackColor = true;
            isChoiceData.CheckedChanged += IsChoiceData_CheckedChanged;
            // 
            // isDefinitionData
            // 
            isDefinitionData.AutoSize = true;
            isDefinitionData.Location = new Point(3, 3);
            isDefinitionData.Name = "isDefinitionData";
            isDefinitionData.Size = new Size(78, 19);
            isDefinitionData.TabIndex = 4;
            isDefinitionData.Text = "Definition";
            isDefinitionData.UseVisualStyleBackColor = true;
            // 
            // isExtendedPropertyData
            // 
            isExtendedPropertyData.AutoSize = true;
            isExtendedPropertyData.Location = new Point(3, 28);
            isExtendedPropertyData.Name = "isExtendedPropertyData";
            isExtendedPropertyData.Size = new Size(123, 19);
            isExtendedPropertyData.TabIndex = 5;
            isExtendedPropertyData.Text = "Extended Property";
            isExtendedPropertyData.UseVisualStyleBackColor = true;
            isExtendedPropertyData.CheckedChanged += IsExtendedPropertyData_CheckedChanged;
            // 
            // isFrameworkSummaryData
            // 
            isFrameworkSummaryData.AutoSize = true;
            isFrameworkSummaryData.Location = new Point(3, 53);
            isFrameworkSummaryData.Name = "isFrameworkSummaryData";
            isFrameworkSummaryData.Size = new Size(139, 19);
            isFrameworkSummaryData.TabIndex = 6;
            isFrameworkSummaryData.Text = "Framework Summary";
            isFrameworkSummaryData.UseVisualStyleBackColor = true;
            // 
            // choicesHeader
            // 
            choicesHeader.AutoSize = true;
            choicesHeader.Location = new Point(3, 120);
            choicesHeader.Name = "choicesHeader";
            choicesHeader.Size = new Size(49, 15);
            choicesHeader.TabIndex = 5;
            choicesHeader.Text = "Choices";
            // 
            // choiceData
            // 
            choiceData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            choiceData.Columns.AddRange(new DataGridViewColumn[] { choiceColumn });
            choiceData.Dock = DockStyle.Fill;
            choiceData.Location = new Point(3, 138);
            choiceData.Name = "choiceData";
            choiceData.Size = new Size(240, 153);
            choiceData.TabIndex = 4;
            choiceData.CellFormatting += ChoiceData_CellFormatting;
            // 
            // choiceColumn
            // 
            choiceColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            choiceColumn.DataPropertyName = "Choice";
            choiceColumn.HeaderText = "Choice";
            choiceColumn.Name = "choiceColumn";
            // 
            // extendedPropertyData
            // 
            extendedPropertyData.AutoSize = true;
            extendedPropertyData.Dock = DockStyle.Fill;
            extendedPropertyData.HeaderText = "Extended Property Name (see MS SQL Extended Properties)";
            extendedPropertyData.Location = new Point(3, 253);
            extendedPropertyData.Multiline = false;
            extendedPropertyData.Name = "extendedPropertyData";
            extendedPropertyData.ReadOnly = false;
            extendedPropertyData.Size = new Size(439, 44);
            extendedPropertyData.TabIndex = 2;
            // 
            // propertyDescriptionData
            // 
            propertyDescriptionData.AutoSize = true;
            propertyDescriptionData.Dock = DockStyle.Fill;
            propertyDescriptionData.HeaderText = "Property Description";
            propertyDescriptionData.Location = new Point(3, 53);
            propertyDescriptionData.Multiline = true;
            propertyDescriptionData.Name = "propertyDescriptionData";
            propertyDescriptionData.ReadOnly = false;
            propertyDescriptionData.Size = new Size(439, 194);
            propertyDescriptionData.TabIndex = 1;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // bindingSource
            // 
            bindingSource.AddingNew += bindingSource_AddingNew;
            bindingSource.BindingComplete += BindingComplete;
            // 
            // propertyToolStrip
            // 
            propertyToolStrip.Items.AddRange(new ToolStripItem[] { addPropertyComand });
            propertyToolStrip.Name = "propertyToolStrip";
            propertyToolStrip.Size = new Size(143, 26);
            // 
            // addPropertyComand
            // 
            addPropertyComand.Image = Properties.Resources.NewProperty;
            addPropertyComand.Name = "addPropertyComand";
            addPropertyComand.Size = new Size(142, 22);
            addPropertyComand.Text = "add Property";
            addPropertyComand.Click += addPropertyComand_Click;
            // 
            // Property
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(703, 535);
            Controls.Add(applicationPropertyLayout);
            Name = "Property";
            Text = "ApplicationProperty";
            Load += ApplicationProperty_Load;
            Controls.SetChildIndex(applicationPropertyLayout, 0);
            applicationPropertyLayout.ResumeLayout(false);
            applicationPropertyLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)propertyNavigation).EndInit();
            propertyTitleLayout.ResumeLayout(false);
            propertyTitleLayout.PerformLayout();
            subTypeFlagsLayout.ResumeLayout(false);
            subTypeFlagsLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)choiceData).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource).EndInit();
            propertyToolStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView propertyNavigation;
        private Controls.TextBoxData propertyTitleData;
        private Controls.TextBoxData propertyDescriptionData;
        private Controls.TextBoxData extendedPropertyData;
        private TableLayoutPanel propertyTitleLayout;
        private ErrorProvider errorProvider;
        private BindingSource bindingSource;
        private CheckBox isChoiceData;
        private CheckBox isDefinitionData;
        private CheckBox isExtendedPropertyData;
        private Label choicesHeader;
        private DataGridViewTextBoxColumn propertyTitleColumn;
        private DataGridViewTextBoxColumn propertyDescriptionColum;
        private CheckBox isFrameworkSummaryData;
        private DataGridView choiceData;
        private DataGridViewTextBoxColumn choiceColumn;
        private ContextMenuStrip propertyToolStrip;
        private ToolStripMenuItem addPropertyComand;
    }
}