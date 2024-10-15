namespace DataDictionary.Main.Forms.Security
{
    partial class ObjectManager
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
            TableLayoutPanel objectSecurityLayout;
            objectNameData = new Controls.TextBoxData();
            objectSecurityData = new DataGridView();
            roleIdColumn = new DataGridViewComboBoxColumn();
            principalIdColum = new DataGridViewComboBoxColumn();
            isGrantColumn = new DataGridViewCheckBoxColumn();
            isDenyColumn = new DataGridViewCheckBoxColumn();
            bindingPermissions = new BindingSource(components);
            objectSecurityLayout = new TableLayoutPanel();
            objectSecurityLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)objectSecurityData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingPermissions).BeginInit();
            SuspendLayout();
            // 
            // objectSecurityLayout
            // 
            objectSecurityLayout.ColumnCount = 1;
            objectSecurityLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            objectSecurityLayout.Controls.Add(objectNameData, 0, 0);
            objectSecurityLayout.Controls.Add(objectSecurityData, 0, 1);
            objectSecurityLayout.Dock = DockStyle.Fill;
            objectSecurityLayout.Location = new Point(0, 25);
            objectSecurityLayout.Name = "objectSecurityLayout";
            objectSecurityLayout.RowCount = 2;
            objectSecurityLayout.RowStyles.Add(new RowStyle());
            objectSecurityLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            objectSecurityLayout.Size = new Size(508, 325);
            objectSecurityLayout.TabIndex = 4;
            // 
            // objectNameData
            // 
            objectNameData.AutoSize = true;
            objectNameData.Dock = DockStyle.Fill;
            objectNameData.HeaderText = "Object Name";
            objectNameData.Location = new Point(3, 3);
            objectNameData.Multiline = false;
            objectNameData.Name = "objectNameData";
            objectNameData.ReadOnly = true;
            objectNameData.Size = new Size(502, 44);
            objectNameData.TabIndex = 0;
            objectNameData.WordWrap = true;
            // 
            // objectSecurityData
            // 
            objectSecurityData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            objectSecurityData.Columns.AddRange(new DataGridViewColumn[] { roleIdColumn, principalIdColum, isGrantColumn, isDenyColumn });
            objectSecurityData.Dock = DockStyle.Fill;
            objectSecurityData.Location = new Point(3, 53);
            objectSecurityData.Name = "objectSecurityData";
            objectSecurityData.Size = new Size(502, 269);
            objectSecurityData.TabIndex = 1;
            // 
            // roleIdColumn
            // 
            roleIdColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            roleIdColumn.HeaderText = "Role Name";
            roleIdColumn.Name = "roleIdColumn";
            // 
            // principalIdColum
            // 
            principalIdColum.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            principalIdColum.HeaderText = "Owner";
            principalIdColum.Name = "principalIdColum";
            // 
            // isGrantColumn
            // 
            isGrantColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            isGrantColumn.HeaderText = "Grant";
            isGrantColumn.Name = "isGrantColumn";
            isGrantColumn.Width = 42;
            // 
            // isDenyColumn
            // 
            isDenyColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            isDenyColumn.HeaderText = "Deny";
            isDenyColumn.Name = "isDenyColumn";
            isDenyColumn.Width = 40;
            // 
            // ObjectManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(508, 350);
            Controls.Add(objectSecurityLayout);
            Name = "ObjectManager";
            Text = "ObjectManager";
            Controls.SetChildIndex(objectSecurityLayout, 0);
            objectSecurityLayout.ResumeLayout(false);
            objectSecurityLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)objectSecurityData).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingPermissions).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel objectSecurityLayout;
        private Controls.TextBoxData objectNameData;
        private DataGridView objectSecurityData;
        private DataGridViewComboBoxColumn roleIdColumn;
        private DataGridViewComboBoxColumn principalIdColum;
        private DataGridViewCheckBoxColumn isGrantColumn;
        private DataGridViewCheckBoxColumn isDenyColumn;
        private BindingSource bindingPermissions;
    }
}