namespace DataDictionary.Main.Controls
{
    partial class ModelAliasNavigation
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            TableLayoutPanel modelAlaisLayout;
            aliasNameData = new TextBoxData();
            aliasScopeData = new ComboBoxData();
            aliasList = new ListView();
            modelAliasGroup = new GroupBox();
            modelAlaisLayout = new TableLayoutPanel();
            modelAlaisLayout.SuspendLayout();
            modelAliasGroup.SuspendLayout();
            SuspendLayout();
            // 
            // modelAlaisLayout
            // 
            modelAlaisLayout.ColumnCount = 1;
            modelAlaisLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            modelAlaisLayout.Controls.Add(aliasNameData, 0, 0);
            modelAlaisLayout.Controls.Add(aliasScopeData, 0, 1);
            modelAlaisLayout.Controls.Add(aliasList, 0, 2);
            modelAlaisLayout.Dock = DockStyle.Fill;
            modelAlaisLayout.Location = new Point(3, 19);
            modelAlaisLayout.Name = "modelAlaisLayout";
            modelAlaisLayout.RowCount = 3;
            modelAlaisLayout.RowStyles.Add(new RowStyle());
            modelAlaisLayout.RowStyles.Add(new RowStyle());
            modelAlaisLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            modelAlaisLayout.Size = new Size(285, 223);
            modelAlaisLayout.TabIndex = 0;
            // 
            // aliasNameData
            // 
            aliasNameData.AutoSize = true;
            aliasNameData.Dock = DockStyle.Fill;
            aliasNameData.HeaderText = "Name";
            aliasNameData.Location = new Point(3, 3);
            aliasNameData.Multiline = false;
            aliasNameData.Name = "aliasNameData";
            aliasNameData.ReadOnly = false;
            aliasNameData.Size = new Size(279, 44);
            aliasNameData.TabIndex = 0;
            // 
            // aliasScopeData
            // 
            aliasScopeData.AutoSize = true;
            aliasScopeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            aliasScopeData.Dock = DockStyle.Fill;
            aliasScopeData.DropDownStyle = ComboBoxStyle.DropDown;
            aliasScopeData.HeaderText = "Scope";
            aliasScopeData.Location = new Point(3, 53);
            aliasScopeData.Name = "aliasScopeData";
            aliasScopeData.ReadOnly = false;
            aliasScopeData.Size = new Size(279, 44);
            aliasScopeData.TabIndex = 1;
            // 
            // aliasList
            // 
            aliasList.Dock = DockStyle.Fill;
            aliasList.FullRowSelect = true;
            aliasList.HeaderStyle = ColumnHeaderStyle.None;
            aliasList.Location = new Point(3, 103);
            aliasList.Name = "aliasList";
            aliasList.Size = new Size(279, 117);
            aliasList.TabIndex = 2;
            aliasList.UseCompatibleStateImageBehavior = false;
            aliasList.View = View.Details;
            aliasList.SelectedIndexChanged += AliasList_SelectedIndexChanged;
            // 
            // modelAliasGroup
            // 
            modelAliasGroup.Controls.Add(modelAlaisLayout);
            modelAliasGroup.Dock = DockStyle.Fill;
            modelAliasGroup.Location = new Point(0, 0);
            modelAliasGroup.Name = "modelAliasGroup";
            modelAliasGroup.Size = new Size(291, 245);
            modelAliasGroup.TabIndex = 0;
            modelAliasGroup.TabStop = false;
            modelAliasGroup.Text = "Alias";
            // 
            // ModelAliasNavigation
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(modelAliasGroup);
            Name = "ModelAliasNavigation";
            Size = new Size(291, 245);
            Load += ModelAliasNavigation_Load;
            modelAlaisLayout.ResumeLayout(false);
            modelAlaisLayout.PerformLayout();
            modelAliasGroup.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox modelAliasGroup;
        private TableLayoutPanel modelAlaisLayout;
        private TextBoxData aliasNameData;
        private ComboBoxData aliasScopeData;
        private ListView aliasList;
    }
}
