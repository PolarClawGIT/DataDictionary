﻿namespace DataDictionary.Main.Forms
{
    partial class DetailDataView
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
            TabControl dataViewLayout;
            TabPage dataObjectLayout;
            TabPage dataTableLayout;
            bindingTableValue = new DataGridView();
            dataTableValue = new DataGridView();
            dataViewLayout = new TabControl();
            dataObjectLayout = new TabPage();
            dataTableLayout = new TabPage();
            dataViewLayout.SuspendLayout();
            dataObjectLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingTableValue).BeginInit();
            dataTableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataTableValue).BeginInit();
            SuspendLayout();
            // 
            // dataViewLayout
            // 
            dataViewLayout.Controls.Add(dataObjectLayout);
            dataViewLayout.Controls.Add(dataTableLayout);
            dataViewLayout.Dock = DockStyle.Fill;
            dataViewLayout.Location = new Point(0, 0);
            dataViewLayout.Name = "dataViewLayout";
            dataViewLayout.SelectedIndex = 0;
            dataViewLayout.Size = new Size(608, 354);
            dataViewLayout.TabIndex = 0;
            // 
            // dataObjectLayout
            // 
            dataObjectLayout.Controls.Add(bindingTableValue);
            dataObjectLayout.Location = new Point(4, 24);
            dataObjectLayout.Name = "dataObjectLayout";
            dataObjectLayout.Padding = new Padding(3);
            dataObjectLayout.Size = new Size(600, 326);
            dataObjectLayout.TabIndex = 0;
            dataObjectLayout.Text = "Data Object";
            dataObjectLayout.UseVisualStyleBackColor = true;
            // 
            // bindingTableValue
            // 
            bindingTableValue.AllowUserToAddRows = false;
            bindingTableValue.AllowUserToDeleteRows = false;
            bindingTableValue.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            bindingTableValue.Dock = DockStyle.Fill;
            bindingTableValue.Location = new Point(3, 3);
            bindingTableValue.Name = "bindingTableValue";
            bindingTableValue.ReadOnly = true;
            bindingTableValue.RowTemplate.Height = 25;
            bindingTableValue.Size = new Size(594, 320);
            bindingTableValue.TabIndex = 0;
            bindingTableValue.RowHeaderMouseDoubleClick += bindingTableValue_RowHeaderMouseDoubleClick;
            // 
            // dataTableLayout
            // 
            dataTableLayout.Controls.Add(dataTableValue);
            dataTableLayout.Location = new Point(4, 24);
            dataTableLayout.Name = "dataTableLayout";
            dataTableLayout.Padding = new Padding(3);
            dataTableLayout.Size = new Size(600, 326);
            dataTableLayout.TabIndex = 1;
            dataTableLayout.Text = "Data Table";
            dataTableLayout.UseVisualStyleBackColor = true;
            // 
            // dataTableValue
            // 
            dataTableValue.AllowUserToAddRows = false;
            dataTableValue.AllowUserToDeleteRows = false;
            dataTableValue.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataTableValue.Dock = DockStyle.Fill;
            dataTableValue.Location = new Point(3, 3);
            dataTableValue.Name = "dataTableValue";
            dataTableValue.ReadOnly = true;
            dataTableValue.RowTemplate.Height = 25;
            dataTableValue.Size = new Size(594, 320);
            dataTableValue.TabIndex = 0;
            // 
            // DetailDataView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(608, 354);
            Controls.Add(dataViewLayout);
            Name = "DetailDataView";
            Text = "Binding Data";
            Load += BindingDataView_Load;
            dataViewLayout.ResumeLayout(false);
            dataObjectLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)bindingTableValue).EndInit();
            dataTableLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataTableValue).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView bindingTableValue;
        private DataGridView dataTableValue;
    }
}