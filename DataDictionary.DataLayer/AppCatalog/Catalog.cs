using System.Reflection;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Base Catalog Interface (data elements only)
    /// </summary>
    public interface ICatalog : ICatalogKeyName
    {
        /// <summary>
        /// Title given to the Catalog. Default is the Database Name.
        /// </summary>
        String? CatalogTitle { get; }

        /// <summary>
        /// Description given to the Catalog.
        /// </summary>
        String? CatalogDescription { get; }

        /// <summary>
        /// The SQL Server that the database was extracted from.
        /// </summary>
        String? ServerName { get; }

        /// <summary>
        /// The Date that the database was extracted.
        /// </summary>
        DateTime? SourceDate { get; }
    }

    /// <summary>
    /// Static class containing constants related to the Catalog database operations.
    /// </summary>
    static class Catalog
    {
        /// <summary>
        /// Identifier for the Catalog.
        /// </summary>
        public const String CatalogId = "@CatalogId";

        /// <summary>
        /// Stored procedure to retrieve the Catalog.
        /// </summary>
        public const String GetProcedure = "[AppCatalog].[procGetCatalog]";

        /// <summary>
        /// Names of system databases.
        /// </summary>
        public const String IsSystem = "tempdb, master, msdb, model";

        /// <summary>
        /// Substring indicating a LocalDB instance.
        /// </summary>
        public const String LocalDbContains = "\\LOCALDB";

        /// <summary>
        /// Name of the default LocalDB instance.
        /// </summary>
        public const String LocalDbName = "(LocalDb)\\MSSQLLocalDb";

        /// <summary>
        /// Stored procedure to set the Catalog.
        /// </summary>
        public const String SetProcedure = "[AppCatalog].[procSetCatalog]";

        /// <summary>
        /// Table type for the Catalog.
        /// </summary>
        public const String TableType = "[AppCatalog].[udttCatalog]";
    }
}