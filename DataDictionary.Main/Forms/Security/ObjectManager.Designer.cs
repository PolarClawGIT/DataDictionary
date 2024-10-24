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
            objectPermissionData = new DataGridView();
            roleIdColumn = new DataGridViewComboBoxColumn();
            isGrantColumn = new DataGridViewCheckBoxColumn();
            isDenyColumn = new DataGridViewCheckBoxColumn();
            objectOwnerData = new DataGridView();
            principalIdColumn = new DataGridViewComboBoxColumn();
            objectNameData = new Controls.TextBoxData();
            bindingPermissions = new BindingSource(components);
            bindingObject = new BindingSource(components);
            bindingOwner = new BindingSource(components);
            objectSecurityLayout = new TableLayoutPanel();
            objectSecurityLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)objectPermissionData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)objectOwnerData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingPermissions).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingObject).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingOwner).BeginInit();
            SuspendLayout();
            // 
            // objectSecurityLayout
            // 
            objectSecurityLayout.ColumnCount = 1;
            objectSecurityLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            objectSecurityLayout.Controls.Add(objectPermissionData, 0, 2);
            objectSecurityLayout.Controls.Add(objectOwnerData, 0, 1);
            objectSecurityLayout.Controls.Add(objectNameData, 0, 0);
            objectSecurityLayout.Dock = DockStyle.Fill;
            objectSecurityLayout.Location = new Point(0, 25);
            objectSecurityLayout.Name = "objectSecurityLayout";
            objectSecurityLayout.RowCount = 3;
            objectSecurityLayout.RowStyles.Add(new RowStyle());
            objectSecurityLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            objectSecurityLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            objectSecurityLayout.Size = new Size(508, 325);
            objectSecurityLayout.TabIndex = 4;
            // 
            // objectPermissionData
            // 
            objectPermissionData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            objectPermissionData.Columns.AddRange(new DataGridViewColumn[] { roleIdColumn, isGrantColumn, isDenyColumn });
            objectPermissionData.Dock = DockStyle.Fill;
            objectPermissionData.Location = new Point(3, 190);
            objectPermissionData.Name = "objectPermissionData";
            objectPermissionData.Size = new Size(502, 132);
            objectPermissionData.TabIndex = 15;
            // 
            // roleIdColumn
            // 
            roleIdColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            roleIdColumn.HeaderText = "Role Name (permission)";
            roleIdColumn.Name = "roleIdColumn";
            // 
            // isGrantColumn
            // 
            isGrantColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            isGrantColumn.FillWeight = 30F;
            isGrantColumn.HeaderText = "is Grant";
            isGrantColumn.Name = "isGrantColumn";
            // 
            // isDenyColumn
            // 
            isDenyColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            isDenyColumn.FillWeight = 30F;
            isDenyColumn.HeaderText = "is Deny";
            isDenyColumn.Name = "isDenyColumn";
            // 
            // objectOwnerData
            // 
            objectOwnerData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            objectOwnerData.Columns.AddRange(new DataGridViewColumn[] { principalIdColumn });
            objectOwnerData.Dock = DockStyle.Fill;
            objectOwnerData.Location = new Point(3, 53);
            objectOwnerData.Name = "objectOwnerData";
            objectOwnerData.Size = new Size(502, 131);
            objectOwnerData.TabIndex = 14;
            // 
            // principalIdColumn
            // 
            principalIdColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            principalIdColumn.DataPropertyName = "PrincipalId";
            principalIdColumn.HeaderText = "Principal Name (owner)";
            principalIdColumn.Name = "principalIdColumn";
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
            // ObjectManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(508, 350);
            Controls.Add(objectSecurityLayout);
            Name = "ObjectManager";
            Text = "ObjectManager";
            Load += ObjectManager_Load;
            Controls.SetChildIndex(objectSecurityLayout, 0);
            objectSecurityLayout.ResumeLayout(false);
            objectSecurityLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)objectPermissionData).EndInit();
            ((System.ComponentModel.ISupportInitialize)objectOwnerData).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingPermissions).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingObject).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingOwner).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel objectSecurityLayout;
        private Controls.TextBoxData objectNameData;
        private BindingSource bindingPermissions;
        private DataGridView objectOwnerData;
        private DataGridViewComboBoxColumn principalIdColumn;
        private DataGridView objectPermissionData;
        private DataGridViewComboBoxColumn roleIdColumn;
        private DataGridViewCheckBoxColumn isGrantColumn;
        private DataGridViewCheckBoxColumn isDenyColumn;
        private BindingSource bindingObject;
        private BindingSource bindingOwner;
    }
}