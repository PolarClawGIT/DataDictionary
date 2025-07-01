namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Base Catalog Schema interface (data elements only)
    /// </summary>
    public interface ISchema : ISchemaKeyName
    { }

    /// <summary>
    /// Static class containing constants related to the Schema database operations.
    /// </summary>
    static class Schema
    {
        /// <summary>
        /// The stored procedure to retrieve schema information.
        /// </summary>
        public const string GetProcedure = "[AppCatalog].[procGetSchema]";

        /// <summary>
        /// A comma-separated list of system schemas.
        /// </summary>
        public const string IsSystem = "dbo, sys, db_owner, db_accessadmin, db_securityadmin, db_ddladmin, db_backupoperator, db_datareader, db_datawriter, db_denydatareader, db_denydatawriter, INFORMATION_SCHEMA, guest";

        /// <summary>
        /// The parameter name for schema ID.
        /// </summary>
        public const string SchemaId = "@SchemaId";

        /// <summary>
        /// The stored procedure to set schema information.
        /// </summary>
        public const string SetProcedure = "[AppCatalog].[procSetSchema]";

        /// <summary>
        /// The user-defined table type for schema.
        /// </summary>
        public const string TableType = "[AppCatalog].[udttSchema]";
    }
}