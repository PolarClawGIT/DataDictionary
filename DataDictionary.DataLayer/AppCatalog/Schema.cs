namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Base Catalog Schema interface (data elements only)
    /// </summary>
    public interface ISchema : ISchemaKeyName
    { }

    static class Schema
    {
        public const String GetProcedure = "[AppCatalog].[procGetSchema]";
        public const String IsSystem = "dbo, sys, db_owner, db_accessadmin, db_securityadmin, db_ddladmin, db_backupoperator, db_datareader, db_datawriter, db_denydatareader, db_denydatawriter, INFORMATION_SCHEMA, guest";
        public const String SchemaId = "@SchemaId";
        public const String SetProcedure = "[AppCatalog].[procSetSchema]";
        public const String TableType = "[AppCatalog].[udttSchema]";
    }
}