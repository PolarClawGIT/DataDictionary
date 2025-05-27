using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.ApplicationWide
{
    partial class HistoryView : ApplicationData
    {
        ITemporalData formData = null!;

        // Crosswalk Item in List View back to source.
        Dictionary<ListViewItem, IDataValue> historyValues = new Dictionary<ListViewItem, IDataValue>();
        Dictionary<ListViewItem, TemporalValue> historyModifications = new Dictionary<ListViewItem, TemporalValue>();

        /// <summary>
        /// Function to open the detail form.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<TemporalValue, Form>? OpenForm { get; init; }

        protected HistoryView() : base()
        {
            InitializeComponent();
            SetIcon(ScopeType.ApplicationHelp);
            SetCommand(
                ScopeType.ApplicationHelp,
                CommandImageType.Open);

            historyValuesData.ResizeColumns();
            historyModificationData.ResizeColumns();

            HistoryValuesData_Resize(historyValuesData, EventArgs.Empty);
            HistoryModificationData_Resize(historyModificationData, EventArgs.Empty);
        }

        public HistoryView(ITemporalData data) : this()
        { formData = data; }

        protected virtual void HistoryView_Load(object sender, EventArgs e)
        {
            // Copy the source data into the working set.
            List<WorkItem> work = new List<WorkItem>();
            IDatabaseWork factory = BusinessData.GetDbFactory();
            work.Add(factory.OpenConnection());
            work.AddRange(formData.Load(factory));

            DoWork(work, onComplete);

            void onComplete(RunWorkerCompletedEventArgs args)
            {
                bindingHistory.DataSource = formData;
                titleData.DataBindings.Add(new Binding(nameof(titleData.Text), bindingHistory, nameof(ITemporalValue.Title)));
                isInsertedData.DataBindings.Add(new Binding(nameof(isInsertedData.Checked), bindingHistory, nameof(ITemporalValue.IsInserted)));
                isDeleteData.DataBindings.Add(new Binding(nameof(isDeleteData.Checked), bindingHistory, nameof(ITemporalValue.IsDeleted)));
                isUpdatedData.DataBindings.Add(new Binding(nameof(isUpdatedData.Checked), bindingHistory, nameof(ITemporalValue.IsUpdated)));
                isCurrentData.DataBindings.Add(new Binding(nameof(isCurrentData.Checked), bindingHistory, nameof(ITemporalValue.IsCurrent)));
                createdByData.DataBindings.Add(new Binding(nameof(createdByData.Text), bindingHistory, nameof(ITemporalValue.CreatedBy)));
                createdOnDate.DataBindings.Add(new Binding(nameof(createdOnDate.Text), bindingHistory, nameof(ITemporalValue.CreatedOn),true, DataSourceUpdateMode.OnPropertyChanged,String.Empty, "MM'/'dd'/'yyyy HH':'mm':'ss zzz"));
                removedByData.DataBindings.Add(new Binding(nameof(removedByData.Text), bindingHistory, nameof(ITemporalValue.RemovedBy)));
                removedOnData.DataBindings.Add(new Binding(nameof(removedOnData.Text), bindingHistory, nameof(ITemporalValue.RemovedOn), true, DataSourceUpdateMode.OnPropertyChanged, String.Empty, "MM'/'dd'/'yyyy HH':'mm':'ss zzz"));

                historyValues.Clear();
                historyValuesData.Items.Clear();
                IReadOnlyList<IDataValue> groups = formData.GetGroups();

                foreach (IDataValue item in groups)
                {
                    TemporalValue lastValue = formData.GetDetails(item).Last();
                    String modification = DbModificationEnumeration.Cast(lastValue.Modification).DisplayName;
                    ListViewItem newItem = new ListViewItem([lastValue.Title, modification]);

                    historyValuesData.Items.Add(newItem);
                    historyValues.Add(newItem, item);
                }

                if (groups.FirstOrDefault() is IDataValue group &&
                    formData.GetDetails(group).LastOrDefault() is TemporalValue value)
                { bindingHistory.Position = formData.IndexOf(value); }

                CommandButtons[CommandImageType.Open].IsEnabled = (OpenForm is not null);
            }
        }

        private void HistoryValuesData_SelectedIndexChanged(object sender, EventArgs e)
        {
            historyModifications.Clear();
            historyModificationData.Items.Clear();

            foreach (ListViewItem listItem in historyValuesData.SelectedItems)
            {
                if (historyValues.TryGetValue(listItem, out IDataValue? item))
                {
                    IReadOnlyList<TemporalValue> temporalValues = formData.GetDetails(item);
                    foreach (TemporalValue temporalItem in temporalValues)
                    {
                        String itemModification = DbModificationEnumeration.Cast(temporalItem.Modification).DisplayName;
                        String itemModifiedOn;
                        if (temporalItem.CreatedOn is DateTime modifiedOnvalue)
                        { itemModifiedOn = modifiedOnvalue.ToString(); }
                        else { itemModifiedOn = String.Empty; }

                        ListViewItem newItem = new ListViewItem([itemModification, itemModifiedOn]);

                        historyModificationData.Items.Add(newItem);
                        historyModifications.Add(newItem, temporalItem);
                    }

                    if (temporalValues.LastOrDefault() is TemporalValue value)
                    { bindingHistory.Position = formData.IndexOf(value); }
                    else { throw new IndexOutOfRangeException("Current Value could not be found"); }
                }
            }

        }

        void HistoryValuesData_Resize(object sender, EventArgs e)
        { historyValuesData.ResizeColumns(); }

        private void HistoryModificationData_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem listItem in historyModificationData.SelectedItems)
            {
                if (historyModifications.TryGetValue(listItem, out TemporalValue? value))
                { bindingHistory.Position = formData.IndexOf(value); }
                else { throw new IndexOutOfRangeException("Current Value could not be found"); }
            }
        }

        void HistoryModificationData_Resize(object sender, EventArgs e)
        { historyModificationData.ResizeColumns(); }

        private void HistoryModificationData_DoubleClick(object sender, EventArgs e)
        { OpenCommand_Click(sender, e); }

        private void HistoryValuesData_DoubleClick(object sender, EventArgs e)
        { BrowseCommand_Click(sender, e); }

        protected override void OpenCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenCommand_Click(sender, e);

            if (bindingHistory.Current is TemporalValue value && OpenForm is not null)
            { Activate(OpenForm(value)); }
        }
    }
}
