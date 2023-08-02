// Ignore Spelling: Nullable

using DataDictionary.DataLayer.ApplicationData;
using DataDictionary.DataLayer.DomainData;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbTableColumnItem : IDbTableColumnName, IBindingTableRow
    {
        //Guid? CatalogId { get; }
        Nullable<Int32> OrdinalPosition { get; }
        String? ColumnDefault { get; }
        Nullable<Boolean> IsNullable { get; }
        String? DataType { get; }
        Nullable<Int32> CharacterMaximumLength { get; }
        Nullable<Int32> CharacterOctetLength { get; }
        Nullable<Byte> NumericPrecision { get; }
        Nullable<Int16> NumericPrecisionRadix { get; }
        Nullable<Int32> NumericScale { get; }
        Nullable<Int16> DateTimePrecision { get; }
        String? CharacterSetCatalog { get; }
        String? CharacterSetSchema { get; }
        String? CharacterSetName { get; }
        String? CollationCatalog { get; }
        String? CollationSchema { get; }
        String? CollationName { get; }
        String? DomainCatalog { get; }
        String? DomainSchema { get; }
        String? DomainName { get; }
        Nullable<Boolean> IsIdentity { get; }
        Nullable<Boolean> IsHidden { get; }
        Nullable<Boolean> IsComputed { get; }
        String? ComputedDefinition { get; }
        String? GeneratedAlwayType { get; }
    }

    public class DbTableColumnItem : BindingTableRow, IDbTableColumnItem, INotifyPropertyChanged, IDbExtendedProperties
    {
        //public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } }
        public String? CatalogName { get { return GetValue("CatalogName"); } }
        public String? SchemaName { get { return GetValue("SchemaName"); } }
        public String? TableName { get { return GetValue("TableName"); } }
        public String? ColumnName { get { return GetValue("ColumnName"); } }
        public Nullable<Int32> OrdinalPosition { get { return GetValue<Int32>("OrdinalPosition"); } }
        public String? ColumnDefault { get { return GetValue("ColumnDefault"); } }
        public Nullable<Boolean> IsNullable { get { return GetValue<Boolean>("IsNullable", BindingItemParsers.BooleanTryParse); } }
        public String? DataType { get { return GetValue("DataType"); } }
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


        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(String)){ AllowDBNull = true},
            new DataColumn("CatalogName", typeof(String)){ AllowDBNull = false},
            new DataColumn("SchemaName", typeof(String)){ AllowDBNull = false},
            new DataColumn("TableName", typeof(String)){ AllowDBNull = false},
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
            return (new DbExtendedPropertyGetCommand(connection)
            { Level0Name = SchemaName, Level0Type = "SCHEMA", Level1Name = TableName, Level1Type = "TABLE", Level2Name = ColumnName, Level2Type = "COLUMN" }).
            GetCommand();
        }

        public static Command GetData(IConnection connection, IModelIdentifier modelId)
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

        public static Command SetData(IConnection connection, IModelIdentifier modelId, IBindingTable<DbTableColumnItem> source)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDatabaseTableColumn]";
            command.AddParameter("@ModelId", modelId.ModelId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDatabaseColumn]", source);
            return command;
        }

        public override String ToString()
        { return new DbTableColumnName(this).ToString(); }
    }

    public static class DbColumnItemExtension
    {

        public static DbTableColumnItem? GetColumn(this IEnumerable<DbTableColumnItem> source, IDbTableColumnName item)
        {
            DbTableColumnName itemName = new DbTableColumnName(item);
            return source.FirstOrDefault(w => itemName == w);
        }

        public static DbTableColumnItem? GetColumn(this IDbTableColumnName item, IEnumerable<DbTableColumnItem> source)
        {
            DbTableColumnName itemName = new DbTableColumnName(item);
            return source.FirstOrDefault(w => itemName == w);
        }

        public static IEnumerable<DbTableColumnItem> GetColumns(this IEnumerable<DbTableColumnItem> source, IDbTableName item)
        {
            DbTableName itemName = new DbTableName(item);
            return source.Where(w => itemName == w);
        }

        public static IEnumerable<DbTableColumnItem> GetColumns(this IDbTableName item, IEnumerable<DbTableColumnItem> source)
        {
            DbTableName itemName = new DbTableName(item);
            return source.Where(w => itemName == w);
        }
    }
}
