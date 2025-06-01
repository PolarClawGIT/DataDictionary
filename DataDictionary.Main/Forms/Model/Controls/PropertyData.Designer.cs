namespace DataDictionary.Main.Forms.Model.Controls
{
    partial class PropertyData
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
            propertyGrid = new DataGridView();
            propertyIdColumn = new DataGridViewComboBoxColumn();
            propertyValueColumn = new DataGridViewTextBoxColumn();
            propertyTabs = new TabControl();
            propertyValueTab = new TabPage();
            propertyValueData = new DataDictionary.Main.Controls.TextBoxData();
            propertyChoiceTab = new TabPage();
            propertyChoiceData = new DataDictionary.Main.Controls.CheckedListBoxData();
            propertyTypeData = new DataDictionary.Main.Controls.ComboBoxData();
            propertyLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)propertyGrid).BeginInit();
            propertyTabs.SuspendLayout();
            propertyValueTab.SuspendLayout();
            propertyChoiceTab.SuspendLayout();
            SuspendLayout();
            // 
            // propertyLayout
            // 
            propertyLayout.ColumnCount = 1;
            propertyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            propertyLayout.Controls.Add(propertyGrid, 0, 0);
            propertyLayout.Controls.Add(propertyTabs, 0, 2);
            propertyLayout.Controls.Add(propertyTypeData, 0, 1);
            propertyLayout.Dock = DockStyle.Fill;
            propertyLayout.Location = new Point(0, 0);
            propertyLayout.Name = "propertyLayout";
            propertyLayout.RowCount = 3;
            propertyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            propertyLayout.RowStyles.Add(new RowStyle());
            propertyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            propertyLayout.Size = new Size(330, 350);
            propertyLayout.TabIndex = 4;
            // 
            // propertyGrid
            // 
            propertyGrid.AllowUserToAddRows = false;
            propertyGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            propertyGrid.Columns.AddRange(new DataGridViewColumn[] { propertyIdColumn, propertyValueColumn });
            propertyGrid.Dock = DockStyle.Fill;
            propertyGrid.Location = new Point(3, 3);
            propertyGrid.Name = "propertyGrid";
            propertyGrid.ReadOnly = true;
            propertyGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            propertyGrid.Size = new Size(324, 143);
            propertyGrid.TabIndex = 3;
            // 
            // propertyIdColumn
            // 
            propertyIdColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            propertyIdColumn.DataPropertyName = "PropertyId";
            propertyIdColumn.FillWeight = 30F;
            propertyIdColumn.HeaderText = "Property";
            propertyIdColumn.Name = "propertyIdColumn";
            propertyIdColumn.ReadOnly = true;
            // 
            // propertyValueColumn
            // 
            propertyValueColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            propertyValueColumn.DataPropertyName = "PropertyValue";
            propertyValueColumn.FillWeight = 70F;
            propertyValueColumn.HeaderText = "Property Value";
            propertyValueColumn.Name = "propertyValueColumn";
            propertyValueColumn.ReadOnly = true;
            // 
            // propertyTabs
            // 
            propertyTabs.Controls.Add(propertyValueTab);
            propertyTabs.Controls.Add(propertyChoiceTab);
            propertyTabs.Dock = DockStyle.Fill;
            propertyTabs.Location = new Point(3, 204);
            propertyTabs.Name = "propertyTabs";
            propertyTabs.SelectedIndex = 0;
            propertyTabs.Size = new Size(324, 143);
            propertyTabs.TabIndex = 2;
            // 
            // propertyValueTab
            // 
            propertyValueTab.BackColor = SystemColors.Control;
            propertyValueTab.Controls.Add(propertyValueData);
            propertyValueTab.Location = new Point(4, 24);
            propertyValueTab.Name = "propertyValueTab";
            propertyValueTab.Padding = new Padding(3);
            propertyValueTab.Size = new Size(316, 115);
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
            propertyValueData.Size = new Size(310, 109);
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
            propertyTypeData.Location = new Point(3, 152);
            propertyTypeData.Name = "propertyTypeData";
            propertyTypeData.ReadOnly = false;
            propertyTypeData.Size = new Size(324, 46);
            propertyTypeData.TabIndex = 0;
            propertyTypeData.SelectedIndexChanged += PropertyTypeData_SelectedIndexChanged;
            propertyTypeData.SelectionChangeCommitted += PropertyTypeData_SelectionChangeCommitted;
            // 
            // PropertyData
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(propertyLayout);
            Name = "PropertyData";
            Size = new Size(330, 350);
            propertyLayout.ResumeLayout(false);
            propertyLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)propertyGrid).EndInit();
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
        private DataGridView propertyGrid;
        private DataGridViewComboBoxColumn propertyIdColumn;
        private DataGridViewTextBoxColumn propertyValueColumn;
    }
}
