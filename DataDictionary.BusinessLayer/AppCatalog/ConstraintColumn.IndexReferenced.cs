using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppCatalog;

/// <inheritdoc/>
public interface IConstraintColumnIndexReferenced : IConstraintColumnKeyReferenced { }

/// <inheritdoc/>
public class ConstraintColumnIndexReferenced : ConstraintColumnKeyReferenced, IConstraintColumnIndexReferenced,
    IKeyEquality<IConstraintColumnIndexReferenced>, IKeyEquality<ConstraintColumnIndexReferenced>
{
    /// <inheritdoc cref="ConstraintColumnKeyReferenced(IConstraintColumnKeyReferenced)"/>
    public ConstraintColumnIndexReferenced(IConstraintColumnIndexReferenced source) : base(source) { }

    /// <inheritdoc/>
    public override TableColumnIndexName AsColumnName()
    { return new TableColumnIndexName(base.AsColumnName()); }

    /// <inheritdoc/>
    public Boolean Equals(IConstraintColumnIndexReferenced? other)
    { return other is IConstraintColumnIndexReferenced value && Equals(new ConstraintColumnKeyReferenced(value)); }

    /// <inheritdoc/>
    public Boolean Equals(ConstraintColumnIndexReferenced? other)
    { return other is IConstraintColumnIndexReferenced value && Equals(new ConstraintColumnKeyReferenced(value)); }
}