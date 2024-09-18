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

    class HistoryView<TValue, TKey> : HistoryView
        where TValue : IModificationValue
    {
        IModificationData modificationValues;

        public HistoryView(Func<IModificationData> data) : base()
        {
            modificationValues = data();
        }

        protected override void HistoryView_Load(Object sender, EventArgs e)
        {
            List<WorkItem> work = new List<WorkItem>();
            IDatabaseWork factory = BusinessData.GetDbFactory();
            work.Add(factory.OpenConnection());
            work.AddRange(modificationValues.Load(factory, true));

            DoWork(work, onComplete);

            void onComplete(RunWorkerCompletedEventArgs args)
            { base.HistoryView_Load(sender, e); }
        }

    }
}
