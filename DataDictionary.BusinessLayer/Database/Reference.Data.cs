using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
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
        ILoadData<IDbCatalogKey>, ISaveData<IDbCatalogKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDatabaseModelItem, IReferenceData
{
    /// <inheritdoc/>
    public required IDatabaseModel Database { get; init; }

    /// <inheritdoc/>
    /// <remarks>Reference</remarks>
    public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDbCatalogKey dataKey)
    { return factory.CreateLoad(this, dataKey).ToList(); }

    /// <inheritdoc/>
    /// <remarks>Reference</remarks>
    public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
    { return factory.CreateLoad(this, dataKey).ToList(); }

    /// <inheritdoc/>
    /// <remarks>Reference</remarks>
    public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDbCatalogKey dataKey)
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
    public IReadOnlyList<WorkItem> Delete(IDbCatalogKey dataKey)
    { return new WorkItem() { WorkName = "Remove Reference", DoWork = () => { this.Remove(dataKey); } }.ToList(); }

}
