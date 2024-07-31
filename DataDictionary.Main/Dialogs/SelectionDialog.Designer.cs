namespace DataDictionary.Main.Dialogs
{
    partial class SelectionDialog
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
            TableLayoutPanel selectionLayout;
            TableLayoutPanel selectionButtonLayout;
            selectionData = new ListView();
            titleColumn = new ColumnHeader();
            titleData = new Controls.TextBoxData();
            descriptionData = new Controls.TextBoxData();
            pathData = new Controls.TextBoxData();
            okCommand = new Button();
            cancelCommand = new Button();
            scopeData = new Controls.TextBoxData();
            selectionLayout = new TableLayoutPanel();
            selectionButtonLayout = new TableLayoutPanel();
            selectionLayout.SuspendLayout();
            selectionButtonLayout.SuspendLayout();
            SuspendLayout();
            // 
            // selectionLayout
            // 
            selectionLayout.ColumnCount = 1;
            selectionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            selectionLayout.Controls.Add(selectionData, 0, 0);
            selectionLayout.Controls.Add(titleData, 0, 1);
            selectionLayout.Controls.Add(pathData, 0, 3);
            selectionLayout.Controls.Add(selectionButtonLayout, 0, 5);
            selectionLayout.Controls.Add(descriptionData, 0, 4);
            selectionLayout.Controls.Add(scopeData, 0, 2);
            selectionLayout.Dock = DockStyle.Fill;
            selectionLayout.Location = new Point(0, 0);
            selectionLayout.Name = "selectionLayout";
            selectionLayout.RowCount = 6;
            selectionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            selectionLayout.RowStyles.Add(new RowStyle());
            selectionLayout.RowStyles.Add(new RowStyle());
            selectionLayout.RowStyles.Add(new RowStyle());
            selectionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            selectionLayout.RowStyles.Add(new RowStyle());
            selectionLayout.Size = new Size(319, 437);
            selectionLayout.TabIndex = 0;
            // 
            // selectionData
            // 
            selectionData.CheckBoxes = true;
            selectionData.Columns.AddRange(new ColumnHeader[] { titleColumn });
            selectionData.Dock = DockStyle.Fill;
            selectionData.FullRowSelect = true;
            selectionData.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            selectionData.Location = new Point(3, 3);
            selectionData.MultiSelect = false;
            selectionData.Name = "selectionData";
            selectionData.Size = new Size(313, 170);
            selectionData.TabIndex = 4;
            selectionData.UseCompatibleStateImageBehavior = false;
            selectionData.View = View.Details;
            selectionData.SizeChanged += SelectionData_SizeChanged;
            // 
            // titleColumn
            // 
            titleColumn.Text = "Title";
            titleColumn.Width = 300;
            // 
            // titleData
            // 
            titleData.AutoSize = true;
            titleData.Dock = DockStyle.Fill;
            titleData.HeaderText = "Title";
            titleData.Location = new Point(3, 179);
            titleData.Multiline = false;
            titleData.Name = "titleData";
            titleData.ReadOnly = true;
            titleData.Size = new Size(313, 44);
            titleData.TabIndex = 1;
            titleData.WordWrap = true;
            // 
            // descriptionData
            // 
            descriptionData.AutoSize = true;
            descriptionData.Dock = DockStyle.Fill;
            descriptionData.HeaderText = "Description";
            descriptionData.Location = new Point(3, 329);
            descriptionData.Multiline = true;
            descriptionData.Name = "descriptionData";
            descriptionData.ReadOnly = true;
            descriptionData.Size = new Size(313, 69);
            descriptionData.TabIndex = 2;
            descriptionData.WordWrap = true;
            // 
            // pathData
            // 
            pathData.AutoSize = true;
            pathData.Dock = DockStyle.Fill;
            pathData.HeaderText = "Path";
            pathData.Location = new Point(3, 279);
            pathData.Multiline = false;
            pathData.Name = "pathData";
            pathData.ReadOnly = true;
            pathData.Size = new Size(313, 44);
            pathData.TabIndex = 5;
            pathData.WordWrap = true;
            // 
            // selectionButtonLayout
            // 
            selectionButtonLayout.AutoSize = true;
            selectionButtonLayout.ColumnCount = 3;
            selectionButtonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            selectionButtonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            selectionButtonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            selectionButtonLayout.Controls.Add(okCommand, 0, 0);
            selectionButtonLayout.Controls.Add(cancelCommand, 2, 0);
            selectionButtonLayout.Dock = DockStyle.Fill;
            selectionButtonLayout.Location = new Point(3, 404);
            selectionButtonLayout.Name = "selectionButtonLayout";
            selectionButtonLayout.RowCount = 1;
            selectionButtonLayout.RowStyles.Add(new RowStyle());
            selectionButtonLayout.Size = new Size(313, 30);
            selectionButtonLayout.TabIndex = 6;
            // 
            // okCommand
            // 
            okCommand.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            okCommand.DialogResult = DialogResult.OK;
            okCommand.Location = new Point(3, 4);
            okCommand.Name = "okCommand";
            okCommand.Size = new Size(75, 23);
            okCommand.TabIndex = 0;
            okCommand.Text = "Ok";
            okCommand.UseVisualStyleBackColor = true;
            // 
            // cancelCommand
            // 
            cancelCommand.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelCommand.Location = new Point(235, 4);
            cancelCommand.Name = "cancelCommand";
            cancelCommand.Size = new Size(75, 23);
            cancelCommand.TabIndex = 1;
            cancelCommand.Text = "Cancel";
            cancelCommand.UseVisualStyleBackColor = true;
            // 
            // scopeData
            // 
            scopeData.AutoSize = true;
            scopeData.Dock = DockStyle.Fill;
            scopeData.HeaderText = "Scope";
            scopeData.Location = new Point(3, 229);
            scopeData.Multiline = false;
            scopeData.Name = "scopeData";
            scopeData.ReadOnly = true;
            scopeData.Size = new Size(313, 44);
            scopeData.TabIndex = 7;
            scopeData.WordWrap = true;
            // 
            // SelectionDialog
            // 
            AcceptButton = okCommand;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelCommand;
            ClientSize = new Size(319, 437);
            Controls.Add(selectionLayout);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SelectionDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "SelectionDialog";
            selectionLayout.ResumeLayout(false);
            selectionLayout.PerformLayout();
            selectionButtonLayout.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private ColumnHeader titleColumn;
        private Button okCommand;
        private Button cancelCommand;
        protected ListView selectionData;
        protected Controls.TextBoxData titleData;
        protected Controls.TextBoxData descriptionData;
        protected Controls.TextBoxData pathData;
        protected Controls.TextBoxData scopeData;
    }
}