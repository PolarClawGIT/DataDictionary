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
            TableLayoutPanel schemaNodeLayout;
            TableLayoutPanel nodeParentLayout;
            TabControl nodeValueTabs;
            templateTitleData = new DataDictionary.Main.Controls.TextBoxData();
            nodeTreeView = new TreeView();
            schemaTitleData = new DataDictionary.Main.Controls.TextBoxData();
            nodeRenderAsData = new DataDictionary.Main.Controls.ComboBoxData();
            nodeRenderOrderData = new DataDictionary.Main.Controls.TextBoxData();
            nodeOwnershipData = new DataGridView();
            nodeParentColumn = new DataGridViewComboBoxColumn();
            nodeParentSelect = new DataDictionary.Main.Controls.ComboBoxData();
            addNodeParentCommand = new Button();
            nodeModelPropertyData = new DataDictionary.Main.Controls.ComboBoxData();
            nodeFixedValueData = new DataDictionary.Main.Controls.TextBoxData();
            objectValueTab = new TabPage();
            objectValueLayout = new TableLayoutPanel();
            nodeObjectPropertyData = new DataDictionary.Main.Controls.ComboBoxData();
            nodeObjectData = new DataDictionary.Main.Controls.ComboBoxData();
            fixedValueTab = new TabPage();
            objectPropertyTab = new TabPage();
            objectPropertyLayout = new TableLayoutPanel();
            fixedValueLayout = new TableLayoutPanel();
            objectNodeName = new DataDictionary.Main.Controls.TextBoxData();
            schemaNodeLayout = new TableLayoutPanel();
            nodeParentLayout = new TableLayoutPanel();
            nodeValueTabs = new TabControl();
            schemaNodeLayout.SuspendLayout();
            nodeParentLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nodeOwnershipData).BeginInit();
            nodeValueTabs.SuspendLayout();
            objectValueLayout.SuspendLayout();
            objectPropertyLayout.SuspendLayout();
            fixedValueLayout.SuspendLayout();
            SuspendLayout();
            // 
            // schemaNodeLayout
            // 
            schemaNodeLayout.ColumnCount = 3;
            schemaNodeLayout.ColumnStyles.Add(new ColumnStyle());
            schemaNodeLayout.ColumnStyles.Add(new ColumnStyle());
            schemaNodeLayout.ColumnStyles.Add(new ColumnStyle());
            schemaNodeLayout.Controls.Add(templateTitleData, 0, 0);
            schemaNodeLayout.Controls.Add(nodeTreeView, 0, 2);
            schemaNodeLayout.Controls.Add(schemaTitleData, 0, 1);
            schemaNodeLayout.Controls.Add(nodeRenderAsData, 0, 4);
            schemaNodeLayout.Controls.Add(nodeRenderOrderData, 2, 4);
            schemaNodeLayout.Location = new Point(65, 68);
            schemaNodeLayout.Name = "schemaNodeLayout";
            schemaNodeLayout.RowCount = 5;
            schemaNodeLayout.RowStyles.Add(new RowStyle());
            schemaNodeLayout.RowStyles.Add(new RowStyle());
            schemaNodeLayout.RowStyles.Add(new RowStyle());
            schemaNodeLayout.RowStyles.Add(new RowStyle());
            schemaNodeLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            schemaNodeLayout.Size = new Size(484, 464);
            schemaNodeLayout.TabIndex = 4;
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
            templateTitleData.Size = new Size(478, 44);
            templateTitleData.TabIndex = 0;
            templateTitleData.WordWrap = true;
            // 
            // nodeTreeView
            // 
            nodeTreeView.Dock = DockStyle.Fill;
            nodeTreeView.Location = new Point(3, 103);
            nodeTreeView.Name = "nodeTreeView";
            schemaNodeLayout.SetRowSpan(nodeTreeView, 2);
            nodeTreeView.Size = new Size(173, 288);
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
            schemaTitleData.Size = new Size(478, 44);
            schemaTitleData.TabIndex = 1;
            schemaTitleData.WordWrap = true;
            // 
            // nodeRenderAsData
            // 
            nodeRenderAsData.AutoSize = true;
            nodeRenderAsData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            nodeRenderAsData.DropDownStyle = ComboBoxStyle.DropDownList;
            nodeRenderAsData.HeaderText = "Render Value As";
            nodeRenderAsData.Location = new Point(3, 397);
            nodeRenderAsData.Name = "nodeRenderAsData";
            nodeRenderAsData.ReadOnly = false;
            nodeRenderAsData.Size = new Size(129, 46);
            nodeRenderAsData.TabIndex = 4;
            // 
            // nodeRenderOrderData
            // 
            nodeRenderOrderData.AutoSize = true;
            nodeRenderOrderData.HeaderText = "Render Order";
            nodeRenderOrderData.Location = new Point(182, 397);
            nodeRenderOrderData.Multiline = false;
            nodeRenderOrderData.Name = "nodeRenderOrderData";
            nodeRenderOrderData.ReadOnly = false;
            nodeRenderOrderData.Size = new Size(120, 44);
            nodeRenderOrderData.TabIndex = 5;
            nodeRenderOrderData.WordWrap = true;
            // 
            // nodeParentLayout
            // 
            nodeParentLayout.ColumnCount = 2;
            nodeParentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            nodeParentLayout.ColumnStyles.Add(new ColumnStyle());
            nodeParentLayout.Controls.Add(nodeOwnershipData, 0, 0);
            nodeParentLayout.Controls.Add(nodeParentSelect, 0, 1);
            nodeParentLayout.Controls.Add(addNodeParentCommand, 1, 1);
            nodeParentLayout.Location = new Point(568, 62);
            nodeParentLayout.Name = "nodeParentLayout";
            nodeParentLayout.RowCount = 2;
            nodeParentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            nodeParentLayout.RowStyles.Add(new RowStyle());
            nodeParentLayout.Size = new Size(186, 153);
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
            nodeOwnershipData.Size = new Size(180, 95);
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
            nodeParentSelect.Location = new Point(3, 104);
            nodeParentSelect.Name = "nodeParentSelect";
            nodeParentSelect.ReadOnly = false;
            nodeParentSelect.Size = new Size(99, 46);
            nodeParentSelect.TabIndex = 4;
            // 
            // addNodeParentCommand
            // 
            addNodeParentCommand.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            addNodeParentCommand.Location = new Point(108, 127);
            addNodeParentCommand.Name = "addNodeParentCommand";
            addNodeParentCommand.Size = new Size(75, 23);
            addNodeParentCommand.TabIndex = 5;
            addNodeParentCommand.Text = "Add";
            addNodeParentCommand.TextImageRelation = TextImageRelation.ImageBeforeText;
            addNodeParentCommand.UseVisualStyleBackColor = true;
            // 
            // nodeModelPropertyData
            // 
            nodeModelPropertyData.AutoSize = true;
            nodeModelPropertyData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            nodeModelPropertyData.DropDownStyle = ComboBoxStyle.DropDownList;
            nodeModelPropertyData.HeaderText = "Model Property";
            nodeModelPropertyData.Location = new Point(3, 101);
            nodeModelPropertyData.Name = "nodeModelPropertyData";
            nodeModelPropertyData.ReadOnly = false;
            nodeModelPropertyData.Size = new Size(129, 43);
            nodeModelPropertyData.TabIndex = 11;
            // 
            // nodeFixedValueData
            // 
            nodeFixedValueData.AutoSize = true;
            nodeFixedValueData.Dock = DockStyle.Fill;
            nodeFixedValueData.HeaderText = "Fixed/Default Value";
            nodeFixedValueData.Location = new Point(3, 76);
            nodeFixedValueData.Multiline = true;
            nodeFixedValueData.Name = "nodeFixedValueData";
            nodeFixedValueData.ReadOnly = false;
            nodeFixedValueData.Size = new Size(194, 68);
            nodeFixedValueData.TabIndex = 13;
            nodeFixedValueData.WordWrap = false;
            // 
            // nodeValueTabs
            // 
            nodeValueTabs.Controls.Add(objectValueTab);
            nodeValueTabs.Controls.Add(fixedValueTab);
            nodeValueTabs.Controls.Add(objectPropertyTab);
            nodeValueTabs.Location = new Point(571, 253);
            nodeValueTabs.Name = "nodeValueTabs";
            nodeValueTabs.SelectedIndex = 0;
            nodeValueTabs.Size = new Size(395, 110);
            nodeValueTabs.TabIndex = 5;
            // 
            // objectValueTab
            // 
            objectValueTab.BackColor = SystemColors.Control;
            objectValueTab.Location = new Point(4, 24);
            objectValueTab.Name = "objectValueTab";
            objectValueTab.Padding = new Padding(3);
            objectValueTab.Size = new Size(387, 82);
            objectValueTab.TabIndex = 0;
            objectValueTab.Text = "Object Value";
            // 
            // objectValueLayout
            // 
            objectValueLayout.ColumnCount = 1;
            objectValueLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.6666641F));
            objectValueLayout.Controls.Add(nodeObjectPropertyData, 0, 2);
            objectValueLayout.Controls.Add(nodeObjectData, 0, 1);
            objectValueLayout.Controls.Add(objectNodeName, 0, 0);
            objectValueLayout.Location = new Point(88, 567);
            objectValueLayout.Name = "objectValueLayout";
            objectValueLayout.RowCount = 3;
            objectValueLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            objectValueLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            objectValueLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            objectValueLayout.Size = new Size(279, 147);
            objectValueLayout.TabIndex = 0;
            // 
            // nodeObjectPropertyData
            // 
            nodeObjectPropertyData.AutoSize = true;
            nodeObjectPropertyData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            nodeObjectPropertyData.Dock = DockStyle.Fill;
            nodeObjectPropertyData.DropDownStyle = ComboBoxStyle.DropDown;
            nodeObjectPropertyData.HeaderText = "Object Property";
            nodeObjectPropertyData.Location = new Point(3, 101);
            nodeObjectPropertyData.Name = "nodeObjectPropertyData";
            nodeObjectPropertyData.ReadOnly = false;
            nodeObjectPropertyData.Size = new Size(273, 43);
            nodeObjectPropertyData.TabIndex = 14;
            // 
            // nodeObjectData
            // 
            nodeObjectData.AutoSize = true;
            nodeObjectData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            nodeObjectData.Dock = DockStyle.Fill;
            nodeObjectData.DropDownStyle = ComboBoxStyle.DropDownList;
            nodeObjectData.HeaderText = "Object Scope";
            nodeObjectData.Location = new Point(3, 52);
            nodeObjectData.Name = "nodeObjectData";
            nodeObjectData.ReadOnly = false;
            nodeObjectData.Size = new Size(273, 43);
            nodeObjectData.TabIndex = 7;
            // 
            // fixedValueTab
            // 
            fixedValueTab.Location = new Point(4, 24);
            fixedValueTab.Name = "fixedValueTab";
            fixedValueTab.Padding = new Padding(3);
            fixedValueTab.Size = new Size(192, 72);
            fixedValueTab.TabIndex = 1;
            fixedValueTab.Text = "Fixed Value";
            fixedValueTab.UseVisualStyleBackColor = true;
            // 
            // objectPropertyTab
            // 
            objectPropertyTab.Location = new Point(4, 24);
            objectPropertyTab.Name = "objectPropertyTab";
            objectPropertyTab.Padding = new Padding(3);
            objectPropertyTab.Size = new Size(192, 72);
            objectPropertyTab.TabIndex = 2;
            objectPropertyTab.Text = "Property Value";
            objectPropertyTab.UseVisualStyleBackColor = true;
            // 
            // objectPropertyLayout
            // 
            objectPropertyLayout.ColumnCount = 1;
            objectPropertyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            objectPropertyLayout.Controls.Add(nodeModelPropertyData, 0, 2);
            objectPropertyLayout.Location = new Point(589, 539);
            objectPropertyLayout.Name = "objectPropertyLayout";
            objectPropertyLayout.RowCount = 3;
            objectPropertyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            objectPropertyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            objectPropertyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            objectPropertyLayout.Size = new Size(200, 147);
            objectPropertyLayout.TabIndex = 6;
            // 
            // fixedValueLayout
            // 
            fixedValueLayout.ColumnCount = 1;
            fixedValueLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            fixedValueLayout.Controls.Add(nodeFixedValueData, 0, 1);
            fixedValueLayout.Location = new Point(586, 369);
            fixedValueLayout.Name = "fixedValueLayout";
            fixedValueLayout.RowCount = 2;
            fixedValueLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            fixedValueLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            fixedValueLayout.Size = new Size(200, 147);
            fixedValueLayout.TabIndex = 7;
            // 
            // objectNodeName
            // 
            objectNodeName.AutoSize = true;
            objectNodeName.Dock = DockStyle.Fill;
            objectNodeName.HeaderText = "Object Node Name (override)";
            objectNodeName.Location = new Point(3, 3);
            objectNodeName.Multiline = false;
            objectNodeName.Name = "objectNodeName";
            objectNodeName.ReadOnly = false;
            objectNodeName.Size = new Size(273, 43);
            objectNodeName.TabIndex = 15;
            objectNodeName.WordWrap = true;
            // 
            // SchemaNode
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1026, 726);
            Controls.Add(fixedValueLayout);
            Controls.Add(objectPropertyLayout);
            Controls.Add(objectValueLayout);
            Controls.Add(nodeValueTabs);
            Controls.Add(nodeParentLayout);
            Controls.Add(schemaNodeLayout);
            Name = "SchemaNode";
            Text = "SchemaNode";
            Load += SchemaNode_Load;
            Controls.SetChildIndex(schemaNodeLayout, 0);
            Controls.SetChildIndex(nodeParentLayout, 0);
            Controls.SetChildIndex(nodeValueTabs, 0);
            Controls.SetChildIndex(objectValueLayout, 0);
            Controls.SetChildIndex(objectPropertyLayout, 0);
            Controls.SetChildIndex(fixedValueLayout, 0);
            schemaNodeLayout.ResumeLayout(false);
            schemaNodeLayout.PerformLayout();
            nodeParentLayout.ResumeLayout(false);
            nodeParentLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nodeOwnershipData).EndInit();
            nodeValueTabs.ResumeLayout(false);
            objectValueLayout.ResumeLayout(false);
            objectValueLayout.PerformLayout();
            objectPropertyLayout.ResumeLayout(false);
            objectPropertyLayout.PerformLayout();
            fixedValueLayout.ResumeLayout(false);
            fixedValueLayout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData templateTitleData;
        private Controls.TextBoxData schemaTitleData;
        private TreeView nodeTreeView;
        private Controls.TextBoxData nodeNameData;
        private Controls.ComboBoxData nodeRenderAsData;
        private Controls.TextBoxData nodeRenderOrderData;
        private DataGridView nodeOwnershipData;
        private DataGridViewComboBoxColumn nodeParentColumn;
        private Controls.ComboBoxData nodeParentSelect;
        private Button addNodeParentCommand;
        private Controls.ComboBoxData nodeModelPropertyData;
        private Controls.ComboBoxData nodeObjectData;
        private Controls.TextBoxData nodeFixedValueData;
        private Controls.ComboBoxData nodeObjectPropertyData;
        private TabPage objectValueTab;
        private TableLayoutPanel objectValueLayout;
        private TabPage fixedValueTab;
        private TabPage objectPropertyTab;
        private TableLayoutPanel objectPropertyLayout;
        private TableLayoutPanel fixedValueLayout;
        private Controls.TextBoxData objectNodeName;
    }
}