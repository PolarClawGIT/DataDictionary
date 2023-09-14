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

namespace DataDictionary.DataLayer.DatabaseData
{
    public interface IDbExtendedPropertyParameter
    {
        String? Level0Type { get; }
        String? Level0Name { get; }
        String? Level1Type { get; }
        String? Level1Name { get; }
        String? Level2Type { get; }
        String? Level2Name { get; }
        String? PropertyName { get; }
        String? PropertyValue { get; }
    }

    public interface IDbExtendedPropertyItem : IDbCatalogKeyUnique, IDbExtendedPropertyParameter, IBindingTableRow
    {

        DbCatalogScope CatalogScope { get; }
        DbObjectScope ObjectScope { get; }
        DbElementScope ElementScope { get; }
        String? ObjectType { get; }
        String? ObjectName { get; }
        //ExtendedPropertyObjectType PropertyObjectType { get; }
    }

    public interface IDbExtendedProperties
    {  // DB Classes that have extended properties.
        Command GetProperties(IConnection connection);
    }

    public class DbExtendedPropertyParameter : IDbExtendedPropertyParameter
    {
        public String? PropertyName { get; set; } // Null will return all extended properties
        public String? PropertyValue { get; set; } // Used in Set
        public String? Level0Type { get; set; }
        public String? Level0Name { get; set; } // Null will return all objects of Level0 matching the Type
        public String? Level1Type { get; set; }
        public String? Level1Name { get; set; } // Null will return all objects of Level1 matching the Type and of Level0 Name
        public String? Level2Type { get; set; }
        public String? Level2Name { get; set; } // Null will return all objects of Level2 matching the Type and of Level0 & Level1 Name

    }

    internal class DbExtendedPropertyGetCommand: DbExtendedPropertyParameter
    {
        public DbCatalogScope CatalogScope
        {
            get { return ExtendedPropertyExtension.GetCatalogScope(Level0Type); }
            set { Level0Type = value.GetScope(); }
        }

        public DbObjectScope ObjectScope
        {
            get { return ExtendedPropertyExtension.GetObjectScope(Level1Type); }
            set { Level1Type = value.GetScope(); }
        }

        public DbElementScope ItemScope
        {
            get { return ExtendedPropertyExtension.GetItemScope(Level2Type); }
            set { Level2Type = value.GetScope(); }
        }

        readonly Command command;

        public DbExtendedPropertyGetCommand(IConnection connection) : base()
        { command = connection.CreateCommand(); }

        public Command GetCommand()
        {
            // There appears to be a bug in Microsoft Code that can cause the parameters to be incorrectly setup when building the SQL statement for the parameters.
            // This appears to be avoid when the parameter is setup with a defined type and length.
            // There is also no way to pass the parameter "Default". For this function, passing NULL is appropriate but this is not always the case.
            command.CommandText = DbScript.DbExtendedPropertyItem;
            command.Parameters.Add(new SqlParameter("@PropertyName", SqlDbType.VarChar, 210));
            command.Parameters.Add(new SqlParameter("@Level0Type", SqlDbType.VarChar, 210));
            command.Parameters.Add(new SqlParameter("@Level0Name", SqlDbType.VarChar, 210));
            command.Parameters.Add(new SqlParameter("@Level1Type", SqlDbType.VarChar, 210));
            command.Parameters.Add(new SqlParameter("@Level1Name", SqlDbType.VarChar, 210));
            command.Parameters.Add(new SqlParameter("@Level2Type", SqlDbType.VarChar, 210));
            command.Parameters.Add(new SqlParameter("@Level2Name", SqlDbType.VarChar, 210));

            command.Parameters["@PropertyName"].Value = PropertyName is null ? DBNull.Value : PropertyName;
            command.Parameters["@Level0Type"].Value = Level0Type is null ? DBNull.Value : Level0Type;
            command.Parameters["@Level0Name"].Value = Level0Name is null ? DBNull.Value : Level0Name;
            command.Parameters["@Level1Type"].Value = Level1Type is null ? DBNull.Value : Level1Type;
            command.Parameters["@Level1Name"].Value = Level1Name is null ? DBNull.Value : Level1Name;
            command.Parameters["@Level2Type"].Value = Level2Type is null ? DBNull.Value : Level2Type;
            command.Parameters["@Level2Name"].Value = Level2Name is null ? DBNull.Value : Level2Name;

            return command;
        }
    }

    public class DbExtendedPropertyItem : BindingTableRow, IDbExtendedPropertyItem, INotifyPropertyChanged
    {
        public String? CatalogName { get { return GetValue("CatalogName"); } }
        public String? Level0Type { get { return GetValue("Level0Type"); } }
        public String? Level0Name { get { return GetValue("Level0Name"); } }
        public DbCatalogScope CatalogScope { get { return ExtendedPropertyExtension.GetCatalogScope(Level0Type); } }

