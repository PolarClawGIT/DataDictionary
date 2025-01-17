namespace DataDictionary.Main.Controls
{
    partial class NavigatorData
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
            ToolStripLabel numberSeperator;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NavigatorData));
            navigationStrip = new ToolStrip();
            moveFirstCommand = new ToolStripButton();
            movePreviousCommand = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            currentRowNumber = new ToolStripLabel();
            totalRowNumber = new ToolStripLabel();
            toolStripSeparator2 = new ToolStripSeparator();
            moveNextCommand = new ToolStripButton();
            moveLastCommand = new ToolStripButton();
            numberSeperator = new ToolStripLabel();
            navigationStrip.SuspendLayout();
            SuspendLayout();
            // 
            // numberSeperator
            // 
            numberSeperator.Name = "numberSeperator";
            numberSeperator.Size = new Size(10, 22);
            numberSeperator.Text = ":";
            // 
            // navigationStrip
            // 
            navigationStrip.GripStyle = ToolStripGripStyle.Hidden;
            navigationStrip.Items.AddRange(new ToolStripItem[] { moveFirstCommand, movePreviousCommand, toolStripSeparator1, currentRowNumber, numberSeperator, totalRowNumber, toolStripSeparator2, moveNextCommand, moveLastCommand });
            navigationStrip.Location = new Point(0, 0);
            navigationStrip.Name = "navigationStrip";
            navigationStrip.Size = new Size(185, 25);
            navigationStrip.TabIndex = 0;
            navigationStrip.Text = "Navigation Strip";
            // 
            // moveFirstCommand
            // 
            moveFirstCommand.DisplayStyle = ToolStripItemDisplayStyle.Text;
            moveFirstCommand.Image = (Image)resources.GetObject("moveFirstCommand.Image");
            moveFirstCommand.ImageTransparentColor = Color.Magenta;
            moveFirstCommand.Name = "moveFirstCommand";
            moveFirstCommand.Size = new Size(23, 22);
            moveFirstCommand.Text = "|<";
            moveFirstCommand.ToolTipText = "Move First";
            moveFirstCommand.Click += MoveFirstCommand_Click;
            // 
            // movePreviousCommand
            // 
            movePreviousCommand.DisplayStyle = ToolStripItemDisplayStyle.Text;
            movePreviousCommand.Image = (Image)resources.GetObject("movePreviousCommand.Image");
            movePreviousCommand.ImageTransparentColor = Color.Magenta;
            movePreviousCommand.Name = "movePreviousCommand";
            movePreviousCommand.Size = new Size(24, 22);
            movePreviousCommand.Text = "<-";
            movePreviousCommand.ToolTipText = "move Previous";
            movePreviousCommand.Click += MovePerviousCommand_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // currentRowNumber
            // 
            currentRowNumber.Name = "currentRowNumber";
            currentRowNumber.Size = new Size(21, 22);
            currentRowNumber.Text = "##";
            currentRowNumber.ToolTipText = "Current Row Number";
            // 
            // totalRowNumber
            // 
            totalRowNumber.Name = "totalRowNumber";
            totalRowNumber.Size = new Size(21, 22);
            totalRowNumber.Text = "##";
            totalRowNumber.ToolTipText = "Total Rows";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // moveNextCommand
            // 
            moveNextCommand.DisplayStyle = ToolStripItemDisplayStyle.Text;
            moveNextCommand.Image = (Image)resources.GetObject("moveNextCommand.Image");
            moveNextCommand.ImageTransparentColor = Color.Magenta;
            moveNextCommand.Name = "moveNextCommand";
            moveNextCommand.Size = new Size(24, 22);
            moveNextCommand.Text = "->";
            moveNextCommand.ToolTipText = "Move Next";
            moveNextCommand.Click += MoveNextCommand_Click;
            // 
            // moveLastCommand
            // 
            moveLastCommand.DisplayStyle = ToolStripItemDisplayStyle.Text;
            moveLastCommand.Image = (Image)resources.GetObject("moveLastCommand.Image");
            moveLastCommand.ImageTransparentColor = Color.Magenta;
            moveLastCommand.Name = "moveLastCommand";
            moveLastCommand.Size = new Size(23, 22);
            moveLastCommand.Text = ">|";
            moveLastCommand.ToolTipText = "Move Last";
            moveLastCommand.Click += MoveLastCommand_Click;
            // 
            // NavigatorData
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            Controls.Add(navigationStrip);
            Name = "NavigatorData";
            Size = new Size(185, 25);
            navigationStrip.ResumeLayout(false);
            navigationStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip navigationStrip;
        private ToolStripButton moveFirstCommand;
        private ToolStripButton movePreviousCommand;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripLabel currentRowNumber;
        private ToolStripLabel totalRowNumber;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton moveNextCommand;
        private ToolStripButton moveLastCommand;
    }
}
