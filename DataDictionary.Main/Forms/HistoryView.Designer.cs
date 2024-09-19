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
            TableLayoutPanel restoreLayout;
            Label restoreWarning;
            GroupBox modificationsGroup;
            titleData = new Controls.TextBoxData();
            modificationData = new Controls.TextBoxData();
            modifiedByData = new Controls.TextBoxData();
            modifiedOnDate = new Controls.TextBoxData();
            viewDetailCommand = new Button();
            restoreCommand = new Button();
            historyModificationData = new ListView();
            historyModificationColumn = new ColumnHeader();
            historyModifiedOnColumn = new ColumnHeader();
            historyValuesData = new ListView();
            historyTitleColumn = new ColumnHeader();
            historyLastModificationColumn = new ColumnHeader();
            historyLayout = new TableLayoutPanel();
            historySummaryGroup = new GroupBox();
            historyDetailLayout = new TableLayoutPanel();
            restoreLayout = new TableLayoutPanel();
            restoreWarning = new Label();
            modificationsGroup = new GroupBox();
            historyLayout.SuspendLayout();
            historySummaryGroup.SuspendLayout();
            historyDetailLayout.SuspendLayout();
            restoreLayout.SuspendLayout();
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
            historyLayout.RowStyles.Add(new RowStyle());
            historyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            historyLayout.Size = new Size(623, 419);
            historyLayout.TabIndex = 4;
            // 
            // historySummaryGroup
            // 
            historySummaryGroup.AutoSize = true;
            historySummaryGroup.Controls.Add(historyDetailLayout);
            historySummaryGroup.Dock = DockStyle.Fill;
            historySummaryGroup.Location = new Point(314, 105);
            historySummaryGroup.Name = "historySummaryGroup";
            historySummaryGroup.Size = new Size(306, 311);
            historySummaryGroup.TabIndex = 2;
            historySummaryGroup.TabStop = false;
            historySummaryGroup.Text = "Summary";
            // 
            // historyDetailLayout
            // 
            historyDetailLayout.ColumnCount = 1;
            historyDetailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            historyDetailLayout.Controls.Add(titleData, 0, 0);
            historyDetailLayout.Controls.Add(modificationData, 0, 1);
            historyDetailLayout.Controls.Add(modifiedByData, 0, 2);
            historyDetailLayout.Controls.Add(modifiedOnDate, 0, 3);
            historyDetailLayout.Controls.Add(restoreLayout, 0, 4);
            historyDetailLayout.Dock = DockStyle.Fill;
            historyDetailLayout.Location = new Point(3, 19);
            historyDetailLayout.Name = "historyDetailLayout";
            historyDetailLayout.RowCount = 5;
            historyDetailLayout.RowStyles.Add(new RowStyle());
            historyDetailLayout.RowStyles.Add(new RowStyle());
            historyDetailLayout.RowStyles.Add(new RowStyle());
            historyDetailLayout.RowStyles.Add(new RowStyle());
            historyDetailLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            historyDetailLayout.Size = new Size(300, 289);
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
            // restoreLayout
            // 
            restoreLayout.ColumnCount = 2;
            restoreLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            restoreLayout.ColumnStyles.Add(new ColumnStyle());
            restoreLayout.Controls.Add(viewDetailCommand, 1, 0);
            restoreLayout.Controls.Add(restoreCommand, 1, 1);
            restoreLayout.Controls.Add(restoreWarning, 0, 0);
            restoreLayout.Dock = DockStyle.Fill;
            restoreLayout.Location = new Point(3, 203);
            restoreLayout.Name = "restoreLayout";
            restoreLayout.RowCount = 2;
            restoreLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            restoreLayout.RowStyles.Add(new RowStyle());
            restoreLayout.Size = new Size(294, 83);
            restoreLayout.TabIndex = 7;
            // 
            // viewDetailCommand
            // 
            viewDetailCommand.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            viewDetailCommand.Location = new Point(216, 28);
            viewDetailCommand.Name = "viewDetailCommand";
            viewDetailCommand.Size = new Size(75, 23);
            viewDetailCommand.TabIndex = 7;
            viewDetailCommand.Text = "Details";
            viewDetailCommand.UseVisualStyleBackColor = true;
            viewDetailCommand.Click += ViewDetailCommand_Click;
            // 
            // restoreCommand
            // 
            restoreCommand.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            restoreCommand.Location = new Point(216, 57);
            restoreCommand.Name = "restoreCommand";
            restoreCommand.Size = new Size(75, 23);
            restoreCommand.TabIndex = 6;
            restoreCommand.Text = "Restore";
            restoreCommand.UseVisualStyleBackColor = true;
            restoreCommand.Click += RestoreCommand_Click;
            // 
            // restoreWarning
            // 
            restoreWarning.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            restoreWarning.AutoSize = true;
            restoreWarning.Location = new Point(3, 53);
            restoreWarning.Name = "restoreWarning";
            restoreLayout.SetRowSpan(restoreWarning, 2);
            restoreWarning.Size = new Size(173, 30);
            restoreWarning.TabIndex = 5;
            restoreWarning.Text = "Restoring a record effects child records.";
            // 
            // modificationsGroup
            // 
            modificationsGroup.Controls.Add(historyModificationData);
            modificationsGroup.Dock = DockStyle.Fill;
            modificationsGroup.Location = new Point(314, 3);
            modificationsGroup.Name = "modificationsGroup";
            modificationsGroup.Size = new Size(306, 96);
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
            historyModificationData.Size = new Size(300, 74);
            historyModificationData.TabIndex = 0;
            historyModificationData.UseCompatibleStateImageBehavior = false;
            historyModificationData.View = View.Details;
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
            historyValuesData.Size = new Size(305, 413);
            historyValuesData.TabIndex = 0;
            historyValuesData.UseCompatibleStateImageBehavior = false;
            historyValuesData.View = View.Details;
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
            ClientSize = new Size(623, 444);
            Controls.Add(historyLayout);
            Name = "HistoryView";
            Text = "HistoryView";
            Load += HistoryView_Load;
            Controls.SetChildIndex(historyLayout, 0);
            historyLayout.ResumeLayout(false);
            historyLayout.PerformLayout();
            historySummaryGroup.ResumeLayout(false);
            historyDetailLayout.ResumeLayout(false);
            historyDetailLayout.PerformLayout();
            restoreLayout.ResumeLayout(false);
            restoreLayout.PerformLayout();
            modificationsGroup.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button restoreCommand;
        private Button viewDetailCommand;
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