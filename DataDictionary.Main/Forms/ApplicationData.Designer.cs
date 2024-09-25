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
            toolStripSeparator = new ToolStripSeparator();
            openFromDatabaseCommand = new ToolStripButton();
            saveToDatabaseCommand = new ToolStripButton();
            deleteFromDatabaseCommand = new ToolStripButton();
            toolStrip = new ToolStrip();
            browseCommand = new ToolStripButton();
            selectCommand = new ToolStripButton();
            newCommand = new ToolStripButton();
            deleteCommand = new ToolStripButton();
            openCommand = new ToolStripButton();
            saveCommand = new ToolStripButton();
            importCommand = new ToolStripDropDownButton();
            exportCommand = new ToolStripDropDownButton();
            historyCommand = new ToolStripButton();
            helpCommand = new ToolStripButton();
            rowStateCommand = new ToolStripLabel();
            helpToolStripButton = new ToolStripButton();
            toolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new Size(6, 25);
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
            toolStrip.Items.AddRange(new ToolStripItem[] { browseCommand, selectCommand, newCommand, deleteCommand, openCommand, saveCommand, importCommand, exportCommand, toolStripSeparator, openFromDatabaseCommand, saveToDatabaseCommand, deleteFromDatabaseCommand, historyCommand, helpCommand, rowStateCommand });
            toolStrip.Location = new Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(649, 25);
            toolStrip.TabIndex = 2;
            toolStrip.VisibleChanged += ToolStrip_VisibleChanged;
            // 
            // browseCommand
            // 
            browseCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            browseCommand.Enabled = false;
            browseCommand.Image = Properties.Resources.Document;
            browseCommand.ImageTransparentColor = Color.Magenta;
            browseCommand.Name = "browseCommand";
            browseCommand.Size = new Size(23, 22);
            browseCommand.Text = "Browse";
            browseCommand.Click += BrowseCommand_Click;
            // 
            // selectCommand
            // 
            selectCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            selectCommand.Enabled = false;
            selectCommand.Image = Properties.Resources.SelectDocument;
            selectCommand.ImageTransparentColor = Color.Magenta;
            selectCommand.Name = "selectCommand";
            selectCommand.Size = new Size(23, 22);
            selectCommand.Text = "Select";
            selectCommand.Click += SelectCommand_Click;
            // 
            // newCommand
            // 
            newCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            newCommand.Enabled = false;
            newCommand.Image = Properties.Resources.NewDocument;
            newCommand.ImageTransparentColor = Color.Magenta;
            newCommand.Name = "newCommand";
            newCommand.Size = new Size(23, 22);
            newCommand.Text = "New";
            newCommand.Click += AddCommand_Click;
            // 
            // deleteCommand
            // 
            deleteCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            deleteCommand.Enabled = false;
            deleteCommand.Image = Properties.Resources.DeleteDocument;
            deleteCommand.ImageTransparentColor = Color.Magenta;
            deleteCommand.Name = "deleteCommand";
            deleteCommand.Size = new Size(23, 22);
            deleteCommand.Text = "Delete";
            deleteCommand.Click += DeleteCommand_Click;
            // 
            // openCommand
            // 
            openCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            openCommand.Enabled = false;
            openCommand.Image = Properties.Resources.OpenDocument;
            openCommand.ImageTransparentColor = Color.Magenta;
            openCommand.Name = "openCommand";
            openCommand.Size = new Size(23, 22);
            openCommand.Text = "Open";
            openCommand.Click += OpenCommand_Click;
            // 
            // saveCommand
            // 
            saveCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            saveCommand.Enabled = false;
            saveCommand.Image = Properties.Resources.SaveDocument;
            saveCommand.ImageTransparentColor = Color.Magenta;
            saveCommand.Name = "saveCommand";
            saveCommand.Size = new Size(23, 22);
            saveCommand.Text = "Save";
            saveCommand.Click += SaveCommand_Click;
            // 
            // importCommand
            // 
            importCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            importCommand.Enabled = false;
            importCommand.Image = Properties.Resources.ImportDocument;
            importCommand.ImageTransparentColor = Color.Magenta;
            importCommand.Name = "importCommand";
            importCommand.ShowDropDownArrow = false;
            importCommand.Size = new Size(20, 22);
            importCommand.Text = "Import";
            importCommand.Click += ImportCommand_Click;
            // 
            // exportCommand
            // 
            exportCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            exportCommand.Enabled = false;
            exportCommand.Image = Properties.Resources.ExportDocument;
            exportCommand.ImageTransparentColor = Color.Magenta;
            exportCommand.Name = "exportCommand";
            exportCommand.ShowDropDownArrow = false;
            exportCommand.Size = new Size(20, 22);
            exportCommand.Text = "Export";
            exportCommand.Click += ExportCommand_Click;
            // 
            // historyCommand
            // 
            historyCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            historyCommand.Enabled = false;
            historyCommand.Image = Properties.Resources.HistoryTable;
            historyCommand.ImageTransparentColor = Color.Magenta;
            historyCommand.Name = "historyCommand";
            historyCommand.Size = new Size(23, 22);
            historyCommand.Text = "Table History";
            historyCommand.Click += HistoryCommand_Click;
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
        protected ToolStripButton helpToolStripButton;
        private ToolStripLabel rowStateCommand;
        private ToolStripButton helpCommand;
        private ToolStripButton openFromDatabaseCommand;
        private ToolStripButton saveToDatabaseCommand;
        private ToolStripButton deleteFromDatabaseCommand;
        private ToolStripButton browseCommand;
        private ToolStripButton newCommand;
        private ToolStripButton deleteCommand;
        private ToolStripButton saveCommand;
        private ToolStripButton openCommand;
        protected ToolStrip toolStrip;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripDropDownButton importCommand;
        private ToolStripDropDownButton exportCommand;
        private ToolStripButton historyCommand;
        private ToolStripButton selectCommand;
    }
}