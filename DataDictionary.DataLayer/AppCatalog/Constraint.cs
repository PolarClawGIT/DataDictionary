namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Base Catalog Constraint interface (data elements only)
    /// </summary>
    public interface IConstraint : IConstraintKeyName, IConstraintType, ITableKeyName
    { }

    /// <summary>
    /// Common Catalog Table Type
    /// </summary>
    public interface IConstraintType
    {
        /// <summary>
        /// Type of Constraint (Primary Key, Unique Key, Foreign Key, Default, ...)
        /// </summary>
        String? ConstraintType { get; }
    }

    /// <summary>  
    /// Static class containing constants related to the Constraints database operations.
    /// </summary>  
    static class Constraint
    {
        /// <summary>  
        /// Identifier for the constraint.  
        /// </summary>  
        public const String ConstraintId = "@ConstraintId";

        /// <summary>  
        /// Stored procedure to retrieve constraint information.  
        /// </summary>  
        public const String GetProcedure = "[AppCatalog].[procGetConstraint]";

        /// <summary>  
        /// Stored procedure to set constraint information.  
        /// </summary>  
        public const String SetProcedure = "[AppCatalog].[procSetConstraint]";

        /// <summary>  
        /// Table type for constraints.  
        /// </summary>  
        public const String TableType = "[AppCatalog].[udttConstraint]";
    }
}