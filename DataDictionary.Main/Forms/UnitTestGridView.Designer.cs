namespace DataDictionary.Main.Forms
{
    partial class UnitTestGridView
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
            unitTestData = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)unitTestData).BeginInit();
            SuspendLayout();
            // 
            // unitTestData
            // 
            unitTestData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            unitTestData.Dock = DockStyle.Fill;
            unitTestData.Location = new Point(0, 0);
            unitTestData.Name = "unitTestData";
            unitTestData.RowTemplate.Height = 25;
            unitTestData.Size = new Size(800, 450);
            unitTestData.TabIndex = 0;
            // 
            // UnitTestGridView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(unitTestData);
            Name = "UnitTestGridView";
            Text = "Unit Test for Grid View";
            Load += UnitTestGridView_Load;
            ((System.ComponentModel.ISupportInitialize)unitTestData).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView unitTestData;
    }
}