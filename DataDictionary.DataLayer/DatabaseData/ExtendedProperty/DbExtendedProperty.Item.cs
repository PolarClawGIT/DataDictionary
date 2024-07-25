using System.Data;
using Toolbox.BindingTable;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.Resource;

namespace DataDictionary.DataLayer.DatabaseData.ExtendedProperty
{
    /// <summary>
    /// Interface for MS SQL ExtendedProperty as stored in the Application Database.
    /// </summary>
    public interface IDbExtendedPropertyItem : IDbCatalogKeyName, IDbCatalogKey, IDbExtendedPropertyParameter
    {
        /// <summary>
        /// MS SQL ExtendedProperty Level 0 Type
        /// </summary>
        DbLevelCatalog CatalogScope { get; }

        /// <summary>
        /// MS SQL ExtendedProperty Level 1 Type
        /// </summary>
        DbLevelObject ObjectScope { get; }

        /// <summary>
        /// MS SQL ExtendedProperty Level 2 Type
        /// </summary>
        DbLevelElement ElementScope { get; }

        /// <summary>
        /// Object Type returned sys.fn_listextendedproperty 
        /// </summary>
        String? ObjectType { get; }

        /// <summary>
        /// Object Name returned by sys.fn_listextendedproperty
        /// </summary>
        String? ObjectName { get; }
    }

    /// <summary>
    /// Implementation of MS SQL ExtendedProperty as stored in the Application Database.
    /// </summary>
    public class DbExtendedPropertyItem : BindingTableRow, IDbExtendedPropertyItem
    {
        /// <inheritdoc/>
        public Guid? CatalogId { get { return GetValue<Guid>(nameof(CatalogId)); } }

        /// <inheritdoc/>
        public string? DatabaseName { get { return GetValue(nameof(DatabaseName)); } }

        /// <inheritdoc/>
        public string? Level0Type { get { return GetValue(nameof(Level0Type)); } }

        /// <inheritdoc/>
        public string? Level0Name { get { return GetValue(nameof(Level0Name)); } }

        /// <inheritdoc/>
        public DbLevelCatalog CatalogScope { get { return ExtendedPropertyExtension.GetCatalogScope(Level0Type); } }

        /// <inheritdoc/>
        public string? Level1Type { get { return GetValue(nameof(Level1Type)); } }

        /// <inheritdoc/>
        public string? Level1Name { get { return GetValue(nameof(Level1Name)); } }

        /// <inheritdoc/>
        public DbLevelObject ObjectScope { get { return ExtendedPropertyExtension.GetObjectScope(Level1Type); } }

        /// <inheritdoc/>
        public string? Level2Type { get { return GetValue(nameof(Level2Type)); } }

        /// <inheritdoc/>
        public string? Level2Name { get { return GetValue(nameof(Level2Name)); } }

        /// <inheritdoc/>
        public DbLevelElement ElementScope { get { return ExtendedPropertyExtension.GetItemScope(Level2Type); } }

        /// <summary>
        /// Alias of ObjectType
        /// </summary>
        protected String? ObjType { get { return GetValue(nameof(ObjType)); } }

        /// <inheritdoc/>
        public string? ObjectType { get { return ObjType; } }

        /// <summary>
        /// Alias of ObjectName
        /// </summary>
        protected String? ObjName { get { return GetValue(nameof(ObjName)); } }

        /// <inheritdoc/>
        public string? ObjectName { get { return ObjName; } }

        /// <inheritdoc/>
        public string? PropertyName { get { return GetValue(nameof(PropertyName)); } }

        /// <inheritdoc/>
        public string? PropertyValue { get { return GetValue(nameof(PropertyValue)); } }

