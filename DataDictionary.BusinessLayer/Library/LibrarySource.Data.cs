using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.LibraryData.Source;
using DataDictionary.DataLayer.ModelData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbLayer = DataDictionary.DataLayer.LibraryData;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Library
{
    /// <summary>
    /// Interface representing Library data
    /// </summary>
    public interface ILibrarySourceData<TValue> : IBindingData<TValue>
    where TValue : LibrarySourceValue
    { }

    class LibrarySourceData<TValue> : DbLayer.Source.LibrarySourceCollection<TValue>, ILibrarySourceData<TValue>,
        ILoadData<DbLayer.Source.ILibrarySourceKey>, ISaveData<DbLayer.Source.ILibrarySourceKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IGetNamedScopes
        where TValue : LibrarySourceValue, new()
    {
        /// <inheritdoc/>
        public required ILibraryModel Library { get; init; }

        /// <inheritdoc/>
        /// <remarks>Library Source</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, DbLayer.Source.ILibrarySourceKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Library Source</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Library Source</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, DbLayer.Source.ILibrarySourceKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Library Source</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Library Source</remarks>
        public IEnumerable<NamedScopePair> GetNamedScopes()
        { return this.Select(s => new NamedScopePair(s)); }
    }
}
