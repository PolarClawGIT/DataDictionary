﻿namespace DataDictionary.Main.Forms.Model
{
    partial class ModelSubjectArea
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
            subjectAreaTitleData = new Controls.TextBoxData();
            subjectAreaDescriptionData = new Controls.TextBoxData();
            attributeTab = new TabPage();
            attributeData = new DataGridView();
            attributeTitleColumn = new DataGridViewTextBoxColumn();
            attributeDescriptionColumn = new DataGridViewTextBoxColumn();
            entityTab = new TabPage();
            entityData = new DataGridView();
            entityTitleColumn = new DataGridViewTextBoxColumn();
            entityDescriptionColumn = new DataGridViewTextBoxColumn();
            memberNameData = new Controls.TextBoxData();
            bindingSubject = new BindingSource(components);
            bindingEntity = new BindingSource(components);
            bindingAttribute = new BindingSource(components);
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
            // 
            // attributeDescriptionColumn
            // 
            attributeDescriptionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            attributeDescriptionColumn.DataPropertyName = "AttributeDescription";
            attributeDescriptionColumn.FillWeight = 60F;
            attributeDescriptionColumn.HeaderText = "Attribute Description";
            attributeDescriptionColumn.Name = "attributeDescriptionColumn";
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
            // 
            // entityDescriptionColumn
            // 
            entityDescriptionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            entityDescriptionColumn.DataPropertyName = "EntityDescription";
            entityDescriptionColumn.FillWeight = 60F;
            entityDescriptionColumn.HeaderText = "Entity Description";
            entityDescriptionColumn.Name = "entityDescriptionColumn";
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
            // ModelSubjectArea
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(379, 463);
            Controls.Add(subjectAreaLayout);
            Name = "ModelSubjectArea";
            Text = "DomainSubjectArea";
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
    }
}