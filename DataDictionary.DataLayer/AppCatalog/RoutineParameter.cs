namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Base Catalog Routine Parameter interface (data elements only)
    /// </summary>
    public interface IRoutineParameter : IRoutineParameterKeyName, 
        IDataType, IOrdinalPosition, IRoutineType, IDomainKeyReference
    { }

    /// <summary>  
    /// Static class containing constants related to the Routine Parameter database operations.
    /// </summary>  
    static class RoutineParameter
    {
        /// <summary>  
        /// The stored procedure to retrieve routine parameters.  
        /// </summary>  
        public const string GetProcedure = "[AppCatalog].[procGetRoutineParameter]";

        /// <summary>  
        /// The stored procedure to set routine parameters.  
        /// </summary>  
        public const string SetProcedure = "[AppCatalog].[procSetRoutineParameter]";

        /// <summary>  
        /// The table type used for routine parameters.  
        /// </summary>  
        public const string TableType = "[AppCatalog].[udttRoutineParameter]";
    }
}