namespace DataDictionary.Main.Controls
{
    partial class ComboBoxData
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
            TableLayoutPanel comboBoxLayout;
            label = new Label();
            comboBox = new ComboBox();
            comboBoxLayout = new TableLayoutPanel();
            comboBoxLayout.SuspendLayout();
            SuspendLayout();
            // 
            // comboBoxLayout
            // 
            comboBoxLayout.AutoSize = true;
            comboBoxLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            comboBoxLayout.ColumnCount = 1;
            comboBoxLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            comboBoxLayout.Controls.Add(label, 0, 0);
            comboBoxLayout.Controls.Add(comboBox, 0, 1);
            comboBoxLayout.Dock = DockStyle.Fill;
            comboBoxLayout.Location = new Point(0, 0);
            comboBoxLayout.Margin = new Padding(0);
            comboBoxLayout.Name = "comboBoxLayout";
            comboBoxLayout.RowCount = 2;
            comboBoxLayout.RowStyles.Add(new RowStyle());
            comboBoxLayout.RowStyles.Add(new RowStyle());
            comboBoxLayout.Size = new Size(127, 44);
            comboBoxLayout.TabIndex = 0;
            // 
            // label
            // 
            label.AutoSize = true;
            label.Location = new Point(0, 0);
            label.Margin = new Padding(0);
            label.Name = "label";
            label.Size = new Size(32, 15);
            label.TabIndex = 0;
            label.Text = "label";
            // 
            // comboBox
            // 
            comboBox.Dock = DockStyle.Fill;
            comboBox.FormattingEnabled = true;
            comboBox.Location = new Point(3, 18);
            comboBox.Name = "comboBox";
            comboBox.Size = new Size(121, 23);
            comboBox.TabIndex = 1;
            comboBox.SelectedIndexChanged += comboBox_SelectedIndexChanged;
            comboBox.Validating += comboBox_Validating;
            comboBox.Validated += comboBox_Validated;
            // 
            // ComboBoxData
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(comboBoxLayout);
            Name = "ComboBoxData";
            Size = new Size(127, 44);
            comboBoxLayout.ResumeLayout(false);
            comboBoxLayout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label;
        private ComboBox comboBox;
    }
}
