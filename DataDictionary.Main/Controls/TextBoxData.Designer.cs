namespace DataDictionary.Main.Controls
{
    partial class TextBoxData
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
            TableLayoutPanel controlLayout;
            label = new Label();
            textBox = new TextBox();
            errorProvider = new ErrorProvider(components);
            controlLayout = new TableLayoutPanel();
            controlLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // controlLayout
            // 
            controlLayout.AutoSize = true;
            controlLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            controlLayout.ColumnCount = 1;
            controlLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            controlLayout.Controls.Add(label, 0, 0);
            controlLayout.Controls.Add(textBox, 0, 1);
            controlLayout.Dock = DockStyle.Fill;
            controlLayout.Location = new Point(0, 0);
            controlLayout.Margin = new Padding(0);
            controlLayout.Name = "controlLayout";
            controlLayout.RowCount = 2;
            controlLayout.RowStyles.Add(new RowStyle());
            controlLayout.RowStyles.Add(new RowStyle());
            controlLayout.Size = new Size(120, 44);
            controlLayout.TabIndex = 0;
            // 
            // label
            // 
            label.AutoEllipsis = true;
            label.AutoSize = true;
            label.Location = new Point(0, 0);
            label.Margin = new Padding(0);
            label.Name = "label";
            label.Size = new Size(32, 15);
            label.TabIndex = 0;
            label.Text = "label";
            // 
            // textBox
            // 
            textBox.Dock = DockStyle.Fill;
            textBox.Location = new Point(3, 18);
            textBox.Name = "textBox";
            textBox.Size = new Size(114, 23);
            textBox.TabIndex = 1;
            textBox.Validating += textBox_Validating;
            textBox.Validated += textBox_Validated;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // TextBoxData
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            Controls.Add(controlLayout);
            Name = "TextBoxData";
            Size = new Size(120, 44);
            controlLayout.ResumeLayout(false);
            controlLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label;
        private TextBox textBox;
        private ErrorProvider errorProvider;
    }
}
