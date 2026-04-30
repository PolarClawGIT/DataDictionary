namespace DataDictionary.Main.Forms.Scripting
{
    partial class SchemaNode
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
            TableLayoutPanel schemaNodeLayout;
            TableLayoutPanel nodeValueLayout;
            TableLayoutPanel nodeParentLayout;
            nodeTabs = new TabControl();
            nodeValueTab = new TabPage();
            nodeNameData = new DataDictionary.Main.Controls.TextBoxData();
            nodeNameSync = new CheckBox();
            nodeRenderOrderData = new DataDictionary.Main.Controls.TextBoxData();
            nodeRenderAsData = new DataDictionary.Main.Controls.ComboBoxData();
            nodeObjectScopeData = new DataDictionary.Main.Controls.ComboBoxData();
            nodeObjectPropertyData = new DataDictionary.Main.Controls.ComboBoxData();
            nodeModelPropertyData = new DataDictionary.Main.Controls.ComboBoxData();
            nodeFixedValueData = new DataDictionary.Main.Controls.TextBoxData();
            isFixedValueData = new CheckBox();
            isObjectValueData = new CheckBox();
            isObjectProperty = new CheckBox();
            nodeParentTab = new TabPage();
            nodeOwnershipData = new DataGridView();
            nodeParentColumn = new DataGridViewComboBoxColumn();
            nodeParentSelect = new DataDictionary.Main.Controls.ComboBoxData();
            addNodeParentCommand = new Button();
            templateTitleData = new DataDictionary.Main.Controls.TextBoxData();
            nodeTreeView = new TreeView();
            schemaTitleData = new DataDictionary.Main.Controls.TextBoxData();
            bindingTemplate = new BindingSource(components);
            bindingSchema = new BindingSource(components);
            bindingNode = new BindingSource(components);
            bindingNodeOwner = new BindingSource(components);
            schemaNodeLayout = new TableLayoutPanel();
            nodeValueLayout = new TableLayoutPanel();
            nodeParentLayout = new TableLayoutPanel();
            schemaNodeLayout.SuspendLayout();
            nodeTabs.SuspendLayout();
            nodeValueTab.SuspendLayout();
            nodeValueLayout.SuspendLayout();
            nodeParentTab.SuspendLayout();
            nodeParentLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nodeOwnershipData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSchema).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingNode).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingNodeOwner).BeginInit();
            SuspendLayout();
            // 
            // schemaNodeLayout
            // 
            schemaNodeLayout.ColumnCount = 3;
            schemaNodeLayout.ColumnStyles.Add(new ColumnStyle());
            schemaNodeLayout.ColumnStyles.Add(new ColumnStyle());
            schemaNodeLayout.ColumnStyles.Add(new ColumnStyle());
            schemaNodeLayout.Controls.Add(nodeTabs, 2, 2);
            schemaNodeLayout.Controls.Add(templateTitleData, 0, 0);
            schemaNodeLayout.Controls.Add(nodeTreeView, 0, 2);
            schemaNodeLayout.Controls.Add(schemaTitleData, 0, 1);
            schemaNodeLayout.Dock = DockStyle.Fill;
            schemaNodeLayout.Location = new Point(0, 25);
            schemaNodeLayout.Name = "schemaNodeLayout";
            schemaNodeLayout.RowCount = 3;
            schemaNodeLayout.RowStyles.Add(new RowStyle());
            schemaNodeLayout.RowStyles.Add(new RowStyle());
            schemaNodeLayout.RowStyles.Add(new RowStyle());
            schemaNodeLayout.Size = new Size(636, 652);
            schemaNodeLayout.TabIndex = 4;
            // 
            // nodeTabs
            // 
            nodeTabs.Controls.Add(nodeValueTab);
            nodeTabs.Controls.Add(nodeParentTab);
            nodeTabs.Dock = DockStyle.Fill;
            nodeTabs.Location = new Point(182, 103);
            nodeTabs.Name = "nodeTabs";
            nodeTabs.SelectedIndex = 0;
            nodeTabs.Size = new Size(451, 546);
            nodeTabs.TabIndex = 5;
            // 
            // nodeValueTab
            // 
            nodeValueTab.BackColor = SystemColors.Control;
            nodeValueTab.Controls.Add(nodeValueLayout);
            nodeValueTab.Location = new Point(4, 24);
            nodeValueTab.Name = "nodeValueTab";
            nodeValueTab.Padding = new Padding(3);
            nodeValueTab.Size = new Size(443, 518);
            nodeValueTab.TabIndex = 0;
            nodeValueTab.Text = "Node Value";
            // 
            // nodeValueLayout
            // 
            nodeValueLayout.AutoSize = true;
            nodeValueLayout.ColumnCount = 3;
            nodeValueLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            nodeValueLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            nodeValueLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            nodeValueLayout.Controls.Add(nodeNameData, 0, 0);
            nodeValueLayout.Controls.Add(nodeNameSync, 2, 0);
            nodeValueLayout.Controls.Add(nodeRenderOrderData, 2, 1);
            nodeValueLayout.Controls.Add(nodeRenderAsData, 0, 1);
            nodeValueLayout.Controls.Add(nodeObjectScopeData, 0, 2);
            nodeValueLayout.Controls.Add(nodeObjectPropertyData, 1, 3);
            nodeValueLayout.Controls.Add(nodeModelPropertyData, 1, 4);
            nodeValueLayout.Controls.Add(nodeFixedValueData, 0, 5);
            nodeValueLayout.Controls.Add(isFixedValueData, 2, 2);
            nodeValueLayout.Controls.Add(isObjectValueData, 0, 3);
            nodeValueLayout.Controls.Add(isObjectProperty, 0, 4);
            nodeValueLayout.Dock = DockStyle.Fill;
            nodeValueLayout.Location = new Point(3, 3);
            nodeValueLayout.Name = "nodeValueLayout";
            nodeValueLayout.RowCount = 6;
            nodeValueLayout.RowStyles.Add(new RowStyle());
            nodeValueLayout.RowStyles.Add(new RowStyle());
            nodeValueLayout.RowStyles.Add(new RowStyle());
            nodeValueLayout.RowStyles.Add(new RowStyle());
            nodeValueLayout.RowStyles.Add(new RowStyle());
            nodeValueLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            nodeValueLayout.Size = new Size(437, 512);
            nodeValueLayout.TabIndex = 0;
            // 
            // nodeNameData
            // 
            nodeNameData.AutoSize = true;
            nodeValueLayout.SetColumnSpan(nodeNameData, 2);
            nodeNameData.Dock = DockStyle.Fill;
            nodeNameData.HeaderText = "Node Name";
            nodeNameData.Location = new Point(3, 3);
            nodeNameData.Multiline = false;
            nodeNameData.Name = "nodeNameData";
            nodeNameData.ReadOnly = false;
            nodeNameData.Size = new Size(284, 44);
            nodeNameData.TabIndex = 15;
            nodeNameData.WordWrap = true;
            // 
            // nodeNameSync
            // 
            nodeNameSync.AutoSize = true;
            nodeNameSync.Location = new Point(293, 3);
            nodeNameSync.Name = "nodeNameSync";
            nodeNameSync.Size = new Size(86, 19);
            nodeNameSync.TabIndex = 16;
            nodeNameSync.Text = "Sync Name";
            nodeNameSync.UseVisualStyleBackColor = true;
            // 
            // nodeRenderOrderData
            // 
            nodeRenderOrderData.AutoSize = true;
            nodeRenderOrderData.HeaderText = "Render Order";
            nodeRenderOrderData.Location = new Point(293, 53);
            nodeRenderOrderData.Multiline = false;
            nodeRenderOrderData.Name = "nodeRenderOrderData";
            nodeRenderOrderData.ReadOnly = false;
            nodeRenderOrderData.Size = new Size(120, 44);
            nodeRenderOrderData.TabIndex = 5;
            nodeRenderOrderData.WordWrap = true;
            // 
            // nodeRenderAsData
            // 
            nodeRenderAsData.AutoSize = true;
            nodeRenderAsData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            nodeValueLayout.SetColumnSpan(nodeRenderAsData, 2);
            nodeRenderAsData.Dock = DockStyle.Fill;
            nodeRenderAsData.DropDownStyle = ComboBoxStyle.DropDownList;
            nodeRenderAsData.HeaderText = "Render Value As";
            nodeRenderAsData.Location = new Point(3, 53);
            nodeRenderAsData.Name = "nodeRenderAsData";
            nodeRenderAsData.ReadOnly = false;
            nodeRenderAsData.Size = new Size(284, 46);
            nodeRenderAsData.TabIndex = 4;
            // 
            // nodeObjectScopeData
            // 
            nodeObjectScopeData.AutoSize = true;
            nodeObjectScopeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            nodeValueLayout.SetColumnSpan(nodeObjectScopeData, 2);
            nodeObjectScopeData.Dock = DockStyle.Fill;
            nodeObjectScopeData.DropDownStyle = ComboBoxStyle.DropDownList;
            nodeObjectScopeData.HeaderText = "Object Type";
            nodeObjectScopeData.Location = new Point(3, 105);
            nodeObjectScopeData.Name = "nodeObjectScopeData";
            nodeObjectScopeData.ReadOnly = false;
            nodeObjectScopeData.Size = new Size(284, 46);
            nodeObjectScopeData.TabIndex = 7;
            // 
            // nodeObjectPropertyData
            // 
            nodeObjectPropertyData.AutoSize = true;
            nodeObjectPropertyData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            nodeValueLayout.SetColumnSpan(nodeObjectPropertyData, 2);
            nodeObjectPropertyData.Dock = DockStyle.Fill;
            nodeObjectPropertyData.DropDownStyle = ComboBoxStyle.DropDown;
            nodeObjectPropertyData.HeaderText = "Object Value of";
            nodeObjectPropertyData.Location = new Point(148, 157);
            nodeObjectPropertyData.Name = "nodeObjectPropertyData";
            nodeObjectPropertyData.ReadOnly = false;
            nodeObjectPropertyData.Size = new Size(286, 46);
            nodeObjectPropertyData.TabIndex = 14;
            // 
            // nodeModelPropertyData
            // 
            nodeModelPropertyData.AutoSize = true;
            nodeModelPropertyData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            nodeValueLayout.SetColumnSpan(nodeModelPropertyData, 2);
            nodeModelPropertyData.Dock = DockStyle.Fill;
            nodeModelPropertyData.DropDownStyle = ComboBoxStyle.DropDownList;
            nodeModelPropertyData.HeaderText = "Object Property of";
            nodeModelPropertyData.Location = new Point(148, 209);
            nodeModelPropertyData.Name = "nodeModelPropertyData";
            nodeModelPropertyData.ReadOnly = false;
            nodeModelPropertyData.Size = new Size(286, 46);
            nodeModelPropertyData.TabIndex = 11;
            // 
            // nodeFixedValueData
            // 
            nodeFixedValueData.AutoSize = true;
            nodeValueLayout.SetColumnSpan(nodeFixedValueData, 3);
            nodeFixedValueData.Dock = DockStyle.Fill;
            nodeFixedValueData.HeaderText = "Fixed or Default Value";
            nodeFixedValueData.Location = new Point(3, 261);
            nodeFixedValueData.Multiline = true;
            nodeFixedValueData.Name = "nodeFixedValueData";
            nodeFixedValueData.ReadOnly = false;
            nodeFixedValueData.Size = new Size(431, 248);
            nodeFixedValueData.TabIndex = 13;
            nodeFixedValueData.WordWrap = false;
            // 
            // isFixedValueData
            // 
            isFixedValueData.AutoSize = true;
            isFixedValueData.Enabled = false;
            isFixedValueData.Location = new Point(293, 105);
            isFixedValueData.Name = "isFixedValueData";
            isFixedValueData.Size = new Size(95, 19);
            isFixedValueData.TabIndex = 17;
            isFixedValueData.Text = "is Fixed Value";
            isFixedValueData.UseVisualStyleBackColor = true;
            // 
            // isObjectValueData
            // 
            isObjectValueData.AutoSize = true;
            isObjectValueData.Enabled = false;
            isObjectValueData.Location = new Point(3, 157);
            isObjectValueData.Name = "isObjectValueData";
            isObjectValueData.Size = new Size(103, 19);
            isObjectValueData.TabIndex = 18;
            isObjectValueData.Text = "is Object Value";
            isObjectValueData.UseVisualStyleBackColor = true;
            // 
            // isObjectProperty
            // 
            isObjectProperty.AutoSize = true;
            isObjectProperty.Enabled = false;
            isObjectProperty.Location = new Point(3, 209);
            isObjectProperty.Name = "isObjectProperty";
            isObjectProperty.Size = new Size(120, 19);
            isObjectProperty.TabIndex = 19;
            isObjectProperty.Text = "is Object Property";
            isObjectProperty.UseVisualStyleBackColor = true;
            // 
            // nodeParentTab
            // 
            nodeParentTab.BackColor = SystemColors.Control;
            nodeParentTab.Controls.Add(nodeParentLayout);
            nodeParentTab.Location = new Point(4, 24);
            nodeParentTab.Name = "nodeParentTab";
            nodeParentTab.Padding = new Padding(3);
            nodeParentTab.Size = new Size(192, 72);
            nodeParentTab.TabIndex = 1;
            nodeParentTab.Text = "Node Parents";
            // 
            // nodeParentLayout
            // 
            nodeParentLayout.AutoSize = true;
            nodeParentLayout.ColumnCount = 2;
            nodeParentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            nodeParentLayout.ColumnStyles.Add(new ColumnStyle());
            nodeParentLayout.Controls.Add(nodeOwnershipData, 0, 0);
            nodeParentLayout.Controls.Add(nodeParentSelect, 0, 1);
            nodeParentLayout.Controls.Add(addNodeParentCommand, 1, 1);
            nodeParentLayout.Dock = DockStyle.Fill;
            nodeParentLayout.Location = new Point(3, 3);
            nodeParentLayout.Name = "nodeParentLayout";
            nodeParentLayout.RowCount = 2;
            nodeParentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            nodeParentLayout.RowStyles.Add(new RowStyle());
            nodeParentLayout.Size = new Size(186, 66);
            nodeParentLayout.TabIndex = 0;
            // 
            // nodeOwnershipData
            // 
            nodeOwnershipData.AllowUserToAddRows = false;
            nodeOwnershipData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            nodeOwnershipData.Columns.AddRange(new DataGridViewColumn[] { nodeParentColumn });
            nodeParentLayout.SetColumnSpan(nodeOwnershipData, 2);
            nodeOwnershipData.Dock = DockStyle.Fill;
            nodeOwnershipData.Location = new Point(3, 3);
            nodeOwnershipData.Name = "nodeOwnershipData";
            nodeOwnershipData.ReadOnly = true;
            nodeOwnershipData.Size = new Size(180, 8);
            nodeOwnershipData.TabIndex = 1;
            // 
            // nodeParentColumn
            // 
            nodeParentColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            nodeParentColumn.HeaderText = "Parent Node";
            nodeParentColumn.Name = "nodeParentColumn";
            nodeParentColumn.ReadOnly = true;
            // 
            // nodeParentSelect
            // 
            nodeParentSelect.AutoSize = true;
            nodeParentSelect.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            nodeParentSelect.Dock = DockStyle.Fill;
            nodeParentSelect.DropDownStyle = ComboBoxStyle.DropDown;
            nodeParentSelect.HeaderText = "Parent Node";
            nodeParentSelect.Location = new Point(3, 17);
            nodeParentSelect.Name = "nodeParentSelect";
            nodeParentSelect.ReadOnly = false;
            nodeParentSelect.Size = new Size(99, 46);
            nodeParentSelect.TabIndex = 4;
            // 
            // addNodeParentCommand
            // 
            addNodeParentCommand.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            addNodeParentCommand.Location = new Point(108, 40);
            addNodeParentCommand.Name = "addNodeParentCommand";
            addNodeParentCommand.Size = new Size(75, 23);
            addNodeParentCommand.TabIndex = 5;
            addNodeParentCommand.Text = "Add";
            addNodeParentCommand.TextImageRelation = TextImageRelation.ImageBeforeText;
            addNodeParentCommand.UseVisualStyleBackColor = true;
            // 
            // templateTitleData
            // 
            templateTitleData.AutoSize = true;
            schemaNodeLayout.SetColumnSpan(templateTitleData, 3);
            templateTitleData.Dock = DockStyle.Fill;
            templateTitleData.HeaderText = "Template";
            templateTitleData.Location = new Point(3, 3);
            templateTitleData.Multiline = false;
            templateTitleData.Name = "templateTitleData";
            templateTitleData.ReadOnly = true;
            templateTitleData.Size = new Size(630, 44);
            templateTitleData.TabIndex = 0;
            templateTitleData.WordWrap = true;
            // 
            // nodeTreeView
            // 
            nodeTreeView.Dock = DockStyle.Fill;
            nodeTreeView.Location = new Point(3, 103);
            nodeTreeView.Name = "nodeTreeView";
            nodeTreeView.Size = new Size(173, 546);
            nodeTreeView.TabIndex = 4;
            // 
            // schemaTitleData
            // 
            schemaTitleData.AutoSize = true;
            schemaNodeLayout.SetColumnSpan(schemaTitleData, 3);
            schemaTitleData.Dock = DockStyle.Fill;
            schemaTitleData.HeaderText = "Schema";
            schemaTitleData.Location = new Point(3, 53);
            schemaTitleData.Multiline = false;
            schemaTitleData.Name = "schemaTitleData";
            schemaTitleData.ReadOnly = true;
            schemaTitleData.Size = new Size(630, 44);
            schemaTitleData.TabIndex = 1;
            schemaTitleData.WordWrap = true;
            // 
            // SchemaNode
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(636, 677);
            Controls.Add(schemaNodeLayout);
            Name = "SchemaNode";
            Text = "SchemaNode";
            Load += SchemaNode_Load;
            Controls.SetChildIndex(schemaNodeLayout, 0);
            schemaNodeLayout.ResumeLayout(false);
            schemaNodeLayout.PerformLayout();
            nodeTabs.ResumeLayout(false);
            nodeValueTab.ResumeLayout(false);
            nodeValueTab.PerformLayout();
            nodeValueLayout.ResumeLayout(false);
            nodeValueLayout.PerformLayout();
            nodeParentTab.ResumeLayout(false);
            nodeParentTab.PerformLayout();
            nodeParentLayout.ResumeLayout(false);
            nodeParentLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nodeOwnershipData).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSchema).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingNode).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingNodeOwner).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData templateTitleData;
        private Controls.TextBoxData schemaTitleData;
        private TreeView nodeTreeView;
        private Controls.ComboBoxData nodeRenderAsData;
        private Controls.TextBoxData nodeRenderOrderData;
        private DataGridView nodeOwnershipData;
        private DataGridViewComboBoxColumn nodeParentColumn;
        private Controls.ComboBoxData nodeParentSelect;
        private Button addNodeParentCommand;
        private Controls.ComboBoxData nodeModelPropertyData;
        private Controls.ComboBoxData nodeObjectScopeData;
        private Controls.TextBoxData nodeFixedValueData;
        private Controls.ComboBoxData nodeObjectPropertyData;
        private Controls.TextBoxData nodeNameData;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel nodeValueLayout;
        private CheckBox nodeNameSync;
        private TabControl nodeTabs;
        private TabPage nodeValueTab;
        private TabPage nodeParentTab;
        private CheckBox isFixedValueData;
        private CheckBox isObjectValueData;
        private CheckBox isObjectProperty;
        private BindingSource bindingTemplate;
        private BindingSource bindingSchema;
        private BindingSource bindingNode;
        private BindingSource bindingNodeOwner;
    }
}