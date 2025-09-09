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
            TableLayoutPanel attributeLayout;
            TabControl nodeOptions;
            TabPage valueTab;
            TableLayoutPanel nodeValueLayout;
            TabPage ownershipTab;
            TableLayoutPanel ownershipLayout;
            nodeNameData = new DataDictionary.Main.Controls.TextBoxData();
            fixedValueData = new DataDictionary.Main.Controls.TextBoxData();
            objectScopeData = new DataDictionary.Main.Controls.ComboBoxData();
            selectObjectCommand = new Button();
            objectPropertyData = new DataDictionary.Main.Controls.TextBoxData();
            modelPropertyData = new DataDictionary.Main.Controls.ComboBoxData();
            ownershipData = new DataGridView();
            elementPathColumn = new DataGridViewTextBoxColumn();
            elementPathData = new DataDictionary.Main.Controls.TextBoxData();
            elementSelectCommand = new Button();
            renderValueAsData = new DataDictionary.Main.Controls.ComboBoxData();
            renderOrderData = new DataDictionary.Main.Controls.TextBoxData();
            nodeNavigation = new TreeView();
            templateData = new DataDictionary.Main.Controls.TextBoxData();
            bindingTemplate = new BindingSource(components);
            bindingTemplateNode = new BindingSource(components);
            nodeCommands = new ContextMenuStrip(components);
            newAttributeCommand = new ToolStripMenuItem();
            newElementCommand = new ToolStripMenuItem();
            attributeLayout = new TableLayoutPanel();
            nodeOptions = new TabControl();
            valueTab = new TabPage();
            nodeValueLayout = new TableLayoutPanel();
            ownershipTab = new TabPage();
            ownershipLayout = new TableLayoutPanel();
            attributeLayout.SuspendLayout();
            nodeOptions.SuspendLayout();
            valueTab.SuspendLayout();
            nodeValueLayout.SuspendLayout();
            ownershipTab.SuspendLayout();
            ownershipLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ownershipData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingTemplateNode).BeginInit();
            nodeCommands.SuspendLayout();
            SuspendLayout();
            // 
            // attributeLayout
            // 
            attributeLayout.ColumnCount = 3;
            attributeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            attributeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            attributeLayout.ColumnStyles.Add(new ColumnStyle());
            attributeLayout.Controls.Add(nodeNameData, 1, 1);
            attributeLayout.Controls.Add(nodeOptions, 1, 3);
            attributeLayout.Controls.Add(renderValueAsData, 2, 1);
            attributeLayout.Controls.Add(renderOrderData, 2, 2);
            attributeLayout.Controls.Add(nodeNavigation, 0, 1);
            attributeLayout.Controls.Add(templateData, 0, 0);
            attributeLayout.Dock = DockStyle.Fill;
            attributeLayout.Location = new Point(0, 25);
            attributeLayout.Name = "attributeLayout";
            attributeLayout.RowCount = 4;
            attributeLayout.RowStyles.Add(new RowStyle());
            attributeLayout.RowStyles.Add(new RowStyle());
            attributeLayout.RowStyles.Add(new RowStyle());
            attributeLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            attributeLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            attributeLayout.Size = new Size(800, 425);
            attributeLayout.TabIndex = 4;
            // 
            // nodeNameData
            // 
            nodeNameData.AutoSize = true;
            nodeNameData.Dock = DockStyle.Fill;
            nodeNameData.HeaderText = "Node Name";
            nodeNameData.Location = new Point(202, 53);
            nodeNameData.Multiline = false;
            nodeNameData.Name = "nodeNameData";
            nodeNameData.ReadOnly = false;
            nodeNameData.Size = new Size(459, 46);
            nodeNameData.TabIndex = 1;
            nodeNameData.WordWrap = true;
            // 
            // nodeOptions
            // 
            attributeLayout.SetColumnSpan(nodeOptions, 2);
            nodeOptions.Controls.Add(valueTab);
            nodeOptions.Controls.Add(ownershipTab);
            nodeOptions.Dock = DockStyle.Fill;
            nodeOptions.Location = new Point(202, 155);
            nodeOptions.Name = "nodeOptions";
            nodeOptions.SelectedIndex = 0;
            nodeOptions.Size = new Size(595, 267);
            nodeOptions.TabIndex = 11;
            // 
            // valueTab
            // 
            valueTab.BackColor = SystemColors.Control;
            valueTab.Controls.Add(nodeValueLayout);
            valueTab.Location = new Point(4, 24);
            valueTab.Name = "valueTab";
            valueTab.Padding = new Padding(3);
            valueTab.Size = new Size(587, 239);
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
            nodeValueLayout.Size = new Size(581, 233);
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
            fixedValueData.Size = new Size(575, 123);
            fixedValueData.TabIndex = 5;
            fixedValueData.WordWrap = false;
            // 
            // objectScopeData
            // 
            objectScopeData.AutoSize = true;
            objectScopeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            objectScopeData.DropDownStyle = ComboBoxStyle.DropDownList;
            objectScopeData.HeaderText = "Object Scope";
            objectScopeData.Location = new Point(3, 132);
            objectScopeData.Name = "objectScopeData";
            objectScopeData.ReadOnly = false;
            objectScopeData.Size = new Size(129, 46);
            objectScopeData.TabIndex = 6;
            // 
            // selectObjectCommand
            // 
            selectObjectCommand.Location = new Point(503, 132);
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
            objectPropertyData.Location = new Point(138, 132);
            objectPropertyData.Multiline = false;
            objectPropertyData.Name = "objectPropertyData";
            objectPropertyData.ReadOnly = false;
            objectPropertyData.Size = new Size(359, 46);
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
            modelPropertyData.Location = new Point(138, 184);
            modelPropertyData.Name = "modelPropertyData";
            modelPropertyData.ReadOnly = false;
            modelPropertyData.Size = new Size(359, 46);
            modelPropertyData.TabIndex = 10;
            // 
            // ownershipTab
            // 
            ownershipTab.BackColor = SystemColors.Control;
            ownershipTab.Controls.Add(ownershipLayout);
            ownershipTab.Location = new Point(4, 24);
            ownershipTab.Name = "ownershipTab";
            ownershipTab.Padding = new Padding(3);
            ownershipTab.Size = new Size(192, 72);
            ownershipTab.TabIndex = 1;
            ownershipTab.Text = "Parent/Owner";
            // 
            // ownershipLayout
            // 
            ownershipLayout.ColumnCount = 2;
            ownershipLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            ownershipLayout.ColumnStyles.Add(new ColumnStyle());
            ownershipLayout.Controls.Add(ownershipData, 0, 0);
            ownershipLayout.Controls.Add(elementPathData, 0, 1);
            ownershipLayout.Controls.Add(elementSelectCommand, 1, 1);
            ownershipLayout.Dock = DockStyle.Fill;
            ownershipLayout.Location = new Point(3, 3);
            ownershipLayout.Name = "ownershipLayout";
            ownershipLayout.RowCount = 2;
            ownershipLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            ownershipLayout.RowStyles.Add(new RowStyle());
            ownershipLayout.Size = new Size(186, 66);
            ownershipLayout.TabIndex = 1;
            // 
            // ownershipData
            // 
            ownershipData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ownershipData.Columns.AddRange(new DataGridViewColumn[] { elementPathColumn });
            ownershipLayout.SetColumnSpan(ownershipData, 2);
            ownershipData.Dock = DockStyle.Fill;
            ownershipData.Location = new Point(3, 3);
            ownershipData.Name = "ownershipData";
            ownershipData.Size = new Size(180, 10);
            ownershipData.TabIndex = 0;
            // 
            // elementPathColumn
            // 
            elementPathColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            elementPathColumn.HeaderText = "Element";
            elementPathColumn.Name = "elementPathColumn";
            // 
            // elementPathData
            // 
            elementPathData.AutoSize = true;
            elementPathData.Dock = DockStyle.Fill;
            elementPathData.HeaderText = "Element Path";
            elementPathData.Location = new Point(3, 19);
            elementPathData.Multiline = false;
            elementPathData.Name = "elementPathData";
            elementPathData.ReadOnly = true;
            elementPathData.Size = new Size(99, 44);
            elementPathData.TabIndex = 1;
            elementPathData.WordWrap = true;
            // 
            // elementSelectCommand
            // 
            elementSelectCommand.Location = new Point(108, 19);
            elementSelectCommand.Name = "elementSelectCommand";
            elementSelectCommand.Size = new Size(75, 23);
            elementSelectCommand.TabIndex = 2;
            elementSelectCommand.Text = "Select";
            elementSelectCommand.TextImageRelation = TextImageRelation.ImageBeforeText;
            elementSelectCommand.UseVisualStyleBackColor = true;
            // 
            // renderValueAsData
            // 
            renderValueAsData.AutoSize = true;
            renderValueAsData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            renderValueAsData.DropDownStyle = ComboBoxStyle.DropDownList;
            renderValueAsData.HeaderText = "Render Value As";
            renderValueAsData.Location = new Point(667, 53);
            renderValueAsData.Name = "renderValueAsData";
            renderValueAsData.ReadOnly = false;
            renderValueAsData.Size = new Size(129, 46);
            renderValueAsData.TabIndex = 3;
            // 
            // renderOrderData
            // 
            renderOrderData.AutoSize = true;
            renderOrderData.HeaderText = "Render Order";
            renderOrderData.Location = new Point(667, 105);
            renderOrderData.Multiline = false;
            renderOrderData.Name = "renderOrderData";
            renderOrderData.ReadOnly = false;
            renderOrderData.Size = new Size(120, 44);
            renderOrderData.TabIndex = 2;
            renderOrderData.WordWrap = true;
            // 
            // nodeNavigation
            // 
            nodeNavigation.Dock = DockStyle.Fill;
            nodeNavigation.Location = new Point(3, 53);
            nodeNavigation.Name = "nodeNavigation";
            attributeLayout.SetRowSpan(nodeNavigation, 3);
            nodeNavigation.Size = new Size(193, 369);
            nodeNavigation.TabIndex = 12;
            // 
            // templateData
            // 
            templateData.AutoSize = true;
            attributeLayout.SetColumnSpan(templateData, 3);
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
            // nodeCommands
            // 
            nodeCommands.Items.AddRange(new ToolStripItem[] { newAttributeCommand, newElementCommand });
            nodeCommands.Name = "nodeCommands";
            nodeCommands.Size = new Size(147, 48);
            // 
            // newAttributeCommand
            // 
            newAttributeCommand.Name = "newAttributeCommand";
            newAttributeCommand.Size = new Size(146, 22);
            newAttributeCommand.Text = "new Attribute";
            newAttributeCommand.Click += NewAttributeCommand_Click;
            // 
            // newElementCommand
            // 
            newElementCommand.Name = "newElementCommand";
            newElementCommand.Size = new Size(146, 22);
            newElementCommand.Text = "new Element";
            newElementCommand.Click += NewElementCommand_Click;
            // 
            // TemplateNode
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(attributeLayout);
            Name = "TemplateNode";
            Text = "Template Node";
            Load += TemplateNode_Load;
            Controls.SetChildIndex(attributeLayout, 0);
            attributeLayout.ResumeLayout(false);
            attributeLayout.PerformLayout();
            nodeOptions.ResumeLayout(false);
            valueTab.ResumeLayout(false);
            nodeValueLayout.ResumeLayout(false);
            nodeValueLayout.PerformLayout();
            ownershipTab.ResumeLayout(false);
            ownershipLayout.ResumeLayout(false);
            ownershipLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ownershipData).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingTemplateNode).EndInit();
            nodeCommands.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData templateData;
        private Controls.TextBoxData nodeNameData;
        private Controls.TextBoxData renderOrderData;
        private Controls.ComboBoxData renderValueAsData;
        private Controls.TextBoxData fixedValueData;
        private Controls.ComboBoxData objectScopeData;
        private Controls.TextBoxData objectPropertyData;
        private Button selectObjectCommand;
        private DataGridView ownershipData;
        private DataGridViewTextBoxColumn elementPathColumn;
        private Controls.ComboBoxData modelPropertyData;
        private Controls.TextBoxData elementPathData;
        private Button elementSelectCommand;
        private TreeView nodeNavigation;
        private BindingSource bindingTemplate;
        private BindingSource bindingTemplateNode;
        private ContextMenuStrip nodeCommands;
        private ToolStripMenuItem newAttributeCommand;
        private ToolStripMenuItem newElementCommand;
    }
}