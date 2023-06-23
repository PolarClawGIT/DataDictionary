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
            TableLayoutPanel schemaLayout;
            Label catalogNameLayout;
            Label schemaNameLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbSchema));
            catalogNameData = new TextBox();
            schemaNameData = new TextBox();
            extendedPropertiesData = new DataGridView();
            propertyNameData = new DataGridViewTextBoxColumn();
            propertyValueData = new DataGridViewTextBoxColumn();
            schemaLayout = new TableLayoutPanel();
            catalogNameLayout = new Label();
            schemaNameLayout = new Label();
            schemaLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)extendedPropertiesData).BeginInit();
            SuspendLayout();
            // 
            // schemaLayout
            // 
            schemaLayout.ColumnCount = 1;
            schemaLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            schemaLayout.Controls.Add(catalogNameLayout, 0, 0);
            schemaLayout.Controls.Add(catalogNameData, 0, 1);
            schemaLayout.Controls.Add(schemaNameLayout, 0, 2);
            schemaLayout.Controls.Add(schemaNameData, 0, 3);
            schemaLayout.Controls.Add(extendedPropertiesData, 0, 4);
            schemaLayout.Dock = DockStyle.Fill;
            schemaLayout.Location = new Point(0, 0);
            schemaLayout.Name = "schemaLayout";
            schemaLayout.RowCount = 5;
            schemaLayout.RowStyles.Add(new RowStyle());
            schemaLayout.RowStyles.Add(new RowStyle());
            schemaLayout.RowStyles.Add(new RowStyle());
            schemaLayout.RowStyles.Add(new RowStyle());
            schemaLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            schemaLayout.Size = new Size(384, 311);
            schemaLayout.TabIndex = 0;
            // 
            // catalogNameLayout
            // 
            catalogNameLayout.AutoSize = true;
            catalogNameLayout.Location = new Point(3, 0);
            catalogNameLayout.Name = "catalogNameLayout";
            catalogNameLayout.Size = new Size(136, 15);
            catalogNameLayout.TabIndex = 0;
            catalogNameLayout.Text = "Catalog Name/Database";
            // 
            // catalogNameData
            // 
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
            schemaNameData.Size = new Size(378, 23);
            schemaNameData.TabIndex = 3;
            // 
            // extendedPropertiesData
            // 
            extendedPropertiesData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            extendedPropertiesData.Columns.AddRange(new DataGridViewColumn[] { propertyNameData, propertyValueData });
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
            // DbSchema
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 311);
            Controls.Add(schemaLayout);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "DbSchema";
            Text = "Database Schema";
            Load += DbSchema_Load;
            schemaLayout.ResumeLayout(false);
            schemaLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)extendedPropertiesData).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TextBox catalogNameData;
        private Label schemaNameLayout;
        private TextBox schemaNameData;
        private DataGridView extendedPropertiesData;
        private DataGridViewTextBoxColumn propertyNameData;
        private DataGridViewTextBoxColumn propertyValueData;
    }
}