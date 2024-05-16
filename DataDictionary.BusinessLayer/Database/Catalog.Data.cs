using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.ModelData;
using System.ComponentModel;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Database
{
    /// <summary>
    /// Wrapper of Catalog Data (The Database)
    /// </summary>
    public interface ICatalogData : IBindingData<CatalogValue>
    { }

    class CatalogData : DbCatalogCollection<CatalogValue>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        ILoadData<IDbCatalogKey>, ISaveData<IDbCatalogKey>,
        IDatabaseModelItem, ICatalogData,
        INamedScopeSource
    {
        /// <inheritdoc/>
        public required IDatabaseModel Database { get; init; }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDbCatalogKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDbCatalogKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IEnumerable<NamedScopePair> GetNamedScopes()
        {
            return this.Where(w => w.IsSystem == false).Select(s => new NamedScopePair(GetValue(s)));

            NamedScopeValueCore GetValue(CatalogValue source)
            {
                NamedScopeValueCore result = new NamedScopeValueCore(source);
                source.PropertyChanged += Source_PropertyChanged;

                return result;

                void Source_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
                {
                    if (e.PropertyName is
                        nameof(source.CatalogTitle) or
                        nameof(source.DatabaseName))
                    { result.TitleChanged(); }
                }
            }
        }
    }
}
