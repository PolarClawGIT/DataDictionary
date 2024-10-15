namespace DataDictionary.Main.Forms.Security
{
    partial class RoleManager
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
            TableLayoutPanel roleLayout;
            roleNameData = new Controls.TextBoxData();
            roleDescriptionData = new Controls.TextBoxData();
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
            principalMemeberData = new DataGridView();
            principalIdColumn = new DataGridViewComboBoxColumn();
            roleSplit = new SplitContainer();
            roleList = new ListView();
            bindingRoles = new BindingSource(components);
            bindingPrincipals = new BindingSource(components);
            roleLayout = new TableLayoutPanel();
            roleLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)principalMemeberData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)roleSplit).BeginInit();
            roleSplit.Panel1.SuspendLayout();
            roleSplit.Panel2.SuspendLayout();
            roleSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingRoles).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingPrincipals).BeginInit();
            SuspendLayout();
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
            roleLayout.Controls.Add(principalMemeberData, 0, 8);
            roleLayout.Dock = DockStyle.Fill;
            roleLayout.Location = new Point(0, 0);
            roleLayout.Name = "roleLayout";
            roleLayout.RowCount = 9;
            roleLayout.RowStyles.Add(new RowStyle());
            roleLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            roleLayout.RowStyles.Add(new RowStyle());
            roleLayout.RowStyles.Add(new RowStyle());
            roleLayout.RowStyles.Add(new RowStyle());
            roleLayout.RowStyles.Add(new RowStyle());
            roleLayout.RowStyles.Add(new RowStyle());
            roleLayout.RowStyles.Add(new RowStyle());
            roleLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            roleLayout.Size = new Size(332, 447);
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
            roleNameData.Size = new Size(326, 44);
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
            roleDescriptionData.Size = new Size(326, 92);
            roleDescriptionData.TabIndex = 1;
            roleDescriptionData.WordWrap = true;
            // 
            // isSecurityAdminData
            // 
            isSecurityAdminData.AutoSize = true;
            isSecurityAdminData.Location = new Point(3, 151);
            isSecurityAdminData.Name = "isSecurityAdminData";
            isSecurityAdminData.Size = new Size(107, 19);
            isSecurityAdminData.TabIndex = 2;
            isSecurityAdminData.Text = "Security Admin";
            isSecurityAdminData.UseVisualStyleBackColor = true;
            // 
            // isHelpAdminData
            // 
            isHelpAdminData.AutoSize = true;
            isHelpAdminData.Location = new Point(3, 176);
            isHelpAdminData.Name = "isHelpAdminData";
            isHelpAdminData.Size = new Size(90, 19);
            isHelpAdminData.TabIndex = 3;
            isHelpAdminData.Text = "Help Admin";
            isHelpAdminData.UseVisualStyleBackColor = true;
            // 
            // isHelpOwnerData
            // 
            isHelpOwnerData.AutoSize = true;
            isHelpOwnerData.Location = new Point(116, 176);
            isHelpOwnerData.Name = "isHelpOwnerData";
            isHelpOwnerData.Size = new Size(89, 19);
            isHelpOwnerData.TabIndex = 4;
            isHelpOwnerData.Text = "Help Owner";
            isHelpOwnerData.UseVisualStyleBackColor = true;
            // 
            // isCatalogAdminData
            // 
            isCatalogAdminData.AutoSize = true;
            isCatalogAdminData.Location = new Point(3, 201);
            isCatalogAdminData.Name = "isCatalogAdminData";
            isCatalogAdminData.Size = new Size(106, 19);
            isCatalogAdminData.TabIndex = 5;
            isCatalogAdminData.Text = "Catalog Admin";
            isCatalogAdminData.UseVisualStyleBackColor = true;
            // 
            // isCatalogOwnerData
            // 
            isCatalogOwnerData.AutoSize = true;
            isCatalogOwnerData.Location = new Point(116, 201);
            isCatalogOwnerData.Name = "isCatalogOwnerData";
            isCatalogOwnerData.Size = new Size(105, 19);
            isCatalogOwnerData.TabIndex = 6;
            isCatalogOwnerData.Text = "Catalog Owner";
            isCatalogOwnerData.UseVisualStyleBackColor = true;
            // 
            // isLibraryAdminData
            // 
            isLibraryAdminData.AutoSize = true;
            isLibraryAdminData.Location = new Point(3, 226);
            isLibraryAdminData.Name = "isLibraryAdminData";
            isLibraryAdminData.Size = new Size(101, 19);
            isLibraryAdminData.TabIndex = 7;
            isLibraryAdminData.Text = "Library Admin";
            isLibraryAdminData.UseVisualStyleBackColor = true;
            // 
            // isLibraryOwnerData
            // 
            isLibraryOwnerData.AutoSize = true;
            isLibraryOwnerData.Location = new Point(116, 226);
            isLibraryOwnerData.Name = "isLibraryOwnerData";
            isLibraryOwnerData.Size = new Size(100, 19);
            isLibraryOwnerData.TabIndex = 8;
            isLibraryOwnerData.Text = "Library Owner";
            isLibraryOwnerData.UseVisualStyleBackColor = true;
            // 
            // isModelAdminData
            // 
            isModelAdminData.AutoSize = true;
            isModelAdminData.Location = new Point(3, 251);
            isModelAdminData.Name = "isModelAdminData";
            isModelAdminData.Size = new Size(99, 19);
            isModelAdminData.TabIndex = 9;
            isModelAdminData.Text = "Model Admin";
            isModelAdminData.UseVisualStyleBackColor = true;
            // 
            // isModelOwnerData
            // 
            isModelOwnerData.AutoSize = true;
            isModelOwnerData.Location = new Point(116, 251);
            isModelOwnerData.Name = "isModelOwnerData";
            isModelOwnerData.Size = new Size(98, 19);
            isModelOwnerData.TabIndex = 10;
            isModelOwnerData.Text = "Model Owner";
            isModelOwnerData.UseVisualStyleBackColor = true;
            // 
            // isScriptAdminData
            // 
            isScriptAdminData.AutoSize = true;
            isScriptAdminData.Location = new Point(3, 276);
            isScriptAdminData.Name = "isScriptAdminData";
            isScriptAdminData.Size = new Size(95, 19);
            isScriptAdminData.TabIndex = 11;
            isScriptAdminData.Text = "Script Admin";
            isScriptAdminData.UseVisualStyleBackColor = true;
            // 
            // isScriptOwnerData
            // 
            isScriptOwnerData.AutoSize = true;
            isScriptOwnerData.Location = new Point(116, 276);
            isScriptOwnerData.Name = "isScriptOwnerData";
            isScriptOwnerData.Size = new Size(94, 19);
            isScriptOwnerData.TabIndex = 12;
            isScriptOwnerData.Text = "Script Owner";
            isScriptOwnerData.UseVisualStyleBackColor = true;
            // 
            // principalMemeberData
            // 
            principalMemeberData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            principalMemeberData.Columns.AddRange(new DataGridViewColumn[] { principalIdColumn });
            roleLayout.SetColumnSpan(principalMemeberData, 2);
            principalMemeberData.Dock = DockStyle.Fill;
            principalMemeberData.Location = new Point(3, 301);
            principalMemeberData.Name = "principalMemeberData";
            principalMemeberData.Size = new Size(326, 143);
            principalMemeberData.TabIndex = 13;
            // 
            // principalIdColumn
            // 
            principalIdColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            principalIdColumn.HeaderText = "Principal Name";
            principalIdColumn.Name = "principalIdColumn";
            // 
            // roleSplit
            // 
            roleSplit.Dock = DockStyle.Fill;
            roleSplit.Location = new Point(0, 25);
            roleSplit.Name = "roleSplit";
            // 
            // roleSplit.Panel1
            // 
            roleSplit.Panel1.Controls.Add(roleList);
            // 
            // roleSplit.Panel2
            // 
            roleSplit.Panel2.Controls.Add(roleLayout);
            roleSplit.Size = new Size(503, 447);
            roleSplit.SplitterDistance = 167;
            roleSplit.TabIndex = 4;
            // 
            // roleList
            // 
            roleList.Dock = DockStyle.Fill;
            roleList.Location = new Point(0, 0);
            roleList.Name = "roleList";
            roleList.Size = new Size(167, 447);
            roleList.TabIndex = 0;
            roleList.UseCompatibleStateImageBehavior = false;
            // 
            // RoleManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(503, 472);
            Controls.Add(roleSplit);
            Name = "RoleManager";
            Text = "RoleManager";
            Controls.SetChildIndex(roleSplit, 0);
            roleLayout.ResumeLayout(false);
            roleLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)principalMemeberData).EndInit();
            roleSplit.Panel1.ResumeLayout(false);
            roleSplit.Panel2.ResumeLayout(false);
            roleSplit.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)roleSplit).EndInit();
            roleSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)bindingRoles).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingPrincipals).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private SplitContainer roleSplit;
        private ListView roleList;
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
        private DataGridView principalMemeberData;
        private DataGridViewComboBoxColumn principalIdColumn;
        private BindingSource bindingRoles;
        private BindingSource bindingPrincipals;
    }
}