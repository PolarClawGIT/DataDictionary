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
using Toolbox.Mediator;

namespace DataDictionary.Main.Forms
{
    partial class DbSchemaView : ApplicationFormBase
    {
        public DbSchemaView() : base()
        { InitializeComponent(); }

        private void DbSchemaView_Load(object sender, EventArgs e)
        { BindData(); }

        void BindData()
        { schemaData.DataSource = Program.DbData.DbSchemas; }

        void UnBindData() { schemaData.DataSource = null; }

        #region IColleague
        protected override void HandleMessage(DbDataBatchStarting message)
        { UnBindData(); }

        protected override void HandleMessage(DbDataBatchCompleted message)
        { BindData(); }
        #endregion
    }
}
