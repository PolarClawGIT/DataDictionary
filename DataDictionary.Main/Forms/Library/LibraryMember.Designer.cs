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
            TableLayoutPanel libraryMemberLayout;
            TabControl memberTabs;
            memberNameSpaceData = new Controls.TextBoxData();
            memberNameData = new Controls.TextBoxData();
            assemblyNameData = new Controls.TextBoxData();
            childMembersTab = new TabPage();
            childMemberData = new DataGridView();
            memberNameColumn = new DataGridViewTextBoxColumn();
            ObjectTypeColumn = new DataGridViewTextBoxColumn();
            memberDataTab = new TabPage();
            memberData = new Controls.TextBoxData();
            memberTypeData = new Controls.TextBoxData();
            objectTypeData = new Controls.TextBoxData();
            libraryMemberLayout = new TableLayoutPanel();
            memberTabs = new TabControl();
            libraryMemberLayout.SuspendLayout();
            memberTabs.SuspendLayout();
            childMembersTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)childMemberData).BeginInit();
            memberDataTab.SuspendLayout();
            SuspendLayout();
            // 
            // libraryMemberLayout
            // 
            libraryMemberLayout.ColumnCount = 2;
            libraryMemberLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            libraryMemberLayout.ColumnStyles.Add(new ColumnStyle());
            libraryMemberLayout.Controls.Add(memberNameSpaceData, 0, 2);
            libraryMemberLayout.Controls.Add(memberNameData, 0, 0);
            libraryMemberLayout.Controls.Add(assemblyNameData, 0, 1);
            libraryMemberLayout.Controls.Add(memberTabs, 0, 4);
            libraryMemberLayout.Controls.Add(memberTypeData, 1, 3);
            libraryMemberLayout.Controls.Add(objectTypeData, 0, 3);
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
            // memberNameSpaceData
            // 
            memberNameSpaceData.AutoSize = true;
            libraryMemberLayout.SetColumnSpan(memberNameSpaceData, 2);
            memberNameSpaceData.Dock = DockStyle.Fill;
            memberNameSpaceData.HeaderText = "Member Name Space";
            memberNameSpaceData.Location = new Point(3, 103);
            memberNameSpaceData.Multiline = false;
            memberNameSpaceData.Name = "memberNameSpaceData";
            memberNameSpaceData.ReadOnly = true;
            memberNameSpaceData.Size = new Size(373, 44);
            memberNameSpaceData.TabIndex = 2;
            // 
            // memberNameData
            // 
            memberNameData.AutoSize = true;
            libraryMemberLayout.SetColumnSpan(memberNameData, 2);
            memberNameData.Dock = DockStyle.Fill;
            memberNameData.HeaderText = "Member Name";
            memberNameData.Location = new Point(3, 3);
            memberNameData.Multiline = false;
            memberNameData.Name = "memberNameData";
            memberNameData.ReadOnly = true;
            memberNameData.Size = new Size(373, 44);
            memberNameData.TabIndex = 0;
            // 
            // assemblyNameData
            // 
            assemblyNameData.AutoSize = true;
            libraryMemberLayout.SetColumnSpan(assemblyNameData, 2);
            assemblyNameData.Dock = DockStyle.Fill;
            assemblyNameData.HeaderText = "Assembly Name";
            assemblyNameData.Location = new Point(3, 53);
            assemblyNameData.Multiline = false;
            assemblyNameData.Name = "assemblyNameData";
            assemblyNameData.ReadOnly = true;
            assemblyNameData.Size = new Size(373, 44);
            assemblyNameData.TabIndex = 1;
            // 
            // memberTabs
            // 
            libraryMemberLayout.SetColumnSpan(memberTabs, 2);
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
            childMemberData.Columns.AddRange(new DataGridViewColumn[] { memberNameColumn, ObjectTypeColumn });
            childMemberData.Dock = DockStyle.Fill;
            childMemberData.Location = new Point(3, 3);
            childMemberData.Name = "childMemberData";
            childMemberData.ReadOnly = true;
            childMemberData.RowTemplate.Height = 25;
            childMemberData.Size = new Size(359, 226);
            childMemberData.TabIndex = 0;
            childMemberData.RowHeaderMouseDoubleClick += childMemberData_RowHeaderMouseDoubleClick;
            // 
            // memberNameColumn
            // 
            memberNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            memberNameColumn.DataPropertyName = "MemberName";
            memberNameColumn.HeaderText = "Member Name";
            memberNameColumn.Name = "memberNameColumn";
            memberNameColumn.ReadOnly = true;
            // 
            // ObjectTypeColumn
            // 
            ObjectTypeColumn.DataPropertyName = "ObjectType";
            ObjectTypeColumn.HeaderText = "Type";
            ObjectTypeColumn.Name = "ObjectTypeColumn";
            ObjectTypeColumn.ReadOnly = true;
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
            // memberTypeData
            // 
            memberTypeData.AutoSize = true;
            memberTypeData.HeaderText = "Member Type";
            memberTypeData.Location = new Point(256, 153);
            memberTypeData.Multiline = false;
            memberTypeData.Name = "memberTypeData";
            memberTypeData.ReadOnly = true;
            memberTypeData.Size = new Size(120, 44);
            memberTypeData.TabIndex = 4;
            // 
            // objectTypeData
            // 
            objectTypeData.AutoSize = true;
            objectTypeData.Dock = DockStyle.Fill;
            objectTypeData.HeaderText = "Object Type";
            objectTypeData.Location = new Point(3, 153);
            objectTypeData.Multiline = false;
            objectTypeData.Name = "objectTypeData";
            objectTypeData.ReadOnly = true;
            objectTypeData.Size = new Size(247, 44);
            objectTypeData.TabIndex = 3;
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
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData memberNameSpaceData;
        private Controls.TextBoxData memberNameData;
        private Controls.TextBoxData memberTypeData;
        private Controls.TextBoxData memberData;
        private Controls.TextBoxData assemblyNameData;
        private TabPage childMembersTab;
        private TabPage memberDataTab;
        private DataGridView childMemberData;
        private DataGridViewTextBoxColumn memberNameColumn;
        private DataGridViewTextBoxColumn ObjectTypeColumn;
        private Controls.TextBoxData objectTypeData;
    }
}