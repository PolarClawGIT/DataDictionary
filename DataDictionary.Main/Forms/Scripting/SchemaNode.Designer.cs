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
            TableLayoutPanel objectValueLayout;
            TableLayoutPanel nodeParentLayout;
            groupBox1 = new GroupBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            isObjectValueData = new CheckBox();
            isObjectProperty = new CheckBox();
            isFixedValueData = new CheckBox();
            tableLayoutPanel4 = new TableLayoutPanel();
            nodeNameData = new DataDictionary.Main.Controls.TextBoxData();
            nodeRenderAsData = new DataDictionary.Main.Controls.ComboBoxData();
            nodeRenderOrderData = new DataDictionary.Main.Controls.TextBoxData();
            checkBox1 = new CheckBox();
            templateTitleData = new DataDictionary.Main.Controls.TextBoxData();
            schemaTitleData = new DataDictionary.Main.Controls.TextBoxData();
            nodeTreeView = new TreeView();
            nodeTabs = new TabControl();
            nodeObjectValueTab = new TabPage();
            nodeObjectScopeData = new DataDictionary.Main.Controls.ComboBoxData();
            nodeObjectPropertyData = new DataDictionary.Main.Controls.ComboBoxData();
            nodePropertyValueTab = new TabPage();
            tableLayoutPanel1 = new TableLayoutPanel();
            nodePropertyScopeData = new DataDictionary.Main.Controls.ComboBoxData();
            nodeModelPropertyData = new DataDictionary.Main.Controls.ComboBoxData();
            nodeFixedValueTab = new TabPage();
            tableLayoutPanel2 = new TableLayoutPanel();
            nodeFixedValueData = new DataDictionary.Main.Controls.TextBoxData();
            nodeParentTab = new TabPage();
            nodeOwnershipData = new DataGridView();
            nodeParentColumn = new DataGridViewComboBoxColumn();
            nodeParentSelect = new DataDictionary.Main.Controls.ComboBoxData();
            addNodeParentCommand = new Button();
            bindingTemplate = new BindingSource(components);
            bindingSchema = new BindingSource(components);
            bindingNode = new BindingSource(components);
            bindingNodeOwner = new BindingSource(components);
            schemaNodeLayout = new TableLayoutPanel();
            objectValueLayout = new TableLayoutPanel();
            nodeParentLayout = new TableLayoutPanel();
            schemaNodeLayout.SuspendLayout();
            groupBox1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            nodeTabs.SuspendLayout();
            nodeObjectValueTab.SuspendLayout();
            objectValueLayout.SuspendLayout();
            nodePropertyValueTab.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            nodeFixedValueTab.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
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
            schemaNodeLayout.Controls.Add(groupBox1, 2, 3);
            schemaNodeLayout.Controls.Add(tableLayoutPanel4, 2, 2);
            schemaNodeLayout.Controls.Add(templateTitleData, 0, 0);
            schemaNodeLayout.Controls.Add(schemaTitleData, 0, 1);
            schemaNodeLayout.Controls.Add(nodeTreeView, 0, 2);
            schemaNodeLayout.Controls.Add(nodeTabs, 2, 4);
            schemaNodeLayout.Dock = DockStyle.Fill;
            schemaNodeLayout.Location = new Point(0, 25);
            schemaNodeLayout.Name = "schemaNodeLayout";
            schemaNodeLayout.RowCount = 5;
            schemaNodeLayout.RowStyles.Add(new RowStyle());
            schemaNodeLayout.RowStyles.Add(new RowStyle());
            schemaNodeLayout.RowStyles.Add(new RowStyle());
            schemaNodeLayout.RowStyles.Add(new RowStyle());
            schemaNodeLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            schemaNodeLayout.Size = new Size(674, 652);
            schemaNodeLayout.TabIndex = 4;
            // 
            // groupBox1
            // 
            groupBox1.AutoSize = true;
            groupBox1.Controls.Add(tableLayoutPanel3);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(182, 213);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(489, 47);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Value Source";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.AutoSize = true;
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.Controls.Add(isObjectValueData, 0, 0);
            tableLayoutPanel3.Controls.Add(isObjectProperty, 1, 0);
            tableLayoutPanel3.Controls.Add(isFixedValueData, 2, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 19);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.Size = new Size(483, 25);
            tableLayoutPanel3.TabIndex = 5;
            // 
            // isObjectValueData
            // 
            isObjectValueData.AutoSize = true;
            isObjectValueData.Enabled = false;
            isObjectValueData.Location = new Point(3, 3);
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
            isObjectProperty.Location = new Point(112, 3);
            isObjectProperty.Name = "isObjectProperty";
            isObjectProperty.Size = new Size(113, 19);
            isObjectProperty.TabIndex = 19;
            isObjectProperty.Text = "is Property Value";
            isObjectProperty.UseVisualStyleBackColor = true;
            // 
            // isFixedValueData
            // 
            isFixedValueData.AutoSize = true;
            isFixedValueData.Enabled = false;
            isFixedValueData.Location = new Point(231, 3);
            isFixedValueData.Name = "isFixedValueData";
            isFixedValueData.Size = new Size(95, 19);
            isFixedValueData.TabIndex = 17;
            isFixedValueData.Text = "is Fixed Value";
            isFixedValueData.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.AutoSize = true;
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.6666641F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel4.Controls.Add(nodeNameData, 0, 0);
            tableLayoutPanel4.Controls.Add(nodeRenderAsData, 0, 1);
            tableLayoutPanel4.Controls.Add(nodeRenderOrderData, 1, 1);
            tableLayoutPanel4.Controls.Add(checkBox1, 1, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(182, 103);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel4.Size = new Size(489, 104);
            tableLayoutPanel4.TabIndex = 6;
            // 
            // nodeNameData
            // 
            nodeNameData.AutoSize = true;
            nodeNameData.Dock = DockStyle.Fill;
            nodeNameData.HeaderText = "Node Name";
            nodeNameData.Location = new Point(3, 3);
            nodeNameData.Multiline = false;
            nodeNameData.Name = "nodeNameData";
            nodeNameData.ReadOnly = false;
            nodeNameData.Size = new Size(319, 46);
            nodeNameData.TabIndex = 15;
            nodeNameData.WordWrap = true;
            // 
            // nodeRenderAsData
            // 
            nodeRenderAsData.AutoSize = true;
            nodeRenderAsData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            nodeRenderAsData.Dock = DockStyle.Fill;
            nodeRenderAsData.DropDownStyle = ComboBoxStyle.DropDownList;
            nodeRenderAsData.HeaderText = "Render Value As";
            nodeRenderAsData.Location = new Point(3, 55);
            nodeRenderAsData.Name = "nodeRenderAsData";
            nodeRenderAsData.ReadOnly = false;
            nodeRenderAsData.Size = new Size(319, 46);
            nodeRenderAsData.TabIndex = 4;
            // 
            // nodeRenderOrderData
            // 
            nodeRenderOrderData.AutoSize = true;
            nodeRenderOrderData.Dock = DockStyle.Fill;
            nodeRenderOrderData.HeaderText = "Render Order";
            nodeRenderOrderData.Location = new Point(328, 55);
            nodeRenderOrderData.Multiline = false;
            nodeRenderOrderData.Name = "nodeRenderOrderData";
            nodeRenderOrderData.ReadOnly = false;
            nodeRenderOrderData.Size = new Size(158, 46);
            nodeRenderOrderData.TabIndex = 5;
            nodeRenderOrderData.WordWrap = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(328, 3);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(138, 19);
            checkBox1.TabIndex = 16;
            checkBox1.Text = "Override Node Name";
            checkBox1.UseVisualStyleBackColor = true;
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
            templateTitleData.Size = new Size(668, 44);
            templateTitleData.TabIndex = 0;
            templateTitleData.WordWrap = true;
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
            schemaTitleData.Size = new Size(668, 44);
            schemaTitleData.TabIndex = 1;
            schemaTitleData.WordWrap = true;
            // 
            // nodeTreeView
            // 
            nodeTreeView.Dock = DockStyle.Fill;
            nodeTreeView.Location = new Point(3, 103);
            nodeTreeView.Name = "nodeTreeView";
            schemaNodeLayout.SetRowSpan(nodeTreeView, 3);
            nodeTreeView.Size = new Size(173, 546);
            nodeTreeView.TabIndex = 4;
            // 
            // nodeTabs
            // 
            nodeTabs.Controls.Add(nodeObjectValueTab);
            nodeTabs.Controls.Add(nodePropertyValueTab);
            nodeTabs.Controls.Add(nodeFixedValueTab);
            nodeTabs.Controls.Add(nodeParentTab);
            nodeTabs.Dock = DockStyle.Fill;
            nodeTabs.Location = new Point(182, 266);
            nodeTabs.Name = "nodeTabs";
            nodeTabs.SelectedIndex = 0;
            nodeTabs.Size = new Size(489, 383);
            nodeTabs.TabIndex = 5;
            // 
            // nodeObjectValueTab
            // 
            nodeObjectValueTab.BackColor = SystemColors.Control;
            nodeObjectValueTab.Controls.Add(objectValueLayout);
            nodeObjectValueTab.Location = new Point(4, 24);
            nodeObjectValueTab.Name = "nodeObjectValueTab";
            nodeObjectValueTab.Padding = new Padding(3);
            nodeObjectValueTab.Size = new Size(481, 355);
            nodeObjectValueTab.TabIndex = 0;
            nodeObjectValueTab.Text = "Object Value";
            // 
            // objectValueLayout
            // 
            objectValueLayout.ColumnCount = 1;
            objectValueLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            objectValueLayout.Controls.Add(nodeObjectScopeData, 0, 0);
            objectValueLayout.Controls.Add(nodeObjectPropertyData, 0, 1);
            objectValueLayout.Dock = DockStyle.Fill;
            objectValueLayout.Location = new Point(3, 3);
            objectValueLayout.Name = "objectValueLayout";
            objectValueLayout.RowCount = 2;
            objectValueLayout.RowStyles.Add(new RowStyle());
            objectValueLayout.RowStyles.Add(new RowStyle());
            objectValueLayout.Size = new Size(475, 349);
            objectValueLayout.TabIndex = 6;
            // 
            // nodeObjectScopeData
            // 
            nodeObjectScopeData.AutoSize = true;
            nodeObjectScopeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            nodeObjectScopeData.Dock = DockStyle.Fill;
            nodeObjectScopeData.DropDownStyle = ComboBoxStyle.DropDownList;
            nodeObjectScopeData.HeaderText = "Object Type";
            nodeObjectScopeData.Location = new Point(3, 3);
            nodeObjectScopeData.Name = "nodeObjectScopeData";
            nodeObjectScopeData.ReadOnly = false;
            nodeObjectScopeData.Size = new Size(469, 46);
            nodeObjectScopeData.TabIndex = 7;
            // 
            // nodeObjectPropertyData
            // 
            nodeObjectPropertyData.AutoSize = true;
            nodeObjectPropertyData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            nodeObjectPropertyData.Dock = DockStyle.Fill;
            nodeObjectPropertyData.DropDownStyle = ComboBoxStyle.DropDown;
            nodeObjectPropertyData.HeaderText = "Value of";
            nodeObjectPropertyData.Location = new Point(3, 55);
            nodeObjectPropertyData.Name = "nodeObjectPropertyData";
            nodeObjectPropertyData.ReadOnly = false;
            nodeObjectPropertyData.Size = new Size(469, 291);
            nodeObjectPropertyData.TabIndex = 14;
            // 
            // nodePropertyValueTab
            // 
            nodePropertyValueTab.BackColor = SystemColors.Control;
            nodePropertyValueTab.Controls.Add(tableLayoutPanel1);
            nodePropertyValueTab.Location = new Point(4, 24);
            nodePropertyValueTab.Name = "nodePropertyValueTab";
            nodePropertyValueTab.Padding = new Padding(3);
            nodePropertyValueTab.Size = new Size(481, 355);
            nodePropertyValueTab.TabIndex = 2;
            nodePropertyValueTab.Text = "Property Value";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(nodePropertyScopeData, 0, 0);
            tableLayoutPanel1.Controls.Add(nodeModelPropertyData, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(475, 349);
            tableLayoutPanel1.TabIndex = 7;
            // 
            // nodePropertyScopeData
            // 
            nodePropertyScopeData.AutoSize = true;
            nodePropertyScopeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            nodePropertyScopeData.Dock = DockStyle.Fill;
            nodePropertyScopeData.DropDownStyle = ComboBoxStyle.DropDownList;
            nodePropertyScopeData.HeaderText = "Object Type";
            nodePropertyScopeData.Location = new Point(3, 3);
            nodePropertyScopeData.Name = "nodePropertyScopeData";
            nodePropertyScopeData.ReadOnly = false;
            nodePropertyScopeData.Size = new Size(469, 46);
            nodePropertyScopeData.TabIndex = 7;
            // 
            // nodeModelPropertyData
            // 
            nodeModelPropertyData.AutoSize = true;
            nodeModelPropertyData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            nodeModelPropertyData.Dock = DockStyle.Fill;
            nodeModelPropertyData.DropDownStyle = ComboBoxStyle.DropDownList;
            nodeModelPropertyData.HeaderText = "Value of";
            nodeModelPropertyData.Location = new Point(3, 55);
            nodeModelPropertyData.Name = "nodeModelPropertyData";
            nodeModelPropertyData.ReadOnly = false;
            nodeModelPropertyData.Size = new Size(469, 291);
            nodeModelPropertyData.TabIndex = 11;
            // 
            // nodeFixedValueTab
            // 
            nodeFixedValueTab.BackColor = SystemColors.Control;
            nodeFixedValueTab.Controls.Add(tableLayoutPanel2);
            nodeFixedValueTab.Location = new Point(4, 24);
            nodeFixedValueTab.Name = "nodeFixedValueTab";
            nodeFixedValueTab.Padding = new Padding(3);
            nodeFixedValueTab.Size = new Size(192, 72);
            nodeFixedValueTab.TabIndex = 3;
            nodeFixedValueTab.Text = "Fixed Value";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(nodeFixedValueData, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(186, 66);
            tableLayoutPanel2.TabIndex = 8;
            // 
            // nodeFixedValueData
            // 
            nodeFixedValueData.AutoSize = true;
            nodeFixedValueData.Dock = DockStyle.Fill;
            nodeFixedValueData.HeaderText = "Fixed or Default Value";
            nodeFixedValueData.Location = new Point(3, 3);
            nodeFixedValueData.Multiline = true;
            nodeFixedValueData.Name = "nodeFixedValueData";
            nodeFixedValueData.ReadOnly = false;
            nodeFixedValueData.Size = new Size(180, 60);
            nodeFixedValueData.TabIndex = 13;
            nodeFixedValueData.WordWrap = false;
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
            // SchemaNode
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(674, 677);
            Controls.Add(schemaNodeLayout);
            Name = "SchemaNode";
            Text = "SchemaNode";
            Load += SchemaNode_Load;
            Controls.SetChildIndex(schemaNodeLayout, 0);
            schemaNodeLayout.ResumeLayout(false);
            schemaNodeLayout.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            nodeTabs.ResumeLayout(false);
            nodeObjectValueTab.ResumeLayout(false);
            objectValueLayout.ResumeLayout(false);
            objectValueLayout.PerformLayout();
            nodePropertyValueTab.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            nodeFixedValueTab.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
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
        private TableLayoutPanel objectValueLayout;
        private TabControl nodeTabs;
        private TabPage nodeObjectValueTab;
        private TabPage nodeParentTab;
        private CheckBox isFixedValueData;
        private CheckBox isObjectValueData;
        private CheckBox isObjectProperty;
        private BindingSource bindingTemplate;
        private BindingSource bindingSchema;
        private BindingSource bindingNode;
        private BindingSource bindingNodeOwner;
        private TabPage nodePropertyValueTab;
        private TabPage nodeFixedValueTab;
        private Controls.ComboBoxData nodePropertyScopeData;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
        private GroupBox groupBox1;
        private CheckBox checkBox1;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
    }
}