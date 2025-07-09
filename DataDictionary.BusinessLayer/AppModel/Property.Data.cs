// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// Interface for retrieving property values.
    /// </summary>
    public interface IPropertyGetValue
    {
        /// <summary>
        /// Attempts to retrieve a Property value based on the specified property index.
        /// </summary>
        /// <param name="propertyIndex">The index of the property to retrieve.</param>
        /// <param name="propertyValue">The retrieved property value, or null if not found.</param>
        /// <returns>True if the property value was found; otherwise, false.</returns>
        Boolean TryGetValue(IPropertyIndex propertyIndex, [NotNullWhen(true)] out IPropertyValue? propertyValue);

        /// <summary>
        /// Attempts to retrieve a Property value based on the specified property index.
        /// </summary>
        /// <param name="propertyIndex"></param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException">If the index cannot be found.</exception>
        IPropertyValue GetValue(IPropertyIndex propertyIndex);

        /// <summary>
        /// Attempts to retrieve a Property value based on the specified Catalog Property.
        /// </summary>
        /// <param name="catalogProperty"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        Boolean TryGetValue(AppCatalog.IPropertyValue catalogProperty, [NotNullWhen(true)] out IPropertyValue? propertyValue);

        /// <summary>
        /// Attempts to retrieve a Property value based on the specified Catalog Property.
        /// </summary>
        /// <param name="catalogProperty"></param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException">If the index cannot be found.</exception>
        IPropertyValue GetValue(AppCatalog.IPropertyValue catalogProperty);
    }

    /// <summary>
    /// Interface component for the Property data
    /// </summary>
    /// <remarks>Used to hide the DataLayer methods from the Application Layer.</remarks>
    public interface IPropertyData :
        IBindingData<PropertyValue>,
        ILoadData, ILoadData<IPropertyIndex>, ISaveData<IPropertyIndex>,
        IPropertyGetValue
    { }

    /// <inheritdoc/>
    class PropertyData : PropertyCollection<PropertyValue>, IPropertyData,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>, IDataTableFile
    {
        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public virtual IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateLoad(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IPropertyIndex dataKey)
        { return factory.CreateLoad(this, (IPropertyKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IPropertyIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IPropertyKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateSave(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IPropertyIndex dataKey)
        { return factory.CreateSave(this, (IPropertyKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public IReadOnlyList<System.Data.DataTable> Export()
        { return this.ToDataTable().ToList(); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public void Import(System.Data.DataSet source)
        { Load(source); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Property", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Property", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public IReadOnlyList<WorkItem> Delete(IPropertyIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Property", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public void Remove(IPropertyIndex dataKey)
        { base.Remove(dataKey); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public void Remove(IModelIndex dataKey)
        { Clear(); }

        /// <inheritdoc/>
        public Boolean TryGetValue(IPropertyIndex propertyIndex, [NotNullWhen(true)] out IPropertyValue? propertyValue)
        {
            PropertyIndex key = new PropertyIndex(propertyIndex);

            if (this.FirstOrDefault(w => key.Equals(w)) is IPropertyValue value)
            { propertyValue = value; return true; }
            else { propertyValue = null; return false; }
        }

        /// <inheritdoc/>
        public IPropertyValue GetValue(IPropertyIndex propertyIndex)
        {
            if (TryGetValue(propertyIndex, out IPropertyValue? propertyValue))
            { return propertyValue; }
            else
            {
                Exception ex = new IndexOutOfRangeException();
                ex.Data.Add(nameof(propertyIndex), propertyIndex);
                throw ex;
            }
        }

        /// <inheritdoc/>
        public Boolean TryGetValue(AppCatalog.IPropertyValue catalogProperty, [NotNullWhen(true)] out IPropertyValue? propertyValue)
        {
            if (this.FirstOrDefault(w =>
                w.PropertyType is DomainPropertyType.MS_ExtendedProperty
                && w.ExtendedPropertyName.Equals(catalogProperty.PropertyName, KeyExtension.CompareString)) is IPropertyValue result)
            { propertyValue = result; return true; }
            else { propertyValue = null; return false; }
        }


        /// <inheritdoc/>
        public IPropertyValue GetValue(AppCatalog.IPropertyValue catalogProperty)
        {
            if (TryGetValue(catalogProperty, out IPropertyValue? propertyValue))
            { return propertyValue; }
            else
            {
                Exception ex = new IndexOutOfRangeException();
                ex.Data.Add(nameof(catalogProperty), catalogProperty);
                throw ex;
            }
        }

        
    }
}
