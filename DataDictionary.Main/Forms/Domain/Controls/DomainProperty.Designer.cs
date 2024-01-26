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
            TabControl propertyTabs;
            TableLayoutPanel propertyValueLayout;
            propertyValueTab = new TabPage();
            propertyTypeData = new DataDictionary.Main.Controls.ComboBoxData();
            propertyValueData = new DataDictionary.Main.Controls.TextBoxData();
            propertyChoiceData = new DataDictionary.Main.Controls.CheckedListBoxData();
            propertyDefinitionTab = new TabPage();
            propertyDefinitionData = new DataDictionary.Main.Controls.RichTextBoxData();
            propertyTabs = new TabControl();
            propertyValueLayout = new TableLayoutPanel();
            propertyTabs.SuspendLayout();
            propertyValueTab.SuspendLayout();
            propertyValueLayout.SuspendLayout();
            propertyDefinitionTab.SuspendLayout();
            SuspendLayout();
            // 
            // propertyTabs
            // 
            propertyTabs.Controls.Add(propertyValueTab);
            propertyTabs.Controls.Add(propertyDefinitionTab);
            propertyTabs.Dock = DockStyle.Fill;
            propertyTabs.Location = new Point(0, 0);
            propertyTabs.Name = "propertyTabs";
            propertyTabs.SelectedIndex = 0;
            propertyTabs.Size = new Size(360, 220);
            propertyTabs.TabIndex = 2;
            // 
            // propertyValueTab
            // 
            propertyValueTab.BackColor = SystemColors.Control;
            propertyValueTab.Controls.Add(propertyValueLayout);
            propertyValueTab.Location = new Point(4, 24);
            propertyValueTab.Name = "propertyValueTab";
            propertyValueTab.Padding = new Padding(3);
            propertyValueTab.Size = new Size(352, 192);
            propertyValueTab.TabIndex = 0;
            propertyValueTab.Text = "Value";
            // 
            // propertyValueLayout
            // 
            propertyValueLayout.ColumnCount = 2;
            propertyValueLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            propertyValueLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            propertyValueLayout.Controls.Add(propertyTypeData, 0, 0);
            propertyValueLayout.Controls.Add(propertyValueData, 0, 1);
            propertyValueLayout.Controls.Add(propertyChoiceData, 1, 0);
            propertyValueLayout.Dock = DockStyle.Fill;
            propertyValueLayout.Location = new Point(3, 3);
            propertyValueLayout.Name = "propertyValueLayout";
            propertyValueLayout.RowCount = 2;
            propertyValueLayout.RowStyles.Add(new RowStyle());
            propertyValueLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            propertyValueLayout.Size = new Size(346, 186);
            propertyValueLayout.TabIndex = 0;
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
            propertyTypeData.Size = new Size(167, 44);
            propertyTypeData.TabIndex = 0;
            propertyTypeData.SelectedIndexChanged += propertyTypeData_SelectedIndexChanged;
            // 
            // propertyValueData
            // 
            propertyValueData.AutoSize = true;
            propertyValueData.Dock = DockStyle.Fill;
            propertyValueData.HeaderText = "Property Value";
            propertyValueData.Location = new Point(3, 53);
            propertyValueData.Multiline = true;
            propertyValueData.Name = "propertyValueData";
            propertyValueData.ReadOnly = false;
            propertyValueData.Size = new Size(167, 130);
            propertyValueData.TabIndex = 1;
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
            propertyChoiceData.Location = new Point(176, 3);
            propertyChoiceData.Name = "propertyChoiceData";
            propertyValueLayout.SetRowSpan(propertyChoiceData, 2);
            propertyChoiceData.Size = new Size(167, 180);
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
            propertyDefinitionTab.Size = new Size(352, 192);
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
            propertyDefinitionData.Size = new Size(346, 186);
            propertyDefinitionData.TabIndex = 0;
            propertyDefinitionData.Validated += PropertyDefinitionData_Validated;
            // 
            // DomainProperty
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(propertyTabs);
            Name = "DomainProperty";
            Size = new Size(360, 220);
            propertyTabs.ResumeLayout(false);
            propertyValueTab.ResumeLayout(false);
            propertyValueLayout.ResumeLayout(false);
            propertyValueLayout.PerformLayout();
            propertyDefinitionTab.ResumeLayout(false);
            propertyDefinitionTab.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabPage propertyValueTab;
        private DataDictionary.Main.Controls.ComboBoxData propertyTypeData;
        private DataDictionary.Main.Controls.TextBoxData propertyValueData;
        private DataDictionary.Main.Controls.CheckedListBoxData propertyChoiceData;
        private TabPage propertyDefinitionTab;
        private DataDictionary.Main.Controls.RichTextBoxData propertyDefinitionData;

    }
}
