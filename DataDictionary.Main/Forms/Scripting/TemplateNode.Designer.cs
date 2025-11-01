namespace DataDictionary.Main.Forms.Scripting
{
    partial class TemplateNode
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
            TabControl nodeOptions;
            TabPage valueTab;
            TableLayoutPanel nodeValueLayout;
            TabPage ownershipTab;
            TableLayoutPanel ownershipLayout;
            fixedValueData = new DataDictionary.Main.Controls.TextBoxData();
            objectScopeData = new DataDictionary.Main.Controls.ComboBoxData();
            selectObjectCommand = new Button();
            objectPropertyData = new DataDictionary.Main.Controls.TextBoxData();
            modelPropertyData = new DataDictionary.Main.Controls.ComboBoxData();
            ownershipData = new DataGridView();
            nodeParentColumn = new DataGridViewComboBoxColumn();
            parentAddCommand = new Button();
            parentNodeData = new DataDictionary.Main.Controls.ComboBoxData();
            nodeLayout = new TableLayoutPanel();
            nodeTreeView = new TreeView();
            templateData = new DataDictionary.Main.Controls.TextBoxData();
            nodeDetailLayout = new TableLayoutPanel();
            nodeNameData = new DataDictionary.Main.Controls.TextBoxData();
            renderOrderData = new DataDictionary.Main.Controls.TextBoxData();
            renderValueAsData = new DataDictionary.Main.Controls.ComboBoxData();
            bindingTemplate = new BindingSource(components);
            bindingNode = new BindingSource(components);
            bindingNodeOwner = new BindingSource(components);
            nodeOptions = new TabControl();
            valueTab = new TabPage();
            nodeValueLayout = new TableLayoutPanel();
            ownershipTab = new TabPage();
            ownershipLayout = new TableLayoutPanel();
            nodeOptions.SuspendLayout();
            valueTab.SuspendLayout();
            nodeValueLayout.SuspendLayout();
            ownershipTab.SuspendLayout();
            ownershipLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ownershipData).BeginInit();
            nodeLayout.SuspendLayout();
            nodeDetailLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingNode).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingNodeOwner).BeginInit();
            SuspendLayout();
            // 
            // nodeOptions
            // 
            nodeDetailLayout.SetColumnSpan(nodeOptions, 2);
            nodeOptions.Controls.Add(valueTab);
            nodeOptions.Controls.Add(ownershipTab);
            nodeOptions.Dock = DockStyle.Fill;
            nodeOptions.Location = new Point(3, 105);
            nodeOptions.Name = "nodeOptions";
            nodeOptions.SelectedIndex = 0;
            nodeOptions.Size = new Size(549, 261);
            nodeOptions.TabIndex = 11;
            // 
            // valueTab
            // 
            valueTab.BackColor = SystemColors.Control;
            valueTab.Controls.Add(nodeValueLayout);
            valueTab.Location = new Point(4, 24);
            valueTab.Name = "valueTab";
            valueTab.Padding = new Padding(3);
            valueTab.Size = new Size(541, 233);
            valueTab.TabIndex = 0;
            valueTab.Text = "Value";
            // 
            // nodeValueLayout
            // 
            nodeValueLayout.ColumnCount = 3;
            nodeValueLayout.ColumnStyles.Add(new ColumnStyle());
            nodeValueLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            nodeValueLayout.ColumnStyles.Add(new ColumnStyle());
            nodeValueLayout.Controls.Add(fixedValueData, 0, 0);
            nodeValueLayout.Controls.Add(objectScopeData, 0, 1);
            nodeValueLayout.Controls.Add(selectObjectCommand, 2, 1);
            nodeValueLayout.Controls.Add(objectPropertyData, 1, 1);
            nodeValueLayout.Controls.Add(modelPropertyData, 1, 2);
            nodeValueLayout.Dock = DockStyle.Fill;
            nodeValueLayout.Location = new Point(3, 3);
            nodeValueLayout.Name = "nodeValueLayout";
            nodeValueLayout.RowCount = 3;
            nodeValueLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            nodeValueLayout.RowStyles.Add(new RowStyle());
            nodeValueLayout.RowStyles.Add(new RowStyle());
            nodeValueLayout.Size = new Size(535, 227);
            nodeValueLayout.TabIndex = 0;
            // 
            // fixedValueData
            // 
            fixedValueData.AutoSize = true;
            nodeValueLayout.SetColumnSpan(fixedValueData, 3);
            fixedValueData.Dock = DockStyle.Fill;
            fixedValueData.HeaderText = "Fixed Value";
            fixedValueData.Location = new Point(3, 3);
            fixedValueData.Multiline = true;
            fixedValueData.Name = "fixedValueData";
            fixedValueData.ReadOnly = false;
            fixedValueData.Size = new Size(529, 117);
            fixedValueData.TabIndex = 5;
            fixedValueData.WordWrap = false;
            // 
            // objectScopeData
            // 
            objectScopeData.AutoSize = true;
            objectScopeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            objectScopeData.DropDownStyle = ComboBoxStyle.DropDownList;
            objectScopeData.HeaderText = "Object Scope";
            objectScopeData.Location = new Point(3, 126);
            objectScopeData.Name = "objectScopeData";
            objectScopeData.ReadOnly = false;
            objectScopeData.Size = new Size(129, 46);
            objectScopeData.TabIndex = 6;
            // 
            // selectObjectCommand
            // 
            selectObjectCommand.Location = new Point(457, 126);
            selectObjectCommand.Name = "selectObjectCommand";
            selectObjectCommand.Size = new Size(75, 23);
            selectObjectCommand.TabIndex = 8;
            selectObjectCommand.Text = "Select";
            selectObjectCommand.TextImageRelation = TextImageRelation.ImageBeforeText;
            selectObjectCommand.UseVisualStyleBackColor = true;
            // 
            // objectPropertyData
            // 
            objectPropertyData.AutoSize = true;
            objectPropertyData.Dock = DockStyle.Fill;
            objectPropertyData.HeaderText = "Object Property";
            objectPropertyData.Location = new Point(138, 126);
            objectPropertyData.Multiline = false;
            objectPropertyData.Name = "objectPropertyData";
            objectPropertyData.ReadOnly = false;
            objectPropertyData.Size = new Size(313, 46);
            objectPropertyData.TabIndex = 7;
            objectPropertyData.WordWrap = true;
            // 
            // modelPropertyData
            // 
            modelPropertyData.AutoSize = true;
            modelPropertyData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            modelPropertyData.Dock = DockStyle.Fill;
            modelPropertyData.DropDownStyle = ComboBoxStyle.DropDownList;
            modelPropertyData.HeaderText = "Model Property";
            modelPropertyData.Location = new Point(138, 178);
            modelPropertyData.Name = "modelPropertyData";
            modelPropertyData.ReadOnly = false;
            modelPropertyData.Size = new Size(313, 46);
            modelPropertyData.TabIndex = 10;
            // 
            // ownershipTab
            // 
            ownershipTab.BackColor = SystemColors.Control;
            ownershipTab.Controls.Add(ownershipLayout);
            ownershipTab.Location = new Point(4, 24);
            ownershipTab.Name = "ownershipTab";
            ownershipTab.Padding = new Padding(3);
            ownershipTab.Size = new Size(541, 233);
            ownershipTab.TabIndex = 1;
            ownershipTab.Text = "Parent/Owner";
            // 
            // ownershipLayout
            // 
            ownershipLayout.ColumnCount = 2;
            ownershipLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            ownershipLayout.ColumnStyles.Add(new ColumnStyle());
            ownershipLayout.Controls.Add(ownershipData, 0, 0);
            ownershipLayout.Controls.Add(parentAddCommand, 1, 1);
            ownershipLayout.Controls.Add(parentNodeData, 0, 1);
            ownershipLayout.Dock = DockStyle.Fill;
            ownershipLayout.Location = new Point(3, 3);
            ownershipLayout.Name = "ownershipLayout";
            ownershipLayout.RowCount = 2;
            ownershipLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            ownershipLayout.RowStyles.Add(new RowStyle());
            ownershipLayout.Size = new Size(535, 227);
            ownershipLayout.TabIndex = 1;
            // 
            // ownershipData
            // 
            ownershipData.AllowUserToAddRows = false;
            ownershipData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ownershipData.Columns.AddRange(new DataGridViewColumn[] { nodeParentColumn });
            ownershipLayout.SetColumnSpan(ownershipData, 2);
            ownershipData.Dock = DockStyle.Fill;
            ownershipData.Location = new Point(3, 3);
            ownershipData.Name = "ownershipData";
            ownershipData.ReadOnly = true;
            ownershipData.Size = new Size(529, 169);
            ownershipData.TabIndex = 0;
            ownershipData.RowValidating += ownershipData_RowValidating;
            // 
            // nodeParentColumn
            // 
            nodeParentColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            nodeParentColumn.HeaderText = "Parent Node";
            nodeParentColumn.Name = "nodeParentColumn";
            nodeParentColumn.ReadOnly = true;
            // 
            // parentAddCommand
            // 
            parentAddCommand.Dock = DockStyle.Bottom;
            parentAddCommand.Location = new Point(457, 201);
            parentAddCommand.Name = "parentAddCommand";
            parentAddCommand.Size = new Size(75, 23);
            parentAddCommand.TabIndex = 2;
            parentAddCommand.Text = "Add";
            parentAddCommand.TextImageRelation = TextImageRelation.ImageBeforeText;
            parentAddCommand.UseVisualStyleBackColor = true;
            parentAddCommand.Click += ParentAddCommand_Click;
            // 
            // parentNodeData
            // 
            parentNodeData.AutoSize = true;
            parentNodeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            parentNodeData.Dock = DockStyle.Fill;
            parentNodeData.DropDownStyle = ComboBoxStyle.DropDown;
            parentNodeData.HeaderText = "Parent Node";
            parentNodeData.Location = new Point(3, 178);
            parentNodeData.Name = "parentNodeData";
            parentNodeData.ReadOnly = false;
            parentNodeData.Size = new Size(448, 46);
            parentNodeData.TabIndex = 3;
            // 
            // nodeLayout
            // 
            nodeLayout.ColumnCount = 2;
            nodeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            nodeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46.6666679F));
            nodeLayout.Controls.Add(nodeTreeView, 0, 1);
            nodeLayout.Controls.Add(templateData, 0, 0);
            nodeLayout.Controls.Add(nodeDetailLayout, 1, 1);
            nodeLayout.Dock = DockStyle.Fill;
            nodeLayout.Location = new Point(0, 25);
            nodeLayout.Name = "nodeLayout";
            nodeLayout.RowCount = 2;
            nodeLayout.RowStyles.Add(new RowStyle());
            nodeLayout.RowStyles.Add(new RowStyle());
            nodeLayout.Size = new Size(800, 425);
            nodeLayout.TabIndex = 4;
            // 
            // nodeTreeView
            // 
            nodeTreeView.Dock = DockStyle.Fill;
            nodeTreeView.Location = new Point(3, 53);
            nodeTreeView.Name = "nodeTreeView";
            nodeTreeView.Size = new Size(233, 369);
            nodeTreeView.TabIndex = 12;
            nodeTreeView.NodeMouseClick += NodeTreeView_NodeSelected;
            // 
            // templateData
            // 
            templateData.AutoSize = true;
            nodeLayout.SetColumnSpan(templateData, 2);
            templateData.Dock = DockStyle.Fill;
            templateData.HeaderText = "Template";
            templateData.Location = new Point(3, 3);
            templateData.Multiline = false;
            templateData.Name = "templateData";
            templateData.ReadOnly = true;
            templateData.Size = new Size(794, 44);
            templateData.TabIndex = 0;
            templateData.WordWrap = true;
            // 
            // nodeDetailLayout
            // 
            nodeDetailLayout.ColumnCount = 2;
            nodeDetailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            nodeDetailLayout.ColumnStyles.Add(new ColumnStyle());
            nodeDetailLayout.Controls.Add(nodeOptions, 0, 2);
            nodeDetailLayout.Controls.Add(nodeNameData, 0, 0);
            nodeDetailLayout.Controls.Add(renderOrderData, 1, 1);
            nodeDetailLayout.Controls.Add(renderValueAsData, 1, 0);
            nodeDetailLayout.Dock = DockStyle.Fill;
            nodeDetailLayout.Location = new Point(242, 53);
            nodeDetailLayout.Name = "nodeDetailLayout";
            nodeDetailLayout.RowCount = 3;
            nodeDetailLayout.RowStyles.Add(new RowStyle());
            nodeDetailLayout.RowStyles.Add(new RowStyle());
            nodeDetailLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            nodeDetailLayout.Size = new Size(555, 369);
            nodeDetailLayout.TabIndex = 13;
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
            nodeNameData.Size = new Size(414, 46);
            nodeNameData.TabIndex = 1;
            nodeNameData.WordWrap = true;
            // 
            // renderOrderData
            // 
            renderOrderData.AutoSize = true;
            renderOrderData.HeaderText = "Render Order";
            renderOrderData.Location = new Point(423, 55);
            renderOrderData.Multiline = false;
            renderOrderData.Name = "renderOrderData";
            renderOrderData.ReadOnly = false;
            renderOrderData.Size = new Size(120, 44);
            renderOrderData.TabIndex = 2;
            renderOrderData.WordWrap = true;
            // 
            // renderValueAsData
            // 
            renderValueAsData.AutoSize = true;
            renderValueAsData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            renderValueAsData.DropDownStyle = ComboBoxStyle.DropDownList;
            renderValueAsData.HeaderText = "Render Value As";
            renderValueAsData.Location = new Point(423, 3);
            renderValueAsData.Name = "renderValueAsData";
            renderValueAsData.ReadOnly = false;
            renderValueAsData.Size = new Size(129, 46);
            renderValueAsData.TabIndex = 3;
            // 
            // bindingNode
            // 
            bindingNode.ListChanged += BindingNode_ListChanged;
            // 
            // bindingNodeOwner
            // 
            bindingNodeOwner.AddingNew += BindingNodeOwner_AddingNew;
            bindingNodeOwner.ListChanged += BindingNodeOwner_ListChanged;
            // 
            // TemplateNode
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(nodeLayout);
            Name = "TemplateNode";
            Text = "Template Node";
            Load += TemplateNode_Load;
            Controls.SetChildIndex(nodeLayout, 0);
            nodeOptions.ResumeLayout(false);
            valueTab.ResumeLayout(false);
            nodeValueLayout.ResumeLayout(false);
            nodeValueLayout.PerformLayout();
            ownershipTab.ResumeLayout(false);
            ownershipLayout.ResumeLayout(false);
            ownershipLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ownershipData).EndInit();
            nodeLayout.ResumeLayout(false);
            nodeLayout.PerformLayout();
            nodeDetailLayout.ResumeLayout(false);
            nodeDetailLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingNode).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingNodeOwner).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData templateData;
        private TreeView nodeTreeView;
        private BindingSource bindingTemplate;
        private BindingSource bindingNode;
        private TableLayoutPanel nodeLayout;
        private BindingSource bindingNodeOwner;
        private TableLayoutPanel nodeDetailLayout;
        private Controls.TextBoxData fixedValueData;
        private Controls.ComboBoxData objectScopeData;
        private Button selectObjectCommand;
        private Controls.TextBoxData objectPropertyData;
        private Controls.ComboBoxData modelPropertyData;
        private DataGridView ownershipData;
        private Button parentAddCommand;
        private Controls.TextBoxData nodeNameData;
        private Controls.TextBoxData renderOrderData;
        private Controls.ComboBoxData renderValueAsData;
        private DataGridViewComboBoxColumn nodeParentColumn;
        private Controls.ComboBoxData parentNodeData;
    }
}