﻿namespace DataDictionary.Main.Forms
{
    partial class HelpSubject
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
            SplitContainer helpSplitLayout;
            TableLayoutPanel helpDetailLayout;
            ToolStrip helpSubjectToolStrip;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpSubject));
            helpContentNavigation = new TreeView();
            newToolStripButton = new ToolStripButton();
            openToolStripButton = new ToolStripButton();
            saveToolStripButton = new ToolStripButton();
            printToolStripButton = new ToolStripButton();
            toolStripSeparator = new ToolStripSeparator();
            cutToolStripButton = new ToolStripButton();
            copyToolStripButton = new ToolStripButton();
            pasteToolStripButton = new ToolStripButton();
            helpSubjectData = new Controls.TextBoxData();
            helpNameSpaceData = new Controls.TextBoxData();
            helpTextData = new Controls.RichTextBoxData();
            errorProvider = new ErrorProvider(components);
            helpSplitLayout = new SplitContainer();
            helpDetailLayout = new TableLayoutPanel();
            helpSubjectToolStrip = new ToolStrip();
            ((System.ComponentModel.ISupportInitialize)helpSplitLayout).BeginInit();
            helpSplitLayout.Panel1.SuspendLayout();
            helpSplitLayout.Panel2.SuspendLayout();
            helpSplitLayout.SuspendLayout();
            helpDetailLayout.SuspendLayout();
            helpSubjectToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // helpSplitLayout
            // 
            helpSplitLayout.BorderStyle = BorderStyle.FixedSingle;
            helpSplitLayout.Dock = DockStyle.Fill;
            helpSplitLayout.Location = new Point(0, 0);
            helpSplitLayout.Name = "helpSplitLayout";
            // 
            // helpSplitLayout.Panel1
            // 
            helpSplitLayout.Panel1.Controls.Add(helpContentNavigation);
            // 
            // helpSplitLayout.Panel2
            // 
            helpSplitLayout.Panel2.Controls.Add(helpDetailLayout);
            helpSplitLayout.Size = new Size(736, 596);
            helpSplitLayout.SplitterDistance = 153;
            helpSplitLayout.TabIndex = 0;
            // 
            // helpContentNavigation
            // 
            helpContentNavigation.Dock = DockStyle.Fill;
            helpContentNavigation.Location = new Point(0, 0);
            helpContentNavigation.Name = "helpContentNavigation";
            helpContentNavigation.Size = new Size(151, 594);
            helpContentNavigation.TabIndex = 0;
            // 
            // helpDetailLayout
            // 
            helpDetailLayout.ColumnCount = 1;
            helpDetailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            helpDetailLayout.Controls.Add(helpSubjectToolStrip, 0, 0);
            helpDetailLayout.Controls.Add(helpSubjectData, 0, 1);
            helpDetailLayout.Controls.Add(helpNameSpaceData, 0, 2);
            helpDetailLayout.Controls.Add(helpTextData, 0, 3);
            helpDetailLayout.Dock = DockStyle.Fill;
            helpDetailLayout.Location = new Point(0, 0);
            helpDetailLayout.Name = "helpDetailLayout";
            helpDetailLayout.RowCount = 4;
            helpDetailLayout.RowStyles.Add(new RowStyle());
            helpDetailLayout.RowStyles.Add(new RowStyle());
            helpDetailLayout.RowStyles.Add(new RowStyle());
            helpDetailLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            helpDetailLayout.Size = new Size(577, 594);
            helpDetailLayout.TabIndex = 0;
            // 
            // helpSubjectToolStrip
            // 
            helpSubjectToolStrip.Items.AddRange(new ToolStripItem[] { newToolStripButton, openToolStripButton, saveToolStripButton, printToolStripButton, toolStripSeparator, cutToolStripButton, copyToolStripButton, pasteToolStripButton });
            helpSubjectToolStrip.Location = new Point(0, 0);
            helpSubjectToolStrip.Name = "helpSubjectToolStrip";
            helpSubjectToolStrip.Size = new Size(577, 25);
            helpSubjectToolStrip.TabIndex = 1;
            helpSubjectToolStrip.Text = "toolStrip1";
            // 
            // newToolStripButton
            // 
            newToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            newToolStripButton.Image = (Image)resources.GetObject("newToolStripButton.Image");
            newToolStripButton.ImageTransparentColor = Color.Magenta;
            newToolStripButton.Name = "newToolStripButton";
            newToolStripButton.Size = new Size(23, 22);
            newToolStripButton.Text = "&New";
            newToolStripButton.Click += newToolStripButton_Click;
            // 
            // openToolStripButton
            // 
            openToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            openToolStripButton.Image = (Image)resources.GetObject("openToolStripButton.Image");
            openToolStripButton.ImageTransparentColor = Color.Magenta;
            openToolStripButton.Name = "openToolStripButton";
            openToolStripButton.Size = new Size(23, 22);
            openToolStripButton.Text = "&Open";
            openToolStripButton.Click += openToolStripButton_Click;
            // 
            // saveToolStripButton
            // 
            saveToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            saveToolStripButton.Image = (Image)resources.GetObject("saveToolStripButton.Image");
            saveToolStripButton.ImageTransparentColor = Color.Magenta;
            saveToolStripButton.Name = "saveToolStripButton";
            saveToolStripButton.Size = new Size(23, 22);
            saveToolStripButton.Text = "&Save";
            saveToolStripButton.Click += saveToolStripButton_Click;
            // 
            // printToolStripButton
            // 
            printToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            printToolStripButton.Enabled = false;
            printToolStripButton.Image = (Image)resources.GetObject("printToolStripButton.Image");
            printToolStripButton.ImageTransparentColor = Color.Magenta;
            printToolStripButton.Name = "printToolStripButton";
            printToolStripButton.Size = new Size(23, 22);
            printToolStripButton.Text = "&Print";
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new Size(6, 25);
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
            // helpSubjectData
            // 
            helpSubjectData.AutoSize = true;
            helpSubjectData.Dock = DockStyle.Fill;
            helpSubjectData.HeaderText = "Subject";
            helpSubjectData.Location = new Point(3, 28);
            helpSubjectData.Multiline = false;
            helpSubjectData.Name = "helpSubjectData";
            helpSubjectData.ReadOnly = false;
            helpSubjectData.Size = new Size(571, 44);
            helpSubjectData.TabIndex = 0;
            helpSubjectData.Validated += helpSubjectData_Validated;
            helpSubjectData.Validating += helpSubjectData_Validating;
            // 
            // helpNameSpaceData
            // 
            helpNameSpaceData.AutoSize = true;
            helpNameSpaceData.Dock = DockStyle.Fill;
            helpNameSpaceData.HeaderText = "Name Space";
            helpNameSpaceData.Location = new Point(3, 78);
            helpNameSpaceData.Multiline = false;
            helpNameSpaceData.Name = "helpNameSpaceData";
            helpNameSpaceData.ReadOnly = false;
            helpNameSpaceData.Size = new Size(571, 44);
            helpNameSpaceData.TabIndex = 1;
            // 
            // helpTextData
            // 
            helpTextData.AutoSize = true;
            helpTextData.Dock = DockStyle.Fill;
            helpTextData.HeaderText = "Text";
            helpTextData.Location = new Point(3, 128);
            helpTextData.Name = "helpTextData";
            helpTextData.ReadOnly = false;
            helpTextData.RichText = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil Segoe UI;}}\r\n{\\*\\generator Riched20 10.0.19041}\\viewkind4\\uc1 \r\n\\pard\\f0\\fs18\\par\r\n}\r\n";
            helpTextData.Size = new Size(571, 463);
            helpTextData.TabIndex = 2;
            helpTextData.Validated += helpTextData_Validated;
            helpTextData.Validating += helpTextData_Validating;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // HelpSubject
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(736, 596);
            Controls.Add(helpSplitLayout);
            Name = "HelpSubject";
            Text = "Help Subject";
            Load += HelpSubject_Load;
            helpSplitLayout.Panel1.ResumeLayout(false);
            helpSplitLayout.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)helpSplitLayout).EndInit();
            helpSplitLayout.ResumeLayout(false);
            helpDetailLayout.ResumeLayout(false);
            helpDetailLayout.PerformLayout();
            helpSubjectToolStrip.ResumeLayout(false);
            helpSubjectToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TreeView helpContentNavigation;
        private Controls.TextBoxData helpNameSpaceData;
        private Controls.RichTextBoxData helpTextData;
        private Controls.TextBoxData helpSubjectData;
        private ToolStripButton newToolStripButton;
        private ToolStripButton openToolStripButton;
        private ToolStripButton saveToolStripButton;
        private ToolStripButton printToolStripButton;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripButton cutToolStripButton;
        private ToolStripButton copyToolStripButton;
        private ToolStripButton pasteToolStripButton;
        private ErrorProvider errorProvider;
    }
}