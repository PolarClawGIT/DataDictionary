using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NameScope;
using DataDictionary.DataLayer.LibraryData.Source;
using DataDictionary.DataLayer.ModelData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Library
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
        public required ILibraryData Library { get; init; }

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

        public IReadOnlyList<WorkItem> Export(IList<NameScopeItem> target)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem()
            {
                WorkName = "Load NameScope, Library",
                DoWork = () =>
                {
                    foreach (LibrarySourceItem item in this)
                    { target.Add(new NameScopeItem(item)); }
                }
            });

            return work;
        }
    }
}
