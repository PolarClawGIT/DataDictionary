// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.DataLayer.AppModel;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppCatalog;

/// <summary>
/// Interface representing Catalog Reference data
/// </summary>
public interface IReferenceData : IBindingData<ReferenceValue>
{ }

class ReferenceData : ReferenceCollection<ReferenceValue>,
        ILoadData<ICatalogIndex>, ISaveData<ICatalogIndex>,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>,
        ICatalogModel, IReferenceData
{
    /// <inheritdoc/>
    public required ICatalog Model { get; init; }

    /// <inheritdoc/>
    /// <remarks>Reference</remarks>
    public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ICatalogIndex dataKey)
    { return factory.CreateLoad(this, (ICatalogKey)dataKey).ToList(); }

    /// <inheritdoc/>
    /// <remarks>Reference</remarks>
    public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ICatalogIndex dataKey, ITemporalIndex asOfUtcDate)
    { return factory.CreateLoad(this, (ICatalogKey)dataKey, asOfUtcDate).ToList(); }

    /// <inheritdoc/>
    /// <remarks>Reference</remarks>
    public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
    { return factory.CreateLoad(this, (IModelKey)dataKey).ToList(); }

    /// <inheritdoc/>
    /// <remarks>Reference</remarks>
    public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
    { return factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate).ToList(); }

    /// <inheritdoc/>
    /// <remarks>Reference</remarks>
    public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ICatalogIndex dataKey)
    { return factory.CreateSave(this, (ICatalogKey)dataKey).ToList(); }

    /// <inheritdoc/>
    /// <remarks>Reference</remarks>
    public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
    { return factory.CreateSave(this).ToList(); }

    /// <inheritdoc/>
    /// <remarks>Reference</remarks>
    public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
    { return Delete(); }

    /// <inheritdoc/>
    /// <remarks>Reference</remarks>
    public IReadOnlyList<WorkItem> Delete()
    { return new WorkItem() { WorkName = "Remove Reference", DoWork = () => { Clear(); } }.ToList(); }

    /// <inheritdoc/>
    /// <remarks>Reference</remarks>
    public IReadOnlyList<WorkItem> Delete(ICatalogIndex dataKey)
    { return new WorkItem() { WorkName = "Remove Reference", DoWork = () => { Remove(dataKey); } }.ToList(); }

    /// <inheritdoc/>
    /// <remarks>Reference</remarks>
    public void Remove(ICatalogIndex dataKey)
    { base.Remove(dataKey); }

    /// <inheritdoc/>
    /// <remarks>Reference</remarks>
    public void Remove(IModelIndex dataKey)
    { Clear(); }
}
