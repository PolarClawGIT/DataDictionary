namespace DataDictionary.Main.Forms.Library
{
    partial class LibraryMember
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
            TableLayoutPanel libraryMemberLayout;
            TabControl memberTabs;
            childMembersTab = new TabPage();
            childMemberData = new DataGridView();
            memberNameColumn = new DataGridViewTextBoxColumn();
            scopeNameColumn = new DataGridViewTextBoxColumn();
            memberDataTab = new TabPage();
            memberData = new Controls.TextBoxData();
            assemblyNameData = new Controls.TextBoxData();
            memberNameSpaceData = new Controls.TextBoxData();
            memberNameData = new Controls.TextBoxData();
            scopeData = new Controls.TextBoxData();
            bindingMember = new BindingSource(components);
            bindingChild = new BindingSource(components);
            libraryMemberLayout = new TableLayoutPanel();
            memberTabs = new TabControl();
            libraryMemberLayout.SuspendLayout();
            memberTabs.SuspendLayout();
            childMembersTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)childMemberData).BeginInit();
            memberDataTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingMember).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingChild).BeginInit();
            SuspendLayout();
            // 
            // libraryMemberLayout
            // 
            libraryMemberLayout.ColumnCount = 1;
            libraryMemberLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            libraryMemberLayout.Controls.Add(memberTabs, 0, 4);
            libraryMemberLayout.Controls.Add(assemblyNameData, 0, 0);
            libraryMemberLayout.Controls.Add(memberNameSpaceData, 0, 1);
            libraryMemberLayout.Controls.Add(memberNameData, 0, 2);
            libraryMemberLayout.Controls.Add(scopeData, 0, 3);
            libraryMemberLayout.Dock = DockStyle.Fill;
            libraryMemberLayout.Location = new Point(0, 25);
            libraryMemberLayout.Name = "libraryMemberLayout";
            libraryMemberLayout.RowCount = 5;
            libraryMemberLayout.RowStyles.Add(new RowStyle());
            libraryMemberLayout.RowStyles.Add(new RowStyle());
            libraryMemberLayout.RowStyles.Add(new RowStyle());
            libraryMemberLayout.RowStyles.Add(new RowStyle());
            libraryMemberLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            libraryMemberLayout.Size = new Size(379, 466);
            libraryMemberLayout.TabIndex = 1;
            // 
            // memberTabs
            // 
            memberTabs.Controls.Add(childMembersTab);
            memberTabs.Controls.Add(memberDataTab);
            memberTabs.Dock = DockStyle.Fill;
            memberTabs.Location = new Point(3, 203);
            memberTabs.Name = "memberTabs";
            memberTabs.SelectedIndex = 0;
            memberTabs.Size = new Size(373, 260);
            memberTabs.TabIndex = 5;
            // 
            // childMembersTab
            // 
            childMembersTab.BackColor = SystemColors.Control;
            childMembersTab.Controls.Add(childMemberData);
            childMembersTab.Location = new Point(4, 24);
            childMembersTab.Name = "childMembersTab";
            childMembersTab.Padding = new Padding(3);
            childMembersTab.Size = new Size(365, 232);
            childMembersTab.TabIndex = 0;
            childMembersTab.Text = "Child Members";
            // 
            // childMemberData
            // 
            childMemberData.AllowUserToAddRows = false;
            childMemberData.AllowUserToDeleteRows = false;
            childMemberData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            childMemberData.Columns.AddRange(new DataGridViewColumn[] { memberNameColumn, scopeNameColumn });
            childMemberData.Dock = DockStyle.Fill;
            childMemberData.Location = new Point(3, 3);
            childMemberData.Name = "childMemberData";
            childMemberData.ReadOnly = true;
            childMemberData.Size = new Size(359, 226);
            childMemberData.TabIndex = 0;
            childMemberData.RowHeaderMouseDoubleClick += childMemberData_RowHeaderMouseDoubleClick;
            // 
            // memberNameColumn
            // 
            memberNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            memberNameColumn.DataPropertyName = "MemberName";
            memberNameColumn.FillWeight = 60F;
            memberNameColumn.HeaderText = "Member Name";
            memberNameColumn.Name = "memberNameColumn";
            memberNameColumn.ReadOnly = true;
            // 
            // scopeNameColumn
            // 
            scopeNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            scopeNameColumn.DataPropertyName = "ScopeName";
            scopeNameColumn.FillWeight = 40F;
            scopeNameColumn.HeaderText = "Scope";
            scopeNameColumn.Name = "scopeNameColumn";
            scopeNameColumn.ReadOnly = true;
            // 
            // memberDataTab
            // 
            memberDataTab.BackColor = SystemColors.Control;
            memberDataTab.Controls.Add(memberData);
            memberDataTab.Location = new Point(4, 24);
            memberDataTab.Name = "memberDataTab";
            memberDataTab.Padding = new Padding(3);
            memberDataTab.Size = new Size(365, 232);
            memberDataTab.TabIndex = 1;
            memberDataTab.Text = "Data";
            // 
            // memberData
            // 
            memberData.AutoSize = true;
            memberData.Dock = DockStyle.Fill;
            memberData.HeaderText = "Member Data (raw)";
            memberData.Location = new Point(3, 3);
            memberData.Multiline = true;
            memberData.Name = "memberData";
            memberData.ReadOnly = true;
            memberData.Size = new Size(359, 226);
            memberData.TabIndex = 3;
            // 
            // assemblyNameData
            // 
            assemblyNameData.AutoSize = true;
            assemblyNameData.Dock = DockStyle.Fill;
            assemblyNameData.HeaderText = "Assembly Name";
            assemblyNameData.Location = new Point(3, 3);
            assemblyNameData.Multiline = false;
            assemblyNameData.Name = "assemblyNameData";
            assemblyNameData.ReadOnly = true;
            assemblyNameData.Size = new Size(373, 44);
            assemblyNameData.TabIndex = 0;
            // 
            // memberNameSpaceData
            // 
            memberNameSpaceData.AutoSize = true;
            memberNameSpaceData.Dock = DockStyle.Fill;
            memberNameSpaceData.HeaderText = "Member Name Space";
            memberNameSpaceData.Location = new Point(3, 53);
            memberNameSpaceData.Multiline = false;
            memberNameSpaceData.Name = "memberNameSpaceData";
            memberNameSpaceData.ReadOnly = true;
            memberNameSpaceData.Size = new Size(373, 44);
            memberNameSpaceData.TabIndex = 1;
            // 
            // memberNameData
            // 
            memberNameData.AutoSize = true;
            memberNameData.Dock = DockStyle.Fill;
            memberNameData.HeaderText = "Member Name";
            memberNameData.Location = new Point(3, 103);
            memberNameData.Multiline = false;
            memberNameData.Name = "memberNameData";
            memberNameData.ReadOnly = true;
            memberNameData.Size = new Size(373, 44);
            memberNameData.TabIndex = 2;
            // 
            // scopeData
            // 
            scopeData.AutoSize = true;
            scopeData.Dock = DockStyle.Fill;
            scopeData.HeaderText = "Scope";
            scopeData.Location = new Point(3, 153);
            scopeData.Multiline = false;
            scopeData.Name = "scopeData";
            scopeData.ReadOnly = true;
            scopeData.Size = new Size(373, 44);
            scopeData.TabIndex = 6;
            // 
            // LibraryMember
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(379, 491);
            Controls.Add(libraryMemberLayout);
            Name = "LibraryMember";
            Text = "LibraryMember";
            Load += LibraryMember_Load;
            Controls.SetChildIndex(libraryMemberLayout, 0);
            libraryMemberLayout.ResumeLayout(false);
            libraryMemberLayout.PerformLayout();
            memberTabs.ResumeLayout(false);
            childMembersTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)childMemberData).EndInit();
            memberDataTab.ResumeLayout(false);
            memberDataTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingMember).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingChild).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData memberNameSpaceData;
        private Controls.TextBoxData memberNameData;
        private Controls.TextBoxData memberData;
        private Controls.TextBoxData assemblyNameData;
        private TabPage childMembersTab;
        private TabPage memberDataTab;
        private DataGridView childMemberData;
        private Controls.TextBoxData scopeData;
        private DataGridViewTextBoxColumn memberNameColumn;
        private DataGridViewTextBoxColumn scopeNameColumn;
        private BindingSource bindingMember;
        private BindingSource bindingChild;
    }
}