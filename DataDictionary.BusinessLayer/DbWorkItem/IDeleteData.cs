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
    /// Interface for Create WorkItems that removes items from the collection
    /// </summary>
    public interface IDeleteData
    {
        /// <summary>
        /// Create WorkItems that removes all items from the collection
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Delete();

        /// <summary>
        /// Removes all items in the collection.
        /// </summary>
        void Clear();
    }

    /// <summary>
    /// Interface for Create WorkItems that removes items from the collection by Key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IDeleteData<TKey> : IDeleteData
        where TKey : IKey
    {
        /// <summary>
        /// Create WorkItems that delete items from the collection by Key
        /// </summary>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Delete(TKey dataKey);

        /// <summary>
        /// Removes items from the collection by Key
        /// </summary>
        /// <param name="dataKey"></param>
        void Remove(TKey dataKey);
    }
}
