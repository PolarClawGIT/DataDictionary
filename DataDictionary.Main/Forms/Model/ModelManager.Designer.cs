namespace DataDictionary.Main.Forms.Model
{
    partial class ModelManager
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
            TableLayoutPanel modelManagerLayout;
            modelNavigation = new DataGridView();
            modelTitleData = new Controls.TextBoxData();
            modelDescriptionData = new Controls.TextBoxData();
            modelBinding = new BindingSource(components);
            modelTitlecolumn = new DataGridViewTextBoxColumn();
            modelDescriptionColumn = new DataGridViewTextBoxColumn();
            inModelColumn = new DataGridViewCheckBoxColumn();
            inDatabaseColumn = new DataGridViewCheckBoxColumn();
            modelManagerLayout = new TableLayoutPanel();
            modelManagerLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)modelNavigation).BeginInit();
            ((System.ComponentModel.ISupportInitialize)modelBinding).BeginInit();
            SuspendLayout();
            // 
            // modelManagerLayout
            // 
            modelManagerLayout.ColumnCount = 1;
            modelManagerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            modelManagerLayout.Controls.Add(modelNavigation, 0, 0);
            modelManagerLayout.Controls.Add(modelTitleData, 0, 1);
            modelManagerLayout.Controls.Add(modelDescriptionData, 0, 2);
            modelManagerLayout.Dock = DockStyle.Fill;
            modelManagerLayout.Location = new Point(0, 25);
            modelManagerLayout.Name = "modelManagerLayout";
            modelManagerLayout.RowCount = 3;
            modelManagerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            modelManagerLayout.RowStyles.Add(new RowStyle());
            modelManagerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            modelManagerLayout.Size = new Size(562, 398);
            modelManagerLayout.TabIndex = 1;
            // 
            // modelNavigation
            // 
            modelNavigation.AllowUserToAddRows = false;
            modelNavigation.AllowUserToDeleteRows = false;
            modelNavigation.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            modelNavigation.Columns.AddRange(new DataGridViewColumn[] { modelTitlecolumn, modelDescriptionColumn, inModelColumn, inDatabaseColumn });
            modelNavigation.Dock = DockStyle.Fill;
            modelNavigation.Location = new Point(3, 3);
            modelNavigation.Name = "modelNavigation";
            modelNavigation.RowTemplate.Height = 25;
            modelNavigation.Size = new Size(556, 237);
            modelNavigation.TabIndex = 0;
            // 
            // modelTitleData
            // 
            modelTitleData.AutoSize = true;
            modelTitleData.Dock = DockStyle.Fill;
            modelTitleData.HeaderText = "Model Title";
            modelTitleData.Location = new Point(3, 246);
            modelTitleData.Multiline = false;
            modelTitleData.Name = "modelTitleData";
            modelTitleData.ReadOnly = false;
            modelTitleData.Size = new Size(556, 44);
            modelTitleData.TabIndex = 1;
            // 
            // modelDescriptionData
            // 
            modelDescriptionData.AutoSize = true;
            modelDescriptionData.Dock = DockStyle.Fill;
            modelDescriptionData.HeaderText = "Model Description";
            modelDescriptionData.Location = new Point(3, 296);
            modelDescriptionData.Multiline = true;
            modelDescriptionData.Name = "modelDescriptionData";
            modelDescriptionData.ReadOnly = false;
            modelDescriptionData.Size = new Size(556, 99);
            modelDescriptionData.TabIndex = 2;
            // 
            // modelBinding
            // 
            modelBinding.BindingComplete += modelBinding_BindingComplete;
            modelBinding.CurrentChanged += modelBinding_CurrentChanged;
            // 
            // modelTitlecolumn
            // 
            modelTitlecolumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            modelTitlecolumn.DataPropertyName = "ModelTitle";
            modelTitlecolumn.FillWeight = 30F;
            modelTitlecolumn.HeaderText = "Model Title";
            modelTitlecolumn.Name = "modelTitlecolumn";
            // 
            // modelDescriptionColumn
            // 
            modelDescriptionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            modelDescriptionColumn.DataPropertyName = "ModelDescription";
            modelDescriptionColumn.FillWeight = 70F;
            modelDescriptionColumn.HeaderText = "Model Description";
            modelDescriptionColumn.Name = "modelDescriptionColumn";
            // 
            // inModelColumn
            // 
            inModelColumn.DataPropertyName = "InModel";
            inModelColumn.HeaderText = "In Model";
            inModelColumn.Name = "inModelColumn";
            inModelColumn.ReadOnly = true;
            // 
            // inDatabaseColumn
            // 
            inDatabaseColumn.DataPropertyName = "InDatabase";
            inDatabaseColumn.HeaderText = "In Database";
            inDatabaseColumn.Name = "inDatabaseColumn";
            inDatabaseColumn.ReadOnly = true;
            // 
            // ModelManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(562, 423);
            Controls.Add(modelManagerLayout);
            Name = "ModelManager";
            Text = "Model Manager";
            Load += ModelManager_Load;
            Controls.SetChildIndex(modelManagerLayout, 0);
            modelManagerLayout.ResumeLayout(false);
            modelManagerLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)modelNavigation).EndInit();
            ((System.ComponentModel.ISupportInitialize)modelBinding).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private BindingSource modelBinding;
        private TableLayoutPanel modelManagerLayout;
        private DataGridView modelNavigation;
        private Controls.TextBoxData modelTitleData;
        private Controls.TextBoxData modelDescriptionData;
        private DataGridViewTextBoxColumn modelTitlecolumn;
        private DataGridViewTextBoxColumn modelDescriptionColumn;
        private DataGridViewCheckBoxColumn inModelColumn;
        private DataGridViewCheckBoxColumn inDatabaseColumn;
    }
}