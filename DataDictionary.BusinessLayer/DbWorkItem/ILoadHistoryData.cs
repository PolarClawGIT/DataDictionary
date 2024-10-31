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
    /// Interface for Create WorkItems that load Modification History into a target.
    /// </summary>
    public interface ILoadHistoryData
    {
        /// <summary>
        /// Create WorkItems that load Modification History into a target.
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> LoadHistory(IDatabaseWork factory, List<ITemporalValue> target);

    }

    /// <summary>
    /// Interface for Create WorkItems that load Modification History into a target by Key.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface ILoadHistoryData<TKey>
        where TKey : IKey
    {
        /// <summary>
        /// Create WorkItems that load Modification History into a target by Key.
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="key"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> LoadHistory(IDatabaseWork factory, TKey key, List<ITemporalValue> target);
    }
}
