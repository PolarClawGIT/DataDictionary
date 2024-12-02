using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.DataLayer.DatabaseData.Reference;
using DataDictionary.DataLayer.ModelData;
using System.ComponentModel;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Database;

/// <summary>
/// Interface representing Catalog Reference data
/// </summary>
public interface IReferenceData : IBindingData<ReferenceValue>
{ }

class ReferenceData : DbReferenceCollection<ReferenceValue>,
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
    { return factory.CreateSave(this, dataKey).ToList(); }

    /// <inheritdoc/>
    /// <remarks>Reference</remarks>
    public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
    { return Delete(); }

    /// <inheritdoc/>
    /// <remarks>Reference</remarks>
    public IReadOnlyList<WorkItem> Delete()
    { return new WorkItem() { WorkName = "Remove Reference", DoWork = () => { this.Clear(); } }.ToList(); }

    /// <inheritdoc/>
    /// <remarks>Reference</remarks>
    public IReadOnlyList<WorkItem> Delete(ICatalogKey dataKey)
    { return new WorkItem() { WorkName = "Remove Reference", DoWork = () => { this.Remove(dataKey); } }.ToList(); }

}
