// Ignore Spelling: Nullable

using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DomainData;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DatabaseData
{
    public interface IDbTableColumnItem : IDbTableColumnKey, IDbCatalogKey, IDbDomainReferenceKey, IDbElementScope, IDbColumn, IBindingTableRow
    {
        Nullable<Boolean> IsNullable { get; }
        String? ColumnDefault { get; }
        Nullable<Boolean> IsIdentity { get; }
        Nullable<Boolean> IsHidden { get; }
        Nullable<Boolean> IsComputed { get; }
        String? ComputedDefinition { get; }
        String? GeneratedAlwayType { get; }
    }

    [Serializable]
    public class DbTableColumnItem : BindingTableRow, IDbTableColumnItem, INotifyPropertyChanged, IDbExtendedProperties, ISerializable
    {
        public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } }
        public String? CatalogName { get { return GetValue("CatalogName"); } }
        public String? SchemaName { get { return GetValue("SchemaName"); } }
        public String? TableName { get { return GetValue("TableName"); } }
        public String? ColumnName { get { return GetValue("ColumnName"); } }
        public Nullable<Int32> OrdinalPosition { get { return GetValue<Int32>("OrdinalPosition"); } }
        public Nullable<Boolean> IsNullable { get { return GetValue<Boolean>("IsNullable", BindingItemParsers.BooleanTryParse); } }
        public String? DataType { get { return GetValue("DataType"); } }
        public String? ColumnDefault { get { return GetValue("ColumnDefault"); } }
        public Nullable<Int32> CharacterMaximumLength { get { return GetValue<Int32>("CharacterMaximumLength"); } }
        public Nullable<Int32> CharacterOctetLength { get { return GetValue<Int32>("CharacterOctetLength"); } }
        public Nullable<Byte> NumericPrecision { get { return GetValue<Byte>("NumericPrecision"); } }
        public Nullable<Int16> NumericPrecisionRadix { get { return GetValue<Int16>("NumericPrecisionRadix"); } }
        public Nullable<Int32> NumericScale { get { return GetValue<Int32>("NumericScale"); } }
        public Nullable<Int16> DateTimePrecision { get { return GetValue<Int16>("DateTimePrecision"); } }
        public String? CharacterSetCatalog { get { return GetValue("CharacterSetCatalog"); } }
        public String? CharacterSetSchema { get { return GetValue("CharacterSetSchema"); } }
        public String? CharacterSetName { get { return GetValue("CharacterSetName"); } }
        public String? CollationCatalog { get { return GetValue("CollationCatalog"); } }
        public String? CollationSchema { get { return GetValue("CollationSchema"); } }
        public String? CollationName { get { return GetValue("CollationName"); } }
        public String? DomainCatalog { get { return GetValue("DomainCatalog"); } }
        public String? DomainSchema { get { return GetValue("DomainSchema"); } }
        public String? DomainName { get { return GetValue("DomainName"); } }
        public Nullable<Boolean> IsIdentity { get { return GetValue<Boolean>("IsIdentity", BindingItemParsers.BooleanTryParse); } }
        public Nullable<Boolean> IsHidden { get { return GetValue<Boolean>("IsHidden", BindingItemParsers.BooleanTryParse); } }
        public Nullable<Boolean> IsComputed { get { return GetValue<Boolean>("IsComputed", BindingItemParsers.BooleanTryParse); } }
        public String? ComputedDefinition { get { return GetValue("ComputedDefinition"); } }
        public String? GeneratedAlwayType { get { return GetValue("GeneratedAlwayType"); } }
        public DbElementScope ElementScope { get; } = DbElementScope.Column;

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(String)){ AllowDBNull = true},
            new DataColumn("CatalogName", typeof(String)){ AllowDBNull = false},
            new DataColumn("SchemaName", typeof(String)){ AllowDBNull = false},
            new DataColumn("TableName", typeof(String)){ AllowDBNull = false},
            new DataColumn("TableType", typeof(String)){ AllowDBNull = false},
            new DataColumn("ColumnName", typeof(String)){ AllowDBNull = false},
            new DataColumn("OrdinalPosition", typeof(Int32)){ AllowDBNull = false},
            new DataColumn("IsNullable", typeof(Boolean)){ AllowDBNull = true},
            new DataColumn("DataType", typeof(String)){ AllowDBNull = true},
            new DataColumn("ColumnDefault", typeof(String)){ AllowDBNull = true},
            new DataColumn("CharacterMaximumLength", typeof(Int32)){ AllowDBNull = true},
            new DataColumn("CharacterOctetLength", typeof(Int32)){ AllowDBNull = true},
            new DataColumn("NumericPrecision", typeof(Byte)){ AllowDBNull = true},
            new DataColumn("NumericPrecisionRadix", typeof(Int16)){ AllowDBNull = true},
            new DataColumn("NumericScale", typeof(Int32)){ AllowDBNull = true},
            new DataColumn("DateTimePrecision", typeof(Int16)){ AllowDBNull = true},
            new DataColumn("CharacterSetCatalog", typeof(String)){ AllowDBNull = true},
            new DataColumn("CharacterSetSchema", typeof(String)){ AllowDBNull = true},
            new DataColumn("CharacterSetName", typeof(String)){ AllowDBNull = true},
            new DataColumn("CollationCatalog", typeof(String)){ AllowDBNull = true},
            new DataColumn("CollationSchema", typeof(String)){ AllowDBNull = true},
            new DataColumn("CollationName", typeof(String)){ AllowDBNull = true},
            new DataColumn("DomainCatalog", typeof(String)){ AllowDBNull = true},
            new DataColumn("DomainSchema", typeof(String)){ AllowDBNull = true},
            new DataColumn("DomainName", typeof(String)){ AllowDBNull = true},
            new DataColumn("IsIdentity", typeof(Boolean)){ AllowDBNull = true},
            new DataColumn("IsHidden", typeof(Boolean)){ AllowDBNull = true},
            new DataColumn("IsComputed", typeof(Boolean)){ AllowDBNull = true},
            new DataColumn("ComputedDefinition", typeof(String)){ AllowDBNull = true},
            new DataColumn("GeneratedAlwayType", typeof(String)){ AllowDBNull = true},
        };

        public DbTableColumnItem() : base() { }

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static Command GetSchema(IConnection connection)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = DbScript.DbColumnItem;
            return command;
        }

        public virtual Command GetProperties(IConnection connection)
        {
            String level1Type = "TABLE";
            if (GetValue("TableType") is "VIEW") { level1Type = "VIEW"; }

            return (new DbExtendedPropertyGetCommand(connection)
            { Level0Name = SchemaName, Level0Type = "SCHEMA", Level1Name = TableName, Level1Type = level1Type, Level2Name = ColumnName, Level2Type = "COLUMN" }).
            GetCommand();
        }

        public static Command GetData(IConnection connection, IModelKey modelId)
        { return GetData(connection, (modelId.ModelId, null, null, null, null)); }

        static Command GetData(IConnection connection, (Guid? modelId, String? catalogName, String? schemaName, String? tableName, String? columnName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDatabaseTableColumn]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@CatalogName", parameters.catalogName);
            command.AddParameter("@SchemaName", parameters.schemaName);
            command.AddParameter("@TableName", parameters.tableName);
            command.AddParameter("@ColumnName", parameters.columnName);
            return command;
        }

        public static Command SetData(IConnection connection, IModelKey modelId, IBindingTable<DbTableColumnItem> source)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDatabaseTableColumn]";
            command.AddParameter("@ModelId", modelId.ModelId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDatabaseTableColumn]", source);
            return command;
        }

        #region ISerializable
        protected DbTableColumnItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        public override String ToString()
        { return new DbTableColumnKey(this).ToString(); }
    }

    public static class DbColumnItemExtension
    {
        public static DbTableColumnItem? GetColumn(this IEnumerable<DbTableColumnItem> source, IDbTableColumnKey item)
        { return source.FirstOrDefault(w => new DbTableColumnKey(item) == new DbTableColumnKey(w)); }

        public static DbTableColumnItem? GetColumn(this IDbTableColumnKey item, IEnumerable<DbTableColumnItem> source)
        { return source.FirstOrDefault(w => new DbTableColumnKey(item) == new DbTableColumnKey(w)); }

        public static IEnumerable<DbTableColumnItem> GetColumns(this IEnumerable<DbTableColumnItem> source, IDbTableKey item)
        { return source.Where(w => new DbTableKey(item) == new DbTableKey(w)); }

        public static IEnumerable<DbTableColumnItem> GetColumns(this IDbTableKey item, IEnumerable<DbTableColumnItem> source)
        { return source.Where(w => new DbTableKey(item) == new DbTableKey(w)); }
    }
}
