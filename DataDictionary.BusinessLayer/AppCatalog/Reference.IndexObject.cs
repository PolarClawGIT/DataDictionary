using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppCatalog;

/// <inheritdoc/>
public interface IReferencedIndexObject : IReferencedKeyObject
{ }

/// <inheritdoc/>
public class ReferencedIndexObject : ReferencedKeyObject, IReferencedIndexObject,
    IKeyEquality<IReferencedIndexObject>, IKeyEquality<ReferencedIndexObject>
{
    /// <inheritdoc cref="ReferencedKeyObject(IReferencedKeyObject)"/>
    public ReferencedIndexObject(IReferencedIndexObject source) : base(source) { }

    /// <inheritdoc/>
    public Boolean Equals(IReferencedIndexObject? other)
    { return other is IReferencedIndexObject value && Equals(new ReferencedKeyObject(value)); }

    /// <inheritdoc/>
    public Boolean Equals(ReferencedIndexObject? other)
    { return other is IReferencedIndexObject value && Equals(new ReferencedKeyObject(value)); }
}