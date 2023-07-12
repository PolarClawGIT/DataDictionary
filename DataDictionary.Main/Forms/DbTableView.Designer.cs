namespace DataDictionary.Main.Forms
{
    partial class DbTableView
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
            tableData = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)tableData).BeginInit();
            SuspendLayout();
            // 
            // tableData
            // 
            tableData.AllowUserToAddRows = false;
            tableData.AllowUserToDeleteRows = false;
            tableData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tableData.Dock = DockStyle.Fill;
            tableData.Location = new Point(0, 0);
            tableData.Name = "tableData";
            tableData.ReadOnly = true;
            tableData.RowTemplate.Height = 25;
            tableData.Size = new Size(665, 390);
            tableData.TabIndex = 0;
            // 
            // DbTableView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(665, 390);
            Controls.Add(tableData);
            Name = "DbTableView";
            Text = "Database Tables";
            Load += DbTableView_Load;
            ((System.ComponentModel.ISupportInitialize)tableData).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView tableData;
    }
}