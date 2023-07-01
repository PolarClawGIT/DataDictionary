using DataDictionary.Main.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataDictionary.Main.Forms
{
    partial class DbTableView : ApplicationFormBase
    {
        public DbTableView() :base()
        { InitializeComponent(); }


        private void DbTableView_Load(object sender, EventArgs e)
        { BindData(); }

        void BindData()
        { tableData.DataSource = Program.DbData.DbTables; }

        void UnBindData() { tableData.DataSource = null; }

        #region IColleague
        protected override void HandleMessage(DbDataBatchStarting message)
        { UnBindData(); }

        protected override void HandleMessage(DbDataBatchCompleted message)
        { BindData(); }
        #endregion

    }
}
