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
            titleData = new DataDictionary.Main.Controls.TextBoxData();
            descriptionData = new DataDictionary.Main.Controls.TextBoxData();
            objectData = new DataGridView();
            DataObjectPathColumn = new DataGridViewTextBoxColumn();
            isInModelData = new CheckBox();
            bindingDataSource = new BindingSource(components);
            objectPathData = new DataDictionary.Main.Controls.SelectTextBoxData();
            dataSourceLayout = new TableLayoutPanel();
            dataSourceLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)objectData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingDataSource).BeginInit();
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
            dataSourceLayout.Dock = DockStyle.Fill;
            dataSourceLayout.Location = new Point(0, 25);
            dataSourceLayout.Name = "dataSourceLayout";
            dataSourceLayout.RowCount = 4;
            dataSourceLayout.RowStyles.Add(new RowStyle());
            dataSourceLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            dataSourceLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            dataSourceLayout.RowStyles.Add(new RowStyle());
            dataSourceLayout.Size = new Size(469, 351);
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
            descriptionData.Size = new Size(463, 69);
            descriptionData.TabIndex = 1;
            descriptionData.WordWrap = true;
            // 
            // objectData
            // 
            objectData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            objectData.Columns.AddRange(new DataGridViewColumn[] { DataObjectPathColumn });
            dataSourceLayout.SetColumnSpan(objectData, 2);
            objectData.Dock = DockStyle.Fill;
            objectData.Location = new Point(3, 128);
            objectData.Name = "objectData";
            objectData.Size = new Size(463, 169);
            objectData.TabIndex = 2;
            // 
            // DataObjectPathColumn
            // 
            DataObjectPathColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataObjectPathColumn.DataPropertyName = "DataObjectPath";
            DataObjectPathColumn.HeaderText = "Path";
            DataObjectPathColumn.Name = "DataObjectPathColumn";
            // 
            // isInModelData
            // 
            isInModelData.AutoSize = true;
            isInModelData.Location = new Point(393, 303);
            isInModelData.Name = "isInModelData";
            isInModelData.Size = new Size(73, 19);
            isInModelData.TabIndex = 4;
            isInModelData.Text = "In Model";
            isInModelData.UseVisualStyleBackColor = true;
            // 
            // objectPathData
            // 
            objectPathData.AutoSize = true;
            objectPathData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            objectPathData.Dock = DockStyle.Fill;
            objectPathData.HeaderText = "Path";
            objectPathData.Location = new Point(3, 303);
            objectPathData.Name = "objectPathData";
            objectPathData.ReadOnly = false;
            objectPathData.SelectIcon = Properties.Resources.XPath;
            objectPathData.Size = new Size(384, 45);
            objectPathData.TabIndex = 5;
            // 
            // DataSource
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(469, 376);
            Controls.Add(dataSourceLayout);
            Name = "DataSource";
            Text = "DataSource";
            Load += DataSource_Load;
            Controls.SetChildIndex(dataSourceLayout, 0);
            dataSourceLayout.ResumeLayout(false);
            dataSourceLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)objectData).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingDataSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData titleData;
        private Controls.TextBoxData descriptionData;
        private DataGridView objectData;
        private DataGridViewTextBoxColumn DataObjectPathColumn;
        private CheckBox isInModelData;
        private BindingSource bindingDataSource;
        private Controls.SelectTextBoxData objectPathData;
    }
}