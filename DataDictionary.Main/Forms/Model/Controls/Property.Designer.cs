namespace DataDictionary.Main.Forms.Model.Controls
{
    partial class Property
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
            propertyLayout = new TableLayoutPanel();
            propertyTabs = new TabControl();
            propertyValueTab = new TabPage();
            propertyValueData = new DataDictionary.Main.Controls.TextBoxData();
            propertyChoiceTab = new TabPage();
            propertyChoiceData = new DataDictionary.Main.Controls.CheckedListBoxData();
            propertyTypeData = new DataDictionary.Main.Controls.ComboBoxData();
            propertyLayout.SuspendLayout();
            propertyTabs.SuspendLayout();
            propertyValueTab.SuspendLayout();
            propertyChoiceTab.SuspendLayout();
            SuspendLayout();
            // 
            // propertyLayout
            // 
            propertyLayout.ColumnCount = 1;
            propertyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            propertyLayout.Controls.Add(propertyTabs, 0, 1);
            propertyLayout.Controls.Add(propertyTypeData, 0, 0);
            propertyLayout.Dock = DockStyle.Fill;
            propertyLayout.Location = new Point(0, 0);
            propertyLayout.Name = "propertyLayout";
            propertyLayout.RowCount = 2;
            propertyLayout.RowStyles.Add(new RowStyle());
            propertyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            propertyLayout.Size = new Size(265, 198);
            propertyLayout.TabIndex = 4;
            // 
            // propertyTabs
            // 
            propertyTabs.Controls.Add(propertyValueTab);
            propertyTabs.Controls.Add(propertyChoiceTab);
            propertyTabs.Dock = DockStyle.Fill;
            propertyTabs.Location = new Point(3, 55);
            propertyTabs.Name = "propertyTabs";
            propertyTabs.SelectedIndex = 0;
            propertyTabs.Size = new Size(259, 140);
            propertyTabs.TabIndex = 2;
            // 
            // propertyValueTab
            // 
            propertyValueTab.BackColor = SystemColors.Control;
            propertyValueTab.Controls.Add(propertyValueData);
            propertyValueTab.Location = new Point(4, 24);
            propertyValueTab.Name = "propertyValueTab";
            propertyValueTab.Padding = new Padding(3);
            propertyValueTab.Size = new Size(251, 112);
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
            propertyValueData.Size = new Size(245, 106);
            propertyValueData.TabIndex = 1;
            propertyValueData.WordWrap = true;
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
            // propertyTypeData
            // 
            propertyTypeData.AutoSize = true;
            propertyTypeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            propertyTypeData.Dock = DockStyle.Fill;
            propertyTypeData.DropDownStyle = ComboBoxStyle.DropDownList;
            propertyTypeData.HeaderText = "Property Type";
            propertyTypeData.Location = new Point(3, 3);
            propertyTypeData.Name = "propertyTypeData";
            propertyTypeData.ReadOnly = false;
            propertyTypeData.Size = new Size(259, 46);
            propertyTypeData.TabIndex = 0;
            propertyTypeData.SelectedIndexChanged += PropertyTypeData_SelectedIndexChanged;
            propertyTypeData.SelectionChangeCommitted += PropertyTypeData_SelectionChangeCommitted;
            // 
            // Property
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(propertyLayout);
            Name = "Property";
            Size = new Size(265, 198);
            propertyLayout.ResumeLayout(false);
            propertyLayout.PerformLayout();
            propertyTabs.ResumeLayout(false);
            propertyValueTab.ResumeLayout(false);
            propertyValueTab.PerformLayout();
            propertyChoiceTab.ResumeLayout(false);
            propertyChoiceTab.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel propertyLayout;
        private TabControl propertyTabs;
        private TabPage propertyValueTab;
        private DataDictionary.Main.Controls.TextBoxData propertyValueData;
        private TabPage propertyChoiceTab;
        private DataDictionary.Main.Controls.CheckedListBoxData propertyChoiceData;
        private DataDictionary.Main.Controls.ComboBoxData propertyTypeData;
    }
}
