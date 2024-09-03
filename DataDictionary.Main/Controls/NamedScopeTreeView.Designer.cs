namespace DataDictionary.Main.Controls
{
    partial class NamedScopeTreeView
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
            TableLayoutPanel treeViewLayout;
            treeViewMenu = new ToolStrip();
            refreshCommand = new ToolStripButton();
            reloadCommand = new ToolStripButton();
            treeViewData = new TreeView();
            treeViewLayout = new TableLayoutPanel();
            treeViewLayout.SuspendLayout();
            treeViewMenu.SuspendLayout();
            SuspendLayout();
            // 
            // treeViewLayout
            // 
            treeViewLayout.ColumnCount = 1;
            treeViewLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            treeViewLayout.Controls.Add(treeViewMenu, 0, 0);
            treeViewLayout.Controls.Add(treeViewData, 0, 1);
            treeViewLayout.Dock = DockStyle.Fill;
            treeViewLayout.Location = new Point(0, 0);
            treeViewLayout.Name = "treeViewLayout";
            treeViewLayout.RowCount = 2;
            treeViewLayout.RowStyles.Add(new RowStyle());
            treeViewLayout.RowStyles.Add(new RowStyle());
            treeViewLayout.Size = new Size(207, 351);
            treeViewLayout.TabIndex = 3;
            // 
            // treeViewMenu
            // 
            treeViewMenu.GripStyle = ToolStripGripStyle.Hidden;
            treeViewMenu.Items.AddRange(new ToolStripItem[] { refreshCommand, reloadCommand });
            treeViewMenu.Location = new Point(0, 0);
            treeViewMenu.Name = "treeViewMenu";
            treeViewMenu.Size = new Size(207, 25);
            treeViewMenu.TabIndex = 2;
            treeViewMenu.Text = "treeViewMenu";
            // 
            // refreshCommand
            // 
            refreshCommand.Alignment = ToolStripItemAlignment.Right;
            refreshCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            refreshCommand.Image = Properties.Resources.RefreshTree;
            refreshCommand.ImageTransparentColor = Color.Magenta;
            refreshCommand.Name = "refreshCommand";
            refreshCommand.Size = new Size(23, 22);
            refreshCommand.Text = "refresh View";
            refreshCommand.Click += RefreshCommand_Click;
            // 
            // reloadCommand
            // 
            reloadCommand.Alignment = ToolStripItemAlignment.Right;
            reloadCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            reloadCommand.Image = Properties.Resources.ReloadTree;
            reloadCommand.ImageTransparentColor = Color.Magenta;
            reloadCommand.Name = "reloadCommand";
            reloadCommand.Size = new Size(23, 22);
            reloadCommand.Text = "refresh Data";
            reloadCommand.Click += ReloadCommand_Click;
            // 
            // treeViewData
            // 
            treeViewData.Dock = DockStyle.Fill;
            treeViewData.Location = new Point(3, 28);
            treeViewData.Name = "treeViewData";
            treeViewData.ShowNodeToolTips = true;
            treeViewData.Size = new Size(201, 320);
            treeViewData.TabIndex = 1;
            treeViewData.BeforeCollapse += TreeViewData_BeforeCollapse;
            treeViewData.BeforeExpand += TreeViewData_BeforeExpand;
            treeViewData.NodeMouseClick += TreeViewData_NodeMouseClick;
            treeViewData.NodeMouseDoubleClick += TreeViewData_NodeMouseDoubleClick;
            // 
            // NamedScopeTreeView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(treeViewLayout);
            Name = "NamedScopeTreeView";
            Size = new Size(207, 351);
            treeViewLayout.ResumeLayout(false);
            treeViewLayout.PerformLayout();
            treeViewMenu.ResumeLayout(false);
            treeViewMenu.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TreeView treeViewData;
        private ToolStrip treeViewMenu;
        private ToolStripButton refreshCommand;
        private ToolStripButton reloadCommand;
    }
}
