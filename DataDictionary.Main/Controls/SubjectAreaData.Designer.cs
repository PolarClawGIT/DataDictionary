namespace DataDictionary.Main.Controls
{
    partial class SubjectAreaData
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
            subjectAreaList = new ListView();
            subjectAreaColumn = new ColumnHeader();
            subjectNameSpaceColumn = new ColumnHeader();
            SuspendLayout();
            // 
            // subjectAreaList
            // 
            subjectAreaList.CheckBoxes = true;
            subjectAreaList.Columns.AddRange(new ColumnHeader[] { subjectAreaColumn, subjectNameSpaceColumn });
            subjectAreaList.Dock = DockStyle.Fill;
            subjectAreaList.Location = new Point(0, 0);
            subjectAreaList.Name = "subjectAreaList";
            subjectAreaList.Size = new Size(330, 350);
            subjectAreaList.TabIndex = 0;
            subjectAreaList.UseCompatibleStateImageBehavior = false;
            subjectAreaList.View = View.Details;
            subjectAreaList.ItemChecked += SubjectAreaData_ItemChecked;
            subjectAreaList.Resize += subjectAreaData_Resize;
            // 
            // subjectAreaColumn
            // 
            subjectAreaColumn.Text = "Subject";
            // 
            // subjectNameSpaceColumn
            // 
            subjectNameSpaceColumn.Text = "NameSpace";
            // 
            // SubjectAreaData
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(subjectAreaList);
            Name = "SubjectAreaData";
            Size = new Size(330, 350);
            ResumeLayout(false);
        }

        #endregion

        private ListView subjectAreaList;
        private ColumnHeader subjectAreaColumn;
        private ColumnHeader subjectNameSpaceColumn;
    }
}
