using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.ModelData;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.CatalogData
{
    /// <summary>
    /// Interface representing Catalog ExtendedProperty data
    /// </summary>
    public interface IExtendedPropertyData : IBindingData<DbExtendedPropertyItem>
    {

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(IDbExtendedPropertyKeyName source);

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(IDbTableColumnKeyName source);

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(IDbTableKeyName source);

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(IDbRoutineParameterKeyName source);

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(IDbConstraintKeyName source);

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(IDbSchemaKeyName source);

    }

    class ExtendedPropertyData : DbExtendedPropertyCollection, IExtendedPropertyData,
        ILoadData<IDbCatalogKey>, ISaveData<IDbCatalogKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>
    {
        internal required ICatalogData Catalog { get; init; }

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
        public IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(IDbExtendedPropertyKeyName source)
        {
            DbExtendedPropertyKeyName key = new DbExtendedPropertyKeyName(source);
            return this.Where(w => key.Equals(w));
        }

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(IDbTableColumnKeyName source)
        {
            DbExtendedPropertyKeyName key = new DbExtendedPropertyKeyName(source);
            return this.Where(w => key.Equals(w));
        }

        /// <inheritdoc/>
        public IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(IDbTableKeyName source)
        {
            DbExtendedPropertyKeyName key = new DbExtendedPropertyKeyName(source);
            return this.Where(w => key.Equals(w));
        }

        /// <inheritdoc/>
        public IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(IDbRoutineParameterKeyName source)
        {
            DbExtendedPropertyKeyName key = new DbExtendedPropertyKeyName(source);
            return this.Where(w => key.Equals(w));
        }

        /// <inheritdoc/>
        public IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(IDbConstraintKeyName source)
        {
            DbExtendedPropertyKeyName key = new DbExtendedPropertyKeyName(source);
            return this.Where(w => key.Equals(w));
        }

        /// <inheritdoc/>
        public IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(IDbSchemaKeyName source)
        {
            DbExtendedPropertyKeyName key = new DbExtendedPropertyKeyName(source);
            return this.Where(w => key.Equals(w));
        }

    }
}
