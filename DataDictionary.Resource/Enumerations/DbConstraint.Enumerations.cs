namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Enumeration support class for Database Constraint type.
/// </summary>
public class DbConstraintEnumeration : Enumeration<DbConstraintType, DbConstraintEnumeration>
{
    /// <summary>
    /// Internal Constructor for Database Constraint Enumeration
    /// </summary>
    /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
    DbConstraintEnumeration(DbConstraintType value, String name) : base(value, name) { }

    /// <summary>
    /// Static constructor, loads data.
    /// </summary>
    static DbConstraintEnumeration()
    {
        List<DbConstraintEnumeration> data = new List<DbConstraintEnumeration>()
    {
        new DbConstraintEnumeration(DbConstraintType.Null,       String.Empty)  { DisplayName = "not defined" },
        new DbConstraintEnumeration(DbConstraintType.Check,      "CHECK")       { DisplayName = "Check" },
        new DbConstraintEnumeration(DbConstraintType.Unique,     "UNIQUE")      { DisplayName = "Unique Key" },
        new DbConstraintEnumeration(DbConstraintType.PrimaryKey, "PRIMARY KEY") { DisplayName = "Primary Key" },
        new DbConstraintEnumeration(DbConstraintType.ForeignKey, "FOREIGN KEY") { DisplayName = "Foreign Key" },
    };

        BuildDictionary(data);
    }
}
