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

    class HistoryView<TValue, TForm> : HistoryView
        where TValue : ITemporalValue
        where TForm : ApplicationBase
    {
        /// <summary>
        /// The Constructor for the Detail/Selected Form.
        /// </summary>
        public Func<TValue, TForm>? SelectedForm { get; init; }


        public HistoryView(ILoadHistoryData loader) : base(loader)
        { }

        protected override void SelectCommand_Click(Object sender, EventArgs e)
        {
            base.SelectCommand_Click(sender, e);

            if (SelectedValue is TValue value && SelectedForm is not null)
            { Activate(() => SelectedForm(value)); }
        }

        protected override void RestoreCommand_Click(Object sender, EventArgs e)
        {
            base.RestoreCommand_Click(sender, e);
        }

    }
}
