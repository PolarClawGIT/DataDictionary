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
            memberNameSpaceData = new Controls.TextBoxData();
            memberNameData = new Controls.TextBoxData();
            memberTypeData = new Controls.TextBoxData();
            memberData = new Controls.TextBoxData();
            assemblyNameData = new Controls.TextBoxData();
            libraryMemberLayout = new TableLayoutPanel();
            libraryMemberLayout.SuspendLayout();
            SuspendLayout();
            // 
            // libraryMemberLayout
            // 
            libraryMemberLayout.ColumnCount = 1;
            libraryMemberLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            libraryMemberLayout.Controls.Add(memberData, 0, 4);
            libraryMemberLayout.Controls.Add(memberTypeData, 0, 3);
            libraryMemberLayout.Controls.Add(memberNameSpaceData, 0, 2);
            libraryMemberLayout.Controls.Add(memberNameData, 0, 0);
            libraryMemberLayout.Controls.Add(assemblyNameData, 0, 1);
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
            memberNameSpaceData.Dock = DockStyle.Fill;
            memberNameSpaceData.HeaderText = "Member Name Space";
            memberNameSpaceData.Location = new Point(3, 103);
            memberNameSpaceData.Multiline = false;
            memberNameSpaceData.Name = "memberNameSpaceData";
            memberNameSpaceData.ReadOnly = true;
            memberNameSpaceData.Size = new Size(373, 44);
            memberNameSpaceData.TabIndex = 0;
            // 
            // memberNameData
            // 
            memberNameData.AutoSize = true;
            memberNameData.Dock = DockStyle.Fill;
            memberNameData.HeaderText = "Member Name";
            memberNameData.Location = new Point(3, 3);
            memberNameData.Multiline = false;
            memberNameData.Name = "memberNameData";
            memberNameData.ReadOnly = true;
            memberNameData.Size = new Size(373, 44);
            memberNameData.TabIndex = 1;
            // 
            // memberTypeData
            // 
            memberTypeData.AutoSize = true;
            memberTypeData.Dock = DockStyle.Fill;
            memberTypeData.HeaderText = "Member Type";
            memberTypeData.Location = new Point(3, 153);
            memberTypeData.Multiline = false;
            memberTypeData.Name = "memberTypeData";
            memberTypeData.ReadOnly = true;
            memberTypeData.Size = new Size(373, 44);
            memberTypeData.TabIndex = 2;
            // 
            // memberData
            // 
            memberData.AutoSize = true;
            memberData.Dock = DockStyle.Fill;
            memberData.HeaderText = "Member Data";
            memberData.Location = new Point(3, 203);
            memberData.Multiline = true;
            memberData.Name = "memberData";
            memberData.ReadOnly = true;
            memberData.Size = new Size(373, 260);
            memberData.TabIndex = 3;
            // 
            // assemblyNameData
            // 
            assemblyNameData.AutoSize = true;
            assemblyNameData.Dock = DockStyle.Fill;
            assemblyNameData.HeaderText = "Assembly Name";
            assemblyNameData.Location = new Point(3, 53);
            assemblyNameData.Multiline = false;
            assemblyNameData.Name = "assemblyNameData";
            assemblyNameData.ReadOnly = true;
            assemblyNameData.Size = new Size(373, 44);
            assemblyNameData.TabIndex = 4;
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
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData memberNameSpaceData;
        private Controls.TextBoxData memberNameData;
        private Controls.TextBoxData memberTypeData;
        private Controls.TextBoxData memberData;
        private Controls.TextBoxData assemblyNameData;
    }
}