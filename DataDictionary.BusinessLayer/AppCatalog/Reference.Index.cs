using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppCatalog;

/// <inheritdoc/>
public interface IReferenceIndex : IReferenceKey
{ }

/// <inheritdoc/>
public class ReferenceIndex : ReferenceKey, IReferenceIndex,
    IKeyEquality<IReferenceIndex>, IKeyEquality<ReferenceIndex>
{
    /// <inheritdoc cref="ReferenceKey(IReferenceKey)"/>
    public ReferenceIndex(IReferenceIndex source) : base(source) { }

    /// <inheritdoc/>
    public Boolean Equals(IReferenceIndex? other)
    { return other is IReferenceKey value && Equals(new ReferenceKey(value)); }

    /// <inheritdoc/>
    public Boolean Equals(ReferenceIndex? other)
    { return other is IReferenceKey value && Equals(new ReferenceKey(value)); }

    /// <summary>
    /// Convert ReferenceIndex to a DataIndex
    /// </summary>
    /// <param name="source"></param>
    public static implicit operator DataIndex(ReferenceIndex source)
    { return new DataIndex() { SystemId = source.ReferenceId ?? Guid.Empty }; }
}
