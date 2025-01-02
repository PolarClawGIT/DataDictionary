namespace DataDictionary.DataLayer.AppCatalog;

/// <summary>
/// Base Catalog ConstraintColumn interface (data elements only)
/// </summary>
public interface IConstraintColumn : IConstraintColumnKeyName, IOrdinalPosition,
    ITableColumnKeyName, IConstraintColumnKeyReferenced
{
    /// <inheritdoc cref="IConstraintColumnKeyName.ColumnName"/>
    new String? ColumnName { get; }
}