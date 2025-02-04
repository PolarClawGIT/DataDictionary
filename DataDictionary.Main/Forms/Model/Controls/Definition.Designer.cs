namespace DataDictionary.Main.Forms.Model.Controls
{
    partial class Definition
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
            syncSummaryTextCommand = new Button();
            definitionTab = new TabControl();
            definitionTab.SuspendLayout();
            definitionTextTab.SuspendLayout();
            definitionSummaryTab.SuspendLayout();
            definitionLayout.SuspendLayout();
            SuspendLayout();
            // 
            // definitionTab
            // 
            definitionLayout.SetColumnSpan(definitionTab, 2);
            definitionTab.Controls.Add(definitionTextTab);
            definitionTab.Controls.Add(definitionSummaryTab);
            definitionTab.Dock = DockStyle.Fill;
            definitionTab.Location = new Point(3, 55);
            definitionTab.Name = "definitionTab";
            definitionTab.SelectedIndex = 0;
            definitionTab.Size = new Size(295, 158);
            definitionTab.TabIndex = 2;
            // 
            // definitionTextTab
            // 
            definitionTextTab.BackColor = SystemColors.Control;
            definitionTextTab.Controls.Add(definitionTextData);
            definitionTextTab.Location = new Point(4, 24);
            definitionTextTab.Name = "definitionTextTab";
            definitionTextTab.Padding = new Padding(3);
            definitionTextTab.Size = new Size(287, 130);
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
            definitionTextData.Size = new Size(281, 124);
            definitionTextData.TabIndex = 0;
            definitionTextData.Validated += DefinitionTextData_Validated;
            // 
            // definitionSummaryTab
            // 
            definitionSummaryTab.BackColor = SystemColors.Control;
            definitionSummaryTab.Controls.Add(definitionSummaryData);
            definitionSummaryTab.Location = new Point(4, 24);
            definitionSummaryTab.Name = "definitionSummaryTab";
            definitionSummaryTab.Padding = new Padding(3);
            definitionSummaryTab.Size = new Size(287, 130);
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
            definitionSummaryData.Size = new Size(281, 124);
            definitionSummaryData.TabIndex = 0;
            // 
            // definitionLayout
            // 
            definitionLayout.ColumnCount = 2;
            definitionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            definitionLayout.ColumnStyles.Add(new ColumnStyle());
            definitionLayout.Controls.Add(syncSummaryTextCommand, 1, 0);
            definitionLayout.Controls.Add(definitionData, 0, 0);
            definitionLayout.Controls.Add(definitionTab, 0, 1);
            definitionLayout.Dock = DockStyle.Fill;
            definitionLayout.Location = new Point(0, 0);
            definitionLayout.Name = "definitionLayout";
            definitionLayout.RowCount = 2;
            definitionLayout.RowStyles.Add(new RowStyle());
            definitionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            definitionLayout.Size = new Size(301, 216);
            definitionLayout.TabIndex = 1;
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
            definitionData.Size = new Size(232, 46);
            definitionData.TabIndex = 0;
            definitionData.SelectedIndexChanged += DefinitionData_SelectedIndexChanged;
            // 
            // syncSummaryTextCommand
            // 
            syncSummaryTextCommand.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            syncSummaryTextCommand.AutoSize = true;
            syncSummaryTextCommand.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            syncSummaryTextCommand.Image = Properties.Resources.SyncContent;
            syncSummaryTextCommand.Location = new Point(241, 24);
            syncSummaryTextCommand.Name = "syncSummaryTextCommand";
            syncSummaryTextCommand.Size = new Size(57, 25);
            syncSummaryTextCommand.TabIndex = 7;
            syncSummaryTextCommand.Text = "sync";
            syncSummaryTextCommand.TextImageRelation = TextImageRelation.ImageBeforeText;
            syncSummaryTextCommand.UseVisualStyleBackColor = true;
            syncSummaryTextCommand.Click += SyncSummaryTextCommand_Click;
            // 
            // Definition
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(definitionLayout);
            Name = "Definition";
            Size = new Size(301, 216);
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
        private TabPage definitionTextTab;
        private DataDictionary.Main.Controls.RichTextBoxData definitionTextData;
        private TabPage definitionSummaryTab;
        private TextBox definitionSummaryData;
        private Button syncSummaryTextCommand;
    }
}
