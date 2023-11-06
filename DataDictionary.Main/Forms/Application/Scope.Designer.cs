namespace DataDictionary.Main.Forms.Application
{
    partial class Scope
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
            TableLayoutPanel scopeLayout;
            errorProvider = new ErrorProvider(components);
            bindingSource = new BindingSource(components);
            scopeNavigation = new DataGridView();
            scopeNameData = new Controls.TextBoxData();
            scopeDescriptionData = new Controls.TextBoxData();
            scopeNameColumn = new DataGridViewTextBoxColumn();
            scopeDescriptionColum = new DataGridViewTextBoxColumn();
            scopeLayout = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource).BeginInit();
            scopeLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)scopeNavigation).BeginInit();
            SuspendLayout();
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // scopeLayout
            // 
            scopeLayout.ColumnCount = 1;
            scopeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            scopeLayout.Controls.Add(scopeNavigation, 0, 0);
            scopeLayout.Controls.Add(scopeNameData, 0, 1);
            scopeLayout.Controls.Add(scopeDescriptionData, 0, 2);
            scopeLayout.Dock = DockStyle.Fill;
            scopeLayout.Location = new Point(0, 25);
            scopeLayout.Name = "scopeLayout";
            scopeLayout.RowCount = 3;
            scopeLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            scopeLayout.RowStyles.Add(new RowStyle());
            scopeLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            scopeLayout.Size = new Size(541, 315);
            scopeLayout.TabIndex = 1;
            // 
            // scopeNavigation
            // 
            scopeNavigation.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            scopeNavigation.Columns.AddRange(new DataGridViewColumn[] { scopeNameColumn, scopeDescriptionColum });
            scopeNavigation.Dock = DockStyle.Fill;
            scopeNavigation.Location = new Point(3, 3);
            scopeNavigation.Name = "scopeNavigation";
            scopeNavigation.RowTemplate.Height = 25;
            scopeNavigation.Size = new Size(535, 179);
            scopeNavigation.TabIndex = 0;
            // 
            // scopeNameData
            // 
            scopeNameData.AutoSize = true;
            scopeNameData.Dock = DockStyle.Fill;
            scopeNameData.HeaderText = "Scope Name";
            scopeNameData.Location = new Point(3, 188);
            scopeNameData.Multiline = false;
            scopeNameData.Name = "scopeNameData";
            scopeNameData.ReadOnly = false;
            scopeNameData.Size = new Size(535, 44);
            scopeNameData.TabIndex = 1;
            // 
            // scopeDescriptionData
            // 
            scopeDescriptionData.AutoSize = true;
            scopeDescriptionData.Dock = DockStyle.Fill;
            scopeDescriptionData.HeaderText = "Scope Description";
            scopeDescriptionData.Location = new Point(3, 238);
            scopeDescriptionData.Multiline = true;
            scopeDescriptionData.Name = "scopeDescriptionData";
            scopeDescriptionData.ReadOnly = false;
            scopeDescriptionData.Size = new Size(535, 74);
            scopeDescriptionData.TabIndex = 2;
            // 
            // scopeNameColumn
            // 
            scopeNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            scopeNameColumn.DataPropertyName = "ScopeName";
            scopeNameColumn.HeaderText = "Scope Name";
            scopeNameColumn.Name = "scopeNameColumn";
            // 
            // scopeDescriptionColum
            // 
            scopeDescriptionColum.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            scopeDescriptionColum.DataPropertyName = "ScopeDescription";
            scopeDescriptionColum.HeaderText = "Description";
            scopeDescriptionColum.Name = "scopeDescriptionColum";
            // 
            // Scope
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(541, 340);
            Controls.Add(scopeLayout);
            Name = "Scope";
            Text = "Scope";
            Load += Scope_Load;
            Controls.SetChildIndex(scopeLayout, 0);
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource).EndInit();
            scopeLayout.ResumeLayout(false);
            scopeLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)scopeNavigation).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ErrorProvider errorProvider;
        private BindingSource bindingSource;
        private TableLayoutPanel scopeLayout;
        private DataGridView scopeNavigation;
        private Controls.TextBoxData scopeNameData;
        private Controls.TextBoxData scopeDescriptionData;
        private DataGridViewTextBoxColumn scopeNameColumn;
        private DataGridViewTextBoxColumn scopeDescriptionColum;
    }
}