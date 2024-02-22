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
            TableLayoutPanel scheamLayout;
            extendedPropertiesData = new DataGridView();
            propertyNameData = new DataGridViewTextBoxColumn();
            propertyValueData = new DataGridViewTextBoxColumn();
            catalogNameData = new Controls.TextBoxData();
            schemaNameData = new Controls.TextBoxData();
            isSystemData = new CheckBox();
            errorProvider = new ErrorProvider(components);
            bindingSchema = new BindingSource(components);
            bindingProperties = new BindingSource(components);
            scheamLayout = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)extendedPropertiesData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSchema).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingProperties).BeginInit();
            scheamLayout.SuspendLayout();
            SuspendLayout();
            // 
            // extendedPropertiesData
            // 
            extendedPropertiesData.AllowUserToAddRows = false;
            extendedPropertiesData.AllowUserToDeleteRows = false;
            extendedPropertiesData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            extendedPropertiesData.Columns.AddRange(new DataGridViewColumn[] { propertyNameData, propertyValueData });
            scheamLayout.SetColumnSpan(extendedPropertiesData, 2);
            extendedPropertiesData.Dock = DockStyle.Fill;
            extendedPropertiesData.Location = new Point(3, 103);
            extendedPropertiesData.Name = "extendedPropertiesData";
            extendedPropertiesData.ReadOnly = true;
            extendedPropertiesData.Size = new Size(404, 180);
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
            scheamLayout.SetColumnSpan(catalogNameData, 2);
            catalogNameData.Dock = DockStyle.Fill;
            catalogNameData.HeaderText = "Catalog Name";
            catalogNameData.Location = new Point(3, 3);
            catalogNameData.Multiline = false;
            catalogNameData.Name = "catalogNameData";
            catalogNameData.ReadOnly = true;
            catalogNameData.Size = new Size(404, 44);
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
            schemaNameData.Size = new Size(323, 44);
            schemaNameData.TabIndex = 7;
            // 
            // isSystemData
            // 
            isSystemData.AutoCheck = false;
            isSystemData.AutoSize = true;
            isSystemData.Location = new Point(332, 53);
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
            // scheamLayout
            // 
            scheamLayout.ColumnCount = 2;
            scheamLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            scheamLayout.ColumnStyles.Add(new ColumnStyle());
            scheamLayout.Controls.Add(isSystemData, 1, 1);
            scheamLayout.Controls.Add(extendedPropertiesData, 0, 2);
            scheamLayout.Controls.Add(catalogNameData, 0, 0);
            scheamLayout.Controls.Add(schemaNameData, 0, 1);
            scheamLayout.Dock = DockStyle.Fill;
            scheamLayout.Location = new Point(0, 25);
            scheamLayout.Name = "scheamLayout";
            scheamLayout.RowCount = 3;
            scheamLayout.RowStyles.Add(new RowStyle());
            scheamLayout.RowStyles.Add(new RowStyle());
            scheamLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            scheamLayout.Size = new Size(410, 286);
            scheamLayout.TabIndex = 1;
            // 
            // DbSchema
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(410, 311);
            Controls.Add(scheamLayout);
            Name = "DbSchema";
            Text = "Database Schema";
            Load += DbSchema_Load;
            Controls.SetChildIndex(scheamLayout, 0);
            ((System.ComponentModel.ISupportInitialize)extendedPropertiesData).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSchema).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingProperties).EndInit();
            scheamLayout.ResumeLayout(false);
            scheamLayout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView extendedPropertiesData;
        private DataGridViewTextBoxColumn propertyNameData;
        private DataGridViewTextBoxColumn propertyValueData;
        private ErrorProvider errorProvider;
        private Controls.TextBoxData catalogNameData;
        private Controls.TextBoxData schemaNameData;
        private CheckBox isSystemData;
        private BindingSource bindingSchema;
        private BindingSource bindingProperties;
        private TableLayoutPanel scheamLayout;
    }
}