namespace DataDictionary.Main.Forms
{
    partial class DbColumn
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
            TableLayoutPanel dbTableLayout;
            TabControl columnDetailLayout;
            TabPage extendedPropertiesTab;
            TabPage columnsTab;
            Label schemaNameLayout;
            Label objectNameLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbColumn));
            extendedPropertiesData = new DataGridView();
            propertyNameData = new DataGridViewTextBoxColumn();
            propertyValueData = new DataGridViewTextBoxColumn();
            catalogNameLayout = new Label();
            catalogNameData = new TextBox();
            schemaNameData = new TextBox();
            tableNameData = new TextBox();
            columnNameLayout = new Label();
            columnNameData = new TextBox();
            errorProvider = new ErrorProvider(components);
            dbTableLayout = new TableLayoutPanel();
            columnDetailLayout = new TabControl();
            extendedPropertiesTab = new TabPage();
            columnsTab = new TabPage();
            schemaNameLayout = new Label();
            objectNameLayout = new Label();
            dbTableLayout.SuspendLayout();
            columnDetailLayout.SuspendLayout();
            extendedPropertiesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)extendedPropertiesData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // dbTableLayout
            // 
            dbTableLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            dbTableLayout.ColumnCount = 1;
            dbTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            dbTableLayout.Controls.Add(columnDetailLayout, 0, 8);
            dbTableLayout.Controls.Add(catalogNameLayout, 0, 0);
            dbTableLayout.Controls.Add(schemaNameLayout, 0, 2);
            dbTableLayout.Controls.Add(objectNameLayout, 0, 4);
            dbTableLayout.Controls.Add(catalogNameData, 0, 1);
            dbTableLayout.Controls.Add(schemaNameData, 0, 3);
            dbTableLayout.Controls.Add(tableNameData, 0, 5);
            dbTableLayout.Controls.Add(columnNameLayout, 0, 6);
            dbTableLayout.Controls.Add(columnNameData, 0, 7);
            dbTableLayout.Dock = DockStyle.Fill;
            dbTableLayout.Location = new Point(0, 0);
            dbTableLayout.Name = "dbTableLayout";
            dbTableLayout.RowCount = 9;
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle());
            dbTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            dbTableLayout.Size = new Size(383, 301);
            dbTableLayout.TabIndex = 1;
            // 
            // columnDetailLayout
            // 
            columnDetailLayout.Controls.Add(extendedPropertiesTab);
            columnDetailLayout.Controls.Add(columnsTab);
            columnDetailLayout.Dock = DockStyle.Fill;
            columnDetailLayout.Location = new Point(3, 179);
            columnDetailLayout.Name = "columnDetailLayout";
            columnDetailLayout.SelectedIndex = 0;
            columnDetailLayout.Size = new Size(377, 119);
            columnDetailLayout.TabIndex = 9;
            // 
            // extendedPropertiesTab
            // 
            extendedPropertiesTab.Controls.Add(extendedPropertiesData);
            extendedPropertiesTab.Location = new Point(4, 24);
            extendedPropertiesTab.Name = "extendedPropertiesTab";
            extendedPropertiesTab.Padding = new Padding(3);
            extendedPropertiesTab.Size = new Size(369, 91);
            extendedPropertiesTab.TabIndex = 0;
            extendedPropertiesTab.Text = "Extended Properties";
            extendedPropertiesTab.UseVisualStyleBackColor = true;
            // 
            // extendedPropertiesData
            // 
            extendedPropertiesData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            extendedPropertiesData.Columns.AddRange(new DataGridViewColumn[] { propertyNameData, propertyValueData });
            extendedPropertiesData.Dock = DockStyle.Fill;
            extendedPropertiesData.Location = new Point(3, 3);
            extendedPropertiesData.Name = "extendedPropertiesData";
            extendedPropertiesData.RowTemplate.Height = 25;
            extendedPropertiesData.Size = new Size(363, 85);
            extendedPropertiesData.TabIndex = 5;
            // 
            // propertyNameData
            // 
            propertyNameData.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            propertyNameData.DataPropertyName = "PropertyName";
            propertyNameData.HeaderText = "Property Name";
            propertyNameData.Name = "propertyNameData";
            propertyNameData.Width = 112;
            // 
            // propertyValueData
            // 
            propertyValueData.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            propertyValueData.DataPropertyName = "PropertyValue";
            propertyValueData.HeaderText = "PropertyValue";
            propertyValueData.Name = "propertyValueData";
            // 
            // columnsTab
            // 
            columnsTab.Location = new Point(4, 24);
            columnsTab.Name = "columnsTab";
            columnsTab.Padding = new Padding(3);
            columnsTab.Size = new Size(369, 91);
            columnsTab.TabIndex = 1;
            columnsTab.Text = "Column Detail";
            columnsTab.UseVisualStyleBackColor = true;
            // 
            // catalogNameLayout
            // 
            catalogNameLayout.AutoSize = true;
            catalogNameLayout.Location = new Point(3, 0);
            catalogNameLayout.Name = "catalogNameLayout";
            catalogNameLayout.Size = new Size(83, 15);
            catalogNameLayout.TabIndex = 0;
            catalogNameLayout.Text = "Catalog Name";
            // 
            // schemaNameLayout
            // 
            schemaNameLayout.AutoSize = true;
            schemaNameLayout.Location = new Point(3, 44);
            schemaNameLayout.Name = "schemaNameLayout";
            schemaNameLayout.Size = new Size(84, 15);
            schemaNameLayout.TabIndex = 1;
            schemaNameLayout.Text = "Schema Name";
            // 
            // objectNameLayout
            // 
            objectNameLayout.AutoSize = true;
            objectNameLayout.Location = new Point(3, 88);
            objectNameLayout.Name = "objectNameLayout";
            objectNameLayout.Size = new Size(77, 15);
            objectNameLayout.TabIndex = 2;
            objectNameLayout.Text = "Object Name";
            // 
            // catalogNameData
            // 
            catalogNameData.Dock = DockStyle.Fill;
            catalogNameData.Location = new Point(3, 18);
            catalogNameData.Name = "catalogNameData";
            catalogNameData.ReadOnly = true;
            catalogNameData.Size = new Size(377, 23);
            catalogNameData.TabIndex = 4;
            // 
            // schemaNameData
            // 
            schemaNameData.Dock = DockStyle.Fill;
            schemaNameData.Location = new Point(3, 62);
            schemaNameData.Name = "schemaNameData";
            schemaNameData.ReadOnly = true;
            schemaNameData.Size = new Size(377, 23);
            schemaNameData.TabIndex = 5;
            // 
            // tableNameData
            // 
            tableNameData.Dock = DockStyle.Fill;
            tableNameData.Location = new Point(3, 106);
            tableNameData.Name = "tableNameData";
            tableNameData.ReadOnly = true;
            tableNameData.Size = new Size(377, 23);
            tableNameData.TabIndex = 6;
            // 
            // columnNameLayout
            // 
            columnNameLayout.AutoSize = true;
            columnNameLayout.Location = new Point(3, 132);
            columnNameLayout.Name = "columnNameLayout";
            columnNameLayout.Size = new Size(85, 15);
            columnNameLayout.TabIndex = 7;
            columnNameLayout.Text = "Column Name";
            // 
            // columnNameData
            // 
            columnNameData.Dock = DockStyle.Fill;
            columnNameData.Location = new Point(3, 150);
            columnNameData.Name = "columnNameData";
            columnNameData.ReadOnly = true;
            columnNameData.Size = new Size(377, 23);
            columnNameData.TabIndex = 8;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // DbColumn
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(383, 301);
            Controls.Add(dbTableLayout);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "DbColumn";
            Text = "Database Column";
            Load += DbColumn_Load;
            dbTableLayout.ResumeLayout(false);
            dbTableLayout.PerformLayout();
            columnDetailLayout.ResumeLayout(false);
            extendedPropertiesTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)extendedPropertiesData).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label catalogNameLayout;
        private TextBox catalogNameData;
        private TextBox schemaNameData;
        private TextBox tableNameData;
        private Label columnNameLayout;
        private TextBox columnNameData;
        private DataGridView extendedPropertiesData;
        private DataGridViewTextBoxColumn propertyNameData;
        private DataGridViewTextBoxColumn propertyValueData;
        private ErrorProvider errorProvider;
    }
}