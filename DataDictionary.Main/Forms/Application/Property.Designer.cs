namespace DataDictionary.Main.Forms.Application
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
            propertyNavigation = new DataGridView();
            propertyTitleColumn = new DataGridViewTextBoxColumn();
            propertyDescriptionColum = new DataGridViewTextBoxColumn();
            propertyDescriptionData = new Controls.TextBoxData();
            propertyNameData = new Controls.TextBoxData();
            propertyTitleLayout = new TableLayoutPanel();
            propertyTitleData = new Controls.TextBoxData();
            obsoleteData = new CheckBox();
            errorProvider = new ErrorProvider(components);
            bindingSource = new BindingSource(components);
            applicationPropertyLayout = new TableLayoutPanel();
            applicationPropertyLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)propertyNavigation).BeginInit();
            propertyTitleLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource).BeginInit();
            SuspendLayout();
            // 
            // applicationPropertyLayout
            // 
            applicationPropertyLayout.ColumnCount = 1;
            applicationPropertyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            applicationPropertyLayout.Controls.Add(propertyNavigation, 0, 0);
            applicationPropertyLayout.Controls.Add(propertyDescriptionData, 0, 2);
            applicationPropertyLayout.Controls.Add(propertyNameData, 0, 3);
            applicationPropertyLayout.Controls.Add(propertyTitleLayout, 0, 1);
            applicationPropertyLayout.Dock = DockStyle.Fill;
            applicationPropertyLayout.Location = new Point(0, 25);
            applicationPropertyLayout.Name = "applicationPropertyLayout";
            applicationPropertyLayout.RowCount = 4;
            applicationPropertyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 66.6666641F));
            applicationPropertyLayout.RowStyles.Add(new RowStyle());
            applicationPropertyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333359F));
            applicationPropertyLayout.RowStyles.Add(new RowStyle());
            applicationPropertyLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            applicationPropertyLayout.Size = new Size(393, 425);
            applicationPropertyLayout.TabIndex = 1;
            // 
            // propertyNavigation
            // 
            propertyNavigation.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            propertyNavigation.Columns.AddRange(new DataGridViewColumn[] { propertyTitleColumn, propertyDescriptionColum });
            propertyNavigation.Dock = DockStyle.Fill;
            propertyNavigation.Location = new Point(3, 3);
            propertyNavigation.Name = "propertyNavigation";
            propertyNavigation.RowTemplate.Height = 25;
            propertyNavigation.Size = new Size(387, 206);
            propertyNavigation.TabIndex = 3;
            propertyNavigation.TabStop = false;
            propertyNavigation.DataError += PropertyNavigation_DataError;
            propertyNavigation.RowValidating += PropertyNavigation_RowValidating;
            // 
            // propertyTitleColumn
            // 
            propertyTitleColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            propertyTitleColumn.DataPropertyName = "PropertyTitle";
            propertyTitleColumn.HeaderText = "Property Title";
            propertyTitleColumn.Name = "propertyTitleColumn";
            propertyTitleColumn.Width = 94;
            // 
            // propertyDescriptionColum
            // 
            propertyDescriptionColum.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            propertyDescriptionColum.DataPropertyName = "PropertyDescription";
            propertyDescriptionColum.HeaderText = "Property Description";
            propertyDescriptionColum.Name = "propertyDescriptionColum";
            // 
            // propertyDescriptionData
            // 
            propertyDescriptionData.AutoSize = true;
            propertyDescriptionData.Dock = DockStyle.Fill;
            propertyDescriptionData.HeaderText = "Property Description";
            propertyDescriptionData.Location = new Point(3, 271);
            propertyDescriptionData.Multiline = true;
            propertyDescriptionData.Name = "propertyDescriptionData";
            propertyDescriptionData.ReadOnly = false;
            propertyDescriptionData.Size = new Size(387, 100);
            propertyDescriptionData.TabIndex = 1;
            // 
            // propertyNameData
            // 
            propertyNameData.AutoSize = true;
            propertyNameData.Dock = DockStyle.Fill;
            propertyNameData.HeaderText = "Extended Property Name (see MS SQL Extended Properties)";
            propertyNameData.Location = new Point(3, 377);
            propertyNameData.Multiline = false;
            propertyNameData.Name = "propertyNameData";
            propertyNameData.ReadOnly = false;
            propertyNameData.Size = new Size(387, 45);
            propertyNameData.TabIndex = 2;
            // 
            // propertyTitleLayout
            // 
            propertyTitleLayout.AutoSize = true;
            propertyTitleLayout.ColumnCount = 2;
            propertyTitleLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            propertyTitleLayout.ColumnStyles.Add(new ColumnStyle());
            propertyTitleLayout.Controls.Add(propertyTitleData, 0, 0);
            propertyTitleLayout.Controls.Add(obsoleteData, 1, 0);
            propertyTitleLayout.Dock = DockStyle.Fill;
            propertyTitleLayout.Location = new Point(3, 215);
            propertyTitleLayout.Name = "propertyTitleLayout";
            propertyTitleLayout.RowCount = 1;
            propertyTitleLayout.RowStyles.Add(new RowStyle());
            propertyTitleLayout.Size = new Size(387, 50);
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
            propertyTitleData.Size = new Size(302, 44);
            propertyTitleData.TabIndex = 0;
            propertyTitleData.Validated += PropertyTitleData_Validated;
            propertyTitleData.Validating += PropertyTitleData_Validating;
            // 
            // obsoleteData
            // 
            obsoleteData.AutoSize = true;
            obsoleteData.Location = new Point(311, 3);
            obsoleteData.Name = "obsoleteData";
            obsoleteData.Size = new Size(73, 19);
            obsoleteData.TabIndex = 2;
            obsoleteData.Text = "Obsolete";
            obsoleteData.UseVisualStyleBackColor = true;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // bindingSource
            // 
            bindingSource.AddingNew += bindingSource_AddingNew;
            // 
            // Property
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(393, 450);
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
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView propertyNavigation;
        private Controls.TextBoxData propertyTitleData;
        private Controls.TextBoxData propertyDescriptionData;
        private Controls.TextBoxData propertyNameData;
        private DataGridViewTextBoxColumn propertyTitleColumn;
        private DataGridViewTextBoxColumn propertyDescriptionColum;
        private TableLayoutPanel propertyTitleLayout;
        private CheckBox obsoleteData;
        private ErrorProvider errorProvider;
        private BindingSource bindingSource;
    }
}