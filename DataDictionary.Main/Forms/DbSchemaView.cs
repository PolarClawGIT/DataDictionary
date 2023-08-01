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
using Toolbox.Mediator;

namespace DataDictionary.Main.Forms
{
    [Obsolete("do not use", true)]
    partial class DbSchemaView : ApplicationFormBase
    {
        public DbSchemaView() : base()
        {
            InitializeComponent();
            this.Icon = Resources.DbSchema;
        }

        private void DbSchemaView_Load(object sender, EventArgs e)
        { BindData(); }

        void BindData()
        { schemaData.DataSource = Program.Data.DbSchemta; }

        void UnBindData() { schemaData.DataSource = null; }

        #region IColleague
        protected override void HandleMessage(DbDataBatchStarting message)
        { UnBindData(); }

        protected override void HandleMessage(DbDataBatchCompleted message)
        { BindData(); }
        #endregion
    }
}
