namespace DataDictionary.Main.Forms
{
    partial class DbTableColumnView
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
            columnData = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)columnData).BeginInit();
            SuspendLayout();
            // 
            // columnData
            // 
            columnData.AllowUserToAddRows = false;
            columnData.AllowUserToDeleteRows = false;
            columnData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            columnData.Dock = DockStyle.Fill;
            columnData.Location = new Point(0, 0);
            columnData.Name = "columnData";
            columnData.ReadOnly = true;
            columnData.RowTemplate.Height = 25;
            columnData.Size = new Size(800, 450);
            columnData.TabIndex = 0;
            // 
            // DbColumnView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(columnData);
            Name = "DbColumnView";
            Text = "Database Columns";
            Load += DbColumnView_Load;
            ((System.ComponentModel.ISupportInitialize)columnData).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView columnData;
    }
}