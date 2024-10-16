namespace DataDictionary.Main.Forms.Security
{
    partial class PrincipalManager
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
            principleLoginData = new Controls.TextBoxData();
            principleNameData = new Controls.TextBoxData();
            principleAnnotationData = new Controls.TextBoxData();
            roleMembershipData = new DataGridView();
            principalSplit = new SplitContainer();
            principleList = new ListView();
            principalColumn = new ColumnHeader();
            bindingPrinciple = new BindingSource(components);
            bindingMembers = new BindingSource(components);
            principalOwnershipData = new DataGridView();
            bindingOwnership = new BindingSource(components);
            ownershipObjectColumn = new DataGridViewTextBoxColumn();
            roleIdColumn = new DataGridViewComboBoxColumn();
            principalLayout = new TableLayoutPanel();
            principalLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)roleMembershipData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)principalSplit).BeginInit();
            principalSplit.Panel1.SuspendLayout();
            principalSplit.Panel2.SuspendLayout();
            principalSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingPrinciple).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingMembers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)principalOwnershipData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingOwnership).BeginInit();
            SuspendLayout();
            // 
            // principalLayout
            // 
            principalLayout.ColumnCount = 1;
            principalLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            principalLayout.Controls.Add(principleLoginData, 0, 0);
            principalLayout.Controls.Add(principleNameData, 0, 1);
            principalLayout.Controls.Add(principleAnnotationData, 0, 2);
            principalLayout.Controls.Add(roleMembershipData, 0, 3);
            principalLayout.Controls.Add(principalOwnershipData, 0, 4);
            principalLayout.Dock = DockStyle.Fill;
            principalLayout.Location = new Point(0, 0);
            principalLayout.Name = "principalLayout";
            principalLayout.RowCount = 5;
            principalLayout.RowStyles.Add(new RowStyle());
            principalLayout.RowStyles.Add(new RowStyle());
            principalLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            principalLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            principalLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            principalLayout.Size = new Size(381, 494);
            principalLayout.TabIndex = 0;
            // 
            // principleLoginData
            // 
            principleLoginData.AutoSize = true;
            principleLoginData.Dock = DockStyle.Fill;
            principleLoginData.HeaderText = "Principle Login";
            principleLoginData.Location = new Point(3, 3);
            principleLoginData.Multiline = false;
            principleLoginData.Name = "principleLoginData";
            principleLoginData.ReadOnly = false;
            principleLoginData.Size = new Size(375, 44);
            principleLoginData.TabIndex = 0;
            principleLoginData.WordWrap = true;
            // 
            // principleNameData
            // 
            principleNameData.AutoSize = true;
            principleNameData.Dock = DockStyle.Fill;
            principleNameData.HeaderText = "Principle Name";
            principleNameData.Location = new Point(3, 53);
            principleNameData.Multiline = false;
            principleNameData.Name = "principleNameData";
            principleNameData.ReadOnly = false;
            principleNameData.Size = new Size(375, 44);
            principleNameData.TabIndex = 1;
            principleNameData.WordWrap = true;
            // 
            // principleAnnotationData
            // 
            principleAnnotationData.AutoSize = true;
            principleAnnotationData.Dock = DockStyle.Fill;
            principleAnnotationData.HeaderText = "Principle Annotation";
            principleAnnotationData.Location = new Point(3, 103);
            principleAnnotationData.Multiline = true;
            principleAnnotationData.Name = "principleAnnotationData";
            principleAnnotationData.ReadOnly = false;
            principleAnnotationData.Size = new Size(375, 72);
            principleAnnotationData.TabIndex = 2;
            principleAnnotationData.WordWrap = true;
            // 
            // roleMembershipData
            // 
            roleMembershipData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            roleMembershipData.Columns.AddRange(new DataGridViewColumn[] { roleIdColumn });
            roleMembershipData.Dock = DockStyle.Fill;
            roleMembershipData.Location = new Point(3, 181);
            roleMembershipData.Name = "roleMembershipData";
            roleMembershipData.ReadOnly = true;
            roleMembershipData.Size = new Size(375, 151);
            roleMembershipData.TabIndex = 3;
            // 
            // principalSplit
            // 
            principalSplit.Dock = DockStyle.Fill;
            principalSplit.Location = new Point(0, 25);
            principalSplit.Name = "principalSplit";
            // 
            // principalSplit.Panel1
            // 
            principalSplit.Panel1.Controls.Add(principleList);
            // 
            // principalSplit.Panel2
            // 
            principalSplit.Panel2.Controls.Add(principalLayout);
            principalSplit.Size = new Size(576, 494);
            principalSplit.SplitterDistance = 191;
            principalSplit.TabIndex = 4;
            // 
            // principleList
            // 
            principleList.Columns.AddRange(new ColumnHeader[] { principalColumn });
            principleList.Dock = DockStyle.Fill;
            principleList.Location = new Point(0, 0);
            principleList.Name = "principleList";
            principleList.Size = new Size(191, 494);
            principleList.TabIndex = 0;
            principleList.UseCompatibleStateImageBehavior = false;
            principleList.View = View.Details;
            principleList.SelectedIndexChanged += PrincipleList_SelectedIndexChanged;
            principleList.Resize += PrincipleList_Resize;
            // 
            // principalColumn
            // 
            principalColumn.Text = "Principal";
            // 
            // bindingPrinciple
            // 
            bindingPrinciple.CurrentChanged += BindingPrinciple_CurrentChanged;
            // 
            // bindingMembers
            // 
            bindingMembers.AddingNew += BindingMembers_AddingNew;
            // 
            // principalOwnershipData
            // 
            principalOwnershipData.AllowUserToAddRows = false;
            principalOwnershipData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            principalOwnershipData.Columns.AddRange(new DataGridViewColumn[] { ownershipObjectColumn });
            principalOwnershipData.Dock = DockStyle.Fill;
            principalOwnershipData.Location = new Point(3, 338);
            principalOwnershipData.Name = "principalOwnershipData";
            principalOwnershipData.ReadOnly = true;
            principalOwnershipData.Size = new Size(375, 153);
            principalOwnershipData.TabIndex = 4;
            // 
            // ownershipObjectColumn
            // 
            ownershipObjectColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ownershipObjectColumn.HeaderText = "Object Name (owned by Princpal)";
            ownershipObjectColumn.Name = "ownershipObjectColumn";
            ownershipObjectColumn.ReadOnly = true;
            // 
            // roleIdColumn
            // 
            roleIdColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            roleIdColumn.DataPropertyName = "RoleId";
            roleIdColumn.HeaderText = "Role Name (member of)";
            roleIdColumn.Name = "roleIdColumn";
            roleIdColumn.ReadOnly = true;
            // 
            // PrincipalManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(576, 519);
            Controls.Add(principalSplit);
            Name = "PrincipalManager";
            Text = "Principal";
            Load += PrincipalManager_Load;
            Controls.SetChildIndex(principalSplit, 0);
            principalLayout.ResumeLayout(false);
            principalLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)roleMembershipData).EndInit();
            principalSplit.Panel1.ResumeLayout(false);
            principalSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)principalSplit).EndInit();
            principalSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)bindingPrinciple).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingMembers).EndInit();
            ((System.ComponentModel.ISupportInitialize)principalOwnershipData).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingOwnership).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private SplitContainer principalSplit;
        private ListView principleList;
        private TableLayoutPanel principalLayout;
        private Controls.TextBoxData principleLoginData;
        private Controls.TextBoxData principleNameData;
        private Controls.TextBoxData principleAnnotationData;
        private DataGridView roleMembershipData;
        private BindingSource bindingPrinciple;
        private BindingSource bindingMembers;
        private ColumnHeader principalColumn;
        private DataGridView principalOwnershipData;
        private BindingSource bindingOwnership;
        private DataGridViewComboBoxColumn roleIdColumn;
        private DataGridViewTextBoxColumn ownershipObjectColumn;
    }
}