using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms
{

    class HistoryView<TValue> : HistoryView
        where TValue : IModificationValue
    {
        
        public HistoryView(ILoadHistoryData loader) : base(loader) 
        { }

        protected override void ViewDetailCommand_Click(Object sender, EventArgs e)
        {
            //base.ViewDetailCommand_Click(sender, e);

            if(base.SelectedValue is TValue)
            {

            }
        }

        protected override void RestoreCommand_Click(Object sender, EventArgs e)
        {
            base.RestoreCommand_Click(sender, e);
        }

    }
}
