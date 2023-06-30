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
        public Nullable<Int32> OrdinalPosition { get; }
        public String? ColumnDefault { get; }
        public Nullable<Boolean> IsNullable { get; }
        public String? DataType { get; }
        public Nullable<Int32> CharacterMaximumLength { get; }
        public Nullable<Int32> CharacterOctetLength { get; }
        public Nullable<Byte> NumericPrecision { get; }
        public Nullable<Int16> NumericPrecisionRadix  { get; }
        public Nullable<Int32> NumericScale { get ; }
        public Nullable<Int16> DateTimePrecision { get ; }
        public String? CharacterSetCatalog { get; }
        public String? CharacterSetSchema { get; }
        public String? CharacterSetName { get; }
        public String? CollationCatalog { get; }
    }


    public class DbColumnItem : BindingTableRow, IDbColumnItem, INotifyPropertyChanged
    {
        public String? CatalogName { get { return GetValue("TABLE_CATALOG"); } }
        public String? SchemaName { get { return GetValue("TABLE_SCHEMA"); } }
        public String? TableName { get { return GetValue("TABLE_NAME"); } }
        public String? ColumnName { get { return GetValue("COLUMN_NAME"); } }
        public Nullable<Int32> OrdinalPosition { get { return GetValue<Int32>("ORDINAL_POSITION"); } }
        public String? ColumnDefault { get { return GetValue("COLUMN_DEFAULT"); } }
        public Nullable<Boolean> IsNullable
        {
            get
            {
                String? value = GetValue("IS_NULLABLE");
                if (value is "yes" or "YES" or "Yes" or "true" or "TRUE" or "True" or "1") { return new Nullable<Boolean>(true); }
                if (value is "no" or "NO" or "No" or "false" or "FALSE" or "False" or "0") { return new Nullable<Boolean>(false); }
                return new Nullable<Boolean>();
            }
        }
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

        public Nullable<Boolean> IsSparse
        { get { return GetValue<Boolean>("IS_SPARSE", BindingItemParsers.BooleanTryPrase); } }

        public Nullable<Boolean> IsColumnSet
        { get { return GetValue<Boolean>("IS_COLUMN_SET", BindingItemParsers.BooleanTryPrase); } }


        public Nullable<Boolean> IsFileStream
        { get { return GetValue<Boolean>("IS_FILESTREAM", BindingItemParsers.BooleanTryPrase); } }

        public Boolean IsSystem { get { return false; } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("TABLE_CATALOG", typeof(String)){ AllowDBNull = false},
            new DataColumn("TABLE_SCHEMA", typeof(String)){ AllowDBNull = false},
            new DataColumn("TABLE_NAME", typeof(String)){ AllowDBNull = false},
            new DataColumn("COLUMN_NAME", typeof(String)){ AllowDBNull = false},
            new DataColumn("ORDINAL_POSITION", typeof(Int32)){ AllowDBNull = false},
            new DataColumn("COLUMN_DEFAULT", typeof(String)){ AllowDBNull = true},
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

        public override IReadOnlyList<DataColumn> ColumnDefinitions { get; } = columnDefinitions;


        public static IDataReader GetDataReader(IConnection connection)
        { return connection.GetReader(Schema.Collection.Columns); }

        public virtual SqlCommand GetProperties(IConnection connection)
        {
            return (new DbExtendedPropertyGetCommand(connection)
            { Level0Name = SchemaName, Level0Type = "SCHEMA", Level1Name = TableName, Level1Type = "TABLE", Level2Name = ColumnName, Level2Type = "COLUMN" }).
            GetCommand();
        }
    }
}
