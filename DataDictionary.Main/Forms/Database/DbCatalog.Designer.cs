namespace DataDictionary.Main.Forms.Database
{
    partial class DbCatalog
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
            catalogTitleData = new Controls.TextBoxData();
            catalogDescriptionData = new Controls.TextBoxData();
            sourceServerNameData = new Controls.TextBoxData();
            sourceDatabaseNameData = new Controls.TextBoxData();
            sourceDateData = new Controls.TextBoxData();
            exportOptions = new ContextMenuStrip(components);
            exportAll = new ToolStripMenuItem();
            exportEntites = new ToolStripMenuItem();
            exportProcesses = new ToolStripMenuItem();
            exportAttributes = new ToolStripMenuItem();
            bindingSource = new BindingSource(components);
            catalogManagerLayout = new TableLayoutPanel();
            catalogManagerLayout.SuspendLayout();
            exportOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource).BeginInit();
            SuspendLayout();
            // 
            // catalogManagerLayout
            // 
            catalogManagerLayout.ColumnCount = 2;
            catalogManagerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.6666641F));
            catalogManagerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            catalogManagerLayout.Controls.Add(catalogTitleData, 0, 0);
            catalogManagerLayout.Controls.Add(catalogDescriptionData, 0, 1);
            catalogManagerLayout.Controls.Add(sourceServerNameData, 0, 2);
            catalogManagerLayout.Controls.Add(sourceDatabaseNameData, 0, 3);
            catalogManagerLayout.Controls.Add(sourceDateData, 1, 3);
            catalogManagerLayout.Dock = DockStyle.Fill;
            catalogManagerLayout.Location = new Point(0, 25);
            catalogManagerLayout.Name = "catalogManagerLayout";
            catalogManagerLayout.RowCount = 4;
            catalogManagerLayout.RowStyles.Add(new RowStyle());
            catalogManagerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            catalogManagerLayout.RowStyles.Add(new RowStyle());
            catalogManagerLayout.RowStyles.Add(new RowStyle());
            catalogManagerLayout.Size = new Size(453, 338);
            catalogManagerLayout.TabIndex = 2;
            // 
            // catalogTitleData
            // 
            catalogTitleData.AutoSize = true;
            catalogManagerLayout.SetColumnSpan(catalogTitleData, 2);
            catalogTitleData.Dock = DockStyle.Fill;
            catalogTitleData.HeaderText = "Catalog Title";
            catalogTitleData.Location = new Point(3, 3);
            catalogTitleData.Multiline = false;
            catalogTitleData.Name = "catalogTitleData";
            catalogTitleData.ReadOnly = false;
            catalogTitleData.Size = new Size(447, 44);
            catalogTitleData.TabIndex = 1;
            catalogTitleData.WordWrap = true;
            // 
            // catalogDescriptionData
            // 
            catalogDescriptionData.AutoSize = true;
            catalogManagerLayout.SetColumnSpan(catalogDescriptionData, 2);
            catalogDescriptionData.Dock = DockStyle.Fill;
            catalogDescriptionData.HeaderText = "Catalog Description";
            catalogDescriptionData.Location = new Point(3, 53);
            catalogDescriptionData.Multiline = true;
            catalogDescriptionData.Name = "catalogDescriptionData";
            catalogDescriptionData.ReadOnly = false;
            catalogDescriptionData.Size = new Size(447, 182);
            catalogDescriptionData.TabIndex = 2;
            catalogDescriptionData.WordWrap = true;
            // 
            // sourceServerNameData
            // 
            sourceServerNameData.AutoSize = true;
            sourceServerNameData.Dock = DockStyle.Fill;
            sourceServerNameData.HeaderText = "Source Server Name";
            sourceServerNameData.Location = new Point(3, 241);
            sourceServerNameData.Multiline = false;
            sourceServerNameData.Name = "sourceServerNameData";
            sourceServerNameData.ReadOnly = true;
            sourceServerNameData.Size = new Size(295, 44);
            sourceServerNameData.TabIndex = 11;
            sourceServerNameData.WordWrap = true;
            // 
            // sourceDatabaseNameData
            // 
            sourceDatabaseNameData.AutoSize = true;
            sourceDatabaseNameData.Dock = DockStyle.Fill;
            sourceDatabaseNameData.HeaderText = "Source Database Name";
            sourceDatabaseNameData.Location = new Point(3, 291);
            sourceDatabaseNameData.Multiline = false;
            sourceDatabaseNameData.Name = "sourceDatabaseNameData";
            sourceDatabaseNameData.ReadOnly = true;
            sourceDatabaseNameData.Size = new Size(295, 44);
            sourceDatabaseNameData.TabIndex = 12;
            sourceDatabaseNameData.WordWrap = true;
            // 
            // sourceDateData
            // 
            sourceDateData.AutoSize = true;
            sourceDateData.Dock = DockStyle.Fill;
            sourceDateData.HeaderText = "Source Date";
            sourceDateData.Location = new Point(304, 291);
            sourceDateData.Multiline = false;
            sourceDateData.Name = "sourceDateData";
            sourceDateData.ReadOnly = true;
            sourceDateData.Size = new Size(146, 44);
            sourceDateData.TabIndex = 10;
            sourceDateData.WordWrap = true;
            // 
            // exportOptions
            // 
            exportOptions.Items.AddRange(new ToolStripItem[] { exportAll, exportEntites, exportProcesses, exportAttributes });
            exportOptions.Name = "contextMenuStrip1";
            exportOptions.Size = new Size(181, 114);
            // 
            // exportAll
            // 
            exportAll.Name = "exportAll";
            exportAll.Size = new Size(180, 22);
            exportAll.Text = "All items";
            exportAll.ToolTipText = "Create all domain model items from database model";
            exportAll.Click += ExportOptionAll_Click;
            // 
            // exportEntites
            // 
            exportEntites.Name = "exportEntites";
            exportEntites.Size = new Size(180, 22);
            exportEntites.Text = "Entities only";
            exportEntites.ToolTipText = "Create domain Entites from database Tables and Views";
            exportEntites.Click += ExportEntites_Click;
            // 
            // exportProcesses
            // 
            exportProcesses.Name = "exportProcesses";
            exportProcesses.Size = new Size(180, 22);
            exportProcesses.Text = "Processes only";
            exportProcesses.ToolTipText = "Create domain Processes from database Procedures and Functions";
            exportProcesses.Click += ExportProcesses_Click;
            // 
            // exportAttributes
            // 
            exportAttributes.Name = "exportAttributes";
            exportAttributes.Size = new Size(180, 22);
            exportAttributes.Text = "Attributes only";
            exportAttributes.ToolTipText = "Create domain Attributes from database Columns";
            exportAttributes.Click += ExportAttributes_Click;
            // 
            // DbCatalog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(453, 363);
            Controls.Add(catalogManagerLayout);
            Name = "DbCatalog";
            Text = "DbCatalog";
            Load += DbCatalog_Load;
            Controls.SetChildIndex(catalogManagerLayout, 0);
            catalogManagerLayout.ResumeLayout(false);
            catalogManagerLayout.PerformLayout();
            exportOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)bindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData catalogTitleData;
        private Controls.TextBoxData catalogDescriptionData;
        private Controls.TextBoxData sourceServerNameData;
        private Controls.TextBoxData sourceDatabaseNameData;
        private Controls.TextBoxData sourceDateData;
        private ContextMenuStrip exportOptions;
        private BindingSource bindingSource;
        private ToolStripMenuItem exportAll;
        private ToolStripMenuItem exportEntites;
        private ToolStripMenuItem exportAttributes;
        private ToolStripMenuItem exportProcesses;
    }
}