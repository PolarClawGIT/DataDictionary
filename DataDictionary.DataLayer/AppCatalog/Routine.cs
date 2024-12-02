namespace DataDictionary.DataLayer.AppCatalog;

/// <summary>
/// Base Catalog Routine interface (data elements only)
/// </summary>
public interface IRoutine : IRoutineKeyName
{
    /// <summary>
    /// Type of Routine (Procedure, Function, ...)
    /// </summary>
    String? RoutineType { get; }
}