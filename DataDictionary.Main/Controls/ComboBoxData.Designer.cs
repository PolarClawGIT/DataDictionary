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
            controlLayout = new TableLayoutPanel();
            comboBox = new ComboBox();
            comboBoxLayout = new TableLayoutPanel();
            label = new Label();
            errorLocation = new Panel();
            controlLayout.SuspendLayout();
            comboBoxLayout.SuspendLayout();
            SuspendLayout();
            // 
            // controlLayout
            // 
            controlLayout.AutoSize = true;
            controlLayout.BackColor = SystemColors.ControlDarkDark;
            controlLayout.ColumnCount = 1;
            comboBoxLayout.SetColumnSpan(controlLayout, 2);
            controlLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            controlLayout.Controls.Add(comboBox, 0, 0);
            controlLayout.Dock = DockStyle.Fill;
            controlLayout.Location = new Point(3, 18);
            controlLayout.Name = "controlLayout";
            controlLayout.RowCount = 1;
            controlLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            controlLayout.Size = new Size(123, 25);
            controlLayout.TabIndex = 3;
            // 
            // comboBox
            // 
            comboBox.Dock = DockStyle.Fill;
            comboBox.FlatStyle = FlatStyle.Flat;
            comboBox.FormattingEnabled = true;
            comboBox.Location = new Point(1, 1);
            comboBox.Margin = new Padding(1);
            comboBox.Name = "comboBox";
            comboBox.Size = new Size(121, 23);
            comboBox.TabIndex = 1;
            comboBox.SelectedIndexChanged += comboBox_SelectedIndexChanged;
            comboBox.SelectionChangeCommitted += comboBox_SelectionChangeCommitted;
            comboBox.Validating += comboBox_Validating;
            comboBox.Validated += comboBox_Validated;
            // 
            // comboBoxLayout
            // 
            comboBoxLayout.AutoSize = true;
            comboBoxLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            comboBoxLayout.ColumnCount = 2;
            comboBoxLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            comboBoxLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 25F));
            comboBoxLayout.Controls.Add(label, 0, 0);
            comboBoxLayout.Controls.Add(errorLocation, 1, 0);
            comboBoxLayout.Controls.Add(controlLayout, 0, 1);
            comboBoxLayout.Dock = DockStyle.Fill;
            comboBoxLayout.Location = new Point(0, 0);
            comboBoxLayout.Margin = new Padding(0);
            comboBoxLayout.Name = "comboBoxLayout";
            comboBoxLayout.RowCount = 2;
            comboBoxLayout.RowStyles.Add(new RowStyle());
            comboBoxLayout.RowStyles.Add(new RowStyle());
            comboBoxLayout.Size = new Size(129, 46);
            comboBoxLayout.TabIndex = 0;
            comboBoxLayout.EnabledChanged += comboBoxLayout_EnabledChanged;
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
            // errorLocation
            // 
            errorLocation.AutoSize = true;
            errorLocation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            errorLocation.Dock = DockStyle.Left;
            errorLocation.Location = new Point(107, 3);
            errorLocation.Name = "errorLocation";
            errorLocation.Size = new Size(0, 9);
            errorLocation.TabIndex = 2;
            // 
            // ComboBoxData
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(comboBoxLayout);
            Name = "ComboBoxData";
            Size = new Size(129, 46);
            controlLayout.ResumeLayout(false);
            comboBoxLayout.ResumeLayout(false);
            comboBoxLayout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label;
        private ComboBox comboBox;
        private Panel errorLocation;
        private TableLayoutPanel comboBoxLayout;
        private TableLayoutPanel controlLayout;
    }
}
