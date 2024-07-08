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
    DbRoutineEnumeration() : base() { }

    /// <summary>
    /// Internal list that contains all the values.
    /// </summary>
    static readonly new List<DbRoutineEnumeration> Data = new List<DbRoutineEnumeration>() 
    {
        new DbRoutineEnumeration() { Value = DbRoutineType.Null, Name = String.Empty, DisplayName = "not defined" },
        new DbRoutineEnumeration() { Value = DbRoutineType.Function, Name = "Function", DisplayName = "Function" },
        new DbRoutineEnumeration() { Value = DbRoutineType.Procedure, Name = "Procedure", DisplayName = "Procedure" },
    };
}
