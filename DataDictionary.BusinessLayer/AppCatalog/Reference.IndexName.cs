using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppCatalog;

/// <inheritdoc/>
public interface IReferenceIndexName : IReferenceKeyName
{ }

/// <inheritdoc/>
public class ReferenceIndexName : ReferenceKeyName, IReferenceIndexName,
    IKeyEquality<IReferenceIndexName>, IKeyEquality<ReferenceIndexName>
{
    /// <inheritdoc cref="ReferenceKeyName(IReferenceKeyName)"/>
    public ReferenceIndexName(IReferenceKeyName source) : base(source) { }

    /// <inheritdoc cref="ReferenceKeyName(ITableKeyName)"/>
    public ReferenceIndexName(ITableIndexName source) : base(source) { }

    /// <inheritdoc cref="ReferenceKeyName(IRoutineKeyName)"/>
    public ReferenceIndexName(IRoutineIndexName source) : base(source) { }

    /// <inheritdoc/>
    public Boolean Equals(IReferenceIndexName? other)
    { return other is IReferenceKeyName value && Equals(new ReferenceKeyName(value)); }

    /// <inheritdoc/>
    public Boolean Equals(ReferenceIndexName? other)
    { return other is IReferenceKeyName value && Equals(new ReferenceKeyName(value)); }

    /// <summary>
    /// Convert DomainIndexName to a DataIndexName
    /// </summary>
    /// <param name="source"></param>
    public static implicit operator DataIndexName(ReferenceIndexName source)
    { return new DataIndexName() { Title = source.ObjectName ?? String.Empty }; }
}