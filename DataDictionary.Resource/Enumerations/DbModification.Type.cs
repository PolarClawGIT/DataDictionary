namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// The SQL Modification/Statement that was executed to produce this row
/// </summary>
public enum DbModificationType
{
    /// <summary>
    /// Not defined, default value.
    /// </summary>
    Null,

    /// <summary>
    /// The row was inserted
    /// </summary>
    Inserted,

    /// <summary>
    /// The row was updated
    /// </summary>
    Updated,

    /// <summary>
    /// The row was deleted
    /// </summary>
    Deleted
}
