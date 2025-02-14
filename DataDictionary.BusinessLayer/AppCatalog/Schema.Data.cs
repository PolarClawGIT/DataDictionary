// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.DbWorkItem;
using Toolbox.Threading;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.ToolSet;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <summary>
    /// Interface representing Catalog Schema data
    /// </summary>
    public interface ISchemaData : IBindingData<SchemaValue>
    { }

    class SchemaData : SchemaCollection<SchemaValue>,
        ILoadData<ICatalogIndex>, ISaveData<ICatalogIndex>,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>,
        ICatalogModel, ISchemaData
    {
        /// <inheritdoc/>
        public required ICatalog Model { get; init; }

        /// <inheritdoc/>
        /// <remarks>Schema</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ICatalogIndex dataKey)
        { return factory.CreateLoad(this, (ICatalogKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Schema</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ICatalogIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (ICatalogKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Schema</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateLoad(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Schema</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Schema</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ICatalogIndex dataKey)
        { return factory.CreateSave(this, (ICatalogKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Schema</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateSave(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Schema</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>Schema</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Schema", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Schema</remarks>
        public IReadOnlyList<WorkItem> Delete(ICatalogIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Schema", DoWork = () => { Remove(dataKey); } }.ToList(); }

    }
}
