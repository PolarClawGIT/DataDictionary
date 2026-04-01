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
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(Constraint));

        /// <summary>
        /// Identifier for the Constraint.
        /// </summary>
        public readonly static String Identifier = dataObject.Identifier;

        /// <summary>
        /// Stored procedure to retrieve the Constraint.
        /// </summary>
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>
        /// Stored procedure to set the Constraint.
        /// </summary>
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>
        /// Table type for the Constraint.
        /// </summary>
        public readonly static String TableType = dataObject.TableType;
    }
}