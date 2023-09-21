namespace DataDictionary.Main.ProofOfConcept
{
    partial class TextEditor
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
            printDocument = new System.Drawing.Printing.PrintDocument();
            richTextBoxData = new Controls.RichTextBoxData();
            printPreviewControl = new PrintPreviewControl();
            tableLayoutPanel1 = new TableLayoutPanel();
            asRichTextCode = new Controls.TextBoxData();
            asPlainText = new Controls.TextBoxData();
            asHtml = new Controls.TextBoxData();
            htmlTextBoxData = new Controls.HtmlTextBoxData();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // printDocument
            // 
            printDocument.PrintPage += printDocument_PrintPage;
            // 
            // richTextBoxData
            // 
            richTextBoxData.AutoSize = true;
            richTextBoxData.Dock = DockStyle.Fill;
            richTextBoxData.HeaderText = "Test Rich Text";
            richTextBoxData.Location = new Point(3, 3);
            richTextBoxData.Name = "richTextBoxData";
            richTextBoxData.ReadOnly = false;
            tableLayoutPanel1.SetRowSpan(richTextBoxData, 4);
            richTextBoxData.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil Segoe UI;}}\r\n{\\*\\generator Riched20 10.0.19041}\\viewkind4\\uc1 \r\n\\pard\\f0\\fs18\\par\r\n}\r\n";
            richTextBoxData.Size = new Size(314, 657);
            richTextBoxData.TabIndex = 0;
            // 
            // printPreviewControl
            // 
            printPreviewControl.Dock = DockStyle.Fill;
            printPreviewControl.Location = new Point(643, 3);
            printPreviewControl.Name = "printPreviewControl";
            tableLayoutPanel1.SetRowSpan(printPreviewControl, 4);
            printPreviewControl.Size = new Size(317, 657);
            printPreviewControl.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Controls.Add(richTextBoxData, 0, 0);
            tableLayoutPanel1.Controls.Add(printPreviewControl, 2, 0);
            tableLayoutPanel1.Controls.Add(asRichTextCode, 1, 0);
            tableLayoutPanel1.Controls.Add(asPlainText, 1, 1);
            tableLayoutPanel1.Controls.Add(asHtml, 1, 2);
            tableLayoutPanel1.Controls.Add(htmlTextBoxData, 1, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 25);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Size = new Size(963, 663);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // asRichTextCode
            // 
            asRichTextCode.AutoSize = true;
            asRichTextCode.Dock = DockStyle.Fill;
            asRichTextCode.HeaderText = "as Rich Text Code";
            asRichTextCode.Location = new Point(323, 3);
            asRichTextCode.Multiline = true;
            asRichTextCode.Name = "asRichTextCode";
            asRichTextCode.ReadOnly = true;
            asRichTextCode.Size = new Size(314, 159);
            asRichTextCode.TabIndex = 2;
            // 
            // asPlainText
            // 
            asPlainText.AutoSize = true;
            asPlainText.Dock = DockStyle.Fill;
            asPlainText.HeaderText = "as Plain Text";
            asPlainText.Location = new Point(323, 168);
            asPlainText.Multiline = true;
            asPlainText.Name = "asPlainText";
            asPlainText.ReadOnly = true;
            asPlainText.Size = new Size(314, 159);
            asPlainText.TabIndex = 3;
            // 
            // asHtml
            // 
            asHtml.AutoSize = true;
            asHtml.Dock = DockStyle.Fill;
            asHtml.HeaderText = "parsed HTML/XML";
            asHtml.Location = new Point(323, 333);
            asHtml.Multiline = true;
            asHtml.Name = "asHtml";
            asHtml.ReadOnly = true;
            asHtml.Size = new Size(314, 159);
            asHtml.TabIndex = 4;
            // 
            // htmlTextBoxData
            // 
            htmlTextBoxData.Dock = DockStyle.Fill;
            htmlTextBoxData.HeaderText = "rendered HTML/XML";
            htmlTextBoxData.Location = new Point(323, 498);
            htmlTextBoxData.Name = "htmlTextBoxData";
            htmlTextBoxData.Size = new Size(314, 162);
            htmlTextBoxData.TabIndex = 5;
            // 
            // TextEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(963, 688);
            Controls.Add(tableLayoutPanel1);
            Name = "TextEditor";
            Text = "HtmlTextEditor";
            Load += TextEditor_Load;
            Controls.SetChildIndex(tableLayoutPanel1, 0);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Drawing.Printing.PrintDocument printDocument;
        private Controls.RichTextBoxData richTextBoxData;
        private PrintPreviewControl printPreviewControl;
        private TableLayoutPanel tableLayoutPanel1;
        private Controls.TextBoxData asRichTextCode;
        private Controls.TextBoxData asPlainText;
        private Controls.TextBoxData asHtml;
        private Controls.HtmlTextBoxData htmlTextBoxData;
    }
}