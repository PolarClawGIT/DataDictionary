// Place holder for file nesting purposes.
namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Interface for DbRoutineType
/// </summary>
public interface IDbRoutineType
{
    /// <summary>
    /// Type of Routine (such as procedure or function)
    /// </summary>
    DbRoutineType RoutineType { get; }
}

public static class DbRoutineExtension
{
    /// <summary>
    /// Gets the Data from DbRoutineEnumeration
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static DbRoutineEnumeration Data(this DbRoutineType source)
    { return DbRoutineEnumeration.AsDictionary[source]; }
}