        public String? Level1Type { get { return GetValue("Level1Type"); } }
        public String? Level1Name { get { return GetValue("Level1Name"); } }
        public DbObjectScope ObjectScope { get { return ExtendedPropertyExtension.GetObjectScope(Level1Type); } }

        public String? Level2Type { get { return GetValue("Level2Type"); } }
        public String? Level2Name { get { return GetValue("Level2Name"); } }
        public DbElementScope ElementScope { get { return ExtendedPropertyExtension.GetItemScope(Level2Type); } }

        public String? ObjectType { get { return GetValue("ObjType"); } }
        public String? ObjectName { get { return GetValue("ObjName"); } }

        //public ExtendedPropertyObjectType PropertyObjectType { get { return ExtendedPropertyExtension.GetPropertyType(ObjectType); } }
        public String? PropertyName { get { return GetValue("PropertyName"); } }
        public String? PropertyValue { get { return GetValue("PropertyValue"); } }

        public DbExtendedPropertyItem() : base()
        {  }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("CatalogName", typeof(String)){ AllowDBNull = false},
            new DataColumn("Level0Type", typeof(String)){ AllowDBNull = true},
            new DataColumn("Level0Name", typeof(String)){ AllowDBNull = true},
            new DataColumn("Level1Type", typeof(String)){ AllowDBNull = true},
            new DataColumn("Level1Name", typeof(String)){ AllowDBNull = true},
            new DataColumn("Level2Type", typeof(String)){ AllowDBNull = true},
            new DataColumn("Level2Name", typeof(String)){ AllowDBNull = true},

            new DataColumn("ObjType", typeof(String)){ AllowDBNull = false},
            new DataColumn("ObjName", typeof(String)){ AllowDBNull = false},
            new DataColumn("PropertyName", typeof(String)){ AllowDBNull = false},
            new DataColumn("PropertyValue", typeof(String)){ AllowDBNull = false},
        };
        //[ObjType][ObjName][PropertyName][PropertyValue]
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static Command GetData(IConnection connection, IModelKey modelId)
        { return GetData(connection, (modelId.ModelId, null, null)); }

        static Command GetData(IConnection connection, (Guid? modelId, Guid? propertyId, String? catalogName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDatabaseExtendedProperty]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@PropertyId", parameters.propertyId);
            command.AddParameter("@CatalogName", parameters.catalogName);
            return command;
        }

