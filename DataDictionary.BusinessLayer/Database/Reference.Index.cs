using DataDictionary.DataLayer.DatabaseData.Reference;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Database;

/// <inheritdoc/>
public interface IReferenceIndex : IDbReferenceKey
{ }

/// <inheritdoc/>
public class ReferenceIndex: DbReferenceKey, IReferenceIndex,
    IKeyEquality<IReferenceIndex>, IKeyEquality<ReferenceIndex>
{
    /// <inheritdoc cref="DbReferenceKey(IDbReferenceKey)"/>
    public ReferenceIndex(IReferenceIndex source) : base(source) { }

    /// <inheritdoc/>
    public Boolean Equals(IReferenceIndex? other)
    { return other is IDbReferenceKey value && Equals(new DbReferenceKey(value)); }

    /// <inheritdoc/>
    public Boolean Equals(ReferenceIndex? other)
    { return other is IDbReferenceKey value && Equals(new DbReferenceKey(value)); }

    /// <summary>
    /// Convert ReferenceIndex to a DataLayerIndex
    /// </summary>
    /// <param name="source"></param>
    public static implicit operator DataLayerIndex(ReferenceIndex source)
    { return new DataLayerIndex() { BusinessLayerId = source.ReferenceId ?? Guid.Empty }; }
}
