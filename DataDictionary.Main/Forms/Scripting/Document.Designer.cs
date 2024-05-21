namespace DataDictionary.Main.Forms.Scripting
{
    partial class Document
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
            TableLayoutPanel documentLayout;
            TableLayoutPanel dataLayout;
            selectionData = new Controls.ComboBoxData();
            schemaData = new Controls.ComboBoxData();
            transformData = new Controls.ComboBoxData();
            documentTabs = new TabControl();
            dataTab = new TabPage();
            inputData = new TextBox();
            inputExceptionData = new Controls.TextBoxData();
            resultTab = new TabPage();
            outputLayout = new TableLayoutPanel();
            outputData = new TextBox();
            outputExecptionData = new Controls.TextBoxData();
            documentCommands = new ContextMenuStrip(components);
            buildCommand = new ToolStripMenuItem();
            bindingDocument = new BindingSource(components);
            documentLayout = new TableLayoutPanel();
            dataLayout = new TableLayoutPanel();
            documentLayout.SuspendLayout();
            documentTabs.SuspendLayout();
            dataTab.SuspendLayout();
            dataLayout.SuspendLayout();
            resultTab.SuspendLayout();
            outputLayout.SuspendLayout();
            documentCommands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingDocument).BeginInit();
            SuspendLayout();
            // 
            // documentLayout
            // 
            documentLayout.ColumnCount = 1;
            documentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            documentLayout.Controls.Add(selectionData, 0, 0);
            documentLayout.Controls.Add(schemaData, 0, 1);
            documentLayout.Controls.Add(transformData, 0, 2);
            documentLayout.Controls.Add(documentTabs, 0, 3);
            documentLayout.Dock = DockStyle.Fill;
            documentLayout.Location = new Point(0, 25);
            documentLayout.Name = "documentLayout";
            documentLayout.RowCount = 4;
            documentLayout.RowStyles.Add(new RowStyle());
            documentLayout.RowStyles.Add(new RowStyle());
            documentLayout.RowStyles.Add(new RowStyle());
            documentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            documentLayout.Size = new Size(572, 488);
            documentLayout.TabIndex = 0;
            // 
            // selectionData
            // 
            selectionData.AutoSize = true;
            selectionData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            selectionData.Dock = DockStyle.Fill;
            selectionData.DropDownStyle = ComboBoxStyle.DropDown;
            selectionData.HeaderText = "Data Selection";
            selectionData.Location = new Point(3, 3);
            selectionData.Name = "selectionData";
            selectionData.ReadOnly = false;
            selectionData.Size = new Size(566, 44);
            selectionData.TabIndex = 0;
            // 
            // schemaData
            // 
            schemaData.AutoSize = true;
            schemaData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            schemaData.Dock = DockStyle.Fill;
            schemaData.DropDownStyle = ComboBoxStyle.DropDown;
            schemaData.HeaderText = "Schema";
            schemaData.Location = new Point(3, 53);
            schemaData.Name = "schemaData";
            schemaData.ReadOnly = false;
            schemaData.Size = new Size(566, 44);
            schemaData.TabIndex = 1;
            // 
            // transformData
            // 
            transformData.AutoSize = true;
            transformData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            transformData.Dock = DockStyle.Fill;
            transformData.DropDownStyle = ComboBoxStyle.DropDown;
            transformData.HeaderText = "Transform";
            transformData.Location = new Point(3, 103);
            transformData.Name = "transformData";
            transformData.ReadOnly = false;
            transformData.Size = new Size(566, 44);
            transformData.TabIndex = 2;
            // 
            // documentTabs
            // 
            documentTabs.Controls.Add(dataTab);
            documentTabs.Controls.Add(resultTab);
            documentTabs.Dock = DockStyle.Fill;
            documentTabs.Location = new Point(3, 153);
            documentTabs.Name = "documentTabs";
            documentTabs.SelectedIndex = 0;
            documentTabs.Size = new Size(566, 332);
            documentTabs.TabIndex = 3;
            // 
            // dataTab
            // 
            dataTab.BackColor = SystemColors.Control;
            dataTab.Controls.Add(dataLayout);
            dataTab.Location = new Point(4, 24);
            dataTab.Name = "dataTab";
            dataTab.Padding = new Padding(3);
            dataTab.Size = new Size(558, 304);
            dataTab.TabIndex = 1;
            dataTab.Text = "Input";
            // 
            // dataLayout
            // 
            dataLayout.ColumnCount = 1;
            dataLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            dataLayout.Controls.Add(inputData, 0, 0);
            dataLayout.Controls.Add(inputExceptionData, 0, 1);
            dataLayout.Dock = DockStyle.Fill;
            dataLayout.Location = new Point(3, 3);
            dataLayout.Name = "dataLayout";
            dataLayout.RowCount = 2;
            dataLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 75F));
            dataLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            dataLayout.Size = new Size(552, 298);
            dataLayout.TabIndex = 0;
            // 
            // inputData
            // 
            inputData.Dock = DockStyle.Fill;
            inputData.Location = new Point(3, 3);
            inputData.Multiline = true;
            inputData.Name = "inputData";
            inputData.ReadOnly = true;
            inputData.ScrollBars = ScrollBars.Both;
            inputData.Size = new Size(546, 217);
            inputData.TabIndex = 0;
            // 
            // inputExceptionData
            // 
            inputExceptionData.AutoSize = true;
            inputExceptionData.Dock = DockStyle.Fill;
            inputExceptionData.HeaderText = "Exception";
            inputExceptionData.Location = new Point(3, 226);
            inputExceptionData.Multiline = true;
            inputExceptionData.Name = "inputExceptionData";
            inputExceptionData.ReadOnly = true;
            inputExceptionData.Size = new Size(546, 69);
            inputExceptionData.TabIndex = 1;
            // 
            // resultTab
            // 
            resultTab.BackColor = SystemColors.Control;
            resultTab.Controls.Add(outputLayout);
            resultTab.Location = new Point(4, 24);
            resultTab.Name = "resultTab";
            resultTab.Padding = new Padding(3);
            resultTab.Size = new Size(192, 72);
            resultTab.TabIndex = 2;
            resultTab.Text = "Output";
            // 
            // outputLayout
            // 
            outputLayout.ColumnCount = 1;
            outputLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            outputLayout.Controls.Add(outputData, 0, 0);
            outputLayout.Controls.Add(outputExecptionData, 0, 1);
            outputLayout.Dock = DockStyle.Fill;
            outputLayout.Location = new Point(3, 3);
            outputLayout.Name = "outputLayout";
            outputLayout.RowCount = 2;
            outputLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 75F));
            outputLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            outputLayout.Size = new Size(186, 66);
            outputLayout.TabIndex = 0;
            // 
            // outputData
            // 
            outputData.Dock = DockStyle.Fill;
            outputData.Location = new Point(3, 3);
            outputData.Multiline = true;
            outputData.Name = "outputData";
            outputData.ReadOnly = true;
            outputData.ScrollBars = ScrollBars.Both;
            outputData.Size = new Size(180, 43);
            outputData.TabIndex = 0;
            // 
            // outputExecptionData
            // 
            outputExecptionData.AutoSize = true;
            outputExecptionData.Dock = DockStyle.Fill;
            outputExecptionData.HeaderText = "Exception";
            outputExecptionData.Location = new Point(3, 52);
            outputExecptionData.Multiline = true;
            outputExecptionData.Name = "outputExecptionData";
            outputExecptionData.ReadOnly = true;
            outputExecptionData.Size = new Size(180, 11);
            outputExecptionData.TabIndex = 1;
            // 
            // documentCommands
            // 
            documentCommands.Items.AddRange(new ToolStripItem[] { buildCommand });
            documentCommands.Name = "documentCommands";
            documentCommands.Size = new Size(161, 26);
            // 
            // buildCommand
            // 
            buildCommand.Image = Properties.Resources.BuildDefinition;
            buildCommand.Name = "buildCommand";
            buildCommand.Size = new Size(160, 22);
            buildCommand.Text = "Build Document";
            buildCommand.ToolTipText = "Build Document";
            buildCommand.Click += BuildCommand_Click;
            // 
            // Document
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(572, 513);
            Controls.Add(documentLayout);
            Name = "Document";
            Text = "Document";
            Load += Document_Load;
            Controls.SetChildIndex(documentLayout, 0);
            documentLayout.ResumeLayout(false);
            documentLayout.PerformLayout();
            documentTabs.ResumeLayout(false);
            dataTab.ResumeLayout(false);
            dataLayout.ResumeLayout(false);
            dataLayout.PerformLayout();
            resultTab.ResumeLayout(false);
            outputLayout.ResumeLayout(false);
            outputLayout.PerformLayout();
            documentCommands.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)bindingDocument).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel documentLayout;
        private Controls.ComboBoxData selectionData;
        private Controls.ComboBoxData schemaData;
        private ContextMenuStrip documentCommands;
        private Controls.ComboBoxData transformData;
        private TabControl documentTabs;
        private TabPage dataTab;
        private TabPage resultTab;
        private ToolStripMenuItem buildCommand;
        private TextBox inputData;
        private Controls.TextBoxData inputExceptionData;
        private TableLayoutPanel outputLayout;
        private TextBox outputData;
        private Controls.TextBoxData outputExecptionData;
        private BindingSource bindingDocument;
    }
}