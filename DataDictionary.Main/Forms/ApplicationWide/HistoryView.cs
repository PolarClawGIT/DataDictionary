using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.ApplicationWide
{
    /// <summary>
    /// Layout of the History View form
    /// </summary>
    partial class HistoryView : ApplicationData
    {
        ILoadHistoryData? loader;
        List<ITemporalValue> modificationValues = new List<ITemporalValue>();


        // Crosswalk Item in List View back to source.
        [Obsolete()]
        Dictionary<ListViewItem, ITemporalValue> historyValues = new Dictionary<ListViewItem, ITemporalValue>();

        [Obsolete()]
        Dictionary<ListViewItem, ITemporalValue> historyModifications = new Dictionary<ListViewItem, ITemporalValue>();

        public ITemporalValue? SelectedValue { get; protected set; }

        public HistoryView() : base()
        {
            InitializeComponent();
            historyValuesData.ResizeColumns();
            historyModificationData.ResizeColumns();

            HistoryValuesData_Resize(historyValuesData, EventArgs.Empty);
            HistoryModificationData_Resize(historyModificationData, EventArgs.Empty);
        }

        public HistoryView(ILoadHistoryData loader) : this()
        { this.loader = loader; }

        public HistoryView(ITemporalView data) : this()
        {
            // TODO: Re-factor to use ITemporalData instead of ILoadHistoryData
            // The generic version of the form will likely not be needed.
            throw new NotImplementedException("Work in progress");
        }

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

                foreach (var item in modificationValues.OrderBy(o => o.Title).GroupBy(g => g.Index))
                {
                    ITemporalValue lastValue = item.OrderBy(o => o.Temporal.CreatedOn).Last();
                    String modification = DbModificationEnumeration.Cast(lastValue.Temporal.Modification).DisplayName;
                    ListViewItem newItem = new ListViewItem([lastValue.Title, modification]);
                    historyValuesData.Items.Add(newItem);
                    historyValues.Add(newItem, lastValue);
                }
            }
        }

        private void HistoryValuesData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (historyValuesData.
                    SelectedItems.
                    OfType<ListViewItem>().
                    FirstOrDefault() is ListViewItem selectedItem
                && historyValues.
                    TryGetValue(selectedItem, out ITemporalValue? selectedValue))
            {
                SetSummary(selectedValue);

                historyModificationData.Items.Clear();
                historyModifications.Clear();

                foreach (ITemporalValue item in GetHistoryDetail(selectedValue).OrderBy(o => o.Temporal.CreatedOn))
                {
                    String itemModification = DbModificationEnumeration.Cast(item.Temporal.Modification).DisplayName;
                    String itemModifiedOn;
                    if (item.Temporal.CreatedOn is DateTime modifiedOnvalue)
                    { itemModifiedOn = modifiedOnvalue.ToString(); }
                    else { itemModifiedOn = String.Empty; }

                    ListViewItem newItem = new ListViewItem([itemModification, itemModifiedOn]);
                    historyModificationData.Items.Add(newItem);
                    historyModifications.Add(newItem, item);
                }
            }
        }

        protected IEnumerable<ITemporalValue> GetHistoryDetail(ITemporalValue selectedValue)
        { return modificationValues.Where(w => selectedValue.Index.Equals(w.Index)); }

        void HistoryValuesData_Resize(object sender, EventArgs e)
        { historyValuesData.ResizeColumns(); }

        private void HistoryModificationData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (historyModificationData.
                    SelectedItems.
                    OfType<ListViewItem>().
                    FirstOrDefault() is ListViewItem selectedItem
                && historyModifications.
                    TryGetValue(selectedItem, out ITemporalValue? selectedValue))
            { SetSummary(selectedValue); }
        }

        void HistoryModificationData_Resize(object sender, EventArgs e)
        { historyModificationData.ResizeColumns(); }

        void SetSummary(ITemporalValue value)
        {
            SelectedValue = value;

            titleData.Text = value.Title;

            isInsertedData.Checked = (value.Temporal.IsInserted is true);
            isUpdatedData.Checked = (value.Temporal.IsUpdated is true);
            isDeleteData.Checked = (value.Temporal.IsDeleted is true);
            isCurrentData.Checked = (value.Temporal.IsCurrent is true);
            createdByData.Text = value.Temporal.CreatedBy ?? String.Empty;
            removedByData.Text = value.Temporal.RemovedBy ?? String.Empty;

            if (value.Temporal.CreatedOn is DateTime createdOn)
            { createdOnDate.Text = createdOn.ToString(); }
            else { createdOnDate.Text = String.Empty; }

            if (value.Temporal.RemovedOn is DateTime removedOn)
            { removedOnData.Text = removedOn.ToString(); }
            else { removedOnData.Text = String.Empty; }
        }

        private void HistoryModificationData_DoubleClick(object sender, EventArgs e)
        { OpenCommand_Click(sender, e); }

        private void HistoryValuesData_DoubleClick(object sender, EventArgs e)
        { BrowseCommand_Click(sender, e); }
    }
}
