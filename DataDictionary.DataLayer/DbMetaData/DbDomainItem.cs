﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbDomainItem : IDbDomainKey, IDbCatalogKey, IDbDomain, IBindingTableRow
    {
        String? DomainDefault { get; }
    }

    public class DbDomainItem : BindingTableRow, IDbDomainItem, INotifyPropertyChanged, IDbExtendedProperties
    {
        public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } }
        public String? CatalogName { get { return GetValue("CatalogName"); } }
        public String? SchemaName { get { return GetValue("SchemaName"); } }
        public String? DomainName { get { return GetValue("DomainName"); } }
        public String? DataType { get { return GetValue("DataType"); } }
        public String? DomainDefault { get { return GetValue("DomainDefault"); } }
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


        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(String)){ AllowDBNull = true},
            new DataColumn("CatalogName", typeof(String)){ AllowDBNull = false},
            new DataColumn("SchemaName", typeof(String)){ AllowDBNull = false},
            new DataColumn("DomainName", typeof(String)){ AllowDBNull = false},
            new DataColumn("DataType", typeof(String)){ AllowDBNull = true},
            new DataColumn("DomainDefault", typeof(String)){ AllowDBNull = true},
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
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static Command GetSchema(IConnection connection)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = DbScript.DbDomainItem;
            return command;
        }

        public virtual Command GetProperties(IConnection connection)
        {
            return (new DbExtendedPropertyGetCommand(connection)
            { Level0Name = SchemaName, Level0Type = "SCHEMA", Level1Name = DomainName, Level1Type = "TYPE" }).
            GetCommand();
        }
    }
}
