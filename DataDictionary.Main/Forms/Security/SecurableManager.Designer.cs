namespace DataDictionary.Main.Forms.Security
{
    partial class SecurableManager
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
            securablePermissionData = new DataGridView();
            roleIdColumn = new DataGridViewComboBoxColumn();
            isGrantColumn = new DataGridViewCheckBoxColumn();
            isDenyColumn = new DataGridViewCheckBoxColumn();
            securableOwnerData = new DataGridView();
            principalIdColumn = new DataGridViewComboBoxColumn();
            securableTitleData = new Controls.TextBoxData();
            bindingPermissions = new BindingSource(components);
            bindingSecurable = new BindingSource(components);
            bindingOwner = new BindingSource(components);
            objectSecurityLayout = new TableLayoutPanel();
            objectSecurityLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)securablePermissionData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)securableOwnerData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingPermissions).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSecurable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingOwner).BeginInit();
            SuspendLayout();
            // 
            // objectSecurityLayout
            // 
            objectSecurityLayout.ColumnCount = 1;
            objectSecurityLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            objectSecurityLayout.Controls.Add(securablePermissionData, 0, 2);
            objectSecurityLayout.Controls.Add(securableOwnerData, 0, 1);
            objectSecurityLayout.Controls.Add(securableTitleData, 0, 0);
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
            // securablePermissionData
            // 
            securablePermissionData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            securablePermissionData.Columns.AddRange(new DataGridViewColumn[] { roleIdColumn, isGrantColumn, isDenyColumn });
            securablePermissionData.Dock = DockStyle.Fill;
            securablePermissionData.Location = new Point(3, 190);
            securablePermissionData.Name = "securablePermissionData";
            securablePermissionData.Size = new Size(502, 132);
            securablePermissionData.TabIndex = 15;
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
            // securableOwnerData
            // 
            securableOwnerData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            securableOwnerData.Columns.AddRange(new DataGridViewColumn[] { principalIdColumn });
            securableOwnerData.Dock = DockStyle.Fill;
            securableOwnerData.Location = new Point(3, 53);
            securableOwnerData.Name = "securableOwnerData";
            securableOwnerData.Size = new Size(502, 131);
            securableOwnerData.TabIndex = 14;
            // 
            // principalIdColumn
            // 
            principalIdColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            principalIdColumn.DataPropertyName = "PrincipalId";
            principalIdColumn.HeaderText = "Principal Name (owner)";
            principalIdColumn.Name = "principalIdColumn";
            // 
            // securableTitleData
            // 
            securableTitleData.AutoSize = true;
            securableTitleData.Dock = DockStyle.Fill;
            securableTitleData.HeaderText = "Securable Title";
            securableTitleData.Location = new Point(3, 3);
            securableTitleData.Multiline = false;
            securableTitleData.Name = "securableTitleData";
            securableTitleData.ReadOnly = true;
            securableTitleData.Size = new Size(502, 44);
            securableTitleData.TabIndex = 0;
            securableTitleData.WordWrap = true;
            // 
            // SecurableManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(508, 350);
            Controls.Add(objectSecurityLayout);
            Name = "SecurableManager";
            Text = "ObjectManager";
            Load += ObjectManager_Load;
            Controls.SetChildIndex(objectSecurityLayout, 0);
            objectSecurityLayout.ResumeLayout(false);
            objectSecurityLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)securablePermissionData).EndInit();
            ((System.ComponentModel.ISupportInitialize)securableOwnerData).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingPermissions).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSecurable).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingOwner).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData securableTitleData;
        private BindingSource bindingPermissions;
        private DataGridView securableOwnerData;
        private DataGridViewComboBoxColumn principalIdColumn;
        private DataGridView securablePermissionData;
        private DataGridViewComboBoxColumn roleIdColumn;
        private DataGridViewCheckBoxColumn isGrantColumn;
        private DataGridViewCheckBoxColumn isDenyColumn;
        private BindingSource bindingSecurable;
        private BindingSource bindingOwner;
    }
}