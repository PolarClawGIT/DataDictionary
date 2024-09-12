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
            components = new System.ComponentModel.Container();
            TableLayoutPanel historyLayout;
            GroupBox modificationsGroup;
            TableLayoutPanel modificationLayout;
            TableLayoutPanel modificationFilterLayout;
            TableLayoutPanel modificationOptionLayout;
            GroupBox historySummaryGroup;
            TableLayoutPanel historyDetailLayout;
            TableLayoutPanel restoreLayout;
            Label restoreWarning;
            historyData = new DataGridView();
            asOfFilter = new Controls.TextBoxData();
            requeryCommand = new Button();
            filterByOnAfter = new CheckBox();
            filterByObject = new CheckBox();
            filterDistinct = new CheckBox();
            titleData = new Controls.TextBoxData();
            descriptionData = new Controls.TextBoxData();
            modificationData = new Controls.TextBoxData();
            modifiedByData = new Controls.TextBoxData();
            modifiedOnDate = new Controls.TextBoxData();
            viewDetailCommand = new Button();
            restoreCommand = new Button();
            bindingModification = new BindingSource(components);
            titleColumn = new DataGridViewTextBoxColumn();
            modifiedOnColumn = new DataGridViewTextBoxColumn();
            historyLayout = new TableLayoutPanel();
            modificationsGroup = new GroupBox();
            modificationLayout = new TableLayoutPanel();
            modificationFilterLayout = new TableLayoutPanel();
            modificationOptionLayout = new TableLayoutPanel();
            historySummaryGroup = new GroupBox();
            historyDetailLayout = new TableLayoutPanel();
            restoreLayout = new TableLayoutPanel();
            restoreWarning = new Label();
            historyLayout.SuspendLayout();
            modificationsGroup.SuspendLayout();
            modificationLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)historyData).BeginInit();
            modificationFilterLayout.SuspendLayout();
            modificationOptionLayout.SuspendLayout();
            historySummaryGroup.SuspendLayout();
            historyDetailLayout.SuspendLayout();
            restoreLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingModification).BeginInit();
            SuspendLayout();
            // 
            // historyLayout
            // 
            historyLayout.ColumnCount = 2;
            historyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            historyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            historyLayout.Controls.Add(modificationsGroup, 0, 0);
            historyLayout.Controls.Add(historySummaryGroup, 1, 0);
            historyLayout.Dock = DockStyle.Fill;
            historyLayout.Location = new Point(0, 25);
            historyLayout.Name = "historyLayout";
            historyLayout.RowCount = 1;
            historyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            historyLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            historyLayout.Size = new Size(556, 419);
            historyLayout.TabIndex = 4;
            // 
            // modificationsGroup
            // 
            modificationsGroup.Controls.Add(modificationLayout);
            modificationsGroup.Dock = DockStyle.Fill;
            modificationsGroup.Location = new Point(3, 3);
            modificationsGroup.Name = "modificationsGroup";
            modificationsGroup.Size = new Size(272, 413);
            modificationsGroup.TabIndex = 0;
            modificationsGroup.TabStop = false;
            modificationsGroup.Text = "Modifications";
            // 
            // modificationLayout
            // 
            modificationLayout.ColumnCount = 1;
            modificationLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            modificationLayout.Controls.Add(historyData, 0, 0);
            modificationLayout.Controls.Add(modificationFilterLayout, 0, 1);
            modificationLayout.Dock = DockStyle.Fill;
            modificationLayout.Location = new Point(3, 19);
            modificationLayout.Name = "modificationLayout";
            modificationLayout.RowCount = 2;
            modificationLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            modificationLayout.RowStyles.Add(new RowStyle());
            modificationLayout.Size = new Size(266, 391);
            modificationLayout.TabIndex = 1;
            // 
            // historyData
            // 
            historyData.AllowUserToAddRows = false;
            historyData.AllowUserToDeleteRows = false;
            historyData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            historyData.Columns.AddRange(new DataGridViewColumn[] { titleColumn, modifiedOnColumn });
            historyData.Dock = DockStyle.Fill;
            historyData.Location = new Point(3, 3);
            historyData.Name = "historyData";
            historyData.Size = new Size(260, 300);
            historyData.TabIndex = 0;
            // 
            // modificationFilterLayout
            // 
            modificationFilterLayout.AutoSize = true;
            modificationFilterLayout.ColumnCount = 2;
            modificationFilterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            modificationFilterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            modificationFilterLayout.Controls.Add(asOfFilter, 1, 0);
            modificationFilterLayout.Controls.Add(requeryCommand, 1, 1);
            modificationFilterLayout.Controls.Add(modificationOptionLayout, 0, 0);
            modificationFilterLayout.Dock = DockStyle.Fill;
            modificationFilterLayout.Location = new Point(3, 309);
            modificationFilterLayout.Name = "modificationFilterLayout";
            modificationFilterLayout.RowCount = 2;
            modificationFilterLayout.RowStyles.Add(new RowStyle());
            modificationFilterLayout.RowStyles.Add(new RowStyle());
            modificationFilterLayout.Size = new Size(260, 79);
            modificationFilterLayout.TabIndex = 1;
            // 
            // asOfFilter
            // 
            asOfFilter.AutoSize = true;
            asOfFilter.HeaderText = "Target Date";
            asOfFilter.Location = new Point(133, 3);
            asOfFilter.Multiline = false;
            asOfFilter.Name = "asOfFilter";
            asOfFilter.ReadOnly = false;
            asOfFilter.Size = new Size(120, 44);
            asOfFilter.TabIndex = 1;
            asOfFilter.WordWrap = true;
            // 
            // requeryCommand
            // 
            requeryCommand.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            requeryCommand.Location = new Point(182, 53);
            requeryCommand.Name = "requeryCommand";
            requeryCommand.Size = new Size(75, 23);
            requeryCommand.TabIndex = 0;
            requeryCommand.Text = "Requery";
            requeryCommand.UseVisualStyleBackColor = true;
            requeryCommand.Click += ReQueryCommand_Click;
            // 
            // modificationOptionLayout
            // 
            modificationOptionLayout.AutoSize = true;
            modificationOptionLayout.ColumnCount = 1;
            modificationOptionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            modificationOptionLayout.Controls.Add(filterByOnAfter, 0, 0);
            modificationOptionLayout.Controls.Add(filterByObject, 0, 1);
            modificationOptionLayout.Controls.Add(filterDistinct, 0, 2);
            modificationOptionLayout.Dock = DockStyle.Fill;
            modificationOptionLayout.Location = new Point(3, 3);
            modificationOptionLayout.Name = "modificationOptionLayout";
            modificationOptionLayout.RowCount = 3;
            modificationFilterLayout.SetRowSpan(modificationOptionLayout, 2);
            modificationOptionLayout.RowStyles.Add(new RowStyle());
            modificationOptionLayout.RowStyles.Add(new RowStyle());
            modificationOptionLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            modificationOptionLayout.Size = new Size(124, 73);
            modificationOptionLayout.TabIndex = 3;
            // 
            // filterByOnAfter
            // 
            filterByOnAfter.AutoSize = true;
            filterByOnAfter.Location = new Point(3, 3);
            filterByOnAfter.Name = "filterByOnAfter";
            filterByOnAfter.Size = new Size(85, 19);
            filterByOnAfter.TabIndex = 2;
            filterByOnAfter.Text = "On or After";
            filterByOnAfter.UseVisualStyleBackColor = true;
            // 
            // filterByObject
            // 
            filterByObject.AutoSize = true;
            filterByObject.Location = new Point(3, 28);
            filterByObject.Name = "filterByObject";
            filterByObject.Size = new Size(100, 19);
            filterByObject.TabIndex = 0;
            filterByObject.Text = "This item only";
            filterByObject.UseVisualStyleBackColor = true;
            // 
            // filterDistinct
            // 
            filterDistinct.AutoSize = true;
            filterDistinct.Dock = DockStyle.Fill;
            filterDistinct.Location = new Point(3, 53);
            filterDistinct.Name = "filterDistinct";
            filterDistinct.Size = new Size(118, 17);
            filterDistinct.TabIndex = 3;
            filterDistinct.Text = "Distinct Items";
            filterDistinct.UseVisualStyleBackColor = true;
            // 
            // historySummaryGroup
            // 
            historySummaryGroup.Controls.Add(historyDetailLayout);
            historySummaryGroup.Dock = DockStyle.Fill;
            historySummaryGroup.Location = new Point(281, 3);
            historySummaryGroup.Name = "historySummaryGroup";
            historySummaryGroup.Size = new Size(272, 413);
            historySummaryGroup.TabIndex = 2;
            historySummaryGroup.TabStop = false;
            historySummaryGroup.Text = "Summary";
            // 
            // historyDetailLayout
            // 
            historyDetailLayout.ColumnCount = 1;
            historyDetailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            historyDetailLayout.Controls.Add(titleData, 0, 0);
            historyDetailLayout.Controls.Add(descriptionData, 0, 1);
            historyDetailLayout.Controls.Add(modificationData, 0, 2);
            historyDetailLayout.Controls.Add(modifiedByData, 0, 3);
            historyDetailLayout.Controls.Add(modifiedOnDate, 0, 4);
            historyDetailLayout.Controls.Add(restoreLayout, 0, 5);
            historyDetailLayout.Dock = DockStyle.Fill;
            historyDetailLayout.Location = new Point(3, 19);
            historyDetailLayout.Name = "historyDetailLayout";
            historyDetailLayout.RowCount = 6;
            historyDetailLayout.RowStyles.Add(new RowStyle());
            historyDetailLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            historyDetailLayout.RowStyles.Add(new RowStyle());
            historyDetailLayout.RowStyles.Add(new RowStyle());
            historyDetailLayout.RowStyles.Add(new RowStyle());
            historyDetailLayout.RowStyles.Add(new RowStyle());
            historyDetailLayout.Size = new Size(266, 391);
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
            titleData.Size = new Size(260, 44);
            titleData.TabIndex = 0;
            titleData.WordWrap = true;
            // 
            // descriptionData
            // 
            descriptionData.AutoSize = true;
            descriptionData.Dock = DockStyle.Fill;
            descriptionData.HeaderText = "Description";
            descriptionData.Location = new Point(3, 53);
            descriptionData.Multiline = true;
            descriptionData.Name = "descriptionData";
            descriptionData.ReadOnly = true;
            descriptionData.Size = new Size(260, 121);
            descriptionData.TabIndex = 1;
            descriptionData.WordWrap = true;
            // 
            // modificationData
            // 
            modificationData.AutoSize = true;
            modificationData.Dock = DockStyle.Fill;
            modificationData.HeaderText = "Modification";
            modificationData.Location = new Point(3, 180);
            modificationData.Multiline = false;
            modificationData.Name = "modificationData";
            modificationData.ReadOnly = true;
            modificationData.Size = new Size(260, 44);
            modificationData.TabIndex = 2;
            modificationData.WordWrap = true;
            // 
            // modifiedByData
            // 
            modifiedByData.AutoSize = true;
            modifiedByData.Dock = DockStyle.Fill;
            modifiedByData.HeaderText = "Modified By";
            modifiedByData.Location = new Point(3, 230);
            modifiedByData.Multiline = false;
            modifiedByData.Name = "modifiedByData";
            modifiedByData.ReadOnly = true;
            modifiedByData.Size = new Size(260, 44);
            modifiedByData.TabIndex = 3;
            modifiedByData.WordWrap = true;
            // 
            // modifiedOnDate
            // 
            modifiedOnDate.AutoSize = true;
            modifiedOnDate.Dock = DockStyle.Fill;
            modifiedOnDate.HeaderText = "Modified On";
            modifiedOnDate.Location = new Point(3, 280);
            modifiedOnDate.Multiline = false;
            modifiedOnDate.Name = "modifiedOnDate";
            modifiedOnDate.ReadOnly = true;
            modifiedOnDate.Size = new Size(260, 44);
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
            restoreLayout.Location = new Point(3, 330);
            restoreLayout.Name = "restoreLayout";
            restoreLayout.RowCount = 2;
            restoreLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            restoreLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            restoreLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            restoreLayout.Size = new Size(260, 58);
            restoreLayout.TabIndex = 7;
            // 
            // viewDetailCommand
            // 
            viewDetailCommand.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            viewDetailCommand.Location = new Point(182, 3);
            viewDetailCommand.Name = "viewDetailCommand";
            viewDetailCommand.Size = new Size(75, 23);
            viewDetailCommand.TabIndex = 7;
            viewDetailCommand.Text = "Details";
            viewDetailCommand.UseVisualStyleBackColor = true;
            viewDetailCommand.Click += ViewDetailCommand_Click;
            // 
            // restoreCommand
            // 
            restoreCommand.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            restoreCommand.Location = new Point(182, 32);
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
            restoreWarning.Location = new Point(3, 28);
            restoreWarning.Name = "restoreWarning";
            restoreLayout.SetRowSpan(restoreWarning, 2);
            restoreWarning.Size = new Size(173, 30);
            restoreWarning.TabIndex = 5;
            restoreWarning.Text = "Restoring a record effects child records.";
            // 
            // titleColumn
            // 
            titleColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            titleColumn.DataPropertyName = "Title";
            titleColumn.HeaderText = "Title/Name";
            titleColumn.Name = "titleColumn";
            // 
            // modifiedOnColumn
            // 
            modifiedOnColumn.DataPropertyName = "ModifiedOn";
            modifiedOnColumn.HeaderText = "Modified On";
            modifiedOnColumn.Name = "modifiedOnColumn";
            // 
            // HistoryView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(556, 444);
            Controls.Add(historyLayout);
            Name = "HistoryView";
            Text = "HistoryView";
            Load += HistoryView_Load;
            Controls.SetChildIndex(historyLayout, 0);
            historyLayout.ResumeLayout(false);
            modificationsGroup.ResumeLayout(false);
            modificationLayout.ResumeLayout(false);
            modificationLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)historyData).EndInit();
            modificationFilterLayout.ResumeLayout(false);
            modificationFilterLayout.PerformLayout();
            modificationOptionLayout.ResumeLayout(false);
            modificationOptionLayout.PerformLayout();
            historySummaryGroup.ResumeLayout(false);
            historyDetailLayout.ResumeLayout(false);
            historyDetailLayout.PerformLayout();
            restoreLayout.ResumeLayout(false);
            restoreLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingModification).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel historyLayout;
        private GroupBox modificationsGroup;
        private TableLayoutPanel historyDetailLayout;
        private Label restoreWarning;
        private Button restoreCommand;
        private Button viewDetailCommand;
        private Button requeryCommand;
        private Controls.TextBoxData asOfFilter;
        private CheckBox filterByObject;
        private CheckBox filterByOnAfter;
        private CheckBox filterDistinct;
        protected BindingSource bindingModification;
        protected DataGridView historyData;
        protected Controls.TextBoxData titleData;
        protected Controls.TextBoxData descriptionData;
        protected Controls.TextBoxData modificationData;
        protected Controls.TextBoxData modifiedByData;
        protected Controls.TextBoxData modifiedOnDate;
        private DataGridViewTextBoxColumn titleColumn;
        private DataGridViewTextBoxColumn modifiedOnColumn;
    }
}