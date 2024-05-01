namespace DataDictionary.Main.Controls
{
    partial class NamedScopeData
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
            TableLayoutPanel nameScopeLayout;
            pathData = new TextBoxData();
            browser = new ListView();
            scopeData = new ComboBoxData();
            applyCommand = new Button();
            groupBox = new GroupBox();
            nameScopeLayout = new TableLayoutPanel();
            nameScopeLayout.SuspendLayout();
            groupBox.SuspendLayout();
            SuspendLayout();
            // 
            // nameScopeLayout
            // 
            nameScopeLayout.ColumnCount = 2;
            nameScopeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            nameScopeLayout.ColumnStyles.Add(new ColumnStyle());
            nameScopeLayout.Controls.Add(pathData, 0, 0);
            nameScopeLayout.Controls.Add(browser, 0, 2);
            nameScopeLayout.Controls.Add(scopeData, 0, 1);
            nameScopeLayout.Controls.Add(applyCommand, 1, 1);
            nameScopeLayout.Dock = DockStyle.Fill;
            nameScopeLayout.Location = new Point(3, 19);
            nameScopeLayout.Name = "nameScopeLayout";
            nameScopeLayout.RowCount = 3;
            nameScopeLayout.RowStyles.Add(new RowStyle());
            nameScopeLayout.RowStyles.Add(new RowStyle());
            nameScopeLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            nameScopeLayout.Size = new Size(266, 207);
            nameScopeLayout.TabIndex = 1;
            // 
            // pathData
            // 
            pathData.AutoSize = true;
            nameScopeLayout.SetColumnSpan(pathData, 2);
            pathData.Dock = DockStyle.Fill;
            pathData.HeaderText = "Path";
            pathData.Location = new Point(3, 3);
            pathData.Multiline = false;
            pathData.Name = "pathData";
            pathData.ReadOnly = false;
            pathData.Size = new Size(260, 44);
            pathData.TabIndex = 0;
            pathData.Validated += PathData_Validated;
            pathData.Validating += PathData_Validating;
            // 
            // browser
            // 
            nameScopeLayout.SetColumnSpan(browser, 2);
            browser.Dock = DockStyle.Fill;
            browser.HeaderStyle = ColumnHeaderStyle.None;
            browser.Location = new Point(3, 103);
            browser.Name = "browser";
            browser.Size = new Size(260, 101);
            browser.TabIndex = 0;
            browser.UseCompatibleStateImageBehavior = false;
            browser.View = View.Details;
            browser.SelectedIndexChanged += Browser_SelectedIndexChanged;
            // 
            // scopeData
            // 
            scopeData.AutoSize = true;
            scopeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            scopeData.Dock = DockStyle.Fill;
            scopeData.DropDownStyle = ComboBoxStyle.DropDown;
            scopeData.HeaderText = "Scope";
            scopeData.Location = new Point(3, 53);
            scopeData.Name = "scopeData";
            scopeData.ReadOnly = false;
            scopeData.Size = new Size(192, 44);
            scopeData.TabIndex = 1;
            // 
            // applyCommand
            // 
            applyCommand.AutoSize = true;
            applyCommand.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            applyCommand.Dock = DockStyle.Bottom;
            applyCommand.Image = Properties.Resources.Namespace;
            applyCommand.Location = new Point(201, 72);
            applyCommand.Name = "applyCommand";
            applyCommand.Size = new Size(62, 25);
            applyCommand.TabIndex = 2;
            applyCommand.Text = "apply";
            applyCommand.TextImageRelation = TextImageRelation.ImageBeforeText;
            applyCommand.UseVisualStyleBackColor = true;
            applyCommand.Click += applyCommand_Click;
            // 
            // groupBox
            // 
            groupBox.AutoSize = true;
            groupBox.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox.Controls.Add(nameScopeLayout);
            groupBox.Dock = DockStyle.Fill;
            groupBox.Location = new Point(0, 0);
            groupBox.Name = "groupBox";
            groupBox.Size = new Size(272, 229);
            groupBox.TabIndex = 0;
            groupBox.TabStop = false;
            groupBox.Text = "Named Scope";
            // 
            // NamedScopeData
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox);
            Name = "NamedScopeData";
            Size = new Size(272, 229);
            nameScopeLayout.ResumeLayout(false);
            nameScopeLayout.PerformLayout();
            groupBox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox;
        private ListView browser;
        private TableLayoutPanel nameScopeLayout;
        private TextBoxData pathData;
        private ComboBoxData scopeData;
        private Button applyCommand;
    }
}
