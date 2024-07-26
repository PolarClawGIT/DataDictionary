namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Enumeration support class for Database Extended Procedure Catalog Level type.
    /// </summary>
    public class DbLevelCatalogEnumeration : Enumeration<DbLevelCatalogType, DbLevelCatalogEnumeration>
    {
        /// <summary>
        /// Internal Constructor for Database Extended Procedure Catalog Level Enumeration
        /// </summary>
        /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
        DbLevelCatalogEnumeration(DbLevelCatalogType value, String name) : base(value, name) { }

        /// <summary>
        /// Static constructor, loads data.
        /// </summary>
        static DbLevelCatalogEnumeration()
        {
            List<DbLevelCatalogEnumeration> data = new List<DbLevelCatalogEnumeration>()
            {
                new DbLevelCatalogEnumeration(DbLevelCatalogType.Null, String.Empty) { DisplayName = "not defined" },
                new DbLevelCatalogEnumeration(DbLevelCatalogType.Assembly,"ASSEMBLY" ),
                new DbLevelCatalogEnumeration(DbLevelCatalogType.Contract, "CONTRACT"),
                new DbLevelCatalogEnumeration(DbLevelCatalogType.EventNotification,"EVENT NOTIFICATION"),
                new DbLevelCatalogEnumeration(DbLevelCatalogType.Filegroup,"FILEGROUP"),
                new DbLevelCatalogEnumeration(DbLevelCatalogType.MessageType, "MESSAGE TYPE"),
                new DbLevelCatalogEnumeration(DbLevelCatalogType.PartitionFunction, "PARTITION FUNCTION"),
                new DbLevelCatalogEnumeration(DbLevelCatalogType.PartitionScheme, "PARTITION SCHEME"),
                new DbLevelCatalogEnumeration(DbLevelCatalogType.RemoteServiceBinding, "REMOTE SERVICE BINDING"),
                new DbLevelCatalogEnumeration(DbLevelCatalogType.Route, "ROUTE"),
                new DbLevelCatalogEnumeration(DbLevelCatalogType.Schema, "SCHEMA"),
                new DbLevelCatalogEnumeration(DbLevelCatalogType.Service, "SERVICE"),
                new DbLevelCatalogEnumeration(DbLevelCatalogType.Trigger, "TRIGGER"),
                new DbLevelCatalogEnumeration(DbLevelCatalogType.Type, "TYPE"),
                new DbLevelCatalogEnumeration(DbLevelCatalogType.User, "USER"),
            };

            BuildDictionary(data);
        }

    }
}
