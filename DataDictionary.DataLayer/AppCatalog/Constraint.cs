namespace DataDictionary.DataLayer.AppCatalog;

/// <summary>
/// Base Catalog Constraint interface (data elements only)
/// </summary>
public interface IConstraint: IConstraintKeyName, IConstraintType, ITableKeyName
{ }

/// <summary>
/// Common Catalog Table Type
/// </summary>
public interface IConstraintType
{
    /// <summary>
    /// Type of Constraint (Primary Key, Unique Key, Foreign Key, Default, ...)
    /// </summary>
    String? ConstraintType { get; }
}