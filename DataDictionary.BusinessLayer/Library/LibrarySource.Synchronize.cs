using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.LibraryData;
using System.ComponentModel;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Library
{
    /// <summary>
    /// LibrarySource Synchronize value
    /// </summary>
    public class LibrarySynchronizeValue : SynchronizeValue<LibrarySourceValue>
    {
        /// <inheritdoc cref="LibrarySourceItem.LibraryTitle"/>
        public String LibraryTitle
        {
            get { return Source.LibraryTitle ?? String.Empty; }
            set { Source.LibraryTitle = value; }
        }

        /// <inheritdoc cref="LibrarySourceItem.AssemblyName"/>
        public String AssemblyName
        { get { return Source.AssemblyName ?? String.Empty; } }

        /// <inheritdoc/>
        public LibrarySynchronizeValue(LibrarySourceValue data) : base(data)
        { }

        /// <inheritdoc/>
        protected override void Source_OnPropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(LibraryTitle) or nameof(AssemblyName))
            { OnPropertyChanged(e.PropertyName); }
        }
    }

    /// <summary>
    /// Library Synchronize to compare what Library's are in the Database vs the Model
    /// </summary>
    public class LibrarySynchronize : SynchronizeData<LibrarySynchronizeValue, LibrarySourceValue, LibrarySourceIndex>
    {
        /// <summary>
        /// Concrete class for the Abstract SourceCollection
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        class SourceCollection<TValue> : LibrarySourceCollection<TValue>
            where TValue : LibrarySourceValue, ILibrarySourceValue, new()
        { }

        /// <inheritdoc/>
        protected override IBindingList<LibrarySourceValue> ModelData { get { return libraryModel.LibrarySources; } }
        ILibraryModel libraryModel;

        /// <inheritdoc/>
        protected override IBindingList<LibrarySourceValue> DatabaseData { get { return sourceData; } }
        SourceCollection<LibrarySourceValue> sourceData = new SourceCollection<LibrarySourceValue>();

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="libraryModel"></param>
        public LibrarySynchronize(ILibraryModel libraryModel) : base()
        {
            this.libraryModel = libraryModel;

            foreach (LibrarySourceValue item in libraryModel.LibrarySources)
            { Add(new LibrarySynchronizeValue(item) { InModel = true }); }
        }

        /// <inheritdoc/>
        protected override LibrarySourceIndex GetKey(LibrarySourceValue data)
        { return new LibrarySourceIndex(data); }

        /// <inheritdoc/>
        protected override LibrarySynchronizeValue GetValue(LibrarySourceValue data)
        { return new LibrarySynchronizeValue(data); }

        /// <summary>
        /// Clears then reloads the Libraries List from the Database.
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> GetLibraries(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem() { DoWork = sourceData.Clear });
            work.Add(factory.CreateLoad(sourceData));
            return work;
        }

        /// <summary>
        /// Loads a Library from a Database into the Model
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> ImportFromFile(FileInfo source)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(libraryModel.Import(source));
            return work;
        }

        /// <summary>
        /// Loads a Library from the Database
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> OpenFromDb(IDatabaseWork factory, ILibrarySourceIndex key)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(libraryModel.Delete(key));
            work.AddRange(libraryModel.Load(factory, key));
            return work;
        }

        /// <summary>
        /// Saves the Library to the Database
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> SaveToDb(IDatabaseWork factory, ILibrarySourceIndex key)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(libraryModel.Save(factory, key));
            work.AddRange(GetLibraries(factory));
            return work;
        }

        /// <summary>
        /// Deletes a Library from the Database
        /// Copy in the Model is not removed.
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> DeleteFromDb(IDatabaseWork factory, ILibrarySourceIndex key)
        {
            List<WorkItem> work = new List<WorkItem>();
            ILibrarySourceKey dbKey = new LibrarySourceKey(key);

            work.Add(new WorkItem()
            {
                DoWork = () =>
                {
                    while (sourceData.FirstOrDefault(w => dbKey.Equals(w)) is LibrarySourceValue item)
                    { sourceData.Remove(dbKey); }
                }
            });
            work.Add(factory.CreateSave(sourceData, dbKey));
            return work;
        }



    }
}
