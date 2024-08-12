using DataDictionary.DataLayer.DatabaseData.Reference;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Database;

/// <inheritdoc/>
public interface IReferencedIndexObject : IDbReferencedKeyObject
{ }

/// <inheritdoc/>
public class ReferencedIndexObject : DbReferencedKeyObject, IReferencedIndexObject,
    IKeyEquality<IReferencedIndexObject>, IKeyEquality<ReferencedIndexObject>
{
    /// <inheritdoc cref="DbReferencedKeyObject(IDbReferencedKeyObject)"/>
    public ReferencedIndexObject(IReferencedIndexObject source) : base(source) { }

    /// <inheritdoc/>
    public Boolean Equals(IReferencedIndexObject? other)
    { return other is IReferencedIndexObject value && Equals(new DbReferencedKeyObject(value)); }

    /// <inheritdoc/>
    public Boolean Equals(ReferencedIndexObject? other)
    { return other is IReferencedIndexObject value && Equals(new DbReferencedKeyObject(value)); }
}