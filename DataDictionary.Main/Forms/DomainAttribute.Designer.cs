namespace DataDictionary.Main.Forms
{
    partial class DomainAttribute
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
            TableLayoutPanel attributeLayout;
            attributeTitleData = new Controls.TextBoxData();
            attributeParentTitleData = new Controls.TextBoxData();
            attributeTextData = new Controls.RichTextBoxData();
            attributeLayout = new TableLayoutPanel();
            attributeLayout.SuspendLayout();
            SuspendLayout();
            // 
            // attributeLayout
            // 
            attributeLayout.ColumnCount = 1;
            attributeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            attributeLayout.Controls.Add(attributeTitleData, 0, 0);
            attributeLayout.Controls.Add(attributeParentTitleData, 0, 1);
            attributeLayout.Controls.Add(attributeTextData, 0, 2);
            attributeLayout.Dock = DockStyle.Fill;
            attributeLayout.Location = new Point(0, 0);
            attributeLayout.Name = "attributeLayout";
            attributeLayout.RowCount = 3;
            attributeLayout.RowStyles.Add(new RowStyle());
            attributeLayout.RowStyles.Add(new RowStyle());
            attributeLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            attributeLayout.Size = new Size(337, 450);
            attributeLayout.TabIndex = 0;
            // 
            // attributeTitleData
            // 
            attributeTitleData.AutoSize = true;
            attributeTitleData.Dock = DockStyle.Fill;
            attributeTitleData.HeaderText = "Attribute Title";
            attributeTitleData.Location = new Point(0, 0);
            attributeTitleData.Margin = new Padding(0);
            attributeTitleData.Name = "attributeTitleData";
            attributeTitleData.ReadOnly = false;
            attributeTitleData.Size = new Size(337, 44);
            attributeTitleData.TabIndex = 0;
            // 
            // attributeParentTitleData
            // 
            attributeParentTitleData.AutoSize = true;
            attributeParentTitleData.Dock = DockStyle.Fill;
            attributeParentTitleData.HeaderText = "Parent Attribute";
            attributeParentTitleData.Location = new Point(0, 44);
            attributeParentTitleData.Margin = new Padding(0);
            attributeParentTitleData.Name = "attributeParentTitleData";
            attributeParentTitleData.ReadOnly = true;
            attributeParentTitleData.Size = new Size(337, 44);
            attributeParentTitleData.TabIndex = 1;
            // 
            // attributeTextData
            // 
            attributeTextData.AutoSize = true;
            attributeTextData.Dock = DockStyle.Fill;
            attributeTextData.HeaderText = "Attribute Text";
            attributeTextData.Location = new Point(0, 88);
            attributeTextData.Margin = new Padding(0);
            attributeTextData.Name = "attributeTextData";
            attributeTextData.ReadOnly = false;
            attributeTextData.Size = new Size(337, 362);
            attributeTextData.TabIndex = 2;
            // 
            // DomainAttribute
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(337, 450);
            Controls.Add(attributeLayout);
            Name = "DomainAttribute";
            Text = "Domain Attribute";
            Load += DomainAttribute_Load;
            attributeLayout.ResumeLayout(false);
            attributeLayout.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Controls.TextBoxData attributeTitleData;
        private Controls.TextBoxData attributeParentTitleData;
        private Controls.RichTextBoxData attributeTextData;
    }
}