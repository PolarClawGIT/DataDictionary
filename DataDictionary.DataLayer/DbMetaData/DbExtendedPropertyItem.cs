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
        ExtendedPropertyItemScope ItemScope { get; }
        String? ObjectType { get; }
        String? ObjectName { get; }
        ExtendedPropertyObjectType PropertyObjectType { get; }
        String? PropertyName { get; }
        String? PropertyValue { get; }
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

        public ExtendedPropertyItemScope ItemScope
        {
            get { return ExtendedPropertyExtension.GetItemScope(Level2Type); }
            set { Level2Type = value.GetScope(); }
        }

        readonly SqlCommand command;

        public DbExtendedPropertyGetCommand(IConnection connection) : base()
        { command = connection.CreateCommand(); }

        public SqlCommand GetCommand()
        {
            // There appears to be a bug in Microsoft Code that can cause the paramters to be incorrectly setup when building the SQL statement for the parameters.
            // This appears to be avoid when the paramter is setup with a defined type and length.
            // There is also no way to pass the parameter "Default". For this function, passing NULL is apporate but this is not always the case.
            command.CommandText = "SELECT Db_Name() [CatalogName], @Level0Type [Level0Type], @Level0Name [Level0Name], @Level1Type [Level1Type], @Level1Name [Level1Name], @Level2Type [Level2Type], @Level2Name [Level2Name], [objtype], [objname], [name], [value] FROM [fn_listextendedproperty](@PropertyName, @Level0Type, @Level0Name, @Level1Type, @Level1Name, @Level2Type, @Level2Name)";
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
        public ExtendedPropertyItemScope ItemScope { get { return ExtendedPropertyExtension.GetItemScope(Level2Type); } }

        public String? ObjectType { get { return GetValue("objtype"); } }
        public String? ObjectName { get { return GetValue("objname"); } }

        public ExtendedPropertyObjectType PropertyObjectType { get { return ExtendedPropertyExtension.GetPropertyType(ObjectType); } }
        public String? PropertyName { get { return GetValue("name"); } }
        public String? PropertyValue { get { return GetValue("value"); } }

        public override IReadOnlyList<DataColumn> ColumnDefinitions { get; } = new List<DataColumn>()
        {
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
    }

    #region Enum ExtendedProperty tranlation
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

    public enum ExtendedPropertyItemScope
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

    public enum ExtendedPropertyObjectType
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
        Aggregate,
        Default,
        Function,
        LogicalFileName,
        Procedure,
        Queue,
        Rule,
        Synonym,
        Table,
        View,
        XmlSchemaCollection,
        Column,
        Constraint,
        Index,
        Parameter,
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
        { return catalogScope.FirstOrDefault(w => w.Value.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Key; }

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
        { return objectScope.FirstOrDefault(w => w.Value.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Key; }

        public static String GetScope(this ExtendedPropertyObjectScope value)
        {
            if (objectScope.ContainsKey(value)) { return objectScope[value]; }
            else { return String.Empty; }
        }

        static Dictionary<ExtendedPropertyItemScope, String> itemScope = new Dictionary<ExtendedPropertyItemScope, String>()
        {
            {ExtendedPropertyItemScope.Default,"DEFAULT"},
            {ExtendedPropertyItemScope.Column,"COLUMN"},
            {ExtendedPropertyItemScope.Constraint,"CONSTRAINT"},
            {ExtendedPropertyItemScope.EventNotification,"EVENT NOTIFICATION"},
            {ExtendedPropertyItemScope.Index,"INDEX"},
            {ExtendedPropertyItemScope.Parameter,"PARAMETER"},
            {ExtendedPropertyItemScope.Trigger,"TRIGGER"},
        };

        public static ExtendedPropertyItemScope GetItemScope(String? value)
        { return itemScope.FirstOrDefault(w => w.Value.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Key; }

        public static String? GetScope(this ExtendedPropertyItemScope value)
        {
            if (itemScope.ContainsKey(value)) { return itemScope[value]; }
            else { return null; }
        }

        static Dictionary<ExtendedPropertyObjectType, String> itemType = new Dictionary<ExtendedPropertyObjectType, String>()
        {
            {ExtendedPropertyObjectType.Assembly,"ASSEMBLY" },
            {ExtendedPropertyObjectType.Contract,"CONTRACT"},
            {ExtendedPropertyObjectType.EventNotification,"EVENT NOTIFICATION"},
            {ExtendedPropertyObjectType.Filegroup,"FILEGROUP"},
            {ExtendedPropertyObjectType.MessageType,"MESSAGE TYPE"},
            {ExtendedPropertyObjectType.PartitionFunction,"PARTITION FUNCTION"},
            {ExtendedPropertyObjectType.PartitionScheme,"PARTITION SCHEME"},
            {ExtendedPropertyObjectType.RemoteServiceBinding,"REMOTE SERVICE BINDING"},
            {ExtendedPropertyObjectType.Route,"ROUTE"},
            {ExtendedPropertyObjectType.Schema,"SCHEMA"},
            {ExtendedPropertyObjectType.Service,"SERVICE"},
            {ExtendedPropertyObjectType.Trigger,"TRIGGER"},
            {ExtendedPropertyObjectType.Type,"TYPE"},
            {ExtendedPropertyObjectType.User,"USER"},
            {ExtendedPropertyObjectType.Aggregate,"AGGREGATE"},
            {ExtendedPropertyObjectType.Default,"DEFAULT"},
            {ExtendedPropertyObjectType.Function,"FUNCTION"},
            {ExtendedPropertyObjectType.LogicalFileName,"LOGICAL FILE NAME"},
            {ExtendedPropertyObjectType.Procedure,"PROCEDURE"},
            {ExtendedPropertyObjectType.Queue,"QUEUE"},
            {ExtendedPropertyObjectType.Rule,"RULE"},
            {ExtendedPropertyObjectType.Synonym,"SYNONYM"},
            {ExtendedPropertyObjectType.Table,"TABLE"},
            {ExtendedPropertyObjectType.View,"VIEW"},
            {ExtendedPropertyObjectType.XmlSchemaCollection,"XML SCHEMA COLLECTION"},
            {ExtendedPropertyObjectType.Column,"COLUMN"},
            {ExtendedPropertyObjectType.Constraint,"CONSTRAINT"},
            {ExtendedPropertyObjectType.Index,"INDEX"},
            {ExtendedPropertyObjectType.Parameter,"PARAMETER"},
        };

        public static ExtendedPropertyObjectType GetPropertyType(String? value)
        { return itemType.FirstOrDefault(w => w.Value.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Key; }

        public static String? GetScope(this ExtendedPropertyObjectType value)
        {
            if (itemType.ContainsKey(value)) { return itemType[value]; }
            else { return null; }
        }

    }
    #endregion
}
