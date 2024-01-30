namespace DataDictionary.Main.Dialogs
{
    partial class HelpSubject
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
            SplitContainer helpSplitLayout;
            helpContentNavigation = new TreeView();
            helpDetailLayout = new TableLayoutPanel();
            helpSubjectData = new Controls.TextBoxData();
            helpNameSpaceData = new Controls.TextBoxData();
            helpToolTipData = new Controls.TextBoxData();
            helpTextData = new Controls.RichTextBoxData();
            nameSpacesGroup = new GroupBox();
            nameSpaceBrowser = new ListView();
            nameSpaceNameColumn = new ColumnHeader();
            nameSpaceTypeColumn = new ColumnHeader();
            errorProvider = new ErrorProvider(components);
            helpBinding = new BindingSource(components);
            helpSplitLayout = new SplitContainer();
            ((System.ComponentModel.ISupportInitialize)helpSplitLayout).BeginInit();
            helpSplitLayout.Panel1.SuspendLayout();
            helpSplitLayout.Panel2.SuspendLayout();
            helpSplitLayout.SuspendLayout();
            helpDetailLayout.SuspendLayout();
            nameSpacesGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)helpBinding).BeginInit();
            SuspendLayout();
            // 
            // helpSplitLayout
            // 
            helpSplitLayout.BorderStyle = BorderStyle.FixedSingle;
            helpSplitLayout.Dock = DockStyle.Fill;
            helpSplitLayout.Location = new Point(0, 25);
            helpSplitLayout.Name = "helpSplitLayout";
            // 
            // helpSplitLayout.Panel1
            // 
            helpSplitLayout.Panel1.Controls.Add(helpContentNavigation);
            // 
            // helpSplitLayout.Panel2
            // 
            helpSplitLayout.Panel2.Controls.Add(helpDetailLayout);
            helpSplitLayout.Size = new Size(736, 571);
            helpSplitLayout.SplitterDistance = 153;
            helpSplitLayout.TabIndex = 0;
            // 
            // helpContentNavigation
            // 
            helpContentNavigation.Dock = DockStyle.Fill;
            helpContentNavigation.Location = new Point(0, 0);
            helpContentNavigation.Name = "helpContentNavigation";
            helpContentNavigation.Size = new Size(151, 569);
            helpContentNavigation.TabIndex = 0;
            helpContentNavigation.NodeMouseDoubleClick += helpContentNavigation_NodeMouseDoubleClick;
            // 
            // helpDetailLayout
            // 
            helpDetailLayout.ColumnCount = 1;
            helpDetailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            helpDetailLayout.Controls.Add(helpSubjectData, 0, 0);
            helpDetailLayout.Controls.Add(helpNameSpaceData, 0, 1);
            helpDetailLayout.Controls.Add(helpToolTipData, 0, 3);
            helpDetailLayout.Controls.Add(helpTextData, 0, 4);
            helpDetailLayout.Controls.Add(nameSpacesGroup, 0, 2);
            helpDetailLayout.Dock = DockStyle.Fill;
            helpDetailLayout.Location = new Point(0, 0);
            helpDetailLayout.Name = "helpDetailLayout";
            helpDetailLayout.RowCount = 5;
            helpDetailLayout.RowStyles.Add(new RowStyle());
            helpDetailLayout.RowStyles.Add(new RowStyle());
            helpDetailLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            helpDetailLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            helpDetailLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            helpDetailLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            helpDetailLayout.Size = new Size(577, 569);
            helpDetailLayout.TabIndex = 0;
            // 
            // helpSubjectData
            // 
            helpSubjectData.AutoSize = true;
            helpSubjectData.Dock = DockStyle.Fill;
            helpSubjectData.HeaderText = "Subject";
            helpSubjectData.Location = new Point(3, 3);
            helpSubjectData.Multiline = false;
            helpSubjectData.Name = "helpSubjectData";
            helpSubjectData.ReadOnly = false;
            helpSubjectData.Size = new Size(571, 44);
            helpSubjectData.TabIndex = 0;
            helpSubjectData.Validated += helpSubjectData_Validated;
            helpSubjectData.Validating += helpSubjectData_Validating;
            // 
            // helpNameSpaceData
            // 
            helpNameSpaceData.AutoSize = true;
            helpNameSpaceData.Dock = DockStyle.Fill;
            helpNameSpaceData.HeaderText = "Name Space";
            helpNameSpaceData.Location = new Point(3, 53);
            helpNameSpaceData.Multiline = false;
            helpNameSpaceData.Name = "helpNameSpaceData";
            helpNameSpaceData.ReadOnly = false;
            helpNameSpaceData.Size = new Size(571, 44);
            helpNameSpaceData.TabIndex = 1;
            // 
            // helpToolTipData
            // 
            helpToolTipData.AutoSize = true;
            helpToolTipData.Dock = DockStyle.Fill;
            helpToolTipData.HeaderText = "Tool Tips";
            helpToolTipData.Location = new Point(3, 196);
            helpToolTipData.Multiline = true;
            helpToolTipData.Name = "helpToolTipData";
            helpToolTipData.ReadOnly = false;
            helpToolTipData.Size = new Size(571, 87);
            helpToolTipData.TabIndex = 4;
            // 
            // helpTextData
            // 
            helpTextData.AutoSize = true;
            helpTextData.Dock = DockStyle.Fill;
            helpTextData.HeaderText = "Text";
            helpTextData.Location = new Point(3, 289);
            helpTextData.Name = "helpTextData";
            helpTextData.ReadOnly = false;
            helpTextData.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil Segoe UI;}}\r\n{\\*\\generator Riched20 10.0.19041}\\viewkind4\\uc1 \r\n\\pard\\f0\\fs18\\par\r\n}\r\n";
            helpTextData.Size = new Size(571, 277);
            helpTextData.TabIndex = 2;
            helpTextData.Validated += helpTextData_Validated;
            helpTextData.Validating += helpTextData_Validating;
            // 
            // nameSpacesGroup
            // 
            nameSpacesGroup.Controls.Add(nameSpaceBrowser);
            nameSpacesGroup.Dock = DockStyle.Fill;
            nameSpacesGroup.Location = new Point(3, 103);
            nameSpacesGroup.Name = "nameSpacesGroup";
            nameSpacesGroup.Size = new Size(571, 87);
            nameSpacesGroup.TabIndex = 5;
            nameSpacesGroup.TabStop = false;
            nameSpacesGroup.Text = "Name Spaces";
            // 
            // nameSpaceBrowser
            // 
            nameSpaceBrowser.Columns.AddRange(new ColumnHeader[] { nameSpaceNameColumn, nameSpaceTypeColumn });
            nameSpaceBrowser.Dock = DockStyle.Fill;
            nameSpaceBrowser.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            nameSpaceBrowser.Location = new Point(3, 19);
            nameSpaceBrowser.MultiSelect = false;
            nameSpaceBrowser.Name = "nameSpaceBrowser";
            nameSpaceBrowser.Size = new Size(565, 65);
            nameSpaceBrowser.TabIndex = 3;
            nameSpaceBrowser.UseCompatibleStateImageBehavior = false;
            nameSpaceBrowser.View = View.Details;
            nameSpaceBrowser.DoubleClick += nameSpaceBrowser_DoubleClick;
            // 
            // nameSpaceNameColumn
            // 
            nameSpaceNameColumn.Text = "Control Name";
            nameSpaceNameColumn.Width = 300;
            // 
            // nameSpaceTypeColumn
            // 
            nameSpaceTypeColumn.Text = "Control Type";
            nameSpaceTypeColumn.Width = 150;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // helpBinding
            // 
            helpBinding.AddingNew += helpBinding_AddingNew;
            helpBinding.BindingComplete += helpBinding_BindingComplete;
            helpBinding.DataError += helpBinding_DataError;
            helpBinding.CurrentChanged += helpBinding_CurrentChanged;
            // 
            // HelpSubject
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(736, 596);
            Controls.Add(helpSplitLayout);
            Name = "HelpSubject";
            Text = "Help Subject";
            FormClosing += HelpSubject_FormClosing;
            Load += HelpSubject_Load;
            Controls.SetChildIndex(helpSplitLayout, 0);
            helpSplitLayout.Panel1.ResumeLayout(false);
            helpSplitLayout.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)helpSplitLayout).EndInit();
            helpSplitLayout.ResumeLayout(false);
            helpDetailLayout.ResumeLayout(false);
            helpDetailLayout.PerformLayout();
            nameSpacesGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)helpBinding).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TreeView helpContentNavigation;
        private Controls.TextBoxData helpNameSpaceData;
        private Controls.RichTextBoxData helpTextData;
        private Controls.TextBoxData helpSubjectData;
        private ErrorProvider errorProvider;
        private TableLayoutPanel helpDetailLayout;
        private ListView nameSpaceBrowser;
        private Controls.TextBoxData helpToolTipData;
        private BindingSource helpBinding;
        private ColumnHeader nameSpaceTypeColumn;
        private ColumnHeader nameSpaceNameColumn;
        private GroupBox nameSpacesGroup;
    }
}