using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.DbWorkItem;
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
        ILoadData<ICatalogKey>, ISaveData<ICatalogKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDatabaseModelItem, IReferenceData
{
    /// <inheritdoc/>
    public required IDatabaseModel Database { get; init; }

    /// <inheritdoc/>
    /// <remarks>Reference</remarks>
    public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ICatalogKey dataKey)
    { return factory.CreateLoad(this, dataKey).ToList(); }

    /// <inheritdoc/>
    /// <remarks>Reference</remarks>
    public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
    { return factory.CreateLoad(this, dataKey).ToList(); }

    /// <inheritdoc/>
    /// <remarks>Reference</remarks>
    public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ICatalogKey dataKey)
    { return factory.CreateSave(this, dataKey).ToList(); }

    /// <inheritdoc/>
    /// <remarks>Reference</remarks>
    public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
    { return factory.CreateSave(this).ToList(); }

    /// <inheritdoc/>
    /// <remarks>Reference</remarks>
    public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
    { return Delete(); }

    /// <inheritdoc/>
    /// <remarks>Reference</remarks>
    public IReadOnlyList<WorkItem> Delete()
    { return new WorkItem() { WorkName = "Remove Reference", DoWork = () => { Clear(); } }.ToList(); }

    /// <inheritdoc/>
    /// <remarks>Reference</remarks>
    public IReadOnlyList<WorkItem> Delete(ICatalogKey dataKey)
    { return new WorkItem() { WorkName = "Remove Reference", DoWork = () => { Remove(dataKey); } }.ToList(); }

}
