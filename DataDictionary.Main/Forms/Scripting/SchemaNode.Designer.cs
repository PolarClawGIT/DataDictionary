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
            TableLayoutPanel nodeLayout;
            GroupBox objectNodeGroup;
            TableLayoutPanel objectLayout;
            GroupBox nodeRenderGroup;
            TableLayoutPanel renderAsLayout;
            templateTitleData = new DataDictionary.Main.Controls.TextBoxData();
            schemaTitleData = new DataDictionary.Main.Controls.TextBoxData();
            splitContainer1 = new SplitContainer();
            nodesTree = new DataDictionary.Main.Controls.XmlBuilderTreeView();
            objectTypeData = new DataDictionary.Main.Controls.ComboBoxData();
            objectPropertyData = new DataDictionary.Main.Controls.TextBoxData();
            objectScopeData = new DataDictionary.Main.Controls.ComboBoxData();
            nodeNameData = new DataDictionary.Main.Controls.TextBoxData();
            renderOrderData = new DataDictionary.Main.Controls.TextBoxData();
            renderTypeCodeData = new DataDictionary.Main.Controls.ComboBoxData();
            renderNodeTypeData = new DataDictionary.Main.Controls.ComboBoxData();
            bindingTemplate = new BindingSource(components);
            bindingSchema = new BindingSource(components);
            bindingNode = new BindingSource(components);
            schemaNodeLayout = new TableLayoutPanel();
            nodeLayout = new TableLayoutPanel();
            objectNodeGroup = new GroupBox();
            objectLayout = new TableLayoutPanel();
            nodeRenderGroup = new GroupBox();
            renderAsLayout = new TableLayoutPanel();
            schemaNodeLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            nodeLayout.SuspendLayout();
            objectNodeGroup.SuspendLayout();
            objectLayout.SuspendLayout();
            nodeRenderGroup.SuspendLayout();
            renderAsLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSchema).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingNode).BeginInit();
            SuspendLayout();
            // 
            // schemaNodeLayout
            // 
            schemaNodeLayout.ColumnCount = 1;
            schemaNodeLayout.ColumnStyles.Add(new ColumnStyle());
            schemaNodeLayout.Controls.Add(templateTitleData, 0, 0);
            schemaNodeLayout.Controls.Add(schemaTitleData, 0, 1);
            schemaNodeLayout.Controls.Add(splitContainer1, 0, 2);
            schemaNodeLayout.Dock = DockStyle.Fill;
            schemaNodeLayout.Location = new Point(0, 25);
            schemaNodeLayout.Name = "schemaNodeLayout";
            schemaNodeLayout.RowCount = 3;
            schemaNodeLayout.RowStyles.Add(new RowStyle());
            schemaNodeLayout.RowStyles.Add(new RowStyle());
            schemaNodeLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            schemaNodeLayout.Size = new Size(555, 508);
            schemaNodeLayout.TabIndex = 4;
            // 
            // templateTitleData
            // 
            templateTitleData.AutoSize = true;
            templateTitleData.Dock = DockStyle.Fill;
            templateTitleData.HeaderText = "Template";
            templateTitleData.Location = new Point(3, 3);
            templateTitleData.Multiline = false;
            templateTitleData.Name = "templateTitleData";
            templateTitleData.ReadOnly = true;
            templateTitleData.Size = new Size(549, 44);
            templateTitleData.TabIndex = 0;
            templateTitleData.WordWrap = true;
            // 
            // schemaTitleData
            // 
            schemaTitleData.AutoSize = true;
            schemaTitleData.Dock = DockStyle.Fill;
            schemaTitleData.HeaderText = "Schema";
            schemaTitleData.Location = new Point(3, 53);
            schemaTitleData.Multiline = false;
            schemaTitleData.Name = "schemaTitleData";
            schemaTitleData.ReadOnly = true;
            schemaTitleData.Size = new Size(549, 44);
            schemaTitleData.TabIndex = 1;
            schemaTitleData.WordWrap = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 103);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(nodesTree);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(nodeLayout);
            splitContainer1.Size = new Size(549, 402);
            splitContainer1.SplitterDistance = 183;
            splitContainer1.TabIndex = 2;
            // 
            // nodesTree
            // 
            nodesTree.Dock = DockStyle.Fill;
            nodesTree.Location = new Point(0, 0);
            nodesTree.Name = "nodesTree";
            nodesTree.Size = new Size(183, 402);
            nodesTree.TabIndex = 0;
            nodesTree.OnNodeSelected += NodesTree_OnNodeSelected;
            nodesTree.OnButtonClick += NodesTree_OnButtonClick;
            // 
            // nodeLayout
            // 
            nodeLayout.ColumnCount = 1;
            nodeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            nodeLayout.Controls.Add(objectNodeGroup, 0, 0);
            nodeLayout.Controls.Add(nodeRenderGroup, 0, 1);
            nodeLayout.Dock = DockStyle.Fill;
            nodeLayout.Location = new Point(0, 0);
            nodeLayout.Name = "nodeLayout";
            nodeLayout.RowCount = 2;
            nodeLayout.RowStyles.Add(new RowStyle());
            nodeLayout.RowStyles.Add(new RowStyle());
            nodeLayout.Size = new Size(362, 402);
            nodeLayout.TabIndex = 0;
            // 
            // objectNodeGroup
            // 
            objectNodeGroup.AutoSize = true;
            objectNodeGroup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            objectNodeGroup.Controls.Add(objectLayout);
            objectNodeGroup.Dock = DockStyle.Fill;
            objectNodeGroup.Location = new Point(3, 3);
            objectNodeGroup.Name = "objectNodeGroup";
            objectNodeGroup.Size = new Size(356, 176);
            objectNodeGroup.TabIndex = 8;
            objectNodeGroup.TabStop = false;
            objectNodeGroup.Text = "Data Source (Object)";
            // 
            // objectLayout
            // 
            objectLayout.AutoSize = true;
            objectLayout.ColumnCount = 1;
            objectLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            objectLayout.Controls.Add(objectTypeData, 0, 2);
            objectLayout.Controls.Add(objectPropertyData, 0, 1);
            objectLayout.Controls.Add(objectScopeData, 0, 0);
            objectLayout.Dock = DockStyle.Fill;
            objectLayout.Location = new Point(3, 19);
            objectLayout.Name = "objectLayout";
            objectLayout.RowCount = 3;
            objectLayout.RowStyles.Add(new RowStyle());
            objectLayout.RowStyles.Add(new RowStyle());
            objectLayout.RowStyles.Add(new RowStyle());
            objectLayout.Size = new Size(350, 154);
            objectLayout.TabIndex = 0;
            // 
            // objectTypeData
            // 
            objectTypeData.AutoSize = true;
            objectTypeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            objectTypeData.Dock = DockStyle.Fill;
            objectTypeData.DropDownStyle = ComboBoxStyle.DropDown;
            objectTypeData.HeaderText = "Object Data Type";
            objectTypeData.Location = new Point(3, 105);
            objectTypeData.Name = "objectTypeData";
            objectTypeData.ReadOnly = true;
            objectTypeData.Size = new Size(344, 46);
            objectTypeData.TabIndex = 10;
            // 
            // objectPropertyData
            // 
            objectPropertyData.AutoSize = true;
            objectPropertyData.Dock = DockStyle.Fill;
            objectPropertyData.HeaderText = "Object Property";
            objectPropertyData.Location = new Point(3, 55);
            objectPropertyData.Multiline = false;
            objectPropertyData.Name = "objectPropertyData";
            objectPropertyData.ReadOnly = true;
            objectPropertyData.Size = new Size(344, 44);
            objectPropertyData.TabIndex = 6;
            objectPropertyData.WordWrap = true;
            // 
            // objectScopeData
            // 
            objectScopeData.AutoSize = true;
            objectScopeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            objectScopeData.Dock = DockStyle.Fill;
            objectScopeData.DropDownStyle = ComboBoxStyle.DropDown;
            objectScopeData.HeaderText = "Object Scope";
            objectScopeData.Location = new Point(3, 3);
            objectScopeData.Name = "objectScopeData";
            objectScopeData.ReadOnly = true;
            objectScopeData.Size = new Size(344, 46);
            objectScopeData.TabIndex = 5;
            // 
            // nodeRenderGroup
            // 
            nodeRenderGroup.AutoSize = true;
            nodeRenderGroup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            nodeRenderGroup.Controls.Add(renderAsLayout);
            nodeRenderGroup.Dock = DockStyle.Fill;
            nodeRenderGroup.Location = new Point(3, 185);
            nodeRenderGroup.Name = "nodeRenderGroup";
            nodeRenderGroup.Size = new Size(356, 226);
            nodeRenderGroup.TabIndex = 9;
            nodeRenderGroup.TabStop = false;
            nodeRenderGroup.Text = "Render As";
            // 
            // renderAsLayout
            // 
            renderAsLayout.AutoSize = true;
            renderAsLayout.ColumnCount = 1;
            renderAsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            renderAsLayout.Controls.Add(nodeNameData, 0, 0);
            renderAsLayout.Controls.Add(renderOrderData, 0, 3);
            renderAsLayout.Controls.Add(renderTypeCodeData, 0, 2);
            renderAsLayout.Controls.Add(renderNodeTypeData, 0, 1);
            renderAsLayout.Dock = DockStyle.Fill;
            renderAsLayout.Location = new Point(3, 19);
            renderAsLayout.Name = "renderAsLayout";
            renderAsLayout.RowCount = 4;
            renderAsLayout.RowStyles.Add(new RowStyle());
            renderAsLayout.RowStyles.Add(new RowStyle());
            renderAsLayout.RowStyles.Add(new RowStyle());
            renderAsLayout.RowStyles.Add(new RowStyle());
            renderAsLayout.Size = new Size(350, 204);
            renderAsLayout.TabIndex = 0;
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
            nodeNameData.Size = new Size(344, 44);
            nodeNameData.TabIndex = 7;
            nodeNameData.WordWrap = true;
            // 
            // renderOrderData
            // 
            renderOrderData.AutoSize = true;
            renderOrderData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            renderOrderData.Dock = DockStyle.Fill;
            renderOrderData.HeaderText = "Render Order";
            renderOrderData.Location = new Point(3, 157);
            renderOrderData.Multiline = false;
            renderOrderData.Name = "renderOrderData";
            renderOrderData.ReadOnly = false;
            renderOrderData.Size = new Size(344, 44);
            renderOrderData.TabIndex = 12;
            renderOrderData.WordWrap = true;
            // 
            // renderTypeCodeData
            // 
            renderTypeCodeData.AutoSize = true;
            renderTypeCodeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            renderTypeCodeData.Dock = DockStyle.Fill;
            renderTypeCodeData.DropDownStyle = ComboBoxStyle.DropDown;
            renderTypeCodeData.HeaderText = "Render Data Type";
            renderTypeCodeData.Location = new Point(3, 105);
            renderTypeCodeData.Name = "renderTypeCodeData";
            renderTypeCodeData.ReadOnly = false;
            renderTypeCodeData.Size = new Size(344, 46);
            renderTypeCodeData.TabIndex = 11;
            // 
            // renderNodeTypeData
            // 
            renderNodeTypeData.AutoSize = true;
            renderNodeTypeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            renderNodeTypeData.Dock = DockStyle.Fill;
            renderNodeTypeData.DropDownStyle = ComboBoxStyle.DropDown;
            renderNodeTypeData.HeaderText = "Render Value as";
            renderNodeTypeData.Location = new Point(3, 53);
            renderNodeTypeData.Name = "renderNodeTypeData";
            renderNodeTypeData.ReadOnly = false;
            renderNodeTypeData.Size = new Size(344, 46);
            renderNodeTypeData.TabIndex = 8;
            // 
            // SchemaNode
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(555, 533);
            Controls.Add(schemaNodeLayout);
            Name = "SchemaNode";
            Text = "SchemaNode";
            Load += SchemaNode_Load;
            Controls.SetChildIndex(schemaNodeLayout, 0);
            schemaNodeLayout.ResumeLayout(false);
            schemaNodeLayout.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            nodeLayout.ResumeLayout(false);
            nodeLayout.PerformLayout();
            objectNodeGroup.ResumeLayout(false);
            objectNodeGroup.PerformLayout();
            objectLayout.ResumeLayout(false);
            objectLayout.PerformLayout();
            nodeRenderGroup.ResumeLayout(false);
            nodeRenderGroup.PerformLayout();
            renderAsLayout.ResumeLayout(false);
            renderAsLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSchema).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingNode).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData templateTitleData;
        private Controls.TextBoxData schemaTitleData;
        private BindingSource bindingTemplate;
        private BindingSource bindingSchema;
        private BindingSource bindingNode;
        private TableLayoutPanel schemaNodeLayout;
        private TableLayoutPanel nodeDetailLayout;
        private Controls.TextBoxData nodeNameData;
        private GroupBox groupBox1;
        private GroupBox nodeRenderGroup;
        private TableLayoutPanel objectLayout;
        private TableLayoutPanel renderAsLayout;
        private Controls.ComboBoxData objectScopeData;
        private Controls.TextBoxData objectPropertyData;
        private Controls.ComboBoxData objectTypeData;
        private Controls.ComboBoxData renderNodeTypeData;
        private Controls.ComboBoxData renderTypeCodeData;
        private Controls.TextBoxData renderOrderData;
        private SplitContainer splitContainer1;
        private Controls.XmlBuilderTreeView nodesTree;
    }
}