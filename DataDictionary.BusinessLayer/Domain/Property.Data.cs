using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.DomainData.Property;
using DataDictionary.DataLayer.ModelData;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <summary>
    /// Interface component for the Property data
    /// </summary>
    /// <remarks>Used to hide the DataLayer methods from the Application Layer.</remarks>
    public interface IPropertyData :
        IBindingData<PropertyValue>,
        ILoadData, ILoadData<IPropertyIndex>, ISaveData<IPropertyIndex>
    { }

    /// <inheritdoc/>
    class PropertyData : DomainPropertyCollection<PropertyValue>, IPropertyData,
        ILoadData<IModelKey>, ISaveData<IModelKey>, IDataTableFile
    {
        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public virtual IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IPropertyIndex dataKey)
        { return Load(factory, (IDomainPropertyKey)dataKey); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDomainPropertyKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IPropertyIndex dataKey)
        { return Save(factory, (IDomainPropertyKey)dataKey); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDomainPropertyKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Definition</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public IReadOnlyList<System.Data.DataTable> Export()
        { return this.ToDataTable().ToList(); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public void Import(System.Data.DataSet source)
        { this.Load(source); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Property", DoWork = () => { this.Clear(); } }.ToList(); }
    }
}
