using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Toolbox.BindingTable;
using Toolbox.DbContext;
using Microsoft.Data.SqlClient;
using System.Collections.Concurrent;
using System.Diagnostics.Contracts;
using System.Reflection;
using DataDictionary.DataLayer.DomainData;
using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;

namespace DataDictionary.DataLayer.DatabaseData.ExtendedProperty
{
    /// <summary>
    /// Interface for MS SQL ExtendedProperty as stored in the Application Database.
    /// </summary>
    public interface IDbExtendedPropertyItem : IDbCatalogKeyUnique, IDbExtendedPropertyParameter, IBindingTableRow
    {
        /// <summary>
        /// MS SQL ExtendedProperty Level 0 Type
        /// </summary>
        DbCatalogScope CatalogScope { get; }

        /// <summary>
        /// MS SQL ExtendedProperty Level 1 Type
        /// </summary>
        DbObjectScope ObjectScope { get; }

        /// <summary>
        /// MS SQL ExtendedProperty Level 2 Type
        /// </summary>
        DbElementScope ElementScope { get; }

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
    public class DbExtendedPropertyItem : BindingTableRow, IDbExtendedPropertyItem, INotifyPropertyChanged
    {
        /// <inheritdoc/>
        public string? CatalogName { get { return GetValue("CatalogName"); } }

        /// <inheritdoc/>
        public string? Level0Type { get { return GetValue("Level0Type"); } }

        /// <inheritdoc/>
        public string? Level0Name { get { return GetValue("Level0Name"); } }

        /// <inheritdoc/>
        public DbCatalogScope CatalogScope { get { return ExtendedPropertyExtension.GetCatalogScope(Level0Type); } }

        /// <inheritdoc/>
        public string? Level1Type { get { return GetValue("Level1Type"); } }

        /// <inheritdoc/>
        public string? Level1Name { get { return GetValue("Level1Name"); } }

        /// <inheritdoc/>
        public DbObjectScope ObjectScope { get { return ExtendedPropertyExtension.GetObjectScope(Level1Type); } }

        /// <inheritdoc/>
        public string? Level2Type { get { return GetValue("Level2Type"); } }

        /// <inheritdoc/>
        public string? Level2Name { get { return GetValue("Level2Name"); } }

        /// <inheritdoc/>
        public DbElementScope ElementScope { get { return ExtendedPropertyExtension.GetItemScope(Level2Type); } }

        /// <inheritdoc/>
        public string? ObjectType { get { return GetValue("ObjType"); } }

        /// <inheritdoc/>
        public string? ObjectName { get { return GetValue("ObjName"); } }

        /// <inheritdoc/>
        public string? PropertyName { get { return GetValue("PropertyName"); } }

        /// <inheritdoc/>
        public string? PropertyValue { get { return GetValue("PropertyValue"); } }

        /// <summary>
        /// Constructor for DbExtendedPropertyItem.
        /// </summary>
        public DbExtendedPropertyItem() : base()
        { }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("CatalogName", typeof(string)){ AllowDBNull = false},
            new DataColumn("Level0Type", typeof(string)){ AllowDBNull = true},
            new DataColumn("Level0Name", typeof(string)){ AllowDBNull = true},
            new DataColumn("Level1Type", typeof(string)){ AllowDBNull = true},
            new DataColumn("Level1Name", typeof(string)){ AllowDBNull = true},
            new DataColumn("Level2Type", typeof(string)){ AllowDBNull = true},
            new DataColumn("Level2Name", typeof(string)){ AllowDBNull = true},

            new DataColumn("ObjType", typeof(string)){ AllowDBNull = false},
            new DataColumn("ObjName", typeof(string)){ AllowDBNull = false},
            new DataColumn("PropertyName", typeof(string)){ AllowDBNull = false},
            new DataColumn("PropertyValue", typeof(string)){ AllowDBNull = false},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }
    }

    /// <summary>
    /// Extension for DbExtendedPropertyItem. Needs to be re-factored and incorporated into the List class.
    /// </summary>
    public static class DbExtendedPropertyItemExtension
    { //TODO: These need to be work with BindingView. Return the Where clause not the result. For now it works.

        /// <summary>
        /// Gets the Extended Properties for the given Key.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<DbExtendedPropertyItem> GetProperties(this IDbSchemaKey item, IEnumerable<DbExtendedPropertyItem> source)
        {
            return source.Where(
                w => w.CatalogScope == DbCatalogScope.Schema &&
                w.ObjectScope is DbObjectScope.NULL &&
                w.ElementScope == DbElementScope.NULL &&
                item.CatalogName is not null && item.CatalogName.Equals(w.CatalogName, KeyExtension.CompareString) &&
                item.SchemaName is not null && item.SchemaName.Equals(w.Level0Name, KeyExtension.CompareString)
                );
        }

