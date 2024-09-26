namespace DataDictionary.Main.Forms
{
    partial class HistoryView
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
            TableLayoutPanel historyLayout;
            GroupBox historySummaryGroup;
            TableLayoutPanel historyDetailLayout;
            GroupBox modificationsGroup;
            titleData = new Controls.TextBoxData();
            modificationData = new Controls.TextBoxData();
            modifiedByData = new Controls.TextBoxData();
            modifiedOnDate = new Controls.TextBoxData();
            historyModificationData = new ListView();
            historyModificationColumn = new ColumnHeader();
            historyModifiedOnColumn = new ColumnHeader();
            historyValuesData = new ListView();
            historyTitleColumn = new ColumnHeader();
            historyLastModificationColumn = new ColumnHeader();
            historyLayout = new TableLayoutPanel();
            historySummaryGroup = new GroupBox();
            historyDetailLayout = new TableLayoutPanel();
            modificationsGroup = new GroupBox();
            historyLayout.SuspendLayout();
            historySummaryGroup.SuspendLayout();
            historyDetailLayout.SuspendLayout();
            modificationsGroup.SuspendLayout();
            SuspendLayout();
            // 
            // historyLayout
            // 
            historyLayout.ColumnCount = 2;
            historyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            historyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            historyLayout.Controls.Add(historySummaryGroup, 1, 1);
            historyLayout.Controls.Add(modificationsGroup, 1, 0);
            historyLayout.Controls.Add(historyValuesData, 0, 0);
            historyLayout.Dock = DockStyle.Fill;
            historyLayout.Location = new Point(0, 25);
            historyLayout.Name = "historyLayout";
            historyLayout.RowCount = 2;
            historyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            historyLayout.RowStyles.Add(new RowStyle());
            historyLayout.Size = new Size(623, 515);
            historyLayout.TabIndex = 4;
            // 
            // historySummaryGroup
            // 
            historySummaryGroup.AutoSize = true;
            historySummaryGroup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            historySummaryGroup.Controls.Add(historyDetailLayout);
            historySummaryGroup.Dock = DockStyle.Fill;
            historySummaryGroup.Location = new Point(314, 290);
            historySummaryGroup.Name = "historySummaryGroup";
            historySummaryGroup.Size = new Size(306, 222);
            historySummaryGroup.TabIndex = 2;
            historySummaryGroup.TabStop = false;
            historySummaryGroup.Text = "Summary";
            // 
            // historyDetailLayout
            // 
            historyDetailLayout.AutoSize = true;
            historyDetailLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            historyDetailLayout.ColumnCount = 1;
            historyDetailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            historyDetailLayout.Controls.Add(titleData, 0, 0);
            historyDetailLayout.Controls.Add(modificationData, 0, 1);
            historyDetailLayout.Controls.Add(modifiedByData, 0, 2);
            historyDetailLayout.Controls.Add(modifiedOnDate, 0, 3);
            historyDetailLayout.Dock = DockStyle.Fill;
            historyDetailLayout.Location = new Point(3, 19);
            historyDetailLayout.Name = "historyDetailLayout";
            historyDetailLayout.RowCount = 4;
            historyDetailLayout.RowStyles.Add(new RowStyle());
            historyDetailLayout.RowStyles.Add(new RowStyle());
            historyDetailLayout.RowStyles.Add(new RowStyle());
            historyDetailLayout.RowStyles.Add(new RowStyle());
            historyDetailLayout.Size = new Size(300, 200);
            historyDetailLayout.TabIndex = 0;
            // 
            // titleData
            // 
            titleData.AutoSize = true;
            titleData.Dock = DockStyle.Fill;
            titleData.HeaderText = "Title/Name";
            titleData.Location = new Point(3, 3);
            titleData.Multiline = false;
            titleData.Name = "titleData";
            titleData.ReadOnly = true;
            titleData.Size = new Size(294, 44);
            titleData.TabIndex = 0;
            titleData.WordWrap = true;
            // 
            // modificationData
            // 
            modificationData.AutoSize = true;
            modificationData.Dock = DockStyle.Fill;
            modificationData.HeaderText = "Modification";
            modificationData.Location = new Point(3, 53);
            modificationData.Multiline = false;
            modificationData.Name = "modificationData";
            modificationData.ReadOnly = true;
            modificationData.Size = new Size(294, 44);
            modificationData.TabIndex = 2;
            modificationData.WordWrap = true;
            // 
            // modifiedByData
            // 
            modifiedByData.AutoSize = true;
            modifiedByData.Dock = DockStyle.Fill;
            modifiedByData.HeaderText = "Modified By";
            modifiedByData.Location = new Point(3, 103);
            modifiedByData.Multiline = false;
            modifiedByData.Name = "modifiedByData";
            modifiedByData.ReadOnly = true;
            modifiedByData.Size = new Size(294, 44);
            modifiedByData.TabIndex = 3;
            modifiedByData.WordWrap = true;
            // 
            // modifiedOnDate
            // 
            modifiedOnDate.AutoSize = true;
            modifiedOnDate.Dock = DockStyle.Fill;
            modifiedOnDate.HeaderText = "Modified On";
            modifiedOnDate.Location = new Point(3, 153);
            modifiedOnDate.Multiline = false;
            modifiedOnDate.Name = "modifiedOnDate";
            modifiedOnDate.ReadOnly = true;
            modifiedOnDate.Size = new Size(294, 44);
            modifiedOnDate.TabIndex = 4;
            modifiedOnDate.WordWrap = true;
            // 
            // modificationsGroup
            // 
            modificationsGroup.Controls.Add(historyModificationData);
            modificationsGroup.Dock = DockStyle.Fill;
            modificationsGroup.Location = new Point(314, 3);
            modificationsGroup.Name = "modificationsGroup";
            modificationsGroup.Size = new Size(306, 281);
            modificationsGroup.TabIndex = 0;
            modificationsGroup.TabStop = false;
            modificationsGroup.Text = "Modifications";
            // 
            // historyModificationData
            // 
            historyModificationData.Columns.AddRange(new ColumnHeader[] { historyModificationColumn, historyModifiedOnColumn });
            historyModificationData.Dock = DockStyle.Fill;
            historyModificationData.Location = new Point(3, 19);
            historyModificationData.MultiSelect = false;
            historyModificationData.Name = "historyModificationData";
            historyModificationData.Size = new Size(300, 259);
            historyModificationData.TabIndex = 0;
            historyModificationData.UseCompatibleStateImageBehavior = false;
            historyModificationData.View = View.Details;
            historyModificationData.SelectedIndexChanged += HistoryModificationData_SelectedIndexChanged;
            historyModificationData.Resize += HistoryModificationData_Resize;
            // 
            // historyModificationColumn
            // 
            historyModificationColumn.Text = "Modification";
            historyModificationColumn.Width = 125;
            // 
            // historyModifiedOnColumn
            // 
            historyModifiedOnColumn.Text = "Modified On";
            historyModifiedOnColumn.Width = 150;
            // 
            // historyValuesData
            // 
            historyValuesData.Columns.AddRange(new ColumnHeader[] { historyTitleColumn, historyLastModificationColumn });
            historyValuesData.Dock = DockStyle.Fill;
            historyValuesData.Location = new Point(3, 3);
            historyValuesData.MultiSelect = false;
            historyValuesData.Name = "historyValuesData";
            historyLayout.SetRowSpan(historyValuesData, 2);
            historyValuesData.Size = new Size(305, 509);
            historyValuesData.TabIndex = 0;
            historyValuesData.UseCompatibleStateImageBehavior = false;
            historyValuesData.View = View.Details;
            historyValuesData.SelectedIndexChanged += HistoryValuesData_SelectedIndexChanged;
            historyValuesData.Resize += HistoryValuesData_Resize;
            // 
            // historyTitleColumn
            // 
            historyTitleColumn.Text = "Title/Name";
            historyTitleColumn.Width = 175;
            // 
            // historyLastModificationColumn
            // 
            historyLastModificationColumn.Text = "Last Action";
            historyLastModificationColumn.Width = 100;
            // 
            // HistoryView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(623, 540);
            Controls.Add(historyLayout);
            Name = "HistoryView";
            Text = "HistoryView";
            Load += HistoryView_Load;
            Controls.SetChildIndex(historyLayout, 0);
            historyLayout.ResumeLayout(false);
            historyLayout.PerformLayout();
            historySummaryGroup.ResumeLayout(false);
            historySummaryGroup.PerformLayout();
            historyDetailLayout.ResumeLayout(false);
            historyDetailLayout.PerformLayout();
            modificationsGroup.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        protected Controls.TextBoxData titleData;
        protected Controls.TextBoxData modificationData;
        protected Controls.TextBoxData modifiedByData;
        protected Controls.TextBoxData modifiedOnDate;
        private ListView historyModificationData;
        private ListView historyValuesData;
        private ColumnHeader historyTitleColumn;
        private ColumnHeader historyLastModificationColumn;
        private ColumnHeader historyModificationColumn;
        private ColumnHeader historyModifiedOnColumn;
    }
}