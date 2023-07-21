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

namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbExtendedPropertyItem : IDbCatalogName
    {
        String? Level0Type { get; }
        String? Level0Name { get; }
        String? Level1Type { get; }
        String? Level1Name { get; }
        String? Level2Type { get; }
        String? Level2Name { get; }
        ExtendedPropertyCatalogScope CatalogScope { get; }
        ExtendedPropertyObjectScope ObjectScope { get; }
        ExtendedPropertyElementScope ElementScope { get; }
        String? ObjectType { get; }
        String? ObjectName { get; }
        //ExtendedPropertyObjectType PropertyObjectType { get; }
        String? PropertyName { get; }
        String? PropertyValue { get; }
    }

    public interface IDbExtendedProperties
    {  // DB Classes that have extended properties.
        Command GetProperties(IConnection connection);
    }

    internal class DbExtendedPropertyGetCommand
    {
        public String? PropertyName { get; set; } // Null will return all extended properties
        public String? Level0Type { get; set; }
        public String? Level0Name { get; set; } // Null will return all objects of Level0 matching the Type
        public String? Level1Type { get; set; }
        public String? Level1Name { get; set; } // Null will return all objects of Level1 matching the Type and of Level0 Name
        public String? Level2Type { get; set; }
        public String? Level2Name { get; set; } // Null will return all objects of Level2 matching the Type and of Level0 & Level1 Name

        public ExtendedPropertyCatalogScope CatalogScope
        {
            get { return ExtendedPropertyExtension.GetCatalogScope(Level0Type); }
            set { Level0Type = value.GetScope(); }
        }

        public ExtendedPropertyObjectScope ObjectScope
        {
            get { return ExtendedPropertyExtension.GetObjectScope(Level1Type); }
            set { Level1Type = value.GetScope(); }
        }

        public ExtendedPropertyElementScope ItemScope
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
        public ExtendedPropertyCatalogScope CatalogScope { get { return ExtendedPropertyExtension.GetCatalogScope(Level0Type); } }

        public String? Level1Type { get { return GetValue("Level1Type"); } }
        public String? Level1Name { get { return GetValue("Level1Name"); } }
        public ExtendedPropertyObjectScope ObjectScope { get { return ExtendedPropertyExtension.GetObjectScope(Level1Type); } }

        public String? Level2Type { get { return GetValue("Level2Type"); } }
        public String? Level2Name { get { return GetValue("Level2Name"); } }
        public ExtendedPropertyElementScope ElementScope { get { return ExtendedPropertyExtension.GetItemScope(Level2Type); } }

        public String? ObjectType { get { return GetValue("objtype"); } }
        public String? ObjectName { get { return GetValue("objname"); } }

        //public ExtendedPropertyObjectType PropertyObjectType { get { return ExtendedPropertyExtension.GetPropertyType(ObjectType); } }
        public String? PropertyName { get { return GetValue("name"); } }
        public String? PropertyValue { get { return GetValue("value"); } }

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

            new DataColumn("objtype", typeof(String)){ AllowDBNull = false},
            new DataColumn("objname", typeof(String)){ AllowDBNull = false},
            new DataColumn("name", typeof(String)){ AllowDBNull = false},
            new DataColumn("value", typeof(String)){ AllowDBNull = false},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }
    }

    public static class DbExtendedPropertyItemExtension
    {
        public static IEnumerable<DbExtendedPropertyItem> GetProperties(this IDbSchemaName item, IEnumerable<DbExtendedPropertyItem> source)
        {
            return source.Where(
                w => w.CatalogScope == ExtendedPropertyCatalogScope.Schema &&
                w.ObjectScope is ExtendedPropertyObjectScope.NULL &&
                w.ElementScope == ExtendedPropertyElementScope.NULL &&
                item.CatalogName is not null && item.CatalogName.Equals(w.CatalogName, ModelFactory.CompareString) &&
                item.SchemaName is not null && item.SchemaName.Equals(w.Level0Name, ModelFactory.CompareString)
                );
        }

        public static IEnumerable<DbExtendedPropertyItem> GetProperties(this IEnumerable<DbExtendedPropertyItem> source, IDbSchemaName item)
        {
            return source.Where(
                w => w.CatalogScope == ExtendedPropertyCatalogScope.Schema &&
                w.ObjectScope is ExtendedPropertyObjectScope.NULL &&
                w.ElementScope == ExtendedPropertyElementScope.NULL &&
                item.CatalogName is not null && item.CatalogName.Equals(w.CatalogName, ModelFactory.CompareString) &&
                item.SchemaName is not null && item.SchemaName.Equals(w.Level0Name, ModelFactory.CompareString)
                );
        }

        public static IEnumerable<DbExtendedPropertyItem> GetProperties(this IDbTableName item, IEnumerable<DbExtendedPropertyItem> source)
        {
            return source.Where(
                w => w.CatalogScope == ExtendedPropertyCatalogScope.Schema &&
                w.ObjectScope is ExtendedPropertyObjectScope.Table or ExtendedPropertyObjectScope.View &&
                w.ElementScope == ExtendedPropertyElementScope.NULL &&
                item.CatalogName is not null && item.CatalogName.Equals(w.CatalogName, ModelFactory.CompareString) &&
                item.SchemaName is not null && item.SchemaName.Equals(w.Level0Name, ModelFactory.CompareString) &&
                item.TableName is not null && item.TableName.Equals(w.Level1Name, ModelFactory.CompareString)
                );
        }

        public static IEnumerable<DbExtendedPropertyItem> GetProperties(this IEnumerable<DbExtendedPropertyItem> source, IDbTableName item)
        {
            return source.Where(
                w => w.CatalogScope == ExtendedPropertyCatalogScope.Schema &&
                w.ObjectScope is ExtendedPropertyObjectScope.Table or ExtendedPropertyObjectScope.View &&
                w.ElementScope == ExtendedPropertyElementScope.NULL &&
                item.CatalogName is not null && item.CatalogName.Equals(w.CatalogName, ModelFactory.CompareString) &&
                item.SchemaName is not null && item.SchemaName.Equals(w.Level0Name, ModelFactory.CompareString) &&
                item.TableName is not null && item.TableName.Equals(w.Level1Name, ModelFactory.CompareString)
                );
        }

        public static IEnumerable<DbExtendedPropertyItem> GetProperties(this IDbColumnName item, IEnumerable<DbExtendedPropertyItem> source)
        {
            return source.Where(
                w => w.CatalogScope == ExtendedPropertyCatalogScope.Schema &&
                w.ObjectScope is ExtendedPropertyObjectScope.Table or ExtendedPropertyObjectScope.View &&
                w.ElementScope == ExtendedPropertyElementScope.Column &&
                item.CatalogName is not null && item.CatalogName.Equals(w.CatalogName, ModelFactory.CompareString) &&
                item.SchemaName is not null && item.SchemaName.Equals(w.Level0Name, ModelFactory.CompareString) &&
                item.TableName is not null && item.TableName.Equals(w.Level1Name, ModelFactory.CompareString) &&
                item.ColumnName is not null && item.ColumnName.Equals(w.Level2Name, ModelFactory.CompareString)
                );
        }

        public static IEnumerable<DbExtendedPropertyItem> GetProperties(this IEnumerable<DbExtendedPropertyItem> source, IDbColumnName item)
        {
            return source.Where(
                w => w.CatalogScope == ExtendedPropertyCatalogScope.Schema &&
                w.ObjectScope is ExtendedPropertyObjectScope.Table or ExtendedPropertyObjectScope.View &&
                w.ElementScope == ExtendedPropertyElementScope.Column &&
                item.CatalogName is not null && item.CatalogName.Equals(w.CatalogName, ModelFactory.CompareString) &&
                item.SchemaName is not null && item.SchemaName.Equals(w.Level0Name, ModelFactory.CompareString) &&
                item.TableName is not null && item.TableName.Equals(w.Level1Name, ModelFactory.CompareString) &&
                item.ColumnName is not null && item.ColumnName.Equals(w.Level2Name, ModelFactory.CompareString)
                );
        }
    }

    #region Enum ExtendedProperty translation
    public enum ExtendedPropertyCatalogScope
    {
        NULL,
        Assembly,
        Contract,
        EventNotification,
        Filegroup,
        MessageType,
        PartitionFunction,
        PartitionScheme,
        RemoteServiceBinding,
        Route,
        Schema,
        Service,
        Trigger,
        Type,
        User,
    }

    public enum ExtendedPropertyObjectScope
    {
        NULL,
        Aggregate,
        Default,
        Function,
        LogicalFileName,
        Procedure,
        Queue,
        Rule,
        Synonym,
        Table,
        Type,
        View,
        XmlSchemaCollection,
    }

    public enum ExtendedPropertyElementScope
    {
        NULL,
        Default,
        Column,
        Constraint,
        EventNotification,
        Index,
        Parameter,
        Trigger,
    }

    /// <summary>
    /// Helper class that translates the Enums listed above to string values returned by the Database.
    /// Source: https://learn.microsoft.com/en-us/sql/relational-databases/system-functions/sys-fn-listextendedproperty-transact-sql?view=sql-server-ver16
    /// </summary>
    static class ExtendedPropertyExtension
    {
        static Dictionary<ExtendedPropertyCatalogScope, String> catalogScope = new Dictionary<ExtendedPropertyCatalogScope, String>()
        {
            {ExtendedPropertyCatalogScope.Assembly,"ASSEMBLY" },
            {ExtendedPropertyCatalogScope.Contract,"CONTRACT"},
            {ExtendedPropertyCatalogScope.EventNotification,"EVENT NOTIFICATION"},
            {ExtendedPropertyCatalogScope.Filegroup,"FILEGROUP"},
            {ExtendedPropertyCatalogScope.MessageType,"MESSAGE TYPE"},
            {ExtendedPropertyCatalogScope.PartitionFunction,"PARTITION FUNCTION"},
            {ExtendedPropertyCatalogScope.PartitionScheme,"PARTITION SCHEME"},
            {ExtendedPropertyCatalogScope.RemoteServiceBinding,"REMOTE SERVICE BINDING"},
            {ExtendedPropertyCatalogScope.Route,"ROUTE"},
            {ExtendedPropertyCatalogScope.Schema,"SCHEMA"},
            {ExtendedPropertyCatalogScope.Service,"SERVICE"},
            {ExtendedPropertyCatalogScope.Trigger,"TRIGGER"},
            {ExtendedPropertyCatalogScope.Type,"TYPE"},
            {ExtendedPropertyCatalogScope.User,"USER"},
        };

        public static ExtendedPropertyCatalogScope GetCatalogScope(String? value)
        { return catalogScope.FirstOrDefault(w => w.Value.Equals(value, ModelFactory.CompareString)).Key; }

        public static String GetScope(this ExtendedPropertyCatalogScope value)
        {
            if (catalogScope.ContainsKey(value)) { return catalogScope[value]; }
            else { return String.Empty; }
        }

        static Dictionary<ExtendedPropertyObjectScope, String> objectScope = new Dictionary<ExtendedPropertyObjectScope, String>()
        {
            {ExtendedPropertyObjectScope.Aggregate,"AGGREGATE"},
            {ExtendedPropertyObjectScope.Default,"DEFAULT"},
            {ExtendedPropertyObjectScope.Function,"FUNCTION"},
            {ExtendedPropertyObjectScope.LogicalFileName,"LOGICAL FILE NAME"},
            {ExtendedPropertyObjectScope.Procedure,"PROCEDURE"},
            {ExtendedPropertyObjectScope.Queue,"QUEUE"},
            {ExtendedPropertyObjectScope.Rule,"RULE"},
            {ExtendedPropertyObjectScope.Synonym,"SYNONYM"},
            {ExtendedPropertyObjectScope.Table,"TABLE"},
            {ExtendedPropertyObjectScope.Type,"TYPE"},
            {ExtendedPropertyObjectScope.View,"VIEW"},
            {ExtendedPropertyObjectScope.XmlSchemaCollection,"XML SCHEMA COLLECTION"},
        };

        public static ExtendedPropertyObjectScope GetObjectScope(String? value)
        { return objectScope.FirstOrDefault(w => w.Value.Equals(value, ModelFactory.CompareString)).Key; }

        public static String GetScope(this ExtendedPropertyObjectScope value)
        {
            if (objectScope.ContainsKey(value)) { return objectScope[value]; }
            else { return String.Empty; }
        }

        static Dictionary<ExtendedPropertyElementScope, String> itemScope = new Dictionary<ExtendedPropertyElementScope, String>()
        {
            {ExtendedPropertyElementScope.Default,"DEFAULT"},
            {ExtendedPropertyElementScope.Column,"COLUMN"},
            {ExtendedPropertyElementScope.Constraint,"CONSTRAINT"},
            {ExtendedPropertyElementScope.EventNotification,"EVENT NOTIFICATION"},
            {ExtendedPropertyElementScope.Index,"INDEX"},
            {ExtendedPropertyElementScope.Parameter,"PARAMETER"},
            {ExtendedPropertyElementScope.Trigger,"TRIGGER"},
        };

        public static ExtendedPropertyElementScope GetItemScope(String? value)
        { return itemScope.FirstOrDefault(w => w.Value.Equals(value, ModelFactory.CompareString)).Key; }

        public static String? GetScope(this ExtendedPropertyElementScope value)
        {
            if (itemScope.ContainsKey(value)) { return itemScope[value]; }
            else { return null; }
        }
    }
    #endregion
}
