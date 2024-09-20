﻿using DataDictionary.BusinessLayer;
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
        Dictionary<ColumnHeader, Single> historyValuesWidths;
        Dictionary<ColumnHeader, Single> historyModificationWidths;

        // Crosswalk Item in List View back to source.
        Dictionary<ListViewItem, IModificationValue> historyValues = new Dictionary<ListViewItem, IModificationValue>();
        Dictionary<ListViewItem, IModificationValue> historyModifications = new Dictionary<ListViewItem, IModificationValue>();

        public IModificationValue? SelectedValue { get; protected set; }

        public HistoryView() : base()
        {
            InitializeComponent();

            // Store and recompute column sizes for List views
            historyValuesWidths = historyValuesData.Columns.
                OfType<ColumnHeader>().
                Select(s => new
                {
                    column = s,
                    value = (Single)s.Width / (Single)(historyValuesData.Columns.OfType<ColumnHeader>().Sum(v => v.Width))
                }).
                ToDictionary(k => k.column, v => v.value);

            historyModificationWidths = historyModificationData.Columns.
                OfType<ColumnHeader>().
                Select(s => new
                {
                    column = s,
                    value = (Single)s.Width / (Single)(historyModificationData.Columns.OfType<ColumnHeader>().Sum(v => v.Width))
                }).
                ToDictionary(k => k.column, v => v.value);

            HistoryValuesData_Resize(historyValuesData, EventArgs.Empty);
            HistoryModificationData_Resize(historyModificationData, EventArgs.Empty);
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
            {
                historyValuesData.Items.Clear();
                historyValues.Clear();

                foreach (var item in modificationValues.GroupBy(g => g.Index))
                {
                    IModificationValue lastValue = item.OrderBy(o => o.ModifiedOn).Last();
                    String modification = DbModificationEnumeration.Cast(lastValue.Modification).DisplayName;
                    ListViewItem newItem = new ListViewItem([lastValue.Title, modification]);
                    historyValuesData.Items.Add(newItem);
                    historyValues.Add(newItem, lastValue);
                }
            }
        }

        /// <summary>
        /// Button for Viewing the Details is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>Implement form Activate with this event.</remarks>
        protected virtual void ViewDetailCommand_Click(object sender, EventArgs e)
        { }

        protected virtual void RestoreCommand_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void HistoryValuesData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (historyValuesData.
                    SelectedItems.
                    OfType<ListViewItem>().
                    FirstOrDefault() is ListViewItem selectedItem
                && historyValues.
                    TryGetValue(selectedItem, out IModificationValue? selectedValue))
            {
                SetSummary(selectedValue);

                historyModificationData.Items.Clear();
                historyModifications.Clear();

                foreach (IModificationValue item in modificationValues.Where(w => selectedValue.Index.Equals(w.Index)))
                {
                    String itemModification = DbModificationEnumeration.Cast(item.Modification).DisplayName;
                    String itemModifiedOn;
                    if (item.ModifiedOn is DateTime modifiedOnvalue)
                    { itemModifiedOn = modifiedOnvalue.ToString(); }
                    else { itemModifiedOn = String.Empty; }

                    ListViewItem newItem = new ListViewItem([itemModification, itemModifiedOn]);
                    historyModificationData.Items.Add(newItem);
                    historyModifications.Add(newItem, item);
                }
            }
        }

        void HistoryValuesData_Resize(object sender, EventArgs e)
        {
            historyValuesData.Columns.
                OfType<ColumnHeader>().
                ToList().
                ForEach(f => f.Width = (Int32)(
                    (historyValuesData.Width -
                        SystemInformation.VerticalScrollBarWidth) *
                    historyValuesWidths[f]));
        }

        private void HistoryModificationData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (historyModificationData.
                    SelectedItems.
                    OfType<ListViewItem>().
                    FirstOrDefault() is ListViewItem selectedItem
                && historyModifications.
                    TryGetValue(selectedItem, out IModificationValue? selectedValue))
            { SetSummary(selectedValue); }
        }

        void HistoryModificationData_Resize(object sender, EventArgs e)
        {
            historyModificationData.Columns.
                OfType<ColumnHeader>().
                ToList().
                ForEach(f => f.Width = (Int32)(
                    (historyModificationData.Width -
                        SystemInformation.VerticalScrollBarWidth) *
                    historyModificationWidths[f]));
        }

        void SetSummary(IModificationValue value)
        {
            SelectedValue = value;

            titleData.Text = value.Title;
            modificationData.Text = DbModificationEnumeration.Cast(value.Modification).DisplayName;

            if (value.ModifiedBy is String modifiedBy)
            { modifiedByData.Text = modifiedBy; }
            else { modifiedByData.Text = String.Empty; }

            if (value.ModifiedOn is DateTime modifiedOn)
            { modifiedOnDate.Text = modifiedOn.ToString(); }
            else { modifiedOnDate.Text = String.Empty; }
        }
    }
}
