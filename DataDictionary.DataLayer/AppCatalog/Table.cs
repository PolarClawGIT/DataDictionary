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

    static class Table
    {
        public const String GetProcedure = "[AppCatalog].[procGetTable]";
        public const String IsSystem = "dbo.__RefactorLog, dbo.sysdiagrams, INFORMATION_SCHEMA.*, sys.*";
        public const String SetProcedure = "[AppCatalog].[procSetTable]";
        public const String TableId = "@TableId";
        public const String TableType = "[AppCatalog].[udttTable]";
    }
}