namespace DataDictionary.Main.Forms.Model
{
    partial class Process
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
            TableLayoutPanel mainLayout;
            TableLayoutPanel argumentOptionsLayout;
            titleData = new DataDictionary.Main.Controls.TextBoxData();
            descriptionData = new DataDictionary.Main.Controls.TextBoxData();
            detailTabLayout = new TabControl();
            detailTab = new TabPage();
            detailLayout = new TableLayoutPanel();
            argumentData = new DataGridView();
            argumentTitleColumn = new DataGridViewTextBoxColumn();
            ordinalPositionColumn = new DataGridViewTextBoxColumn();
            argumentLayout = new TableLayoutPanel();
            argumentTitleData = new DataDictionary.Main.Controls.TextBoxData();
            argumentDescriptionData = new DataDictionary.Main.Controls.TextBoxData();
            argumentNameData = new DataDictionary.Main.Controls.TextBoxData();
            argumentOrdinalPositionData = new DataDictionary.Main.Controls.TextBoxData();
            argumentIsInputData = new CheckBox();
            argumentIsOutputData = new CheckBox();
            argumentButtonLayout = new TableLayoutPanel();
            argumentSelectCommand = new Button();
            argumentNewCommand = new Button();
            propertyTab = new TabPage();
            propertyData = new DataDictionary.Main.Forms.Model.Controls.PropertyData();
            definitionTab = new TabPage();
            definitionData = new DataDictionary.Main.Forms.Model.Controls.DefinitionData();
            aliasTab = new TabPage();
            aliasData = new DataDictionary.Main.Forms.Model.Controls.AliasData();
            subjectAreaTab = new TabPage();
            subjectAreaLayout = new TableLayoutPanel();
            subjectArea = new DataDictionary.Main.Forms.Model.Controls.SubjectAreaData();
            memberNameData = new DataDictionary.Main.Controls.TextBoxData();
            bindingProcess = new BindingSource(components);
            bindingAlias = new BindingSource(components);
            bindingProperty = new BindingSource(components);
            bindingSubjectArea = new BindingSource(components);
            bindingDefinition = new BindingSource(components);
            bindingArgument = new BindingSource(components);
            argumentIsPassedData = new CheckBox();
            argumentIsContributorData = new CheckBox();
            argumentAsValueData = new CheckBox();
            argumentIsReturnedData = new CheckBox();
            argumentIsAlteredData = new CheckBox();
            argumentAsReferenceData = new CheckBox();
            mainLayout = new TableLayoutPanel();
            argumentOptionsLayout = new TableLayoutPanel();
            mainLayout.SuspendLayout();
            detailTabLayout.SuspendLayout();
            detailTab.SuspendLayout();
            detailLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)argumentData).BeginInit();
            argumentLayout.SuspendLayout();
            argumentButtonLayout.SuspendLayout();
            propertyTab.SuspendLayout();
            definitionTab.SuspendLayout();
            aliasTab.SuspendLayout();
            subjectAreaTab.SuspendLayout();
            subjectAreaLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingProcess).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingAlias).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingProperty).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSubjectArea).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingDefinition).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingArgument).BeginInit();
            argumentOptionsLayout.SuspendLayout();
            SuspendLayout();
            // 
            // mainLayout
            // 
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.Controls.Add(titleData, 0, 0);
            mainLayout.Controls.Add(descriptionData, 0, 1);
            mainLayout.Controls.Add(detailTabLayout, 0, 2);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(0, 25);
            mainLayout.Name = "mainLayout";
            mainLayout.RowCount = 3;
            mainLayout.RowStyles.Add(new RowStyle());
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            mainLayout.Size = new Size(447, 701);
            mainLayout.TabIndex = 4;
            // 
            // titleData
            // 
            titleData.AutoSize = true;
            titleData.Dock = DockStyle.Fill;
            titleData.HeaderText = "Title";
            titleData.Location = new Point(3, 3);
            titleData.Multiline = false;
            titleData.Name = "titleData";
            titleData.ReadOnly = false;
            titleData.Size = new Size(441, 44);
            titleData.TabIndex = 0;
            titleData.WordWrap = true;
            // 
            // descriptionData
            // 
            descriptionData.AutoSize = true;
            descriptionData.Dock = DockStyle.Fill;
            descriptionData.HeaderText = "Description";
            descriptionData.Location = new Point(3, 53);
            descriptionData.Multiline = true;
            descriptionData.Name = "descriptionData";
            descriptionData.ReadOnly = false;
            descriptionData.Size = new Size(441, 124);
            descriptionData.TabIndex = 1;
            descriptionData.WordWrap = true;
            // 
            // detailTabLayout
            // 
            detailTabLayout.Controls.Add(detailTab);
            detailTabLayout.Controls.Add(propertyTab);
            detailTabLayout.Controls.Add(definitionTab);
            detailTabLayout.Controls.Add(aliasTab);
            detailTabLayout.Controls.Add(subjectAreaTab);
            detailTabLayout.Dock = DockStyle.Fill;
            detailTabLayout.Location = new Point(3, 183);
            detailTabLayout.Name = "detailTabLayout";
            detailTabLayout.SelectedIndex = 0;
            detailTabLayout.Size = new Size(441, 515);
            detailTabLayout.TabIndex = 2;
            // 
            // detailTab
            // 
            detailTab.BackColor = SystemColors.Control;
            detailTab.Controls.Add(detailLayout);
            detailTab.Location = new Point(4, 24);
            detailTab.Name = "detailTab";
            detailTab.Padding = new Padding(3);
            detailTab.Size = new Size(433, 487);
            detailTab.TabIndex = 0;
            detailTab.Text = "Details";
            // 
            // detailLayout
            // 
            detailLayout.ColumnCount = 1;
            detailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            detailLayout.Controls.Add(argumentData, 0, 0);
            detailLayout.Controls.Add(argumentLayout, 0, 1);
            detailLayout.Controls.Add(argumentButtonLayout, 0, 2);
            detailLayout.Dock = DockStyle.Fill;
            detailLayout.Location = new Point(3, 3);
            detailLayout.Name = "detailLayout";
            detailLayout.RowCount = 3;
            detailLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            detailLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            detailLayout.RowStyles.Add(new RowStyle());
            detailLayout.Size = new Size(427, 481);
            detailLayout.TabIndex = 1;
            // 
            // argumentData
            // 
            argumentData.AllowUserToAddRows = false;
            argumentData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            argumentData.Columns.AddRange(new DataGridViewColumn[] { argumentTitleColumn, ordinalPositionColumn });
            argumentData.Dock = DockStyle.Fill;
            argumentData.Location = new Point(3, 3);
            argumentData.Name = "argumentData";
            argumentData.ReadOnly = true;
            argumentData.Size = new Size(421, 172);
            argumentData.TabIndex = 0;
            // 
            // argumentTitleColumn
            // 
            argumentTitleColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            argumentTitleColumn.DataPropertyName = "ArgumentTitle";
            argumentTitleColumn.FillWeight = 70F;
            argumentTitleColumn.HeaderText = "Argument";
            argumentTitleColumn.Name = "argumentTitleColumn";
            argumentTitleColumn.ReadOnly = true;
            // 
            // ordinalPositionColumn
            // 
            ordinalPositionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ordinalPositionColumn.DataPropertyName = "OrdinalPosition";
            ordinalPositionColumn.FillWeight = 30F;
            ordinalPositionColumn.HeaderText = "Order";
            ordinalPositionColumn.Name = "ordinalPositionColumn";
            ordinalPositionColumn.ReadOnly = true;
            // 
            // argumentLayout
            // 
            argumentLayout.ColumnCount = 2;
            argumentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.6666641F));
            argumentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            argumentLayout.Controls.Add(argumentOrdinalPositionData, 1, 2);
            argumentLayout.Controls.Add(argumentTitleData, 0, 0);
            argumentLayout.Controls.Add(argumentDescriptionData, 0, 1);
            argumentLayout.Controls.Add(argumentNameData, 0, 2);
            argumentLayout.Controls.Add(argumentOptionsLayout, 0, 3);
            argumentLayout.Dock = DockStyle.Fill;
            argumentLayout.Location = new Point(3, 181);
            argumentLayout.Name = "argumentLayout";
            argumentLayout.RowCount = 4;
            argumentLayout.RowStyles.Add(new RowStyle());
            argumentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            argumentLayout.RowStyles.Add(new RowStyle());
            argumentLayout.RowStyles.Add(new RowStyle());
            argumentLayout.Size = new Size(421, 261);
            argumentLayout.TabIndex = 0;
            // 
            // argumentTitleData
            // 
            argumentTitleData.AutoSize = true;
            argumentLayout.SetColumnSpan(argumentTitleData, 2);
            argumentTitleData.Dock = DockStyle.Fill;
            argumentTitleData.HeaderText = "Argument Title";
            argumentTitleData.Location = new Point(3, 3);
            argumentTitleData.Multiline = false;
            argumentTitleData.Name = "argumentTitleData";
            argumentTitleData.ReadOnly = false;
            argumentTitleData.Size = new Size(415, 44);
            argumentTitleData.TabIndex = 1;
            argumentTitleData.WordWrap = true;
            // 
            // argumentDescriptionData
            // 
            argumentDescriptionData.AutoSize = true;
            argumentLayout.SetColumnSpan(argumentDescriptionData, 2);
            argumentDescriptionData.Dock = DockStyle.Fill;
            argumentDescriptionData.HeaderText = "Argument Description";
            argumentDescriptionData.Location = new Point(3, 53);
            argumentDescriptionData.Multiline = true;
            argumentDescriptionData.Name = "argumentDescriptionData";
            argumentDescriptionData.ReadOnly = false;
            argumentDescriptionData.Size = new Size(415, 74);
            argumentDescriptionData.TabIndex = 2;
            argumentDescriptionData.WordWrap = true;
            // 
            // argumentNameData
            // 
            argumentNameData.AutoSize = true;
            argumentNameData.Dock = DockStyle.Fill;
            argumentNameData.HeaderText = "Argument Name";
            argumentNameData.Location = new Point(3, 133);
            argumentNameData.Multiline = false;
            argumentNameData.Name = "argumentNameData";
            argumentNameData.ReadOnly = false;
            argumentNameData.Size = new Size(274, 44);
            argumentNameData.TabIndex = 3;
            argumentNameData.WordWrap = true;
            argumentNameData.Validating += ArgumentNameData_Validating;
            // 
            // argumentOrdinalPositionData
            // 
            argumentOrdinalPositionData.AutoSize = true;
            argumentOrdinalPositionData.HeaderText = "Ordinal Position";
            argumentOrdinalPositionData.Location = new Point(283, 133);
            argumentOrdinalPositionData.Multiline = false;
            argumentOrdinalPositionData.Name = "argumentOrdinalPositionData";
            argumentOrdinalPositionData.ReadOnly = false;
            argumentOrdinalPositionData.Size = new Size(120, 44);
            argumentOrdinalPositionData.TabIndex = 4;
            argumentOrdinalPositionData.WordWrap = true;
            // 
            // argumentIsInputData
            // 
            argumentIsInputData.AutoSize = true;
            argumentIsInputData.Enabled = false;
            argumentIsInputData.Location = new Point(279, 3);
            argumentIsInputData.Name = "argumentIsInputData";
            argumentIsInputData.Size = new Size(54, 19);
            argumentIsInputData.TabIndex = 5;
            argumentIsInputData.Text = "Input";
            argumentIsInputData.UseVisualStyleBackColor = true;
            // 
            // argumentIsOutputData
            // 
            argumentIsOutputData.AutoSize = true;
            argumentIsOutputData.Enabled = false;
            argumentIsOutputData.Location = new Point(279, 28);
            argumentIsOutputData.Name = "argumentIsOutputData";
            argumentIsOutputData.Size = new Size(64, 19);
            argumentIsOutputData.TabIndex = 6;
            argumentIsOutputData.Text = "Output";
            argumentIsOutputData.UseVisualStyleBackColor = true;
            // 
            // argumentButtonLayout
            // 
            argumentButtonLayout.AutoSize = true;
            argumentButtonLayout.ColumnCount = 3;
            argumentButtonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            argumentButtonLayout.ColumnStyles.Add(new ColumnStyle());
            argumentButtonLayout.ColumnStyles.Add(new ColumnStyle());
            argumentButtonLayout.Controls.Add(argumentSelectCommand, 2, 0);
            argumentButtonLayout.Controls.Add(argumentNewCommand, 1, 0);
            argumentButtonLayout.Dock = DockStyle.Fill;
            argumentButtonLayout.Location = new Point(3, 448);
            argumentButtonLayout.Name = "argumentButtonLayout";
            argumentButtonLayout.RowCount = 1;
            argumentButtonLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            argumentButtonLayout.Size = new Size(421, 30);
            argumentButtonLayout.TabIndex = 7;
            // 
            // argumentSelectCommand
            // 
            argumentSelectCommand.Location = new Point(343, 3);
            argumentSelectCommand.Name = "argumentSelectCommand";
            argumentSelectCommand.Size = new Size(75, 23);
            argumentSelectCommand.TabIndex = 1;
            argumentSelectCommand.Text = "Select";
            argumentSelectCommand.TextImageRelation = TextImageRelation.ImageBeforeText;
            argumentSelectCommand.UseVisualStyleBackColor = true;
            argumentSelectCommand.Click += ArgumentSelectCommand_Click;
            // 
            // argumentNewCommand
            // 
            argumentNewCommand.Location = new Point(262, 3);
            argumentNewCommand.Name = "argumentNewCommand";
            argumentNewCommand.Size = new Size(75, 23);
            argumentNewCommand.TabIndex = 0;
            argumentNewCommand.Text = "New";
            argumentNewCommand.TextImageRelation = TextImageRelation.ImageBeforeText;
            argumentNewCommand.UseVisualStyleBackColor = true;
            argumentNewCommand.Click += ArgumentNewCommand_Click;
            // 
            // propertyTab
            // 
            propertyTab.BackColor = SystemColors.Control;
            propertyTab.Controls.Add(propertyData);
            propertyTab.Location = new Point(4, 24);
            propertyTab.Name = "propertyTab";
            propertyTab.Padding = new Padding(3);
            propertyTab.Size = new Size(192, 72);
            propertyTab.TabIndex = 1;
            propertyTab.Text = "Properties";
            // 
            // propertyData
            // 
            propertyData.Dock = DockStyle.Fill;
            propertyData.Location = new Point(3, 3);
            propertyData.Name = "propertyData";
            propertyData.Size = new Size(186, 66);
            propertyData.TabIndex = 0;
            // 
            // definitionTab
            // 
            definitionTab.BackColor = SystemColors.Control;
            definitionTab.Controls.Add(definitionData);
            definitionTab.Location = new Point(4, 24);
            definitionTab.Name = "definitionTab";
            definitionTab.Padding = new Padding(3);
            definitionTab.Size = new Size(192, 72);
            definitionTab.TabIndex = 4;
            definitionTab.Text = "Definitions";
            // 
            // definitionData
            // 
            definitionData.Dock = DockStyle.Fill;
            definitionData.Location = new Point(3, 3);
            definitionData.Name = "definitionData";
            definitionData.Size = new Size(186, 66);
            definitionData.TabIndex = 0;
            // 
            // aliasTab
            // 
            aliasTab.BackColor = SystemColors.Control;
            aliasTab.Controls.Add(aliasData);
            aliasTab.Location = new Point(4, 24);
            aliasTab.Name = "aliasTab";
            aliasTab.Padding = new Padding(3);
            aliasTab.Size = new Size(192, 72);
            aliasTab.TabIndex = 2;
            aliasTab.Text = "Aliases";
            // 
            // aliasData
            // 
            aliasData.Dock = DockStyle.Fill;
            aliasData.Location = new Point(3, 3);
            aliasData.Name = "aliasData";
            aliasData.Size = new Size(186, 66);
            aliasData.TabIndex = 0;
            // 
            // subjectAreaTab
            // 
            subjectAreaTab.BackColor = SystemColors.Control;
            subjectAreaTab.Controls.Add(subjectAreaLayout);
            subjectAreaTab.Location = new Point(4, 24);
            subjectAreaTab.Name = "subjectAreaTab";
            subjectAreaTab.Padding = new Padding(3);
            subjectAreaTab.Size = new Size(192, 72);
            subjectAreaTab.TabIndex = 3;
            subjectAreaTab.Text = "Subject Area";
            // 
            // subjectAreaLayout
            // 
            subjectAreaLayout.ColumnCount = 1;
            subjectAreaLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            subjectAreaLayout.Controls.Add(subjectArea, 0, 1);
            subjectAreaLayout.Controls.Add(memberNameData, 0, 0);
            subjectAreaLayout.Dock = DockStyle.Fill;
            subjectAreaLayout.Location = new Point(3, 3);
            subjectAreaLayout.Name = "subjectAreaLayout";
            subjectAreaLayout.RowCount = 2;
            subjectAreaLayout.RowStyles.Add(new RowStyle());
            subjectAreaLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            subjectAreaLayout.Size = new Size(186, 66);
            subjectAreaLayout.TabIndex = 1;
            // 
            // subjectArea
            // 
            subjectArea.Dock = DockStyle.Fill;
            subjectArea.Location = new Point(3, 53);
            subjectArea.Name = "subjectArea";
            subjectArea.Size = new Size(180, 10);
            subjectArea.TabIndex = 0;
            // 
            // memberNameData
            // 
            memberNameData.AutoSize = true;
            memberNameData.Dock = DockStyle.Fill;
            memberNameData.HeaderText = "Subject Member Name";
            memberNameData.Location = new Point(3, 3);
            memberNameData.Multiline = false;
            memberNameData.Name = "memberNameData";
            memberNameData.ReadOnly = false;
            memberNameData.Size = new Size(180, 44);
            memberNameData.TabIndex = 1;
            memberNameData.WordWrap = true;
            // 
            // bindingArgument
            // 
            bindingArgument.AddingNew += BindingArgument_AddingNew;
            bindingArgument.CurrentChanged += BindingArgument_CurrentChanged;
            // 
            // argumentOptionsLayout
            // 
            argumentOptionsLayout.AutoSize = true;
            argumentOptionsLayout.ColumnCount = 3;
            argumentLayout.SetColumnSpan(argumentOptionsLayout, 2);
            argumentOptionsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            argumentOptionsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            argumentOptionsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            argumentOptionsLayout.Controls.Add(argumentIsPassedData, 0, 0);
            argumentOptionsLayout.Controls.Add(argumentIsOutputData, 2, 1);
            argumentOptionsLayout.Controls.Add(argumentIsInputData, 2, 0);
            argumentOptionsLayout.Controls.Add(argumentIsContributorData, 1, 0);
            argumentOptionsLayout.Controls.Add(argumentIsReturnedData, 0, 1);
            argumentOptionsLayout.Controls.Add(argumentAsValueData, 0, 2);
            argumentOptionsLayout.Controls.Add(argumentAsReferenceData, 1, 2);
            argumentOptionsLayout.Controls.Add(argumentIsAlteredData, 1, 1);
            argumentOptionsLayout.Dock = DockStyle.Fill;
            argumentOptionsLayout.Location = new Point(3, 183);
            argumentOptionsLayout.Name = "argumentOptionsLayout";
            argumentOptionsLayout.RowCount = 3;
            argumentOptionsLayout.RowStyles.Add(new RowStyle());
            argumentOptionsLayout.RowStyles.Add(new RowStyle());
            argumentOptionsLayout.RowStyles.Add(new RowStyle());
            argumentOptionsLayout.Size = new Size(415, 75);
            argumentOptionsLayout.TabIndex = 7;
            // 
            // argumentIsPassedData
            // 
            argumentIsPassedData.AutoSize = true;
            argumentIsPassedData.Location = new Point(3, 3);
            argumentIsPassedData.Name = "argumentIsPassedData";
            argumentIsPassedData.Size = new Size(62, 19);
            argumentIsPassedData.TabIndex = 0;
            argumentIsPassedData.Text = "Passed";
            argumentIsPassedData.UseVisualStyleBackColor = true;
            // 
            // argumentIsContributorData
            // 
            argumentIsContributorData.AutoSize = true;
            argumentIsContributorData.Location = new Point(141, 3);
            argumentIsContributorData.Name = "argumentIsContributorData";
            argumentIsContributorData.Size = new Size(88, 19);
            argumentIsContributorData.TabIndex = 1;
            argumentIsContributorData.Text = "Contributor";
            argumentIsContributorData.UseVisualStyleBackColor = true;
            // 
            // argumentAsValueData
            // 
            argumentAsValueData.AutoSize = true;
            argumentAsValueData.Location = new Point(3, 53);
            argumentAsValueData.Name = "argumentAsValueData";
            argumentAsValueData.Size = new Size(70, 19);
            argumentAsValueData.TabIndex = 2;
            argumentAsValueData.Text = "As Value";
            argumentAsValueData.UseVisualStyleBackColor = true;
            // 
            // argumentIsReturnedData
            // 
            argumentIsReturnedData.AutoSize = true;
            argumentIsReturnedData.Location = new Point(3, 28);
            argumentIsReturnedData.Name = "argumentIsReturnedData";
            argumentIsReturnedData.Size = new Size(74, 19);
            argumentIsReturnedData.TabIndex = 3;
            argumentIsReturnedData.Text = "Returned";
            argumentIsReturnedData.UseVisualStyleBackColor = true;
            // 
            // argumentIsAlteredData
            // 
            argumentIsAlteredData.AutoSize = true;
            argumentIsAlteredData.Location = new Point(141, 28);
            argumentIsAlteredData.Name = "argumentIsAlteredData";
            argumentIsAlteredData.Size = new Size(64, 19);
            argumentIsAlteredData.TabIndex = 4;
            argumentIsAlteredData.Text = "Altered";
            argumentIsAlteredData.UseVisualStyleBackColor = true;
            // 
            // argumentAsReferenceData
            // 
            argumentAsReferenceData.AutoSize = true;
            argumentAsReferenceData.Location = new Point(141, 53);
            argumentAsReferenceData.Name = "argumentAsReferenceData";
            argumentAsReferenceData.Size = new Size(94, 19);
            argumentAsReferenceData.TabIndex = 5;
            argumentAsReferenceData.Text = "As Reference";
            argumentAsReferenceData.UseVisualStyleBackColor = true;
            // 
            // Process
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(447, 726);
            Controls.Add(mainLayout);
            Name = "Process";
            Text = "Process";
            Load += Process_Load;
            Controls.SetChildIndex(mainLayout, 0);
            mainLayout.ResumeLayout(false);
            mainLayout.PerformLayout();
            detailTabLayout.ResumeLayout(false);
            detailTab.ResumeLayout(false);
            detailLayout.ResumeLayout(false);
            detailLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)argumentData).EndInit();
            argumentLayout.ResumeLayout(false);
            argumentLayout.PerformLayout();
            argumentButtonLayout.ResumeLayout(false);
            propertyTab.ResumeLayout(false);
            definitionTab.ResumeLayout(false);
            aliasTab.ResumeLayout(false);
            subjectAreaTab.ResumeLayout(false);
            subjectAreaLayout.ResumeLayout(false);
            subjectAreaLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingProcess).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingAlias).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingProperty).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSubjectArea).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingDefinition).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingArgument).EndInit();
            argumentOptionsLayout.ResumeLayout(false);
            argumentOptionsLayout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataDictionary.Main.Controls.TextBoxData titleData;
        private DataDictionary.Main.Controls.TextBoxData descriptionData;
        private TabControl detailTabLayout;
        private TabPage detailTab;
        private TabPage propertyTab;
        private Controls.PropertyData propertyData;
        private TabPage definitionTab;
        private Controls.DefinitionData definitionData;
        private TabPage aliasTab;
        private TabPage subjectAreaTab;
        private TableLayoutPanel subjectAreaLayout;
        private Controls.SubjectAreaData subjectArea;
        private DataDictionary.Main.Controls.TextBoxData memberNameData;
        private TableLayoutPanel argumentLayout;
        private DataGridView argumentData;
        private DataGridViewTextBoxColumn argumentTitleColumn;
        private DataGridViewTextBoxColumn ordinalPositionColumn;
        private DataDictionary.Main.Controls.TextBoxData argumentTitleData;
        private DataDictionary.Main.Controls.TextBoxData argumentDescriptionData;
        private DataDictionary.Main.Controls.TextBoxData argumentNameData;
        private DataDictionary.Main.Controls.TextBoxData argumentOrdinalPositionData;
        private CheckBox argumentIsInputData;
        private CheckBox argumentIsOutputData;
        private TableLayoutPanel argumentButtonLayout;
        private Button argumentSelectCommand;
        private Button argumentNewCommand;
        private BindingSource bindingProcess;
        private BindingSource bindingAlias;
        private BindingSource bindingProperty;
        private BindingSource bindingSubjectArea;
        private BindingSource bindingDefinition;
        private BindingSource bindingArgument;
        private TableLayoutPanel detailLayout;
        private Controls.AliasData aliasData;
        private CheckBox argumentIsPassedData;
        private CheckBox argumentIsContributorData;
        private CheckBox argumentAsValueData;
        private CheckBox argumentIsReturnedData;
        private CheckBox argumentIsAlteredData;
        private CheckBox argumentAsReferenceData;
    }
}