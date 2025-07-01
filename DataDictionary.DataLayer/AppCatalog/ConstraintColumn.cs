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
        /// <summary>  
        /// The stored procedure to retrieve constraint column data.  
        /// </summary>  
        public const String GetProcedure = "[AppCatalog].[procGetConstraintColumn]";

        /// <summary>  
        /// The stored procedure to set constraint column data.  
        /// </summary>  
        public const String SetProcedure = "[AppCatalog].[procSetConstraintColumn]";

        /// <summary>  
        /// The user-defined table type for constraint column data.  
        /// </summary>  
        public const String TableType = "[AppCatalog].[udttConstraintColumn]";
    }
}