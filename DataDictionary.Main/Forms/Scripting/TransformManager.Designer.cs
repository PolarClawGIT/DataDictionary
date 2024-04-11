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
            GroupBox outputTypeGroup;
            TableLayoutPanel outputTypeLayout;
            transformDescriptionData = new Controls.TextBoxData();
            outputAsTextData = new CheckBox();
            outputAsXmlData = new CheckBox();
            transformScriptData = new Controls.TextBoxData();
            transformTitleData = new Controls.TextBoxData();
            transformToolStrip = new ContextMenuStrip(components);
            addTransformCommand = new ToolStripMenuItem();
            removeTransformCommand = new ToolStripMenuItem();
            bindingTransform = new BindingSource(components);
            transformExceptionData = new Controls.TextBoxData();
            transformLayout = new TableLayoutPanel();
            outputTypeGroup = new GroupBox();
            outputTypeLayout = new TableLayoutPanel();
            transformLayout.SuspendLayout();
            outputTypeGroup.SuspendLayout();
            outputTypeLayout.SuspendLayout();
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
            transformLayout.Controls.Add(outputTypeGroup, 0, 2);
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
            // transformDescriptionData
            // 
            transformDescriptionData.AutoSize = true;
            transformDescriptionData.Dock = DockStyle.Fill;
            transformDescriptionData.HeaderText = "Transform Description";
            transformDescriptionData.Location = new Point(3, 53);
            transformDescriptionData.Multiline = true;
            transformDescriptionData.Name = "transformDescriptionData";
            transformDescriptionData.ReadOnly = false;
            transformDescriptionData.Size = new Size(446, 100);
            transformDescriptionData.TabIndex = 2;
            // 
            // outputTypeGroup
            // 
            outputTypeGroup.AutoSize = true;
            outputTypeGroup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            outputTypeGroup.Controls.Add(outputTypeLayout);
            outputTypeGroup.Dock = DockStyle.Fill;
            outputTypeGroup.Location = new Point(3, 159);
            outputTypeGroup.Name = "outputTypeGroup";
            outputTypeGroup.Size = new Size(446, 47);
            outputTypeGroup.TabIndex = 3;
            outputTypeGroup.TabStop = false;
            outputTypeGroup.Text = "Output Type";
            // 
            // outputTypeLayout
            // 
            outputTypeLayout.AutoSize = true;
            outputTypeLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            outputTypeLayout.ColumnCount = 2;
            outputTypeLayout.ColumnStyles.Add(new ColumnStyle());
            outputTypeLayout.ColumnStyles.Add(new ColumnStyle());
            outputTypeLayout.Controls.Add(outputAsTextData, 0, 0);
            outputTypeLayout.Controls.Add(outputAsXmlData, 1, 0);
            outputTypeLayout.Dock = DockStyle.Fill;
            outputTypeLayout.Location = new Point(3, 19);
            outputTypeLayout.Name = "outputTypeLayout";
            outputTypeLayout.RowCount = 1;
            outputTypeLayout.RowStyles.Add(new RowStyle());
            outputTypeLayout.Size = new Size(440, 25);
            outputTypeLayout.TabIndex = 0;
            // 
            // outputAsTextData
            // 
            outputAsTextData.AutoSize = true;
            outputAsTextData.Location = new Point(3, 3);
            outputAsTextData.Name = "outputAsTextData";
            outputAsTextData.Size = new Size(61, 19);
            outputAsTextData.TabIndex = 0;
            outputAsTextData.Text = "as Text";
            outputAsTextData.UseVisualStyleBackColor = true;
            // 
            // outputAsXmlData
            // 
            outputAsXmlData.AutoSize = true;
            outputAsXmlData.Location = new Point(70, 3);
            outputAsXmlData.Name = "outputAsXmlData";
            outputAsXmlData.Size = new Size(64, 19);
            outputAsXmlData.TabIndex = 1;
            outputAsXmlData.Text = "as XML";
            outputAsXmlData.UseVisualStyleBackColor = true;
            // 
            // transformScriptData
            // 
            transformScriptData.AutoSize = true;
            transformScriptData.Dock = DockStyle.Fill;
            transformScriptData.HeaderText = "Transform Script";
            transformScriptData.Location = new Point(3, 212);
            transformScriptData.Multiline = true;
            transformScriptData.Name = "transformScriptData";
            transformScriptData.ReadOnly = false;
            transformScriptData.Size = new Size(446, 207);
            transformScriptData.TabIndex = 4;
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
            // transformExceptionData
            // 
            transformExceptionData.AutoSize = true;
            transformExceptionData.Dock = DockStyle.Fill;
            transformExceptionData.HeaderText = "Transform Exception";
            transformExceptionData.Location = new Point(3, 425);
            transformExceptionData.Multiline = true;
            transformExceptionData.Name = "transformExceptionData";
            transformExceptionData.ReadOnly = true;
            transformExceptionData.Size = new Size(446, 101);
            transformExceptionData.TabIndex = 5;
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
            outputTypeGroup.ResumeLayout(false);
            outputTypeGroup.PerformLayout();
            outputTypeLayout.ResumeLayout(false);
            outputTypeLayout.PerformLayout();
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
        private CheckBox outputAsTextData;
        private CheckBox outputAsXmlData;
        private Controls.TextBoxData transformScriptData;
        private Controls.TextBoxData transformExceptionData;
    }
}