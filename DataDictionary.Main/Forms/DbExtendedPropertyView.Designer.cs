namespace DataDictionary.Main.Forms
{
    partial class DbExtendedPropertyView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbExtendedPropertyView));
            extendedPropertyData = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)extendedPropertyData).BeginInit();
            SuspendLayout();
            // 
            // extendedPropertyData
            // 
            extendedPropertyData.AllowUserToAddRows = false;
            extendedPropertyData.AllowUserToDeleteRows = false;
            extendedPropertyData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            extendedPropertyData.Dock = DockStyle.Fill;
            extendedPropertyData.Location = new Point(0, 0);
            extendedPropertyData.Name = "extendedPropertyData";
            extendedPropertyData.ReadOnly = true;
            extendedPropertyData.RowTemplate.Height = 25;
            extendedPropertyData.Size = new Size(800, 450);
            extendedPropertyData.TabIndex = 0;
            // 
            // DbExtendedPropertyView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(extendedPropertyData);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "DbExtendedPropertyView";
            Text = "Extended Properties";
            Load += DbExtendedPropertyView_Load;
            ((System.ComponentModel.ISupportInitialize)extendedPropertyData).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView extendedPropertyData;
    }
}