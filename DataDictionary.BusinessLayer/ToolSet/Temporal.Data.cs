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
    //TODO: POC code

    public interface ITemporalData
    {

        IEnumerable<ITemporalValue> Items();
        IEnumerable<ITemporalValue> Values(IDataIndex data);

    }

    public class TemporalData<TData, TValue> : ITemporalData
        where TValue : class, IDataValue, IBindingRowState, IBindingPropertyChanged, ITemporalValue
        where TData : class, IBindingData<TValue>, IEnumerable<TValue>, IBindingTable, new ()
    {
        TData currentData = new TData();

        //Func<TValue,TIndex> ToIndex { get; init; }

        public Func<IDatabaseWork, TData, WorkItem> CreateLoad { get; init; }

        public TemporalData()
        { }

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

        public virtual IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(CreateLoad(factory, currentData));
            return work;
        }
    }



}
