using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.DatabaseData.Reference;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Database;

/// <inheritdoc/>
public interface IReferenceIndexName : IDbReferenceKeyName
{ }

/// <inheritdoc/>
public class ReferenceIndexName : DbReferenceKeyName, IReferenceIndexName,
    IKeyEquality<IReferenceIndexName>, IKeyEquality<ReferenceIndexName>
{
    /// <inheritdoc cref="DbReferenceKeyName(IDbReferenceKeyName)"/>
    public ReferenceIndexName(IDbReferenceKeyName source) : base(source) { }

    /// <inheritdoc cref="DbReferenceKeyName(IDbTableKeyName)"/>
    public ReferenceIndexName(ITableIndexName source) : base(source) { }

    /// <inheritdoc cref="DbReferenceKeyName(IDbRoutineKeyName)"/>
    public ReferenceIndexName(IRoutineIndexName source) : base(source) { }

    /// <inheritdoc/>
    public Boolean Equals(IReferenceIndexName? other)
    { return other is IDbReferenceKeyName value && Equals(new DbReferenceKeyName(value)); }

    /// <inheritdoc/>
    public Boolean Equals(ReferenceIndexName? other)
    { return other is IDbReferenceKeyName value && Equals(new DbReferenceKeyName(value)); }

    /// <summary>
    /// Convert DomainIndexName to a DataIndexName
    /// </summary>
    /// <param name="source"></param>
    public static implicit operator DataIndexName(ReferenceIndexName source)
    { return new DataIndexName() { Title = source.ObjectName ?? String.Empty }; }
}