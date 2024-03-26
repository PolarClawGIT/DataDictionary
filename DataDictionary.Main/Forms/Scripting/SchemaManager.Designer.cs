namespace DataDictionary.Main.Forms.Scripting
{
    partial class SchemaManager
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
            TableLayoutPanel schemaManagerLayout;
            schemaNavigation = new DataGridView();
            schemaTitleData = new Controls.TextBoxData();
            schemaDescriptionData = new Controls.TextBoxData();
            bindingSchema = new BindingSource(components);
            schemaToolStrip = new ContextMenuStrip(components);
            addSchemaCommand = new ToolStripMenuItem();
            removeSchemaCommand = new ToolStripMenuItem();
            openSchemaElements = new ToolStripMenuItem();
            schemaManagerLayout = new TableLayoutPanel();
            schemaManagerLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)schemaNavigation).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSchema).BeginInit();
            schemaToolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // schemaManagerLayout
            // 
            schemaManagerLayout.ColumnCount = 1;
            schemaManagerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            schemaManagerLayout.Controls.Add(schemaNavigation, 0, 0);
            schemaManagerLayout.Controls.Add(schemaTitleData, 0, 1);
            schemaManagerLayout.Controls.Add(schemaDescriptionData, 0, 2);
            schemaManagerLayout.Dock = DockStyle.Fill;
            schemaManagerLayout.Location = new Point(0, 25);
            schemaManagerLayout.Name = "schemaManagerLayout";
            schemaManagerLayout.RowCount = 3;
            schemaManagerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            schemaManagerLayout.RowStyles.Add(new RowStyle());
            schemaManagerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            schemaManagerLayout.Size = new Size(448, 353);
            schemaManagerLayout.TabIndex = 4;
            // 
            // schemaNavigation
            // 
            schemaNavigation.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            schemaNavigation.Dock = DockStyle.Fill;
            schemaNavigation.Location = new Point(3, 3);
            schemaNavigation.Name = "schemaNavigation";
            schemaNavigation.Size = new Size(442, 175);
            schemaNavigation.TabIndex = 0;
            // 
            // schemaTitleData
            // 
            schemaTitleData.AutoSize = true;
            schemaTitleData.Dock = DockStyle.Fill;
            schemaTitleData.HeaderText = "Scheme Title";
            schemaTitleData.Location = new Point(3, 184);
            schemaTitleData.Multiline = false;
            schemaTitleData.Name = "schemaTitleData";
            schemaTitleData.ReadOnly = false;
            schemaTitleData.Size = new Size(442, 44);
            schemaTitleData.TabIndex = 1;
            // 
            // schemaDescriptionData
            // 
            schemaDescriptionData.AutoSize = true;
            schemaDescriptionData.Dock = DockStyle.Fill;
            schemaDescriptionData.HeaderText = "Schma Description";
            schemaDescriptionData.Location = new Point(3, 234);
            schemaDescriptionData.Multiline = true;
            schemaDescriptionData.Name = "schemaDescriptionData";
            schemaDescriptionData.ReadOnly = false;
            schemaDescriptionData.Size = new Size(442, 116);
            schemaDescriptionData.TabIndex = 2;
            // 
            // schemaToolStrip
            // 
            schemaToolStrip.Items.AddRange(new ToolStripItem[] { addSchemaCommand, removeSchemaCommand, openSchemaElements });
            schemaToolStrip.Name = "schemaToolStrip";
            schemaToolStrip.Size = new Size(181, 92);
            // 
            // addSchemaCommand
            // 
            addSchemaCommand.Image = Properties.Resources.NewXMLSchema;
            addSchemaCommand.Name = "addSchemaCommand";
            addSchemaCommand.Size = new Size(180, 22);
            addSchemaCommand.Text = "add Schema";
            addSchemaCommand.Click += addSchemaCommand_Click;
            // 
            // removeSchemaCommand
            // 
            removeSchemaCommand.Image = Properties.Resources.DeleteXMLSchema;
            removeSchemaCommand.Name = "removeSchemaCommand";
            removeSchemaCommand.Size = new Size(180, 22);
            removeSchemaCommand.Text = "remove Schema";
            removeSchemaCommand.Click += removeSchemaCommand_Click;
            // 
            // openSchemaElements
            // 
            openSchemaElements.Image = Properties.Resources.XMLElement;
            openSchemaElements.Name = "openSchemaElements";
            openSchemaElements.Size = new Size(180, 22);
            openSchemaElements.Text = "open Elements";
            openSchemaElements.Click += openSchemaElements_Click;
            // 
            // SchemaManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(448, 378);
            Controls.Add(schemaManagerLayout);
            Name = "SchemaManager";
            Text = "Scripting Schema Manager";
            Load += SchemaManager_Load;
            Controls.SetChildIndex(schemaManagerLayout, 0);
            schemaManagerLayout.ResumeLayout(false);
            schemaManagerLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)schemaNavigation).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSchema).EndInit();
            schemaToolStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView schemaNavigation;
        private BindingSource bindingSchema;
        private Controls.TextBoxData schemaTitleData;
        private Controls.TextBoxData schemaDescriptionData;
        private ContextMenuStrip schemaToolStrip;
        private ToolStripMenuItem addSchemaCommand;
        private ToolStripMenuItem openSchemaElements;
        private ToolStripMenuItem removeSchemaCommand;
    }
}