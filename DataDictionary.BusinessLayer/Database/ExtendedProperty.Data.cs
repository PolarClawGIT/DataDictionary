using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.ModelData;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Database
{
    /// <summary>
    /// Interface representing Catalog ExtendedProperty data
    /// </summary>
    public interface IExtendedPropertyData: IBindingData<ExtendedPropertyValue>
    {

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(IExtendedPropertyIndexName source);

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(ITableColumnIndexName source);

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(ITableIndexName source);

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(IRoutineIndexName source);

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(IRoutineParameterIndexName source);

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(IConstraintIndexName source);

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(ISchemaIndexName source);
    }

    class ExtendedPropertyData : DbExtendedPropertyCollection<ExtendedPropertyValue>, IExtendedPropertyData,
        ILoadData<IDbCatalogKey>, ISaveData<IDbCatalogKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDatabaseModelItem
    {
        /// <inheritdoc/>
        public required IDatabaseModel Database { get; init; }

        /// <inheritdoc/>
        /// <remarks>ExtendedProperty</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDbCatalogKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ExtendedProperty</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ExtendedProperty</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDbCatalogKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ExtendedProperty</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(IExtendedPropertyIndexName source)
        {
            DbExtendedPropertyKeyName key = new DbExtendedPropertyKeyName(source);
            return this.Where(w => key.Equals(w));
        }

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(ITableColumnIndexName source)
        {
            DbExtendedPropertyKeyName key = new DbExtendedPropertyKeyName(source);
            return this.Where(w => key.Equals(w));
        }

        /// <inheritdoc/>
        public IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(ITableIndexName source)
        {
            DbExtendedPropertyKeyName key = new DbExtendedPropertyKeyName(source);
            return this.Where(w => key.Equals(w));
        }

        /// <inheritdoc/>
        public IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(IRoutineIndexName source)
        {
            DbExtendedPropertyKeyName key = new DbExtendedPropertyKeyName(source);
            return this.Where(w => key.Equals(w));
        }

        /// <inheritdoc/>
        public IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(IRoutineParameterIndexName source)
        {
            DbExtendedPropertyKeyName key = new DbExtendedPropertyKeyName(source);
            return this.Where(w => key.Equals(w));
        }

        /// <inheritdoc/>
        public IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(IConstraintIndexName source)
        {
            DbExtendedPropertyKeyName key = new DbExtendedPropertyKeyName(source);
            return this.Where(w => key.Equals(w));
        }

        /// <inheritdoc/>
        public IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(ISchemaIndexName source)
        {
            DbExtendedPropertyKeyName key = new DbExtendedPropertyKeyName(source);
            return this.Where(w => key.Equals(w));
        }

    }
}
