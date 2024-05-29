using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ModelData;
using DbLayer = DataDictionary.DataLayer.LibraryData;
using Toolbox.Threading;
using System.ComponentModel;

namespace DataDictionary.BusinessLayer.Library
{
    /// <summary>
    /// Interface representing Library data
    /// </summary>
    public interface ILibrarySourceData: IBindingData<LibrarySourceValue>
    { }

    class LibrarySourceData: DbLayer.Source.LibrarySourceCollection<LibrarySourceValue>, ILibrarySourceData,
        ILoadData<DbLayer.Source.ILibrarySourceKey>, ISaveData<DbLayer.Source.ILibrarySourceKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        INamedScopeSource
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
        {
            return this.Select(s => new NamedScopePair(GetValue(s)));

            NamedScopeValueCore GetValue(LibrarySourceValue source)
            {
                NamedScopeValueCore result = new NamedScopeValueCore(source);
                source.PropertyChanged += Source_PropertyChanged;

                return result;

                void Source_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
                {
                    if (e.PropertyName is
                        nameof(source.LibraryTitle) or
                        nameof(source.AssemblyName))
                    { result.TitleChanged(); }
                }
            }
        }
    }
}