        /// <summary>
        /// Gets the Extended Properties for the given Key.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static IEnumerable<DbExtendedPropertyItem> GetProperties(this IEnumerable<DbExtendedPropertyItem> source, IDbSchemaKey item)
        {
            return source.Where(
                w => w.CatalogScope == DbCatalogScope.Schema &&
                w.ObjectScope is DbObjectScope.NULL &&
                w.ElementScope == DbElementScope.NULL &&
                item.CatalogName is not null && item.CatalogName.Equals(w.CatalogName, KeyExtension.CompareString) &&
                item.SchemaName is not null && item.SchemaName.Equals(w.Level0Name, KeyExtension.CompareString)
                );
        }

        /// <summary>
        /// Gets the Extended Properties for the given Key.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<DbExtendedPropertyItem> GetProperties(this IDbTableKey item, IEnumerable<DbExtendedPropertyItem> source)
        {
            return source.Where(
                w => w.CatalogScope == DbCatalogScope.Schema &&
                w.ObjectScope is DbObjectScope.Table or DbObjectScope.View &&
                w.ElementScope == DbElementScope.NULL &&
                item.CatalogName is not null && item.CatalogName.Equals(w.CatalogName, KeyExtension.CompareString) &&
                item.SchemaName is not null && item.SchemaName.Equals(w.Level0Name, KeyExtension.CompareString) &&
                item.TableName is not null && item.TableName.Equals(w.Level1Name, KeyExtension.CompareString)
                );
        }

        /// <summary>
        /// Gets the Extended Properties for the given Key.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static IEnumerable<DbExtendedPropertyItem> GetProperties(this IEnumerable<DbExtendedPropertyItem> source, IDbTableKey item)
        {
            return source.Where(
                w => w.CatalogScope == DbCatalogScope.Schema &&
                w.ObjectScope is DbObjectScope.Table or DbObjectScope.View &&
                w.ElementScope == DbElementScope.NULL &&
                item.CatalogName is not null && item.CatalogName.Equals(w.CatalogName, KeyExtension.CompareString) &&
                item.SchemaName is not null && item.SchemaName.Equals(w.Level0Name, KeyExtension.CompareString) &&
                item.TableName is not null && item.TableName.Equals(w.Level1Name, KeyExtension.CompareString)
                );
        }

        /// <summary>
        /// Gets the Extended Properties for the given Key.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<DbExtendedPropertyItem> GetProperties(this IDbConstraintKey item, IEnumerable<DbExtendedPropertyItem> source)
        {
            return source.Where(
                w => item.CatalogName is not null && item.CatalogName.Equals(w.CatalogName, KeyExtension.CompareString) &&
                item.SchemaName is not null && item.SchemaName.Equals(w.Level0Name, KeyExtension.CompareString) &&
                item.ConstraintName is not null && item.ConstraintName.Equals(w.Level2Name, KeyExtension.CompareString)
                );
        }

        /// <summary>
        /// Gets the Extended Properties for the given Key.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static IEnumerable<DbExtendedPropertyItem> GetProperties(this IEnumerable<DbExtendedPropertyItem> source, IDbConstraintKey item)
        {
            return source.Where(
                w => item.CatalogName is not null && item.CatalogName.Equals(w.CatalogName, KeyExtension.CompareString) &&
                item.SchemaName is not null && item.SchemaName.Equals(w.Level0Name, KeyExtension.CompareString) &&
                item.ConstraintName is not null && item.ConstraintName.Equals(w.Level2Name, KeyExtension.CompareString)
                );
        }

        /// <summary>
        /// Gets the Extended Properties for the given Key.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<DbExtendedPropertyItem> GetProperties(this IDbTableColumnKey item, IEnumerable<DbExtendedPropertyItem> source)
        {
            return source.Where(
                w => item.CatalogName is not null && item.CatalogName.Equals(w.CatalogName, KeyExtension.CompareString) &&
                item.SchemaName is not null && item.SchemaName.Equals(w.Level0Name, KeyExtension.CompareString) &&
                item.TableName is not null && item.TableName.Equals(w.Level1Name, KeyExtension.CompareString) &&
                item.ColumnName is not null && item.ColumnName.Equals(w.Level2Name, KeyExtension.CompareString)
                );
        }

