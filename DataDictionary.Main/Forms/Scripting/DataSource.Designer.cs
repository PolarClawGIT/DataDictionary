namespace DataDictionary.Main.Forms.Scripting
{
    partial class DataSource
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
            TableLayoutPanel dataSourceLayout;
            TableLayoutPanel objectCommandLayout;
            titleData = new DataDictionary.Main.Controls.TextBoxData();
            descriptionData = new DataDictionary.Main.Controls.TextBoxData();
            objectData = new DataGridView();
            isInModelData = new CheckBox();
            objectPathData = new DataDictionary.Main.Controls.TextBoxData();
            objectTitleData = new DataDictionary.Main.Controls.TextBoxData();
            objectScopeData = new DataDictionary.Main.Controls.TextBoxData();
            selectObjectCommand = new Button();
            newObjectCommand = new Button();
            bindingDataSource = new BindingSource(components);
            bindingDataObject = new BindingSource(components);
            DataObjectPathColumn = new DataGridViewTextBoxColumn();
            dataSourceLayout = new TableLayoutPanel();
            objectCommandLayout = new TableLayoutPanel();
            dataSourceLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)objectData).BeginInit();
            objectCommandLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingDataSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingDataObject).BeginInit();
            SuspendLayout();
            // 
            // dataSourceLayout
            // 
            dataSourceLayout.ColumnCount = 2;
            dataSourceLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            dataSourceLayout.ColumnStyles.Add(new ColumnStyle());
            dataSourceLayout.Controls.Add(titleData, 0, 0);
            dataSourceLayout.Controls.Add(descriptionData, 0, 1);
            dataSourceLayout.Controls.Add(objectData, 0, 2);
            dataSourceLayout.Controls.Add(isInModelData, 1, 3);
            dataSourceLayout.Controls.Add(objectPathData, 0, 3);
            dataSourceLayout.Controls.Add(objectTitleData, 0, 4);
            dataSourceLayout.Controls.Add(objectScopeData, 1, 4);
            dataSourceLayout.Controls.Add(objectCommandLayout, 0, 5);
            dataSourceLayout.Dock = DockStyle.Fill;
            dataSourceLayout.Location = new Point(0, 25);
            dataSourceLayout.Name = "dataSourceLayout";
            dataSourceLayout.RowCount = 6;
            dataSourceLayout.RowStyles.Add(new RowStyle());
            dataSourceLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            dataSourceLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            dataSourceLayout.RowStyles.Add(new RowStyle());
            dataSourceLayout.RowStyles.Add(new RowStyle());
            dataSourceLayout.RowStyles.Add(new RowStyle());
            dataSourceLayout.Size = new Size(469, 477);
            dataSourceLayout.TabIndex = 4;
            // 
            // titleData
            // 
            titleData.AutoSize = true;
            dataSourceLayout.SetColumnSpan(titleData, 2);
            titleData.Dock = DockStyle.Fill;
            titleData.HeaderText = "Data Source Title";
            titleData.Location = new Point(3, 3);
            titleData.Multiline = false;
            titleData.Name = "titleData";
            titleData.ReadOnly = false;
            titleData.Size = new Size(463, 44);
            titleData.TabIndex = 0;
            titleData.WordWrap = true;
            // 
            // descriptionData
            // 
            descriptionData.AutoSize = true;
            dataSourceLayout.SetColumnSpan(descriptionData, 2);
            descriptionData.Dock = DockStyle.Fill;
            descriptionData.HeaderText = "Data Source Description";
            descriptionData.Location = new Point(3, 53);
            descriptionData.Multiline = true;
            descriptionData.Name = "descriptionData";
            descriptionData.ReadOnly = false;
            descriptionData.Size = new Size(463, 81);
            descriptionData.TabIndex = 1;
            descriptionData.WordWrap = true;
            // 
            // objectData
            // 
            objectData.AllowUserToAddRows = false;
            objectData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            objectData.Columns.AddRange(new DataGridViewColumn[] { DataObjectPathColumn });
            dataSourceLayout.SetColumnSpan(objectData, 2);
            objectData.Dock = DockStyle.Fill;
            objectData.Location = new Point(3, 140);
            objectData.Name = "objectData";
            objectData.ReadOnly = true;
            objectData.Size = new Size(463, 198);
            objectData.TabIndex = 2;
            // 
            // isInModelData
            // 
            isInModelData.AutoSize = true;
            isInModelData.Location = new Point(346, 344);
            isInModelData.Name = "isInModelData";
            isInModelData.Size = new Size(73, 19);
            isInModelData.TabIndex = 4;
            isInModelData.Text = "In Model";
            isInModelData.UseVisualStyleBackColor = true;
            // 
            // objectPathData
            // 
            objectPathData.AutoSize = true;
            objectPathData.Dock = DockStyle.Fill;
            objectPathData.HeaderText = "Path";
            objectPathData.Location = new Point(3, 344);
            objectPathData.Multiline = false;
            objectPathData.Name = "objectPathData";
            objectPathData.ReadOnly = false;
            objectPathData.Size = new Size(337, 44);
            objectPathData.TabIndex = 6;
            objectPathData.WordWrap = true;
            // 
            // objectTitleData
            // 
            objectTitleData.AutoSize = true;
            objectTitleData.Dock = DockStyle.Fill;
            objectTitleData.HeaderText = "Title";
            objectTitleData.Location = new Point(3, 394);
            objectTitleData.Multiline = false;
            objectTitleData.Name = "objectTitleData";
            objectTitleData.ReadOnly = true;
            objectTitleData.Size = new Size(337, 44);
            objectTitleData.TabIndex = 7;
            objectTitleData.WordWrap = true;
            // 
            // objectScopeData
            // 
            objectScopeData.AutoSize = true;
            objectScopeData.Dock = DockStyle.Fill;
            objectScopeData.HeaderText = "Scope";
            objectScopeData.Location = new Point(346, 394);
            objectScopeData.Multiline = false;
            objectScopeData.Name = "objectScopeData";
            objectScopeData.ReadOnly = true;
            objectScopeData.Size = new Size(120, 44);
            objectScopeData.TabIndex = 8;
            objectScopeData.WordWrap = true;
            // 
            // objectCommandLayout
            // 
            objectCommandLayout.AutoSize = true;
            objectCommandLayout.ColumnCount = 3;
            dataSourceLayout.SetColumnSpan(objectCommandLayout, 2);
            objectCommandLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            objectCommandLayout.ColumnStyles.Add(new ColumnStyle());
            objectCommandLayout.ColumnStyles.Add(new ColumnStyle());
            objectCommandLayout.Controls.Add(selectObjectCommand, 2, 0);
            objectCommandLayout.Controls.Add(newObjectCommand, 1, 0);
            objectCommandLayout.Dock = DockStyle.Fill;
            objectCommandLayout.Location = new Point(3, 444);
            objectCommandLayout.Name = "objectCommandLayout";
            objectCommandLayout.RowCount = 1;
            objectCommandLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            objectCommandLayout.Size = new Size(463, 30);
            objectCommandLayout.TabIndex = 9;
            // 
            // selectObjectCommand
            // 
            selectObjectCommand.Location = new Point(385, 3);
            selectObjectCommand.Name = "selectObjectCommand";
            selectObjectCommand.Size = new Size(75, 23);
            selectObjectCommand.TabIndex = 1;
            selectObjectCommand.Text = "Select";
            selectObjectCommand.TextImageRelation = TextImageRelation.ImageBeforeText;
            selectObjectCommand.UseVisualStyleBackColor = true;
            selectObjectCommand.Click += SelectObjectCommand_Click;
            // 
            // newObjectCommand
            // 
            newObjectCommand.Location = new Point(304, 3);
            newObjectCommand.Name = "newObjectCommand";
            newObjectCommand.Size = new Size(75, 23);
            newObjectCommand.TabIndex = 0;
            newObjectCommand.Text = "New";
            newObjectCommand.TextImageRelation = TextImageRelation.ImageBeforeText;
            newObjectCommand.UseVisualStyleBackColor = true;
            newObjectCommand.Click += NewObjectCommand_Click;
            // 
            // bindingDataObject
            // 
            bindingDataObject.AddingNew += BindingDataObject_AddingNew;
            bindingDataObject.CurrentItemChanged += BindingDataObject_CurrentItemChanged;
            // 
            // DataObjectPathColumn
            // 
            DataObjectPathColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataObjectPathColumn.DataPropertyName = "ObjectPath";
            DataObjectPathColumn.HeaderText = "Path";
            DataObjectPathColumn.Name = "DataObjectPathColumn";
            DataObjectPathColumn.ReadOnly = true;
            // 
            // DataSource
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(469, 502);
            Controls.Add(dataSourceLayout);
            Name = "DataSource";
            Text = "DataSource";
            Load += DataSource_Load;
            Controls.SetChildIndex(dataSourceLayout, 0);
            dataSourceLayout.ResumeLayout(false);
            dataSourceLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)objectData).EndInit();
            objectCommandLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)bindingDataSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingDataObject).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData titleData;
        private Controls.TextBoxData descriptionData;
        private DataGridView objectData;
        private CheckBox isInModelData;
        private BindingSource bindingDataSource;
        private BindingSource bindingDataObject;
        private Controls.TextBoxData objectPathData;
        private Controls.TextBoxData objectTitleData;
        private Controls.TextBoxData objectScopeData;
        private Button newObjectCommand;
        private Button selectObjectCommand;
        private DataGridViewTextBoxColumn DataObjectPathColumn;
    }
}