        /// <summary>
        /// Constructor for DbExtendedPropertyItem.
        /// </summary>
        public DbExtendedPropertyItem() : base()
        { }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(CatalogId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(DatabaseName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(Level0Type), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(Level0Name), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(Level1Type), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(Level1Name), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(Level2Type), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(Level2Name), typeof(string)){ AllowDBNull = true},

            new DataColumn(nameof(ObjType), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(ObjName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(PropertyName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(PropertyValue), typeof(string)){ AllowDBNull = false},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }
    }

    #region Enum ExtendedProperty translation

    /// <summary>
    /// Helper class that translates the Enums listed above to string values returned by the Database.
    /// Source: https://learn.microsoft.com/en-us/sql/relational-databases/system-functions/sys-fn-listextendedproperty-transact-sql?view=sql-server-ver16
    /// </summary>
    static class ExtendedPropertyExtension
    {
        static Dictionary<DbLevelCatalog, string> catalogScope = new Dictionary<DbLevelCatalog, string>()
        {
            {DbLevelCatalog.Assembly,"ASSEMBLY" },
            {DbLevelCatalog.Contract,"CONTRACT"},
            {DbLevelCatalog.EventNotification,"EVENT NOTIFICATION"},
            {DbLevelCatalog.Filegroup,"FILEGROUP"},
            {DbLevelCatalog.MessageType,"MESSAGE TYPE"},
            {DbLevelCatalog.PartitionFunction,"PARTITION FUNCTION"},
            {DbLevelCatalog.PartitionScheme,"PARTITION SCHEME"},
            {DbLevelCatalog.RemoteServiceBinding,"REMOTE SERVICE BINDING"},
            {DbLevelCatalog.Route,"ROUTE"},
            {DbLevelCatalog.Schema,"SCHEMA"},
            {DbLevelCatalog.Service,"SERVICE"},
            {DbLevelCatalog.Trigger,"TRIGGER"},
            {DbLevelCatalog.Type,"TYPE"},
            {DbLevelCatalog.User,"USER"},
        };

        public static DbLevelCatalog GetCatalogScope(string? value)
        { return catalogScope.FirstOrDefault(w => w.Value.Equals(value, KeyExtension.CompareString)).Key; }

        public static string GetScope(this DbLevelCatalog value)
        {
            if (catalogScope.ContainsKey(value)) { return catalogScope[value]; }
            else { return string.Empty; }
        }

        static Dictionary<DbLevelObject, string> objectScope = new Dictionary<DbLevelObject, string>()
        {
            {DbLevelObject.Aggregate,"AGGREGATE"},
            {DbLevelObject.Default,"DEFAULT"},
            {DbLevelObject.Function,"FUNCTION"},
            {DbLevelObject.LogicalFileName,"LOGICAL FILE NAME"},
            {DbLevelObject.Procedure,"PROCEDURE"},
            {DbLevelObject.Queue,"QUEUE"},
            {DbLevelObject.Rule,"RULE"},
            {DbLevelObject.Synonym,"SYNONYM"},
            {DbLevelObject.Table,"TABLE"},
            {DbLevelObject.Type,"TYPE"},
            {DbLevelObject.View,"VIEW"},
            {DbLevelObject.XmlSchemaCollection,"XML SCHEMA COLLECTION"},
        };

        public static DbLevelObject GetObjectScope(string? value)
        { return objectScope.FirstOrDefault(w => w.Value.Equals(value, KeyExtension.CompareString)).Key; }

        public static string GetScope(this DbLevelObject value)
        {
            if (objectScope.ContainsKey(value)) { return objectScope[value]; }
            else { return string.Empty; }
        }

        static Dictionary<DbLevelElement, string> itemScope = new Dictionary<DbLevelElement, string>()
        {
            {DbLevelElement.Default,"DEFAULT"},
            {DbLevelElement.Column,"COLUMN"},
            {DbLevelElement.Constraint,"CONSTRAINT"},
            {DbLevelElement.EventNotification,"EVENT NOTIFICATION"},
            {DbLevelElement.Index,"INDEX"},
            {DbLevelElement.Parameter,"PARAMETER"},
            {DbLevelElement.Trigger,"TRIGGER"},
        };

        public static DbLevelElement GetItemScope(string? value)
        { return itemScope.FirstOrDefault(w => w.Value.Equals(value, KeyExtension.CompareString)).Key; }

        public static string? GetScope(this DbLevelElement value)
        {
            if (itemScope.ContainsKey(value)) { return itemScope[value]; }
            else { return null; }
        }
    }
    #endregion
}
