namespace DataDictionary.Main.Forms.ProofOfConcept
{
    partial class Document
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
            TableLayoutPanel documentFileLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Document));
            rootFolderData = new DataDictionary.Main.Controls.ComboBoxData();
            relativePathData = new DataDictionary.Main.Controls.SelectTextBoxData();
            localPathData = new DataDictionary.Main.Controls.TextBoxData();
            fileNameData = new DataDictionary.Main.Controls.SelectTextBoxData();
            documentTitleData = new DataDictionary.Main.Controls.TextBoxData();
            documentContentToolStrip = new ToolStrip();
            documentContentData = new TextBox();
            documentFileLayout = new TableLayoutPanel();
            documentFileLayout.SuspendLayout();
            SuspendLayout();
            // 
            // documentFileLayout
            // 
            documentFileLayout.AutoSize = true;
            documentFileLayout.ColumnCount = 1;
            documentFileLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            documentFileLayout.Controls.Add(rootFolderData, 0, 1);
            documentFileLayout.Controls.Add(relativePathData, 0, 2);
            documentFileLayout.Controls.Add(localPathData, 0, 3);
            documentFileLayout.Controls.Add(fileNameData, 0, 4);
            documentFileLayout.Controls.Add(documentTitleData, 0, 0);
            documentFileLayout.Controls.Add(documentContentToolStrip, 0, 5);
            documentFileLayout.Controls.Add(documentContentData, 0, 6);
            documentFileLayout.Dock = DockStyle.Fill;
            documentFileLayout.Location = new Point(0, 25);
            documentFileLayout.Name = "documentFileLayout";
            documentFileLayout.RowCount = 7;
            documentFileLayout.RowStyles.Add(new RowStyle());
            documentFileLayout.RowStyles.Add(new RowStyle());
            documentFileLayout.RowStyles.Add(new RowStyle());
            documentFileLayout.RowStyles.Add(new RowStyle());
            documentFileLayout.RowStyles.Add(new RowStyle());
            documentFileLayout.RowStyles.Add(new RowStyle());
            documentFileLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            documentFileLayout.Size = new Size(559, 560);
            documentFileLayout.TabIndex = 4;
            // 
            // rootFolderData
            // 
            rootFolderData.AutoSize = true;
            rootFolderData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            rootFolderData.Dock = DockStyle.Fill;
            rootFolderData.DropDownStyle = ComboBoxStyle.DropDown;
            rootFolderData.HeaderText = "Root Folder";
            rootFolderData.Location = new Point(3, 53);
            rootFolderData.Name = "rootFolderData";
            rootFolderData.ReadOnly = false;
            rootFolderData.Size = new Size(553, 46);
            rootFolderData.TabIndex = 4;
            // 
            // relativePathData
            // 
            relativePathData.AutoSize = true;
            relativePathData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            relativePathData.Dock = DockStyle.Fill;
            relativePathData.HeaderText = "Relative Path";
            relativePathData.Location = new Point(3, 105);
            relativePathData.Name = "relativePathData";
            relativePathData.ReadOnly = false;
            relativePathData.SelectIcon = (Image)resources.GetObject("relativePathData.SelectIcon");
            relativePathData.Size = new Size(553, 44);
            relativePathData.TabIndex = 5;
            // 
            // localPathData
            // 
            localPathData.AutoSize = true;
            localPathData.Dock = DockStyle.Fill;
            localPathData.HeaderText = "Local Path";
            localPathData.Location = new Point(3, 155);
            localPathData.Multiline = false;
            localPathData.Name = "localPathData";
            localPathData.ReadOnly = true;
            localPathData.Size = new Size(553, 44);
            localPathData.TabIndex = 6;
            localPathData.WordWrap = true;
            // 
            // fileNameData
            // 
            fileNameData.AutoSize = true;
            fileNameData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fileNameData.Dock = DockStyle.Fill;
            fileNameData.HeaderText = "File Name";
            fileNameData.Location = new Point(3, 205);
            fileNameData.Name = "fileNameData";
            fileNameData.ReadOnly = false;
            fileNameData.SelectIcon = (Image)resources.GetObject("fileNameData.SelectIcon");
            fileNameData.Size = new Size(553, 44);
            fileNameData.TabIndex = 7;
            // 
            // documentTitleData
            // 
            documentTitleData.AutoSize = true;
            documentTitleData.Dock = DockStyle.Fill;
            documentTitleData.HeaderText = "Title";
            documentTitleData.Location = new Point(3, 3);
            documentTitleData.Multiline = false;
            documentTitleData.Name = "documentTitleData";
            documentTitleData.ReadOnly = false;
            documentTitleData.Size = new Size(553, 44);
            documentTitleData.TabIndex = 8;
            documentTitleData.WordWrap = true;
            // 
            // documentContentToolStrip
            // 
            documentContentToolStrip.Location = new Point(0, 252);
            documentContentToolStrip.Name = "documentContentToolStrip";
            documentContentToolStrip.Size = new Size(559, 25);
            documentContentToolStrip.TabIndex = 9;
            documentContentToolStrip.Text = "Document Content tools";
            // 
            // documentContentData
            // 
            documentContentData.Dock = DockStyle.Fill;
            documentContentData.Location = new Point(3, 280);
            documentContentData.MaxLength = 0;
            documentContentData.Multiline = true;
            documentContentData.Name = "documentContentData";
            documentContentData.ScrollBars = ScrollBars.Both;
            documentContentData.Size = new Size(553, 277);
            documentContentData.TabIndex = 10;
            // 
            // Document
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(559, 585);
            Controls.Add(documentFileLayout);
            Name = "Document";
            Text = "Document";
            Controls.SetChildIndex(documentFileLayout, 0);
            documentFileLayout.ResumeLayout(false);
            documentFileLayout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.ComboBoxData rootFolderData;
        private Controls.SelectTextBoxData relativePathData;
        private Controls.TextBoxData localPathData;
        private Controls.SelectTextBoxData fileNameData;
        private Controls.TextBoxData documentTitleData;
        private ToolStrip documentContentToolStrip;
        private TextBox documentContentData;
    }
}