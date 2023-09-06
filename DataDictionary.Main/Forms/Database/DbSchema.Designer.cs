namespace DataDictionary.Main.Forms.Database
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
            extendedPropertiesData = new DataGridView();
            propertyNameData = new DataGridViewTextBoxColumn();
            propertyValueData = new DataGridViewTextBoxColumn();
            catalogNameData = new Controls.TextBoxData();
            schemaNameData = new Controls.TextBoxData();
            isSystemData = new CheckBox();
            errorProvider = new ErrorProvider(components);
            dbSchemaLayout = new TableLayoutPanel();
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
            dbSchemaLayout.Controls.Add(extendedPropertiesData, 0, 2);
            dbSchemaLayout.Controls.Add(catalogNameData, 0, 0);
            dbSchemaLayout.Controls.Add(schemaNameData, 0, 1);
            dbSchemaLayout.Controls.Add(isSystemData, 1, 1);
            dbSchemaLayout.Dock = DockStyle.Fill;
            dbSchemaLayout.Location = new Point(0, 0);
            dbSchemaLayout.Name = "dbSchemaLayout";
            dbSchemaLayout.RowCount = 3;
            dbSchemaLayout.RowStyles.Add(new RowStyle());
            dbSchemaLayout.RowStyles.Add(new RowStyle());
            dbSchemaLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            dbSchemaLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            dbSchemaLayout.Size = new Size(384, 311);
            dbSchemaLayout.TabIndex = 0;
            // 
            // extendedPropertiesData
            // 
            extendedPropertiesData.AllowUserToAddRows = false;
            extendedPropertiesData.AllowUserToDeleteRows = false;
            extendedPropertiesData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            extendedPropertiesData.Columns.AddRange(new DataGridViewColumn[] { propertyNameData, propertyValueData });
            dbSchemaLayout.SetColumnSpan(extendedPropertiesData, 2);
            extendedPropertiesData.Dock = DockStyle.Fill;
            extendedPropertiesData.Location = new Point(3, 103);
            extendedPropertiesData.Name = "extendedPropertiesData";
            extendedPropertiesData.ReadOnly = true;
            extendedPropertiesData.RowTemplate.Height = 25;
            extendedPropertiesData.Size = new Size(378, 205);
            extendedPropertiesData.TabIndex = 4;
            // 
            // propertyNameData
            // 
            propertyNameData.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            propertyNameData.DataPropertyName = "PropertyName";
            propertyNameData.HeaderText = "Property Name";
            propertyNameData.Name = "propertyNameData";
            propertyNameData.ReadOnly = true;
            propertyNameData.Width = 112;
            // 
            // propertyValueData
            // 
            propertyValueData.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            propertyValueData.DataPropertyName = "PropertyValue";
            propertyValueData.HeaderText = "PropertyValue";
            propertyValueData.Name = "propertyValueData";
            propertyValueData.ReadOnly = true;
            // 
            // catalogNameData
            // 
            catalogNameData.AutoSize = true;
            dbSchemaLayout.SetColumnSpan(catalogNameData, 2);
            catalogNameData.Dock = DockStyle.Fill;
            catalogNameData.HeaderText = "Catalog Name";
            catalogNameData.Location = new Point(3, 3);
            catalogNameData.Multiline = false;
            catalogNameData.Name = "catalogNameData";
            catalogNameData.ReadOnly = true;
            catalogNameData.Size = new Size(378, 44);
            catalogNameData.TabIndex = 6;
            // 
            // schemaNameData
            // 
            schemaNameData.AutoSize = true;
            schemaNameData.Dock = DockStyle.Fill;
            schemaNameData.HeaderText = "Schema Name";
            schemaNameData.Location = new Point(3, 53);
            schemaNameData.Multiline = false;
            schemaNameData.Name = "schemaNameData";
            schemaNameData.ReadOnly = true;
            schemaNameData.Size = new Size(297, 44);
            schemaNameData.TabIndex = 7;
            // 
            // isSystemData
            // 
            isSystemData.AutoCheck = false;
            isSystemData.AutoSize = true;
            isSystemData.Location = new Point(306, 53);
            isSystemData.Name = "isSystemData";
            isSystemData.Size = new Size(75, 19);
            isSystemData.TabIndex = 5;
            isSystemData.Text = "Is System";
            isSystemData.UseVisualStyleBackColor = true;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // DbSchema
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 311);
            Controls.Add(dbSchemaLayout);
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
        private DataGridView extendedPropertiesData;
        private DataGridViewTextBoxColumn propertyNameData;
        private DataGridViewTextBoxColumn propertyValueData;
        private ErrorProvider errorProvider;
        private Controls.TextBoxData catalogNameData;
        private Controls.TextBoxData schemaNameData;
        private CheckBox isSystemData;
    }
}