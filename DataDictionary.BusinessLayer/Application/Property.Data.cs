using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ApplicationData.Property;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Application
{
    /// <summary>
    /// Interface component for the Property data
    /// </summary>
    /// <remarks>Used to hide the DataLayer methods from the Application Layer.</remarks>
    public interface IPropertyData:
        IBindingData<PropertyValue>,
        ISaveData, ILoadData
    { }

    /// <summary>
    /// Wrapper Class for Application Properties.
    /// </summary>
    class PropertyData: PropertyCollection<PropertyValue>, IPropertyData
    {
        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public virtual IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public virtual IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return factory.CreateSave(this).ToList(); }
    }
}
