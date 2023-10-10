namespace DataDictionary.Main.Forms.Database
{
    partial class CatalogManager
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
            TableLayoutPanel catalogManagerLayout;
            TableLayoutPanel libraryManagerLayoutCheckBoxs;
            inModelData = new CheckBox();
            inDatabaseData = new CheckBox();
            catalogNavigation = new DataGridView();
            catalogTitleData = new Controls.TextBoxData();
            catalogDescriptionData = new Controls.TextBoxData();
            sourceDateData = new Controls.TextBoxData();
            catalogBinding = new BindingSource(components);
            comboBoxData1 = new Controls.ComboBoxData();
            sourceDatabaseNameData = new Controls.ComboBoxData();
            catalogManagerLayout = new TableLayoutPanel();
            libraryManagerLayoutCheckBoxs = new TableLayoutPanel();
            catalogManagerLayout.SuspendLayout();
            libraryManagerLayoutCheckBoxs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)catalogNavigation).BeginInit();
            ((System.ComponentModel.ISupportInitialize)catalogBinding).BeginInit();
            SuspendLayout();
            // 
            // catalogManagerLayout
            // 
            catalogManagerLayout.ColumnCount = 2;
            catalogManagerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.6666641F));
            catalogManagerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            catalogManagerLayout.Controls.Add(libraryManagerLayoutCheckBoxs, 1, 3);
            catalogManagerLayout.Controls.Add(catalogNavigation, 0, 0);
            catalogManagerLayout.Controls.Add(catalogTitleData, 0, 1);
            catalogManagerLayout.Controls.Add(catalogDescriptionData, 0, 2);
            catalogManagerLayout.Controls.Add(sourceDateData, 1, 4);
            catalogManagerLayout.Controls.Add(comboBoxData1, 0, 3);
            catalogManagerLayout.Controls.Add(sourceDatabaseNameData, 0, 4);
            catalogManagerLayout.Location = new Point(39, 203);
            catalogManagerLayout.Name = "catalogManagerLayout";
            catalogManagerLayout.RowCount = 5;
            catalogManagerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            catalogManagerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            catalogManagerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            catalogManagerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            catalogManagerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            catalogManagerLayout.Size = new Size(384, 306);
            catalogManagerLayout.TabIndex = 1;
            // 
            // libraryManagerLayoutCheckBoxs
            // 
            libraryManagerLayoutCheckBoxs.AutoSize = true;
            libraryManagerLayoutCheckBoxs.ColumnCount = 1;
            libraryManagerLayoutCheckBoxs.ColumnStyles.Add(new ColumnStyle());
            libraryManagerLayoutCheckBoxs.Controls.Add(inModelData, 0, 0);
            libraryManagerLayoutCheckBoxs.Controls.Add(inDatabaseData, 0, 1);
            libraryManagerLayoutCheckBoxs.Dock = DockStyle.Fill;
            libraryManagerLayoutCheckBoxs.Location = new Point(259, 186);
            libraryManagerLayoutCheckBoxs.Name = "libraryManagerLayoutCheckBoxs";
            libraryManagerLayoutCheckBoxs.RowCount = 2;
            libraryManagerLayoutCheckBoxs.RowStyles.Add(new RowStyle());
            libraryManagerLayoutCheckBoxs.RowStyles.Add(new RowStyle());
            libraryManagerLayoutCheckBoxs.Size = new Size(122, 55);
            libraryManagerLayoutCheckBoxs.TabIndex = 9;
            // 
            // inModelData
            // 
            inModelData.AutoSize = true;
            inModelData.Location = new Point(3, 3);
            inModelData.Name = "inModelData";
            inModelData.Size = new Size(73, 19);
            inModelData.TabIndex = 0;
            inModelData.Text = "In Model";
            inModelData.UseVisualStyleBackColor = true;
            // 
            // inDatabaseData
            // 
            inDatabaseData.AutoSize = true;
            inDatabaseData.Location = new Point(3, 28);
            inDatabaseData.Name = "inDatabaseData";
            inDatabaseData.Size = new Size(87, 19);
            inDatabaseData.TabIndex = 1;
            inDatabaseData.Text = "In Database";
            inDatabaseData.UseVisualStyleBackColor = true;
            // 
            // catalogNavigation
            // 
            catalogNavigation.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            catalogManagerLayout.SetColumnSpan(catalogNavigation, 2);
            catalogNavigation.Dock = DockStyle.Fill;
            catalogNavigation.Location = new Point(3, 3);
            catalogNavigation.Name = "catalogNavigation";
            catalogNavigation.RowTemplate.Height = 25;
            catalogNavigation.Size = new Size(378, 55);
            catalogNavigation.TabIndex = 0;
            // 
            // catalogTitleData
            // 
            catalogTitleData.AutoSize = true;
            catalogManagerLayout.SetColumnSpan(catalogTitleData, 2);
            catalogTitleData.Dock = DockStyle.Fill;
            catalogTitleData.HeaderText = "Catalog Title";
            catalogTitleData.Location = new Point(3, 64);
            catalogTitleData.Multiline = false;
            catalogTitleData.Name = "catalogTitleData";
            catalogTitleData.ReadOnly = false;
            catalogTitleData.Size = new Size(378, 55);
            catalogTitleData.TabIndex = 1;
            // 
            // catalogDescriptionData
            // 
            catalogDescriptionData.AutoSize = true;
            catalogManagerLayout.SetColumnSpan(catalogDescriptionData, 2);
            catalogDescriptionData.Dock = DockStyle.Fill;
            catalogDescriptionData.HeaderText = "Catalog Description";
            catalogDescriptionData.Location = new Point(3, 125);
            catalogDescriptionData.Multiline = true;
            catalogDescriptionData.Name = "catalogDescriptionData";
            catalogDescriptionData.ReadOnly = false;
            catalogDescriptionData.Size = new Size(378, 55);
            catalogDescriptionData.TabIndex = 2;
            // 
            // sourceDateData
            // 
            sourceDateData.AutoSize = true;
            sourceDateData.Dock = DockStyle.Fill;
            sourceDateData.HeaderText = "Source Date";
            sourceDateData.Location = new Point(259, 247);
            sourceDateData.Multiline = false;
            sourceDateData.Name = "sourceDateData";
            sourceDateData.ReadOnly = true;
            sourceDateData.Size = new Size(122, 56);
            sourceDateData.TabIndex = 10;
            // 
            // comboBoxData1
            // 
            comboBoxData1.AutoSize = true;
            comboBoxData1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            comboBoxData1.DataSource = null;
            comboBoxData1.DisplayMember = "";
            comboBoxData1.Dock = DockStyle.Fill;
            comboBoxData1.HeaderText = "Source Server Name";
            comboBoxData1.Location = new Point(3, 186);
            comboBoxData1.Name = "comboBoxData1";
            comboBoxData1.ReadOnly = false;
            comboBoxData1.Size = new Size(250, 55);
            comboBoxData1.TabIndex = 11;
            comboBoxData1.ValueMember = "";
            // 
            // sourceDatabaseNameData
            // 
            sourceDatabaseNameData.AutoSize = true;
            sourceDatabaseNameData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            sourceDatabaseNameData.DataSource = null;
            sourceDatabaseNameData.DisplayMember = "";
            sourceDatabaseNameData.Dock = DockStyle.Fill;
            sourceDatabaseNameData.HeaderText = "Source Database Name";
            sourceDatabaseNameData.Location = new Point(3, 247);
            sourceDatabaseNameData.Name = "sourceDatabaseNameData";
            sourceDatabaseNameData.ReadOnly = false;
            sourceDatabaseNameData.Size = new Size(250, 56);
            sourceDatabaseNameData.TabIndex = 12;
            sourceDatabaseNameData.ValueMember = "";
            // 
            // CatalogManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(523, 634);
            Controls.Add(catalogManagerLayout);
            Name = "CatalogManager";
            Text = "Catalog Manager";
            Controls.SetChildIndex(catalogManagerLayout, 0);
            catalogManagerLayout.ResumeLayout(false);
            catalogManagerLayout.PerformLayout();
            libraryManagerLayoutCheckBoxs.ResumeLayout(false);
            libraryManagerLayoutCheckBoxs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)catalogNavigation).EndInit();
            ((System.ComponentModel.ISupportInitialize)catalogBinding).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel catalogManagerLayout;
        private DataGridView catalogNavigation;
        private BindingSource catalogBinding;
        private Controls.TextBoxData catalogTitleData;
        private Controls.TextBoxData catalogDescriptionData;
        private CheckBox inModelData;
        private CheckBox inDatabaseData;
        private Controls.TextBoxData sourceDateData;
        private Controls.ComboBoxData comboBoxData1;
        private Controls.ComboBoxData sourceDatabaseNameData;
    }
}