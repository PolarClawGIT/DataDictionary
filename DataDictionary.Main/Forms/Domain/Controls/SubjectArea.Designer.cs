namespace DataDictionary.Main.Forms.Domain.Controls
{
    partial class SubjectArea
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            subjectAreaData = new ListView();
            subjectAreaColumn = new ColumnHeader();
            subjectNameSpaceColumn = new ColumnHeader();
            SuspendLayout();
            // 
            // subjectAreaData
            // 
            subjectAreaData.CheckBoxes = true;
            subjectAreaData.Columns.AddRange(new ColumnHeader[] { subjectAreaColumn, subjectNameSpaceColumn });
            subjectAreaData.Dock = DockStyle.Fill;
            subjectAreaData.Location = new Point(0, 0);
            subjectAreaData.Name = "subjectAreaData";
            subjectAreaData.Size = new Size(136, 114);
            subjectAreaData.TabIndex = 0;
            subjectAreaData.UseCompatibleStateImageBehavior = false;
            subjectAreaData.View = View.Details;
            subjectAreaData.ItemChecked += SubjectAreaData_ItemChecked;
            subjectAreaData.Resize += subjectAreaData_Resize;
            // 
            // subjectAreaColumn
            // 
            subjectAreaColumn.Text = "Subject";
            // 
            // subjectNameSpaceColumn
            // 
            subjectNameSpaceColumn.Text = "NameSpace";
            // 
            // SubjectArea
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(subjectAreaData);
            Name = "SubjectArea";
            Size = new Size(136, 114);
            ResumeLayout(false);
        }

        #endregion

        private ListView subjectAreaData;
        private ColumnHeader subjectAreaColumn;
        private ColumnHeader subjectNameSpaceColumn;
    }
}
