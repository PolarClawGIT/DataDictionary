namespace DataDictionary.Main.Forms
{
    partial class ApplicationBase
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
            ToolStripSeparator toolStripSeparator;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationBase));
            toolStrip = new ToolStrip();
            toolStripContextMenuPlaceHolder = new ToolStripLabel();
            helpToolStripButton = new ToolStripButton();
            rowStateCommand = new ToolStripLabel();
            cutToolStripButton = new ToolStripButton();
            copyToolStripButton = new ToolStripButton();
            pasteToolStripButton = new ToolStripButton();
            openFromDatabaseCommand = new ToolStripButton();
            saveToDatabaseCommand = new ToolStripButton();
            deleteFromDatabaseCommand = new ToolStripButton();
            toolTip = new ToolTip(components);
            databaseCommands = new ContextMenuStrip(components);
            dummyItem = new ToolStripMenuItem();
            clipboardCommands = new ContextMenuStrip(components);
            dummy = new ToolStripMenuItem();
            toolStripSeparator = new ToolStripSeparator();
            toolStrip.SuspendLayout();
            databaseCommands.SuspendLayout();
            clipboardCommands.SuspendLayout();
            SuspendLayout();
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new Size(6, 25);
            // 
            // toolStrip
            // 
            toolStrip.Items.AddRange(new ToolStripItem[] { toolStripContextMenuPlaceHolder, helpToolStripButton, rowStateCommand });
            toolStrip.Location = new Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(800, 25);
            toolStrip.TabIndex = 0;
            toolStrip.VisibleChanged += toolStrip_VisibleChanged;
            // 
            // toolStripContextMenuPlaceHolder
            // 
            toolStripContextMenuPlaceHolder.MergeIndex = 1;
            toolStripContextMenuPlaceHolder.Name = "toolStripContextMenuPlaceHolder";
            toolStripContextMenuPlaceHolder.Size = new Size(119, 22);
            toolStripContextMenuPlaceHolder.Text = "Context Place Holder";
            toolStripContextMenuPlaceHolder.ToolTipText = "This is a Place Holder for Context Menu Items";
            toolStripContextMenuPlaceHolder.Visible = false;
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
            helpToolStripButton.Click += helpToolStripButton_Click;
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
            // cutToolStripButton
            // 
            cutToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            cutToolStripButton.Enabled = false;
            cutToolStripButton.Image = (Image)resources.GetObject("cutToolStripButton.Image");
            cutToolStripButton.ImageTransparentColor = Color.Magenta;
            cutToolStripButton.Name = "cutToolStripButton";
            cutToolStripButton.Size = new Size(23, 20);
            cutToolStripButton.Text = "C&ut";
            // 
            // copyToolStripButton
            // 
            copyToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            copyToolStripButton.Enabled = false;
            copyToolStripButton.Image = (Image)resources.GetObject("copyToolStripButton.Image");
            copyToolStripButton.ImageTransparentColor = Color.Magenta;
            copyToolStripButton.Name = "copyToolStripButton";
            copyToolStripButton.Size = new Size(23, 20);
            copyToolStripButton.Text = "&Copy";
            // 
            // pasteToolStripButton
            // 
            pasteToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            pasteToolStripButton.Enabled = false;
            pasteToolStripButton.Image = (Image)resources.GetObject("pasteToolStripButton.Image");
            pasteToolStripButton.ImageTransparentColor = Color.Magenta;
            pasteToolStripButton.Name = "pasteToolStripButton";
            pasteToolStripButton.Size = new Size(23, 20);
            pasteToolStripButton.Text = "&Paste";
            // 
            // openFromDatabaseCommand
            // 
            openFromDatabaseCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            openFromDatabaseCommand.Enabled = false;
            openFromDatabaseCommand.Image = Properties.Resources.OpenTable;
            openFromDatabaseCommand.ImageTransparentColor = Color.Magenta;
            openFromDatabaseCommand.Name = "openFromDatabaseCommand";
            openFromDatabaseCommand.Size = new Size(23, 20);
            openFromDatabaseCommand.Text = "&Open from Database";
            // 
            // saveToDatabaseCommand
            // 
            saveToDatabaseCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            saveToDatabaseCommand.Enabled = false;
            saveToDatabaseCommand.Image = Properties.Resources.SaveTable;
            saveToDatabaseCommand.ImageTransparentColor = Color.Magenta;
            saveToDatabaseCommand.Name = "saveToDatabaseCommand";
            saveToDatabaseCommand.Size = new Size(23, 20);
            saveToDatabaseCommand.Text = "&Save to Database";
            // 
            // deleteFromDatabaseCommand
            // 
            deleteFromDatabaseCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            deleteFromDatabaseCommand.Enabled = false;
            deleteFromDatabaseCommand.Image = Properties.Resources.DeleteTable;
            deleteFromDatabaseCommand.ImageTransparentColor = Color.Magenta;
            deleteFromDatabaseCommand.Name = "deleteFromDatabaseCommand";
            deleteFromDatabaseCommand.Size = new Size(23, 20);
            deleteFromDatabaseCommand.Text = "&Delete from Database";
            // 
            // databaseCommands
            // 
            databaseCommands.Items.AddRange(new ToolStripItem[] { openFromDatabaseCommand, saveToDatabaseCommand, deleteFromDatabaseCommand });
            databaseCommands.Name = "databaseCommands";
            databaseCommands.Size = new Size(84, 73);
            // 
            // dummyItem
            // 
            dummyItem.Name = "dummyItem";
            dummyItem.Size = new Size(32, 19);
            // 
            // clipboardCommands
            // 
            clipboardCommands.Items.AddRange(new ToolStripItem[] { cutToolStripButton, copyToolStripButton, pasteToolStripButton });
            clipboardCommands.Name = "clipboardCommands";
            clipboardCommands.Size = new Size(84, 73);
            // 
            // dummy
            // 
            dummy.Name = "dummy";
            dummy.Size = new Size(32, 19);
            // 
            // ApplicationBase
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(toolStrip);
            Name = "ApplicationBase";
            Text = "ApplicationBase";
            Load += ApplicationBase_Load;
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            databaseCommands.ResumeLayout(false);
            clipboardCommands.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        protected ToolStrip toolStrip;
        protected ToolStripButton cutToolStripButton;
        protected ToolStripButton copyToolStripButton;
        protected ToolStripButton pasteToolStripButton;
        protected ToolStripButton helpToolStripButton;
        protected ToolStripButton openFromDatabaseCommand;
        protected ToolStripButton saveToDatabaseCommand;
        protected ToolStripButton deleteFromDatabaseCommand;
        private ToolStripLabel rowStateCommand;
        protected ToolTip toolTip;
        private ToolStripLabel toolStripContextMenuPlaceHolder;
        protected ContextMenuStrip databaseCommands;
        private ToolStripMenuItem dummyItem;
        private ToolStripMenuItem dummy;
        protected ContextMenuStrip clipboardCommands;
    }
}