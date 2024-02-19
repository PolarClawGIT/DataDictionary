using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.LibraryData.Source;
using DataDictionary.DataLayer.ModelData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.LibraryData
{
    /// <summary>
    /// Interface representing Library data
    /// </summary>
    public interface ILibrarySourceData : IBindingData<LibrarySourceItem>
    { }

    /// <summary>
    /// Implementation for Library data
    /// </summary>
    public class LibrarySourceData: LibrarySourceCollection, ILibrarySourceData,
        ILoadData<ILibrarySourceKey>, ISaveData<ILibrarySourceKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>
    {
        /// <inheritdoc/>
        /// <remarks>Table</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ILibrarySourceKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Table</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Table</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ILibrarySourceKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Table</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }
    }
}
