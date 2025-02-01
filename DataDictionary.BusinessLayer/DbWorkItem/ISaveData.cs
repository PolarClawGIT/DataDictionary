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
    /// Interface for Create workItems that Save Data to the Model
    /// </summary>
    public interface ISaveData : ILoadData
    {
        /// <summary>
        /// Create workItems that Save Data to the Model
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Save(IDatabaseWork factory);
    }

    /// <summary>
    /// Interface for Create WorkItems that Save Data to the Model by Key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface ISaveData<TKey> : ILoadData<TKey>
    where TKey : IKey
    {
        /// <summary>
        /// Create WorkItems that Save Data to the Model by Key
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Save(IDatabaseWork factory, TKey dataKey);
    }

}
