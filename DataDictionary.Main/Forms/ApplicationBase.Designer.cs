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
            ToolStripSeparator toolStripSeparator1;
            ToolStripSeparator toolStripSeparator2;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationBase));
            toolStrip = new ToolStrip();
            newItemCommand = new ToolStripButton();
            deleteItemCommand = new ToolStripButton();
            importDataCommand = new ToolStripSplitButton();
            openFromDatabaseCommand = new ToolStripButton();
            saveToDatabaseCommand = new ToolStripButton();
            deleteFromDatabaseCommand = new ToolStripButton();
            cutToolStripButton = new ToolStripButton();
            copyToolStripButton = new ToolStripButton();
            pasteToolStripButton = new ToolStripButton();
            helpToolStripButton = new ToolStripButton();
            rowStateCommand = new ToolStripLabel();
            toolTip = new ToolTip(components);
            exportDataCommand = new ToolStripSplitButton();
            toolStripSeparator = new ToolStripSeparator();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new Size(6, 25);
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // toolStrip
            // 
            toolStrip.Items.AddRange(new ToolStripItem[] { newItemCommand, deleteItemCommand, importDataCommand, exportDataCommand, toolStripSeparator2, openFromDatabaseCommand, saveToDatabaseCommand, deleteFromDatabaseCommand, toolStripSeparator, cutToolStripButton, copyToolStripButton, pasteToolStripButton, toolStripSeparator1, helpToolStripButton, rowStateCommand });
            toolStrip.Location = new Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(800, 25);
            toolStrip.TabIndex = 0;
            toolStrip.VisibleChanged += toolStrip_VisibleChanged;
            // 
            // newItemCommand
            // 
            newItemCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            newItemCommand.Enabled = false;
            newItemCommand.Image = Properties.Resources.NewDocument;
            newItemCommand.ImageTransparentColor = Color.Magenta;
            newItemCommand.Name = "newItemCommand";
            newItemCommand.Size = new Size(23, 22);
            newItemCommand.Text = "New Item";
            // 
            // deleteItemCommand
            // 
            deleteItemCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            deleteItemCommand.Enabled = false;
            deleteItemCommand.Image = Properties.Resources.DeleteDocument;
            deleteItemCommand.ImageTransparentColor = Color.Magenta;
            deleteItemCommand.Name = "deleteItemCommand";
            deleteItemCommand.Size = new Size(23, 22);
            deleteItemCommand.Text = "Delete Item";
            // 
            // importDataCommand
            // 
            importDataCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            importDataCommand.Enabled = false;
            importDataCommand.Image = Properties.Resources.ImportCatalogPart;
            importDataCommand.ImageTransparentColor = Color.Magenta;
            importDataCommand.Name = "importDataCommand";
            importDataCommand.Size = new Size(32, 22);
            importDataCommand.Text = "Import Data";
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
            // 
            // cutToolStripButton
            // 
            cutToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            cutToolStripButton.Enabled = false;
            cutToolStripButton.Image = (Image)resources.GetObject("cutToolStripButton.Image");
            cutToolStripButton.ImageTransparentColor = Color.Magenta;
            cutToolStripButton.Name = "cutToolStripButton";
            cutToolStripButton.Size = new Size(23, 22);
            cutToolStripButton.Text = "C&ut";
            // 
            // copyToolStripButton
            // 
            copyToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            copyToolStripButton.Enabled = false;
            copyToolStripButton.Image = (Image)resources.GetObject("copyToolStripButton.Image");
            copyToolStripButton.ImageTransparentColor = Color.Magenta;
            copyToolStripButton.Name = "copyToolStripButton";
            copyToolStripButton.Size = new Size(23, 22);
            copyToolStripButton.Text = "&Copy";
            // 
            // pasteToolStripButton
            // 
            pasteToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            pasteToolStripButton.Enabled = false;
            pasteToolStripButton.Image = (Image)resources.GetObject("pasteToolStripButton.Image");
            pasteToolStripButton.ImageTransparentColor = Color.Magenta;
            pasteToolStripButton.Name = "pasteToolStripButton";
            pasteToolStripButton.Size = new Size(23, 22);
            pasteToolStripButton.Text = "&Paste";
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
            // exportDataCommand
            // 
            exportDataCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            exportDataCommand.Enabled = false;
            exportDataCommand.Image = Properties.Resources.ExportCatalogPart;
            exportDataCommand.ImageTransparentColor = Color.Magenta;
            exportDataCommand.Name = "exportDataCommand";
            exportDataCommand.Size = new Size(32, 22);
            exportDataCommand.Text = "Export Data";
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
        protected ToolStripButton newItemCommand;
        protected ToolStripButton deleteItemCommand;
        protected ToolStripSplitButton importDataCommand;
        private ToolStripLabel rowStateCommand;
        protected ToolTip toolTip;
        protected ToolStripSplitButton exportDataCommand;
    }
}