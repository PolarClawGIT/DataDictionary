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
    public interface ILibrarySourceData :
        IBindingData<LibrarySourceItem>,
        ILoadData<ILibrarySourceKey>, ILoadData<IModelKey>,
        ISaveData<ILibrarySourceKey>, ISaveData<IModelKey>
    {
        /// <summary>
        /// List of Domain Properties for the Entities within the Model.
        /// </summary>
        ILibraryMemberData LibraryMembers { get; }
    }

    /// <summary>
    /// Implementation for Library data
    /// </summary>
    public class LibrarySourceData: LibrarySourceCollection, ILibrarySourceData
    {
        /// <inheritdoc/>
        public ILibraryMemberData LibraryMembers { get { return members; } }
        private readonly LibraryMemberData members;

        /// <summary>
        /// Constructor for LibrarySourceData
        /// </summary>
        public LibrarySourceData() : base ()
        {
            members = new LibraryMemberData();
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ILibrarySourceKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateLoad(this, dataKey));
            work.AddRange(members.Load(factory, dataKey));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateLoad(this, dataKey));
            work.AddRange(members.Load(factory, dataKey));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ILibrarySourceKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateSave(this, dataKey));
            work.AddRange(members.Save(factory, dataKey));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(factory.CreateSave(this, dataKey).ToList());
            work.AddRange(members.Save(factory, dataKey));

            return work;
        }

        /// <inheritdoc />
        public override void Remove(ILibrarySourceKey sourceItem)
        {
            base.Remove(sourceItem);
            members.Remove(sourceItem);
        }
    }
}
