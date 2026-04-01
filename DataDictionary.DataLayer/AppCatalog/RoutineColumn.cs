// Ignore Spelling: Nullable
namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Base Catalog RoutineColumn interface (data elements only)
    /// </summary>
    public interface IRoutineColumn : IRoutineColumnKeyName, IDataType, IOrdinalPosition, IRoutineType, IDomainKeyReference
    {
        /// <summary>
        /// Is the Column Nullable
        /// </summary>
        Boolean? IsNullable { get; }

        /// <summary>
        /// Column Default value
        /// </summary>
        String? ColumnDefault { get; }

        /// <summary>
        /// Is the Column an Identity
        /// </summary>
        Boolean? IsIdentity { get; }

        /// <summary>
        /// Is the Column Computed
        /// </summary>
        Boolean? IsComputed { get; }

        /// <summary>
        /// If the Column is Computed, the Computed Definition
        /// </summary>
        String? ComputedDefinition { get; }
    }

    /// <summary>  
    /// Static class containing constants related to the Routine Column database operations..  
    /// </summary>  
    static class RoutineColumn
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(RoutineColumn));

        /// <summary>  
        /// Stored procedure to retrieve RoutineColumn data.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// Stored procedure to set RoutineColumn data.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// User-defined table type for RoutineColumn.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}