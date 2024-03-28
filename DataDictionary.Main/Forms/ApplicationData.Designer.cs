namespace DataDictionary.Main.Forms
{
    partial class ApplicationData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationData));
            openFromDatabaseCommand = new ToolStripButton();
            saveToDatabaseCommand = new ToolStripButton();
            deleteFromDatabaseCommand = new ToolStripButton();
            toolStrip = new ToolStrip();
            helpCommand = new ToolStripButton();
            rowStateCommand = new ToolStripLabel();
            helpToolStripButton = new ToolStripButton();
            toolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // openFromDatabaseCommand
            // 
            openFromDatabaseCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            openFromDatabaseCommand.Enabled = false;
            openFromDatabaseCommand.Image = Properties.Resources.OpenTable;
            openFromDatabaseCommand.ImageTransparentColor = Color.Magenta;
            openFromDatabaseCommand.Name = "openFromDatabaseCommand";
            openFromDatabaseCommand.Size = new Size(23, 22);
            openFromDatabaseCommand.Text = "&Open from Database";
            openFromDatabaseCommand.Click += OpenFromDatabaseCommand_Click;
            // 
            // saveToDatabaseCommand
            // 
            saveToDatabaseCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            saveToDatabaseCommand.Enabled = false;
            saveToDatabaseCommand.Image = Properties.Resources.SaveTable;
            saveToDatabaseCommand.ImageTransparentColor = Color.Magenta;
            saveToDatabaseCommand.Name = "saveToDatabaseCommand";
            saveToDatabaseCommand.Size = new Size(23, 22);
            saveToDatabaseCommand.Text = "&Save to Database";
            saveToDatabaseCommand.Click += SaveToDatabaseCommand_Click;
            // 
            // deleteFromDatabaseCommand
            // 
            deleteFromDatabaseCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            deleteFromDatabaseCommand.Enabled = false;
            deleteFromDatabaseCommand.Image = Properties.Resources.DeleteTable;
            deleteFromDatabaseCommand.ImageTransparentColor = Color.Magenta;
            deleteFromDatabaseCommand.Name = "deleteFromDatabaseCommand";
            deleteFromDatabaseCommand.Size = new Size(23, 22);
            deleteFromDatabaseCommand.Text = "&Delete from Database";
            deleteFromDatabaseCommand.Click += DeleteFromDatabaseCommand_Click;
            // 
            // toolStrip
            // 
            toolStrip.Items.AddRange(new ToolStripItem[] { openFromDatabaseCommand, saveToDatabaseCommand, deleteFromDatabaseCommand, helpCommand, rowStateCommand });
            toolStrip.Location = new Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(649, 25);
            toolStrip.TabIndex = 2;
            toolStrip.VisibleChanged += toolStrip_VisibleChanged;
            // 
            // helpCommand
            // 
            helpCommand.Alignment = ToolStripItemAlignment.Right;
            helpCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            helpCommand.Image = Properties.Resources.StatusHelp;
            helpCommand.ImageTransparentColor = Color.Magenta;
            helpCommand.Name = "helpCommand";
            helpCommand.Size = new Size(23, 22);
            helpCommand.Text = "Help";
            helpCommand.Click += helpToolStripButton_Click;
            // 
            // rowStateCommand
            // 
            rowStateCommand.Alignment = ToolStripItemAlignment.Right;
            rowStateCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            rowStateCommand.Image = Properties.Resources.Row;
            rowStateCommand.Name = "rowStateCommand";
            rowStateCommand.Size = new Size(16, 22);
            rowStateCommand.Text = "Row State";
            // 
            // helpToolStripButton
            // 
            helpToolStripButton.Alignment = ToolStripItemAlignment.Right;
            helpToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            helpToolStripButton.Image = (Image)resources.GetObject("helpToolStripButton.Image");
            helpToolStripButton.ImageTransparentColor = Color.Magenta;
            helpToolStripButton.Name = "helpToolStripButton";
            helpToolStripButton.Size = new Size(23, 22);
            helpToolStripButton.Text = "He&lp";
            // 
            // ApplicationData
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(649, 450);
            Controls.Add(toolStrip);
            Name = "ApplicationData";
            Text = "ApplicationData";
            Load += ApplicationData_Load;
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        protected ToolStrip toolStrip;
        protected ToolStripButton helpToolStripButton;
        private ToolStripLabel rowStateCommand;
        private ToolStripButton helpCommand;
        private ToolStripButton openFromDatabaseCommand;
        private ToolStripButton saveToDatabaseCommand;
        private ToolStripButton deleteFromDatabaseCommand;
    }
}