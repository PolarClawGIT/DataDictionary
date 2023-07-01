using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbColumnItem : IDbColumnName, IDbIsSystem
    {
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
        Nullable<Boolean> IsSparse { get; }
        Nullable<Boolean> IsColumnSet { get; }
        Nullable<Boolean> IsFileStream { get; }
    }

    public class DbColumnItem : BindingTableRow, IDbColumnItem, INotifyPropertyChanged
    {
        public String? CatalogName { get { return GetValue("TABLE_CATALOG"); } }
        public String? SchemaName { get { return GetValue("TABLE_SCHEMA"); } }
        public String? ObjectName { get { return GetValue("TABLE_NAME"); } }
        public String? ColumnName { get { return GetValue("COLUMN_NAME"); } }
        public Nullable<Int32> OrdinalPosition { get { return GetValue<Int32>("ORDINAL_POSITION"); } }
        public String? ColumnDefault { get { return GetValue("COLUMN_DEFAULT"); } }
        public Nullable<Boolean> IsNullable { get { return GetValue<Boolean>("IS_NULLABLE", BindingItemParsers.BooleanTryPrase); } }
        public String? DataType { get { return GetValue("DATA_TYPE"); } }
        public Nullable<Int32> CharacterMaximumLength { get { return GetValue<Int32>("CHARACTER_MAXIMUM_LENGTH"); } }
        public Nullable<Int32> CharacterOctetLength { get { return GetValue<Int32>("CHARACTER_OCTET_LENGTH"); } }
        public Nullable<Byte> NumericPrecision { get { return GetValue<Byte>("NUMERIC_PRECISION"); } }
        public Nullable<Int16> NumericPrecisionRadix { get { return GetValue<Int16>("NUMERIC_PRECISION_RADIX"); } }
        public Nullable<Int32> NumericScale { get { return GetValue<Int32>("NUMERIC_SCALE"); } }
        public Nullable<Int16> DateTimePrecision { get { return GetValue<Int16>("DATETIME_PRECISION"); } }
        public String? CharacterSetCatalog { get { return GetValue("CHARACTER_SET_CATALOG"); } }
        public String? CharacterSetSchema { get { return GetValue("CHARACTER_SET_SCHEMA"); } }
        public String? CharacterSetName { get { return GetValue("CHARACTER_SET_NAME"); } }
        public String? CollationCatalog { get { return GetValue("COLLATION_CATALOG"); } }
        public Nullable<Boolean> IsSparse { get { return GetValue<Boolean>("IS_SPARSE", BindingItemParsers.BooleanTryPrase); } }
        public Nullable<Boolean> IsColumnSet { get { return GetValue<Boolean>("IS_COLUMN_SET", BindingItemParsers.BooleanTryPrase); } }
        public Nullable<Boolean> IsFileStream { get { return GetValue<Boolean>("IS_FILESTREAM", BindingItemParsers.BooleanTryPrase); } }
        public Boolean IsSystem { get { return false; } } //TODO: Identites, calculated, and other system managed fields needs to be detected/addressed

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("TABLE_CATALOG", typeof(String)){ AllowDBNull = false},
            new DataColumn("TABLE_SCHEMA", typeof(String)){ AllowDBNull = false},
            new DataColumn("TABLE_NAME", typeof(String)){ AllowDBNull = false},
            new DataColumn("COLUMN_NAME", typeof(String)){ AllowDBNull = false},
            new DataColumn("ORDINAL_POSITION", typeof(Int32)){ AllowDBNull = false},
            new DataColumn("COLUMN_DEFAULT", typeof(String)){ AllowDBNull = true},
            new DataColumn("IS_NULLABLE", typeof(String)){ AllowDBNull = true},
            new DataColumn("DATA_TYPE", typeof(String)){ AllowDBNull = true},
            new DataColumn("CHARACTER_MAXIMUM_LENGTH", typeof(Int32)){ AllowDBNull = true},
            new DataColumn("CHARACTER_OCTET_LENGTH", typeof(Int32)){ AllowDBNull = true},
            new DataColumn("NUMERIC_PRECISION", typeof(Byte)){ AllowDBNull = true},
            new DataColumn("NUMERIC_PRECISION_RADIX", typeof(Int16)){ AllowDBNull = true},
            new DataColumn("NUMERIC_SCALE", typeof(Int32)){ AllowDBNull = true},
            new DataColumn("DATETIME_PRECISION", typeof(Int16)){ AllowDBNull = true},
            new DataColumn("CHARACTER_SET_CATALOG", typeof(String)){ AllowDBNull = true},
            new DataColumn("CHARACTER_SET_SCHEMA", typeof(String)){ AllowDBNull = true},
            new DataColumn("CHARACTER_SET_NAME", typeof(String)){ AllowDBNull = true},
            new DataColumn("COLLATION_CATALOG", typeof(String)){ AllowDBNull = true},
            new DataColumn("IS_SPARSE", typeof(String)){ AllowDBNull = true},
            new DataColumn("IS_COLUMN_SET", typeof(String)){ AllowDBNull = true},
            new DataColumn("IS_FILESTREAM", typeof(String)){ AllowDBNull = true},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        public static IDataReader GetDataReader(IConnection connection)
        { return connection.GetReader(Schema.Collection.Columns); }

        public virtual SqlCommand GetProperties(IConnection connection)
        {
            return (new DbExtendedPropertyGetCommand(connection)
            { Level0Name = SchemaName, Level0Type = "SCHEMA", Level1Name = ObjectName, Level1Type = "TABLE", Level2Name = ColumnName, Level2Type = "COLUMN" }).
            GetCommand();
        }
    }
}
