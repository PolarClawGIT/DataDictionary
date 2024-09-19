using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.DbWorkItem
{
    /// <summary>
    /// Interface for Create WorkItems that loads data from the Database
    /// </summary>
    public interface ILoadData : IDeleteData
    {
        /// <summary>
        /// Create WorkItems that loads data from the Database
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Load(IDatabaseWork factory);
    }

    /// <summary>
    /// Interface for Create WorkItems that loads data from the Database by Key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface ILoadData<TKey> : IDeleteData<TKey>
        where TKey : IKey
    {
        /// <summary>
        /// Create WorkItems that loads data from the Database by Key
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Load(IDatabaseWork factory, TKey dataKey);
    }

    /// <summary>
    /// Interface for Create workItems that Save Data to the Database
    /// </summary>
    public interface ISaveData : ILoadData
    {
        /// <summary>
        /// Create workItems that Save Data to the Database
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Save(IDatabaseWork factory);
    }

    /// <summary>
    /// Interface for Create WorkItems that Save Data to the Database by Key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface ISaveData<TKey> : ILoadData<TKey>
    where TKey : IKey
    {
        /// <summary>
        /// Create WorkItems that Save Data to the Database by Key
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Save(IDatabaseWork factory, TKey dataKey);
    }

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
        public IReadOnlyList<WorkItem> LoadHistory(IDatabaseWork factory, List<IModificationValue> target);

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
        IReadOnlyList<WorkItem> LoadHistory(IDatabaseWork factory, TKey key, List<IModificationValue> target);
    }
}
