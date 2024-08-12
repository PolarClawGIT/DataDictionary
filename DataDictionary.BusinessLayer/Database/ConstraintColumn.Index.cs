using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Database;

/// <inheritdoc/>
public interface IConstraintColumnIndexName : IDbConstraintColumnKeyName { }

/// <inheritdoc/>
public class ConstraintColumnIndexName : DbConstraintColumnKeyName, IConstraintColumnIndexName,
    IKeyEquality<IConstraintColumnIndexName>, IKeyEquality<ConstraintColumnIndexName>
{
    /// <inheritdoc cref="DbConstraintColumnKeyName(IDbConstraintColumnKeyName)"/>
    public ConstraintColumnIndexName(IConstraintColumnIndexName source) : base(source) { }

    /// <inheritdoc cref="DbConstraintColumnKeyName(IDbTableColumnKeyName)"/>
    public ConstraintColumnIndexName(ITableColumnIndexName source) : base(source) { }

    /// <inheritdoc/>
    public Boolean Equals(IConstraintColumnIndexName? other)
    { return other is IConstraintColumnIndexName value && Equals(new DbConstraintColumnKeyName(value)); }

    /// <inheritdoc/>
    public Boolean Equals(ConstraintColumnIndexName? other)
    { return other is IConstraintColumnIndexName value && Equals(new DbConstraintColumnKeyName(value)); }
}