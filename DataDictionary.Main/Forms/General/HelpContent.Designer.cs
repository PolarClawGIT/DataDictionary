namespace DataDictionary.Main.Forms.General
{
    partial class HelpContent
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
            TableLayoutPanel helpDetailLayout;
            helpContentNavigation = new TreeView();
            helpSubjectData = new Controls.TextBoxData();
            helpTextData = new RichTextBox();
            helpBinding = new BindingSource(components);
            helpSplitLayout = new SplitContainer();
            helpDetailLayout = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)helpSplitLayout).BeginInit();
            helpSplitLayout.Panel1.SuspendLayout();
            helpSplitLayout.Panel2.SuspendLayout();
            helpSplitLayout.SuspendLayout();
            helpDetailLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)helpBinding).BeginInit();
            SuspendLayout();
            // 
            // helpSplitLayout
            // 
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
            helpSplitLayout.Size = new Size(800, 425);
            helpSplitLayout.SplitterDistance = 266;
            helpSplitLayout.TabIndex = 4;
            // 
            // helpContentNavigation
            // 
            helpContentNavigation.Dock = DockStyle.Fill;
            helpContentNavigation.Location = new Point(0, 0);
            helpContentNavigation.Name = "helpContentNavigation";
            helpContentNavigation.Size = new Size(266, 425);
            helpContentNavigation.TabIndex = 1;
            helpContentNavigation.NodeMouseClick += HelpContentNavigation_NodeMouseClick;
            // 
            // helpDetailLayout
            // 
            helpDetailLayout.ColumnCount = 1;
            helpDetailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            helpDetailLayout.Controls.Add(helpSubjectData, 0, 0);
            helpDetailLayout.Controls.Add(helpTextData, 0, 1);
            helpDetailLayout.Dock = DockStyle.Fill;
            helpDetailLayout.Location = new Point(0, 0);
            helpDetailLayout.Name = "helpDetailLayout";
            helpDetailLayout.RowCount = 2;
            helpDetailLayout.RowStyles.Add(new RowStyle());
            helpDetailLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            helpDetailLayout.Size = new Size(530, 425);
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
            helpSubjectData.ReadOnly = true;
            helpSubjectData.Size = new Size(524, 44);
            helpSubjectData.TabIndex = 1;
            helpSubjectData.WordWrap = true;
            // 
            // helpTextData
            // 
            helpTextData.BackColor = SystemColors.Window;
            helpTextData.Dock = DockStyle.Fill;
            helpTextData.Location = new Point(3, 53);
            helpTextData.Name = "helpTextData";
            helpTextData.ReadOnly = true;
            helpTextData.Size = new Size(524, 369);
            helpTextData.TabIndex = 2;
            helpTextData.Text = "";
            // 
            // HelpContent
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(helpSplitLayout);
            Name = "HelpContent";
            Text = "HelpContent";
            Load += HelpContent_Load;
            Controls.SetChildIndex(helpSplitLayout, 0);
            helpSplitLayout.Panel1.ResumeLayout(false);
            helpSplitLayout.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)helpSplitLayout).EndInit();
            helpSplitLayout.ResumeLayout(false);
            helpDetailLayout.ResumeLayout(false);
            helpDetailLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)helpBinding).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TreeView helpContentNavigation;
        private Controls.TextBoxData helpSubjectData;
        private BindingSource helpBinding;
        private RichTextBox helpTextData;
    }
}