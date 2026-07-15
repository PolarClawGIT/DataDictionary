namespace DataDictionary.Main.Controls
{
    partial class SchemaNodeTreeView
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
            TableLayoutPanel controlLayout;
            treeViewMenu = new ToolStrip();
            refreshCommand = new ToolStripButton();
            reloadCommand = new ToolStripButton();
            headerTitle = new ToolStripLabel();
            treeViewData = new TreeView();
            controlLayout = new TableLayoutPanel();
            treeViewMenu.SuspendLayout();
            controlLayout.SuspendLayout();
            SuspendLayout();
            // 
            // treeViewMenu
            // 
            treeViewMenu.GripStyle = ToolStripGripStyle.Hidden;
            treeViewMenu.Items.AddRange(new ToolStripItem[] { refreshCommand, reloadCommand, headerTitle });
            treeViewMenu.Location = new Point(0, 0);
            treeViewMenu.Name = "treeViewMenu";
            treeViewMenu.Size = new Size(188, 25);
            treeViewMenu.TabIndex = 4;
            treeViewMenu.Text = "treeViewMenu";
            // 
            // refreshCommand
            // 
            refreshCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            refreshCommand.ImageTransparentColor = Color.Magenta;
            refreshCommand.Name = "refreshCommand";
            refreshCommand.Size = new Size(23, 22);
            refreshCommand.Text = "refresh Navigation";
            // 
            // reloadCommand
            // 
            reloadCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            reloadCommand.ImageTransparentColor = Color.Magenta;
            reloadCommand.Name = "reloadCommand";
            reloadCommand.Size = new Size(23, 22);
            reloadCommand.Text = "refresh Navigation Data";
            // 
            // headerTitle
            // 
            headerTitle.Name = "headerTitle";
            headerTitle.Size = new Size(51, 22);
            headerTitle.Text = "(header)";
            // 
            // treeViewData
            // 
            treeViewData.Dock = DockStyle.Fill;
            treeViewData.Location = new Point(3, 28);
            treeViewData.Name = "treeViewData";
            treeViewData.ShowNodeToolTips = true;
            treeViewData.Size = new Size(182, 237);
            treeViewData.TabIndex = 3;
            treeViewData.BeforeCollapse += TreeViewData_BeforeCollapse;
            treeViewData.BeforeExpand += TreeViewData_BeforeExpand;
            treeViewData.NodeMouseClick += TreeViewData_NodeMouseClick;
            treeViewData.NodeMouseDoubleClick += TreeViewData_NodeMouseDoubleClick;
            // 
            // controlLayout
            // 
            controlLayout.AutoSize = true;
            controlLayout.ColumnCount = 1;
            controlLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            controlLayout.Controls.Add(treeViewMenu, 0, 0);
            controlLayout.Controls.Add(treeViewData, 0, 1);
            controlLayout.Dock = DockStyle.Fill;
            controlLayout.Location = new Point(0, 0);
            controlLayout.Name = "controlLayout";
            controlLayout.RowCount = 2;
            controlLayout.RowStyles.Add(new RowStyle());
            controlLayout.RowStyles.Add(new RowStyle());
            controlLayout.Size = new Size(188, 268);
            controlLayout.TabIndex = 5;
            // 
            // SchemaNodeTreeView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(controlLayout);
            Name = "SchemaNodeTreeView";
            Size = new Size(188, 268);
            treeViewMenu.ResumeLayout(false);
            treeViewMenu.PerformLayout();
            controlLayout.ResumeLayout(false);
            controlLayout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip treeViewMenu;
        private ToolStripButton refreshCommand;
        private ToolStripButton reloadCommand;
        private ToolStripLabel headerTitle;
        private TreeView treeViewData;
        private TableLayoutPanel controlLayout;
    }
}
