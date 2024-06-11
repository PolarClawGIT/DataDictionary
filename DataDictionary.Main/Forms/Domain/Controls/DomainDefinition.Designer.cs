namespace DataDictionary.Main.Forms.Domain.Controls
{
    partial class DomainDefinition
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
            TabControl definitionTab;
            definitionTextTab = new TabPage();
            definitionTextData = new DataDictionary.Main.Controls.RichTextBoxData();
            definitionSummaryTab = new TabPage();
            definitionSummaryData = new TextBox();
            definitionLayout = new TableLayoutPanel();
            definitionData = new DataDictionary.Main.Controls.ComboBoxData();
            applyCommand = new Button();
            Sync = new Button();
            definitionTab = new TabControl();
            definitionTab.SuspendLayout();
            definitionTextTab.SuspendLayout();
            definitionSummaryTab.SuspendLayout();
            definitionLayout.SuspendLayout();
            SuspendLayout();
            // 
            // definitionTab
            // 
            definitionLayout.SetColumnSpan(definitionTab, 3);
            definitionTab.Controls.Add(definitionTextTab);
            definitionTab.Controls.Add(definitionSummaryTab);
            definitionTab.Dock = DockStyle.Fill;
            definitionTab.Location = new Point(3, 53);
            definitionTab.Name = "definitionTab";
            definitionTab.SelectedIndex = 0;
            definitionTab.Size = new Size(324, 184);
            definitionTab.TabIndex = 2;
            // 
            // definitionTextTab
            // 
            definitionTextTab.BackColor = SystemColors.Control;
            definitionTextTab.Controls.Add(definitionTextData);
            definitionTextTab.Location = new Point(4, 24);
            definitionTextTab.Name = "definitionTextTab";
            definitionTextTab.Padding = new Padding(3);
            definitionTextTab.Size = new Size(316, 156);
            definitionTextTab.TabIndex = 1;
            definitionTextTab.Text = "Full Text";
            // 
            // definitionTextData
            // 
            definitionTextData.AutoSize = true;
            definitionTextData.Dock = DockStyle.Fill;
            definitionTextData.HeaderText = "Definition";
            definitionTextData.HeaderVisible = false;
            definitionTextData.Location = new Point(3, 3);
            definitionTextData.Name = "definitionTextData";
            definitionTextData.ReadOnly = false;
            definitionTextData.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil Segoe UI;}}\r\n{\\*\\generator Riched20 10.0.19041}\\viewkind4\\uc1 \r\n\\pard\\f0\\fs18\\par\r\n}\r\n";
            definitionTextData.Size = new Size(310, 150);
            definitionTextData.TabIndex = 0;
            definitionTextData.Validated += DefinitionTextData_Validated;
            definitionTextData.Enter += DefinitionTextData_Enter;
            // 
            // definitionSummaryTab
            // 
            definitionSummaryTab.BackColor = SystemColors.Control;
            definitionSummaryTab.Controls.Add(definitionSummaryData);
            definitionSummaryTab.Location = new Point(4, 24);
            definitionSummaryTab.Name = "definitionSummaryTab";
            definitionSummaryTab.Padding = new Padding(3);
            definitionSummaryTab.Size = new Size(316, 156);
            definitionSummaryTab.TabIndex = 0;
            definitionSummaryTab.Text = "Summary";
            // 
            // definitionSummaryData
            // 
            definitionSummaryData.Dock = DockStyle.Fill;
            definitionSummaryData.Location = new Point(3, 3);
            definitionSummaryData.Multiline = true;
            definitionSummaryData.Name = "definitionSummaryData";
            definitionSummaryData.ScrollBars = ScrollBars.Both;
            definitionSummaryData.Size = new Size(310, 150);
            definitionSummaryData.TabIndex = 0;
            // 
            // definitionLayout
            // 
            definitionLayout.ColumnCount = 3;
            definitionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            definitionLayout.ColumnStyles.Add(new ColumnStyle());
            definitionLayout.ColumnStyles.Add(new ColumnStyle());
            definitionLayout.Controls.Add(definitionData, 0, 0);
            definitionLayout.Controls.Add(definitionTab, 0, 1);
            definitionLayout.Controls.Add(applyCommand, 2, 0);
            definitionLayout.Controls.Add(Sync, 1, 0);
            definitionLayout.Dock = DockStyle.Fill;
            definitionLayout.Location = new Point(0, 0);
            definitionLayout.Name = "definitionLayout";
            definitionLayout.RowCount = 2;
            definitionLayout.RowStyles.Add(new RowStyle());
            definitionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            definitionLayout.Size = new Size(330, 240);
            definitionLayout.TabIndex = 0;
            // 
            // definitionData
            // 
            definitionData.AutoSize = true;
            definitionData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            definitionData.Dock = DockStyle.Fill;
            definitionData.DropDownStyle = ComboBoxStyle.DropDown;
            definitionData.HeaderText = "Definition type";
            definitionData.Location = new Point(3, 3);
            definitionData.Name = "definitionData";
            definitionData.ReadOnly = false;
            definitionData.Size = new Size(193, 44);
            definitionData.TabIndex = 0;
            definitionData.SelectedIndexChanged += definitionData_SelectedIndexChanged;
            definitionData.Validated += DefinitionData_Validated;
            // 
            // applyCommand
            // 
            applyCommand.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            applyCommand.AutoSize = true;
            applyCommand.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            applyCommand.Image = Properties.Resources.NewRichTextBox;
            applyCommand.Location = new Point(265, 22);
            applyCommand.Name = "applyCommand";
            applyCommand.Size = new Size(62, 25);
            applyCommand.TabIndex = 5;
            applyCommand.Text = "apply";
            applyCommand.TextImageRelation = TextImageRelation.ImageBeforeText;
            applyCommand.UseVisualStyleBackColor = true;
            applyCommand.Click += ApplyCommand_Click;
            // 
            // Sync
            // 
            Sync.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Sync.AutoSize = true;
            Sync.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Sync.Image = Properties.Resources.SyncContent;
            Sync.Location = new Point(202, 22);
            Sync.Name = "Sync";
            Sync.Size = new Size(57, 25);
            Sync.TabIndex = 6;
            Sync.Text = "sync";
            Sync.TextImageRelation = TextImageRelation.ImageBeforeText;
            Sync.UseVisualStyleBackColor = true;
            Sync.Click += Sync_Click;
            // 
            // DomainDefinition
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(definitionLayout);
            Name = "DomainDefinition";
            Size = new Size(330, 240);
            definitionTab.ResumeLayout(false);
            definitionTextTab.ResumeLayout(false);
            definitionTextTab.PerformLayout();
            definitionSummaryTab.ResumeLayout(false);
            definitionSummaryTab.PerformLayout();
            definitionLayout.ResumeLayout(false);
            definitionLayout.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel definitionLayout;
        private DataDictionary.Main.Controls.ComboBoxData definitionData;
        private TabPage definitionSummaryTab;
        private TabPage definitionTextTab;
        private Button applyCommand;
        private TextBox definitionSummaryData;
        private DataDictionary.Main.Controls.RichTextBoxData definitionTextData;
        private Button Sync;
    }
}
