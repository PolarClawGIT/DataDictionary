using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Database;

/// <inheritdoc/>
public interface IConstraintColumnIndexReferenced : IDbConstraintColumnKeyReferenced { }

/// <inheritdoc/>
public class ConstraintColumnIndexReferenced : DbConstraintColumnKeyReferenced, IConstraintColumnIndexReferenced,
    IKeyEquality<IConstraintColumnIndexReferenced>, IKeyEquality<ConstraintColumnIndexReferenced>
{
    /// <inheritdoc cref="DbConstraintColumnKeyReferenced(IDbConstraintColumnKeyReferenced)"/>
    public ConstraintColumnIndexReferenced(IConstraintColumnIndexReferenced source) : base(source) { }

    /// <inheritdoc/>
    public override TableColumnIndexName AsColumnName()
    { return new TableColumnIndexName(base.AsColumnName()); }

    /// <inheritdoc/>
    public Boolean Equals(IConstraintColumnIndexReferenced? other)
    { return other is IConstraintColumnIndexReferenced value && Equals(new DbConstraintColumnKeyReferenced(value)); }

    /// <inheritdoc/>
    public Boolean Equals(ConstraintColumnIndexReferenced? other)
    { return other is IConstraintColumnIndexReferenced value && Equals(new DbConstraintColumnKeyReferenced(value)); }
}