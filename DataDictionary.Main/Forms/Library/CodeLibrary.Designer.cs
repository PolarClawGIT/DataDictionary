namespace DataDictionary.Main.Forms.Library
{
    partial class CodeLibrary
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
            openFileDialog = new OpenFileDialog();
            libraryMemberData = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)libraryMemberData).BeginInit();
            SuspendLayout();
            // 
            // libraryMemberData
            // 
            libraryMemberData.AllowUserToAddRows = false;
            libraryMemberData.AllowUserToDeleteRows = false;
            libraryMemberData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            libraryMemberData.Dock = DockStyle.Fill;
            libraryMemberData.Location = new Point(0, 25);
            libraryMemberData.Name = "libraryMemberData";
            libraryMemberData.ReadOnly = true;
            libraryMemberData.RowTemplate.Height = 25;
            libraryMemberData.Size = new Size(800, 425);
            libraryMemberData.TabIndex = 1;
            // 
            // CodeLibrary
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(libraryMemberData);
            Name = "CodeLibrary";
            Text = "CodeLibrary";
            Controls.SetChildIndex(libraryMemberData, 0);
            ((System.ComponentModel.ISupportInitialize)libraryMemberData).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OpenFileDialog openFileDialog;
        private DataGridView libraryMemberData;
    }
}