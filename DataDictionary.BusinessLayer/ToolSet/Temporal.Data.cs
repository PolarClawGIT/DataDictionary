using DataDictionary.BusinessLayer.DbWorkItem;
using System.ComponentModel;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.ToolSet
{
    /// <summary>
    /// Values of TemporalValue built from an ITemporalValue
    /// </summary>
    public interface ITemporalData : IBindingList<TemporalValue>
    {
        /// <summary>
        /// For a Given group, return the values.
        /// </summary>
        /// <param name="groupValue"></param>
        /// <returns></returns>
        IReadOnlyList<TemporalValue> GetDetails(IDataValue groupValue);

        /// <summary>
        /// Returns the Last value for each Index.
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<IDataValue> GetGroups();

        /// <summary>
        /// Load the TemporalData from the Database
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Load(IDatabaseWork factory);
    }

    /// <summary>
    /// Values of TemporalValue built from an ITemporalValue
    /// </summary>
    class TemporalData<TData, TValue> : BindingList<TemporalValue>, ITemporalData
        where TValue : class, IDataValue, IBindingRowState, IBindingPropertyChanged, ITemporal
        where TData : class, IBindingData<TValue>, IEnumerable<TValue>, IBindingTable, new()

    {
        TData data = new TData();

        public Func<TValue, DataIndex> ToDataIndex { get; init; } = (value) => ((IDataValue)value).Index;

        public Func<TValue, TemporalIndex> ToTemporalIndex { get; init; } = (value) => new TemporalIndex(value);

        /// <summary>
        /// Function that returns the HistoryCommand WorkItem for the base.
        /// </summary>
        public required Func<IDatabaseWork, TData, WorkItem> CreateLoad { get; init; }

        /// <summary>
        /// Constructor
        /// </summary>
        public TemporalData()
        { }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem() { DoWork = () => { data.Clear(); } });
            work.Add(CreateLoad(factory, data));
            work.Add(new WorkItem()
            {
                DoWork = () =>
                {
                    this.Clear();
                    this.RaiseListChangedEvents = false;
                    this.AddRange(data.
                        OfType<ITemporal>().
                        Select(s => new TemporalValue(s)));
                    this.RaiseListChangedEvents = true;
                    this.ResetBindings();
                    this.AllowEdit = false;
                    this.AllowNew = false;
                    this.AllowRemove = false;
                }
            });
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<IDataValue> GetGroups()
        {
            return this.
                OrderBy(o => o.Title).
                GroupBy(g => g.Index).
                Select(s => s.OrderBy(o => o.AsOfUtcDate).
                    OfType<IDataValue>().
                    Last()).
                ToList();
        }

        /// <inheritdoc/>
        public IReadOnlyList<TemporalValue> GetDetails(IDataValue groupValue)
        {
            return this.
                Where(w => groupValue.Index.Equals(w.Index)).
                OrderBy(o => o.AsOfUtcDate).
                ToList();
        }


    }
}
