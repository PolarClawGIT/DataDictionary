namespace DataDictionary.Main.Forms
{
    partial class ClipboardView
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
            clipboardSplitLayout = new SplitContainer();
            clipboardDataType = new ListBox();
            clipboardData = new TextBox();
            clipbordViewLayout = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)clipboardSplitLayout).BeginInit();
            clipboardSplitLayout.Panel1.SuspendLayout();
            clipboardSplitLayout.Panel2.SuspendLayout();
            clipboardSplitLayout.SuspendLayout();
            clipbordViewLayout.SuspendLayout();
            SuspendLayout();
            // 
            // clipboardSplitLayout
            // 
            clipboardSplitLayout.Dock = DockStyle.Fill;
            clipboardSplitLayout.Location = new Point(3, 3);
            clipboardSplitLayout.Name = "clipboardSplitLayout";
            // 
            // clipboardSplitLayout.Panel1
            // 
            clipboardSplitLayout.Panel1.Controls.Add(clipboardDataType);
            // 
            // clipboardSplitLayout.Panel2
            // 
            clipboardSplitLayout.Panel2.Controls.Add(clipboardData);
            clipboardSplitLayout.Size = new Size(794, 419);
            clipboardSplitLayout.SplitterDistance = 168;
            clipboardSplitLayout.TabIndex = 0;
            // 
            // clipboardDataType
            // 
            clipboardDataType.Dock = DockStyle.Fill;
            clipboardDataType.FormattingEnabled = true;
            clipboardDataType.ItemHeight = 15;
            clipboardDataType.Location = new Point(0, 0);
            clipboardDataType.Name = "clipboardDataType";
            clipboardDataType.Size = new Size(168, 419);
            clipboardDataType.TabIndex = 0;
            clipboardDataType.SelectedIndexChanged += clipboardDataType_SelectedIndexChanged;
            // 
            // clipboardData
            // 
            clipboardData.Dock = DockStyle.Fill;
            clipboardData.Location = new Point(0, 0);
            clipboardData.Multiline = true;
            clipboardData.Name = "clipboardData";
            clipboardData.ReadOnly = true;
            clipboardData.Size = new Size(622, 419);
            clipboardData.TabIndex = 0;
            // 
            // clipbordViewLayout
            // 
            clipbordViewLayout.ColumnCount = 1;
            clipbordViewLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            clipbordViewLayout.Controls.Add(clipboardSplitLayout, 0, 0);
            clipbordViewLayout.Dock = DockStyle.Fill;
            clipbordViewLayout.Location = new Point(0, 25);
            clipbordViewLayout.Name = "clipbordViewLayout";
            clipbordViewLayout.RowCount = 1;
            clipbordViewLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            clipbordViewLayout.Size = new Size(800, 425);
            clipbordViewLayout.TabIndex = 1;
            // 
            // ClipboardView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(clipbordViewLayout);
            Name = "ClipboardView";
            Text = "ClipboardView";
            Load += ClipboardView_Load;
            Controls.SetChildIndex(clipbordViewLayout, 0);
            clipboardSplitLayout.Panel1.ResumeLayout(false);
            clipboardSplitLayout.Panel2.ResumeLayout(false);
            clipboardSplitLayout.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)clipboardSplitLayout).EndInit();
            clipboardSplitLayout.ResumeLayout(false);
            clipbordViewLayout.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private SplitContainer clipboardSplitLayout;
        private ListBox clipboardDataType;
        private TextBox clipboardData;
        private TableLayoutPanel clipbordViewLayout;
    }
}