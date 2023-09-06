namespace DataDictionary.Main.Forms.Domain
{
    partial class DomainAttribute
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
            TableLayoutPanel attributeLayout;
            TabControl attributeTabLayout;
            TabPage attributeDefinitionTab;
            TableLayoutPanel attributeDefinitionLayout;
            TabPage attributePropertyTab;
            TabPage attributeAlaisTab;
            TableLayoutPanel attributeAlaisTabLayout;
            attributeDescriptionData = new Controls.TextBoxData();
            attributeTitleData = new Controls.TextBoxData();
            attributeParentTitleData = new Controls.TextBoxData();
            attributeDefinitionTypeData = new Controls.ComboBoxData();
            richTextBoxData1 = new Controls.RichTextBoxData();
            attributeDefinitionNavigation = new DataGridView();
            attributePropertiesData = new DataGridView();
            propertyNameData = new DataGridViewComboBoxColumn();
            propertyValueData = new DataGridViewTextBoxColumn();
            attributeAlaisData = new DataGridView();
            aliasCatalogNameData = new DataGridViewTextBoxColumn();
            aliasSchemaNameData = new DataGridViewTextBoxColumn();
            alaisObjectNameData = new DataGridViewTextBoxColumn();
            aliasElementNameData = new DataGridViewTextBoxColumn();
            definitionTypeColumn = new DataGridViewComboBoxColumn();
            definitionColumn = new DataGridViewTextBoxColumn();
            attributeLayout = new TableLayoutPanel();
            attributeTabLayout = new TabControl();
            attributeDefinitionTab = new TabPage();
            attributeDefinitionLayout = new TableLayoutPanel();
            attributePropertyTab = new TabPage();
            attributeAlaisTab = new TabPage();
            attributeAlaisTabLayout = new TableLayoutPanel();
            attributeLayout.SuspendLayout();
            attributeTabLayout.SuspendLayout();
            attributeDefinitionTab.SuspendLayout();
            attributeDefinitionLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)attributeDefinitionNavigation).BeginInit();
            attributePropertyTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)attributePropertiesData).BeginInit();
            attributeAlaisTab.SuspendLayout();
            attributeAlaisTabLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)attributeAlaisData).BeginInit();
            SuspendLayout();
            // 
            // attributeLayout
            // 
            attributeLayout.AutoSize = true;
            attributeLayout.ColumnCount = 1;
            attributeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            attributeLayout.Controls.Add(attributeDescriptionData, 0, 2);
            attributeLayout.Controls.Add(attributeTitleData, 0, 0);
            attributeLayout.Controls.Add(attributeParentTitleData, 0, 1);
            attributeLayout.Controls.Add(attributeTabLayout, 0, 3);
            attributeLayout.Dock = DockStyle.Fill;
            attributeLayout.Location = new Point(0, 25);
            attributeLayout.Name = "attributeLayout";
            attributeLayout.RowCount = 4;
            attributeLayout.RowStyles.Add(new RowStyle());
            attributeLayout.RowStyles.Add(new RowStyle());
            attributeLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            attributeLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            attributeLayout.Size = new Size(529, 616);
            attributeLayout.TabIndex = 0;
            // 
            // attributeDescriptionData
            // 
            attributeDescriptionData.AutoSize = true;
            attributeDescriptionData.Dock = DockStyle.Fill;
            attributeDescriptionData.HeaderText = "Description (Summary)";
            attributeDescriptionData.Location = new Point(3, 91);
            attributeDescriptionData.Multiline = true;
            attributeDescriptionData.Name = "attributeDescriptionData";
            attributeDescriptionData.ReadOnly = false;
            attributeDescriptionData.Size = new Size(523, 99);
            attributeDescriptionData.TabIndex = 0;
            // 
            // attributeTitleData
            // 
            attributeTitleData.AutoSize = true;
            attributeTitleData.Dock = DockStyle.Fill;
            attributeTitleData.HeaderText = "Attribute Title";
            attributeTitleData.Location = new Point(0, 0);
            attributeTitleData.Margin = new Padding(0);
            attributeTitleData.Multiline = false;
            attributeTitleData.Name = "attributeTitleData";
            attributeTitleData.ReadOnly = false;
            attributeTitleData.Size = new Size(529, 44);
            attributeTitleData.TabIndex = 0;
            // 
            // attributeParentTitleData
            // 
            attributeParentTitleData.AutoSize = true;
            attributeParentTitleData.Dock = DockStyle.Fill;
            attributeParentTitleData.HeaderText = "Parent Attribute";
            attributeParentTitleData.Location = new Point(0, 44);
            attributeParentTitleData.Margin = new Padding(0);
            attributeParentTitleData.Multiline = false;
            attributeParentTitleData.Name = "attributeParentTitleData";
            attributeParentTitleData.ReadOnly = true;
            attributeParentTitleData.Size = new Size(529, 44);
            attributeParentTitleData.TabIndex = 1;
            // 
            // attributeTabLayout
            // 
            attributeTabLayout.Controls.Add(attributeDefinitionTab);
            attributeTabLayout.Controls.Add(attributePropertyTab);
            attributeTabLayout.Controls.Add(attributeAlaisTab);
            attributeTabLayout.Dock = DockStyle.Fill;
            attributeTabLayout.Location = new Point(3, 196);
            attributeTabLayout.Name = "attributeTabLayout";
            attributeTabLayout.SelectedIndex = 0;
            attributeTabLayout.Size = new Size(523, 417);
            attributeTabLayout.TabIndex = 4;
            // 
            // attributeDefinitionTab
            // 
            attributeDefinitionTab.Controls.Add(attributeDefinitionLayout);
            attributeDefinitionTab.Location = new Point(4, 24);
            attributeDefinitionTab.Name = "attributeDefinitionTab";
            attributeDefinitionTab.Padding = new Padding(3);
            attributeDefinitionTab.Size = new Size(515, 389);
            attributeDefinitionTab.TabIndex = 0;
            attributeDefinitionTab.Text = "Definition";
            attributeDefinitionTab.UseVisualStyleBackColor = true;
            // 
            // attributeDefinitionLayout
            // 
            attributeDefinitionLayout.ColumnCount = 1;
            attributeDefinitionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            attributeDefinitionLayout.Controls.Add(attributeDefinitionTypeData, 0, 1);
            attributeDefinitionLayout.Controls.Add(richTextBoxData1, 0, 2);
            attributeDefinitionLayout.Controls.Add(attributeDefinitionNavigation, 0, 0);
            attributeDefinitionLayout.Dock = DockStyle.Fill;
            attributeDefinitionLayout.Location = new Point(3, 3);
            attributeDefinitionLayout.Name = "attributeDefinitionLayout";
            attributeDefinitionLayout.RowCount = 3;
            attributeDefinitionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            attributeDefinitionLayout.RowStyles.Add(new RowStyle());
            attributeDefinitionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            attributeDefinitionLayout.Size = new Size(509, 383);
            attributeDefinitionLayout.TabIndex = 0;
            // 
            // attributeDefinitionTypeData
            // 
            attributeDefinitionTypeData.AutoSize = true;
            attributeDefinitionTypeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            attributeDefinitionTypeData.DataSource = null;
            attributeDefinitionTypeData.DisplayMember = "";
            attributeDefinitionTypeData.Dock = DockStyle.Fill;
            attributeDefinitionTypeData.HeaderText = "Definition Type";
            attributeDefinitionTypeData.Location = new Point(3, 102);
            attributeDefinitionTypeData.Name = "attributeDefinitionTypeData";
            attributeDefinitionTypeData.ReadOnly = false;
            attributeDefinitionTypeData.SelectedIndex = -1;
            attributeDefinitionTypeData.SelectedItem = null;
            attributeDefinitionTypeData.SelectedValue = null;
            attributeDefinitionTypeData.Size = new Size(503, 44);
            attributeDefinitionTypeData.TabIndex = 0;
            attributeDefinitionTypeData.ValueMember = "";
            // 
            // richTextBoxData1
            // 
            richTextBoxData1.AutoSize = true;
            richTextBoxData1.Dock = DockStyle.Fill;
            richTextBoxData1.HeaderText = "Definition";
            richTextBoxData1.Location = new Point(3, 152);
            richTextBoxData1.Name = "richTextBoxData1";
            richTextBoxData1.ReadOnly = false;
            richTextBoxData1.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil Segoe UI;}}\r\n{\\*\\generator Riched20 10.0.19041}\\viewkind4\\uc1 \r\n\\pard\\f0\\fs18\\par\r\n}\r\n";
            richTextBoxData1.Size = new Size(503, 228);
            richTextBoxData1.TabIndex = 1;
            // 
            // attributeDefinitionNavigation
            // 
            attributeDefinitionNavigation.AllowUserToAddRows = false;
            attributeDefinitionNavigation.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            attributeDefinitionNavigation.Columns.AddRange(new DataGridViewColumn[] { definitionTypeColumn, definitionColumn });
            attributeDefinitionNavigation.Dock = DockStyle.Fill;
            attributeDefinitionNavigation.Location = new Point(3, 3);
            attributeDefinitionNavigation.Name = "attributeDefinitionNavigation";
            attributeDefinitionNavigation.RowTemplate.Height = 25;
            attributeDefinitionNavigation.Size = new Size(503, 93);
            attributeDefinitionNavigation.TabIndex = 2;
            // 
            // attributePropertyTab
            // 
            attributePropertyTab.Controls.Add(attributePropertiesData);
            attributePropertyTab.Location = new Point(4, 24);
            attributePropertyTab.Name = "attributePropertyTab";
            attributePropertyTab.Padding = new Padding(3);
            attributePropertyTab.Size = new Size(515, 389);
            attributePropertyTab.TabIndex = 1;
            attributePropertyTab.Text = "Properties";
            attributePropertyTab.UseVisualStyleBackColor = true;
            // 
            // attributePropertiesData
            // 
            attributePropertiesData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            attributePropertiesData.Columns.AddRange(new DataGridViewColumn[] { propertyNameData, propertyValueData });
            attributePropertiesData.Dock = DockStyle.Fill;
            attributePropertiesData.Location = new Point(3, 3);
            attributePropertiesData.Name = "attributePropertiesData";
            attributePropertiesData.RowTemplate.Height = 25;
            attributePropertiesData.Size = new Size(509, 383);
            attributePropertiesData.TabIndex = 0;
            attributePropertiesData.RowValidated += attributePropertiesData_RowValidated;
            // 
            // propertyNameData
            // 
            propertyNameData.DataPropertyName = "PropertyId";
            propertyNameData.HeaderText = "Property Name";
            propertyNameData.Name = "propertyNameData";
            propertyNameData.Width = 150;
            // 
            // propertyValueData
            // 
            propertyValueData.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            propertyValueData.DataPropertyName = "PropertyValue";
            propertyValueData.HeaderText = "Property Value";
            propertyValueData.Name = "propertyValueData";
            // 
            // attributeAlaisTab
            // 
            attributeAlaisTab.Controls.Add(attributeAlaisTabLayout);
            attributeAlaisTab.Location = new Point(4, 24);
            attributeAlaisTab.Name = "attributeAlaisTab";
            attributeAlaisTab.Size = new Size(515, 389);
            attributeAlaisTab.TabIndex = 2;
            attributeAlaisTab.Text = "Alias";
            attributeAlaisTab.UseVisualStyleBackColor = true;
            // 
            // attributeAlaisTabLayout
            // 
            attributeAlaisTabLayout.ColumnCount = 1;
            attributeAlaisTabLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            attributeAlaisTabLayout.Controls.Add(attributeAlaisData, 0, 0);
            attributeAlaisTabLayout.Dock = DockStyle.Fill;
            attributeAlaisTabLayout.Location = new Point(0, 0);
            attributeAlaisTabLayout.Name = "attributeAlaisTabLayout";
            attributeAlaisTabLayout.RowCount = 1;
            attributeAlaisTabLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            attributeAlaisTabLayout.Size = new Size(515, 389);
            attributeAlaisTabLayout.TabIndex = 1;
            // 
            // attributeAlaisData
            // 
            attributeAlaisData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            attributeAlaisData.Columns.AddRange(new DataGridViewColumn[] { aliasCatalogNameData, aliasSchemaNameData, alaisObjectNameData, aliasElementNameData });
            attributeAlaisData.Dock = DockStyle.Fill;
            attributeAlaisData.Location = new Point(3, 3);
            attributeAlaisData.Name = "attributeAlaisData";
            attributeAlaisData.RowTemplate.Height = 25;
            attributeAlaisData.Size = new Size(509, 383);
            attributeAlaisData.TabIndex = 0;
            attributeAlaisData.RowValidated += attributeAlaisData_RowValidated;
            // 
            // aliasCatalogNameData
            // 
            aliasCatalogNameData.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            aliasCatalogNameData.DataPropertyName = "CatalogName";
            aliasCatalogNameData.HeaderText = "Catalog Name";
            aliasCatalogNameData.Name = "aliasCatalogNameData";
            // 
            // aliasSchemaNameData
            // 
            aliasSchemaNameData.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            aliasSchemaNameData.DataPropertyName = "SchemaName";
            aliasSchemaNameData.HeaderText = "Schema Name";
            aliasSchemaNameData.Name = "aliasSchemaNameData";
            // 
            // alaisObjectNameData
            // 
            alaisObjectNameData.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            alaisObjectNameData.DataPropertyName = "ObjectName";
            alaisObjectNameData.HeaderText = "Object Name";
            alaisObjectNameData.Name = "alaisObjectNameData";
            // 
            // aliasElementNameData
            // 
            aliasElementNameData.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            aliasElementNameData.DataPropertyName = "ElementName";
            aliasElementNameData.HeaderText = "Element Name";
            aliasElementNameData.Name = "aliasElementNameData";
            // 
            // definitionTypeColumn
            // 
            definitionTypeColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            definitionTypeColumn.HeaderText = "Definition Type";
            definitionTypeColumn.Name = "definitionTypeColumn";
            definitionTypeColumn.Width = 92;
            // 
            // definitionColumn
            // 
            definitionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            definitionColumn.HeaderText = "Definition";
            definitionColumn.Name = "definitionColumn";
            // 
            // DomainAttribute
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(529, 641);
            Controls.Add(attributeLayout);
            Name = "DomainAttribute";
            Text = "Domain Attribute";
            Load += DomainAttribute_Load;
            Controls.SetChildIndex(attributeLayout, 0);
            attributeLayout.ResumeLayout(false);
            attributeLayout.PerformLayout();
            attributeTabLayout.ResumeLayout(false);
            attributeDefinitionTab.ResumeLayout(false);
            attributeDefinitionLayout.ResumeLayout(false);
            attributeDefinitionLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)attributeDefinitionNavigation).EndInit();
            attributePropertyTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)attributePropertiesData).EndInit();
            attributeAlaisTab.ResumeLayout(false);
            attributeAlaisTabLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)attributeAlaisData).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData attributeTitleData;
        private Controls.TextBoxData attributeParentTitleData;
        private DataGridView attributePropertiesData;
        private DataGridView attributeAlaisData;
        private DataGridViewTextBoxColumn aliasCatalogNameData;
        private DataGridViewTextBoxColumn aliasSchemaNameData;
        private DataGridViewTextBoxColumn alaisObjectNameData;
        private DataGridViewTextBoxColumn aliasElementNameData;
        private DataGridViewComboBoxColumn propertyNameData;
        private DataGridViewTextBoxColumn propertyValueData;
        private Controls.TextBoxData attributeDescriptionData;
        private Controls.ComboBoxData attributeDefinitionTypeData;
        private Controls.RichTextBoxData richTextBoxData1;
        private DataGridView attributeDefinitionNavigation;
        private DataGridViewComboBoxColumn definitionTypeColumn;
        private DataGridViewTextBoxColumn definitionColumn;
    }
}