using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataDictionary.Main.Forms.Database
{
    partial class DatabaseManager : ApplicationBase
    {
        public DatabaseManager()
        {
            InitializeComponent();
            openToolStripButton.Enabled = true;
            saveToolStripButton.Enabled = Settings.Default.IsOnLineMode;
            openToolStripButton.Click += OpenToolStripButton_Click;
            saveToolStripButton.Click += SaveToolStripButton_Click;

            openToolStripButton.ToolTipText = "Open and add a Database from the Db Schema";
            saveToolStripButton.ToolTipText = "Save the changes back to the application database";
            this.Icon = Resources.Icon_Database;
        }

        private void SaveToolStripButton_Click(object? sender, EventArgs e)
        {
            
        }

        private void OpenToolStripButton_Click(object? sender, EventArgs e)
        {
            
        }

        private void DatabaseManager_Load(object sender, EventArgs e)
        {

        }

        void BindData() { }

        void UnBindData() { }

        #region IColleague
        protected override void HandleMessage(DbDataBatchStarting message)
        { UnBindData(); }

        protected override void HandleMessage(DbDataBatchCompleted message)
        { BindData(); }
        #endregion


    }
}