        public static Command SetData(IConnection connection, IModelKey modelId, IBindingTable<DbExtendedPropertyItem> source)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDatabaseExtendedProperty]";
            command.AddParameter("@ModelId", modelId.ModelId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDatabaseExtendedProperty]", source);
            return command;
        }

    }

    public static class DbExtendedPropertyItemExtension
    { //TODO: These need to be work with BindingView. Return the Where clause not the result. For now it works.
        public static IEnumerable<DbExtendedPropertyItem> GetProperties(this IDbSchemaKey item, IEnumerable<DbExtendedPropertyItem> source)
        {
            return source.Where(
                w => w.CatalogScope == DbCatalogScope.Schema &&
                w.ObjectScope is DbObjectScope.NULL &&
                w.ElementScope == DbElementScope.NULL &&
                item.CatalogName is not null && item.CatalogName.Equals(w.CatalogName, ModelFactory.CompareString) &&
                item.SchemaName is not null && item.SchemaName.Equals(w.Level0Name, ModelFactory.CompareString)
                );
        }

        public static IEnumerable<DbExtendedPropertyItem> GetProperties(this IEnumerable<DbExtendedPropertyItem> source, IDbSchemaKey item)
        {
            return source.Where(
                w => w.CatalogScope == DbCatalogScope.Schema &&
                w.ObjectScope is DbObjectScope.NULL &&
                w.ElementScope == DbElementScope.NULL &&
                item.CatalogName is not null && item.CatalogName.Equals(w.CatalogName, ModelFactory.CompareString) &&
                item.SchemaName is not null && item.SchemaName.Equals(w.Level0Name, ModelFactory.CompareString)
                );
        }

        public static IEnumerable<DbExtendedPropertyItem> GetProperties(this IDbTableKey item, IEnumerable<DbExtendedPropertyItem> source)
        {
            return source.Where(
                w => w.CatalogScope == DbCatalogScope.Schema &&
                w.ObjectScope is DbObjectScope.Table or DbObjectScope.View &&
                w.ElementScope == DbElementScope.NULL &&
                item.CatalogName is not null && item.CatalogName.Equals(w.CatalogName, ModelFactory.CompareString) &&
                item.SchemaName is not null && item.SchemaName.Equals(w.Level0Name, ModelFactory.CompareString) &&
                item.TableName is not null && item.TableName.Equals(w.Level1Name, ModelFactory.CompareString)
                );
        }

        public static IEnumerable<DbExtendedPropertyItem> GetProperties(this IEnumerable<DbExtendedPropertyItem> source, IDbTableKey item)
        {
            return source.Where(
                w => w.CatalogScope == DbCatalogScope.Schema &&
                w.ObjectScope is DbObjectScope.Table or DbObjectScope.View &&
                w.ElementScope == DbElementScope.NULL &&
                item.CatalogName is not null && item.CatalogName.Equals(w.CatalogName, ModelFactory.CompareString) &&
                item.SchemaName is not null && item.SchemaName.Equals(w.Level0Name, ModelFactory.CompareString) &&
                item.TableName is not null && item.TableName.Equals(w.Level1Name, ModelFactory.CompareString)
                );
        }

        public static IEnumerable<DbExtendedPropertyItem> GetProperties(this IDbConstraintKey item, IEnumerable<DbExtendedPropertyItem> source)
        {
            return source.Where(
                w =>  item.CatalogName is not null && item.CatalogName.Equals(w.CatalogName, ModelFactory.CompareString) &&
                item.SchemaName is not null && item.SchemaName.Equals(w.Level0Name, ModelFactory.CompareString) &&
                item.ConstraintName is not null && item.ConstraintName.Equals(w.Level2Name, ModelFactory.CompareString)
                );
        }

        public static IEnumerable<DbExtendedPropertyItem> GetProperties(this IEnumerable<DbExtendedPropertyItem> source, IDbConstraintKey item)
        {
            return source.Where(
                w => item.CatalogName is not null && item.CatalogName.Equals(w.CatalogName, ModelFactory.CompareString) &&
                item.SchemaName is not null && item.SchemaName.Equals(w.Level0Name, ModelFactory.CompareString) &&
                item.ConstraintName is not null && item.ConstraintName.Equals(w.Level2Name, ModelFactory.CompareString)
                );
        }

        public static IEnumerable<DbExtendedPropertyItem> GetProperties(this IDbTableColumnKey item, IEnumerable<DbExtendedPropertyItem> source)
        {
            return source.Where(
                w => item.CatalogName is not null && item.CatalogName.Equals(w.CatalogName, ModelFactory.CompareString) &&
                item.SchemaName is not null && item.SchemaName.Equals(w.Level0Name, ModelFactory.CompareString) &&
                item.TableName is not null && item.TableName.Equals(w.Level1Name, ModelFactory.CompareString) &&
                item.ColumnName is not null && item.ColumnName.Equals(w.Level2Name, ModelFactory.CompareString)
                );
        }

        public static IEnumerable<DbExtendedPropertyItem> GetProperties(this IEnumerable<DbExtendedPropertyItem> source, IDbTableColumnKey item)
        {
            return source.Where(
                w => item.CatalogName is not null && item.CatalogName.Equals(w.CatalogName, ModelFactory.CompareString) &&
                item.SchemaName is not null && item.SchemaName.Equals(w.Level0Name, ModelFactory.CompareString) &&
                item.TableName is not null && item.TableName.Equals(w.Level1Name, ModelFactory.CompareString) &&
                item.ColumnName is not null && item.ColumnName.Equals(w.Level2Name, ModelFactory.CompareString)
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
        static Dictionary<DbCatalogScope, String> catalogScope = new Dictionary<DbCatalogScope, String>()
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

        public static DbCatalogScope GetCatalogScope(String? value)
        { return catalogScope.FirstOrDefault(w => w.Value.Equals(value, ModelFactory.CompareString)).Key; }

        public static String GetScope(this DbCatalogScope value)
        {
            if (catalogScope.ContainsKey(value)) { return catalogScope[value]; }
            else { return String.Empty; }
        }

        static Dictionary<DbObjectScope, String> objectScope = new Dictionary<DbObjectScope, String>()
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

        public static DbObjectScope GetObjectScope(String? value)
        { return objectScope.FirstOrDefault(w => w.Value.Equals(value, ModelFactory.CompareString)).Key; }

        public static String GetScope(this DbObjectScope value)
        {
            if (objectScope.ContainsKey(value)) { return objectScope[value]; }
            else { return String.Empty; }
        }

        static Dictionary<DbElementScope, String> itemScope = new Dictionary<DbElementScope, String>()
        {
            {DbElementScope.Default,"DEFAULT"},
            {DbElementScope.Column,"COLUMN"},
            {DbElementScope.Constraint,"CONSTRAINT"},
            {DbElementScope.EventNotification,"EVENT NOTIFICATION"},
            {DbElementScope.Index,"INDEX"},
            {DbElementScope.Parameter,"PARAMETER"},
            {DbElementScope.Trigger,"TRIGGER"},
        };

        public static DbElementScope GetItemScope(String? value)
        { return itemScope.FirstOrDefault(w => w.Value.Equals(value, ModelFactory.CompareString)).Key; }

        public static String? GetScope(this DbElementScope value)
        {
            if (itemScope.ContainsKey(value)) { return itemScope[value]; }
            else { return null; }
        }
    }
    #endregion
}
