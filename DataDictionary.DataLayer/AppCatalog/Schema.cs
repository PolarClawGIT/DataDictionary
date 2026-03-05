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
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(Schema));

        /// <summary>
        /// The stored procedure to retrieve schema information.
        /// </summary>
        public readonly static String GetProcedure = schema.FullName("[procGetSchema]");

        /// <summary>
        /// A comma-separated list of system schemas.
        /// </summary>
        public readonly static String IsSystem = "dbo, sys, db_owner, db_accessadmin, db_securityadmin, db_ddladmin, db_backupoperator, db_datareader, db_datawriter, db_denydatareader, db_denydatawriter, INFORMATION_SCHEMA, guest";

        /// <summary>
        /// The parameter name for schema ID.
        /// </summary>
        public readonly static String SchemaId = "@SchemaId";

        /// <summary>
        /// The stored procedure to set schema information.
        /// </summary>
        public readonly static String SetProcedure = schema.FullName("[procSetSchema]");

        /// <summary>
        /// The user-defined table type for schema.
        /// </summary>
        public readonly static String TableType = schema.FullName("[udttSchema]");
    }
}