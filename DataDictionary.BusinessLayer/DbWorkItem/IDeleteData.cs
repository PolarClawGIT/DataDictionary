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
    /// Interface for Create WorkItems that removes items from the collection by Key
    /// </summary>
    public interface IDeleteData
    {
        /// <summary>
        /// Create WorkItems that removes/delete all items from the collection
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Delete();
    }

    /// <summary>
    /// Interface for Create WorkItems that removes items from the collection by Key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IDeleteData<TKey> : IDeleteData
        where TKey : IKey
    {
        /// <summary>
        /// Create WorkItems that removes/delete items from the collection by Key
        /// </summary>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Delete(TKey dataKey);
    }
}
