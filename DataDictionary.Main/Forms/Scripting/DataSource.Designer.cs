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
            TableLayoutPanel dataSourceLayout;
            dataSourceTitleData = new DataDictionary.Main.Controls.TextBoxData();
            descriptionData = new DataDictionary.Main.Controls.TextBoxData();
            objectData = new DataGridView();
            DataObjectPathColumn = new DataGridViewTextBoxColumn();
            objectPathData = new DataDictionary.Main.Controls.TextBoxData();
            isInModelData = new CheckBox();
            selectPathCommand = new Button();
            dataSourceLayout = new TableLayoutPanel();
            dataSourceLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)objectData).BeginInit();
            SuspendLayout();
            // 
            // dataSourceLayout
            // 
            dataSourceLayout.ColumnCount = 2;
            dataSourceLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            dataSourceLayout.ColumnStyles.Add(new ColumnStyle());
            dataSourceLayout.Controls.Add(dataSourceTitleData, 0, 0);
            dataSourceLayout.Controls.Add(descriptionData, 0, 1);
            dataSourceLayout.Controls.Add(objectData, 0, 2);
            dataSourceLayout.Controls.Add(objectPathData, 0, 3);
            dataSourceLayout.Controls.Add(isInModelData, 1, 3);
            dataSourceLayout.Controls.Add(selectPathCommand, 1, 4);
            dataSourceLayout.Dock = DockStyle.Fill;
            dataSourceLayout.Location = new Point(0, 25);
            dataSourceLayout.Name = "dataSourceLayout";
            dataSourceLayout.RowCount = 5;
            dataSourceLayout.RowStyles.Add(new RowStyle());
            dataSourceLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            dataSourceLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            dataSourceLayout.RowStyles.Add(new RowStyle());
            dataSourceLayout.RowStyles.Add(new RowStyle());
            dataSourceLayout.Size = new Size(469, 351);
            dataSourceLayout.TabIndex = 4;
            // 
            // dataSourceTitleData
            // 
            dataSourceTitleData.AutoSize = true;
            dataSourceLayout.SetColumnSpan(dataSourceTitleData, 2);
            dataSourceTitleData.Dock = DockStyle.Fill;
            dataSourceTitleData.HeaderText = "Data Source Title";
            dataSourceTitleData.Location = new Point(3, 3);
            dataSourceTitleData.Multiline = false;
            dataSourceTitleData.Name = "dataSourceTitleData";
            dataSourceTitleData.ReadOnly = false;
            dataSourceTitleData.Size = new Size(463, 44);
            dataSourceTitleData.TabIndex = 0;
            dataSourceTitleData.WordWrap = true;
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
            descriptionData.Size = new Size(463, 68);
            descriptionData.TabIndex = 1;
            descriptionData.WordWrap = true;
            // 
            // objectData
            // 
            objectData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            objectData.Columns.AddRange(new DataGridViewColumn[] { DataObjectPathColumn });
            dataSourceLayout.SetColumnSpan(objectData, 2);
            objectData.Dock = DockStyle.Fill;
            objectData.Location = new Point(3, 127);
            objectData.Name = "objectData";
            objectData.Size = new Size(463, 166);
            objectData.TabIndex = 2;
            // 
            // DataObjectPathColumn
            // 
            DataObjectPathColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataObjectPathColumn.DataPropertyName = "DataObjectPath";
            DataObjectPathColumn.HeaderText = "Path";
            DataObjectPathColumn.Name = "DataObjectPathColumn";
            // 
            // objectPathData
            // 
            objectPathData.AutoSize = true;
            objectPathData.Dock = DockStyle.Fill;
            objectPathData.HeaderText = "Path";
            objectPathData.Location = new Point(3, 299);
            objectPathData.Multiline = false;
            objectPathData.Name = "objectPathData";
            objectPathData.ReadOnly = false;
            dataSourceLayout.SetRowSpan(objectPathData, 2);
            objectPathData.Size = new Size(382, 49);
            objectPathData.TabIndex = 3;
            objectPathData.WordWrap = true;
            // 
            // isInModelData
            // 
            isInModelData.AutoSize = true;
            isInModelData.Location = new Point(391, 299);
            isInModelData.Name = "isInModelData";
            isInModelData.Size = new Size(73, 19);
            isInModelData.TabIndex = 4;
            isInModelData.Text = "In Model";
            isInModelData.UseVisualStyleBackColor = true;
            // 
            // selectPathCommand
            // 
            selectPathCommand.Location = new Point(391, 324);
            selectPathCommand.Name = "selectPathCommand";
            selectPathCommand.Size = new Size(75, 23);
            selectPathCommand.TabIndex = 5;
            selectPathCommand.Text = "Select";
            selectPathCommand.TextImageRelation = TextImageRelation.ImageBeforeText;
            selectPathCommand.UseVisualStyleBackColor = true;
            // 
            // DataSource
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(469, 376);
            Controls.Add(dataSourceLayout);
            Name = "DataSource";
            Text = "DataSource";
            Controls.SetChildIndex(dataSourceLayout, 0);
            dataSourceLayout.ResumeLayout(false);
            dataSourceLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)objectData).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData dataSourceTitleData;
        private Controls.TextBoxData descriptionData;
        private DataGridView objectData;
        private DataGridViewTextBoxColumn DataObjectPathColumn;
        private Controls.TextBoxData objectPathData;
        private CheckBox isInModelData;
        private Button selectPathCommand;
    }
}