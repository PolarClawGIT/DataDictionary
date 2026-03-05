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
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(RoutineParameter));

        /// <summary>  
        /// The stored procedure to retrieve routine parameters.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetRoutineParameter]");

        /// <summary>  
        /// The stored procedure to set routine parameters.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetRoutineParameter]");

        /// <summary>  
        /// The table type used for routine parameters.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttRoutineParameter]");
    }
}