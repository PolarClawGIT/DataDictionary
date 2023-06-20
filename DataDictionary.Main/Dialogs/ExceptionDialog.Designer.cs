namespace DataDictionary.Main.Dialog
{
    partial class ExceptionDialog
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
            TabPage exceptionDetailStackTraceLaout;
            exceptionStackTraceData = new TextBox();
            exceptionLayout = new TableLayoutPanel();
            exceptionTypeLayout = new Label();
            exceptionTypeData = new TextBox();
            exceptionMessageLayout = new Label();
            exceptionMessageData = new TextBox();
            exceptionDetailLayout = new TabControl();
            exceptionDetailSummaryLayout = new TabPage();
            tableLayoutPanel1 = new TableLayoutPanel();
            exceptionApplicationLayout = new Label();
            exceptionApplicationData = new TextBox();
            exceptionWorkstationLayout = new Label();
            exceptionWorkstationData = new TextBox();
            excpetionOsVersionData = new TextBox();
            excpetionOsVersionLayout = new Label();
            exceptionUserNameLayout = new Label();
            exceptionUserNameData = new TextBox();
            exceptionApplicationVersionLayout = new Label();
            exceptionApplicationVersionData = new TextBox();
            exceptionDetailDataLayout = new TabPage();
            exceptionData = new DataGridView();
            exceptionDataKey = new DataGridViewTextBoxColumn();
            exceptionDataValue = new DataGridViewTextBoxColumn();
            exceptionDetailSqlErrorLayout = new TabPage();
            exceptionSqlErrors = new DataGridView();
            exceptionSqlErrorClass = new DataGridViewTextBoxColumn();
            exceptionSqlErrorNumber = new DataGridViewTextBoxColumn();
            exceptionSqlErrorState = new DataGridViewTextBoxColumn();
            exceptionSqlErrorMessage = new DataGridViewTextBoxColumn();
            exceptionSqlErrorProcedure = new DataGridViewTextBoxColumn();
            exceptionSqlErrorLineNumber = new DataGridViewTextBoxColumn();
            exceptionDetailAsXmlLayout = new TabPage();
            exceptionAsXmlData = new TextBox();
            exceptionDetailStackTraceLaout = new TabPage();
            exceptionDetailStackTraceLaout.SuspendLayout();
            exceptionLayout.SuspendLayout();
            exceptionDetailLayout.SuspendLayout();
            exceptionDetailSummaryLayout.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            exceptionDetailDataLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)exceptionData).BeginInit();
            exceptionDetailSqlErrorLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)exceptionSqlErrors).BeginInit();
            exceptionDetailAsXmlLayout.SuspendLayout();
            SuspendLayout();
            // 
            // exceptionDetailStackTraceLaout
            // 
            exceptionDetailStackTraceLaout.Controls.Add(exceptionStackTraceData);
            exceptionDetailStackTraceLaout.Location = new Point(4, 24);
            exceptionDetailStackTraceLaout.Name = "exceptionDetailStackTraceLaout";
            exceptionDetailStackTraceLaout.Padding = new Padding(3);
            exceptionDetailStackTraceLaout.Size = new Size(318, 310);
            exceptionDetailStackTraceLaout.TabIndex = 5;
            exceptionDetailStackTraceLaout.Text = "Stack Trace";
            exceptionDetailStackTraceLaout.UseVisualStyleBackColor = true;
            // 
            // exceptionStackTraceData
            // 
            exceptionStackTraceData.Dock = DockStyle.Fill;
            exceptionStackTraceData.Location = new Point(3, 3);
            exceptionStackTraceData.Multiline = true;
            exceptionStackTraceData.Name = "exceptionStackTraceData";
            exceptionStackTraceData.ReadOnly = true;
            exceptionStackTraceData.Size = new Size(312, 304);
            exceptionStackTraceData.TabIndex = 12;
            // 
            // exceptionLayout
            // 
            exceptionLayout.ColumnCount = 1;
            exceptionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            exceptionLayout.Controls.Add(exceptionTypeLayout, 0, 0);
            exceptionLayout.Controls.Add(exceptionTypeData, 0, 1);
            exceptionLayout.Controls.Add(exceptionMessageLayout, 0, 2);
            exceptionLayout.Controls.Add(exceptionMessageData, 0, 3);
            exceptionLayout.Controls.Add(exceptionDetailLayout, 0, 4);
            exceptionLayout.Dock = DockStyle.Fill;
            exceptionLayout.Location = new Point(0, 0);
            exceptionLayout.Name = "exceptionLayout";
            exceptionLayout.RowCount = 5;
            exceptionLayout.RowStyles.Add(new RowStyle());
            exceptionLayout.RowStyles.Add(new RowStyle());
            exceptionLayout.RowStyles.Add(new RowStyle());
            exceptionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            exceptionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 75F));
            exceptionLayout.Size = new Size(332, 517);
            exceptionLayout.TabIndex = 0;
            // 
            // exceptionTypeLayout
            // 
            exceptionTypeLayout.AutoSize = true;
            exceptionTypeLayout.Location = new Point(3, 0);
            exceptionTypeLayout.Name = "exceptionTypeLayout";
            exceptionTypeLayout.Size = new Size(31, 15);
            exceptionTypeLayout.TabIndex = 0;
            exceptionTypeLayout.Text = "Type";
            // 
            // exceptionTypeData
            // 
            exceptionTypeData.Dock = DockStyle.Fill;
            exceptionTypeData.Location = new Point(3, 18);
            exceptionTypeData.Name = "exceptionTypeData";
            exceptionTypeData.ReadOnly = true;
            exceptionTypeData.Size = new Size(326, 23);
            exceptionTypeData.TabIndex = 1;
            // 
            // exceptionMessageLayout
            // 
            exceptionMessageLayout.AutoSize = true;
            exceptionMessageLayout.Dock = DockStyle.Fill;
            exceptionMessageLayout.Location = new Point(3, 44);
            exceptionMessageLayout.Name = "exceptionMessageLayout";
            exceptionMessageLayout.Size = new Size(326, 15);
            exceptionMessageLayout.TabIndex = 2;
            exceptionMessageLayout.Text = "Message";
            // 
            // exceptionMessageData
            // 
            exceptionMessageData.Dock = DockStyle.Fill;
            exceptionMessageData.Location = new Point(3, 62);
            exceptionMessageData.Multiline = true;
            exceptionMessageData.Name = "exceptionMessageData";
            exceptionMessageData.ReadOnly = true;
            exceptionMessageData.Size = new Size(326, 108);
            exceptionMessageData.TabIndex = 3;
            // 
            // exceptionDetailLayout
            // 
            exceptionDetailLayout.Controls.Add(exceptionDetailSummaryLayout);
            exceptionDetailLayout.Controls.Add(exceptionDetailStackTraceLaout);
            exceptionDetailLayout.Controls.Add(exceptionDetailDataLayout);
            exceptionDetailLayout.Controls.Add(exceptionDetailSqlErrorLayout);
            exceptionDetailLayout.Controls.Add(exceptionDetailAsXmlLayout);
            exceptionDetailLayout.Dock = DockStyle.Fill;
            exceptionDetailLayout.Location = new Point(3, 176);
            exceptionDetailLayout.Name = "exceptionDetailLayout";
            exceptionDetailLayout.SelectedIndex = 0;
            exceptionDetailLayout.Size = new Size(326, 338);
            exceptionDetailLayout.TabIndex = 4;
            // 
            // exceptionDetailSummaryLayout
            // 
            exceptionDetailSummaryLayout.BackColor = SystemColors.Control;
            exceptionDetailSummaryLayout.Controls.Add(tableLayoutPanel1);
            exceptionDetailSummaryLayout.Location = new Point(4, 24);
            exceptionDetailSummaryLayout.Name = "exceptionDetailSummaryLayout";
            exceptionDetailSummaryLayout.Padding = new Padding(3);
            exceptionDetailSummaryLayout.Size = new Size(318, 310);
            exceptionDetailSummaryLayout.TabIndex = 0;
            exceptionDetailSummaryLayout.Text = "Summary";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(exceptionApplicationLayout, 0, 0);
            tableLayoutPanel1.Controls.Add(exceptionApplicationData, 0, 1);
            tableLayoutPanel1.Controls.Add(exceptionWorkstationLayout, 0, 4);
            tableLayoutPanel1.Controls.Add(exceptionWorkstationData, 0, 5);
            tableLayoutPanel1.Controls.Add(excpetionOsVersionData, 0, 7);
            tableLayoutPanel1.Controls.Add(excpetionOsVersionLayout, 0, 6);
            tableLayoutPanel1.Controls.Add(exceptionUserNameLayout, 0, 8);
            tableLayoutPanel1.Controls.Add(exceptionUserNameData, 0, 9);
            tableLayoutPanel1.Controls.Add(exceptionApplicationVersionLayout, 0, 2);
            tableLayoutPanel1.Controls.Add(exceptionApplicationVersionData, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 10;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(312, 304);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // exceptionApplicationLayout
            // 
            exceptionApplicationLayout.AutoSize = true;
            exceptionApplicationLayout.Location = new Point(3, 0);
            exceptionApplicationLayout.Name = "exceptionApplicationLayout";
            exceptionApplicationLayout.Size = new Size(68, 15);
            exceptionApplicationLayout.TabIndex = 0;
            exceptionApplicationLayout.Text = "Application";
            // 
            // exceptionApplicationData
            // 
            exceptionApplicationData.Dock = DockStyle.Fill;
            exceptionApplicationData.Location = new Point(3, 18);
            exceptionApplicationData.Name = "exceptionApplicationData";
            exceptionApplicationData.ReadOnly = true;
            exceptionApplicationData.Size = new Size(306, 23);
            exceptionApplicationData.TabIndex = 1;
            // 
            // exceptionWorkstationLayout
            // 
            exceptionWorkstationLayout.AutoSize = true;
            exceptionWorkstationLayout.Location = new Point(3, 88);
            exceptionWorkstationLayout.Name = "exceptionWorkstationLayout";
            exceptionWorkstationLayout.Size = new Size(71, 15);
            exceptionWorkstationLayout.TabIndex = 2;
            exceptionWorkstationLayout.Text = "Workstation";
            // 
            // exceptionWorkstationData
            // 
            exceptionWorkstationData.Dock = DockStyle.Fill;
            exceptionWorkstationData.Location = new Point(3, 106);
            exceptionWorkstationData.Name = "exceptionWorkstationData";
            exceptionWorkstationData.ReadOnly = true;
            exceptionWorkstationData.Size = new Size(306, 23);
            exceptionWorkstationData.TabIndex = 3;
            // 
            // excpetionOsVersionData
            // 
            excpetionOsVersionData.Dock = DockStyle.Fill;
            excpetionOsVersionData.Location = new Point(3, 150);
            excpetionOsVersionData.Name = "excpetionOsVersionData";
            excpetionOsVersionData.ReadOnly = true;
            excpetionOsVersionData.Size = new Size(306, 23);
            excpetionOsVersionData.TabIndex = 4;
            // 
            // excpetionOsVersionLayout
            // 
            excpetionOsVersionLayout.AutoSize = true;
            excpetionOsVersionLayout.Location = new Point(3, 132);
            excpetionOsVersionLayout.Name = "excpetionOsVersionLayout";
            excpetionOsVersionLayout.Size = new Size(63, 15);
            excpetionOsVersionLayout.TabIndex = 5;
            excpetionOsVersionLayout.Text = "OS Version";
            // 
            // exceptionUserNameLayout
            // 
            exceptionUserNameLayout.AutoSize = true;
            exceptionUserNameLayout.Location = new Point(3, 176);
            exceptionUserNameLayout.Name = "exceptionUserNameLayout";
            exceptionUserNameLayout.Size = new Size(65, 15);
            exceptionUserNameLayout.TabIndex = 6;
            exceptionUserNameLayout.Text = "User Name";
            // 
            // exceptionUserNameData
            // 
            exceptionUserNameData.Dock = DockStyle.Fill;
            exceptionUserNameData.Location = new Point(3, 194);
            exceptionUserNameData.Name = "exceptionUserNameData";
            exceptionUserNameData.ReadOnly = true;
            exceptionUserNameData.Size = new Size(306, 23);
            exceptionUserNameData.TabIndex = 7;
            // 
            // exceptionApplicationVersionLayout
            // 
            exceptionApplicationVersionLayout.AutoSize = true;
            exceptionApplicationVersionLayout.Location = new Point(3, 44);
            exceptionApplicationVersionLayout.Name = "exceptionApplicationVersionLayout";
            exceptionApplicationVersionLayout.Size = new Size(109, 15);
            exceptionApplicationVersionLayout.TabIndex = 8;
            exceptionApplicationVersionLayout.Text = "Application Version";
            // 
            // exceptionApplicationVersionData
            // 
            exceptionApplicationVersionData.Dock = DockStyle.Fill;
            exceptionApplicationVersionData.Location = new Point(3, 62);
            exceptionApplicationVersionData.Name = "exceptionApplicationVersionData";
            exceptionApplicationVersionData.ReadOnly = true;
            exceptionApplicationVersionData.Size = new Size(306, 23);
            exceptionApplicationVersionData.TabIndex = 9;
            // 
            // exceptionDetailDataLayout
            // 
            exceptionDetailDataLayout.BackColor = SystemColors.Control;
            exceptionDetailDataLayout.Controls.Add(exceptionData);
            exceptionDetailDataLayout.Location = new Point(4, 24);
            exceptionDetailDataLayout.Name = "exceptionDetailDataLayout";
            exceptionDetailDataLayout.Size = new Size(318, 310);
            exceptionDetailDataLayout.TabIndex = 3;
            exceptionDetailDataLayout.Text = "Data";
            // 
            // exceptionData
            // 
            exceptionData.AllowUserToAddRows = false;
            exceptionData.AllowUserToDeleteRows = false;
            exceptionData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            exceptionData.Columns.AddRange(new DataGridViewColumn[] { exceptionDataKey, exceptionDataValue });
            exceptionData.Dock = DockStyle.Fill;
            exceptionData.Location = new Point(0, 0);
            exceptionData.Name = "exceptionData";
            exceptionData.RowTemplate.Height = 25;
            exceptionData.Size = new Size(318, 310);
            exceptionData.TabIndex = 0;
            // 
            // exceptionDataKey
            // 
            exceptionDataKey.DataPropertyName = "Key";
            exceptionDataKey.HeaderText = "Key";
            exceptionDataKey.Name = "exceptionDataKey";
            exceptionDataKey.ReadOnly = true;
            // 
            // exceptionDataValue
            // 
            exceptionDataValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            exceptionDataValue.DataPropertyName = "value";
            exceptionDataValue.HeaderText = "Value";
            exceptionDataValue.Name = "exceptionDataValue";
            exceptionDataValue.ReadOnly = true;
            // 
            // exceptionDetailSqlErrorLayout
            // 
            exceptionDetailSqlErrorLayout.BackColor = SystemColors.Control;
            exceptionDetailSqlErrorLayout.Controls.Add(exceptionSqlErrors);
            exceptionDetailSqlErrorLayout.Location = new Point(4, 24);
            exceptionDetailSqlErrorLayout.Name = "exceptionDetailSqlErrorLayout";
            exceptionDetailSqlErrorLayout.Size = new Size(318, 310);
            exceptionDetailSqlErrorLayout.TabIndex = 4;
            exceptionDetailSqlErrorLayout.Text = "SQL Errors";
            // 
            // exceptionSqlErrors
            // 
            exceptionSqlErrors.AllowUserToAddRows = false;
            exceptionSqlErrors.AllowUserToDeleteRows = false;
            exceptionSqlErrors.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            exceptionSqlErrors.Columns.AddRange(new DataGridViewColumn[] { exceptionSqlErrorClass, exceptionSqlErrorNumber, exceptionSqlErrorState, exceptionSqlErrorMessage, exceptionSqlErrorProcedure, exceptionSqlErrorLineNumber });
            exceptionSqlErrors.Dock = DockStyle.Fill;
            exceptionSqlErrors.Location = new Point(0, 0);
            exceptionSqlErrors.Name = "exceptionSqlErrors";
            exceptionSqlErrors.RowTemplate.Height = 25;
            exceptionSqlErrors.Size = new Size(318, 310);
            exceptionSqlErrors.TabIndex = 0;
            // 
            // exceptionSqlErrorClass
            // 
            exceptionSqlErrorClass.DataPropertyName = "Class";
            exceptionSqlErrorClass.HeaderText = "Class";
            exceptionSqlErrorClass.Name = "exceptionSqlErrorClass";
            exceptionSqlErrorClass.ReadOnly = true;
            // 
            // exceptionSqlErrorNumber
            // 
            exceptionSqlErrorNumber.DataPropertyName = "Number";
            exceptionSqlErrorNumber.HeaderText = "Number";
            exceptionSqlErrorNumber.Name = "exceptionSqlErrorNumber";
            exceptionSqlErrorNumber.ReadOnly = true;
            // 
            // exceptionSqlErrorState
            // 
            exceptionSqlErrorState.DataPropertyName = "State";
            exceptionSqlErrorState.HeaderText = "State";
            exceptionSqlErrorState.Name = "exceptionSqlErrorState";
            exceptionSqlErrorState.ReadOnly = true;
            // 
            // exceptionSqlErrorMessage
            // 
            exceptionSqlErrorMessage.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            exceptionSqlErrorMessage.DataPropertyName = "Message";
            exceptionSqlErrorMessage.HeaderText = "Message";
            exceptionSqlErrorMessage.MinimumWidth = 100;
            exceptionSqlErrorMessage.Name = "exceptionSqlErrorMessage";
            exceptionSqlErrorMessage.ReadOnly = true;
            // 
            // exceptionSqlErrorProcedure
            // 
            exceptionSqlErrorProcedure.DataPropertyName = "Procedure";
            exceptionSqlErrorProcedure.HeaderText = "Procedure";
            exceptionSqlErrorProcedure.Name = "exceptionSqlErrorProcedure";
            exceptionSqlErrorProcedure.ReadOnly = true;
            // 
            // exceptionSqlErrorLineNumber
            // 
            exceptionSqlErrorLineNumber.DataPropertyName = "LineNumber";
            exceptionSqlErrorLineNumber.HeaderText = "Line Number";
            exceptionSqlErrorLineNumber.Name = "exceptionSqlErrorLineNumber";
            // 
            // exceptionDetailAsXmlLayout
            // 
            exceptionDetailAsXmlLayout.BackColor = SystemColors.Control;
            exceptionDetailAsXmlLayout.Controls.Add(exceptionAsXmlData);
            exceptionDetailAsXmlLayout.Location = new Point(4, 24);
            exceptionDetailAsXmlLayout.Name = "exceptionDetailAsXmlLayout";
            exceptionDetailAsXmlLayout.Size = new Size(318, 310);
            exceptionDetailAsXmlLayout.TabIndex = 2;
            exceptionDetailAsXmlLayout.Text = "As XML";
            // 
            // exceptionAsXmlData
            // 
            exceptionAsXmlData.Dock = DockStyle.Fill;
            exceptionAsXmlData.Location = new Point(0, 0);
            exceptionAsXmlData.Multiline = true;
            exceptionAsXmlData.Name = "exceptionAsXmlData";
            exceptionAsXmlData.ReadOnly = true;
            exceptionAsXmlData.ScrollBars = ScrollBars.Both;
            exceptionAsXmlData.Size = new Size(318, 310);
            exceptionAsXmlData.TabIndex = 0;
            exceptionAsXmlData.WordWrap = false;
            // 
            // ExceptionDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(332, 517);
            Controls.Add(exceptionLayout);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ExceptionDialog";
            ShowIcon = false;
            Text = "Application Exception";
            Load += ExceptionDialog_Load;
            exceptionDetailStackTraceLaout.ResumeLayout(false);
            exceptionDetailStackTraceLaout.PerformLayout();
            exceptionLayout.ResumeLayout(false);
            exceptionLayout.PerformLayout();
            exceptionDetailLayout.ResumeLayout(false);
            exceptionDetailSummaryLayout.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            exceptionDetailDataLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)exceptionData).EndInit();
            exceptionDetailSqlErrorLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)exceptionSqlErrors).EndInit();
            exceptionDetailAsXmlLayout.ResumeLayout(false);
            exceptionDetailAsXmlLayout.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel exceptionLayout;
        private Label exceptionTypeLayout;
        private TextBox exceptionTypeData;
        private Label exceptionMessageLayout;
        private TextBox exceptionMessageData;
        private TabControl exceptionDetailLayout;
        private TabPage exceptionDetailSummaryLayout;
        private TabPage exceptionDetailAsXmlLayout;
        private TextBox exceptionAsXmlData;
        private TableLayoutPanel tableLayoutPanel1;
        private Label exceptionApplicationLayout;
        private TextBox exceptionApplicationData;
        private Label exceptionWorkstationLayout;
        private TextBox exceptionWorkstationData;
        private TextBox excpetionOsVersionData;
        private Label excpetionOsVersionLayout;
        private Label exceptionUserNameLayout;
        private TextBox exceptionUserNameData;
        private Label exceptionApplicationVersionLayout;
        private TextBox exceptionApplicationVersionData;
        private TabPage exceptionDetailDataLayout;
        private DataGridView exceptionData;
        private DataGridViewTextBoxColumn exceptionDataKey;
        private DataGridViewTextBoxColumn exceptionDataValue;
        private TabPage exceptionDetailSqlErrorLayout;
        private DataGridView exceptionSqlErrors;
        private DataGridViewTextBoxColumn exceptionSqlErrorClass;
        private DataGridViewTextBoxColumn exceptionSqlErrorNumber;
        private DataGridViewTextBoxColumn exceptionSqlErrorState;
        private DataGridViewTextBoxColumn exceptionSqlErrorMessage;
        private DataGridViewTextBoxColumn exceptionSqlErrorProcedure;
        private DataGridViewTextBoxColumn exceptionSqlErrorLineNumber;
        private TextBox exceptionStackTraceData;
    }
}