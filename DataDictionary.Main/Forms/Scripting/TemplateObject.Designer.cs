namespace DataDictionary.Main.Forms.Scripting
{
    partial class TemplateObject
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
            TableLayoutPanel templateObjectLayout;
            TableLayoutPanel aliasCommandLayout;
            GroupBox objectOptionGroup;
            TableLayoutPanel objectOptionsLayout;
            Label excludeLabel;
            Label orphanedLabel;
            selectCommand = new Button();
            addCommand = new Button();
            isInModelData = new CheckBox();
            objectNameData = new DataDictionary.Main.Controls.TextBoxData();
            objectGrid = new DataGridView();
            objectScopeColumn = new DataGridViewComboBoxColumn();
            objectNameColumn = new DataGridViewTextBoxColumn();
            objectScopeData = new DataDictionary.Main.Controls.ComboBoxData();
            templateTitleData = new DataDictionary.Main.Controls.TextBoxData();
            objectIsExcluded = new CheckBox();
            objectKeepOrphaned = new CheckBox();
            templateObjectLayout = new TableLayoutPanel();
            aliasCommandLayout = new TableLayoutPanel();
            objectOptionGroup = new GroupBox();
            objectOptionsLayout = new TableLayoutPanel();
            excludeLabel = new Label();
            orphanedLabel = new Label();
            templateObjectLayout.SuspendLayout();
            aliasCommandLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)objectGrid).BeginInit();
            objectOptionGroup.SuspendLayout();
            objectOptionsLayout.SuspendLayout();
            SuspendLayout();
            // 
            // templateObjectLayout
            // 
            templateObjectLayout.ColumnCount = 2;
            templateObjectLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            templateObjectLayout.ColumnStyles.Add(new ColumnStyle());
            templateObjectLayout.Controls.Add(aliasCommandLayout, 1, 2);
            templateObjectLayout.Controls.Add(objectNameData, 0, 3);
            templateObjectLayout.Controls.Add(objectGrid, 0, 1);
            templateObjectLayout.Controls.Add(objectScopeData, 0, 2);
            templateObjectLayout.Controls.Add(templateTitleData, 0, 0);
            templateObjectLayout.Controls.Add(objectOptionGroup, 0, 4);
            templateObjectLayout.Dock = DockStyle.Fill;
            templateObjectLayout.Location = new Point(0, 25);
            templateObjectLayout.Name = "templateObjectLayout";
            templateObjectLayout.RowCount = 5;
            templateObjectLayout.RowStyles.Add(new RowStyle());
            templateObjectLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            templateObjectLayout.RowStyles.Add(new RowStyle());
            templateObjectLayout.RowStyles.Add(new RowStyle());
            templateObjectLayout.RowStyles.Add(new RowStyle());
            templateObjectLayout.Size = new Size(512, 482);
            templateObjectLayout.TabIndex = 4;
            // 
            // aliasCommandLayout
            // 
            aliasCommandLayout.AutoSize = true;
            aliasCommandLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            aliasCommandLayout.ColumnCount = 1;
            aliasCommandLayout.ColumnStyles.Add(new ColumnStyle());
            aliasCommandLayout.Controls.Add(selectCommand, 0, 1);
            aliasCommandLayout.Controls.Add(addCommand, 0, 2);
            aliasCommandLayout.Controls.Add(isInModelData, 0, 0);
            aliasCommandLayout.Dock = DockStyle.Fill;
            aliasCommandLayout.Location = new Point(428, 290);
            aliasCommandLayout.Name = "aliasCommandLayout";
            aliasCommandLayout.RowCount = 3;
            templateObjectLayout.SetRowSpan(aliasCommandLayout, 2);
            aliasCommandLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            aliasCommandLayout.RowStyles.Add(new RowStyle());
            aliasCommandLayout.RowStyles.Add(new RowStyle());
            aliasCommandLayout.Size = new Size(81, 96);
            aliasCommandLayout.TabIndex = 4;
            // 
            // selectCommand
            // 
            selectCommand.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            selectCommand.Location = new Point(3, 39);
            selectCommand.Name = "selectCommand";
            selectCommand.Size = new Size(75, 24);
            selectCommand.TabIndex = 1;
            selectCommand.Text = "Select";
            selectCommand.TextImageRelation = TextImageRelation.ImageBeforeText;
            selectCommand.UseVisualStyleBackColor = true;
            // 
            // addCommand
            // 
            addCommand.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            addCommand.Location = new Point(3, 69);
            addCommand.Name = "addCommand";
            addCommand.Size = new Size(75, 24);
            addCommand.TabIndex = 2;
            addCommand.Text = "New";
            addCommand.TextImageRelation = TextImageRelation.ImageBeforeText;
            addCommand.UseVisualStyleBackColor = true;
            // 
            // isInModelData
            // 
            isInModelData.AutoSize = true;
            isInModelData.Enabled = false;
            isInModelData.Location = new Point(3, 3);
            isInModelData.Name = "isInModelData";
            isInModelData.Size = new Size(73, 19);
            isInModelData.TabIndex = 0;
            isInModelData.Text = "in Model";
            isInModelData.UseVisualStyleBackColor = true;
            // 
            // objectNameData
            // 
            objectNameData.AutoSize = true;
            objectNameData.Dock = DockStyle.Fill;
            objectNameData.HeaderText = "Object Name";
            objectNameData.Location = new Point(3, 342);
            objectNameData.Multiline = false;
            objectNameData.Name = "objectNameData";
            objectNameData.ReadOnly = true;
            objectNameData.Size = new Size(419, 44);
            objectNameData.TabIndex = 2;
            objectNameData.WordWrap = true;
            // 
            // objectGrid
            // 
            objectGrid.AllowUserToAddRows = false;
            objectGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            objectGrid.Columns.AddRange(new DataGridViewColumn[] { objectScopeColumn, objectNameColumn });
            templateObjectLayout.SetColumnSpan(objectGrid, 2);
            objectGrid.Dock = DockStyle.Fill;
            objectGrid.Location = new Point(3, 53);
            objectGrid.Name = "objectGrid";
            objectGrid.ReadOnly = true;
            objectGrid.Size = new Size(506, 231);
            objectGrid.TabIndex = 0;
            // 
            // objectScopeColumn
            // 
            objectScopeColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            objectScopeColumn.DataPropertyName = "ObjectScope";
            objectScopeColumn.FillWeight = 50F;
            objectScopeColumn.HeaderText = "Scope";
            objectScopeColumn.Name = "objectScopeColumn";
            objectScopeColumn.ReadOnly = true;
            // 
            // objectNameColumn
            // 
            objectNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            objectNameColumn.DataPropertyName = "ObjectName";
            objectNameColumn.HeaderText = "Object Name";
            objectNameColumn.Name = "objectNameColumn";
            objectNameColumn.ReadOnly = true;
            // 
            // objectScopeData
            // 
            objectScopeData.AutoSize = true;
            objectScopeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            objectScopeData.Dock = DockStyle.Fill;
            objectScopeData.DropDownStyle = ComboBoxStyle.DropDown;
            objectScopeData.HeaderText = "Scope";
            objectScopeData.Location = new Point(3, 290);
            objectScopeData.Name = "objectScopeData";
            objectScopeData.ReadOnly = true;
            objectScopeData.Size = new Size(419, 46);
            objectScopeData.TabIndex = 1;
            // 
            // templateTitleData
            // 
            templateTitleData.AutoSize = true;
            templateObjectLayout.SetColumnSpan(templateTitleData, 2);
            templateTitleData.Dock = DockStyle.Fill;
            templateTitleData.HeaderText = "Template";
            templateTitleData.Location = new Point(3, 3);
            templateTitleData.Multiline = false;
            templateTitleData.Name = "templateTitleData";
            templateTitleData.ReadOnly = true;
            templateTitleData.Size = new Size(506, 44);
            templateTitleData.TabIndex = 1;
            templateTitleData.WordWrap = true;
            // 
            // objectOptionGroup
            // 
            objectOptionGroup.AutoSize = true;
            templateObjectLayout.SetColumnSpan(objectOptionGroup, 2);
            objectOptionGroup.Controls.Add(objectOptionsLayout);
            objectOptionGroup.Dock = DockStyle.Fill;
            objectOptionGroup.Location = new Point(3, 392);
            objectOptionGroup.Name = "objectOptionGroup";
            objectOptionGroup.Size = new Size(506, 87);
            objectOptionGroup.TabIndex = 5;
            objectOptionGroup.TabStop = false;
            objectOptionGroup.Text = "Document Behavior";
            // 
            // objectOptionsLayout
            // 
            objectOptionsLayout.AutoSize = true;
            objectOptionsLayout.ColumnCount = 2;
            objectOptionsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            objectOptionsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            objectOptionsLayout.Controls.Add(objectIsExcluded, 0, 0);
            objectOptionsLayout.Controls.Add(objectKeepOrphaned, 1, 0);
            objectOptionsLayout.Controls.Add(excludeLabel, 0, 1);
            objectOptionsLayout.Controls.Add(orphanedLabel, 1, 1);
            objectOptionsLayout.Dock = DockStyle.Fill;
            objectOptionsLayout.Location = new Point(3, 19);
            objectOptionsLayout.Name = "objectOptionsLayout";
            objectOptionsLayout.RowCount = 2;
            objectOptionsLayout.RowStyles.Add(new RowStyle());
            objectOptionsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            objectOptionsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            objectOptionsLayout.Size = new Size(500, 65);
            objectOptionsLayout.TabIndex = 6;
            // 
            // objectIsExcluded
            // 
            objectIsExcluded.AutoSize = true;
            objectIsExcluded.Location = new Point(3, 3);
            objectIsExcluded.Name = "objectIsExcluded";
            objectIsExcluded.Size = new Size(84, 19);
            objectIsExcluded.TabIndex = 0;
            objectIsExcluded.Text = "Is Excluded";
            objectIsExcluded.UseVisualStyleBackColor = true;
            // 
            // objectKeepOrphaned
            // 
            objectKeepOrphaned.AutoSize = true;
            objectKeepOrphaned.Location = new Point(253, 3);
            objectKeepOrphaned.Name = "objectKeepOrphaned";
            objectKeepOrphaned.Size = new Size(108, 19);
            objectKeepOrphaned.TabIndex = 1;
            objectKeepOrphaned.Text = "Keep Orphaned";
            objectKeepOrphaned.UseVisualStyleBackColor = true;
            // 
            // excludeLabel
            // 
            excludeLabel.Dock = DockStyle.Fill;
            excludeLabel.Location = new Point(3, 25);
            excludeLabel.Name = "excludeLabel";
            excludeLabel.Size = new Size(244, 40);
            excludeLabel.TabIndex = 2;
            excludeLabel.Text = "Exclude this Object when Documents are auto-generated.";
            // 
            // orphanedLabel
            // 
            orphanedLabel.Dock = DockStyle.Fill;
            orphanedLabel.Location = new Point(253, 25);
            orphanedLabel.Name = "orphanedLabel";
            orphanedLabel.Size = new Size(244, 40);
            orphanedLabel.TabIndex = 3;
            orphanedLabel.Text = "Keep Orphaned Document for Object not in the Model";
            // 
            // TemplateObject
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(512, 507);
            Controls.Add(templateObjectLayout);
            Name = "TemplateObject";
            Text = "TemplateObject";
            Load += TemplateObject_Load;
            Controls.SetChildIndex(templateObjectLayout, 0);
            templateObjectLayout.ResumeLayout(false);
            templateObjectLayout.PerformLayout();
            aliasCommandLayout.ResumeLayout(false);
            aliasCommandLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)objectGrid).EndInit();
            objectOptionGroup.ResumeLayout(false);
            objectOptionGroup.PerformLayout();
            objectOptionsLayout.ResumeLayout(false);
            objectOptionsLayout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData templateTitleData;
        private GroupBox objectOptionGroup;
        private CheckBox objectIsExcluded;
        private CheckBox objectKeepOrphaned;
        private DataGridView objectGrid;
        private Controls.TextBoxData objectNameData;
        private Controls.ComboBoxData objectScopeData;
        private Button selectCommand;
        private Button addCommand;
        private CheckBox isInModelData;
        private DataGridViewComboBoxColumn objectScopeColumn;
        private DataGridViewTextBoxColumn objectNameColumn;
    }
}