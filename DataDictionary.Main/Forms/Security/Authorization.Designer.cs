namespace DataDictionary.Main.Forms.Security
{
    partial class Authorization
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
            TableLayoutPanel principalLayout;
            TableLayoutPanel roleLayout;
            TableLayoutPanel securableLayout;
            principalLoginData = new DataDictionary.Main.Controls.TextBoxData();
            principalNameData = new DataDictionary.Main.Controls.TextBoxData();
            principalAnnotationData = new DataDictionary.Main.Controls.TextBoxData();
            membershipData = new DataGridView();
            roleIdColumn = new DataGridViewComboBoxColumn();
            roleNameData = new DataDictionary.Main.Controls.TextBoxData();
            roleDescriptionData = new DataDictionary.Main.Controls.TextBoxData();
            isSecurityAdminData = new CheckBox();
            isHelpAdminData = new CheckBox();
            isHelpOwnerData = new CheckBox();
            isCatalogAdminData = new CheckBox();
            isCatalogOwnerData = new CheckBox();
            isLibraryAdminData = new CheckBox();
            isLibraryOwnerData = new CheckBox();
            isModelAdminData = new CheckBox();
            isModelOwnerData = new CheckBox();
            isScriptAdminData = new CheckBox();
            isScriptOwnerData = new CheckBox();
            objectPermissionData = new DataGridView();
            permissionRoleColumn = new DataGridViewTextBoxColumn();
            isGrantColumn = new DataGridViewCheckBoxColumn();
            isDenyColumn = new DataGridViewCheckBoxColumn();
            ownershipData = new DataGridView();
            principlaNameColumn = new DataGridViewTextBoxColumn();
            securableTitleData = new DataDictionary.Main.Controls.TextBoxData();
            principalsTab = new TabPage();
            principalSplit = new SplitContainer();
            principalData = new DataGridView();
            principalNameColumn = new DataGridViewTextBoxColumn();
            authorizationTab = new TabControl();
            rolesTab = new TabPage();
            roleSplit = new SplitContainer();
            roleData = new DataGridView();
            roleNameColumn = new DataGridViewTextBoxColumn();
            securableTab = new TabPage();
            securableSplit = new SplitContainer();
            securableData = new DataGridView();
            securableNameColumn = new DataGridViewTextBoxColumn();
            bindingPrincipal = new BindingSource(components);
            bindingRole = new BindingSource(components);
            bindingSecurable = new BindingSource(components);
            bindingMember = new BindingSource(components);
            bindingOwner = new BindingSource(components);
            bindingPermission = new BindingSource(components);
            principalLayout = new TableLayoutPanel();
            roleLayout = new TableLayoutPanel();
            securableLayout = new TableLayoutPanel();
            principalLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)membershipData).BeginInit();
            roleLayout.SuspendLayout();
            securableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)objectPermissionData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ownershipData).BeginInit();
            principalsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)principalSplit).BeginInit();
            principalSplit.Panel1.SuspendLayout();
            principalSplit.Panel2.SuspendLayout();
            principalSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)principalData).BeginInit();
            authorizationTab.SuspendLayout();
            rolesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)roleSplit).BeginInit();
            roleSplit.Panel1.SuspendLayout();
            roleSplit.Panel2.SuspendLayout();
            roleSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)roleData).BeginInit();
            securableTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)securableSplit).BeginInit();
            securableSplit.Panel1.SuspendLayout();
            securableSplit.Panel2.SuspendLayout();
            securableSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)securableData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingPrincipal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingRole).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSecurable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingMember).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingOwner).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingPermission).BeginInit();
            SuspendLayout();
            // 
            // principalLayout
            // 
            principalLayout.ColumnCount = 1;
            principalLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            principalLayout.Controls.Add(principalLoginData, 0, 0);
            principalLayout.Controls.Add(principalNameData, 0, 1);
            principalLayout.Controls.Add(principalAnnotationData, 0, 2);
            principalLayout.Controls.Add(membershipData, 0, 3);
            principalLayout.Dock = DockStyle.Fill;
            principalLayout.Location = new Point(0, 0);
            principalLayout.Name = "principalLayout";
            principalLayout.RowCount = 4;
            principalLayout.RowStyles.Add(new RowStyle());
            principalLayout.RowStyles.Add(new RowStyle());
            principalLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            principalLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            principalLayout.Size = new Size(350, 435);
            principalLayout.TabIndex = 0;
            // 
            // principalLoginData
            // 
            principalLoginData.AutoSize = true;
            principalLoginData.Dock = DockStyle.Fill;
            principalLoginData.HeaderText = "Principal Login";
            principalLoginData.Location = new Point(3, 3);
            principalLoginData.Multiline = false;
            principalLoginData.Name = "principalLoginData";
            principalLoginData.ReadOnly = false;
            principalLoginData.Size = new Size(344, 44);
            principalLoginData.TabIndex = 0;
            principalLoginData.WordWrap = true;
            // 
            // principalNameData
            // 
            principalNameData.AutoSize = true;
            principalNameData.Dock = DockStyle.Fill;
            principalNameData.HeaderText = "Principal Name";
            principalNameData.Location = new Point(3, 53);
            principalNameData.Multiline = false;
            principalNameData.Name = "principalNameData";
            principalNameData.ReadOnly = false;
            principalNameData.Size = new Size(344, 44);
            principalNameData.TabIndex = 1;
            principalNameData.WordWrap = true;
            // 
            // principalAnnotationData
            // 
            principalAnnotationData.AutoSize = true;
            principalAnnotationData.Dock = DockStyle.Fill;
            principalAnnotationData.HeaderText = "Principal Annotation";
            principalAnnotationData.Location = new Point(3, 103);
            principalAnnotationData.Multiline = true;
            principalAnnotationData.Name = "principalAnnotationData";
            principalAnnotationData.ReadOnly = false;
            principalAnnotationData.Size = new Size(344, 105);
            principalAnnotationData.TabIndex = 2;
            principalAnnotationData.WordWrap = true;
            // 
            // membershipData
            // 
            membershipData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            membershipData.Columns.AddRange(new DataGridViewColumn[] { roleIdColumn });
            membershipData.Dock = DockStyle.Fill;
            membershipData.Location = new Point(3, 214);
            membershipData.Name = "membershipData";
            membershipData.Size = new Size(344, 218);
            membershipData.TabIndex = 3;
            // 
            // roleIdColumn
            // 
            roleIdColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            roleIdColumn.DataPropertyName = "RoleId";
            roleIdColumn.HeaderText = "Role Name (member of)";
            roleIdColumn.Name = "roleIdColumn";
            // 
            // roleLayout
            // 
            roleLayout.AutoSize = true;
            roleLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            roleLayout.ColumnCount = 2;
            roleLayout.ColumnStyles.Add(new ColumnStyle());
            roleLayout.ColumnStyles.Add(new ColumnStyle());
            roleLayout.Controls.Add(roleNameData, 0, 0);
            roleLayout.Controls.Add(roleDescriptionData, 0, 1);
            roleLayout.Controls.Add(isSecurityAdminData, 0, 2);
            roleLayout.Controls.Add(isHelpAdminData, 0, 3);
            roleLayout.Controls.Add(isHelpOwnerData, 1, 3);
            roleLayout.Controls.Add(isCatalogAdminData, 0, 4);
            roleLayout.Controls.Add(isCatalogOwnerData, 1, 4);
            roleLayout.Controls.Add(isLibraryAdminData, 0, 5);
            roleLayout.Controls.Add(isLibraryOwnerData, 1, 5);
            roleLayout.Controls.Add(isModelAdminData, 0, 6);
            roleLayout.Controls.Add(isModelOwnerData, 1, 6);
            roleLayout.Controls.Add(isScriptAdminData, 0, 7);
            roleLayout.Controls.Add(isScriptOwnerData, 1, 7);
            roleLayout.Dock = DockStyle.Fill;
            roleLayout.Location = new Point(0, 0);
            roleLayout.Name = "roleLayout";
            roleLayout.RowCount = 8;
            roleLayout.RowStyles.Add(new RowStyle());
            roleLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            roleLayout.RowStyles.Add(new RowStyle());
            roleLayout.RowStyles.Add(new RowStyle());
            roleLayout.RowStyles.Add(new RowStyle());
            roleLayout.RowStyles.Add(new RowStyle());
            roleLayout.RowStyles.Add(new RowStyle());
            roleLayout.RowStyles.Add(new RowStyle());
            roleLayout.Size = new Size(105, 66);
            roleLayout.TabIndex = 0;
            // 
            // roleNameData
            // 
            roleNameData.AutoSize = true;
            roleNameData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            roleLayout.SetColumnSpan(roleNameData, 2);
            roleNameData.Dock = DockStyle.Fill;
            roleNameData.HeaderText = "Role Name";
            roleNameData.Location = new Point(3, 3);
            roleNameData.Multiline = false;
            roleNameData.Name = "roleNameData";
            roleNameData.ReadOnly = false;
            roleNameData.Size = new Size(218, 44);
            roleNameData.TabIndex = 0;
            roleNameData.WordWrap = true;
            // 
            // roleDescriptionData
            // 
            roleDescriptionData.AutoSize = true;
            roleDescriptionData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            roleLayout.SetColumnSpan(roleDescriptionData, 2);
            roleDescriptionData.Dock = DockStyle.Fill;
            roleDescriptionData.HeaderText = "Role Description";
            roleDescriptionData.Location = new Point(3, 53);
            roleDescriptionData.Multiline = true;
            roleDescriptionData.Name = "roleDescriptionData";
            roleDescriptionData.ReadOnly = false;
            roleDescriptionData.Size = new Size(218, 1);
            roleDescriptionData.TabIndex = 1;
            roleDescriptionData.WordWrap = true;
            // 
            // isSecurityAdminData
            // 
            isSecurityAdminData.AutoSize = true;
            isSecurityAdminData.Location = new Point(3, -81);
            isSecurityAdminData.Name = "isSecurityAdminData";
            isSecurityAdminData.Size = new Size(107, 19);
            isSecurityAdminData.TabIndex = 2;
            isSecurityAdminData.Text = "Security Admin";
            isSecurityAdminData.UseVisualStyleBackColor = true;
            // 
            // isHelpAdminData
            // 
            isHelpAdminData.AutoSize = true;
            isHelpAdminData.Location = new Point(3, -56);
            isHelpAdminData.Name = "isHelpAdminData";
            isHelpAdminData.Size = new Size(90, 19);
            isHelpAdminData.TabIndex = 3;
            isHelpAdminData.Text = "Help Admin";
            isHelpAdminData.UseVisualStyleBackColor = true;
            // 
            // isHelpOwnerData
            // 
            isHelpOwnerData.AutoSize = true;
            isHelpOwnerData.Location = new Point(116, -56);
            isHelpOwnerData.Name = "isHelpOwnerData";
            isHelpOwnerData.Size = new Size(89, 19);
            isHelpOwnerData.TabIndex = 4;
            isHelpOwnerData.Text = "Help Owner";
            isHelpOwnerData.UseVisualStyleBackColor = true;
            // 
            // isCatalogAdminData
            // 
            isCatalogAdminData.AutoSize = true;
            isCatalogAdminData.Location = new Point(3, -31);
            isCatalogAdminData.Name = "isCatalogAdminData";
            isCatalogAdminData.Size = new Size(106, 19);
            isCatalogAdminData.TabIndex = 5;
            isCatalogAdminData.Text = "Catalog Admin";
            isCatalogAdminData.UseVisualStyleBackColor = true;
            // 
            // isCatalogOwnerData
            // 
            isCatalogOwnerData.AutoSize = true;
            isCatalogOwnerData.Location = new Point(116, -31);
            isCatalogOwnerData.Name = "isCatalogOwnerData";
            isCatalogOwnerData.Size = new Size(105, 19);
            isCatalogOwnerData.TabIndex = 6;
            isCatalogOwnerData.Text = "Catalog Owner";
            isCatalogOwnerData.UseVisualStyleBackColor = true;
            // 
            // isLibraryAdminData
            // 
            isLibraryAdminData.AutoSize = true;
            isLibraryAdminData.Location = new Point(3, -6);
            isLibraryAdminData.Name = "isLibraryAdminData";
            isLibraryAdminData.Size = new Size(101, 19);
            isLibraryAdminData.TabIndex = 7;
            isLibraryAdminData.Text = "Library Admin";
            isLibraryAdminData.UseVisualStyleBackColor = true;
            // 
            // isLibraryOwnerData
            // 
            isLibraryOwnerData.AutoSize = true;
            isLibraryOwnerData.Location = new Point(116, -6);
            isLibraryOwnerData.Name = "isLibraryOwnerData";
            isLibraryOwnerData.Size = new Size(100, 19);
            isLibraryOwnerData.TabIndex = 8;
            isLibraryOwnerData.Text = "Library Owner";
            isLibraryOwnerData.UseVisualStyleBackColor = true;
            // 
            // isModelAdminData
            // 
            isModelAdminData.AutoSize = true;
            isModelAdminData.Location = new Point(3, 19);
            isModelAdminData.Name = "isModelAdminData";
            isModelAdminData.Size = new Size(99, 19);
            isModelAdminData.TabIndex = 9;
            isModelAdminData.Text = "Model Admin";
            isModelAdminData.UseVisualStyleBackColor = true;
            // 
            // isModelOwnerData
            // 
            isModelOwnerData.AutoSize = true;
            isModelOwnerData.Location = new Point(116, 19);
            isModelOwnerData.Name = "isModelOwnerData";
            isModelOwnerData.Size = new Size(98, 19);
            isModelOwnerData.TabIndex = 10;
            isModelOwnerData.Text = "Model Owner";
            isModelOwnerData.UseVisualStyleBackColor = true;
            // 
            // isScriptAdminData
            // 
            isScriptAdminData.AutoSize = true;
            isScriptAdminData.Location = new Point(3, 44);
            isScriptAdminData.Name = "isScriptAdminData";
            isScriptAdminData.Size = new Size(95, 19);
            isScriptAdminData.TabIndex = 11;
            isScriptAdminData.Text = "Script Admin";
            isScriptAdminData.UseVisualStyleBackColor = true;
            // 
            // isScriptOwnerData
            // 
            isScriptOwnerData.AutoSize = true;
            isScriptOwnerData.Location = new Point(116, 44);
            isScriptOwnerData.Name = "isScriptOwnerData";
            isScriptOwnerData.Size = new Size(94, 19);
            isScriptOwnerData.TabIndex = 12;
            isScriptOwnerData.Text = "Script Owner";
            isScriptOwnerData.UseVisualStyleBackColor = true;
            // 
            // securableLayout
            // 
            securableLayout.ColumnCount = 1;
            securableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            securableLayout.Controls.Add(objectPermissionData, 0, 2);
            securableLayout.Controls.Add(ownershipData, 0, 1);
            securableLayout.Controls.Add(securableTitleData, 0, 0);
            securableLayout.Dock = DockStyle.Fill;
            securableLayout.Location = new Point(0, 0);
            securableLayout.Name = "securableLayout";
            securableLayout.RowCount = 3;
            securableLayout.RowStyles.Add(new RowStyle());
            securableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            securableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            securableLayout.Size = new Size(105, 66);
            securableLayout.TabIndex = 0;
            // 
            // objectPermissionData
            // 
            objectPermissionData.AllowUserToAddRows = false;
            objectPermissionData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            objectPermissionData.Columns.AddRange(new DataGridViewColumn[] { permissionRoleColumn, isGrantColumn, isDenyColumn });
            securableLayout.SetColumnSpan(objectPermissionData, 2);
            objectPermissionData.Dock = DockStyle.Fill;
            objectPermissionData.Location = new Point(3, 61);
            objectPermissionData.Name = "objectPermissionData";
            objectPermissionData.Size = new Size(99, 2);
            objectPermissionData.TabIndex = 15;
            // 
            // permissionRoleColumn
            // 
            permissionRoleColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            permissionRoleColumn.HeaderText = "Role Permission";
            permissionRoleColumn.Name = "permissionRoleColumn";
            // 
            // isGrantColumn
            // 
            isGrantColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            isGrantColumn.DataPropertyName = "IsGrant";
            isGrantColumn.FillWeight = 30F;
            isGrantColumn.HeaderText = "is Grant";
            isGrantColumn.Name = "isGrantColumn";
            // 
            // isDenyColumn
            // 
            isDenyColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            isDenyColumn.DataPropertyName = "IsDeny";
            isDenyColumn.FillWeight = 30F;
            isDenyColumn.HeaderText = "is Deny";
            isDenyColumn.Name = "isDenyColumn";
            // 
            // ownershipData
            // 
            ownershipData.AllowUserToAddRows = false;
            ownershipData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ownershipData.Columns.AddRange(new DataGridViewColumn[] { principlaNameColumn });
            ownershipData.Dock = DockStyle.Fill;
            ownershipData.Location = new Point(3, 53);
            ownershipData.Name = "ownershipData";
            ownershipData.ReadOnly = true;
            ownershipData.Size = new Size(99, 2);
            ownershipData.TabIndex = 5;
            // 
            // principlaNameColumn
            // 
            principlaNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            principlaNameColumn.HeaderText = "Owned by Principal";
            principlaNameColumn.Name = "principlaNameColumn";
            principlaNameColumn.ReadOnly = true;
            // 
            // securableTitleData
            // 
            securableTitleData.AutoSize = true;
            securableTitleData.Dock = DockStyle.Fill;
            securableTitleData.HeaderText = "Object";
            securableTitleData.Location = new Point(3, 3);
            securableTitleData.Multiline = false;
            securableTitleData.Name = "securableTitleData";
            securableTitleData.ReadOnly = false;
            securableTitleData.Size = new Size(99, 44);
            securableTitleData.TabIndex = 0;
            securableTitleData.WordWrap = true;
            // 
            // principalsTab
            // 
            principalsTab.BackColor = SystemColors.Control;
            principalsTab.Controls.Add(principalSplit);
            principalsTab.Location = new Point(4, 24);
            principalsTab.Name = "principalsTab";
            principalsTab.Padding = new Padding(3);
            principalsTab.Size = new Size(613, 441);
            principalsTab.TabIndex = 0;
            principalsTab.Text = "Principals";
            // 
            // principalSplit
            // 
            principalSplit.Dock = DockStyle.Fill;
            principalSplit.Location = new Point(3, 3);
            principalSplit.Name = "principalSplit";
            // 
            // principalSplit.Panel1
            // 
            principalSplit.Panel1.Controls.Add(principalData);
            // 
            // principalSplit.Panel2
            // 
            principalSplit.Panel2.Controls.Add(principalLayout);
            principalSplit.Size = new Size(607, 435);
            principalSplit.SplitterDistance = 253;
            principalSplit.TabIndex = 5;
            // 
            // principalData
            // 
            principalData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            principalData.Columns.AddRange(new DataGridViewColumn[] { principalNameColumn });
            principalData.Dock = DockStyle.Fill;
            principalData.Location = new Point(0, 0);
            principalData.Name = "principalData";
            principalData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            principalData.Size = new Size(253, 435);
            principalData.TabIndex = 0;
            // 
            // principalNameColumn
            // 
            principalNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            principalNameColumn.DataPropertyName = "PrincipalName";
            principalNameColumn.HeaderText = "Principal";
            principalNameColumn.Name = "principalNameColumn";
            // 
            // authorizationTab
            // 
            authorizationTab.Controls.Add(principalsTab);
            authorizationTab.Controls.Add(rolesTab);
            authorizationTab.Controls.Add(securableTab);
            authorizationTab.Dock = DockStyle.Fill;
            authorizationTab.Location = new Point(0, 25);
            authorizationTab.Name = "authorizationTab";
            authorizationTab.SelectedIndex = 0;
            authorizationTab.Size = new Size(621, 469);
            authorizationTab.TabIndex = 5;
            // 
            // rolesTab
            // 
            rolesTab.BackColor = SystemColors.Control;
            rolesTab.Controls.Add(roleSplit);
            rolesTab.Location = new Point(4, 24);
            rolesTab.Name = "rolesTab";
            rolesTab.Padding = new Padding(3);
            rolesTab.Size = new Size(192, 72);
            rolesTab.TabIndex = 1;
            rolesTab.Text = "Roles";
            // 
            // roleSplit
            // 
            roleSplit.Dock = DockStyle.Fill;
            roleSplit.Location = new Point(3, 3);
            roleSplit.Name = "roleSplit";
            // 
            // roleSplit.Panel1
            // 
            roleSplit.Panel1.Controls.Add(roleData);
            // 
            // roleSplit.Panel2
            // 
            roleSplit.Panel2.Controls.Add(roleLayout);
            roleSplit.Size = new Size(186, 66);
            roleSplit.SplitterDistance = 77;
            roleSplit.TabIndex = 5;
            // 
            // roleData
            // 
            roleData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            roleData.Columns.AddRange(new DataGridViewColumn[] { roleNameColumn });
            roleData.Dock = DockStyle.Fill;
            roleData.Location = new Point(0, 0);
            roleData.Name = "roleData";
            roleData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            roleData.Size = new Size(77, 66);
            roleData.TabIndex = 0;
            // 
            // roleNameColumn
            // 
            roleNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            roleNameColumn.DataPropertyName = "RoleName";
            roleNameColumn.HeaderText = "Role Name";
            roleNameColumn.Name = "roleNameColumn";
            // 
            // securableTab
            // 
            securableTab.BackColor = SystemColors.Control;
            securableTab.Controls.Add(securableSplit);
            securableTab.Location = new Point(4, 24);
            securableTab.Name = "securableTab";
            securableTab.Padding = new Padding(3);
            securableTab.Size = new Size(192, 72);
            securableTab.TabIndex = 2;
            securableTab.Text = "Securables";
            // 
            // securableSplit
            // 
            securableSplit.Dock = DockStyle.Fill;
            securableSplit.Location = new Point(3, 3);
            securableSplit.Name = "securableSplit";
            // 
            // securableSplit.Panel1
            // 
            securableSplit.Panel1.Controls.Add(securableData);
            // 
            // securableSplit.Panel2
            // 
            securableSplit.Panel2.Controls.Add(securableLayout);
            securableSplit.Size = new Size(186, 66);
            securableSplit.SplitterDistance = 77;
            securableSplit.TabIndex = 0;
            // 
            // securableData
            // 
            securableData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            securableData.Columns.AddRange(new DataGridViewColumn[] { securableNameColumn });
            securableData.Dock = DockStyle.Fill;
            securableData.Location = new Point(0, 0);
            securableData.Name = "securableData";
            securableData.Size = new Size(77, 66);
            securableData.TabIndex = 0;
            // 
            // securableNameColumn
            // 
            securableNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            securableNameColumn.HeaderText = "Securable";
            securableNameColumn.Name = "securableNameColumn";
            // 
            // Authorization
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(621, 494);
            Controls.Add(authorizationTab);
            Name = "Authorization";
            Text = "Authorization Manager";
            Load += Authorization_Load;
            Controls.SetChildIndex(authorizationTab, 0);
            principalLayout.ResumeLayout(false);
            principalLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)membershipData).EndInit();
            roleLayout.ResumeLayout(false);
            roleLayout.PerformLayout();
            securableLayout.ResumeLayout(false);
            securableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)objectPermissionData).EndInit();
            ((System.ComponentModel.ISupportInitialize)ownershipData).EndInit();
            principalsTab.ResumeLayout(false);
            principalSplit.Panel1.ResumeLayout(false);
            principalSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)principalSplit).EndInit();
            principalSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)principalData).EndInit();
            authorizationTab.ResumeLayout(false);
            rolesTab.ResumeLayout(false);
            roleSplit.Panel1.ResumeLayout(false);
            roleSplit.Panel2.ResumeLayout(false);
            roleSplit.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)roleSplit).EndInit();
            roleSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)roleData).EndInit();
            securableTab.ResumeLayout(false);
            securableSplit.Panel1.ResumeLayout(false);
            securableSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)securableSplit).EndInit();
            securableSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)securableData).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingPrincipal).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingRole).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSecurable).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingMember).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingOwner).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingPermission).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TabPage rolesTab;
        private SplitContainer roleSplit;
        private DataGridView roleData;
        private DataGridViewTextBoxColumn roleNameColumn;
        private Controls.TextBoxData roleNameData;
        private Controls.TextBoxData roleDescriptionData;
        private CheckBox isSecurityAdminData;
        private CheckBox isHelpAdminData;
        private CheckBox isHelpOwnerData;
        private CheckBox isCatalogAdminData;
        private CheckBox isCatalogOwnerData;
        private CheckBox isLibraryAdminData;
        private CheckBox isLibraryOwnerData;
        private CheckBox isModelAdminData;
        private CheckBox isModelOwnerData;
        private CheckBox isScriptAdminData;
        private CheckBox isScriptOwnerData;
        private DataGridView membershipData;
        private DataGridViewComboBoxColumn principalIdColumn;
        private SplitContainer principalSplit;
        private DataGridView principalData;
        private DataGridViewTextBoxColumn principalNameColumn;
        private Controls.TextBoxData principalLoginData;
        private Controls.TextBoxData principalNameData;
        private Controls.TextBoxData principalAnnotationData;
        private DataGridViewComboBoxColumn roleIdColumn;
        private TabPage securableTab;
        private SplitContainer securableSplit;
        private DataGridView securableData;
        private DataGridViewTextBoxColumn securableNameColumn;
        private TableLayoutPanel securableLayout;
        private DataGridView objectPermissionData;
        private DataGridView ownershipData;
        private Controls.TextBoxData securableTitleData;
        private DataGridViewTextBoxColumn permissionRoleColumn;
        private DataGridViewCheckBoxColumn isGrantColumn;
        private DataGridViewCheckBoxColumn isDenyColumn;
        private DataGridViewTextBoxColumn principlaNameColumn;
        private BindingSource bindingPrincipal;
        private BindingSource bindingRole;
        private BindingSource bindingSecurable;
        private BindingSource bindingMember;
        private BindingSource bindingOwner;
        private BindingSource bindingPermission;
        private TabControl authorizationTab;
        private TabPage principalsTab;
    }
}