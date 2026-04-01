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
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(Catalog));

        /// <summary>
        /// Identifier for the Catalog.
        /// </summary>
        public readonly static String Identifier = dataObject.Identifier;

        /// <summary>
        /// Stored procedure to retrieve the Catalog.
        /// </summary>
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>
        /// Stored procedure to set the Catalog.
        /// </summary>
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>
        /// Table type for the Catalog.
        /// </summary>
        public readonly static String TableType = dataObject.TableType;

        /// <summary>
        /// Names of system databases.
        /// </summary>
        public readonly static String IsSystem = "tempdb, master, msdb, model";

        /// <summary>
        /// Substring indicating a LocalDB instance.
        /// </summary>
        public readonly static String LocalDbContains = "\\LOCALDB";

        /// <summary>
        /// Name of the default LocalDB instance.
        /// </summary>
        public readonly static String LocalDbName = "(LocalDb)\\MSSQLLocalDb";
    }
}