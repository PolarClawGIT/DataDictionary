namespace DataDictionary.Main.Forms.Model
{
    partial class Model
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
            TableLayoutPanel modelManagerLayout;
            modelTitleData = new Controls.TextBoxData();
            modelDescriptionData = new Controls.TextBoxData();
            bindingModel = new BindingSource(components);
            modelToolStrip = new ContextMenuStrip(components);
            openModelManagerCommand = new ToolStripMenuItem();
            modelManagerLayout = new TableLayoutPanel();
            modelManagerLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingModel).BeginInit();
            modelToolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // modelManagerLayout
            // 
            modelManagerLayout.ColumnCount = 1;
            modelManagerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            modelManagerLayout.Controls.Add(modelTitleData, 0, 0);
            modelManagerLayout.Controls.Add(modelDescriptionData, 0, 1);
            modelManagerLayout.Dock = DockStyle.Fill;
            modelManagerLayout.Location = new Point(0, 25);
            modelManagerLayout.Name = "modelManagerLayout";
            modelManagerLayout.RowCount = 2;
            modelManagerLayout.RowStyles.Add(new RowStyle());
            modelManagerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            modelManagerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            modelManagerLayout.Size = new Size(390, 293);
            modelManagerLayout.TabIndex = 2;
            // 
            // modelTitleData
            // 
            modelTitleData.AutoSize = true;
            modelTitleData.Dock = DockStyle.Fill;
            modelTitleData.HeaderText = "Model Title";
            modelTitleData.Location = new Point(3, 3);
            modelTitleData.Multiline = false;
            modelTitleData.Name = "modelTitleData";
            modelTitleData.ReadOnly = false;
            modelTitleData.Size = new Size(384, 44);
            modelTitleData.TabIndex = 1;
            // 
            // modelDescriptionData
            // 
            modelDescriptionData.AutoSize = true;
            modelDescriptionData.Dock = DockStyle.Fill;
            modelDescriptionData.HeaderText = "Model Description";
            modelDescriptionData.Location = new Point(3, 53);
            modelDescriptionData.Multiline = true;
            modelDescriptionData.Name = "modelDescriptionData";
            modelDescriptionData.ReadOnly = false;
            modelDescriptionData.Size = new Size(384, 237);
            modelDescriptionData.TabIndex = 2;
            // 
            // modelToolStrip
            // 
            modelToolStrip.Items.AddRange(new ToolStripItem[] { openModelManagerCommand });
            modelToolStrip.Name = "modelToolStrip";
            modelToolStrip.Size = new Size(191, 48);
            // 
            // openModelManagerCommand
            // 
            openModelManagerCommand.Image = Properties.Resources.SaveSoftwareDefinitionModel;
            openModelManagerCommand.Name = "openModelManagerCommand";
            openModelManagerCommand.Size = new Size(190, 22);
            openModelManagerCommand.Text = "Open Model Manager";
            openModelManagerCommand.Click += openModelManagerCommand_Click;
            // 
            // Model
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(390, 318);
            Controls.Add(modelManagerLayout);
            Name = "Model";
            Text = "Model";
            Load += Model_Load;
            Controls.SetChildIndex(modelManagerLayout, 0);
            modelManagerLayout.ResumeLayout(false);
            modelManagerLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingModel).EndInit();
            modelToolStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData modelTitleData;
        private Controls.TextBoxData modelDescriptionData;
        private BindingSource bindingModel;
        private ContextMenuStrip modelToolStrip;
        private ToolStripMenuItem openModelManagerCommand;
    }
}