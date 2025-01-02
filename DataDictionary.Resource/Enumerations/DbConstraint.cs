namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Interface for DbConstraintType
/// </summary>
public interface IDbConstraintType
{
    /// <summary>
    /// Type of Constraint (Primary Key, Unique Key, Foreign Key, ...)
    /// </summary>
    DbConstraintType ConstraintType { get; }
}