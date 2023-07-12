namespace DataDictionary.Main.Forms
{
    partial class DbSchemaView
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
            schemaData = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)schemaData).BeginInit();
            SuspendLayout();
            // 
            // schemaData
            // 
            schemaData.AllowUserToAddRows = false;
            schemaData.AllowUserToDeleteRows = false;
            schemaData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            schemaData.Dock = DockStyle.Fill;
            schemaData.Location = new Point(0, 0);
            schemaData.Name = "schemaData";
            schemaData.ReadOnly = true;
            schemaData.RowTemplate.Height = 25;
            schemaData.Size = new Size(516, 343);
            schemaData.TabIndex = 0;
            // 
            // DbSchemaView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(516, 343);
            Controls.Add(schemaData);
            Name = "DbSchemaView";
            Text = "Database Schemas";
            Load += DbSchemaView_Load;
            ((System.ComponentModel.ISupportInitialize)schemaData).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView schemaData;
    }
}