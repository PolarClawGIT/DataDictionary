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
            roleIdColumn = new DataGridViewComboBoxColumn();
            isApplicationUserData = new CheckBox();
            principalSplit = new SplitContainer();
            principleList = new ListView();
            bindingPrinciple = new BindingSource(components);
            bindingRole = new BindingSource(components);
            principalLayout = new TableLayoutPanel();
            principalLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)roleMembershipData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)principalSplit).BeginInit();
            principalSplit.Panel1.SuspendLayout();
            principalSplit.Panel2.SuspendLayout();
            principalSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingPrinciple).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingRole).BeginInit();
            SuspendLayout();
            // 
            // principalLayout
            // 
            principalLayout.ColumnCount = 2;
            principalLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            principalLayout.ColumnStyles.Add(new ColumnStyle());
            principalLayout.Controls.Add(principleLoginData, 0, 0);
            principalLayout.Controls.Add(principleNameData, 0, 1);
            principalLayout.Controls.Add(principleAnnotationData, 0, 2);
            principalLayout.Controls.Add(roleMembershipData, 0, 3);
            principalLayout.Controls.Add(isApplicationUserData, 1, 0);
            principalLayout.Dock = DockStyle.Fill;
            principalLayout.Location = new Point(0, 0);
            principalLayout.Name = "principalLayout";
            principalLayout.RowCount = 4;
            principalLayout.RowStyles.Add(new RowStyle());
            principalLayout.RowStyles.Add(new RowStyle());
            principalLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            principalLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            principalLayout.Size = new Size(321, 391);
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
            principleLoginData.Size = new Size(196, 44);
            principleLoginData.TabIndex = 0;
            principleLoginData.WordWrap = true;
            // 
            // principleNameData
            // 
            principleNameData.AutoSize = true;
            principalLayout.SetColumnSpan(principleNameData, 2);
            principleNameData.Dock = DockStyle.Fill;
            principleNameData.HeaderText = "Principle Name";
            principleNameData.Location = new Point(3, 53);
            principleNameData.Multiline = false;
            principleNameData.Name = "principleNameData";
            principleNameData.ReadOnly = false;
            principleNameData.Size = new Size(315, 44);
            principleNameData.TabIndex = 1;
            principleNameData.WordWrap = true;
            // 
            // principleAnnotationData
            // 
            principleAnnotationData.AutoSize = true;
            principalLayout.SetColumnSpan(principleAnnotationData, 2);
            principleAnnotationData.Dock = DockStyle.Fill;
            principleAnnotationData.HeaderText = "Principle Annotation";
            principleAnnotationData.Location = new Point(3, 103);
            principleAnnotationData.Multiline = true;
            principleAnnotationData.Name = "principleAnnotationData";
            principleAnnotationData.ReadOnly = false;
            principleAnnotationData.Size = new Size(315, 110);
            principleAnnotationData.TabIndex = 2;
            principleAnnotationData.WordWrap = true;
            // 
            // roleMembershipData
            // 
            roleMembershipData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            roleMembershipData.Columns.AddRange(new DataGridViewColumn[] { roleIdColumn });
            principalLayout.SetColumnSpan(roleMembershipData, 2);
            roleMembershipData.Dock = DockStyle.Fill;
            roleMembershipData.Location = new Point(3, 219);
            roleMembershipData.Name = "roleMembershipData";
            roleMembershipData.Size = new Size(315, 169);
            roleMembershipData.TabIndex = 3;
            // 
            // roleIdColumn
            // 
            roleIdColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            roleIdColumn.HeaderText = "Role Name";
            roleIdColumn.Name = "roleIdColumn";
            // 
            // isApplicationUserData
            // 
            isApplicationUserData.AutoSize = true;
            isApplicationUserData.Enabled = false;
            isApplicationUserData.Location = new Point(205, 3);
            isApplicationUserData.Name = "isApplicationUserData";
            isApplicationUserData.Size = new Size(113, 19);
            isApplicationUserData.TabIndex = 4;
            isApplicationUserData.Text = "Application User";
            isApplicationUserData.UseVisualStyleBackColor = true;
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
            principalSplit.Size = new Size(487, 391);
            principalSplit.SplitterDistance = 162;
            principalSplit.TabIndex = 4;
            // 
            // principleList
            // 
            principleList.Dock = DockStyle.Fill;
            principleList.Location = new Point(0, 0);
            principleList.Name = "principleList";
            principleList.Size = new Size(162, 391);
            principleList.TabIndex = 0;
            principleList.UseCompatibleStateImageBehavior = false;
            principleList.View = View.Details;
            // 
            // PrincipalManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(487, 416);
            Controls.Add(principalSplit);
            Name = "PrincipalManager";
            Text = "Principal";
            Controls.SetChildIndex(principalSplit, 0);
            principalLayout.ResumeLayout(false);
            principalLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)roleMembershipData).EndInit();
            principalSplit.Panel1.ResumeLayout(false);
            principalSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)principalSplit).EndInit();
            principalSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)bindingPrinciple).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingRole).EndInit();
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
        private DataGridViewComboBoxColumn roleIdColumn;
        private BindingSource bindingPrinciple;
        private BindingSource bindingRole;
        private CheckBox isApplicationUserData;
    }
}