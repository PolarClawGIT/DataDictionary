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
            components = new System.ComponentModel.Container();
            TableLayoutPanel templateObjectLayout;
            GroupBox objectOptionGroup;
            TableLayoutPanel objectOptionsLayout;
            Label excludeLabel;
            Label orphanedLabel;
            objectData = new DataGridView();
            objectScopeColumn = new DataGridViewComboBoxColumn();
            objectNameColumn = new DataGridViewTextBoxColumn();
            objectNameData = new DataDictionary.Main.Controls.TextBoxData();
            objectScopeData = new DataDictionary.Main.Controls.ComboBoxData();
            templateTitleData = new DataDictionary.Main.Controls.TextBoxData();
            objectIsExcluded = new CheckBox();
            objectKeepOrphaned = new CheckBox();
            isInModelData = new CheckBox();
            bindingTemplate = new BindingSource(components);
            bindingObject = new BindingSource(components);
            templateObjectLayout = new TableLayoutPanel();
            objectOptionGroup = new GroupBox();
            objectOptionsLayout = new TableLayoutPanel();
            excludeLabel = new Label();
            orphanedLabel = new Label();
            templateObjectLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)objectData).BeginInit();
            objectOptionGroup.SuspendLayout();
            objectOptionsLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingObject).BeginInit();
            SuspendLayout();
            // 
            // templateObjectLayout
            // 
            templateObjectLayout.ColumnCount = 2;
            templateObjectLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            templateObjectLayout.ColumnStyles.Add(new ColumnStyle());
            templateObjectLayout.Controls.Add(objectData, 0, 1);
            templateObjectLayout.Controls.Add(objectNameData, 0, 3);
            templateObjectLayout.Controls.Add(objectScopeData, 0, 2);
            templateObjectLayout.Controls.Add(templateTitleData, 0, 0);
            templateObjectLayout.Controls.Add(objectOptionGroup, 0, 4);
            templateObjectLayout.Controls.Add(isInModelData, 1, 3);
            templateObjectLayout.Dock = DockStyle.Fill;
            templateObjectLayout.Location = new Point(0, 25);
            templateObjectLayout.Name = "templateObjectLayout";
            templateObjectLayout.RowCount = 5;
            templateObjectLayout.RowStyles.Add(new RowStyle());
            templateObjectLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            templateObjectLayout.RowStyles.Add(new RowStyle());
            templateObjectLayout.RowStyles.Add(new RowStyle());
            templateObjectLayout.RowStyles.Add(new RowStyle());
            templateObjectLayout.Size = new Size(548, 482);
            templateObjectLayout.TabIndex = 4;
            // 
            // objectData
            // 
            objectData.AllowUserToAddRows = false;
            objectData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            objectData.Columns.AddRange(new DataGridViewColumn[] { objectScopeColumn, objectNameColumn });
            templateObjectLayout.SetColumnSpan(objectData, 2);
            objectData.Dock = DockStyle.Fill;
            objectData.Location = new Point(3, 53);
            objectData.Name = "objectData";
            objectData.ReadOnly = true;
            objectData.Size = new Size(542, 231);
            objectData.TabIndex = 9;
            // 
            // objectScopeColumn
            // 
            objectScopeColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            objectScopeColumn.DataPropertyName = "ObjectScope";
            objectScopeColumn.FillWeight = 40F;
            objectScopeColumn.HeaderText = "Object Scope";
            objectScopeColumn.Name = "objectScopeColumn";
            objectScopeColumn.ReadOnly = true;
            // 
            // objectNameColumn
            // 
            objectNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            objectNameColumn.DataPropertyName = "ObjectName";
            objectNameColumn.FillWeight = 60F;
            objectNameColumn.HeaderText = "Object Name";
            objectNameColumn.Name = "objectNameColumn";
            objectNameColumn.ReadOnly = true;
            // 
            // objectNameData
            // 
            objectNameData.AutoSize = true;
            objectNameData.Dock = DockStyle.Fill;
            objectNameData.HeaderText = "Object Name";
            objectNameData.Location = new Point(3, 342);
            objectNameData.Multiline = false;
            objectNameData.Name = "objectNameData";
            objectNameData.ReadOnly = false;
            objectNameData.Size = new Size(463, 44);
            objectNameData.TabIndex = 2;
            objectNameData.WordWrap = true;
            objectNameData.Validating += ObjectNameData_Validating;
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
            objectScopeData.ReadOnly = false;
            objectScopeData.Size = new Size(463, 46);
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
            templateTitleData.Size = new Size(542, 44);
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
            objectOptionGroup.Size = new Size(542, 87);
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
            objectOptionsLayout.Size = new Size(536, 65);
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
            objectKeepOrphaned.Location = new Point(271, 3);
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
            excludeLabel.Size = new Size(262, 40);
            excludeLabel.TabIndex = 2;
            excludeLabel.Text = "Exclude this Object when Documents are auto-generated.";
            // 
            // orphanedLabel
            // 
            orphanedLabel.Dock = DockStyle.Fill;
            orphanedLabel.Location = new Point(271, 25);
            orphanedLabel.Name = "orphanedLabel";
            orphanedLabel.Size = new Size(262, 40);
            orphanedLabel.TabIndex = 3;
            orphanedLabel.Text = "Keep Orphaned Document for Object not in the Model";
            // 
            // isInModelData
            // 
            isInModelData.AutoSize = true;
            isInModelData.Enabled = false;
            isInModelData.Location = new Point(472, 342);
            isInModelData.Name = "isInModelData";
            isInModelData.Size = new Size(73, 19);
            isInModelData.TabIndex = 0;
            isInModelData.Text = "in Model";
            isInModelData.UseVisualStyleBackColor = true;
            // 
            // bindingObject
            // 
            bindingObject.CurrentChanged += BindingObject_CurrentChanged;
            // 
            // TemplateObject
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(548, 507);
            Controls.Add(templateObjectLayout);
            Name = "TemplateObject";
            Text = "TemplateObject";
            Load += TemplateObject_Load;
            Controls.SetChildIndex(templateObjectLayout, 0);
            templateObjectLayout.ResumeLayout(false);
            templateObjectLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)objectData).EndInit();
            objectOptionGroup.ResumeLayout(false);
            objectOptionGroup.PerformLayout();
            objectOptionsLayout.ResumeLayout(false);
            objectOptionsLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingObject).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData templateTitleData;
        private GroupBox objectOptionGroup;
        private CheckBox objectIsExcluded;
        private CheckBox objectKeepOrphaned;
        private Controls.TextBoxData objectNameData;
        private Controls.ComboBoxData objectScopeData;
        private CheckBox isInModelData;
        private BindingSource bindingTemplate;
        private BindingSource bindingObject;
        private DataGridView objectData;
        private DataGridViewComboBoxColumn objectScopeColumn;
        private DataGridViewTextBoxColumn objectNameColumn;
    }
}