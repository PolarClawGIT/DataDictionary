namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Base Catalog Routine Parameter interface (data elements only)
    /// </summary>
    public interface IRoutineParameter : IRoutineParameterKeyName, 
        IDataType, IOrdinalPosition, IRoutineType, IDomainKeyReference
    { }

    static class RoutineParameter
    {
        public const String GetProcedure = "[AppCatalog].[procGetRoutineParameter]";
        public const String SetProcedure = "[AppCatalog].[procSetRoutineParameter]";
        public const String TableType = "[AppCatalog].[udttRoutineParameter]";
    }
}