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
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(RoutineParameter));

        /// <summary>  
        /// The stored procedure to retrieve routine parameters.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure to set routine parameters.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The table type used for routine parameters.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}