using DataDictionary.BusinessLayer.DbWorkItem;
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
        ILoadData<ICatalogKey>, ISaveData<ICatalogKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        ICatalogModel
    {
        /// <inheritdoc/>
        public required ICatalog Model { get; init; }

        /// <inheritdoc/>
        /// <remarks>ExtendedProperty</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ICatalogKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ExtendedProperty</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ExtendedProperty</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ICatalogKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ExtendedProperty</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ExtendedProperty</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>ExtendedProperty</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove ExtendedProperty", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>ExtendedProperty</remarks>
        public IReadOnlyList<WorkItem> Delete(ICatalogKey dataKey)
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
