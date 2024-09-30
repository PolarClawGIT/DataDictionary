namespace DataDictionary.Main.ProofOfConcept
{
    partial class NavigationTree
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
            mainTree = new Controls.NamedScopeTreeView();
            SuspendLayout();
            // 
            // mainTree
            // 
            mainTree.Dock = DockStyle.Fill;
            mainTree.DoWork = null;
            mainTree.Location = new Point(0, 0);
            mainTree.Name = "mainTree";
            mainTree.Size = new Size(370, 618);
            mainTree.TabIndex = 0;
            // 
            // NavigationTree
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(370, 618);
            Controls.Add(mainTree);
            Name = "NavigationTree";
            Text = "NavigationTree";
            Load += NavigationTree_Load;
            Controls.SetChildIndex(mainTree, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.NamedScopeTreeView mainTree;
    }
}