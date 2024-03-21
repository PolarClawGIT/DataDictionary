namespace DataDictionary.Main.Forms.Scripting
{
    partial class DataBuilder
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
            TableLayoutPanel dataBuilderLayout;
            GroupBox nodeTypeGroup;
            TableLayoutPanel nodeTypeLayout;
            GroupBox dataFormatGroup;
            TableLayoutPanel dataFormatLayout;
            GroupBox nodeOptionsGroup;
            TableLayoutPanel optionsLayout;
            Label columnLabel;
            schemaNameData = new Controls.TextBoxData();
            xmlData = new Controls.TextBoxData();
            schemaColumnData = new DataGridView();
            scriptAsElementData = new CheckBox();
            scriptAsAttributeData = new CheckBox();
            scriptAsTextData = new CheckBox();
            scriptAsCData = new CheckBox();
            scriptAsXmlData = new CheckBox();
            scriptIncludeDataType = new CheckBox();
            scriptIncludeAllowNull = new CheckBox();
            schemaScopeData = new Controls.TextBoxData();
            itemSelection = new TreeView();
            splitContainer = new SplitContainer();
            bindingSchema = new BindingSource(components);
            dataBuilderLayout = new TableLayoutPanel();
            nodeTypeGroup = new GroupBox();
            nodeTypeLayout = new TableLayoutPanel();
            dataFormatGroup = new GroupBox();
            dataFormatLayout = new TableLayoutPanel();
            nodeOptionsGroup = new GroupBox();
            optionsLayout = new TableLayoutPanel();
            columnLabel = new Label();
            dataBuilderLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)schemaColumnData).BeginInit();
            nodeTypeGroup.SuspendLayout();
            nodeTypeLayout.SuspendLayout();
            dataFormatGroup.SuspendLayout();
            dataFormatLayout.SuspendLayout();
            nodeOptionsGroup.SuspendLayout();
            optionsLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSchema).BeginInit();
            SuspendLayout();
            // 
            // dataBuilderLayout
            // 
            dataBuilderLayout.ColumnCount = 2;
            dataBuilderLayout.ColumnStyles.Add(new ColumnStyle());
            dataBuilderLayout.ColumnStyles.Add(new ColumnStyle());
            dataBuilderLayout.Controls.Add(schemaNameData, 0, 0);
            dataBuilderLayout.Controls.Add(xmlData, 0, 6);
            dataBuilderLayout.Controls.Add(schemaColumnData, 0, 2);
            dataBuilderLayout.Controls.Add(nodeTypeGroup, 1, 3);
            dataBuilderLayout.Controls.Add(dataFormatGroup, 1, 4);
            dataBuilderLayout.Controls.Add(nodeOptionsGroup, 1, 5);
            dataBuilderLayout.Controls.Add(schemaScopeData, 1, 1);
            dataBuilderLayout.Controls.Add(columnLabel, 0, 1);
            dataBuilderLayout.Dock = DockStyle.Fill;
            dataBuilderLayout.Location = new Point(0, 0);
            dataBuilderLayout.Name = "dataBuilderLayout";
            dataBuilderLayout.RowCount = 7;
            dataBuilderLayout.RowStyles.Add(new RowStyle());
            dataBuilderLayout.RowStyles.Add(new RowStyle());
            dataBuilderLayout.RowStyles.Add(new RowStyle());
            dataBuilderLayout.RowStyles.Add(new RowStyle());
            dataBuilderLayout.RowStyles.Add(new RowStyle());
            dataBuilderLayout.RowStyles.Add(new RowStyle());
            dataBuilderLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            dataBuilderLayout.Size = new Size(611, 487);
            dataBuilderLayout.TabIndex = 4;
            // 
            // schemaNameData
            // 
            schemaNameData.AutoSize = true;
            dataBuilderLayout.SetColumnSpan(schemaNameData, 2);
            schemaNameData.Dock = DockStyle.Fill;
            schemaNameData.HeaderText = "Schema Name";
            schemaNameData.Location = new Point(3, 3);
            schemaNameData.Multiline = false;
            schemaNameData.Name = "schemaNameData";
            schemaNameData.ReadOnly = true;
            schemaNameData.Size = new Size(605, 44);
            schemaNameData.TabIndex = 2;
            // 
            // xmlData
            // 
            xmlData.AutoSize = true;
            dataBuilderLayout.SetColumnSpan(xmlData, 2);
            xmlData.Dock = DockStyle.Fill;
            xmlData.HeaderText = "XML Data";
            xmlData.Location = new Point(3, 312);
            xmlData.Multiline = true;
            xmlData.Name = "xmlData";
            xmlData.ReadOnly = true;
            xmlData.Size = new Size(605, 172);
            xmlData.TabIndex = 0;
            // 
            // schemaColumnData
            // 
            schemaColumnData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            schemaColumnData.Dock = DockStyle.Fill;
            schemaColumnData.Location = new Point(3, 68);
            schemaColumnData.Name = "schemaColumnData";
            dataBuilderLayout.SetRowSpan(schemaColumnData, 4);
            schemaColumnData.Size = new Size(334, 238);
            schemaColumnData.TabIndex = 4;
            // 
            // nodeTypeGroup
            // 
            nodeTypeGroup.AutoSize = true;
            nodeTypeGroup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            nodeTypeGroup.Controls.Add(nodeTypeLayout);
            nodeTypeGroup.Dock = DockStyle.Fill;
            nodeTypeGroup.Location = new Point(343, 103);
            nodeTypeGroup.Name = "nodeTypeGroup";
            nodeTypeGroup.Size = new Size(265, 47);
            nodeTypeGroup.TabIndex = 12;
            nodeTypeGroup.TabStop = false;
            nodeTypeGroup.Text = "Node Type";
            // 
            // nodeTypeLayout
            // 
            nodeTypeLayout.AutoSize = true;
            nodeTypeLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            nodeTypeLayout.ColumnCount = 2;
            nodeTypeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            nodeTypeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            nodeTypeLayout.Controls.Add(scriptAsElementData, 0, 0);
            nodeTypeLayout.Controls.Add(scriptAsAttributeData, 1, 0);
            nodeTypeLayout.Dock = DockStyle.Fill;
            nodeTypeLayout.Location = new Point(3, 19);
            nodeTypeLayout.Name = "nodeTypeLayout";
            nodeTypeLayout.RowCount = 1;
            nodeTypeLayout.RowStyles.Add(new RowStyle());
            nodeTypeLayout.Size = new Size(259, 25);
            nodeTypeLayout.TabIndex = 0;
            // 
            // scriptAsElementData
            // 
            scriptAsElementData.AutoSize = true;
            scriptAsElementData.Location = new Point(3, 3);
            scriptAsElementData.Name = "scriptAsElementData";
            scriptAsElementData.Size = new Size(83, 19);
            scriptAsElementData.TabIndex = 5;
            scriptAsElementData.Text = "as Element";
            scriptAsElementData.UseVisualStyleBackColor = true;
            // 
            // scriptAsAttributeData
            // 
            scriptAsAttributeData.AutoSize = true;
            scriptAsAttributeData.Location = new Point(132, 3);
            scriptAsAttributeData.Name = "scriptAsAttributeData";
            scriptAsAttributeData.Size = new Size(87, 19);
            scriptAsAttributeData.TabIndex = 6;
            scriptAsAttributeData.Text = "as Attribute";
            scriptAsAttributeData.UseVisualStyleBackColor = true;
            // 
            // dataFormatGroup
            // 
            dataFormatGroup.AutoSize = true;
            dataFormatGroup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            dataFormatGroup.Controls.Add(dataFormatLayout);
            dataFormatGroup.Dock = DockStyle.Fill;
            dataFormatGroup.Location = new Point(343, 156);
            dataFormatGroup.Name = "dataFormatGroup";
            dataFormatGroup.Size = new Size(265, 72);
            dataFormatGroup.TabIndex = 13;
            dataFormatGroup.TabStop = false;
            dataFormatGroup.Text = "Data format";
            // 
            // dataFormatLayout
            // 
            dataFormatLayout.AutoSize = true;
            dataFormatLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            dataFormatLayout.ColumnCount = 2;
            dataFormatLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            dataFormatLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            dataFormatLayout.Controls.Add(scriptAsTextData, 0, 0);
            dataFormatLayout.Controls.Add(scriptAsCData, 1, 0);
            dataFormatLayout.Controls.Add(scriptAsXmlData, 1, 1);
            dataFormatLayout.Dock = DockStyle.Fill;
            dataFormatLayout.Location = new Point(3, 19);
            dataFormatLayout.Name = "dataFormatLayout";
            dataFormatLayout.RowCount = 2;
            dataFormatLayout.RowStyles.Add(new RowStyle());
            dataFormatLayout.RowStyles.Add(new RowStyle());
            dataFormatLayout.Size = new Size(259, 50);
            dataFormatLayout.TabIndex = 0;
            // 
            // scriptAsTextData
            // 
            scriptAsTextData.AutoSize = true;
            scriptAsTextData.Location = new Point(3, 3);
            scriptAsTextData.Name = "scriptAsTextData";
            scriptAsTextData.Size = new Size(61, 19);
            scriptAsTextData.TabIndex = 7;
            scriptAsTextData.Text = "as Text";
            scriptAsTextData.UseVisualStyleBackColor = true;
            // 
            // scriptAsCData
            // 
            scriptAsCData.AutoSize = true;
            scriptAsCData.Location = new Point(132, 3);
            scriptAsCData.Name = "scriptAsCData";
            scriptAsCData.Size = new Size(72, 19);
            scriptAsCData.TabIndex = 8;
            scriptAsCData.Text = "as CData";
            scriptAsCData.UseVisualStyleBackColor = true;
            // 
            // scriptAsXmlData
            // 
            scriptAsXmlData.AutoSize = true;
            scriptAsXmlData.Location = new Point(132, 28);
            scriptAsXmlData.Name = "scriptAsXmlData";
            scriptAsXmlData.Size = new Size(64, 19);
            scriptAsXmlData.TabIndex = 9;
            scriptAsXmlData.Text = "as XML";
            scriptAsXmlData.UseVisualStyleBackColor = true;
            // 
            // nodeOptionsGroup
            // 
            nodeOptionsGroup.AutoSize = true;
            nodeOptionsGroup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            nodeOptionsGroup.Controls.Add(optionsLayout);
            nodeOptionsGroup.Dock = DockStyle.Fill;
            nodeOptionsGroup.Location = new Point(343, 234);
            nodeOptionsGroup.Name = "nodeOptionsGroup";
            nodeOptionsGroup.Size = new Size(265, 72);
            nodeOptionsGroup.TabIndex = 14;
            nodeOptionsGroup.TabStop = false;
            nodeOptionsGroup.Text = "Options";
            // 
            // optionsLayout
            // 
            optionsLayout.AutoSize = true;
            optionsLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            optionsLayout.ColumnCount = 1;
            optionsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            optionsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            optionsLayout.Controls.Add(scriptIncludeDataType, 0, 0);
            optionsLayout.Controls.Add(scriptIncludeAllowNull, 0, 1);
            optionsLayout.Dock = DockStyle.Fill;
            optionsLayout.Location = new Point(3, 19);
            optionsLayout.Name = "optionsLayout";
            optionsLayout.RowCount = 2;
            optionsLayout.RowStyles.Add(new RowStyle());
            optionsLayout.RowStyles.Add(new RowStyle());
            optionsLayout.Size = new Size(259, 50);
            optionsLayout.TabIndex = 0;
            // 
            // scriptIncludeDataType
            // 
            scriptIncludeDataType.AutoSize = true;
            scriptIncludeDataType.Location = new Point(3, 3);
            scriptIncludeDataType.Name = "scriptIncludeDataType";
            scriptIncludeDataType.Size = new Size(119, 19);
            scriptIncludeDataType.TabIndex = 10;
            scriptIncludeDataType.Text = "include Data Type";
            scriptIncludeDataType.UseVisualStyleBackColor = true;
            // 
            // scriptIncludeAllowNull
            // 
            scriptIncludeAllowNull.AutoSize = true;
            scriptIncludeAllowNull.Location = new Point(3, 28);
            scriptIncludeAllowNull.Name = "scriptIncludeAllowNull";
            scriptIncludeAllowNull.Size = new Size(112, 19);
            scriptIncludeAllowNull.TabIndex = 11;
            scriptIncludeAllowNull.Text = "include Nullable";
            scriptIncludeAllowNull.UseVisualStyleBackColor = true;
            // 
            // schemaScopeData
            // 
            schemaScopeData.AutoSize = true;
            schemaScopeData.Dock = DockStyle.Fill;
            schemaScopeData.HeaderText = "Scope";
            schemaScopeData.Location = new Point(343, 53);
            schemaScopeData.Multiline = false;
            schemaScopeData.Name = "schemaScopeData";
            schemaScopeData.ReadOnly = true;
            dataBuilderLayout.SetRowSpan(schemaScopeData, 2);
            schemaScopeData.Size = new Size(265, 44);
            schemaScopeData.TabIndex = 3;
            // 
            // columnLabel
            // 
            columnLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            columnLabel.AutoSize = true;
            columnLabel.Location = new Point(3, 50);
            columnLabel.Name = "columnLabel";
            columnLabel.Size = new Size(55, 15);
            columnLabel.TabIndex = 15;
            columnLabel.Text = "Columns";
            // 
            // itemSelection
            // 
            itemSelection.CheckBoxes = true;
            itemSelection.Dock = DockStyle.Fill;
            itemSelection.FullRowSelect = true;
            itemSelection.HideSelection = false;
            itemSelection.Location = new Point(0, 0);
            itemSelection.Name = "itemSelection";
            itemSelection.Size = new Size(307, 487);
            itemSelection.TabIndex = 4;
            itemSelection.AfterCheck += itemSelection_AfterCheck;
            itemSelection.AfterSelect += itemSelection_AfterSelect;
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 25);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(itemSelection);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(dataBuilderLayout);
            splitContainer.Size = new Size(922, 487);
            splitContainer.SplitterDistance = 307;
            splitContainer.TabIndex = 2;
            // 
            // DataBuilder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(922, 512);
            Controls.Add(splitContainer);
            Name = "DataBuilder";
            Text = "XML Data Builder";
            Load += DetailXmlView_Load;
            Controls.SetChildIndex(splitContainer, 0);
            dataBuilderLayout.ResumeLayout(false);
            dataBuilderLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)schemaColumnData).EndInit();
            nodeTypeGroup.ResumeLayout(false);
            nodeTypeGroup.PerformLayout();
            nodeTypeLayout.ResumeLayout(false);
            nodeTypeLayout.PerformLayout();
            dataFormatGroup.ResumeLayout(false);
            dataFormatGroup.PerformLayout();
            dataFormatLayout.ResumeLayout(false);
            dataFormatLayout.PerformLayout();
            nodeOptionsGroup.ResumeLayout(false);
            nodeOptionsGroup.PerformLayout();
            optionsLayout.ResumeLayout(false);
            optionsLayout.PerformLayout();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)bindingSchema).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TreeView itemSelection;
        private SplitContainer splitContainer;
        private Controls.TextBoxData xmlData;
        private Controls.TextBoxData schemaNameData;
        private DataGridView schemaColumnData;
        private CheckBox scriptAsElementData;
        private CheckBox scriptAsAttributeData;
        private CheckBox scriptAsTextData;
        private CheckBox scriptAsCData;
        private CheckBox scriptAsXmlData;
        private CheckBox scriptIncludeDataType;
        private CheckBox scriptIncludeAllowNull;
        private Controls.TextBoxData schemaScopeData;
        private BindingSource bindingSchema;
    }
}