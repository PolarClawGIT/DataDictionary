namespace DataDictionary.Main.Dialogs
{
    partial class SelectionDialog
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
            TableLayoutPanel selectionDialogLayout;
            TableLayoutPanel filterSelectionLayout;
            TableLayoutPanel commandLayout;
            descriptionData = new Controls.TextBoxData();
            selectionFilterGroup = new GroupBox();
            groupByScope = new RadioButton();
            groupByPath = new RadioButton();
            filterScope = new ComboBox();
            filterPath = new ComboBox();
            selectionData = new ListView();
            titleData = new Controls.TextBoxData();
            scopeData = new Controls.TextBoxData();
            pathData = new Controls.TextBoxData();
            acceptCommand = new Button();
            cancelCommand = new Button();
            bindingSource = new BindingSource(components);
            selectionDialogLayout = new TableLayoutPanel();
            filterSelectionLayout = new TableLayoutPanel();
            commandLayout = new TableLayoutPanel();
            selectionDialogLayout.SuspendLayout();
            selectionFilterGroup.SuspendLayout();
            filterSelectionLayout.SuspendLayout();
            commandLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource).BeginInit();
            SuspendLayout();
            // 
            // selectionDialogLayout
            // 
            selectionDialogLayout.ColumnCount = 1;
            selectionDialogLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            selectionDialogLayout.Controls.Add(descriptionData, 0, 5);
            selectionDialogLayout.Controls.Add(selectionFilterGroup, 0, 0);
            selectionDialogLayout.Controls.Add(selectionData, 0, 1);
            selectionDialogLayout.Controls.Add(titleData, 0, 2);
            selectionDialogLayout.Controls.Add(scopeData, 0, 3);
            selectionDialogLayout.Controls.Add(pathData, 0, 4);
            selectionDialogLayout.Controls.Add(commandLayout, 0, 6);
            selectionDialogLayout.Dock = DockStyle.Fill;
            selectionDialogLayout.Location = new Point(0, 0);
            selectionDialogLayout.Name = "selectionDialogLayout";
            selectionDialogLayout.RowCount = 7;
            selectionDialogLayout.RowStyles.Add(new RowStyle());
            selectionDialogLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            selectionDialogLayout.RowStyles.Add(new RowStyle());
            selectionDialogLayout.RowStyles.Add(new RowStyle());
            selectionDialogLayout.RowStyles.Add(new RowStyle());
            selectionDialogLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            selectionDialogLayout.RowStyles.Add(new RowStyle());
            selectionDialogLayout.Size = new Size(424, 522);
            selectionDialogLayout.TabIndex = 0;
            // 
            // descriptionData
            // 
            descriptionData.AutoSize = true;
            descriptionData.Dock = DockStyle.Fill;
            descriptionData.HeaderText = "Description";
            descriptionData.Location = new Point(3, 414);
            descriptionData.Multiline = true;
            descriptionData.Name = "descriptionData";
            descriptionData.ReadOnly = true;
            descriptionData.Size = new Size(418, 69);
            descriptionData.TabIndex = 7;
            descriptionData.WordWrap = true;
            // 
            // selectionFilterGroup
            // 
            selectionFilterGroup.AutoSize = true;
            selectionFilterGroup.Controls.Add(filterSelectionLayout);
            selectionFilterGroup.Dock = DockStyle.Fill;
            selectionFilterGroup.Location = new Point(3, 3);
            selectionFilterGroup.Name = "selectionFilterGroup";
            selectionFilterGroup.Size = new Size(418, 80);
            selectionFilterGroup.TabIndex = 0;
            selectionFilterGroup.TabStop = false;
            selectionFilterGroup.Text = "Filter";
            // 
            // filterSelectionLayout
            // 
            filterSelectionLayout.AutoSize = true;
            filterSelectionLayout.ColumnCount = 2;
            filterSelectionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            filterSelectionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            filterSelectionLayout.Controls.Add(groupByScope, 0, 0);
            filterSelectionLayout.Controls.Add(groupByPath, 1, 0);
            filterSelectionLayout.Controls.Add(filterScope, 0, 1);
            filterSelectionLayout.Controls.Add(filterPath, 1, 1);
            filterSelectionLayout.Dock = DockStyle.Fill;
            filterSelectionLayout.Location = new Point(3, 19);
            filterSelectionLayout.Name = "filterSelectionLayout";
            filterSelectionLayout.RowCount = 2;
            filterSelectionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            filterSelectionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            filterSelectionLayout.Size = new Size(412, 58);
            filterSelectionLayout.TabIndex = 0;
            // 
            // groupByScope
            // 
            groupByScope.AutoSize = true;
            groupByScope.Location = new Point(3, 3);
            groupByScope.Name = "groupByScope";
            groupByScope.Size = new Size(73, 19);
            groupByScope.TabIndex = 0;
            groupByScope.Text = "by Scope";
            groupByScope.UseVisualStyleBackColor = true;
            // 
            // groupByPath
            // 
            groupByPath.AutoSize = true;
            groupByPath.Location = new Point(209, 3);
            groupByPath.Name = "groupByPath";
            groupByPath.Size = new Size(65, 19);
            groupByPath.TabIndex = 1;
            groupByPath.Text = "by Path";
            groupByPath.UseVisualStyleBackColor = true;
            // 
            // filterScope
            // 
            filterScope.Dock = DockStyle.Fill;
            filterScope.FormattingEnabled = true;
            filterScope.Location = new Point(3, 32);
            filterScope.Name = "filterScope";
            filterScope.Size = new Size(200, 23);
            filterScope.TabIndex = 2;
            // 
            // filterPath
            // 
            filterPath.Dock = DockStyle.Fill;
            filterPath.FormattingEnabled = true;
            filterPath.Location = new Point(209, 32);
            filterPath.Name = "filterPath";
            filterPath.Size = new Size(200, 23);
            filterPath.TabIndex = 3;
            // 
            // selectionData
            // 
            selectionData.CheckBoxes = true;
            selectionData.Dock = DockStyle.Fill;
            selectionData.Location = new Point(3, 89);
            selectionData.Name = "selectionData";
            selectionData.Size = new Size(418, 169);
            selectionData.TabIndex = 1;
            selectionData.UseCompatibleStateImageBehavior = false;
            selectionData.View = View.Details;
            selectionData.ItemCheck += SelectionData_ItemCheck;
            selectionData.ItemChecked += SelectionData_ItemChecked;
            selectionData.ItemSelectionChanged += SelectionData_ItemSelectionChanged;
            // 
            // titleData
            // 
            titleData.AutoSize = true;
            titleData.Dock = DockStyle.Fill;
            titleData.HeaderText = "Title";
            titleData.Location = new Point(3, 264);
            titleData.Multiline = false;
            titleData.Name = "titleData";
            titleData.ReadOnly = true;
            titleData.Size = new Size(418, 44);
            titleData.TabIndex = 2;
            titleData.WordWrap = true;
            // 
            // scopeData
            // 
            scopeData.AutoSize = true;
            scopeData.Dock = DockStyle.Fill;
            scopeData.HeaderText = "Scope";
            scopeData.Location = new Point(3, 314);
            scopeData.Multiline = false;
            scopeData.Name = "scopeData";
            scopeData.ReadOnly = true;
            scopeData.Size = new Size(418, 44);
            scopeData.TabIndex = 3;
            scopeData.WordWrap = true;
            // 
            // pathData
            // 
            pathData.AutoSize = true;
            pathData.Dock = DockStyle.Fill;
            pathData.HeaderText = "Path";
            pathData.Location = new Point(3, 364);
            pathData.Multiline = false;
            pathData.Name = "pathData";
            pathData.ReadOnly = true;
            pathData.Size = new Size(418, 44);
            pathData.TabIndex = 4;
            pathData.WordWrap = true;
            // 
            // commandLayout
            // 
            commandLayout.AutoSize = true;
            commandLayout.ColumnCount = 3;
            commandLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            commandLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            commandLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            commandLayout.Controls.Add(acceptCommand, 0, 0);
            commandLayout.Controls.Add(cancelCommand, 2, 0);
            commandLayout.Dock = DockStyle.Fill;
            commandLayout.Location = new Point(3, 489);
            commandLayout.Name = "commandLayout";
            commandLayout.RowCount = 1;
            commandLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            commandLayout.Size = new Size(418, 30);
            commandLayout.TabIndex = 6;
            // 
            // acceptCommand
            // 
            acceptCommand.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            acceptCommand.DialogResult = DialogResult.OK;
            acceptCommand.Location = new Point(3, 4);
            acceptCommand.Name = "acceptCommand";
            acceptCommand.Size = new Size(75, 23);
            acceptCommand.TabIndex = 0;
            acceptCommand.Text = "Ok";
            acceptCommand.UseVisualStyleBackColor = true;
            // 
            // cancelCommand
            // 
            cancelCommand.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelCommand.DialogResult = DialogResult.Cancel;
            cancelCommand.Location = new Point(340, 4);
            cancelCommand.Name = "cancelCommand";
            cancelCommand.Size = new Size(75, 23);
            cancelCommand.TabIndex = 1;
            cancelCommand.Text = "cancel";
            cancelCommand.UseVisualStyleBackColor = true;
            // 
            // bindingSource
            // 
            bindingSource.AllowNew = false;
            // 
            // SelectionDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(424, 522);
            Controls.Add(selectionDialogLayout);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SelectionDialog";
            Text = "Selection Dialog";
            Load += SelectionDialog_Load;
            SizeChanged += SelectionDialog_SizeChanged;
            selectionDialogLayout.ResumeLayout(false);
            selectionDialogLayout.PerformLayout();
            selectionFilterGroup.ResumeLayout(false);
            selectionFilterGroup.PerformLayout();
            filterSelectionLayout.ResumeLayout(false);
            filterSelectionLayout.PerformLayout();
            commandLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)bindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox selectionFilterGroup;
        private RadioButton groupByScope;
        private RadioButton groupByPath;
        private ComboBox filterScope;
        private ComboBox filterPath;
        private ListView selectionData;
        private Controls.TextBoxData titleData;
        private Controls.TextBoxData scopeData;
        private Controls.TextBoxData pathData;
        private Button acceptCommand;
        private Button cancelCommand;
        private Controls.TextBoxData descriptionData;
        private BindingSource bindingSource;
    }
}