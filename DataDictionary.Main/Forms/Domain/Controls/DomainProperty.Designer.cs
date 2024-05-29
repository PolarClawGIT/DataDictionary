namespace DataDictionary.Main.Forms.Domain.Controls
{
    partial class DomainProperty
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
            propertyTabs = new TabControl();
            propertyValueTab = new TabPage();
            propertyValueData = new DataDictionary.Main.Controls.TextBoxData();
            propertyChoiceTab = new TabPage();
            propertyChoiceData = new DataDictionary.Main.Controls.CheckedListBoxData();
            propertyDefinitionTab = new TabPage();
            propertyDefinitionData = new DataDictionary.Main.Controls.RichTextBoxData();
            propertyTypeData = new DataDictionary.Main.Controls.ComboBoxData();
            propertyLayout = new TableLayoutPanel();
            applyCommand = new Button();
            propertyTabs.SuspendLayout();
            propertyValueTab.SuspendLayout();
            propertyChoiceTab.SuspendLayout();
            propertyDefinitionTab.SuspendLayout();
            propertyLayout.SuspendLayout();
            SuspendLayout();
            // 
            // propertyTabs
            // 
            propertyLayout.SetColumnSpan(propertyTabs, 2);
            propertyTabs.Controls.Add(propertyValueTab);
            propertyTabs.Controls.Add(propertyChoiceTab);
            propertyTabs.Controls.Add(propertyDefinitionTab);
            propertyTabs.Dock = DockStyle.Fill;
            propertyTabs.Location = new Point(3, 53);
            propertyTabs.Name = "propertyTabs";
            propertyTabs.SelectedIndex = 0;
            propertyTabs.Size = new Size(309, 212);
            propertyTabs.TabIndex = 2;
            // 
            // propertyValueTab
            // 
            propertyValueTab.BackColor = SystemColors.Control;
            propertyValueTab.Controls.Add(propertyValueData);
            propertyValueTab.Location = new Point(4, 24);
            propertyValueTab.Name = "propertyValueTab";
            propertyValueTab.Padding = new Padding(3);
            propertyValueTab.Size = new Size(301, 184);
            propertyValueTab.TabIndex = 0;
            propertyValueTab.Text = "Value";
            // 
            // propertyValueData
            // 
            propertyValueData.AutoSize = true;
            propertyValueData.Dock = DockStyle.Fill;
            propertyValueData.HeaderText = "Property Value";
            propertyValueData.Location = new Point(3, 3);
            propertyValueData.Multiline = true;
            propertyValueData.Name = "propertyValueData";
            propertyValueData.ReadOnly = false;
            propertyValueData.Size = new Size(295, 178);
            propertyValueData.TabIndex = 1;
            // 
            // propertyChoiceTab
            // 
            propertyChoiceTab.BackColor = SystemColors.Control;
            propertyChoiceTab.Controls.Add(propertyChoiceData);
            propertyChoiceTab.Location = new Point(4, 24);
            propertyChoiceTab.Name = "propertyChoiceTab";
            propertyChoiceTab.Padding = new Padding(3);
            propertyChoiceTab.Size = new Size(192, 72);
            propertyChoiceTab.TabIndex = 2;
            propertyChoiceTab.Text = "Choice";
            // 
            // propertyChoiceData
            // 
            propertyChoiceData.AutoSize = true;
            propertyChoiceData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            propertyChoiceData.CheckOnClick = true;
            propertyChoiceData.DataSource = null;
            propertyChoiceData.DisplayMember = "";
            propertyChoiceData.Dock = DockStyle.Fill;
            propertyChoiceData.HeaderText = "Property Choice";
            propertyChoiceData.Location = new Point(3, 3);
            propertyChoiceData.Name = "propertyChoiceData";
            propertyChoiceData.Size = new Size(186, 66);
            propertyChoiceData.TabIndex = 2;
            propertyChoiceData.ItemCheck += PropertyChoiceData_ItemCheck;
            propertyChoiceData.EnabledChanged += PropertyChoiceData_EnabledChanged;
            // 
            // propertyDefinitionTab
            // 
            propertyDefinitionTab.BackColor = SystemColors.Control;
            propertyDefinitionTab.Controls.Add(propertyDefinitionData);
            propertyDefinitionTab.Location = new Point(4, 24);
            propertyDefinitionTab.Name = "propertyDefinitionTab";
            propertyDefinitionTab.Padding = new Padding(3);
            propertyDefinitionTab.Size = new Size(192, 72);
            propertyDefinitionTab.TabIndex = 1;
            propertyDefinitionTab.Text = "Definition";
            // 
            // propertyDefinitionData
            // 
            propertyDefinitionData.AutoSize = true;
            propertyDefinitionData.Dock = DockStyle.Fill;
            propertyDefinitionData.HeaderText = "Definition";
            propertyDefinitionData.Location = new Point(3, 3);
            propertyDefinitionData.Name = "propertyDefinitionData";
            propertyDefinitionData.ReadOnly = false;
            propertyDefinitionData.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil Segoe UI;}}\r\n{\\*\\generator Riched20 10.0.19041}\\viewkind4\\uc1 \r\n\\pard\\f0\\fs18\\par\r\n}\r\n";
            propertyDefinitionData.Size = new Size(186, 66);
            propertyDefinitionData.TabIndex = 0;
            propertyDefinitionData.Validated += PropertyDefinitionData_Validated;
            // 
            // propertyTypeData
            // 
            propertyTypeData.AutoSize = true;
            propertyTypeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            propertyTypeData.Dock = DockStyle.Fill;
            propertyTypeData.DropDownStyle = ComboBoxStyle.DropDown;
            propertyTypeData.HeaderText = "Property Type";
            propertyTypeData.Location = new Point(3, 3);
            propertyTypeData.Name = "propertyTypeData";
            propertyTypeData.ReadOnly = false;
            propertyTypeData.Size = new Size(241, 44);
            propertyTypeData.TabIndex = 0;
            propertyTypeData.SelectedIndexChanged += propertyTypeData_SelectedIndexChanged;
            // 
            // propertyLayout
            // 
            propertyLayout.ColumnCount = 2;
            propertyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            propertyLayout.ColumnStyles.Add(new ColumnStyle());
            propertyLayout.Controls.Add(applyCommand, 1, 0);
            propertyLayout.Controls.Add(propertyTabs, 0, 1);
            propertyLayout.Controls.Add(propertyTypeData, 0, 0);
            propertyLayout.Dock = DockStyle.Fill;
            propertyLayout.Location = new Point(0, 0);
            propertyLayout.Name = "propertyLayout";
            propertyLayout.RowCount = 2;
            propertyLayout.RowStyles.Add(new RowStyle());
            propertyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            propertyLayout.Size = new Size(315, 268);
            propertyLayout.TabIndex = 3;
            // 
            // applyCommand
            // 
            applyCommand.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            applyCommand.AutoSize = true;
            applyCommand.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            applyCommand.Image = Properties.Resources.NewProperty;
            applyCommand.Location = new Point(250, 22);
            applyCommand.Name = "applyCommand";
            applyCommand.Size = new Size(62, 25);
            applyCommand.TabIndex = 4;
            applyCommand.Text = "apply";
            applyCommand.TextImageRelation = TextImageRelation.ImageBeforeText;
            applyCommand.UseVisualStyleBackColor = true;
            applyCommand.Click += ApplyCommand_Click;
            // 
            // DomainProperty
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(propertyLayout);
            Name = "DomainProperty";
            Size = new Size(315, 268);
            propertyTabs.ResumeLayout(false);
            propertyValueTab.ResumeLayout(false);
            propertyValueTab.PerformLayout();
            propertyChoiceTab.ResumeLayout(false);
            propertyChoiceTab.PerformLayout();
            propertyDefinitionTab.ResumeLayout(false);
            propertyDefinitionTab.PerformLayout();
            propertyLayout.ResumeLayout(false);
            propertyLayout.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabPage propertyValueTab;
        private DataDictionary.Main.Controls.ComboBoxData propertyTypeData;
        private DataDictionary.Main.Controls.TextBoxData propertyValueData;
        private DataDictionary.Main.Controls.CheckedListBoxData propertyChoiceData;
        private TabPage propertyDefinitionTab;
        private DataDictionary.Main.Controls.RichTextBoxData propertyDefinitionData;
        private TabControl propertyTabs;
        private TabPage propertyChoiceTab;
        private TableLayoutPanel propertyLayout;
        private Button applyCommand;
    }
}
