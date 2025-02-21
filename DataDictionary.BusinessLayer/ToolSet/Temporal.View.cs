using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.ToolSet
{
    /// <summary>
    /// Interface for the Temporal view
    /// </summary>
    public interface ITemporalView
    {
        /// <summary>
        /// Gets the list of values taking the last item for each Index.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ITemporalValue> Items();

        /// <summary>
        /// Get the list of values for a given index.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        IEnumerable<ITemporalValue> Values(IDataIndex data);

        /// <summary>
        /// Loads the Temporal Data
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Load(IDatabaseWork factory);
    }

    /// <summary>
    /// Wrapper class that returns the BindingViews for the Temporal values
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    class TemporalData<TData, TValue> : ITemporalView
        where TValue : class, IDataValue, IBindingRowState, IBindingPropertyChanged, ITemporalValue
        where TData : class, IBindingData<TValue>, IEnumerable<TValue>, IBindingTable, new()
    {
        TData currentData = new TData();

        //Func<TValue,TIndex> ToIndex { get; init; }

        public required Func<IDatabaseWork, TData, WorkItem> CreateLoad { get; init; }

        public TemporalData() { }

        public IEnumerable<ITemporalValue> Items()
        {
            return currentData.
                OfType<ITemporalValue>().
                OrderBy(o => o.Title).
                GroupBy(g => g.Index).
                Select(s => s.OrderBy(o => o.Temporal.AsOfUtcDate).Last()).
                ToList();
        }

        public IEnumerable<ITemporalValue> Values(IDataIndex data)
        {
            DataIndex key = new DataIndex(data);

            return currentData.
                OfType<ITemporalValue>().
                Where(w => key.Equals(w)).
                OrderBy(o => o.Temporal.AsOfUtcDate).
                ToList();
        }

        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(CreateLoad(factory, currentData));
            return work;
        }
    }



}
