namespace DataDictionary.Main.Forms.Scripting
{
    partial class DataBuilder
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
            itemSelection = new TreeView();
            splitContainer = new SplitContainer();
            xmlData = new TextBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            SuspendLayout();
            // 
            // itemSelection
            // 
            itemSelection.CheckBoxes = true;
            itemSelection.Dock = DockStyle.Fill;
            itemSelection.Location = new Point(0, 0);
            itemSelection.Name = "itemSelection";
            itemSelection.Size = new Size(284, 617);
            itemSelection.TabIndex = 4;
            itemSelection.AfterCheck += itemSelection_AfterCheck;
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 25);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(itemSelection);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(xmlData);
            splitContainer.Size = new Size(852, 617);
            splitContainer.SplitterDistance = 284;
            splitContainer.TabIndex = 2;
            // 
            // xmlData
            // 
            xmlData.Dock = DockStyle.Fill;
            xmlData.Location = new Point(0, 0);
            xmlData.Multiline = true;
            xmlData.Name = "xmlData";
            xmlData.ReadOnly = true;
            xmlData.Size = new Size(564, 617);
            xmlData.TabIndex = 0;
            // 
            // DetailXmlView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(852, 642);
            Controls.Add(splitContainer);
            Name = "DetailXmlView";
            Text = "DetailXmlView";
            Load += DetailXmlView_Load;
            Controls.SetChildIndex(splitContainer, 0);
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TreeView itemSelection;
        private SplitContainer splitContainer;
        private TextBox xmlData;
    }
}