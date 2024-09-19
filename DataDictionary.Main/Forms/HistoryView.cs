using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms
{
    /// <summary>
    /// Layout of the History View form
    /// </summary>
    partial class HistoryView : ApplicationData
    {

        ILoadHistoryData? loader;
        List<IModificationValue> modificationValues = new List<IModificationValue>();

        // Initial width of the columns, for resizing calculations.
        Int32 titleColumnWidth;
        Int32 lastModificationColumnWidth;
        Int32 modificationColumnWidth;
        Int32 modifiedOnColumnWidth;

        public HistoryView() : base()
        {
            InitializeComponent();
            titleColumnWidth = historyTitleColumn.Width;
            lastModificationColumnWidth = historyLastModificationColumn.Width;
            modificationColumnWidth = historyModificationColumn.Width;
            modifiedOnColumnWidth = historyModifiedOnColumn.Width;
        }

        public HistoryView(ILoadHistoryData loader) : this()
        { this.loader = loader; }

        protected virtual void HistoryView_Load(object sender, EventArgs e)
        {
            // Copy the source data into the working set.
            List<WorkItem> work = new List<WorkItem>();
            IDatabaseWork factory = BusinessData.GetDbFactory();
            work.Add(factory.OpenConnection());

            if (loader is not null)
            { work.AddRange(loader.LoadHistory(factory, modificationValues)); }

            DoWork(work, onComplete);

            void onComplete(RunWorkerCompletedEventArgs args)
            { HistoryValueData_Load(); }
        }


        void HistoryValueData_Load()
        {
            historyValuesData.Items.Clear();

            foreach (var item in modificationValues.GroupBy(g => g.Index))
            {
                IModificationValue lastValue = item.OrderBy(o => o.ModifiedOn).Last();
                ListViewItem newItem = new ListViewItem([lastValue.Title, DbModificationEnumeration.Cast(lastValue.Modification).DisplayName]);
                historyValuesData.Items.Add(newItem);
            }

        }

        void HistoryModificationData_Load()
        {
            historyModificationData.Items.Clear();
        }



        protected virtual void ViewDetailCommand_Click(object sender, EventArgs e)
        {

        }

        protected virtual void RestoreCommand_Click(object sender, EventArgs e)
        {

        }

        protected virtual void BindingModification_CurrentItemChanged(object sender, EventArgs e)
        {

        }


        private void HistoryValuesData_Resize(object sender, EventArgs e)
        {
            
        }

        private void HistoryModificationData_Resize(object sender, EventArgs e)
        {

        }
    }
}
