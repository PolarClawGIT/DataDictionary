namespace DataDictionary.Main.Forms.App
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
            TabControl helpTabs;
            helpContentNavigation = new TreeView();
            helpDetailLayout = new TableLayoutPanel();
            helpSubjectData = new Controls.TextBoxData();
            helpDescriptionLayout = new TabPage();
            helpTextData = new Controls.RichTextBoxData();
            helpNameSpaceLayout = new TabPage();
            nameSpaceGroupLayout = new TableLayoutPanel();
            helpNameSpaceData = new Controls.TextBoxData();
            helpToolTipData = new Controls.TextBoxData();
            controlsGroup = new GroupBox();
            controlData = new ListView();
            controlNameColumn = new ColumnHeader();
            controlTypeColumn = new ColumnHeader();
            errorProvider = new ErrorProvider(components);
            helpBinding = new BindingSource(components);
            nameSpaceBinding = new BindingSource(components);
            helpSplitLayout = new SplitContainer();
            helpTabs = new TabControl();
            ((System.ComponentModel.ISupportInitialize)helpSplitLayout).BeginInit();
            helpSplitLayout.Panel1.SuspendLayout();
            helpSplitLayout.Panel2.SuspendLayout();
            helpSplitLayout.SuspendLayout();
            helpDetailLayout.SuspendLayout();
            helpTabs.SuspendLayout();
            helpDescriptionLayout.SuspendLayout();
            helpNameSpaceLayout.SuspendLayout();
            nameSpaceGroupLayout.SuspendLayout();
            controlsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)helpBinding).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nameSpaceBinding).BeginInit();
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
            helpContentNavigation.NodeMouseClick += HelpContentNavigation_NodeMouseClick;
            // 
            // helpDetailLayout
            // 
            helpDetailLayout.ColumnCount = 1;
            helpDetailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            helpDetailLayout.Controls.Add(helpSubjectData, 0, 0);
            helpDetailLayout.Controls.Add(helpTabs, 0, 1);
            helpDetailLayout.Dock = DockStyle.Fill;
            helpDetailLayout.Location = new Point(0, 0);
            helpDetailLayout.Name = "helpDetailLayout";
            helpDetailLayout.RowCount = 2;
            helpDetailLayout.RowStyles.Add(new RowStyle());
            helpDetailLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
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
            // helpTabs
            // 
            helpTabs.Controls.Add(helpDescriptionLayout);
            helpTabs.Controls.Add(helpNameSpaceLayout);
            helpTabs.Dock = DockStyle.Fill;
            helpTabs.Location = new Point(3, 53);
            helpTabs.Name = "helpTabs";
            helpTabs.SelectedIndex = 0;
            helpTabs.Size = new Size(571, 513);
            helpTabs.TabIndex = 6;
            // 
            // helpDescriptionLayout
            // 
            helpDescriptionLayout.BackColor = SystemColors.Control;
            helpDescriptionLayout.Controls.Add(helpTextData);
            helpDescriptionLayout.Location = new Point(4, 24);
            helpDescriptionLayout.Name = "helpDescriptionLayout";
            helpDescriptionLayout.Padding = new Padding(3);
            helpDescriptionLayout.Size = new Size(563, 485);
            helpDescriptionLayout.TabIndex = 0;
            helpDescriptionLayout.Text = "Description";
            // 
            // helpTextData
            // 
            helpTextData.AutoSize = true;
            helpTextData.Dock = DockStyle.Fill;
            helpTextData.HeaderText = "Text";
            helpTextData.Location = new Point(3, 3);
            helpTextData.Name = "helpTextData";
            helpTextData.ReadOnly = false;
            helpTextData.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil Segoe UI;}}\r\n{\\*\\generator Riched20 10.0.19041}\\viewkind4\\uc1 \r\n\\pard\\f0\\fs18\\par\r\n}\r\n";
            helpTextData.Size = new Size(557, 479);
            helpTextData.TabIndex = 2;
            helpTextData.Validated += helpTextData_Validated;
            helpTextData.Validating += helpTextData_Validating;
            // 
            // helpNameSpaceLayout
            // 
            helpNameSpaceLayout.BackColor = SystemColors.Control;
            helpNameSpaceLayout.Controls.Add(nameSpaceGroupLayout);
            helpNameSpaceLayout.Location = new Point(4, 24);
            helpNameSpaceLayout.Name = "helpNameSpaceLayout";
            helpNameSpaceLayout.Padding = new Padding(3);
            helpNameSpaceLayout.Size = new Size(563, 485);
            helpNameSpaceLayout.TabIndex = 1;
            helpNameSpaceLayout.Text = "Programmability";
            // 
            // nameSpaceGroupLayout
            // 
            nameSpaceGroupLayout.ColumnCount = 1;
            nameSpaceGroupLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            nameSpaceGroupLayout.Controls.Add(helpNameSpaceData, 0, 0);
            nameSpaceGroupLayout.Controls.Add(helpToolTipData, 0, 1);
            nameSpaceGroupLayout.Controls.Add(controlsGroup, 0, 2);
            nameSpaceGroupLayout.Dock = DockStyle.Fill;
            nameSpaceGroupLayout.Location = new Point(3, 3);
            nameSpaceGroupLayout.Name = "nameSpaceGroupLayout";
            nameSpaceGroupLayout.RowCount = 3;
            nameSpaceGroupLayout.RowStyles.Add(new RowStyle());
            nameSpaceGroupLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            nameSpaceGroupLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            nameSpaceGroupLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            nameSpaceGroupLayout.Size = new Size(557, 479);
            nameSpaceGroupLayout.TabIndex = 1;
            // 
            // helpNameSpaceData
            // 
            helpNameSpaceData.AutoSize = true;
            helpNameSpaceData.Dock = DockStyle.Fill;
            helpNameSpaceData.HeaderText = "Name Space";
            helpNameSpaceData.Location = new Point(3, 3);
            helpNameSpaceData.Multiline = false;
            helpNameSpaceData.Name = "helpNameSpaceData";
            helpNameSpaceData.ReadOnly = false;
            helpNameSpaceData.Size = new Size(551, 44);
            helpNameSpaceData.TabIndex = 1;
            // 
            // helpToolTipData
            // 
            helpToolTipData.AutoSize = true;
            helpToolTipData.Dock = DockStyle.Fill;
            helpToolTipData.HeaderText = "Short Summary (aka Tool Tip)";
            helpToolTipData.Location = new Point(3, 53);
            helpToolTipData.Multiline = true;
            helpToolTipData.Name = "helpToolTipData";
            helpToolTipData.ReadOnly = false;
            helpToolTipData.Size = new Size(551, 79);
            helpToolTipData.TabIndex = 4;
            // 
            // controlsGroup
            // 
            controlsGroup.Controls.Add(controlData);
            controlsGroup.Dock = DockStyle.Fill;
            controlsGroup.Location = new Point(3, 138);
            controlsGroup.Name = "controlsGroup";
            controlsGroup.Size = new Size(551, 338);
            controlsGroup.TabIndex = 6;
            controlsGroup.TabStop = false;
            controlsGroup.Text = "Controls for: ";
            // 
            // controlData
            // 
            controlData.CheckBoxes = true;
            controlData.Columns.AddRange(new ColumnHeader[] { controlNameColumn, controlTypeColumn });
            controlData.Dock = DockStyle.Fill;
            controlData.Location = new Point(3, 19);
            controlData.Name = "controlData";
            controlData.Size = new Size(545, 316);
            controlData.TabIndex = 5;
            controlData.UseCompatibleStateImageBehavior = false;
            controlData.View = View.Details;
            controlData.Resize += controlData_Resize;
            // 
            // controlNameColumn
            // 
            controlNameColumn.Text = "Control Name";
            controlNameColumn.Width = 300;
            // 
            // controlTypeColumn
            // 
            controlTypeColumn.Text = "Control Type";
            controlTypeColumn.Width = 150;
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
            helpTabs.ResumeLayout(false);
            helpDescriptionLayout.ResumeLayout(false);
            helpDescriptionLayout.PerformLayout();
            helpNameSpaceLayout.ResumeLayout(false);
            nameSpaceGroupLayout.ResumeLayout(false);
            nameSpaceGroupLayout.PerformLayout();
            controlsGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)helpBinding).EndInit();
            ((System.ComponentModel.ISupportInitialize)nameSpaceBinding).EndInit();
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
        private Controls.TextBoxData helpToolTipData;
        private BindingSource helpBinding;
        private TableLayoutPanel nameSpaceGroupLayout;
        private BindingSource nameSpaceBinding;
        private TabPage helpDescriptionLayout;
        private TabPage helpNameSpaceLayout;
        private ListView controlData;
        private ColumnHeader controlNameColumn;
        private ColumnHeader controlTypeColumn;
        private GroupBox controlsGroup;
    }
}