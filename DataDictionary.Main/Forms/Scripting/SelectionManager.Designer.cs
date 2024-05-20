namespace DataDictionary.Main.Forms.Scripting
{
    partial class SelectionManager
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
            TableLayoutPanel selectionManagerLayout;
            BusinessLayer.NamedScope.NamedScopePath namedScopePath1 = new BusinessLayer.NamedScope.NamedScopePath();
            selectionTitleData = new Controls.TextBoxData();
            selectionDescriptionData = new Controls.TextBoxData();
            schemaData = new Controls.ComboBoxData();
            transformData = new Controls.ComboBoxData();
            selectionItemData = new DataGridView();
            scopeNameData = new DataGridViewTextBoxColumn();
            selectionMemberData = new DataGridViewTextBoxColumn();
            namedScopeData = new Controls.NamedScopeData();
            bindingSelection = new BindingSource(components);
            bindingSelectionItem = new BindingSource(components);
            selectionToolStrip = new ContextMenuStrip(components);
            removeSelectionCommand = new ToolStripMenuItem();
            selectionManagerLayout = new TableLayoutPanel();
            selectionManagerLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)selectionItemData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSelection).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSelectionItem).BeginInit();
            selectionToolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // selectionManagerLayout
            // 
            selectionManagerLayout.ColumnCount = 1;
            selectionManagerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            selectionManagerLayout.Controls.Add(selectionTitleData, 0, 0);
            selectionManagerLayout.Controls.Add(selectionDescriptionData, 0, 1);
            selectionManagerLayout.Controls.Add(schemaData, 0, 2);
            selectionManagerLayout.Controls.Add(transformData, 0, 3);
            selectionManagerLayout.Controls.Add(selectionItemData, 0, 4);
            selectionManagerLayout.Controls.Add(namedScopeData, 0, 5);
            selectionManagerLayout.Dock = DockStyle.Fill;
            selectionManagerLayout.Location = new Point(0, 25);
            selectionManagerLayout.Name = "selectionManagerLayout";
            selectionManagerLayout.RowCount = 6;
            selectionManagerLayout.RowStyles.Add(new RowStyle());
            selectionManagerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 22.2222214F));
            selectionManagerLayout.RowStyles.Add(new RowStyle());
            selectionManagerLayout.RowStyles.Add(new RowStyle());
            selectionManagerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 22.2222214F));
            selectionManagerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 55.5555573F));
            selectionManagerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            selectionManagerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            selectionManagerLayout.Size = new Size(451, 584);
            selectionManagerLayout.TabIndex = 4;
            // 
            // selectionTitleData
            // 
            selectionTitleData.AutoSize = true;
            selectionTitleData.Dock = DockStyle.Fill;
            selectionTitleData.HeaderText = "Selection Title";
            selectionTitleData.Location = new Point(3, 3);
            selectionTitleData.Multiline = false;
            selectionTitleData.Name = "selectionTitleData";
            selectionTitleData.ReadOnly = false;
            selectionTitleData.Size = new Size(445, 44);
            selectionTitleData.TabIndex = 0;
            // 
            // selectionDescriptionData
            // 
            selectionDescriptionData.AutoSize = true;
            selectionDescriptionData.Dock = DockStyle.Fill;
            selectionDescriptionData.HeaderText = "Selection Description";
            selectionDescriptionData.Location = new Point(3, 53);
            selectionDescriptionData.Multiline = true;
            selectionDescriptionData.Name = "selectionDescriptionData";
            selectionDescriptionData.ReadOnly = false;
            selectionDescriptionData.Size = new Size(445, 90);
            selectionDescriptionData.TabIndex = 1;
            // 
            // schemaData
            // 
            schemaData.AutoSize = true;
            schemaData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            schemaData.Dock = DockStyle.Fill;
            schemaData.DropDownStyle = ComboBoxStyle.DropDownList;
            schemaData.HeaderText = "Schema";
            schemaData.Location = new Point(3, 149);
            schemaData.Name = "schemaData";
            schemaData.ReadOnly = false;
            schemaData.Size = new Size(445, 44);
            schemaData.TabIndex = 2;
            // 
            // transformData
            // 
            transformData.AutoSize = true;
            transformData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            transformData.Dock = DockStyle.Fill;
            transformData.DropDownStyle = ComboBoxStyle.DropDownList;
            transformData.HeaderText = "Transform";
            transformData.Location = new Point(3, 199);
            transformData.Name = "transformData";
            transformData.ReadOnly = false;
            transformData.Size = new Size(445, 44);
            transformData.TabIndex = 3;
            // 
            // selectionItemData
            // 
            selectionItemData.AllowUserToAddRows = false;
            selectionItemData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            selectionItemData.Columns.AddRange(new DataGridViewColumn[] { scopeNameData, selectionMemberData });
            selectionItemData.Dock = DockStyle.Fill;
            selectionItemData.Location = new Point(3, 249);
            selectionItemData.Name = "selectionItemData";
            selectionItemData.Size = new Size(445, 90);
            selectionItemData.TabIndex = 4;
            // 
            // scopeNameData
            // 
            scopeNameData.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            scopeNameData.DataPropertyName = "Scope";
            scopeNameData.FillWeight = 50F;
            scopeNameData.HeaderText = "Scope Name";
            scopeNameData.Name = "scopeNameData";
            // 
            // selectionMemberData
            // 
            selectionMemberData.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            selectionMemberData.DataPropertyName = "SelectionPath";
            selectionMemberData.HeaderText = "Selection Path";
            selectionMemberData.Name = "selectionMemberData";
            // 
            // namedScopeData
            // 
            namedScopeData.ApplyImage = Properties.Resources.NewXPath;
            namedScopeData.ApplyText = "apply";
            namedScopeData.Dock = DockStyle.Fill;
            namedScopeData.HeaderText = "Selected Item";
            namedScopeData.Location = new Point(3, 345);
            namedScopeData.Name = "namedScopeData";
            namedScopeData.ReadOnly = false;
            namedScopeData.Scope = DataLayer.ApplicationData.Scope.ScopeType.Null;
            namedScopeData.ScopePath = namedScopePath1;
            namedScopeData.Size = new Size(445, 236);
            namedScopeData.TabIndex = 5;
            namedScopeData.OnApply += NamedScopeData_OnApply;
            // 
            // bindingSelection
            // 
            bindingSelection.AddingNew += BindingSelection_AddingNew;
            // 
            // bindingSelectionItem
            // 
            bindingSelectionItem.AddingNew += BindingSelectionItem_AddingNew;
            bindingSelectionItem.CurrentChanged += BindingSelectionItem_CurrentChanged;
            // 
            // selectionToolStrip
            // 
            selectionToolStrip.Items.AddRange(new ToolStripItem[] { removeSelectionCommand });
            selectionToolStrip.Name = "selectionToolStrip";
            selectionToolStrip.Size = new Size(166, 26);
            // 
            // removeSelectionCommand
            // 
            removeSelectionCommand.Image = Properties.Resources.DeleteXPath;
            removeSelectionCommand.Name = "removeSelectionCommand";
            removeSelectionCommand.Size = new Size(165, 22);
            removeSelectionCommand.Text = "remove Selection";
            removeSelectionCommand.Click += RemoveSelectionCommand_Click;
            // 
            // SelectionManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(451, 609);
            Controls.Add(selectionManagerLayout);
            Name = "SelectionManager";
            Text = "SelectionManager";
            Load += SelectionManager_Load;
            Controls.SetChildIndex(selectionManagerLayout, 0);
            selectionManagerLayout.ResumeLayout(false);
            selectionManagerLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)selectionItemData).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSelection).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSelectionItem).EndInit();
            selectionToolStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData selectionTitleData;
        private Controls.TextBoxData selectionDescriptionData;
        private Controls.ComboBoxData schemaData;
        private Controls.ComboBoxData transformData;
        private DataGridView selectionItemData;
        private BindingSource bindingSelection;
        private BindingSource bindingSelectionItem;
        private ContextMenuStrip selectionToolStrip;
        private Controls.NamedScopeData namedScopeData;
        private ToolStripMenuItem removeSelectionCommand;
        private DataGridViewTextBoxColumn scopeNameData;
        private DataGridViewTextBoxColumn selectionMemberData;
    }
}