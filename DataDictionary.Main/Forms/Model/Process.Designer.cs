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
            TableLayoutPanel aliasCommandLayout;
            titleData = new DataDictionary.Main.Controls.TextBoxData();
            descriptionData = new DataDictionary.Main.Controls.TextBoxData();
            detailTabLayout = new TabControl();
            detailTab = new TabPage();
            argumentLayout = new TableLayoutPanel();
            argumentData = new DataGridView();
            argumentTitleColumn = new DataGridViewTextBoxColumn();
            ordinalPositionColumn = new DataGridViewTextBoxColumn();
            argumentTitleData = new DataDictionary.Main.Controls.TextBoxData();
            argumentDescriptionData = new DataDictionary.Main.Controls.TextBoxData();
            argumentNameData = new DataDictionary.Main.Controls.TextBoxData();
            argumentTypeData = new DataDictionary.Main.Controls.TextBoxData();
            argumentOptionLayout = new TableLayoutPanel();
            argumentOrdinalPositionDat = new DataDictionary.Main.Controls.TextBoxData();
            ArgumentIsInputData = new CheckBox();
            argumentIsOutput = new CheckBox();
            argumentButtonLayout = new TableLayoutPanel();
            argumentSelectCommand = new Button();
            argumentNewCommand = new Button();
            propertyTab = new TabPage();
            propertyData = new DataDictionary.Main.Forms.Model.Controls.Property();
            definitionTab = new TabPage();
            definitionData = new DataDictionary.Main.Forms.Model.Controls.Definition();
            aliasTab = new TabPage();
            aliaseLayout = new TableLayoutPanel();
            aliasesData = new DataGridView();
            aliaseScopeColumn = new DataGridViewComboBoxColumn();
            aliasNameColumn = new DataGridViewTextBoxColumn();
            aliasNameData = new DataDictionary.Main.Controls.TextBoxData();
            aliasScopeData = new DataDictionary.Main.Controls.ComboBoxData();
            aliasSelectCommand = new Button();
            aliasAddCommand = new Button();
            isAliasInModelData = new CheckBox();
            subjectAreaTab = new TabPage();
            subjectAreaLayout = new TableLayoutPanel();
            subjectArea = new DataDictionary.Main.Forms.Model.Controls.SubjectArea();
            memberNameData = new DataDictionary.Main.Controls.TextBoxData();
            bindingProcess = new BindingSource(components);
            bindingAlias = new BindingSource(components);
            bindingProperty = new BindingSource(components);
            bindingSubjectArea = new BindingSource(components);
            bindingDefinition = new BindingSource(components);
            bindingArgument = new BindingSource(components);
            mainLayout = new TableLayoutPanel();
            aliasCommandLayout = new TableLayoutPanel();
            mainLayout.SuspendLayout();
            detailTabLayout.SuspendLayout();
            detailTab.SuspendLayout();
            argumentLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)argumentData).BeginInit();
            argumentOptionLayout.SuspendLayout();
            argumentButtonLayout.SuspendLayout();
            propertyTab.SuspendLayout();
            definitionTab.SuspendLayout();
            aliasTab.SuspendLayout();
            aliaseLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)aliasesData).BeginInit();
            aliasCommandLayout.SuspendLayout();
            subjectAreaTab.SuspendLayout();
            subjectAreaLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingProcess).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingAlias).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingProperty).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSubjectArea).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingDefinition).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingArgument).BeginInit();
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
            mainLayout.Size = new Size(460, 640);
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
            titleData.Size = new Size(454, 44);
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
            descriptionData.Size = new Size(454, 112);
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
            detailTabLayout.Location = new Point(3, 171);
            detailTabLayout.Name = "detailTabLayout";
            detailTabLayout.SelectedIndex = 0;
            detailTabLayout.Size = new Size(454, 466);
            detailTabLayout.TabIndex = 2;
            // 
            // detailTab
            // 
            detailTab.BackColor = SystemColors.Control;
            detailTab.Controls.Add(argumentLayout);
            detailTab.Location = new Point(4, 24);
            detailTab.Name = "detailTab";
            detailTab.Padding = new Padding(3);
            detailTab.Size = new Size(446, 438);
            detailTab.TabIndex = 0;
            detailTab.Text = "Details";
            // 
            // argumentLayout
            // 
            argumentLayout.ColumnCount = 2;
            argumentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.6666641F));
            argumentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            argumentLayout.Controls.Add(argumentData, 0, 0);
            argumentLayout.Controls.Add(argumentTitleData, 0, 1);
            argumentLayout.Controls.Add(argumentDescriptionData, 0, 2);
            argumentLayout.Controls.Add(argumentNameData, 0, 3);
            argumentLayout.Controls.Add(argumentTypeData, 0, 4);
            argumentLayout.Controls.Add(argumentOptionLayout, 1, 3);
            argumentLayout.Controls.Add(argumentButtonLayout, 0, 5);
            argumentLayout.Dock = DockStyle.Fill;
            argumentLayout.Location = new Point(3, 3);
            argumentLayout.Name = "argumentLayout";
            argumentLayout.RowCount = 6;
            argumentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            argumentLayout.RowStyles.Add(new RowStyle());
            argumentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            argumentLayout.RowStyles.Add(new RowStyle());
            argumentLayout.RowStyles.Add(new RowStyle());
            argumentLayout.RowStyles.Add(new RowStyle());
            argumentLayout.Size = new Size(440, 432);
            argumentLayout.TabIndex = 0;
            // 
            // argumentData
            // 
            argumentData.AllowUserToAddRows = false;
            argumentData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            argumentData.Columns.AddRange(new DataGridViewColumn[] { argumentTitleColumn, ordinalPositionColumn });
            argumentLayout.SetColumnSpan(argumentData, 2);
            argumentData.Dock = DockStyle.Fill;
            argumentData.Location = new Point(3, 3);
            argumentData.Name = "argumentData";
            argumentData.ReadOnly = true;
            argumentData.Size = new Size(434, 138);
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
            // argumentTitleData
            // 
            argumentTitleData.AutoSize = true;
            argumentLayout.SetColumnSpan(argumentTitleData, 2);
            argumentTitleData.Dock = DockStyle.Fill;
            argumentTitleData.HeaderText = "Argument Title";
            argumentTitleData.Location = new Point(3, 147);
            argumentTitleData.Multiline = false;
            argumentTitleData.Name = "argumentTitleData";
            argumentTitleData.ReadOnly = false;
            argumentTitleData.Size = new Size(434, 44);
            argumentTitleData.TabIndex = 1;
            argumentTitleData.WordWrap = true;
            // 
            // argumentDescriptionData
            // 
            argumentDescriptionData.AutoSize = true;
            argumentLayout.SetColumnSpan(argumentDescriptionData, 2);
            argumentDescriptionData.Dock = DockStyle.Fill;
            argumentDescriptionData.HeaderText = "Argument Description";
            argumentDescriptionData.Location = new Point(3, 197);
            argumentDescriptionData.Multiline = true;
            argumentDescriptionData.Name = "argumentDescriptionData";
            argumentDescriptionData.ReadOnly = false;
            argumentDescriptionData.Size = new Size(434, 90);
            argumentDescriptionData.TabIndex = 2;
            argumentDescriptionData.WordWrap = true;
            // 
            // argumentNameData
            // 
            argumentNameData.AutoSize = true;
            argumentNameData.Dock = DockStyle.Fill;
            argumentNameData.HeaderText = "Argument Name";
            argumentNameData.Location = new Point(3, 293);
            argumentNameData.Multiline = false;
            argumentNameData.Name = "argumentNameData";
            argumentNameData.ReadOnly = false;
            argumentNameData.Size = new Size(287, 44);
            argumentNameData.TabIndex = 3;
            argumentNameData.WordWrap = true;
            // 
            // argumentTypeData
            // 
            argumentTypeData.AutoSize = true;
            argumentTypeData.Dock = DockStyle.Fill;
            argumentTypeData.HeaderText = "Argument Type";
            argumentTypeData.Location = new Point(3, 343);
            argumentTypeData.Multiline = false;
            argumentTypeData.Name = "argumentTypeData";
            argumentTypeData.ReadOnly = false;
            argumentTypeData.Size = new Size(287, 50);
            argumentTypeData.TabIndex = 5;
            argumentTypeData.WordWrap = true;
            // 
            // argumentOptionLayout
            // 
            argumentOptionLayout.ColumnCount = 1;
            argumentOptionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            argumentOptionLayout.Controls.Add(argumentOrdinalPositionDat, 0, 0);
            argumentOptionLayout.Controls.Add(ArgumentIsInputData, 0, 1);
            argumentOptionLayout.Controls.Add(argumentIsOutput, 0, 2);
            argumentOptionLayout.Dock = DockStyle.Fill;
            argumentOptionLayout.Location = new Point(296, 293);
            argumentOptionLayout.Name = "argumentOptionLayout";
            argumentOptionLayout.RowCount = 3;
            argumentLayout.SetRowSpan(argumentOptionLayout, 2);
            argumentOptionLayout.RowStyles.Add(new RowStyle());
            argumentOptionLayout.RowStyles.Add(new RowStyle());
            argumentOptionLayout.RowStyles.Add(new RowStyle());
            argumentOptionLayout.Size = new Size(141, 100);
            argumentOptionLayout.TabIndex = 6;
            // 
            // argumentOrdinalPositionDat
            // 
            argumentOrdinalPositionDat.AutoSize = true;
            argumentOrdinalPositionDat.HeaderText = "Ordinal Position";
            argumentOrdinalPositionDat.Location = new Point(3, 3);
            argumentOrdinalPositionDat.Multiline = false;
            argumentOrdinalPositionDat.Name = "argumentOrdinalPositionDat";
            argumentOrdinalPositionDat.ReadOnly = false;
            argumentOrdinalPositionDat.Size = new Size(120, 44);
            argumentOrdinalPositionDat.TabIndex = 4;
            argumentOrdinalPositionDat.WordWrap = true;
            // 
            // ArgumentIsInputData
            // 
            ArgumentIsInputData.AutoSize = true;
            ArgumentIsInputData.Location = new Point(3, 53);
            ArgumentIsInputData.Name = "ArgumentIsInputData";
            ArgumentIsInputData.Size = new Size(65, 19);
            ArgumentIsInputData.TabIndex = 5;
            ArgumentIsInputData.Text = "Is Input";
            ArgumentIsInputData.UseVisualStyleBackColor = true;
            // 
            // argumentIsOutput
            // 
            argumentIsOutput.AutoSize = true;
            argumentIsOutput.Location = new Point(3, 78);
            argumentIsOutput.Name = "argumentIsOutput";
            argumentIsOutput.Size = new Size(75, 19);
            argumentIsOutput.TabIndex = 6;
            argumentIsOutput.Text = "Is Output";
            argumentIsOutput.UseVisualStyleBackColor = true;
            // 
            // argumentButtonLayout
            // 
            argumentButtonLayout.AutoSize = true;
            argumentButtonLayout.ColumnCount = 3;
            argumentLayout.SetColumnSpan(argumentButtonLayout, 2);
            argumentButtonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            argumentButtonLayout.ColumnStyles.Add(new ColumnStyle());
            argumentButtonLayout.ColumnStyles.Add(new ColumnStyle());
            argumentButtonLayout.Controls.Add(argumentSelectCommand, 2, 0);
            argumentButtonLayout.Controls.Add(argumentNewCommand, 1, 0);
            argumentButtonLayout.Dock = DockStyle.Fill;
            argumentButtonLayout.Location = new Point(3, 399);
            argumentButtonLayout.Name = "argumentButtonLayout";
            argumentButtonLayout.RowCount = 1;
            argumentButtonLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            argumentButtonLayout.Size = new Size(434, 30);
            argumentButtonLayout.TabIndex = 7;
            // 
            // argumentSelectCommand
            // 
            argumentSelectCommand.Location = new Point(356, 3);
            argumentSelectCommand.Name = "argumentSelectCommand";
            argumentSelectCommand.Size = new Size(75, 23);
            argumentSelectCommand.TabIndex = 1;
            argumentSelectCommand.Text = "Select";
            argumentSelectCommand.TextImageRelation = TextImageRelation.ImageBeforeText;
            argumentSelectCommand.UseVisualStyleBackColor = true;
            // 
            // argumentNewCommand
            // 
            argumentNewCommand.Location = new Point(275, 3);
            argumentNewCommand.Name = "argumentNewCommand";
            argumentNewCommand.Size = new Size(75, 23);
            argumentNewCommand.TabIndex = 0;
            argumentNewCommand.Text = "New";
            argumentNewCommand.TextImageRelation = TextImageRelation.ImageBeforeText;
            argumentNewCommand.UseVisualStyleBackColor = true;
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
            aliasTab.Controls.Add(aliaseLayout);
            aliasTab.Location = new Point(4, 24);
            aliasTab.Name = "aliasTab";
            aliasTab.Padding = new Padding(3);
            aliasTab.Size = new Size(192, 72);
            aliasTab.TabIndex = 2;
            aliasTab.Text = "Aliases";
            // 
            // aliaseLayout
            // 
            aliaseLayout.ColumnCount = 2;
            aliaseLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            aliaseLayout.ColumnStyles.Add(new ColumnStyle());
            aliaseLayout.Controls.Add(aliasesData, 0, 0);
            aliaseLayout.Controls.Add(aliasNameData, 0, 2);
            aliaseLayout.Controls.Add(aliasScopeData, 0, 1);
            aliaseLayout.Controls.Add(aliasCommandLayout, 1, 1);
            aliaseLayout.Dock = DockStyle.Fill;
            aliaseLayout.Location = new Point(3, 3);
            aliaseLayout.Name = "aliaseLayout";
            aliaseLayout.RowCount = 3;
            aliaseLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            aliaseLayout.RowStyles.Add(new RowStyle());
            aliaseLayout.RowStyles.Add(new RowStyle());
            aliaseLayout.Size = new Size(186, 66);
            aliaseLayout.TabIndex = 1;
            // 
            // aliasesData
            // 
            aliasesData.AllowUserToAddRows = false;
            aliasesData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            aliasesData.Columns.AddRange(new DataGridViewColumn[] { aliaseScopeColumn, aliasNameColumn });
            aliaseLayout.SetColumnSpan(aliasesData, 2);
            aliasesData.Dock = DockStyle.Fill;
            aliasesData.Location = new Point(3, 3);
            aliasesData.Name = "aliasesData";
            aliasesData.ReadOnly = true;
            aliasesData.Size = new Size(180, 1);
            aliasesData.TabIndex = 0;
            // 
            // aliaseScopeColumn
            // 
            aliaseScopeColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            aliaseScopeColumn.DataPropertyName = "AliasScope";
            aliaseScopeColumn.FillWeight = 50F;
            aliaseScopeColumn.HeaderText = "Scope";
            aliaseScopeColumn.Name = "aliaseScopeColumn";
            aliaseScopeColumn.ReadOnly = true;
            // 
            // aliasNameColumn
            // 
            aliasNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            aliasNameColumn.DataPropertyName = "AliasPath";
            aliasNameColumn.HeaderText = "Alias Name";
            aliasNameColumn.Name = "aliasNameColumn";
            aliasNameColumn.ReadOnly = true;
            // 
            // aliasNameData
            // 
            aliasNameData.AutoSize = true;
            aliasNameData.Dock = DockStyle.Fill;
            aliasNameData.HeaderText = "Alias Name";
            aliasNameData.Location = new Point(3, 19);
            aliasNameData.Multiline = false;
            aliasNameData.Name = "aliasNameData";
            aliasNameData.ReadOnly = true;
            aliasNameData.Size = new Size(93, 44);
            aliasNameData.TabIndex = 2;
            aliasNameData.WordWrap = true;
            // 
            // aliasScopeData
            // 
            aliasScopeData.AutoSize = true;
            aliasScopeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            aliasScopeData.Dock = DockStyle.Fill;
            aliasScopeData.DropDownStyle = ComboBoxStyle.DropDown;
            aliasScopeData.HeaderText = "Scope";
            aliasScopeData.Location = new Point(3, -33);
            aliasScopeData.Name = "aliasScopeData";
            aliasScopeData.ReadOnly = true;
            aliasScopeData.Size = new Size(93, 46);
            aliasScopeData.TabIndex = 1;
            // 
            // aliasCommandLayout
            // 
            aliasCommandLayout.AutoSize = true;
            aliasCommandLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            aliasCommandLayout.ColumnCount = 1;
            aliasCommandLayout.ColumnStyles.Add(new ColumnStyle());
            aliasCommandLayout.Controls.Add(aliasSelectCommand, 0, 1);
            aliasCommandLayout.Controls.Add(aliasAddCommand, 0, 2);
            aliasCommandLayout.Controls.Add(isAliasInModelData, 0, 0);
            aliasCommandLayout.Dock = DockStyle.Fill;
            aliasCommandLayout.Location = new Point(102, -33);
            aliasCommandLayout.Name = "aliasCommandLayout";
            aliasCommandLayout.RowCount = 3;
            aliaseLayout.SetRowSpan(aliasCommandLayout, 2);
            aliasCommandLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            aliasCommandLayout.RowStyles.Add(new RowStyle());
            aliasCommandLayout.RowStyles.Add(new RowStyle());
            aliasCommandLayout.Size = new Size(81, 96);
            aliasCommandLayout.TabIndex = 4;
            // 
            // aliasSelectCommand
            // 
            aliasSelectCommand.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            aliasSelectCommand.Location = new Point(3, 39);
            aliasSelectCommand.Name = "aliasSelectCommand";
            aliasSelectCommand.Size = new Size(75, 24);
            aliasSelectCommand.TabIndex = 1;
            aliasSelectCommand.Text = "Select";
            aliasSelectCommand.TextImageRelation = TextImageRelation.ImageBeforeText;
            aliasSelectCommand.UseVisualStyleBackColor = true;
            // 
            // aliasAddCommand
            // 
            aliasAddCommand.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            aliasAddCommand.Location = new Point(3, 69);
            aliasAddCommand.Name = "aliasAddCommand";
            aliasAddCommand.Size = new Size(75, 24);
            aliasAddCommand.TabIndex = 2;
            aliasAddCommand.Text = "New";
            aliasAddCommand.TextImageRelation = TextImageRelation.ImageBeforeText;
            aliasAddCommand.UseVisualStyleBackColor = true;
            // 
            // isAliasInModelData
            // 
            isAliasInModelData.AutoSize = true;
            isAliasInModelData.Enabled = false;
            isAliasInModelData.Location = new Point(3, 3);
            isAliasInModelData.Name = "isAliasInModelData";
            isAliasInModelData.Size = new Size(73, 19);
            isAliasInModelData.TabIndex = 0;
            isAliasInModelData.Text = "in Model";
            isAliasInModelData.UseVisualStyleBackColor = true;
            // 
            // subjectAreaTab
            // 
            subjectAreaTab.BackColor = SystemColors.Control;
            subjectAreaTab.Controls.Add(subjectAreaLayout);
            subjectAreaTab.Location = new Point(4, 24);
            subjectAreaTab.Name = "subjectAreaTab";
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
            subjectAreaLayout.Location = new Point(0, 0);
            subjectAreaLayout.Name = "subjectAreaLayout";
            subjectAreaLayout.RowCount = 2;
            subjectAreaLayout.RowStyles.Add(new RowStyle());
            subjectAreaLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            subjectAreaLayout.Size = new Size(192, 72);
            subjectAreaLayout.TabIndex = 1;
            // 
            // subjectArea
            // 
            subjectArea.Dock = DockStyle.Fill;
            subjectArea.Location = new Point(3, 53);
            subjectArea.Name = "subjectArea";
            subjectArea.Size = new Size(186, 16);
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
            memberNameData.Size = new Size(186, 44);
            memberNameData.TabIndex = 1;
            memberNameData.WordWrap = true;
            // 
            // Process
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(460, 665);
            Controls.Add(mainLayout);
            Name = "Process";
            Text = "Process";
            Controls.SetChildIndex(mainLayout, 0);
            mainLayout.ResumeLayout(false);
            mainLayout.PerformLayout();
            detailTabLayout.ResumeLayout(false);
            detailTab.ResumeLayout(false);
            argumentLayout.ResumeLayout(false);
            argumentLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)argumentData).EndInit();
            argumentOptionLayout.ResumeLayout(false);
            argumentOptionLayout.PerformLayout();
            argumentButtonLayout.ResumeLayout(false);
            propertyTab.ResumeLayout(false);
            definitionTab.ResumeLayout(false);
            aliasTab.ResumeLayout(false);
            aliaseLayout.ResumeLayout(false);
            aliaseLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)aliasesData).EndInit();
            aliasCommandLayout.ResumeLayout(false);
            aliasCommandLayout.PerformLayout();
            subjectAreaTab.ResumeLayout(false);
            subjectAreaLayout.ResumeLayout(false);
            subjectAreaLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingProcess).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingAlias).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingProperty).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSubjectArea).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingDefinition).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingArgument).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataDictionary.Main.Controls.TextBoxData titleData;
        private DataDictionary.Main.Controls.TextBoxData descriptionData;
        private TabControl detailTabLayout;
        private TabPage detailTab;
        private TabPage propertyTab;
        private Controls.Property propertyData;
        private TabPage definitionTab;
        private Controls.Definition definitionData;
        private TabPage aliasTab;
        private TableLayoutPanel aliaseLayout;
        private DataGridView aliasesData;
        private DataGridViewComboBoxColumn aliaseScopeColumn;
        private DataGridViewTextBoxColumn aliasNameColumn;
        private DataDictionary.Main.Controls.TextBoxData aliasNameData;
        private DataDictionary.Main.Controls.ComboBoxData aliasScopeData;
        private Button aliasSelectCommand;
        private Button aliasAddCommand;
        private CheckBox isAliasInModelData;
        private TabPage subjectAreaTab;
        private TableLayoutPanel subjectAreaLayout;
        private Controls.SubjectArea subjectArea;
        private DataDictionary.Main.Controls.TextBoxData memberNameData;
        private TableLayoutPanel argumentLayout;
        private DataGridView argumentData;
        private DataGridViewTextBoxColumn argumentTitleColumn;
        private DataGridViewTextBoxColumn ordinalPositionColumn;
        private DataDictionary.Main.Controls.TextBoxData argumentTitleData;
        private DataDictionary.Main.Controls.TextBoxData argumentDescriptionData;
        private DataDictionary.Main.Controls.TextBoxData argumentNameData;
        private DataDictionary.Main.Controls.TextBoxData argumentOrdinalPositionDat;
        private DataDictionary.Main.Controls.TextBoxData argumentTypeData;
        private TableLayoutPanel argumentOptionLayout;
        private CheckBox ArgumentIsInputData;
        private CheckBox argumentIsOutput;
        private TableLayoutPanel argumentButtonLayout;
        private Button argumentSelectCommand;
        private Button argumentNewCommand;
        private BindingSource bindingProcess;
        private BindingSource bindingAlias;
        private BindingSource bindingProperty;
        private BindingSource bindingSubjectArea;
        private BindingSource bindingDefinition;
        private BindingSource bindingArgument;
    }
}