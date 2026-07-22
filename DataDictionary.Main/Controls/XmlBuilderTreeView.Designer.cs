namespace DataDictionary.Main.Controls
{
    partial class XmlBuilderTreeView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XmlBuilderTreeView));
            treeViewMenu = new ToolStrip();
            viewDetailsCommand = new ToolStripButton();
            overrideCommand = new ToolStripButton();
            useDefaultCommand = new ToolStripButton();
            headerTitle = new ToolStripLabel();
            treeViewData = new TreeView();
            controlLayout = new TableLayoutPanel();
            controlLayout.SuspendLayout();
            treeViewMenu.SuspendLayout();
            SuspendLayout();
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
            // treeViewMenu
            // 
            treeViewMenu.CanOverflow = false;
            treeViewMenu.GripStyle = ToolStripGripStyle.Hidden;
            treeViewMenu.Items.AddRange(new ToolStripItem[] { viewDetailsCommand, overrideCommand, useDefaultCommand, headerTitle });
            treeViewMenu.Location = new Point(0, 0);
            treeViewMenu.Name = "treeViewMenu";
            treeViewMenu.Size = new Size(188, 25);
            treeViewMenu.TabIndex = 4;
            treeViewMenu.Text = "treeViewMenu";
            // 
            // viewDetailsCommand
            // 
            viewDetailsCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            viewDetailsCommand.Image = (Image)resources.GetObject("viewDetailsCommand.Image");
            viewDetailsCommand.ImageTransparentColor = Color.Magenta;
            viewDetailsCommand.Name = "viewDetailsCommand";
            viewDetailsCommand.Size = new Size(23, 22);
            viewDetailsCommand.Text = "View Details";
            viewDetailsCommand.Click += ViewDetailsCommand_Click;
            // 
            // overrideCommand
            // 
            overrideCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            overrideCommand.Image = (Image)resources.GetObject("overrideCommand.Image");
            overrideCommand.ImageTransparentColor = Color.Magenta;
            overrideCommand.Name = "overrideCommand";
            overrideCommand.Size = new Size(23, 22);
            overrideCommand.Text = "toolStripButton1";
            overrideCommand.ToolTipText = "Override Defaults";
            overrideCommand.Click += OverrideCommand_Click;
            // 
            // useDefaultCommand
            // 
            useDefaultCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            useDefaultCommand.Image = (Image)resources.GetObject("useDefaultCommand.Image");
            useDefaultCommand.ImageTransparentColor = Color.Magenta;
            useDefaultCommand.Name = "useDefaultCommand";
            useDefaultCommand.Size = new Size(23, 22);
            useDefaultCommand.Text = "toolStripButton1";
            useDefaultCommand.ToolTipText = "Use Defaults";
            useDefaultCommand.Click += UseDefaultCommand_Click;
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
            // XmlBuilderTreeView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(controlLayout);
            Name = "XmlBuilderTreeView";
            Size = new Size(188, 268);
            controlLayout.ResumeLayout(false);
            controlLayout.PerformLayout();
            treeViewMenu.ResumeLayout(false);
            treeViewMenu.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip treeViewMenu;
        private ToolStripLabel headerTitle;
        private TreeView treeViewData;
        private TableLayoutPanel controlLayout;
        private ToolStripButton useDefaultCommand;
        private ToolStripButton overrideCommand;
        private ToolStripButton viewDetailsCommand;
    }
}
