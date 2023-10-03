namespace DataDictionary.Main.Forms.Library
{
    partial class LibrarySource
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
            components = new System.ComponentModel.Container();
            TableLayoutPanel libraryManagerLayout;
            libraryTitleData = new Controls.TextBoxData();
            libraryDescriptionData = new Controls.TextBoxData();
            asseblyNameData = new Controls.TextBoxData();
            sourceFileNameData = new Controls.TextBoxData();
            sourceFileDate = new Controls.TextBoxData();
            errorProvider = new ErrorProvider(components);
            libraryManagerLayout = new TableLayoutPanel();
            libraryManagerLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // libraryManagerLayout
            // 
            libraryManagerLayout.ColumnCount = 2;
            libraryManagerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            libraryManagerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            libraryManagerLayout.Controls.Add(libraryTitleData, 0, 0);
            libraryManagerLayout.Controls.Add(libraryDescriptionData, 0, 1);
            libraryManagerLayout.Controls.Add(asseblyNameData, 0, 2);
            libraryManagerLayout.Controls.Add(sourceFileNameData, 0, 3);
            libraryManagerLayout.Controls.Add(sourceFileDate, 1, 3);
            libraryManagerLayout.Dock = DockStyle.Fill;
            libraryManagerLayout.Location = new Point(0, 25);
            libraryManagerLayout.Name = "libraryManagerLayout";
            libraryManagerLayout.RowCount = 4;
            libraryManagerLayout.RowStyles.Add(new RowStyle());
            libraryManagerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            libraryManagerLayout.RowStyles.Add(new RowStyle());
            libraryManagerLayout.RowStyles.Add(new RowStyle());
            libraryManagerLayout.Size = new Size(468, 405);
            libraryManagerLayout.TabIndex = 2;
            // 
            // libraryTitleData
            // 
            libraryTitleData.AutoSize = true;
            libraryManagerLayout.SetColumnSpan(libraryTitleData, 2);
            libraryTitleData.Dock = DockStyle.Fill;
            libraryTitleData.HeaderText = "Library Title";
            libraryTitleData.Location = new Point(3, 3);
            libraryTitleData.Multiline = false;
            libraryTitleData.Name = "libraryTitleData";
            libraryTitleData.ReadOnly = false;
            libraryTitleData.Size = new Size(462, 44);
            libraryTitleData.TabIndex = 1;
            libraryTitleData.Validating += libraryTitleData_Validating;
            // 
            // libraryDescriptionData
            // 
            libraryDescriptionData.AutoSize = true;
            libraryManagerLayout.SetColumnSpan(libraryDescriptionData, 2);
            libraryDescriptionData.Dock = DockStyle.Fill;
            libraryDescriptionData.HeaderText = "Library Description";
            libraryDescriptionData.Location = new Point(3, 53);
            libraryDescriptionData.Multiline = true;
            libraryDescriptionData.Name = "libraryDescriptionData";
            libraryDescriptionData.ReadOnly = false;
            libraryDescriptionData.Size = new Size(462, 249);
            libraryDescriptionData.TabIndex = 2;
            // 
            // asseblyNameData
            // 
            asseblyNameData.AutoSize = true;
            libraryManagerLayout.SetColumnSpan(asseblyNameData, 2);
            asseblyNameData.Dock = DockStyle.Fill;
            asseblyNameData.HeaderText = "Assembly Name";
            asseblyNameData.Location = new Point(3, 308);
            asseblyNameData.Multiline = false;
            asseblyNameData.Name = "asseblyNameData";
            asseblyNameData.ReadOnly = true;
            asseblyNameData.Size = new Size(462, 44);
            asseblyNameData.TabIndex = 3;
            asseblyNameData.Validating += asseblyNameData_Validating;
            // 
            // sourceFileNameData
            // 
            sourceFileNameData.AutoSize = true;
            sourceFileNameData.Dock = DockStyle.Fill;
            sourceFileNameData.HeaderText = "Source File";
            sourceFileNameData.Location = new Point(3, 358);
            sourceFileNameData.Multiline = false;
            sourceFileNameData.Name = "sourceFileNameData";
            sourceFileNameData.ReadOnly = true;
            sourceFileNameData.Size = new Size(321, 44);
            sourceFileNameData.TabIndex = 4;
            // 
            // sourceFileDate
            // 
            sourceFileDate.AutoSize = true;
            sourceFileDate.Dock = DockStyle.Fill;
            sourceFileDate.HeaderText = "Source File Date";
            sourceFileDate.Location = new Point(330, 358);
            sourceFileDate.Multiline = false;
            sourceFileDate.Name = "sourceFileDate";
            sourceFileDate.ReadOnly = true;
            sourceFileDate.Size = new Size(135, 44);
            sourceFileDate.TabIndex = 7;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // LibrarySource
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(468, 430);
            Controls.Add(libraryManagerLayout);
            Name = "LibrarySource";
            Text = "LibrarySource";
            Load += LibrarySource_Load;
            Controls.SetChildIndex(libraryManagerLayout, 0);
            libraryManagerLayout.ResumeLayout(false);
            libraryManagerLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData libraryTitleData;
        private Controls.TextBoxData libraryDescriptionData;
        private Controls.TextBoxData asseblyNameData;
        private Controls.TextBoxData sourceFileNameData;
        private Controls.TextBoxData sourceFileDate;
        private ErrorProvider errorProvider;
    }
}