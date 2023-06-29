using DataDictionary.Main.Controls;
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
    partial class DbExtendedPropertyView : ApplicationFormBase
    {
        public DbExtendedPropertyView() : base()
        {
            InitializeComponent();
        }

        private void DbExtendedPropertyView_Load(object sender, EventArgs e)
        { BindData(); }

        void BindData()
        { extendedPropertyData.DataSource = Program.DbData.DbExtendedProperties; }

        void UnBindData()
        { extendedPropertyData.DataSource = null; }

        #region IColleague
        protected override void HandleMessage(DbDataBatchStarting message)
        { base.HandleMessage(message); UnBindData(); }

        protected override void HandleMessage(DbDataBatchCompleted message)
        { base.HandleMessage(message); BindData(); }
        #endregion


    }
}
