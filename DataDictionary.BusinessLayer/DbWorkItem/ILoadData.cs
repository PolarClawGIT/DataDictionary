using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.DbWorkItem
{
    /// <summary>
    /// Interface for Create WorkItems that loads data from the Model
    /// </summary>
    public interface ILoadData : IDeleteData
    {
        /// <summary>
        /// Create WorkItems that loads data from the Model
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Load(IDatabaseWork factory);
    }

    /// <summary>
    /// Interface for Create WorkItems that loads data from the Model by Key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface ILoadData<TKey> : IDeleteData<TKey>
        where TKey : IKey
    {
        /// <summary>
        /// Create WorkItems that loads data from the Model by Key
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Load(IDatabaseWork factory, TKey dataKey);

        /// <summary>
        /// Create WorkItems that loads data from the Model by Key and date
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="dataKey"></param>
        /// <param name="asOfUtcDate"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Load(IDatabaseWork factory, TKey dataKey, ITemporalIndex asOfUtcDate);
    }
}
