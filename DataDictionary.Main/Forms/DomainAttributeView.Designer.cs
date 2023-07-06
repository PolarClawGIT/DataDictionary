namespace DataDictionary.Main.Forms
{
    partial class DomainAttributeView
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
            attributeData = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)attributeData).BeginInit();
            SuspendLayout();
            // 
            // attributeData
            // 
            attributeData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            attributeData.Dock = DockStyle.Fill;
            attributeData.Location = new Point(0, 0);
            attributeData.Name = "attributeData";
            attributeData.RowTemplate.Height = 25;
            attributeData.Size = new Size(461, 319);
            attributeData.TabIndex = 0;
            // 
            // DomainAttributeView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(461, 319);
            Controls.Add(attributeData);
            Name = "DomainAttributeView";
            Text = "Domain Attributes";
            Load += DomainAttributeView_Load;
            ((System.ComponentModel.ISupportInitialize)attributeData).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView attributeData;
    }
}