namespace DataDictionary.Main.Controls
{
    partial class HtmlTextBoxData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HtmlTextBoxData));
            Panel webPanel;
            label = new Label();
            toolStrip = new ToolStrip();
            toolStripBold = new ToolStripButton();
            toolStripItalic = new ToolStripButton();
            toolStripUnderline = new ToolStripButton();
            toolStripBulletList = new ToolStripButton();
            toolStripStrikeThrough = new ToolStripButton();
            toolStripClearFormating = new ToolStripButton();
            toolStripSeparator = new ToolStripSeparator();
            toolStripCut = new ToolStripButton();
            toolStripCopy = new ToolStripButton();
            toolStripPaste = new ToolStripButton();
            errorLocation = new Panel();
            webBrowser = new WebBrowser();
            controlLayout = new TableLayoutPanel();
            webPanel = new Panel();
            controlLayout.SuspendLayout();
            toolStrip.SuspendLayout();
            webPanel.SuspendLayout();
            SuspendLayout();
            // 
            // controlLayout
            // 
            controlLayout.ColumnCount = 2;
            controlLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            controlLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 25F));
            controlLayout.Controls.Add(label, 0, 0);
            controlLayout.Controls.Add(toolStrip, 0, 1);
            controlLayout.Controls.Add(errorLocation, 1, 0);
            controlLayout.Controls.Add(webPanel, 0, 2);
            controlLayout.Dock = DockStyle.Fill;
            controlLayout.Location = new Point(0, 0);
            controlLayout.Margin = new Padding(0);
            controlLayout.Name = "controlLayout";
            controlLayout.RowCount = 3;
            controlLayout.RowStyles.Add(new RowStyle());
            controlLayout.RowStyles.Add(new RowStyle());
            controlLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            controlLayout.Size = new Size(281, 150);
            controlLayout.TabIndex = 1;
            // 
            // label
            // 
            label.AutoSize = true;
            label.Location = new Point(0, 0);
            label.Margin = new Padding(0);
            label.Name = "label";
            label.Size = new Size(32, 15);
            label.TabIndex = 0;
            label.Text = "label";
            // 
            // toolStrip
            // 
            controlLayout.SetColumnSpan(toolStrip, 2);
            toolStrip.Items.AddRange(new ToolStripItem[] { toolStripBold, toolStripItalic, toolStripUnderline, toolStripBulletList, toolStripStrikeThrough, toolStripClearFormating, toolStripSeparator, toolStripCut, toolStripCopy, toolStripPaste });
            toolStrip.Location = new Point(3, 18);
            toolStrip.Margin = new Padding(3);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(275, 25);
            toolStrip.TabIndex = 2;
            toolStrip.Text = "toolStrip1";
            // 
            // toolStripBold
            // 
            toolStripBold.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBold.Image = Properties.Resources.Bold;
            toolStripBold.ImageTransparentColor = Color.Magenta;
            toolStripBold.Name = "toolStripBold";
            toolStripBold.Size = new Size(23, 22);
            toolStripBold.Text = "Bold";
            toolStripBold.Click += toolStripBold_Click;
            // 
            // toolStripItalic
            // 
            toolStripItalic.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripItalic.Image = Properties.Resources.Italic;
            toolStripItalic.ImageTransparentColor = Color.Magenta;
            toolStripItalic.Name = "toolStripItalic";
            toolStripItalic.Size = new Size(23, 22);
            toolStripItalic.Text = "Italic";
            toolStripItalic.ToolTipText = "Italic";
            toolStripItalic.Click += toolStripItalic_Click;
            // 
            // toolStripUnderline
            // 
            toolStripUnderline.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripUnderline.Image = Properties.Resources.Underline;
            toolStripUnderline.ImageTransparentColor = Color.Magenta;
            toolStripUnderline.Name = "toolStripUnderline";
            toolStripUnderline.Size = new Size(23, 22);
            toolStripUnderline.Text = "Underline";
            toolStripUnderline.ToolTipText = "Underline";
            toolStripUnderline.Click += toolStripUnderline_Click;
            // 
            // toolStripBulletList
            // 
            toolStripBulletList.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBulletList.Image = Properties.Resources.BulletList;
            toolStripBulletList.ImageTransparentColor = Color.Magenta;
            toolStripBulletList.Name = "toolStripBulletList";
            toolStripBulletList.Size = new Size(23, 22);
            toolStripBulletList.Text = "Bullet List";
            toolStripBulletList.ToolTipText = "Bullet List";
            toolStripBulletList.Click += toolStripBulletList_Click;
            // 
            // toolStripStrikeThrough
            // 
            toolStripStrikeThrough.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripStrikeThrough.Image = Properties.Resources.StrikeThrough;
            toolStripStrikeThrough.ImageTransparentColor = Color.Magenta;
            toolStripStrikeThrough.Name = "toolStripStrikeThrough";
            toolStripStrikeThrough.Size = new Size(23, 22);
            toolStripStrikeThrough.Text = "Strike Through";
            toolStripStrikeThrough.ToolTipText = "Strike Through";
            toolStripStrikeThrough.Click += toolStripStrikeThrough_Click;
            // 
            // toolStripClearFormating
            // 
            toolStripClearFormating.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripClearFormating.Image = Properties.Resources.CleanData;
            toolStripClearFormating.ImageTransparentColor = Color.Magenta;
            toolStripClearFormating.Name = "toolStripClearFormating";
            toolStripClearFormating.Size = new Size(23, 22);
            toolStripClearFormating.Text = "Clear Formating";
            toolStripClearFormating.ToolTipText = "Clear Formating";
            toolStripClearFormating.Click += toolStripClearFormating_Click;
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new Size(6, 25);
            // 
            // toolStripCut
            // 
            toolStripCut.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripCut.Image = (Image)resources.GetObject("toolStripCut.Image");
            toolStripCut.ImageTransparentColor = Color.Magenta;
            toolStripCut.Name = "toolStripCut";
            toolStripCut.Size = new Size(23, 22);
            toolStripCut.Text = "Cut";
            toolStripCut.Click += toolStripCut_Click;
            // 
            // toolStripCopy
            // 
            toolStripCopy.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripCopy.Image = (Image)resources.GetObject("toolStripCopy.Image");
            toolStripCopy.ImageTransparentColor = Color.Magenta;
            toolStripCopy.Name = "toolStripCopy";
            toolStripCopy.Size = new Size(23, 22);
            toolStripCopy.Text = "Copy";
            toolStripCopy.Click += toolStripCopy_Click;
            // 
            // toolStripPaste
            // 
            toolStripPaste.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripPaste.Image = (Image)resources.GetObject("toolStripPaste.Image");
            toolStripPaste.ImageTransparentColor = Color.Magenta;
            toolStripPaste.Name = "toolStripPaste";
            toolStripPaste.Size = new Size(23, 22);
            toolStripPaste.Text = "Paste";
            toolStripPaste.Click += toolStripPaste_Click;
            // 
            // errorLocation
            // 
            errorLocation.AutoSize = true;
            errorLocation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            errorLocation.Dock = DockStyle.Left;
            errorLocation.Location = new Point(259, 3);
            errorLocation.Name = "errorLocation";
            errorLocation.Size = new Size(0, 9);
            errorLocation.TabIndex = 3;
            // 
            // webPanel
            // 
            webPanel.BorderStyle = BorderStyle.FixedSingle;
            controlLayout.SetColumnSpan(webPanel, 2);
            webPanel.Controls.Add(webBrowser);
            webPanel.Dock = DockStyle.Fill;
            webPanel.Location = new Point(3, 49);
            webPanel.Name = "webPanel";
            webPanel.Size = new Size(275, 98);
            webPanel.TabIndex = 4;
            // 
            // webBrowser
            // 
            webBrowser.Dock = DockStyle.Fill;
            webBrowser.Location = new Point(0, 0);
            webBrowser.Name = "webBrowser";
            webBrowser.Size = new Size(273, 96);
            webBrowser.TabIndex = 0;
            // 
            // HtmlTextBoxData
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(controlLayout);
            Name = "HtmlTextBoxData";
            Size = new Size(281, 150);
            controlLayout.ResumeLayout(false);
            controlLayout.PerformLayout();
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            webPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label label;
        private ToolStrip toolStrip;
        private ToolStripButton toolStripBold;
        private ToolStripButton toolStripItalic;
        private ToolStripButton toolStripUnderline;
        private ToolStripButton toolStripBulletList;
        private ToolStripButton toolStripStrikeThrough;
        private ToolStripButton toolStripClearFormating;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripButton toolStripCut;
        private ToolStripButton toolStripCopy;
        private ToolStripButton toolStripPaste;
        private Panel errorLocation;
        private WebBrowser webBrowser;
    }
}
