namespace DataDictionary.Main.Dialogs
{
    partial class ViewTextTemplate
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
            TableLayoutPanel textTemplateLayout;
            textTemplateResult = new TextBox();
            textTemplateLayout = new TableLayoutPanel();
            textTemplateLayout.SuspendLayout();
            SuspendLayout();
            // 
            // textTemplateLayout
            // 
            textTemplateLayout.ColumnCount = 1;
            textTemplateLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            textTemplateLayout.Controls.Add(textTemplateResult, 0, 1);
            textTemplateLayout.Dock = DockStyle.Fill;
            textTemplateLayout.Location = new Point(0, 0);
            textTemplateLayout.Name = "textTemplateLayout";
            textTemplateLayout.RowCount = 2;
            textTemplateLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            textTemplateLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            textTemplateLayout.Size = new Size(800, 450);
            textTemplateLayout.TabIndex = 0;
            // 
            // textTemplateResult
            // 
            textTemplateResult.Dock = DockStyle.Fill;
            textTemplateResult.Location = new Point(3, 23);
            textTemplateResult.Multiline = true;
            textTemplateResult.Name = "textTemplateResult";
            textTemplateResult.ScrollBars = ScrollBars.Both;
            textTemplateResult.Size = new Size(794, 424);
            textTemplateResult.TabIndex = 0;
            // 
            // ViewTextTemplate
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(textTemplateLayout);
            Name = "ViewTextTemplate";
            Text = "ViewTextTemplate";
            Load += ViewTextTemplate_Load;
            Controls.SetChildIndex(textTemplateLayout, 0);
            textTemplateLayout.ResumeLayout(false);
            textTemplateLayout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textTemplateResult;
    }
}