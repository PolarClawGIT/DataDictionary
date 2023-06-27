using DataDictionary.DataLayer.DbMetaData;
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
    partial class DbColumn : ApplicationFormBase
    {
        IDbColumnItem? thisData;

        public DbColumn() : base()
        { 
            InitializeComponent(); 
            Program.Messenger.AddColleague(this);
            SendMessage(new FormAddMdiChild() { ChildForm = this });
        }

        public DbColumn(IDbColumnItem data) :this()
        { thisData = data; }

        private void DbColumn_Load(object sender, EventArgs e)
        {
            
        }

        #region IColleague
        #endregion
    }
}
