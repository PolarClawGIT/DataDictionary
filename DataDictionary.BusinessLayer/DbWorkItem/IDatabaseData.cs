using DataDictionary.DataLayer;
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
    public interface ILoadData
    {
        /// <summary>
        /// Create WorkItems that loads data from the Database
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Load(IDatabaseWork factory);

        /// <summary>
        /// Create WorkItems that loads data from a DataSet (normally a file)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Load(System.Data.DataSet source);
    }

    /// <summary>
    /// Interface for Create WorkItems that loads data from the Database by Key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface ILoadData<TKey>
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
    public interface ISaveData: ILoadData
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
    public interface ISaveData<TKey>: ISaveData, ILoadData<TKey>
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
    /// <typeparam name="TKey"></typeparam>
    public interface IRemoveData<TKey>
        where TKey : IKey
    {
        /// <summary>
        /// Create WorkItems that removes items from the collection by Key
        /// </summary>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Remove(TKey dataKey);
    }

    /// <summary>
    /// Interface for Create WorkItems Load/Save 
    /// </summary>
    public interface IDatabaseData: ILoadData, ISaveData
    { }

    /// <summary>
    /// Interface for Create WorkItems Load/Save by Key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IDatabaseData<TKey> : IDatabaseData, ILoadData<TKey>, ISaveData<TKey>, IRemoveData<TKey>
    where TKey : IKey
    { }
}
