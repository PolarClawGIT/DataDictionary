namespace DataDictionary.Main.Forms
{
    partial class ApplicationProperty
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
            TableLayoutPanel applicationPropertyLayout;
            applicationPropertyNavigation = new DataGridView();
            propertyTitleColumn = new DataGridViewTextBoxColumn();
            propertyDescriptionColum = new DataGridViewTextBoxColumn();
            propertyTitleData = new Controls.TextBoxData();
            propertyDescriptionData = new Controls.TextBoxData();
            propertyNameData = new Controls.TextBoxData();
            applicationPropertyLayout = new TableLayoutPanel();
            applicationPropertyLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)applicationPropertyNavigation).BeginInit();
            SuspendLayout();
            // 
            // applicationPropertyLayout
            // 
            applicationPropertyLayout.ColumnCount = 1;
            applicationPropertyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            applicationPropertyLayout.Controls.Add(applicationPropertyNavigation, 0, 0);
            applicationPropertyLayout.Controls.Add(propertyTitleData, 0, 1);
            applicationPropertyLayout.Controls.Add(propertyDescriptionData, 0, 2);
            applicationPropertyLayout.Controls.Add(propertyNameData, 0, 3);
            applicationPropertyLayout.Dock = DockStyle.Fill;
            applicationPropertyLayout.Location = new Point(0, 0);
            applicationPropertyLayout.Name = "applicationPropertyLayout";
            applicationPropertyLayout.RowCount = 4;
            applicationPropertyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 66.6666641F));
            applicationPropertyLayout.RowStyles.Add(new RowStyle());
            applicationPropertyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            applicationPropertyLayout.RowStyles.Add(new RowStyle());
            applicationPropertyLayout.Size = new Size(424, 450);
            applicationPropertyLayout.TabIndex = 0;
            // 
            // applicationPropertyNavigation
            // 
            applicationPropertyNavigation.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            applicationPropertyNavigation.Columns.AddRange(new DataGridViewColumn[] { propertyTitleColumn, propertyDescriptionColum });
            applicationPropertyNavigation.Dock = DockStyle.Fill;
            applicationPropertyNavigation.Location = new Point(3, 3);
            applicationPropertyNavigation.MultiSelect = false;
            applicationPropertyNavigation.Name = "applicationPropertyNavigation";
            applicationPropertyNavigation.RowTemplate.Height = 25;
            applicationPropertyNavigation.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            applicationPropertyNavigation.Size = new Size(418, 227);
            applicationPropertyNavigation.TabIndex = 0;
            applicationPropertyNavigation.SelectionChanged += applicationPropertyNavigation_SelectionChanged;
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
            // propertyTitleData
            // 
            propertyTitleData.AutoSize = true;
            propertyTitleData.Dock = DockStyle.Fill;
            propertyTitleData.HeaderText = "Property Title";
            propertyTitleData.Location = new Point(3, 236);
            propertyTitleData.Multiline = false;
            propertyTitleData.Name = "propertyTitleData";
            propertyTitleData.ReadOnly = false;
            propertyTitleData.Size = new Size(418, 44);
            propertyTitleData.TabIndex = 1;
            // 
            // propertyDescriptionData
            // 
            propertyDescriptionData.AutoSize = true;
            propertyDescriptionData.Dock = DockStyle.Fill;
            propertyDescriptionData.HeaderText = "Property Description";
            propertyDescriptionData.Location = new Point(3, 286);
            propertyDescriptionData.Multiline = true;
            propertyDescriptionData.Name = "propertyDescriptionData";
            propertyDescriptionData.ReadOnly = false;
            propertyDescriptionData.Size = new Size(418, 110);
            propertyDescriptionData.TabIndex = 2;
            // 
            // propertyNameData
            // 
            propertyNameData.AutoSize = true;
            propertyNameData.Dock = DockStyle.Fill;
            propertyNameData.HeaderText = "Extended Property Name (see MS SQL Extended Properties)";
            propertyNameData.Location = new Point(3, 402);
            propertyNameData.Multiline = false;
            propertyNameData.Name = "propertyNameData";
            propertyNameData.ReadOnly = false;
            propertyNameData.Size = new Size(418, 45);
            propertyNameData.TabIndex = 3;
            // 
            // ApplicationProperty
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(424, 450);
            Controls.Add(applicationPropertyLayout);
            Name = "ApplicationProperty";
            Text = "ApplicationProperty";
            Load += ApplicationProperty_Load;
            applicationPropertyLayout.ResumeLayout(false);
            applicationPropertyLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)applicationPropertyNavigation).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView applicationPropertyNavigation;
        private Controls.TextBoxData propertyTitleData;
        private Controls.TextBoxData propertyDescriptionData;
        private Controls.TextBoxData propertyNameData;
        private DataGridViewTextBoxColumn propertyTitleColumn;
        private DataGridViewTextBoxColumn propertyDescriptionColum;
    }
}