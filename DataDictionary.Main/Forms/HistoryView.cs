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

        IModificationData? sourceData = null;
        HistoryViewItems dataValues = new HistoryViewItems() { };

        public HistoryView() : base()
        {
            InitializeComponent();
        }

        public HistoryView(Func<IModificationData> data) : this()
        {
            sourceData = data();
        }

        protected virtual void HistoryView_Load(object sender, EventArgs e)
        {
            // Copy the source data into the working set.
            List<WorkItem> work = new List<WorkItem>();
            IDatabaseWork factory = BusinessData.GetDbFactory();
            work.Add(factory.OpenConnection());
            if (sourceData is not null)
            {
                work.AddRange(sourceData.Load(factory, true));
                work.Add(new WorkItem()
                {
                    DoWork = () =>
                    {
                        if (sourceData is IEnumerable<IModificationValue> values)
                        {
                            foreach (IModificationValue item in values)
                            { dataValues.Add(new HistoryViewItem(item)); }
                        }
                    }
                });
            }

            DoWork(work, onComplete);

            void onComplete(RunWorkerCompletedEventArgs args)
            { HistoryData_Load(); }
        }

        Dictionary<ListViewItem, HistoryViewItem> historyValues = new Dictionary<ListViewItem, HistoryViewItem>();
        void HistoryData_Load()
        {
            historyData.Groups.Clear();
            historyData.Items.Clear();
            historyValues.Clear();

            foreach (var historyGroups in dataValues.OrderBy(o => o.ModifiedOn).GroupBy(g => g.Index))
            {
                HistoryViewItem LastItem = historyGroups.OrderBy(o => o.ModifiedOn).Last();
                ListViewGroup newGroup = new ListViewGroup(LastItem.Title);

                foreach (HistoryViewItem historyItem in historyGroups)
                {
                    List<String> values = new List<string>();
                    values.Add(DbModificationEnumeration.Cast(historyItem.Modification).DisplayName);
                    if (historyItem.ModifiedOn is DateTime modifiedOnValue)
                    { values.Add(modifiedOnValue.ToString()); }
                    else { values.Add(String.Empty); }

                    ListViewItem newItem = new ListViewItem(values.ToArray(), newGroup);
                    historyData.Items.Add(newItem);
                    historyValues.Add(newItem, historyItem);
                }

                historyData.Groups.Add(newGroup);
            }
        }

        private void HistoryData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (historyData.SelectedItems.Count > 0 && historyValues.ContainsKey(historyData.SelectedItems[0]))
            {
                var selectedItem = historyValues[(historyData.SelectedItems[0])];
                titleData.Text = selectedItem.Title;
                descriptionData.Text = selectedItem.Description;
                modificationData.Text = DbModificationEnumeration.Cast(selectedItem.Modification).DisplayName;

                if(selectedItem.ModifiedOn is DateTime modifiedOnValue)
                { modifiedOnDate.Text = modifiedOnValue.ToString(); }
                else { modifiedOnDate.Text = String.Empty; }

                if (selectedItem.ModifiedBy is String modifiedByValue)
                { modifiedByData.Text = modifiedByValue; }
                else { modifiedByData.Text = String.Empty; }
            }
        }

        protected virtual void ViewDetailCommand_Click(object sender, EventArgs e)
        {

        }

        protected virtual void RestoreCommand_Click(object sender, EventArgs e)
        {

        }

        protected virtual void ReQueryCommand_Click(object sender, EventArgs e)
        {

        }

        protected virtual void BindingModification_CurrentItemChanged(object sender, EventArgs e)
        {

        }


    }
}
