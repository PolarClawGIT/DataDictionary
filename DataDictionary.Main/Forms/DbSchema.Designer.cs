namespace DataDictionary.Main.Forms
{
    partial class DbSchema
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
            TableLayoutPanel dbSchemaLayout;
            Label schemaNameLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbSchema));
            catalogNameLayout = new Label();
            catalogNameData = new TextBox();
            schemaNameData = new TextBox();
            extendedPropertiesData = new DataGridView();
            propertyNameData = new DataGridViewTextBoxColumn();
            propertyValueData = new DataGridViewTextBoxColumn();
            errorProvider = new ErrorProvider(components);
            isSystemData = new CheckBox();
            dbSchemaLayout = new TableLayoutPanel();
            schemaNameLayout = new Label();
            dbSchemaLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)extendedPropertiesData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // dbSchemaLayout
            // 
            dbSchemaLayout.ColumnCount = 2;
            dbSchemaLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            dbSchemaLayout.ColumnStyles.Add(new ColumnStyle());
            dbSchemaLayout.Controls.Add(catalogNameLayout, 0, 0);
            dbSchemaLayout.Controls.Add(catalogNameData, 0, 1);
            dbSchemaLayout.Controls.Add(schemaNameLayout, 0, 2);
            dbSchemaLayout.Controls.Add(schemaNameData, 0, 3);
            dbSchemaLayout.Controls.Add(extendedPropertiesData, 0, 4);
            dbSchemaLayout.Controls.Add(isSystemData, 1, 3);
            dbSchemaLayout.Dock = DockStyle.Fill;
            dbSchemaLayout.Location = new Point(0, 0);
            dbSchemaLayout.Name = "dbSchemaLayout";
            dbSchemaLayout.RowCount = 5;
            dbSchemaLayout.RowStyles.Add(new RowStyle());
            dbSchemaLayout.RowStyles.Add(new RowStyle());
            dbSchemaLayout.RowStyles.Add(new RowStyle());
            dbSchemaLayout.RowStyles.Add(new RowStyle());
            dbSchemaLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            dbSchemaLayout.Size = new Size(384, 311);
            dbSchemaLayout.TabIndex = 0;
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
            // catalogNameData
            // 
            dbSchemaLayout.SetColumnSpan(catalogNameData, 2);
            catalogNameData.Dock = DockStyle.Fill;
            catalogNameData.Location = new Point(3, 18);
            catalogNameData.Name = "catalogNameData";
            catalogNameData.ReadOnly = true;
            catalogNameData.Size = new Size(378, 23);
            catalogNameData.TabIndex = 1;
            // 
            // schemaNameLayout
            // 
            schemaNameLayout.AutoSize = true;
            schemaNameLayout.Location = new Point(3, 44);
            schemaNameLayout.Name = "schemaNameLayout";
            schemaNameLayout.Size = new Size(84, 15);
            schemaNameLayout.TabIndex = 2;
            schemaNameLayout.Text = "Schema Name";
            // 
            // schemaNameData
            // 
            schemaNameData.Dock = DockStyle.Fill;
            schemaNameData.Location = new Point(3, 62);
            schemaNameData.Name = "schemaNameData";
            schemaNameData.ReadOnly = true;
            schemaNameData.Size = new Size(297, 23);
            schemaNameData.TabIndex = 3;
            // 
            // extendedPropertiesData
            // 
            extendedPropertiesData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            extendedPropertiesData.Columns.AddRange(new DataGridViewColumn[] { propertyNameData, propertyValueData });
            dbSchemaLayout.SetColumnSpan(extendedPropertiesData, 2);
            extendedPropertiesData.Dock = DockStyle.Fill;
            extendedPropertiesData.Location = new Point(3, 91);
            extendedPropertiesData.Name = "extendedPropertiesData";
            extendedPropertiesData.RowTemplate.Height = 25;
            extendedPropertiesData.Size = new Size(378, 217);
            extendedPropertiesData.TabIndex = 4;
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
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // isSystemData
            // 
            isSystemData.AutoCheck = false;
            isSystemData.AutoSize = true;
            isSystemData.Location = new Point(306, 62);
            isSystemData.Name = "isSystemData";
            isSystemData.Size = new Size(75, 19);
            isSystemData.TabIndex = 5;
            isSystemData.Text = "Is System";
            isSystemData.UseVisualStyleBackColor = true;
            // 
            // DbSchema
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 311);
            Controls.Add(dbSchemaLayout);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "DbSchema";
            Text = "Database Schema";
            Load += DbSchema_Load;
            dbSchemaLayout.ResumeLayout(false);
            dbSchemaLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)extendedPropertiesData).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TextBox catalogNameData;
        private TextBox schemaNameData;
        private DataGridView extendedPropertiesData;
        private DataGridViewTextBoxColumn propertyNameData;
        private DataGridViewTextBoxColumn propertyValueData;
        private ErrorProvider errorProvider;
        private Label catalogNameLayout;
        private CheckBox isSystemData;
    }
}