namespace DataDictionary.DataLayer.AppCatalog;

/// <summary>
/// Base Catalog Routine Parameter interface (data elements only)
/// </summary>
public interface IRoutineParameter : IRoutineParameterKeyName, 
    IDataType, IOrdinalPosition, IRoutineType, IDomainKeyReference
{ }