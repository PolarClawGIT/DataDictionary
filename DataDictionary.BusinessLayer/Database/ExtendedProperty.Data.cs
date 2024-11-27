using DataDictionary.BusinessLayer.AppCatalog;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.AppCatalog;
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
        IEnumerable<PropertyItem> GetExtendedProperty(IExtendedPropertyIndexName source);

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<PropertyItem> GetExtendedProperty(ITableColumnIndexName source);

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<PropertyItem> GetExtendedProperty(ITableIndexName source);

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<PropertyItem> GetExtendedProperty(IRoutineIndexName source);

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<PropertyItem> GetExtendedProperty(IRoutineParameterIndexName source);

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<PropertyItem> GetExtendedProperty(IConstraintIndexName source);

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<PropertyItem> GetExtendedProperty(ISchemaIndexName source);
    }

    class ExtendedPropertyData : PropertyCollection<ExtendedPropertyValue>, IExtendedPropertyData,
        ILoadData<ICatalogKey>, ISaveData<ICatalogKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDatabaseModelItem
    {
        /// <inheritdoc/>
        public required IDatabaseModel Database { get; init; }

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
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ExtendedProperty</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>ExtendedProperty</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove ExtendedProperty", DoWork = () => { this.Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>DoExtendedPropertymain</remarks>
        public IReadOnlyList<WorkItem> Delete(ICatalogKey dataKey)
        { return new WorkItem() { WorkName = "Remove ExtendedProperty", DoWork = () => { this.Remove(dataKey); } }.ToList(); }

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IEnumerable<PropertyItem> GetExtendedProperty(IExtendedPropertyIndexName source)
        {
            PropertyKeyObject key = new PropertyKeyObject(source);
            return this.Where(w => key.Equals(w));
        }

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IEnumerable<PropertyItem> GetExtendedProperty(ITableColumnIndexName source)
        {
            PropertyKeyObject key = new PropertyKeyObject(source);
            return this.Where(w => key.Equals(w));
        }

        /// <inheritdoc/>
        public IEnumerable<PropertyItem> GetExtendedProperty(ITableIndexName source)
        {
            PropertyKeyObject key = new PropertyKeyObject(source);
            return this.Where(w => key.Equals(w));
        }

        /// <inheritdoc/>
        public IEnumerable<PropertyItem> GetExtendedProperty(IRoutineIndexName source)
        {
            PropertyKeyObject key = new PropertyKeyObject(source);
            return this.Where(w => key.Equals(w));
        }

        /// <inheritdoc/>
        public IEnumerable<PropertyItem> GetExtendedProperty(IRoutineParameterIndexName source)
        {
            PropertyKeyObject key = new PropertyKeyObject(source);
            return this.Where(w => key.Equals(w));
        }

        /// <inheritdoc/>
        public IEnumerable<PropertyItem> GetExtendedProperty(IConstraintIndexName source)
        {
            PropertyKeyObject key = new PropertyKeyObject(source);
            return this.Where(w => key.Equals(w));
        }

        /// <inheritdoc/>
        public IEnumerable<PropertyItem> GetExtendedProperty(ISchemaIndexName source)
        {
            PropertyKeyObject key = new PropertyKeyObject(source);
            return this.Where(w => key.Equals(w));
        }


    }
}
