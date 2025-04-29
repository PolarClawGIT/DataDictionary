namespace DataDictionary.Main.Forms.Model.Controls
{
    partial class Definition
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            TabControl definitionTab;
            definitionTextTab = new TabPage();
            definitionTextData = new DataDictionary.Main.Controls.RichTextBoxData();
            definitionSummaryTab = new TabPage();
            definitionSummaryData = new TextBox();
            definitionLayout = new TableLayoutPanel();
            definitionData = new DataGridView();
            definitionColumn = new DataGridViewComboBoxColumn();
            definitionSummaryColumn = new DataGridViewTextBoxColumn();
            definitionTypeData = new DataDictionary.Main.Controls.ComboBoxData();
            fullTextTools = new ContextMenuStrip(components);
            syncTextToSummary = new ToolStripMenuItem();
            definitionTab = new TabControl();
            definitionTab.SuspendLayout();
            definitionTextTab.SuspendLayout();
            definitionSummaryTab.SuspendLayout();
            definitionLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)definitionData).BeginInit();
            fullTextTools.SuspendLayout();
            SuspendLayout();
            // 
            // definitionTab
            // 
            definitionTab.Controls.Add(definitionTextTab);
            definitionTab.Controls.Add(definitionSummaryTab);
            definitionTab.Dock = DockStyle.Fill;
            definitionTab.Location = new Point(3, 173);
            definitionTab.Name = "definitionTab";
            definitionTab.SelectedIndex = 0;
            definitionTab.Size = new Size(295, 171);
            definitionTab.TabIndex = 2;
            // 
            // definitionTextTab
            // 
            definitionTextTab.BackColor = SystemColors.Control;
            definitionTextTab.Controls.Add(definitionTextData);
            definitionTextTab.Location = new Point(4, 24);
            definitionTextTab.Name = "definitionTextTab";
            definitionTextTab.Padding = new Padding(3);
            definitionTextTab.Size = new Size(287, 143);
            definitionTextTab.TabIndex = 1;
            definitionTextTab.Text = "Full Text";
            // 
            // definitionTextData
            // 
            definitionTextData.AutoSize = true;
            definitionTextData.Dock = DockStyle.Fill;
            definitionTextData.HeaderText = "Definition";
            definitionTextData.HeaderVisible = false;
            definitionTextData.Location = new Point(3, 3);
            definitionTextData.Name = "definitionTextData";
            definitionTextData.ReadOnly = false;
            definitionTextData.Size = new Size(281, 137);
            definitionTextData.TabIndex = 0;
            definitionTextData.ToolStripVisible = true;
            definitionTextData.Validated += DefinitionTextData_Validated;
            // 
            // definitionSummaryTab
            // 
            definitionSummaryTab.BackColor = SystemColors.Control;
            definitionSummaryTab.Controls.Add(definitionSummaryData);
            definitionSummaryTab.Location = new Point(4, 24);
            definitionSummaryTab.Name = "definitionSummaryTab";
            definitionSummaryTab.Padding = new Padding(3);
            definitionSummaryTab.Size = new Size(287, 114);
            definitionSummaryTab.TabIndex = 0;
            definitionSummaryTab.Text = "Summary";
            // 
            // definitionSummaryData
            // 
            definitionSummaryData.Dock = DockStyle.Fill;
            definitionSummaryData.Location = new Point(3, 3);
            definitionSummaryData.Multiline = true;
            definitionSummaryData.Name = "definitionSummaryData";
            definitionSummaryData.ScrollBars = ScrollBars.Both;
            definitionSummaryData.Size = new Size(281, 108);
            definitionSummaryData.TabIndex = 0;
            // 
            // definitionLayout
            // 
            definitionLayout.ColumnCount = 1;
            definitionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            definitionLayout.Controls.Add(definitionData, 0, 0);
            definitionLayout.Controls.Add(definitionTab, 0, 2);
            definitionLayout.Controls.Add(definitionTypeData, 0, 1);
            definitionLayout.Dock = DockStyle.Fill;
            definitionLayout.Location = new Point(0, 0);
            definitionLayout.Name = "definitionLayout";
            definitionLayout.RowCount = 3;
            definitionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            definitionLayout.RowStyles.Add(new RowStyle());
            definitionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            definitionLayout.Size = new Size(301, 347);
            definitionLayout.TabIndex = 1;
            // 
            // definitionData
            // 
            definitionData.AllowUserToAddRows = false;
            definitionData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            definitionData.Columns.AddRange(new DataGridViewColumn[] { definitionColumn, definitionSummaryColumn });
            definitionData.Dock = DockStyle.Fill;
            definitionData.Location = new Point(3, 3);
            definitionData.Name = "definitionData";
            definitionData.ReadOnly = true;
            definitionData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            definitionData.Size = new Size(295, 112);
            definitionData.TabIndex = 3;
            // 
            // definitionColumn
            // 
            definitionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            definitionColumn.DataPropertyName = "DefinitionId";
            definitionColumn.FillWeight = 50F;
            definitionColumn.HeaderText = "Definition";
            definitionColumn.Name = "definitionColumn";
            definitionColumn.ReadOnly = true;
            // 
            // definitionSummaryColumn
            // 
            definitionSummaryColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            definitionSummaryColumn.DataPropertyName = "DefinitionSummary";
            definitionSummaryColumn.HeaderText = "Definition Summary";
            definitionSummaryColumn.Name = "definitionSummaryColumn";
            definitionSummaryColumn.ReadOnly = true;
            // 
            // definitionTypeData
            // 
            definitionTypeData.AutoSize = true;
            definitionTypeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            definitionTypeData.Dock = DockStyle.Fill;
            definitionTypeData.DropDownStyle = ComboBoxStyle.DropDownList;
            definitionTypeData.HeaderText = "Definition type";
            definitionTypeData.Location = new Point(3, 121);
            definitionTypeData.Name = "definitionTypeData";
            definitionTypeData.ReadOnly = false;
            definitionTypeData.Size = new Size(295, 46);
            definitionTypeData.TabIndex = 0;
            definitionTypeData.SelectedIndexChanged += DefinitionData_SelectedIndexChanged;
            definitionTypeData.SelectionChangeCommitted += DefinitionData_SelectionChangeCommitted;
            // 
            // fullTextTools
            // 
            fullTextTools.Items.AddRange(new ToolStripItem[] { syncTextToSummary });
            fullTextTools.Name = "fullTextTools";
            fullTextTools.Size = new Size(191, 26);
            // 
            // syncTextToSummary
            // 
            syncTextToSummary.DisplayStyle = ToolStripItemDisplayStyle.Image;
            syncTextToSummary.Image = Properties.Resources.SyncContent;
            syncTextToSummary.Name = "syncTextToSummary";
            syncTextToSummary.Size = new Size(190, 22);
            syncTextToSummary.Text = "sync Text to Summary";
            syncTextToSummary.ToolTipText = "Sync the RTF text to the Summary";
            syncTextToSummary.Click += SyncTextToSummary_Click;
            // 
            // Definition
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(definitionLayout);
            Name = "Definition";
            Size = new Size(301, 347);
            definitionTab.ResumeLayout(false);
            definitionTextTab.ResumeLayout(false);
            definitionTextTab.PerformLayout();
            definitionSummaryTab.ResumeLayout(false);
            definitionSummaryTab.PerformLayout();
            definitionLayout.ResumeLayout(false);
            definitionLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)definitionData).EndInit();
            fullTextTools.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel definitionLayout;
        private DataDictionary.Main.Controls.ComboBoxData definitionTypeData;
        private TabPage definitionTextTab;
        private DataDictionary.Main.Controls.RichTextBoxData definitionTextData;
        private TabPage definitionSummaryTab;
        private TextBox definitionSummaryData;
        private ContextMenuStrip fullTextTools;
        private ToolStripMenuItem syncTextToSummary;
        private DataGridView definitionData;
        private DataGridViewComboBoxColumn definitionColumn;
        private DataGridViewTextBoxColumn definitionSummaryColumn;
    }
}
