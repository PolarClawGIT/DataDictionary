namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Base Catalog Table interface (data elements only)
    /// </summary>
    public interface ITable : ITableKeyName, ITableType
    {

    }

    /// <summary>
    /// Common Catalog Table Type
    /// </summary>
    public interface ITableType
    {
        /// <summary>
        /// Type of Table (Table, Temporal Table, Historic Table, View)
        /// </summary>
        String? TableType { get; }
    }

    /// <summary>
    /// Static class containing constants related to the Tables database operations.
    /// </summary>
    static class Table
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(Table));

        /// <summary>
        /// The stored procedure used to retrieve table information.
        /// </summary>
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>
        /// A list of system tables that should be excluded from certain operations.
        /// </summary>
        public readonly static String IsSystem = "dbo.__RefactorLog, dbo.sysdiagrams, INFORMATION_SCHEMA.*, sys.*";

        /// <summary>
        /// The stored procedure used to set table information.
        /// </summary>
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>
        /// The parameter name for the table ID.
        /// </summary>
        public readonly static String Identifier = dataObject.Identifier;

        /// <summary>
        /// The user-defined table type used in operations.
        /// </summary>
        public readonly static String TableType = dataObject.TableType;
    }
}