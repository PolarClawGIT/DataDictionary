namespace DataDictionary.DataLayer.AppCatalog;

/// <summary>
/// Base Catalog Routine interface (data elements only)
/// </summary>
public interface IRoutine : IRoutineKeyName, IRoutineType
{

}

/// <summary>
/// Common Catalog Routine Type
/// </summary>
public interface IRoutineType
{
    /// <summary>
    /// Type of Routine (Procedure, Function, ...)
    /// </summary>
    String? RoutineType { get; }
}