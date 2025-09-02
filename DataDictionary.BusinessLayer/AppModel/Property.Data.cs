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
    /// Interface component for the Property data
    /// </summary>
    /// <remarks>Used to hide the DataLayer methods from the Application Layer.</remarks>
    public interface IPropertyData :
        IBindingData<PropertyValue>,
        ILoadData, ILoadData<IPropertyIndex>, ISaveData<IPropertyIndex>,
        ITryGetValue<IPropertyIndex, IPropertyValue>,
        ITryGetValue<AppCatalog.IPropertyValue, IPropertyValue>
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
        public Boolean TryGetValue(AppCatalog.IPropertyValue catalogProperty, [NotNullWhen(true)] out IPropertyValue? propertyValue)
        {
            if (this.FirstOrDefault(w =>
                w.PropertyType is DomainPropertyType.MS_ExtendedProperty
                && w.ExtendedPropertyName.Equals(catalogProperty.PropertyName, KeyExtension.CompareString)) is IPropertyValue result)
            { propertyValue = result; return true; }
            else { propertyValue = null; return false; }
        }
    }
}
