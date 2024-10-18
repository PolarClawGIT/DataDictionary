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
            principalLoginData = new Controls.TextBoxData();
            principalNameData = new Controls.TextBoxData();
            principalAnnotationData = new Controls.TextBoxData();
            membershipData = new DataGridView();
            roleIdColumn = new DataGridViewComboBoxColumn();
            ownershipData = new DataGridView();
            ownershipObjectColumn = new DataGridViewTextBoxColumn();
            principalSplit = new SplitContainer();
            principalData = new DataGridView();
            principalNameColumn = new DataGridViewTextBoxColumn();
            bindingPrincipal = new BindingSource(components);
            bindingMembers = new BindingSource(components);
            bindingOwnership = new BindingSource(components);
            principalLayout = new TableLayoutPanel();
            principalLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)membershipData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ownershipData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)principalSplit).BeginInit();
            principalSplit.Panel1.SuspendLayout();
            principalSplit.Panel2.SuspendLayout();
            principalSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)principalData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingPrincipal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingMembers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingOwnership).BeginInit();
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
            principalLayout.Controls.Add(ownershipData, 0, 4);
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
            // principalLoginData
            // 
            principalLoginData.AutoSize = true;
            principalLoginData.Dock = DockStyle.Fill;
            principalLoginData.HeaderText = "Principal Login";
            principalLoginData.Location = new Point(3, 3);
            principalLoginData.Multiline = false;
            principalLoginData.Name = "principalLoginData";
            principalLoginData.ReadOnly = false;
            principalLoginData.Size = new Size(375, 44);
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
            principalNameData.Size = new Size(375, 44);
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
            principalAnnotationData.Size = new Size(375, 72);
            principalAnnotationData.TabIndex = 2;
            principalAnnotationData.WordWrap = true;
            // 
            // membershipData
            // 
            membershipData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            membershipData.Columns.AddRange(new DataGridViewColumn[] { roleIdColumn });
            membershipData.Dock = DockStyle.Fill;
            membershipData.Location = new Point(3, 181);
            membershipData.Name = "membershipData";
            membershipData.Size = new Size(375, 151);
            membershipData.TabIndex = 3;
            // 
            // roleIdColumn
            // 
            roleIdColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            roleIdColumn.DataPropertyName = "RoleId";
            roleIdColumn.HeaderText = "Role Name (member of)";
            roleIdColumn.Name = "roleIdColumn";
            // 
            // ownershipData
            // 
            ownershipData.AllowUserToAddRows = false;
            ownershipData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ownershipData.Columns.AddRange(new DataGridViewColumn[] { ownershipObjectColumn });
            ownershipData.Dock = DockStyle.Fill;
            ownershipData.Location = new Point(3, 338);
            ownershipData.Name = "ownershipData";
            ownershipData.ReadOnly = true;
            ownershipData.Size = new Size(375, 153);
            ownershipData.TabIndex = 4;
            // 
            // ownershipObjectColumn
            // 
            ownershipObjectColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ownershipObjectColumn.HeaderText = "Object Name (owned by Principal)";
            ownershipObjectColumn.Name = "ownershipObjectColumn";
            ownershipObjectColumn.ReadOnly = true;
            // 
            // principalSplit
            // 
            principalSplit.Dock = DockStyle.Fill;
            principalSplit.Location = new Point(0, 25);
            principalSplit.Name = "principalSplit";
            // 
            // principalSplit.Panel1
            // 
            principalSplit.Panel1.Controls.Add(principalData);
            // 
            // principalSplit.Panel2
            // 
            principalSplit.Panel2.Controls.Add(principalLayout);
            principalSplit.Size = new Size(576, 494);
            principalSplit.SplitterDistance = 191;
            principalSplit.TabIndex = 4;
            // 
            // principalData
            // 
            principalData.AllowUserToAddRows = false;
            principalData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            principalData.Columns.AddRange(new DataGridViewColumn[] { principalNameColumn });
            principalData.Dock = DockStyle.Fill;
            principalData.Location = new Point(0, 0);
            principalData.Name = "principalData";
            principalData.ReadOnly = true;
            principalData.RowHeadersVisible = false;
            principalData.Size = new Size(191, 494);
            principalData.TabIndex = 0;
            principalData.DataError += principalData_DataError;
            // 
            // principalNameColumn
            // 
            principalNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            principalNameColumn.DataPropertyName = "PrincipalName";
            principalNameColumn.HeaderText = "Principal";
            principalNameColumn.Name = "principalNameColumn";
            principalNameColumn.ReadOnly = true;
            // 
            // bindingPrincipal
            // 
            bindingPrincipal.AddingNew += BindingPrincipal_AddingNew;
            bindingPrincipal.DataError += bindingPrincipal_DataError;
            bindingPrincipal.CurrentChanged += BindingPrincipal_CurrentChanged;
            // 
            // bindingMembers
            // 
            bindingMembers.AddingNew += BindingMembers_AddingNew;
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
            ((System.ComponentModel.ISupportInitialize)membershipData).EndInit();
            ((System.ComponentModel.ISupportInitialize)ownershipData).EndInit();
            principalSplit.Panel1.ResumeLayout(false);
            principalSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)principalSplit).EndInit();
            principalSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)principalData).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingPrincipal).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingMembers).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingOwnership).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private SplitContainer principalSplit;
        private TableLayoutPanel principalLayout;
        private Controls.TextBoxData principalLoginData;
        private Controls.TextBoxData principalNameData;
        private Controls.TextBoxData principalAnnotationData;
        private DataGridView membershipData;
        private BindingSource bindingPrincipal;
        private BindingSource bindingMembers;
        private DataGridView ownershipData;
        private BindingSource bindingOwnership;
        private DataGridViewComboBoxColumn roleIdColumn;
        private DataGridViewTextBoxColumn ownershipObjectColumn;
        private DataGridView principalData;
        private DataGridViewTextBoxColumn principalNameColumn;
    }
}