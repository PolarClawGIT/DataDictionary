// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.DataLayer.AppModel;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <summary>
    /// Interface representing Catalog ExtendedProperty data
    /// </summary>
    public interface IPropertyData : IBindingData<PropertyValue>
    {
        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<IPropertyValue> GetProperty(IPropertyIndexObject source);

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<IPropertyValue> GetProperty(ITableColumnIndexName source);

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<IPropertyValue> GetProperty(ITableIndexName source);

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<IPropertyValue> GetProperty(IRoutineIndexName source);

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<IPropertyValue> GetProperty(IRoutineParameterIndexName source);

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<IPropertyValue> GetProperty(IConstraintIndexName source);

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<IPropertyValue> GetProperty(ISchemaIndexName source);
    }

    class PropertyData : DataLayer.AppCatalog.PropertyCollection<PropertyValue>, IPropertyData,
        ILoadData<ICatalogIndex>, ISaveData<ICatalogIndex>,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>,
        ICatalogModel
    {
        /// <inheritdoc/>
        public required ICatalog Model { get; init; }

        /// <inheritdoc/>
        /// <remarks>ExtendedProperty</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ICatalogIndex dataKey)
        { return factory.CreateLoad(this, (ICatalogKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ExtendedProperty</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ICatalogIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (ICatalogKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ExtendedProperty</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateLoad(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ExtendedProperty</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ExtendedProperty</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ICatalogIndex dataKey)
        { return factory.CreateSave(this, (ICatalogKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ExtendedProperty</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateSave(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ExtendedProperty</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>ExtendedProperty</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove ExtendedProperty", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>ExtendedProperty</remarks>
        public IReadOnlyList<WorkItem> Delete(ICatalogIndex dataKey)
        { return new WorkItem() { WorkName = "Remove ExtendedProperty", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>ExtendedProperty</remarks>
        public IEnumerable<IPropertyValue> GetProperty(IPropertyIndexObject source)
        {
            PropertyKeyObject key = new PropertyKeyObject(source);
            return this.Where(w => key.Equals(w));
        }

        /// <inheritdoc/>
        /// <remarks>ExtendedProperty</remarks>
        public IEnumerable<IPropertyValue> GetProperty(ITableColumnIndexName source)
        {
            PropertyKeyObject key = new PropertyKeyObject(source);
            return this.Where(w => key.Equals(w));
        }

        /// <inheritdoc/>
        /// <remarks>ExtendedProperty</remarks>
        public IEnumerable<IPropertyValue> GetProperty(ITableIndexName source)
        {
            PropertyKeyObject key = new PropertyKeyObject(source);
            return this.Where(w => key.Equals(w));
        }

        /// <inheritdoc/>
        public IEnumerable<IPropertyValue> GetProperty(IRoutineIndexName source)
        {
            PropertyKeyObject key = new PropertyKeyObject(source);
            return this.Where(w => key.Equals(w));
        }

        /// <inheritdoc/>
        /// <remarks>ExtendedProperty</remarks>
        public IEnumerable<IPropertyValue> GetProperty(IRoutineParameterIndexName source)
        {
            PropertyKeyObject key = new PropertyKeyObject(source);
            return this.Where(w => key.Equals(w));
        }

        /// <inheritdoc/>
        /// <remarks>ExtendedProperty</remarks>
        public IEnumerable<IPropertyValue> GetProperty(IConstraintIndexName source)
        {
            PropertyKeyObject key = new PropertyKeyObject(source);
            return this.Where(w => key.Equals(w));
        }

        /// <inheritdoc/>
        /// <remarks>ExtendedProperty</remarks>
        public IEnumerable<IPropertyValue> GetProperty(ISchemaIndexName source)
        {
            PropertyKeyObject key = new PropertyKeyObject(source);
            return this.Where(w => key.Equals(w));
        }


    }
}
