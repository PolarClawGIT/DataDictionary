using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.ModelData;
using Toolbox.Threading;
using System.ComponentModel;

namespace DataDictionary.BusinessLayer.Database
{
    /// <summary>
    /// Interface representing Catalog Schema data
    /// </summary>
    public interface ISchemaData : IBindingData<SchemaValue>
    { }

    class SchemaData : DbSchemaCollection<SchemaValue>,
        ILoadData<IDbCatalogKey>, ISaveData<IDbCatalogKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDatabaseModelItem, ISchemaData,
        INamedScopeSource
    {
        /// <inheritdoc/>
        public required IDatabaseModel Database { get; init; }

        /// <inheritdoc/>
        /// <remarks>Schema</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDbCatalogKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Schema</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Schema</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDbCatalogKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Schema</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Schema</remarks>
        public IEnumerable<NamedScopePair> GetNamedScopes()
        {
            List<NamedScopePair> result = new List<NamedScopePair>();
            foreach (SchemaValue item in this.Where(w => w.IsSystem == false))
            {
                DbCatalogKeyName nameKey = new DbCatalogKeyName(item);
                if (Database.DbCatalogs.FirstOrDefault(w => nameKey.Equals(w)) is CatalogValue parent)
                { result.Add(new NamedScopePair(parent.GetIndex(), GetValue(item))); }
            }

            return result;

            NamedScopeValueCore GetValue(SchemaValue source)
            {
                NamedScopeValueCore result = new NamedScopeValueCore(source);
                source.PropertyChanged += Source_PropertyChanged;

                return result;

                void Source_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
                {
                    if (e.PropertyName is 
                        nameof(source.DatabaseName) or
                        nameof(source.SchemaName))
                    { result.TitleChanged(); }
                }
            }
        }
    }
}
