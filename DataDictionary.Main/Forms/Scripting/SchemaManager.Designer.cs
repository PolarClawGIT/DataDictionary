namespace DataDictionary.Main.Forms.Scripting
{
    partial class SchemaManager
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
            TableLayoutPanel schemaManagerLayout;
            GroupBox elementGroup;
            TableLayoutPanel elementLayout;
            ListViewGroup listViewGroup1 = new ListViewGroup("Scope Name 1", HorizontalAlignment.Left);
            ListViewItem listViewItem1 = new ListViewItem(new string[] { "Column Name 1", "Column 1" }, -1);
            GroupBox elementNillableGroup;
            TableLayoutPanel elementNillableLayout;
            GroupBox renderDataAsGroup;
            TableLayoutPanel renderDataAsLayout;
            GroupBox elementRenderGroup;
            TableLayoutPanel renderAsLayout;
            schemaTitleData = new Controls.TextBoxData();
            schemaDescriptionData = new Controls.TextBoxData();
            elementSelection = new ListView();
            columnName = new ColumnHeader();
            elementOptionsLayout = new TableLayoutPanel();
            scopeNameData = new Controls.TextBoxData();
            columNameData = new Controls.TextBoxData();
            elementNameData = new Controls.TextBoxData();
            renderNillableTrue = new CheckBox();
            elementTypeData = new Controls.ComboBoxData();
            renderDataAsText = new CheckBox();
            renderDataAsCData = new CheckBox();
            renderDataAsXml = new CheckBox();
            renderAsAttribute = new CheckBox();
            renderAsElement = new CheckBox();
            bindingSchema = new BindingSource(components);
            schemaToolStrip = new ContextMenuStrip(components);
            addSchemaCommand = new ToolStripMenuItem();
            removeSchemaCommand = new ToolStripMenuItem();
            bindingElement = new BindingSource(components);
            schemaManagerLayout = new TableLayoutPanel();
            elementGroup = new GroupBox();
            elementLayout = new TableLayoutPanel();
            elementNillableGroup = new GroupBox();
            elementNillableLayout = new TableLayoutPanel();
            renderDataAsGroup = new GroupBox();
            renderDataAsLayout = new TableLayoutPanel();
            elementRenderGroup = new GroupBox();
            renderAsLayout = new TableLayoutPanel();
            schemaManagerLayout.SuspendLayout();
            elementGroup.SuspendLayout();
            elementLayout.SuspendLayout();
            elementOptionsLayout.SuspendLayout();
            elementNillableGroup.SuspendLayout();
            elementNillableLayout.SuspendLayout();
            renderDataAsGroup.SuspendLayout();
            renderDataAsLayout.SuspendLayout();
            elementRenderGroup.SuspendLayout();
            renderAsLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSchema).BeginInit();
            schemaToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingElement).BeginInit();
            SuspendLayout();
            // 
            // schemaManagerLayout
            // 
            schemaManagerLayout.ColumnCount = 1;
            schemaManagerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            schemaManagerLayout.Controls.Add(schemaTitleData, 0, 0);
            schemaManagerLayout.Controls.Add(schemaDescriptionData, 0, 1);
            schemaManagerLayout.Controls.Add(elementGroup, 0, 2);
            schemaManagerLayout.Dock = DockStyle.Fill;
            schemaManagerLayout.Location = new Point(0, 25);
            schemaManagerLayout.Name = "schemaManagerLayout";
            schemaManagerLayout.RowCount = 3;
            schemaManagerLayout.RowStyles.Add(new RowStyle());
            schemaManagerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            schemaManagerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            schemaManagerLayout.Size = new Size(802, 476);
            schemaManagerLayout.TabIndex = 4;
            // 
            // schemaTitleData
            // 
            schemaTitleData.AutoSize = true;
            schemaTitleData.Dock = DockStyle.Fill;
            schemaTitleData.HeaderText = "Scheme Title";
            schemaTitleData.Location = new Point(3, 3);
            schemaTitleData.Multiline = false;
            schemaTitleData.Name = "schemaTitleData";
            schemaTitleData.ReadOnly = false;
            schemaTitleData.Size = new Size(796, 44);
            schemaTitleData.TabIndex = 1;
            // 
            // schemaDescriptionData
            // 
            schemaDescriptionData.AutoSize = true;
            schemaDescriptionData.Dock = DockStyle.Fill;
            schemaDescriptionData.HeaderText = "Schma Description";
            schemaDescriptionData.Location = new Point(3, 53);
            schemaDescriptionData.Multiline = true;
            schemaDescriptionData.Name = "schemaDescriptionData";
            schemaDescriptionData.ReadOnly = false;
            schemaDescriptionData.Size = new Size(796, 79);
            schemaDescriptionData.TabIndex = 2;
            // 
            // elementGroup
            // 
            elementGroup.Controls.Add(elementLayout);
            elementGroup.Dock = DockStyle.Fill;
            elementGroup.Location = new Point(3, 138);
            elementGroup.Name = "elementGroup";
            elementGroup.Size = new Size(796, 335);
            elementGroup.TabIndex = 0;
            elementGroup.TabStop = false;
            elementGroup.Text = "Elements";
            // 
            // elementLayout
            // 
            elementLayout.ColumnCount = 2;
            elementLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            elementLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            elementLayout.Controls.Add(elementSelection, 0, 0);
            elementLayout.Controls.Add(elementOptionsLayout, 1, 0);
            elementLayout.Dock = DockStyle.Fill;
            elementLayout.Location = new Point(3, 19);
            elementLayout.Name = "elementLayout";
            elementLayout.RowCount = 1;
            elementLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            elementLayout.Size = new Size(790, 313);
            elementLayout.TabIndex = 0;
            // 
            // elementSelection
            // 
            elementSelection.CheckBoxes = true;
            elementSelection.Columns.AddRange(new ColumnHeader[] { columnName });
            elementSelection.Dock = DockStyle.Fill;
            listViewGroup1.Header = "Scope Name 1";
            listViewGroup1.Name = "sampleScope";
            elementSelection.Groups.AddRange(new ListViewGroup[] { listViewGroup1 });
            listViewItem1.Group = listViewGroup1;
            listViewItem1.StateImageIndex = 0;
            elementSelection.Items.AddRange(new ListViewItem[] { listViewItem1 });
            elementSelection.Location = new Point(3, 3);
            elementSelection.MultiSelect = false;
            elementSelection.Name = "elementSelection";
            elementSelection.Size = new Size(310, 307);
            elementSelection.TabIndex = 0;
            elementSelection.UseCompatibleStateImageBehavior = false;
            elementSelection.View = View.Details;
            elementSelection.ItemChecked += elementSelection_ItemChecked;
            elementSelection.SelectedIndexChanged += elementSelection_SelectedIndexChanged;
            // 
            // columnName
            // 
            columnName.Text = "Column";
            columnName.Width = 300;
            // 
            // elementOptionsLayout
            // 
            elementOptionsLayout.ColumnCount = 2;
            elementOptionsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            elementOptionsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            elementOptionsLayout.Controls.Add(elementNillableGroup, 1, 4);
            elementOptionsLayout.Controls.Add(elementTypeData, 1, 3);
            elementOptionsLayout.Controls.Add(renderDataAsGroup, 0, 4);
            elementOptionsLayout.Controls.Add(elementRenderGroup, 0, 3);
            elementOptionsLayout.Controls.Add(columNameData, 0, 1);
            elementOptionsLayout.Controls.Add(elementNameData, 0, 2);
            elementOptionsLayout.Controls.Add(scopeNameData, 0, 0);
            elementOptionsLayout.Dock = DockStyle.Fill;
            elementOptionsLayout.Location = new Point(319, 3);
            elementOptionsLayout.Name = "elementOptionsLayout";
            elementOptionsLayout.RowCount = 6;
            elementOptionsLayout.RowStyles.Add(new RowStyle());
            elementOptionsLayout.RowStyles.Add(new RowStyle());
            elementOptionsLayout.RowStyles.Add(new RowStyle());
            elementOptionsLayout.RowStyles.Add(new RowStyle());
            elementOptionsLayout.RowStyles.Add(new RowStyle());
            elementOptionsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            elementOptionsLayout.Size = new Size(468, 307);
            elementOptionsLayout.TabIndex = 6;
            // 
            // scopeNameData
            // 
            scopeNameData.AutoSize = true;
            elementOptionsLayout.SetColumnSpan(scopeNameData, 2);
            scopeNameData.Dock = DockStyle.Fill;
            scopeNameData.HeaderText = "Scope Name";
            scopeNameData.Location = new Point(3, 3);
            scopeNameData.Multiline = false;
            scopeNameData.Name = "scopeNameData";
            scopeNameData.ReadOnly = true;
            scopeNameData.Size = new Size(462, 44);
            scopeNameData.TabIndex = 0;
            // 
            // columNameData
            // 
            columNameData.AutoSize = true;
            elementOptionsLayout.SetColumnSpan(columNameData, 2);
            columNameData.Dock = DockStyle.Fill;
            columNameData.HeaderText = "Column Name";
            columNameData.Location = new Point(3, 53);
            columNameData.Multiline = false;
            columNameData.Name = "columNameData";
            columNameData.ReadOnly = true;
            columNameData.Size = new Size(462, 44);
            columNameData.TabIndex = 1;
            // 
            // elementNameData
            // 
            elementNameData.AutoSize = true;
            elementOptionsLayout.SetColumnSpan(elementNameData, 2);
            elementNameData.Dock = DockStyle.Fill;
            elementNameData.HeaderText = "Element/Attribute Name";
            elementNameData.Location = new Point(3, 103);
            elementNameData.Multiline = false;
            elementNameData.Name = "elementNameData";
            elementNameData.ReadOnly = false;
            elementNameData.Size = new Size(462, 44);
            elementNameData.TabIndex = 2;
            // 
            // elementNillableGroup
            // 
            elementNillableGroup.AutoSize = true;
            elementNillableGroup.Controls.Add(elementNillableLayout);
            elementNillableGroup.Dock = DockStyle.Fill;
            elementNillableGroup.Location = new Point(237, 206);
            elementNillableGroup.Name = "elementNillableGroup";
            elementNillableGroup.Size = new Size(228, 47);
            elementNillableGroup.TabIndex = 4;
            elementNillableGroup.TabStop = false;
            elementNillableGroup.Text = "render Nillable";
            // 
            // elementNillableLayout
            // 
            elementNillableLayout.AutoSize = true;
            elementNillableLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            elementNillableLayout.ColumnCount = 2;
            elementNillableLayout.ColumnStyles.Add(new ColumnStyle());
            elementNillableLayout.ColumnStyles.Add(new ColumnStyle());
            elementNillableLayout.Controls.Add(renderNillableTrue, 0, 0);
            elementNillableLayout.Dock = DockStyle.Fill;
            elementNillableLayout.Location = new Point(3, 19);
            elementNillableLayout.Name = "elementNillableLayout";
            elementNillableLayout.RowCount = 1;
            elementNillableLayout.RowStyles.Add(new RowStyle());
            elementNillableLayout.Size = new Size(222, 25);
            elementNillableLayout.TabIndex = 0;
            // 
            // renderNillableTrue
            // 
            renderNillableTrue.AutoSize = true;
            renderNillableTrue.Location = new Point(3, 3);
            renderNillableTrue.Name = "renderNillableTrue";
            renderNillableTrue.Size = new Size(48, 19);
            renderNillableTrue.TabIndex = 0;
            renderNillableTrue.Text = "True";
            renderNillableTrue.UseVisualStyleBackColor = true;
            // 
            // elementTypeData
            // 
            elementTypeData.AutoSize = true;
            elementTypeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            elementTypeData.Dock = DockStyle.Fill;
            elementTypeData.DropDownStyle = ComboBoxStyle.DropDown;
            elementTypeData.HeaderText = "render Type";
            elementTypeData.Location = new Point(237, 153);
            elementTypeData.Name = "elementTypeData";
            elementTypeData.ReadOnly = false;
            elementTypeData.Size = new Size(228, 47);
            elementTypeData.TabIndex = 3;
            // 
            // renderDataAsGroup
            // 
            renderDataAsGroup.AutoSize = true;
            renderDataAsGroup.Controls.Add(renderDataAsLayout);
            renderDataAsGroup.Dock = DockStyle.Fill;
            renderDataAsGroup.Location = new Point(3, 206);
            renderDataAsGroup.Name = "renderDataAsGroup";
            renderDataAsGroup.Size = new Size(228, 47);
            renderDataAsGroup.TabIndex = 6;
            renderDataAsGroup.TabStop = false;
            renderDataAsGroup.Text = "render Data As";
            // 
            // renderDataAsLayout
            // 
            renderDataAsLayout.AutoSize = true;
            renderDataAsLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            renderDataAsLayout.ColumnCount = 3;
            renderDataAsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            renderDataAsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            renderDataAsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            renderDataAsLayout.Controls.Add(renderDataAsText, 0, 0);
            renderDataAsLayout.Controls.Add(renderDataAsCData, 1, 0);
            renderDataAsLayout.Controls.Add(renderDataAsXml, 2, 0);
            renderDataAsLayout.Dock = DockStyle.Fill;
            renderDataAsLayout.Location = new Point(3, 19);
            renderDataAsLayout.Name = "renderDataAsLayout";
            renderDataAsLayout.RowCount = 1;
            renderDataAsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            renderDataAsLayout.Size = new Size(222, 25);
            renderDataAsLayout.TabIndex = 0;
            // 
            // renderDataAsText
            // 
            renderDataAsText.AutoSize = true;
            renderDataAsText.Location = new Point(3, 3);
            renderDataAsText.Name = "renderDataAsText";
            renderDataAsText.Size = new Size(47, 19);
            renderDataAsText.TabIndex = 0;
            renderDataAsText.Text = "Text";
            renderDataAsText.UseVisualStyleBackColor = true;
            // 
            // renderDataAsCData
            // 
            renderDataAsCData.AutoSize = true;
            renderDataAsCData.Location = new Point(76, 3);
            renderDataAsCData.Name = "renderDataAsCData";
            renderDataAsCData.Size = new Size(58, 19);
            renderDataAsCData.TabIndex = 1;
            renderDataAsCData.Text = "CData";
            renderDataAsCData.UseVisualStyleBackColor = true;
            // 
            // renderDataAsXml
            // 
            renderDataAsXml.AutoSize = true;
            renderDataAsXml.Location = new Point(149, 3);
            renderDataAsXml.Name = "renderDataAsXml";
            renderDataAsXml.Size = new Size(50, 19);
            renderDataAsXml.TabIndex = 2;
            renderDataAsXml.Text = "XML";
            renderDataAsXml.UseVisualStyleBackColor = true;
            // 
            // elementRenderGroup
            // 
            elementRenderGroup.AutoSize = true;
            elementRenderGroup.Controls.Add(renderAsLayout);
            elementRenderGroup.Dock = DockStyle.Fill;
            elementRenderGroup.Location = new Point(3, 153);
            elementRenderGroup.Name = "elementRenderGroup";
            elementRenderGroup.Size = new Size(228, 47);
            elementRenderGroup.TabIndex = 5;
            elementRenderGroup.TabStop = false;
            elementRenderGroup.Text = "render As";
            // 
            // renderAsLayout
            // 
            renderAsLayout.AutoSize = true;
            renderAsLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            renderAsLayout.ColumnCount = 3;
            renderAsLayout.ColumnStyles.Add(new ColumnStyle());
            renderAsLayout.ColumnStyles.Add(new ColumnStyle());
            renderAsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            renderAsLayout.Controls.Add(renderAsAttribute, 1, 0);
            renderAsLayout.Controls.Add(renderAsElement, 0, 0);
            renderAsLayout.Dock = DockStyle.Fill;
            renderAsLayout.Location = new Point(3, 19);
            renderAsLayout.Name = "renderAsLayout";
            renderAsLayout.RowCount = 1;
            renderAsLayout.RowStyles.Add(new RowStyle());
            renderAsLayout.Size = new Size(222, 25);
            renderAsLayout.TabIndex = 0;
            // 
            // renderAsAttribute
            // 
            renderAsAttribute.AutoSize = true;
            renderAsAttribute.Location = new Point(78, 3);
            renderAsAttribute.Name = "renderAsAttribute";
            renderAsAttribute.Size = new Size(73, 19);
            renderAsAttribute.TabIndex = 2;
            renderAsAttribute.Text = "Attribute";
            renderAsAttribute.UseVisualStyleBackColor = true;
            // 
            // renderAsElement
            // 
            renderAsElement.AutoSize = true;
            renderAsElement.Location = new Point(3, 3);
            renderAsElement.Name = "renderAsElement";
            renderAsElement.Size = new Size(69, 19);
            renderAsElement.TabIndex = 3;
            renderAsElement.Text = "Element";
            renderAsElement.UseVisualStyleBackColor = true;
            // 
            // schemaToolStrip
            // 
            schemaToolStrip.Items.AddRange(new ToolStripItem[] { addSchemaCommand, removeSchemaCommand });
            schemaToolStrip.Name = "schemaToolStrip";
            schemaToolStrip.Size = new Size(160, 48);
            // 
            // addSchemaCommand
            // 
            addSchemaCommand.Image = Properties.Resources.NewXMLSchema;
            addSchemaCommand.Name = "addSchemaCommand";
            addSchemaCommand.Size = new Size(159, 22);
            addSchemaCommand.Text = "add Schema";
            addSchemaCommand.Click += addSchemaCommand_Click;
            // 
            // removeSchemaCommand
            // 
            removeSchemaCommand.Image = Properties.Resources.DeleteXMLSchema;
            removeSchemaCommand.Name = "removeSchemaCommand";
            removeSchemaCommand.Size = new Size(159, 22);
            removeSchemaCommand.Text = "remove Schema";
            removeSchemaCommand.Click += removeSchemaCommand_Click;
            // 
            // bindingElement
            // 
            bindingElement.AddingNew += bindingElement_AddingNew;
            // 
            // SchemaManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(802, 501);
            Controls.Add(schemaManagerLayout);
            Name = "SchemaManager";
            Text = "Scripting Schema Manager";
            Load += SchemaManager_Load;
            Controls.SetChildIndex(schemaManagerLayout, 0);
            schemaManagerLayout.ResumeLayout(false);
            schemaManagerLayout.PerformLayout();
            elementGroup.ResumeLayout(false);
            elementLayout.ResumeLayout(false);
            elementOptionsLayout.ResumeLayout(false);
            elementOptionsLayout.PerformLayout();
            elementNillableGroup.ResumeLayout(false);
            elementNillableGroup.PerformLayout();
            elementNillableLayout.ResumeLayout(false);
            elementNillableLayout.PerformLayout();
            renderDataAsGroup.ResumeLayout(false);
            renderDataAsGroup.PerformLayout();
            renderDataAsLayout.ResumeLayout(false);
            renderDataAsLayout.PerformLayout();
            elementRenderGroup.ResumeLayout(false);
            elementRenderGroup.PerformLayout();
            renderAsLayout.ResumeLayout(false);
            renderAsLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSchema).EndInit();
            schemaToolStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)bindingElement).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private BindingSource bindingSchema;
        private Controls.TextBoxData schemaTitleData;
        private Controls.TextBoxData schemaDescriptionData;
        private ContextMenuStrip schemaToolStrip;
        private ToolStripMenuItem addSchemaCommand;
        private ToolStripMenuItem removeSchemaCommand;
        private GroupBox elementGroup;
        private ListView elementSelection;
        private ColumnHeader columnName;
        private TableLayoutPanel elementLayout;
        private Controls.TextBoxData scopeNameData;
        private Controls.TextBoxData columNameData;
        private Controls.TextBoxData elementNameData;
        private Controls.ComboBoxData elementTypeData;
        private TableLayoutPanel elementNillableLayout;
        private CheckBox renderNillableTrue;
        private TableLayoutPanel renderLayout;
        private GroupBox elementRenderGroup;
        private CheckBox renderDataAsText;
        private CheckBox renderDataAsCData;
        private CheckBox renderDataAsXml;
        private BindingSource bindingElement;
        private CheckBox renderAsAttribute;
        private CheckBox renderAsElement;
        private TableLayoutPanel elementOptionsLayout;
    }
}