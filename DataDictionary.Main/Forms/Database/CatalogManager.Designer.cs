﻿namespace DataDictionary.Main.Forms.Database
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
            catalogNavigation = new DataGridView();
            catalogTitleColumn = new DataGridViewTextBoxColumn();
            databaseNameColumn = new DataGridViewTextBoxColumn();
            inModelColumn = new DataGridViewCheckBoxColumn();
            inDatabase = new DataGridViewCheckBoxColumn();
            catalogTitleData = new Controls.TextBoxData();
            catalogDescriptionData = new Controls.TextBoxData();
            sourceDateData = new Controls.TextBoxData();
            sourceServerNameData = new Controls.TextBoxData();
            sourceDatabaseNameData = new Controls.TextBoxData();
            catalogBinding = new BindingSource(components);
            catalogManagerLayout = new TableLayoutPanel();
            catalogManagerLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)catalogNavigation).BeginInit();
            ((System.ComponentModel.ISupportInitialize)catalogBinding).BeginInit();
            SuspendLayout();
            // 
            // catalogManagerLayout
            // 
            catalogManagerLayout.ColumnCount = 2;
            catalogManagerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.6666641F));
            catalogManagerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            catalogManagerLayout.Controls.Add(catalogNavigation, 0, 0);
            catalogManagerLayout.Controls.Add(catalogTitleData, 0, 1);
            catalogManagerLayout.Controls.Add(catalogDescriptionData, 0, 2);
            catalogManagerLayout.Controls.Add(sourceDateData, 1, 4);
            catalogManagerLayout.Controls.Add(sourceServerNameData, 0, 3);
            catalogManagerLayout.Controls.Add(sourceDatabaseNameData, 0, 4);
            catalogManagerLayout.Dock = DockStyle.Fill;
            catalogManagerLayout.Location = new Point(0, 25);
            catalogManagerLayout.Name = "catalogManagerLayout";
            catalogManagerLayout.RowCount = 5;
            catalogManagerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            catalogManagerLayout.RowStyles.Add(new RowStyle());
            catalogManagerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            catalogManagerLayout.RowStyles.Add(new RowStyle());
            catalogManagerLayout.RowStyles.Add(new RowStyle());
            catalogManagerLayout.Size = new Size(611, 490);
            catalogManagerLayout.TabIndex = 1;
            // 
            // catalogNavigation
            // 
            catalogNavigation.AllowUserToAddRows = false;
            catalogNavigation.AllowUserToDeleteRows = false;
            catalogNavigation.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            catalogNavigation.Columns.AddRange(new DataGridViewColumn[] { catalogTitleColumn, databaseNameColumn, inModelColumn, inDatabase });
            catalogManagerLayout.SetColumnSpan(catalogNavigation, 2);
            catalogNavigation.Dock = DockStyle.Fill;
            catalogNavigation.Location = new Point(3, 3);
            catalogNavigation.Name = "catalogNavigation";
            catalogNavigation.Size = new Size(605, 232);
            catalogNavigation.TabIndex = 0;
            // 
            // catalogTitleColumn
            // 
            catalogTitleColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            catalogTitleColumn.DataPropertyName = "CatalogTitle";
            catalogTitleColumn.HeaderText = "Catalog Title";
            catalogTitleColumn.MinimumWidth = 150;
            catalogTitleColumn.Name = "catalogTitleColumn";
            // 
            // databaseNameColumn
            // 
            databaseNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            databaseNameColumn.DataPropertyName = "DatabaseName";
            databaseNameColumn.HeaderText = "Database Name";
            databaseNameColumn.MinimumWidth = 150;
            databaseNameColumn.Name = "databaseNameColumn";
            databaseNameColumn.ReadOnly = true;
            // 
            // inModelColumn
            // 
            inModelColumn.DataPropertyName = "InModel";
            inModelColumn.HeaderText = "In Model";
            inModelColumn.Name = "inModelColumn";
            inModelColumn.ReadOnly = true;
            // 
            // inDatabase
            // 
            inDatabase.DataPropertyName = "InDatabase";
            inDatabase.HeaderText = "In Database";
            inDatabase.Name = "inDatabase";
            inDatabase.ReadOnly = true;
            // 
            // catalogTitleData
            // 
            catalogTitleData.AutoSize = true;
            catalogManagerLayout.SetColumnSpan(catalogTitleData, 2);
            catalogTitleData.Dock = DockStyle.Fill;
            catalogTitleData.HeaderText = "Catalog Title";
            catalogTitleData.Location = new Point(3, 241);
            catalogTitleData.Multiline = false;
            catalogTitleData.Name = "catalogTitleData";
            catalogTitleData.ReadOnly = false;
            catalogTitleData.Size = new Size(605, 44);
            catalogTitleData.TabIndex = 1;
            catalogTitleData.WordWrap = true;
            // 
            // catalogDescriptionData
            // 
            catalogDescriptionData.AutoSize = true;
            catalogManagerLayout.SetColumnSpan(catalogDescriptionData, 2);
            catalogDescriptionData.Dock = DockStyle.Fill;
            catalogDescriptionData.HeaderText = "Catalog Description";
            catalogDescriptionData.Location = new Point(3, 291);
            catalogDescriptionData.Multiline = true;
            catalogDescriptionData.Name = "catalogDescriptionData";
            catalogDescriptionData.ReadOnly = false;
            catalogDescriptionData.Size = new Size(605, 96);
            catalogDescriptionData.TabIndex = 2;
            catalogDescriptionData.WordWrap = true;
            // 
            // sourceDateData
            // 
            sourceDateData.AutoSize = true;
            sourceDateData.Dock = DockStyle.Fill;
            sourceDateData.HeaderText = "Source Date";
            sourceDateData.Location = new Point(410, 443);
            sourceDateData.Multiline = false;
            sourceDateData.Name = "sourceDateData";
            sourceDateData.ReadOnly = true;
            sourceDateData.Size = new Size(198, 44);
            sourceDateData.TabIndex = 10;
            sourceDateData.WordWrap = true;
            // 
            // sourceServerNameData
            // 
            sourceServerNameData.AutoSize = true;
            sourceServerNameData.Dock = DockStyle.Fill;
            sourceServerNameData.HeaderText = "Source Server Name";
            sourceServerNameData.Location = new Point(3, 393);
            sourceServerNameData.Multiline = false;
            sourceServerNameData.Name = "sourceServerNameData";
            sourceServerNameData.ReadOnly = true;
            sourceServerNameData.Size = new Size(401, 44);
            sourceServerNameData.TabIndex = 11;
            sourceServerNameData.WordWrap = true;
            // 
            // sourceDatabaseNameData
            // 
            sourceDatabaseNameData.AutoSize = true;
            sourceDatabaseNameData.Dock = DockStyle.Fill;
            sourceDatabaseNameData.HeaderText = "Source Database Name";
            sourceDatabaseNameData.Location = new Point(3, 443);
            sourceDatabaseNameData.Multiline = false;
            sourceDatabaseNameData.Name = "sourceDatabaseNameData";
            sourceDatabaseNameData.ReadOnly = true;
            sourceDatabaseNameData.Size = new Size(401, 44);
            sourceDatabaseNameData.TabIndex = 12;
            sourceDatabaseNameData.WordWrap = true;
            // 
            // catalogBinding
            // 
            catalogBinding.CurrentChanged += CatalogBinding_CurrentChanged;
            // 
            // CatalogManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(611, 515);
            Controls.Add(catalogManagerLayout);
            Name = "CatalogManager";
            Text = "Catalog Manager";
            Load += CatalogManager_Load;
            Controls.SetChildIndex(catalogManagerLayout, 0);
            catalogManagerLayout.ResumeLayout(false);
            catalogManagerLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)catalogNavigation).EndInit();
            ((System.ComponentModel.ISupportInitialize)catalogBinding).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView catalogNavigation;
        private BindingSource catalogBinding;
        private Controls.TextBoxData catalogTitleData;
        private Controls.TextBoxData catalogDescriptionData;
        private Controls.TextBoxData sourceDateData;
        private Controls.TextBoxData sourceServerNameData;
        private Controls.TextBoxData sourceDatabaseNameData;
        private DataGridViewTextBoxColumn catalogTitleColumn;
        private DataGridViewTextBoxColumn databaseNameColumn;
        private DataGridViewCheckBoxColumn inModelColumn;
        private DataGridViewCheckBoxColumn inDatabase;
    }
}