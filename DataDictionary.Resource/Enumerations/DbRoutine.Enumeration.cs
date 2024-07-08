using System.Diagnostics.CodeAnalysis;
namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Enumeration support class for Database Routine type.
/// </summary>
public class DbRoutineEnumeration : Enumeration<DbRoutineType, DbRoutineEnumeration>
{
    /// <summary>
    /// Internal Constructor for Database Routine Enumeration
    /// </summary>
    /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
    DbRoutineEnumeration(DbRoutineType source) : base(source) { }

    static DbRoutineEnumeration()
    {
        List<DbRoutineEnumeration> data = new List<DbRoutineEnumeration>()
        {
            new DbRoutineEnumeration(DbRoutineType.Null) { Name = String.Empty, DisplayName = "not defined" },
            new DbRoutineEnumeration(DbRoutineType.Function),
            new DbRoutineEnumeration(DbRoutineType.Procedure),
        };

        BuildDictionary(data);
    }
}