        /// <summary>
        /// Gets the Extended Properties for the given Key.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static IEnumerable<DbExtendedPropertyItem> GetProperties(this IEnumerable<DbExtendedPropertyItem> source, IDbTableColumnKey item)
        {
            return source.Where(
                w => item.CatalogName is not null && item.CatalogName.Equals(w.CatalogName, KeyExtension.CompareString) &&
                item.SchemaName is not null && item.SchemaName.Equals(w.Level0Name, KeyExtension.CompareString) &&
                item.TableName is not null && item.TableName.Equals(w.Level1Name, KeyExtension.CompareString) &&
                item.ColumnName is not null && item.ColumnName.Equals(w.Level2Name, KeyExtension.CompareString)
                );
        }
    }

    #region Enum ExtendedProperty translation

    /// <summary>
    /// Helper class that translates the Enums listed above to string values returned by the Database.
    /// Source: https://learn.microsoft.com/en-us/sql/relational-databases/system-functions/sys-fn-listextendedproperty-transact-sql?view=sql-server-ver16
    /// </summary>
    static class ExtendedPropertyExtension
    {
        static Dictionary<DbCatalogScope, string> catalogScope = new Dictionary<DbCatalogScope, string>()
        {
            {DbCatalogScope.Assembly,"ASSEMBLY" },
            {DbCatalogScope.Contract,"CONTRACT"},
            {DbCatalogScope.EventNotification,"EVENT NOTIFICATION"},
            {DbCatalogScope.Filegroup,"FILEGROUP"},
            {DbCatalogScope.MessageType,"MESSAGE TYPE"},
            {DbCatalogScope.PartitionFunction,"PARTITION FUNCTION"},
            {DbCatalogScope.PartitionScheme,"PARTITION SCHEME"},
            {DbCatalogScope.RemoteServiceBinding,"REMOTE SERVICE BINDING"},
            {DbCatalogScope.Route,"ROUTE"},
            {DbCatalogScope.Schema,"SCHEMA"},
            {DbCatalogScope.Service,"SERVICE"},
            {DbCatalogScope.Trigger,"TRIGGER"},
            {DbCatalogScope.Type,"TYPE"},
            {DbCatalogScope.User,"USER"},
        };

        public static DbCatalogScope GetCatalogScope(string? value)
        { return catalogScope.FirstOrDefault(w => w.Value.Equals(value, KeyExtension.CompareString)).Key; }

        public static string GetScope(this DbCatalogScope value)
        {
            if (catalogScope.ContainsKey(value)) { return catalogScope[value]; }
            else { return string.Empty; }
        }

        static Dictionary<DbObjectScope, string> objectScope = new Dictionary<DbObjectScope, string>()
        {
            {DbObjectScope.Aggregate,"AGGREGATE"},
            {DbObjectScope.Default,"DEFAULT"},
            {DbObjectScope.Function,"FUNCTION"},
            {DbObjectScope.LogicalFileName,"LOGICAL FILE NAME"},
            {DbObjectScope.Procedure,"PROCEDURE"},
            {DbObjectScope.Queue,"QUEUE"},
            {DbObjectScope.Rule,"RULE"},
            {DbObjectScope.Synonym,"SYNONYM"},
            {DbObjectScope.Table,"TABLE"},
            {DbObjectScope.Type,"TYPE"},
            {DbObjectScope.View,"VIEW"},
            {DbObjectScope.XmlSchemaCollection,"XML SCHEMA COLLECTION"},
        };

        public static DbObjectScope GetObjectScope(string? value)
        { return objectScope.FirstOrDefault(w => w.Value.Equals(value, KeyExtension.CompareString)).Key; }

        public static string GetScope(this DbObjectScope value)
        {
            if (objectScope.ContainsKey(value)) { return objectScope[value]; }
            else { return string.Empty; }
        }

        static Dictionary<DbElementScope, string> itemScope = new Dictionary<DbElementScope, string>()
        {
            {DbElementScope.Default,"DEFAULT"},
            {DbElementScope.Column,"COLUMN"},
            {DbElementScope.Constraint,"CONSTRAINT"},
            {DbElementScope.EventNotification,"EVENT NOTIFICATION"},
            {DbElementScope.Index,"INDEX"},
            {DbElementScope.Parameter,"PARAMETER"},
            {DbElementScope.Trigger,"TRIGGER"},
        };

        public static DbElementScope GetItemScope(string? value)
        { return itemScope.FirstOrDefault(w => w.Value.Equals(value, KeyExtension.CompareString)).Key; }

        public static string? GetScope(this DbElementScope value)
        {
            if (itemScope.ContainsKey(value)) { return itemScope[value]; }
            else { return null; }
        }
    }
    #endregion
}
