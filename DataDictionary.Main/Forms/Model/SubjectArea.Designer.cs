namespace DataDictionary.Main.Forms.Model
{
    partial class SubjectArea
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
            TableLayoutPanel subjectAreaLayout;
            TabControl subjectAreaTab;
            subjectAreaTitleData = new DataDictionary.Main.Controls.TextBoxData();
            subjectAreaDescriptionData = new DataDictionary.Main.Controls.TextBoxData();
            attributeTab = new TabPage();
            attributeData = new DataGridView();
            attributeTitleColumn = new DataGridViewTextBoxColumn();
            attributeDescriptionColumn = new DataGridViewTextBoxColumn();
            entityTab = new TabPage();
            entityData = new DataGridView();
            entityTitleColumn = new DataGridViewTextBoxColumn();
            entityDescriptionColumn = new DataGridViewTextBoxColumn();
            memberNameData = new DataDictionary.Main.Controls.TextBoxData();
            bindingSubject = new BindingSource(components);
            bindingEntity = new BindingSource(components);
            bindingAttribute = new BindingSource(components);
            processTab = new TabPage();
            processGrid = new DataGridView();
            processTitleColumn = new DataGridViewTextBoxColumn();
            processDescriptionColumn = new DataGridViewTextBoxColumn();
            bindingProcess = new BindingSource(components);
            subjectAreaLayout = new TableLayoutPanel();
            subjectAreaTab = new TabControl();
            subjectAreaLayout.SuspendLayout();
            subjectAreaTab.SuspendLayout();
            attributeTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)attributeData).BeginInit();
            entityTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)entityData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSubject).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingEntity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingAttribute).BeginInit();
            processTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)processGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingProcess).BeginInit();
            SuspendLayout();
            // 
            // subjectAreaLayout
            // 
            subjectAreaLayout.ColumnCount = 1;
            subjectAreaLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            subjectAreaLayout.Controls.Add(subjectAreaTitleData, 0, 0);
            subjectAreaLayout.Controls.Add(subjectAreaDescriptionData, 0, 1);
            subjectAreaLayout.Controls.Add(subjectAreaTab, 0, 3);
            subjectAreaLayout.Controls.Add(memberNameData, 0, 2);
            subjectAreaLayout.Dock = DockStyle.Fill;
            subjectAreaLayout.Location = new Point(0, 25);
            subjectAreaLayout.Name = "subjectAreaLayout";
            subjectAreaLayout.RowCount = 4;
            subjectAreaLayout.RowStyles.Add(new RowStyle());
            subjectAreaLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            subjectAreaLayout.RowStyles.Add(new RowStyle());
            subjectAreaLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            subjectAreaLayout.Size = new Size(379, 438);
            subjectAreaLayout.TabIndex = 0;
            // 
            // subjectAreaTitleData
            // 
            subjectAreaTitleData.AutoSize = true;
            subjectAreaTitleData.Dock = DockStyle.Fill;
            subjectAreaTitleData.HeaderText = "Subject Area Title";
            subjectAreaTitleData.Location = new Point(3, 3);
            subjectAreaTitleData.Multiline = false;
            subjectAreaTitleData.Name = "subjectAreaTitleData";
            subjectAreaTitleData.ReadOnly = false;
            subjectAreaTitleData.Size = new Size(373, 44);
            subjectAreaTitleData.TabIndex = 0;
            subjectAreaTitleData.WordWrap = true;
            // 
            // subjectAreaDescriptionData
            // 
            subjectAreaDescriptionData.AutoSize = true;
            subjectAreaDescriptionData.Dock = DockStyle.Fill;
            subjectAreaDescriptionData.HeaderText = "Subject Area Description";
            subjectAreaDescriptionData.Location = new Point(3, 53);
            subjectAreaDescriptionData.Multiline = true;
            subjectAreaDescriptionData.Name = "subjectAreaDescriptionData";
            subjectAreaDescriptionData.ReadOnly = false;
            subjectAreaDescriptionData.Size = new Size(373, 129);
            subjectAreaDescriptionData.TabIndex = 1;
            subjectAreaDescriptionData.WordWrap = true;
            // 
            // subjectAreaTab
            // 
            subjectAreaTab.Controls.Add(attributeTab);
            subjectAreaTab.Controls.Add(entityTab);
            subjectAreaTab.Controls.Add(processTab);
            subjectAreaTab.Dock = DockStyle.Fill;
            subjectAreaTab.Location = new Point(3, 238);
            subjectAreaTab.Name = "subjectAreaTab";
            subjectAreaTab.SelectedIndex = 0;
            subjectAreaTab.Size = new Size(373, 197);
            subjectAreaTab.TabIndex = 2;
            // 
            // attributeTab
            // 
            attributeTab.BackColor = SystemColors.Control;
            attributeTab.Controls.Add(attributeData);
            attributeTab.Location = new Point(4, 24);
            attributeTab.Name = "attributeTab";
            attributeTab.Padding = new Padding(3);
            attributeTab.Size = new Size(365, 169);
            attributeTab.TabIndex = 0;
            attributeTab.Text = "Attributes";
            // 
            // attributeData
            // 
            attributeData.AllowUserToAddRows = false;
            attributeData.AllowUserToDeleteRows = false;
            attributeData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            attributeData.Columns.AddRange(new DataGridViewColumn[] { attributeTitleColumn, attributeDescriptionColumn });
            attributeData.Dock = DockStyle.Fill;
            attributeData.Location = new Point(3, 3);
            attributeData.Name = "attributeData";
            attributeData.ReadOnly = true;
            attributeData.Size = new Size(359, 163);
            attributeData.TabIndex = 0;
            // 
            // attributeTitleColumn
            // 
            attributeTitleColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            attributeTitleColumn.DataPropertyName = "AttributeTitle";
            attributeTitleColumn.FillWeight = 40F;
            attributeTitleColumn.HeaderText = "Attribute Title";
            attributeTitleColumn.Name = "attributeTitleColumn";
            attributeTitleColumn.ReadOnly = true;
            // 
            // attributeDescriptionColumn
            // 
            attributeDescriptionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            attributeDescriptionColumn.DataPropertyName = "AttributeDescription";
            attributeDescriptionColumn.FillWeight = 60F;
            attributeDescriptionColumn.HeaderText = "Attribute Description";
            attributeDescriptionColumn.Name = "attributeDescriptionColumn";
            attributeDescriptionColumn.ReadOnly = true;
            // 
            // entityTab
            // 
            entityTab.BackColor = SystemColors.Control;
            entityTab.Controls.Add(entityData);
            entityTab.Location = new Point(4, 24);
            entityTab.Name = "entityTab";
            entityTab.Padding = new Padding(3);
            entityTab.Size = new Size(365, 169);
            entityTab.TabIndex = 1;
            entityTab.Text = "Entities";
            // 
            // entityData
            // 
            entityData.AllowUserToAddRows = false;
            entityData.AllowUserToDeleteRows = false;
            entityData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            entityData.Columns.AddRange(new DataGridViewColumn[] { entityTitleColumn, entityDescriptionColumn });
            entityData.Dock = DockStyle.Fill;
            entityData.Location = new Point(3, 3);
            entityData.Name = "entityData";
            entityData.ReadOnly = true;
            entityData.Size = new Size(359, 163);
            entityData.TabIndex = 0;
            // 
            // entityTitleColumn
            // 
            entityTitleColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            entityTitleColumn.DataPropertyName = "EntityTitle";
            entityTitleColumn.FillWeight = 40F;
            entityTitleColumn.HeaderText = "Entity Title";
            entityTitleColumn.Name = "entityTitleColumn";
            entityTitleColumn.ReadOnly = true;
            // 
            // entityDescriptionColumn
            // 
            entityDescriptionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            entityDescriptionColumn.DataPropertyName = "EntityDescription";
            entityDescriptionColumn.FillWeight = 60F;
            entityDescriptionColumn.HeaderText = "Entity Description";
            entityDescriptionColumn.Name = "entityDescriptionColumn";
            entityDescriptionColumn.ReadOnly = true;
            // 
            // memberNameData
            // 
            memberNameData.AutoSize = true;
            memberNameData.Dock = DockStyle.Fill;
            memberNameData.HeaderText = "Subject Member Name";
            memberNameData.Location = new Point(3, 188);
            memberNameData.Multiline = false;
            memberNameData.Name = "memberNameData";
            memberNameData.ReadOnly = false;
            memberNameData.Size = new Size(373, 44);
            memberNameData.TabIndex = 3;
            memberNameData.WordWrap = true;
            memberNameData.Validating += MemberNameData_Validating;
            // 
            // processTab
            // 
            processTab.Controls.Add(processGrid);
            processTab.Location = new Point(4, 24);
            processTab.Name = "processTab";
            processTab.Padding = new Padding(3);
            processTab.Size = new Size(365, 169);
            processTab.TabIndex = 2;
            processTab.Text = "Processes";
            processTab.UseVisualStyleBackColor = true;
            // 
            // processGrid
            // 
            processGrid.AllowUserToAddRows = false;
            processGrid.AllowUserToDeleteRows = false;
            processGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            processGrid.Columns.AddRange(new DataGridViewColumn[] { processTitleColumn, processDescriptionColumn });
            processGrid.Dock = DockStyle.Fill;
            processGrid.Location = new Point(3, 3);
            processGrid.Name = "processGrid";
            processGrid.ReadOnly = true;
            processGrid.Size = new Size(359, 163);
            processGrid.TabIndex = 0;
            // 
            // processTitleColumn
            // 
            processTitleColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            processTitleColumn.FillWeight = 40F;
            processTitleColumn.HeaderText = "Process Title";
            processTitleColumn.Name = "processTitleColumn";
            processTitleColumn.ReadOnly = true;
            // 
            // processDescriptionColumn
            // 
            processDescriptionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            processDescriptionColumn.FillWeight = 60F;
            processDescriptionColumn.HeaderText = "Process Description";
            processDescriptionColumn.Name = "processDescriptionColumn";
            processDescriptionColumn.ReadOnly = true;
            // 
            // SubjectArea
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(379, 463);
            Controls.Add(subjectAreaLayout);
            Name = "SubjectArea";
            Text = "SubjectArea";
            Load += DomainSubjectArea_Load;
            Controls.SetChildIndex(subjectAreaLayout, 0);
            subjectAreaLayout.ResumeLayout(false);
            subjectAreaLayout.PerformLayout();
            subjectAreaTab.ResumeLayout(false);
            attributeTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)attributeData).EndInit();
            entityTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)entityData).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSubject).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingEntity).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingAttribute).EndInit();
            processTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)processGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingProcess).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataDictionary.Main.Controls.TextBoxData subjectAreaTitleData;
        private DataDictionary.Main.Controls.TextBoxData subjectAreaDescriptionData;
        private TabPage attributeTab;
        private DataGridView attributeData;
        private TabPage entityTab;
        private DataGridView entityData;
        private DataGridViewTextBoxColumn attributeTitleColumn;
        private DataGridViewTextBoxColumn attributeDescriptionColumn;
        private DataGridViewTextBoxColumn entityTitleColumn;
        private DataGridViewTextBoxColumn entityDescriptionColumn;
        private BindingSource bindingSubject;
        private DataDictionary.Main.Controls.TextBoxData memberNameData;
        private BindingSource bindingEntity;
        private BindingSource bindingAttribute;
        private TabPage processTab;
        private DataGridView processGrid;
        private DataGridViewTextBoxColumn processTitleColumn;
        private DataGridViewTextBoxColumn processDescriptionColumn;
        private BindingSource bindingProcess;
    }
}