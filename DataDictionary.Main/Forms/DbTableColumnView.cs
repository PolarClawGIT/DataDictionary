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

namespace DataDictionary.Main.Forms
{
    [Obsolete("do not use", true)]
    partial class DbTableColumnView : ApplicationFormBase
    {
        public DbTableColumnView() : base()
        {
            InitializeComponent();
            this.Icon = Resources.DbColumn;
        }

        private void DbColumnView_Load(object sender, EventArgs e)
        { BindData(); }

        void BindData()
        { columnData.DataSource = Program.Data.DbColumns; }

        void UnBindData() { columnData.DataSource = null; }

        #region IColleague
        protected override void HandleMessage(DbDataBatchStarting message)
        { UnBindData(); }

        protected override void HandleMessage(DbDataBatchCompleted message)
        { BindData(); }
        #endregion


    }
}
