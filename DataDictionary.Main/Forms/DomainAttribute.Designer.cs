namespace DataDictionary.Main.Forms
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
            TabPage attributePropertyTab;
            TabPage attributeAlaisTab;
            TableLayoutPanel attributeAlaisTabLayout;
            attributeDescriptionData = new Controls.RichTextBoxData();
            attributePropertiesData = new DataGridView();
            propertyNameData = new DataGridViewComboBoxColumn();
            propertyValueData = new DataGridViewTextBoxColumn();
            attributeAlaisData = new DataGridView();
            aliasCatalogNameData = new DataGridViewTextBoxColumn();
            aliasSchemaNameData = new DataGridViewTextBoxColumn();
            alaisObjectNameData = new DataGridViewTextBoxColumn();
            aliasElementNameData = new DataGridViewTextBoxColumn();
            attributeTitleData = new Controls.TextBoxData();
            attributeParentTitleData = new Controls.TextBoxData();
            attributeLayout = new TableLayoutPanel();
            attributeTabLayout = new TabControl();
            attributeDefinitionTab = new TabPage();
            attributePropertyTab = new TabPage();
            attributeAlaisTab = new TabPage();
            attributeAlaisTabLayout = new TableLayoutPanel();
            attributeLayout.SuspendLayout();
            attributeTabLayout.SuspendLayout();
            attributeDefinitionTab.SuspendLayout();
            attributePropertyTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)attributePropertiesData).BeginInit();
            attributeAlaisTab.SuspendLayout();
            attributeAlaisTabLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)attributeAlaisData).BeginInit();
            SuspendLayout();
            // 
            // attributeLayout
            // 
            attributeLayout.ColumnCount = 1;
            attributeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            attributeLayout.Controls.Add(attributeTabLayout, 0, 2);
            attributeLayout.Controls.Add(attributeTitleData, 0, 0);
            attributeLayout.Controls.Add(attributeParentTitleData, 0, 1);
            attributeLayout.Dock = DockStyle.Fill;
            attributeLayout.Location = new Point(0, 0);
            attributeLayout.Name = "attributeLayout";
            attributeLayout.RowCount = 3;
            attributeLayout.RowStyles.Add(new RowStyle());
            attributeLayout.RowStyles.Add(new RowStyle());
            attributeLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            attributeLayout.Size = new Size(529, 522);
            attributeLayout.TabIndex = 0;
            // 
            // attributeTabLayout
            // 
            attributeTabLayout.Controls.Add(attributeDefinitionTab);
            attributeTabLayout.Controls.Add(attributePropertyTab);
            attributeTabLayout.Controls.Add(attributeAlaisTab);
            attributeTabLayout.Dock = DockStyle.Fill;
            attributeTabLayout.Location = new Point(3, 91);
            attributeTabLayout.Name = "attributeTabLayout";
            attributeTabLayout.SelectedIndex = 0;
            attributeTabLayout.Size = new Size(523, 428);
            attributeTabLayout.TabIndex = 4;
            // 
            // attributeDefinitionTab
            // 
            attributeDefinitionTab.Controls.Add(attributeDescriptionData);
            attributeDefinitionTab.Location = new Point(4, 24);
            attributeDefinitionTab.Name = "attributeDefinitionTab";
            attributeDefinitionTab.Padding = new Padding(3);
            attributeDefinitionTab.Size = new Size(515, 400);
            attributeDefinitionTab.TabIndex = 0;
            attributeDefinitionTab.Text = "Definition";
            attributeDefinitionTab.UseVisualStyleBackColor = true;
            // 
            // attributeDescriptionData
            // 
            attributeDescriptionData.AutoSize = true;
            attributeDescriptionData.Dock = DockStyle.Fill;
            attributeDescriptionData.HeaderText = "Attribute Description";
            attributeDescriptionData.Location = new Point(3, 3);
            attributeDescriptionData.Margin = new Padding(0);
            attributeDescriptionData.Name = "attributeDescriptionData";
            attributeDescriptionData.ReadOnly = false;
            attributeDescriptionData.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil Segoe UI;}}\r\n{\\*\\generator Riched20 10.0.19041}\\viewkind4\\uc1 \r\n\\pard\\f0\\fs18\\par\r\n}\r\n";
            attributeDescriptionData.Size = new Size(509, 394);
            attributeDescriptionData.TabIndex = 3;
            // 
            // attributePropertyTab
            // 
            attributePropertyTab.Controls.Add(attributePropertiesData);
            attributePropertyTab.Location = new Point(4, 24);
            attributePropertyTab.Name = "attributePropertyTab";
            attributePropertyTab.Padding = new Padding(3);
            attributePropertyTab.Size = new Size(515, 400);
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
            attributePropertiesData.Size = new Size(509, 394);
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
            attributeAlaisTab.Size = new Size(515, 400);
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
            attributeAlaisTabLayout.Size = new Size(515, 400);
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
            attributeAlaisData.Size = new Size(509, 394);
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
            // DomainAttribute
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(529, 522);
            Controls.Add(attributeLayout);
            Name = "DomainAttribute";
            Text = "Domain Attribute";
            Load += DomainAttribute_Load;
            attributeLayout.ResumeLayout(false);
            attributeLayout.PerformLayout();
            attributeTabLayout.ResumeLayout(false);
            attributeDefinitionTab.ResumeLayout(false);
            attributeDefinitionTab.PerformLayout();
            attributePropertyTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)attributePropertiesData).EndInit();
            attributeAlaisTab.ResumeLayout(false);
            attributeAlaisTabLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)attributeAlaisData).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Controls.TextBoxData attributeTitleData;
        private Controls.TextBoxData attributeParentTitleData;
        private Controls.RichTextBoxData attributeDescriptionData;
        private DataGridView attributePropertiesData;
        private DataGridView attributeAlaisData;
        private DataGridViewTextBoxColumn aliasCatalogNameData;
        private DataGridViewTextBoxColumn aliasSchemaNameData;
        private DataGridViewTextBoxColumn alaisObjectNameData;
        private DataGridViewTextBoxColumn aliasElementNameData;
        private DataGridViewComboBoxColumn propertyNameData;
        private DataGridViewTextBoxColumn propertyValueData;
    }
}