namespace DataDictionary.Main.Forms.Domain.Controls
{
    partial class DomainAlias
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
            TableLayoutPanel aliasLayout;
            GroupBox aliasBrowseLayout;
            aliasNameData = new DataDictionary.Main.Controls.TextBoxData();
            aliasScopeData = new DataDictionary.Main.Controls.ComboBoxData();
            aliasBrowser = new ListView();
            aliasLayout = new TableLayoutPanel();
            aliasBrowseLayout = new GroupBox();
            aliasLayout.SuspendLayout();
            aliasBrowseLayout.SuspendLayout();
            SuspendLayout();
            // 
            // aliasLayout
            // 
            aliasLayout.ColumnCount = 1;
            aliasLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            aliasLayout.Controls.Add(aliasNameData, 0, 0);
            aliasLayout.Controls.Add(aliasScopeData, 0, 1);
            aliasLayout.Controls.Add(aliasBrowseLayout, 0, 2);
            aliasLayout.Dock = DockStyle.Fill;
            aliasLayout.Location = new Point(0, 0);
            aliasLayout.Name = "aliasLayout";
            aliasLayout.RowCount = 3;
            aliasLayout.RowStyles.Add(new RowStyle());
            aliasLayout.RowStyles.Add(new RowStyle());
            aliasLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            aliasLayout.Size = new Size(262, 196);
            aliasLayout.TabIndex = 0;
            // 
            // aliasNameData
            // 
            aliasNameData.AutoSize = true;
            aliasNameData.Dock = DockStyle.Fill;
            aliasNameData.HeaderText = "Alias Name";
            aliasNameData.Location = new Point(3, 3);
            aliasNameData.Multiline = false;
            aliasNameData.Name = "aliasNameData";
            aliasNameData.ReadOnly = false;
            aliasNameData.Size = new Size(256, 44);
            aliasNameData.TabIndex = 0;
            // 
            // aliasScopeData
            // 
            aliasScopeData.AutoSize = true;
            aliasScopeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            aliasScopeData.Dock = DockStyle.Fill;
            aliasScopeData.DropDownStyle = ComboBoxStyle.DropDown;
            aliasScopeData.HeaderText = "Alias Scope";
            aliasScopeData.Location = new Point(3, 53);
            aliasScopeData.Name = "aliasScopeData";
            aliasScopeData.ReadOnly = false;
            aliasScopeData.Size = new Size(256, 44);
            aliasScopeData.TabIndex = 1;
            // 
            // aliasBrowseLayout
            // 
            aliasBrowseLayout.Controls.Add(aliasBrowser);
            aliasBrowseLayout.Dock = DockStyle.Fill;
            aliasBrowseLayout.Location = new Point(3, 103);
            aliasBrowseLayout.Name = "aliasBrowseLayout";
            aliasBrowseLayout.Size = new Size(256, 90);
            aliasBrowseLayout.TabIndex = 2;
            aliasBrowseLayout.TabStop = false;
            aliasBrowseLayout.Text = "Alias browser";
            // 
            // aliasBrowser
            // 
            aliasBrowser.Dock = DockStyle.Fill;
            aliasBrowser.FullRowSelect = true;
            aliasBrowser.HeaderStyle = ColumnHeaderStyle.None;
            aliasBrowser.Location = new Point(3, 19);
            aliasBrowser.MultiSelect = false;
            aliasBrowser.Name = "aliasBrowser";
            aliasBrowser.Size = new Size(250, 68);
            aliasBrowser.TabIndex = 0;
            aliasBrowser.UseCompatibleStateImageBehavior = false;
            aliasBrowser.View = View.Details;
            aliasBrowser.SelectedIndexChanged += AliasBrowser_SelectedIndexChanged;
            aliasBrowser.DoubleClick += AliasBrowser_DoubleClick;
            // 
            // DomainAlias
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(aliasLayout);
            Name = "DomainAlias";
            Size = new Size(262, 196);
            aliasLayout.ResumeLayout(false);
            aliasLayout.PerformLayout();
            aliasBrowseLayout.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataDictionary.Main.Controls.TextBoxData aliasNameData;
        private DataDictionary.Main.Controls.ComboBoxData aliasScopeData;
        private ListView aliasBrowser;
    }
}
