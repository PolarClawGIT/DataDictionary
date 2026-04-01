namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Base Catalog ConstraintColumn interface (data elements only)
    /// </summary>
    public interface IConstraintColumn : IConstraintColumnKeyName, IOrdinalPosition,
        ITableColumnKeyName, IConstraintColumnKeyReferenced
    {
        /// <inheritdoc cref="IConstraintColumnKeyName.ColumnName"/>
        new String? ColumnName { get; }
    }

    ///<summary>  
    /// Static class containing constants related to the Constraint Column database operations.
    /// </summary>  
    static class ConstraintColumn
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(ConstraintColumn));

        /// <summary>  
        /// The stored procedure to retrieve constraint column data.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure to set constraint column data.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The user-defined table type for constraint column data.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}