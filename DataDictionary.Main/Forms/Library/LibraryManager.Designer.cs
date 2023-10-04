namespace DataDictionary.Main.Forms.Library
{
    partial class LibraryManager
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
            TableLayoutPanel libraryManagerLayout;
            TableLayoutPanel libraryManagerLayoutCheckBoxs;
            sourceFileDate = new Controls.TextBoxData();
            libraryNavigation = new DataGridView();
            libraryTitleColumn = new DataGridViewTextBoxColumn();
            libraryDescriptionColumn = new DataGridViewTextBoxColumn();
            inModelColumn = new DataGridViewCheckBoxColumn();
            inDatabaseColumn = new DataGridViewCheckBoxColumn();
            libraryTitleData = new Controls.TextBoxData();
            libraryDescriptionData = new Controls.TextBoxData();
            asseblyNameData = new Controls.TextBoxData();
            sourceFileNameData = new Controls.TextBoxData();
            inModelData = new CheckBox();
            inDatabaseData = new CheckBox();
            libraryBinding = new BindingSource(components);
            errorProvider = new ErrorProvider(components);
            openFileDialog = new OpenFileDialog();
            libraryManagerLayout = new TableLayoutPanel();
            libraryManagerLayoutCheckBoxs = new TableLayoutPanel();
            libraryManagerLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)libraryNavigation).BeginInit();
            libraryManagerLayoutCheckBoxs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)libraryBinding).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // libraryManagerLayout
            // 
            libraryManagerLayout.ColumnCount = 2;
            libraryManagerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            libraryManagerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            libraryManagerLayout.Controls.Add(sourceFileDate, 1, 4);
            libraryManagerLayout.Controls.Add(libraryNavigation, 0, 0);
            libraryManagerLayout.Controls.Add(libraryTitleData, 0, 1);
            libraryManagerLayout.Controls.Add(libraryDescriptionData, 0, 2);
            libraryManagerLayout.Controls.Add(asseblyNameData, 0, 3);
            libraryManagerLayout.Controls.Add(sourceFileNameData, 0, 4);
            libraryManagerLayout.Controls.Add(libraryManagerLayoutCheckBoxs, 1, 3);
            libraryManagerLayout.Dock = DockStyle.Fill;
            libraryManagerLayout.Location = new Point(0, 25);
            libraryManagerLayout.Name = "libraryManagerLayout";
            libraryManagerLayout.RowCount = 5;
            libraryManagerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 75.0000153F));
            libraryManagerLayout.RowStyles.Add(new RowStyle());
            libraryManagerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 24.9999828F));
            libraryManagerLayout.RowStyles.Add(new RowStyle());
            libraryManagerLayout.RowStyles.Add(new RowStyle());
            libraryManagerLayout.Size = new Size(566, 598);
            libraryManagerLayout.TabIndex = 1;
            // 
            // sourceFileDate
            // 
            sourceFileDate.AutoSize = true;
            sourceFileDate.Dock = DockStyle.Fill;
            sourceFileDate.HeaderText = "Source File Date";
            sourceFileDate.Location = new Point(399, 550);
            sourceFileDate.Multiline = false;
            sourceFileDate.Name = "sourceFileDate";
            sourceFileDate.ReadOnly = true;
            sourceFileDate.Size = new Size(164, 45);
            sourceFileDate.TabIndex = 7;
            // 
            // libraryNavigation
            // 
            libraryNavigation.AllowUserToAddRows = false;
            libraryNavigation.AllowUserToDeleteRows = false;
            libraryNavigation.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            libraryNavigation.Columns.AddRange(new DataGridViewColumn[] { libraryTitleColumn, libraryDescriptionColumn, inModelColumn, inDatabaseColumn });
            libraryManagerLayout.SetColumnSpan(libraryNavigation, 2);
            libraryNavigation.Dock = DockStyle.Fill;
            libraryNavigation.Location = new Point(3, 3);
            libraryNavigation.Name = "libraryNavigation";
            libraryNavigation.RowTemplate.Height = 25;
            libraryNavigation.Size = new Size(560, 325);
            libraryNavigation.TabIndex = 0;
            // 
            // libraryTitleColumn
            // 
            libraryTitleColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            libraryTitleColumn.DataPropertyName = "LibraryTitle";
            libraryTitleColumn.FillWeight = 40F;
            libraryTitleColumn.HeaderText = "Title";
            libraryTitleColumn.Name = "libraryTitleColumn";
            // 
            // libraryDescriptionColumn
            // 
            libraryDescriptionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            libraryDescriptionColumn.DataPropertyName = "LibraryDescription";
            libraryDescriptionColumn.FillWeight = 60F;
            libraryDescriptionColumn.HeaderText = "Description";
            libraryDescriptionColumn.Name = "libraryDescriptionColumn";
            // 
            // inModelColumn
            // 
            inModelColumn.DataPropertyName = "InModel";
            inModelColumn.HeaderText = "In Model";
            inModelColumn.Name = "inModelColumn";
            // 
            // inDatabaseColumn
            // 
            inDatabaseColumn.DataPropertyName = "InDatabase";
            inDatabaseColumn.HeaderText = "In Database";
            inDatabaseColumn.Name = "inDatabaseColumn";
            // 
            // libraryTitleData
            // 
            libraryTitleData.AutoSize = true;
            libraryManagerLayout.SetColumnSpan(libraryTitleData, 2);
            libraryTitleData.Dock = DockStyle.Fill;
            libraryTitleData.HeaderText = "Library Title";
            libraryTitleData.Location = new Point(3, 334);
            libraryTitleData.Multiline = false;
            libraryTitleData.Name = "libraryTitleData";
            libraryTitleData.ReadOnly = false;
            libraryTitleData.Size = new Size(560, 44);
            libraryTitleData.TabIndex = 1;
            libraryTitleData.Validating += libraryTitleData_Validating;
            // 
            // libraryDescriptionData
            // 
            libraryDescriptionData.AutoSize = true;
            libraryManagerLayout.SetColumnSpan(libraryDescriptionData, 2);
            libraryDescriptionData.Dock = DockStyle.Fill;
            libraryDescriptionData.HeaderText = "Library Description";
            libraryDescriptionData.Location = new Point(3, 384);
            libraryDescriptionData.Multiline = true;
            libraryDescriptionData.Name = "libraryDescriptionData";
            libraryDescriptionData.ReadOnly = false;
            libraryDescriptionData.Size = new Size(560, 104);
            libraryDescriptionData.TabIndex = 2;
            // 
            // asseblyNameData
            // 
            asseblyNameData.AutoSize = true;
            asseblyNameData.Dock = DockStyle.Fill;
            asseblyNameData.HeaderText = "Assembly Name";
            asseblyNameData.Location = new Point(3, 494);
            asseblyNameData.Multiline = false;
            asseblyNameData.Name = "asseblyNameData";
            asseblyNameData.ReadOnly = true;
            asseblyNameData.Size = new Size(390, 50);
            asseblyNameData.TabIndex = 3;
            asseblyNameData.Validating += asseblyNameData_Validating;
            // 
            // sourceFileNameData
            // 
            sourceFileNameData.AutoSize = true;
            sourceFileNameData.Dock = DockStyle.Fill;
            sourceFileNameData.HeaderText = "Source File";
            sourceFileNameData.Location = new Point(3, 550);
            sourceFileNameData.Multiline = false;
            sourceFileNameData.Name = "sourceFileNameData";
            sourceFileNameData.ReadOnly = true;
            sourceFileNameData.Size = new Size(390, 45);
            sourceFileNameData.TabIndex = 4;
            // 
            // libraryManagerLayoutCheckBoxs
            // 
            libraryManagerLayoutCheckBoxs.AutoSize = true;
            libraryManagerLayoutCheckBoxs.ColumnCount = 1;
            libraryManagerLayoutCheckBoxs.ColumnStyles.Add(new ColumnStyle());
            libraryManagerLayoutCheckBoxs.Controls.Add(inModelData, 0, 0);
            libraryManagerLayoutCheckBoxs.Controls.Add(inDatabaseData, 0, 1);
            libraryManagerLayoutCheckBoxs.Dock = DockStyle.Fill;
            libraryManagerLayoutCheckBoxs.Location = new Point(399, 494);
            libraryManagerLayoutCheckBoxs.Name = "libraryManagerLayoutCheckBoxs";
            libraryManagerLayoutCheckBoxs.RowCount = 2;
            libraryManagerLayoutCheckBoxs.RowStyles.Add(new RowStyle());
            libraryManagerLayoutCheckBoxs.RowStyles.Add(new RowStyle());
            libraryManagerLayoutCheckBoxs.Size = new Size(164, 50);
            libraryManagerLayoutCheckBoxs.TabIndex = 8;
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
            // libraryBinding
            // 
            libraryBinding.BindingComplete += libraryBinding_BindingComplete;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // openFileDialog
            // 
            openFileDialog.FileName = "openFileDialog";
            // 
            // LibraryManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(566, 623);
            Controls.Add(libraryManagerLayout);
            Name = "LibraryManager";
            Text = "LibraryManager";
            Load += LibraryManager_Load;
            Controls.SetChildIndex(libraryManagerLayout, 0);
            libraryManagerLayout.ResumeLayout(false);
            libraryManagerLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)libraryNavigation).EndInit();
            libraryManagerLayoutCheckBoxs.ResumeLayout(false);
            libraryManagerLayoutCheckBoxs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)libraryBinding).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView libraryNavigation;
        private Controls.TextBoxData libraryTitleData;
        private Controls.TextBoxData libraryDescriptionData;
        private Controls.TextBoxData asseblyNameData;
        private Controls.TextBoxData sourceFileNameData;
        private Controls.TextBoxData sourceFileDate;
        private BindingSource libraryBinding;
        private ErrorProvider errorProvider;
        private CheckBox inModelData;
        private CheckBox inDatabaseData;
        private OpenFileDialog openFileDialog;
        private DataGridViewTextBoxColumn libraryTitleColumn;
        private DataGridViewTextBoxColumn libraryDescriptionColumn;
        private DataGridViewCheckBoxColumn inModelColumn;
        private DataGridViewCheckBoxColumn inDatabaseColumn;
    }
}