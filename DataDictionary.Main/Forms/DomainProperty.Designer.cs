namespace DataDictionary.Main.Forms
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            TableLayoutPanel domainPropertyLayout;
            attributeNameData = new Controls.TextBoxData();
            perpertyValueData = new Controls.TextBoxData();
            propertyNameValue = new Controls.ComboBoxData();
            domainPropertyLayout = new TableLayoutPanel();
            domainPropertyLayout.SuspendLayout();
            SuspendLayout();
            // 
            // domainPropertyLayout
            // 
            domainPropertyLayout.ColumnCount = 1;
            domainPropertyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            domainPropertyLayout.Controls.Add(attributeNameData, 0, 0);
            domainPropertyLayout.Controls.Add(perpertyValueData, 0, 2);
            domainPropertyLayout.Controls.Add(propertyNameValue, 0, 1);
            domainPropertyLayout.Dock = DockStyle.Fill;
            domainPropertyLayout.Location = new Point(0, 0);
            domainPropertyLayout.Name = "domainPropertyLayout";
            domainPropertyLayout.RowCount = 3;
            domainPropertyLayout.RowStyles.Add(new RowStyle());
            domainPropertyLayout.RowStyles.Add(new RowStyle());
            domainPropertyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            domainPropertyLayout.Size = new Size(271, 250);
            domainPropertyLayout.TabIndex = 0;
            // 
            // attributeNameData
            // 
            attributeNameData.AutoSize = true;
            attributeNameData.Dock = DockStyle.Fill;
            attributeNameData.HeaderText = "Attribute Name";
            attributeNameData.Location = new Point(3, 3);
            attributeNameData.Multiline = false;
            attributeNameData.Name = "attributeNameData";
            attributeNameData.ReadOnly = true;
            attributeNameData.Size = new Size(265, 44);
            attributeNameData.TabIndex = 0;
            // 
            // perpertyValueData
            // 
            perpertyValueData.AutoSize = true;
            perpertyValueData.Dock = DockStyle.Fill;
            perpertyValueData.HeaderText = "Peroperty Value";
            perpertyValueData.Location = new Point(3, 103);
            perpertyValueData.Multiline = true;
            perpertyValueData.Name = "perpertyValueData";
            perpertyValueData.ReadOnly = false;
            perpertyValueData.Size = new Size(265, 144);
            perpertyValueData.TabIndex = 1;
            // 
            // propertyNameValue
            // 
            propertyNameValue.AutoSize = true;
            propertyNameValue.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            propertyNameValue.DataSource = null;
            propertyNameValue.Dock = DockStyle.Fill;
            propertyNameValue.HeaderText = "Propety Name";
            propertyNameValue.Location = new Point(3, 53);
            propertyNameValue.Name = "propertyNameValue";
            propertyNameValue.ReadOnly = false;
            propertyNameValue.SelectedIndex = -1;
            propertyNameValue.SelectedItem = null;
            propertyNameValue.SelectedValue = null;
            propertyNameValue.Size = new Size(265, 44);
            propertyNameValue.TabIndex = 2;
            // 
            // DomainProperty
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(271, 250);
            Controls.Add(domainPropertyLayout);
            Name = "DomainProperty";
            Text = "DomainProperty";
            domainPropertyLayout.ResumeLayout(false);
            domainPropertyLayout.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Controls.TextBoxData attributeNameData;
        private Controls.TextBoxData perpertyValueData;
        private Controls.ComboBoxData propertyNameValue;
    }
}