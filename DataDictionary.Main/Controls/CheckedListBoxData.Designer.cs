namespace DataDictionary.Main.Controls
{
    partial class CheckedListBoxData
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
            TableLayoutPanel checkedListBoxLayout;
            label = new Label();
            checkedListBox = new CheckedListBox();
            errorLocation = new Panel();
            checkedListBoxLayout = new TableLayoutPanel();
            checkedListBoxLayout.SuspendLayout();
            SuspendLayout();
            // 
            // checkedListBoxLayout
            // 
            checkedListBoxLayout.AutoSize = true;
            checkedListBoxLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            checkedListBoxLayout.ColumnCount = 2;
            checkedListBoxLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            checkedListBoxLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 25F));
            checkedListBoxLayout.Controls.Add(label, 0, 0);
            checkedListBoxLayout.Controls.Add(checkedListBox, 0, 1);
            checkedListBoxLayout.Controls.Add(errorLocation, 1, 0);
            checkedListBoxLayout.Dock = DockStyle.Fill;
            checkedListBoxLayout.Location = new Point(0, 0);
            checkedListBoxLayout.Name = "checkedListBoxLayout";
            checkedListBoxLayout.RowCount = 2;
            checkedListBoxLayout.RowStyles.Add(new RowStyle());
            checkedListBoxLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            checkedListBoxLayout.Size = new Size(126, 115);
            checkedListBoxLayout.TabIndex = 0;
            // 
            // label
            // 
            label.AutoSize = true;
            label.Location = new Point(3, 0);
            label.Name = "label";
            label.Size = new Size(32, 15);
            label.TabIndex = 0;
            label.Text = "label";
            // 
            // checkedListBox
            // 
            checkedListBoxLayout.SetColumnSpan(checkedListBox, 2);
            checkedListBox.Dock = DockStyle.Fill;
            checkedListBox.FormattingEnabled = true;
            checkedListBox.IntegralHeight = false;
            checkedListBox.Location = new Point(3, 18);
            checkedListBox.Name = "checkedListBox";
            checkedListBox.Size = new Size(120, 94);
            checkedListBox.TabIndex = 1;
            checkedListBox.ItemCheck += checkedListBox_ItemCheck;
            checkedListBox.SelectedIndexChanged += checkedListBox_SelectedIndexChanged;
            checkedListBox.Validating += checkedListBox_Validating;
            checkedListBox.Validated += checkedListBox_Validated;
            // 
            // errorLocation
            // 
            errorLocation.AutoSize = true;
            errorLocation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            errorLocation.Dock = DockStyle.Left;
            errorLocation.Location = new Point(104, 3);
            errorLocation.Name = "errorLocation";
            errorLocation.Size = new Size(0, 9);
            errorLocation.TabIndex = 2;
            // 
            // CheckedListBoxData
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(checkedListBoxLayout);
            Name = "CheckedListBoxData";
            Size = new Size(126, 115);
            EnabledChanged += CheckedListBoxData_EnabledChanged;
            checkedListBoxLayout.ResumeLayout(false);
            checkedListBoxLayout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label;
        private CheckedListBox checkedListBox;
        private Panel errorLocation;
    }
}
