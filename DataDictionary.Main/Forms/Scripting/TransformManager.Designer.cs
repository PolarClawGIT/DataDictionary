namespace DataDictionary.Main.Forms.Scripting
{
    partial class TransformManager
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
            TableLayoutPanel transformLayout;
            ToolStripLabel transformScriptToolLabel;
            ToolStripSeparator toolStripSeparator1;
            transformTitleData = new Controls.TextBoxData();
            transformDescriptionData = new Controls.TextBoxData();
            tranformScriptMenu = new ToolStrip();
            formatScriptCommand = new ToolStripButton();
            transformScriptAsText = new ToolStripButton();
            transformScriptAsXml = new ToolStripButton();
            transformScriptData = new TextBox();
            transformExceptionData = new Controls.TextBoxData();
            transformToolStrip = new ContextMenuStrip(components);
            addTransformCommand = new ToolStripMenuItem();
            removeTransformCommand = new ToolStripMenuItem();
            bindingTransform = new BindingSource(components);
            transformLayout = new TableLayoutPanel();
            transformScriptToolLabel = new ToolStripLabel();
            toolStripSeparator1 = new ToolStripSeparator();
            transformLayout.SuspendLayout();
            tranformScriptMenu.SuspendLayout();
            transformToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingTransform).BeginInit();
            SuspendLayout();
            // 
            // transformLayout
            // 
            transformLayout.ColumnCount = 1;
            transformLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            transformLayout.Controls.Add(transformTitleData, 0, 0);
            transformLayout.Controls.Add(transformDescriptionData, 0, 1);
            transformLayout.Controls.Add(tranformScriptMenu, 0, 2);
            transformLayout.Controls.Add(transformScriptData, 0, 3);
            transformLayout.Controls.Add(transformExceptionData, 0, 4);
            transformLayout.Dock = DockStyle.Fill;
            transformLayout.Location = new Point(0, 25);
            transformLayout.Name = "transformLayout";
            transformLayout.RowCount = 5;
            transformLayout.RowStyles.Add(new RowStyle());
            transformLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            transformLayout.RowStyles.Add(new RowStyle());
            transformLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            transformLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            transformLayout.Size = new Size(452, 529);
            transformLayout.TabIndex = 5;
            // 
            // transformTitleData
            // 
            transformTitleData.AutoSize = true;
            transformTitleData.Dock = DockStyle.Fill;
            transformTitleData.HeaderText = "Transform Title";
            transformTitleData.Location = new Point(3, 3);
            transformTitleData.Multiline = false;
            transformTitleData.Name = "transformTitleData";
            transformTitleData.ReadOnly = false;
            transformTitleData.Size = new Size(446, 44);
            transformTitleData.TabIndex = 1;
            // 
            // transformDescriptionData
            // 
            transformDescriptionData.AutoSize = true;
            transformDescriptionData.Dock = DockStyle.Fill;
            transformDescriptionData.HeaderText = "Transform Description";
            transformDescriptionData.Location = new Point(3, 53);
            transformDescriptionData.Multiline = true;
            transformDescriptionData.Name = "transformDescriptionData";
            transformDescriptionData.ReadOnly = false;
            transformDescriptionData.Size = new Size(446, 107);
            transformDescriptionData.TabIndex = 2;
            // 
            // tranformScriptMenu
            // 
            tranformScriptMenu.GripStyle = ToolStripGripStyle.Hidden;
            tranformScriptMenu.Items.AddRange(new ToolStripItem[] { transformScriptToolLabel, toolStripSeparator1, formatScriptCommand, transformScriptAsText, transformScriptAsXml });
            tranformScriptMenu.Location = new Point(0, 163);
            tranformScriptMenu.Name = "tranformScriptMenu";
            tranformScriptMenu.Size = new Size(452, 25);
            tranformScriptMenu.TabIndex = 6;
            tranformScriptMenu.Text = "Transform Script Menu";
            // 
            // transformScriptToolLabel
            // 
            transformScriptToolLabel.Name = "transformScriptToolLabel";
            transformScriptToolLabel.Size = new Size(93, 22);
            transformScriptToolLabel.Text = "Transform Script";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // formatScriptCommand
            // 
            formatScriptCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            formatScriptCommand.Image = Properties.Resources.XSLTransform;
            formatScriptCommand.ImageTransparentColor = Color.Magenta;
            formatScriptCommand.Name = "formatScriptCommand";
            formatScriptCommand.Size = new Size(23, 22);
            formatScriptCommand.Text = "Format Script";
            formatScriptCommand.Click += FormatScriptCommand_Click;
            // 
            // transformScriptAsText
            // 
            transformScriptAsText.CheckOnClick = true;
            transformScriptAsText.DisplayStyle = ToolStripItemDisplayStyle.Image;
            transformScriptAsText.Image = Properties.Resources.TextFile;
            transformScriptAsText.ImageTransparentColor = Color.Magenta;
            transformScriptAsText.Name = "transformScriptAsText";
            transformScriptAsText.Size = new Size(23, 22);
            transformScriptAsText.Text = "as Text";
            transformScriptAsText.ToolTipText = "after Transform output as Text";
            transformScriptAsText.Click += TransformScriptOutputAs_Click;
            // 
            // transformScriptAsXml
            // 
            transformScriptAsXml.CheckOnClick = true;
            transformScriptAsXml.DisplayStyle = ToolStripItemDisplayStyle.Image;
            transformScriptAsXml.Image = Properties.Resources.XmlFile;
            transformScriptAsXml.ImageTransparentColor = Color.Magenta;
            transformScriptAsXml.Name = "transformScriptAsXml";
            transformScriptAsXml.Size = new Size(23, 22);
            transformScriptAsXml.Text = "as XML";
            transformScriptAsXml.ToolTipText = "after Transform output as XML";
            transformScriptAsXml.Click += TransformScriptOutputAs_Click;
            // 
            // transformScriptData
            // 
            transformScriptData.Dock = DockStyle.Fill;
            transformScriptData.Location = new Point(3, 191);
            transformScriptData.Multiline = true;
            transformScriptData.Name = "transformScriptData";
            transformScriptData.Size = new Size(446, 221);
            transformScriptData.TabIndex = 7;
            // 
            // transformExceptionData
            // 
            transformExceptionData.AutoSize = true;
            transformExceptionData.Dock = DockStyle.Fill;
            transformExceptionData.HeaderText = "Transform Exception";
            transformExceptionData.Location = new Point(3, 418);
            transformExceptionData.Multiline = true;
            transformExceptionData.Name = "transformExceptionData";
            transformExceptionData.ReadOnly = true;
            transformExceptionData.Size = new Size(446, 108);
            transformExceptionData.TabIndex = 5;
            // 
            // transformToolStrip
            // 
            transformToolStrip.Items.AddRange(new ToolStripItem[] { addTransformCommand, removeTransformCommand });
            transformToolStrip.Name = "transformToolStrip";
            transformToolStrip.Size = new Size(171, 48);
            // 
            // addTransformCommand
            // 
            addTransformCommand.Image = Properties.Resources.NewXSLTransform;
            addTransformCommand.Name = "addTransformCommand";
            addTransformCommand.Size = new Size(170, 22);
            addTransformCommand.Text = "add Transform";
            addTransformCommand.Click += addTransformCommand_Click;
            // 
            // removeTransformCommand
            // 
            removeTransformCommand.Image = Properties.Resources.DeleteXSLTransform;
            removeTransformCommand.Name = "removeTransformCommand";
            removeTransformCommand.Size = new Size(170, 22);
            removeTransformCommand.Text = "remove Transform";
            removeTransformCommand.Click += removeTransformCommand_Click;
            // 
            // TransformManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(452, 554);
            Controls.Add(transformLayout);
            Name = "TransformManager";
            Text = "TransformManager";
            Load += TransformManager_Load;
            Controls.SetChildIndex(transformLayout, 0);
            transformLayout.ResumeLayout(false);
            transformLayout.PerformLayout();
            tranformScriptMenu.ResumeLayout(false);
            tranformScriptMenu.PerformLayout();
            transformToolStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)bindingTransform).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ContextMenuStrip transformToolStrip;
        private ToolStripMenuItem addTransformCommand;
        private ToolStripMenuItem removeTransformCommand;
        private BindingSource bindingTransform;
        private Controls.TextBoxData transformTitleData;
        private Controls.TextBoxData transformDescriptionData;
        private Controls.TextBoxData transformExceptionData;
        private TextBox transformScriptData;
        private ToolStrip tranformScriptMenu;
        private ToolStripButton formatScriptCommand;
        private ToolStripButton transformScriptAsText;
        private ToolStripButton transformScriptAsXml;
    }
}