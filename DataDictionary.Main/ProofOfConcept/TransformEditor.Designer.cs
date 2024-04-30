namespace DataDictionary.Main.Forms.ProofOfConcept
{
    partial class TransformEditor
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
            TableLayoutPanel transformEditorLayout;
            GroupBox resultGroup;
            TableLayoutPanel transformResultLayout;
            GroupBox transformGroup;
            TableLayoutPanel transformLayout;
            GroupBox sourceGroup;
            TableLayoutPanel sourceLayout;
            titleData = new Controls.TextBoxData();
            resultExceptionData = new Controls.TextBoxData();
            descriptionData = new Controls.TextBoxData();
            bindingTransform = new BindingSource(components);
            transformExceptionData = new Controls.TextBoxData();
            resultData = new TextBox();
            transformData = new TextBox();
            transformCommand = new Button();
            sourceData = new TextBox();
            sourceExceptionData = new Controls.TextBoxData();
            transformEditorLayout = new TableLayoutPanel();
            resultGroup = new GroupBox();
            transformResultLayout = new TableLayoutPanel();
            transformGroup = new GroupBox();
            transformLayout = new TableLayoutPanel();
            sourceGroup = new GroupBox();
            sourceLayout = new TableLayoutPanel();
            transformEditorLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingTransform).BeginInit();
            resultGroup.SuspendLayout();
            transformResultLayout.SuspendLayout();
            transformGroup.SuspendLayout();
            transformLayout.SuspendLayout();
            sourceGroup.SuspendLayout();
            sourceLayout.SuspendLayout();
            SuspendLayout();
            // 
            // transformEditorLayout
            // 
            transformEditorLayout.ColumnCount = 3;
            transformEditorLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            transformEditorLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            transformEditorLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            transformEditorLayout.Controls.Add(titleData, 0, 0);
            transformEditorLayout.Controls.Add(descriptionData, 0, 1);
            transformEditorLayout.Controls.Add(resultGroup, 2, 2);
            transformEditorLayout.Controls.Add(transformGroup, 1, 2);
            transformEditorLayout.Controls.Add(sourceGroup, 0, 2);
            transformEditorLayout.Dock = DockStyle.Fill;
            transformEditorLayout.Location = new Point(0, 25);
            transformEditorLayout.Name = "transformEditorLayout";
            transformEditorLayout.RowCount = 3;
            transformEditorLayout.RowStyles.Add(new RowStyle());
            transformEditorLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            transformEditorLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            transformEditorLayout.Size = new Size(926, 726);
            transformEditorLayout.TabIndex = 2;
            // 
            // titleData
            // 
            titleData.AutoSize = true;
            transformEditorLayout.SetColumnSpan(titleData, 2);
            titleData.Dock = DockStyle.Fill;
            titleData.HeaderText = "Title";
            titleData.Location = new Point(3, 3);
            titleData.Multiline = false;
            titleData.Name = "titleData";
            titleData.ReadOnly = false;
            titleData.Size = new Size(610, 44);
            titleData.TabIndex = 0;
            // 
            // resultExceptionData
            // 
            resultExceptionData.AutoScroll = true;
            resultExceptionData.AutoSize = true;
            resultExceptionData.Dock = DockStyle.Fill;
            resultExceptionData.HeaderText = "Exception";
            resultExceptionData.Location = new Point(3, 413);
            resultExceptionData.Multiline = true;
            resultExceptionData.Name = "resultExceptionData";
            resultExceptionData.ReadOnly = true;
            resultExceptionData.Size = new Size(292, 97);
            resultExceptionData.TabIndex = 1;
            // 
            // descriptionData
            // 
            descriptionData.AutoSize = true;
            transformEditorLayout.SetColumnSpan(descriptionData, 3);
            descriptionData.Dock = DockStyle.Fill;
            descriptionData.HeaderText = "Description";
            descriptionData.Location = new Point(3, 53);
            descriptionData.Multiline = true;
            descriptionData.Name = "descriptionData";
            descriptionData.ReadOnly = false;
            descriptionData.Size = new Size(920, 129);
            descriptionData.TabIndex = 1;
            // 
            // resultGroup
            // 
            resultGroup.Controls.Add(transformResultLayout);
            resultGroup.Dock = DockStyle.Fill;
            resultGroup.Location = new Point(619, 188);
            resultGroup.Name = "resultGroup";
            resultGroup.Size = new Size(304, 535);
            resultGroup.TabIndex = 2;
            resultGroup.TabStop = false;
            resultGroup.Text = "Result";
            // 
            // transformResultLayout
            // 
            transformResultLayout.ColumnCount = 1;
            transformResultLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            transformResultLayout.Controls.Add(resultExceptionData, 0, 1);
            transformResultLayout.Controls.Add(resultData, 0, 0);
            transformResultLayout.Dock = DockStyle.Fill;
            transformResultLayout.Location = new Point(3, 19);
            transformResultLayout.Name = "transformResultLayout";
            transformResultLayout.RowCount = 2;
            transformResultLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            transformResultLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            transformResultLayout.Size = new Size(298, 513);
            transformResultLayout.TabIndex = 0;
            // 
            // transformGroup
            // 
            transformGroup.Controls.Add(transformLayout);
            transformGroup.Dock = DockStyle.Fill;
            transformGroup.Location = new Point(311, 188);
            transformGroup.Name = "transformGroup";
            transformGroup.Size = new Size(302, 535);
            transformGroup.TabIndex = 3;
            transformGroup.TabStop = false;
            transformGroup.Text = "XSLT Transform";
            // 
            // transformLayout
            // 
            transformLayout.ColumnCount = 1;
            transformLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            transformLayout.Controls.Add(transformData, 0, 0);
            transformLayout.Controls.Add(transformCommand, 0, 1);
            transformLayout.Controls.Add(transformExceptionData, 0, 2);
            transformLayout.Dock = DockStyle.Fill;
            transformLayout.Location = new Point(3, 19);
            transformLayout.Name = "transformLayout";
            transformLayout.RowCount = 3;
            transformLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 78.9473648F));
            transformLayout.RowStyles.Add(new RowStyle());
            transformLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 21.0526314F));
            transformLayout.Size = new Size(296, 513);
            transformLayout.TabIndex = 0;
            // 
            // transformExceptionData
            // 
            transformExceptionData.AutoScroll = true;
            transformExceptionData.AutoSize = true;
            transformExceptionData.Dock = DockStyle.Fill;
            transformExceptionData.HeaderText = "Exception";
            transformExceptionData.Location = new Point(3, 414);
            transformExceptionData.Multiline = true;
            transformExceptionData.Name = "transformExceptionData";
            transformExceptionData.ReadOnly = true;
            transformExceptionData.Size = new Size(290, 96);
            transformExceptionData.TabIndex = 2;
            // 
            // resultData
            // 
            resultData.Dock = DockStyle.Fill;
            resultData.Location = new Point(3, 3);
            resultData.Multiline = true;
            resultData.Name = "resultData";
            resultData.ReadOnly = true;
            resultData.Size = new Size(292, 404);
            resultData.TabIndex = 2;
            // 
            // transformData
            // 
            transformData.Dock = DockStyle.Fill;
            transformData.Location = new Point(3, 3);
            transformData.Multiline = true;
            transformData.Name = "transformData";
            transformData.Size = new Size(290, 374);
            transformData.TabIndex = 3;
            // 
            // transformCommand
            // 
            transformCommand.Image = Properties.Resources.XSLTransform;
            transformCommand.ImageAlign = ContentAlignment.TopLeft;
            transformCommand.Location = new Point(3, 383);
            transformCommand.Name = "transformCommand";
            transformCommand.Size = new Size(101, 25);
            transformCommand.TabIndex = 4;
            transformCommand.Text = "Transform";
            transformCommand.TextImageRelation = TextImageRelation.ImageBeforeText;
            transformCommand.UseVisualStyleBackColor = true;
            transformCommand.Click += TransformCommand_Click;
            // 
            // sourceGroup
            // 
            sourceGroup.Controls.Add(sourceLayout);
            sourceGroup.Dock = DockStyle.Fill;
            sourceGroup.Location = new Point(3, 188);
            sourceGroup.Name = "sourceGroup";
            sourceGroup.Size = new Size(302, 535);
            sourceGroup.TabIndex = 4;
            sourceGroup.TabStop = false;
            sourceGroup.Text = "Source";
            // 
            // sourceLayout
            // 
            sourceLayout.ColumnCount = 1;
            sourceLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            sourceLayout.Controls.Add(sourceData, 0, 0);
            sourceLayout.Controls.Add(sourceExceptionData, 0, 1);
            sourceLayout.Dock = DockStyle.Fill;
            sourceLayout.Location = new Point(3, 19);
            sourceLayout.Name = "sourceLayout";
            sourceLayout.RowCount = 2;
            sourceLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            sourceLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            sourceLayout.Size = new Size(296, 513);
            sourceLayout.TabIndex = 0;
            // 
            // sourceData
            // 
            sourceData.Dock = DockStyle.Fill;
            sourceData.Location = new Point(3, 3);
            sourceData.Multiline = true;
            sourceData.Name = "sourceData";
            sourceData.ReadOnly = true;
            sourceData.Size = new Size(290, 404);
            sourceData.TabIndex = 0;
            // 
            // sourceExceptionData
            // 
            sourceExceptionData.AutoSize = true;
            sourceExceptionData.Dock = DockStyle.Fill;
            sourceExceptionData.HeaderText = "Exception";
            sourceExceptionData.Location = new Point(3, 413);
            sourceExceptionData.Multiline = true;
            sourceExceptionData.Name = "sourceExceptionData";
            sourceExceptionData.ReadOnly = true;
            sourceExceptionData.Size = new Size(290, 97);
            sourceExceptionData.TabIndex = 1;
            // 
            // TransformEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(926, 751);
            Controls.Add(transformEditorLayout);
            Name = "TransformEditor";
            Text = "TransformEditor";
            Load += TransformEditor_Load;
            Controls.SetChildIndex(transformEditorLayout, 0);
            transformEditorLayout.ResumeLayout(false);
            transformEditorLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingTransform).EndInit();
            resultGroup.ResumeLayout(false);
            transformResultLayout.ResumeLayout(false);
            transformResultLayout.PerformLayout();
            transformGroup.ResumeLayout(false);
            transformLayout.ResumeLayout(false);
            transformLayout.PerformLayout();
            sourceGroup.ResumeLayout(false);
            sourceLayout.ResumeLayout(false);
            sourceLayout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Controls.TextBoxData titleData;
        private Controls.TextBoxData descriptionData;
        private BindingSource bindingTransform;
        private Controls.TextBoxData resultExceptionData;
        private TextBox resultData;
        private Controls.TextBoxData transformExceptionData;
        private TextBox transformData;
        private Button transformCommand;
        private TextBox sourceData;
        private Controls.TextBoxData sourceExceptionData;
    }
}