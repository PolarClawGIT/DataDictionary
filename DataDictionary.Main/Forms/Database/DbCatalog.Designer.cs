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
            importOptions = new ContextMenuStrip(components);
            importOptionEntity = new ToolStripMenuItem();
            importOptionAttribute = new ToolStripMenuItem();
            importOptionProcesses = new ToolStripMenuItem();
            catalogManagerLayout = new TableLayoutPanel();
            catalogManagerLayout.SuspendLayout();
            importOptions.SuspendLayout();
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
            // 
            // importOptions
            // 
            importOptions.Items.AddRange(new ToolStripItem[] { importOptionEntity, importOptionAttribute, importOptionProcesses });
            importOptions.Name = "contextMenuStrip1";
            importOptions.Size = new Size(181, 92);
            // 
            // importOptionEntity
            // 
            importOptionEntity.Checked = true;
            importOptionEntity.CheckOnClick = true;
            importOptionEntity.CheckState = CheckState.Checked;
            importOptionEntity.Name = "importOptionEntity";
            importOptionEntity.Size = new Size(180, 22);
            importOptionEntity.Text = "Import Entities";
            // 
            // importOptionAttribute
            // 
            importOptionAttribute.Checked = true;
            importOptionAttribute.CheckOnClick = true;
            importOptionAttribute.CheckState = CheckState.Checked;
            importOptionAttribute.Name = "importOptionAttribute";
            importOptionAttribute.Size = new Size(180, 22);
            importOptionAttribute.Text = "Import Attributes";
            // 
            // importOptionProcesses
            // 
            importOptionProcesses.CheckOnClick = true;
            importOptionProcesses.Enabled = false;
            importOptionProcesses.Name = "importOptionProcesses";
            importOptionProcesses.Size = new Size(180, 22);
            importOptionProcesses.Text = "Import Processes";
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
            importOptions.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData catalogTitleData;
        private Controls.TextBoxData catalogDescriptionData;
        private Controls.TextBoxData sourceServerNameData;
        private Controls.TextBoxData sourceDatabaseNameData;
        private Controls.TextBoxData sourceDateData;
        private ContextMenuStrip importOptions;
        private ToolStripMenuItem importOptionEntity;
        private ToolStripMenuItem importOptionAttribute;
        private ToolStripMenuItem importOptionProcesses;
    }
}