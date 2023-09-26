namespace DataDictionary.Main.Forms.Database
{
    partial class DbRoutine
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
            TableLayoutPanel routineLayout;
            TabControl routineTab;
            catalogNameData = new Controls.TextBoxData();
            schemaNameData = new Controls.TextBoxData();
            routineNameData = new Controls.TextBoxData();
            routineTypeData = new Controls.TextBoxData();
            isSystemData = new CheckBox();
            extendedPropertyData = new TabPage();
            extendedPropertiesData = new DataGridView();
            propertyNameData = new DataGridViewTextBoxColumn();
            propertyValueData = new DataGridViewTextBoxColumn();
            routineParameterData = new TabPage();
            parametersData = new DataGridView();
            ParameterNameValue = new DataGridViewTextBoxColumn();
            DataTypeValue = new DataGridViewTextBoxColumn();
            IsNullableValue = new DataGridViewCheckBoxColumn();
            routineDependencyData = new TabPage();
            dependenciesData = new DataGridView();
            referenceSchemaValue = new DataGridViewTextBoxColumn();
            referenceObjectValue = new DataGridViewTextBoxColumn();
            referenceObjectTypeValue = new DataGridViewTextBoxColumn();
            referenceColumnValue = new DataGridViewTextBoxColumn();
            routineLayout = new TableLayoutPanel();
            routineTab = new TabControl();
            routineLayout.SuspendLayout();
            routineTab.SuspendLayout();
            extendedPropertyData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)extendedPropertiesData).BeginInit();
            routineParameterData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)parametersData).BeginInit();
            routineDependencyData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dependenciesData).BeginInit();
            SuspendLayout();
            // 
            // routineLayout
            // 
            routineLayout.ColumnCount = 2;
            routineLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            routineLayout.ColumnStyles.Add(new ColumnStyle());
            routineLayout.Controls.Add(catalogNameData, 0, 0);
            routineLayout.Controls.Add(schemaNameData, 0, 1);
            routineLayout.Controls.Add(routineNameData, 0, 2);
            routineLayout.Controls.Add(routineTypeData, 0, 3);
            routineLayout.Controls.Add(isSystemData, 1, 3);
            routineLayout.Controls.Add(routineTab, 0, 4);
            routineLayout.Dock = DockStyle.Fill;
            routineLayout.Location = new Point(0, 25);
            routineLayout.Name = "routineLayout";
            routineLayout.RowCount = 5;
            routineLayout.RowStyles.Add(new RowStyle());
            routineLayout.RowStyles.Add(new RowStyle());
            routineLayout.RowStyles.Add(new RowStyle());
            routineLayout.RowStyles.Add(new RowStyle());
            routineLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            routineLayout.Size = new Size(439, 453);
            routineLayout.TabIndex = 1;
            // 
            // catalogNameData
            // 
            catalogNameData.AutoSize = true;
            routineLayout.SetColumnSpan(catalogNameData, 2);
            catalogNameData.Dock = DockStyle.Fill;
            catalogNameData.HeaderText = "Catalog Name";
            catalogNameData.Location = new Point(3, 3);
            catalogNameData.Multiline = false;
            catalogNameData.Name = "catalogNameData";
            catalogNameData.ReadOnly = true;
            catalogNameData.Size = new Size(433, 44);
            catalogNameData.TabIndex = 0;
            // 
            // schemaNameData
            // 
            schemaNameData.AutoSize = true;
            routineLayout.SetColumnSpan(schemaNameData, 2);
            schemaNameData.Dock = DockStyle.Fill;
            schemaNameData.HeaderText = "Schema Name";
            schemaNameData.Location = new Point(3, 53);
            schemaNameData.Multiline = false;
            schemaNameData.Name = "schemaNameData";
            schemaNameData.ReadOnly = true;
            schemaNameData.Size = new Size(433, 44);
            schemaNameData.TabIndex = 1;
            // 
            // routineNameData
            // 
            routineNameData.AutoSize = true;
            routineLayout.SetColumnSpan(routineNameData, 2);
            routineNameData.Dock = DockStyle.Fill;
            routineNameData.HeaderText = "Routine Name";
            routineNameData.Location = new Point(3, 103);
            routineNameData.Multiline = false;
            routineNameData.Name = "routineNameData";
            routineNameData.ReadOnly = true;
            routineNameData.Size = new Size(433, 44);
            routineNameData.TabIndex = 2;
            // 
            // routineTypeData
            // 
            routineTypeData.AutoSize = true;
            routineTypeData.Dock = DockStyle.Fill;
            routineTypeData.HeaderText = "Routine Type Name";
            routineTypeData.Location = new Point(3, 153);
            routineTypeData.Multiline = false;
            routineTypeData.Name = "routineTypeData";
            routineTypeData.ReadOnly = true;
            routineTypeData.Size = new Size(352, 44);
            routineTypeData.TabIndex = 3;
            // 
            // isSystemData
            // 
            isSystemData.AutoSize = true;
            isSystemData.Location = new Point(361, 153);
            isSystemData.Name = "isSystemData";
            isSystemData.Size = new Size(75, 19);
            isSystemData.TabIndex = 4;
            isSystemData.Text = "Is System";
            isSystemData.UseVisualStyleBackColor = true;
            // 
            // routineTab
            // 
            routineLayout.SetColumnSpan(routineTab, 2);
            routineTab.Controls.Add(routineParameterData);
            routineTab.Controls.Add(routineDependencyData);
            routineTab.Controls.Add(extendedPropertyData);
            routineTab.Dock = DockStyle.Fill;
            routineTab.Location = new Point(3, 203);
            routineTab.Name = "routineTab";
            routineTab.SelectedIndex = 0;
            routineTab.Size = new Size(433, 247);
            routineTab.TabIndex = 5;
            // 
            // extendedPropertyData
            // 
            extendedPropertyData.BackColor = SystemColors.Control;
            extendedPropertyData.Controls.Add(extendedPropertiesData);
            extendedPropertyData.Location = new Point(4, 24);
            extendedPropertyData.Name = "extendedPropertyData";
            extendedPropertyData.Padding = new Padding(3);
            extendedPropertyData.Size = new Size(425, 219);
            extendedPropertyData.TabIndex = 0;
            extendedPropertyData.Text = "Extended Properties";
            // 
            // extendedPropertiesData
            // 
            extendedPropertiesData.AllowUserToAddRows = false;
            extendedPropertiesData.AllowUserToDeleteRows = false;
            extendedPropertiesData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            extendedPropertiesData.Columns.AddRange(new DataGridViewColumn[] { propertyNameData, propertyValueData });
            extendedPropertiesData.Dock = DockStyle.Fill;
            extendedPropertiesData.Location = new Point(3, 3);
            extendedPropertiesData.Name = "extendedPropertiesData";
            extendedPropertiesData.ReadOnly = true;
            extendedPropertiesData.RowTemplate.Height = 25;
            extendedPropertiesData.Size = new Size(419, 213);
            extendedPropertiesData.TabIndex = 6;
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
            // routineParameterData
            // 
            routineParameterData.BackColor = SystemColors.Control;
            routineParameterData.Controls.Add(parametersData);
            routineParameterData.Location = new Point(4, 24);
            routineParameterData.Name = "routineParameterData";
            routineParameterData.Padding = new Padding(3);
            routineParameterData.Size = new Size(425, 219);
            routineParameterData.TabIndex = 1;
            routineParameterData.Text = "Parameters";
            // 
            // parametersData
            // 
            parametersData.AllowUserToAddRows = false;
            parametersData.AllowUserToDeleteRows = false;
            parametersData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            parametersData.Columns.AddRange(new DataGridViewColumn[] { ParameterNameValue, DataTypeValue, IsNullableValue });
            parametersData.Dock = DockStyle.Fill;
            parametersData.Location = new Point(3, 3);
            parametersData.Name = "parametersData";
            parametersData.RowTemplate.Height = 25;
            parametersData.Size = new Size(419, 213);
            parametersData.TabIndex = 1;
            // 
            // ParameterNameValue
            // 
            ParameterNameValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ParameterNameValue.DataPropertyName = "ParameterName";
            ParameterNameValue.HeaderText = "Parameter Name";
            ParameterNameValue.Name = "ParameterNameValue";
            // 
            // DataTypeValue
            // 
            DataTypeValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataTypeValue.DataPropertyName = "DataType";
            DataTypeValue.HeaderText = "Data Type";
            DataTypeValue.Name = "DataTypeValue";
            // 
            // IsNullableValue
            // 
            IsNullableValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            IsNullableValue.DataPropertyName = "IsNullable";
            IsNullableValue.HeaderText = "Is Nullable";
            IsNullableValue.Name = "IsNullableValue";
            IsNullableValue.Width = 61;
            // 
            // routineDependencyData
            // 
            routineDependencyData.BackColor = SystemColors.Control;
            routineDependencyData.Controls.Add(dependenciesData);
            routineDependencyData.Location = new Point(4, 24);
            routineDependencyData.Name = "routineDependencyData";
            routineDependencyData.Size = new Size(425, 219);
            routineDependencyData.TabIndex = 2;
            routineDependencyData.Text = "Dependencies";
            // 
            // dependenciesData
            // 
            dependenciesData.AllowUserToAddRows = false;
            dependenciesData.AllowUserToDeleteRows = false;
            dependenciesData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dependenciesData.Columns.AddRange(new DataGridViewColumn[] { referenceSchemaValue, referenceObjectValue, referenceObjectTypeValue, referenceColumnValue });
            dependenciesData.Dock = DockStyle.Fill;
            dependenciesData.Location = new Point(0, 0);
            dependenciesData.Name = "dependenciesData";
            dependenciesData.RowTemplate.Height = 25;
            dependenciesData.Size = new Size(425, 219);
            dependenciesData.TabIndex = 0;
            // 
            // referenceSchemaValue
            // 
            referenceSchemaValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            referenceSchemaValue.DataPropertyName = "ReferenceSchemaName";
            referenceSchemaValue.HeaderText = "Schema Name";
            referenceSchemaValue.Name = "referenceSchemaValue";
            // 
            // referenceObjectValue
            // 
            referenceObjectValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            referenceObjectValue.DataPropertyName = "ReferenceObjectName";
            referenceObjectValue.HeaderText = "Object Name";
            referenceObjectValue.Name = "referenceObjectValue";
            // 
            // referenceObjectTypeValue
            // 
            referenceObjectTypeValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            referenceObjectTypeValue.DataPropertyName = "ReferenceObjectType";
            referenceObjectTypeValue.FillWeight = 70F;
            referenceObjectTypeValue.HeaderText = "Object Type";
            referenceObjectTypeValue.Name = "referenceObjectTypeValue";
            // 
            // referenceColumnValue
            // 
            referenceColumnValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            referenceColumnValue.DataPropertyName = "ReferenceColumnName";
            referenceColumnValue.HeaderText = "Column Name";
            referenceColumnValue.Name = "referenceColumnValue";
            // 
            // DbRoutine
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(439, 478);
            Controls.Add(routineLayout);
            Name = "DbRoutine";
            Text = "DbRoutine";
            Load += DbRoutine_Load;
            Controls.SetChildIndex(routineLayout, 0);
            routineLayout.ResumeLayout(false);
            routineLayout.PerformLayout();
            routineTab.ResumeLayout(false);
            extendedPropertyData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)extendedPropertiesData).EndInit();
            routineParameterData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)parametersData).EndInit();
            routineDependencyData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dependenciesData).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel routineLayout;
        private Controls.TextBoxData catalogNameData;
        private Controls.TextBoxData schemaNameData;
        private Controls.TextBoxData routineNameData;
        private Controls.TextBoxData routineTypeData;
        private CheckBox isSystemData;
        private TabPage extendedPropertyData;
        private TabPage routineParameterData;
        private TabPage routineDependencyData;
        private DataGridView extendedPropertiesData;
        private DataGridViewTextBoxColumn propertyNameData;
        private DataGridViewTextBoxColumn propertyValueData;
        private DataGridView parametersData;
        private DataGridViewTextBoxColumn ParameterNameValue;
        private DataGridViewTextBoxColumn DataTypeValue;
        private DataGridViewCheckBoxColumn IsNullableValue;
        private DataGridView dependenciesData;
        private DataGridViewTextBoxColumn referenceSchemaValue;
        private DataGridViewTextBoxColumn referenceObjectValue;
        private DataGridViewTextBoxColumn referenceObjectTypeValue;
        private DataGridViewTextBoxColumn referenceColumnValue;
    }
}