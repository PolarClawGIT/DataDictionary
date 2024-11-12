using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppCatalog
{
    public class DomainInformationSchema : BindingTableRow
    {

        public String DatabaseName { get { return GetValue(nameof(DatabaseName)) ?? String.Empty; } }


        public String SchemaName { get { return GetValue(nameof(SchemaName)) ?? String.Empty; } }


        public String DomainName { get { return GetValue(nameof(DomainName)) ?? String.Empty; } }

        /// <inheritdoc/>
        public String? DataType { get { return GetValue(nameof(DataType)); } }

        /// <inheritdoc/>
        public String? DomainDefault { get { return GetValue(nameof(DomainDefault)); } }

        /// <inheritdoc/>
        public Int32? CharacterMaximumLength { get { return GetValue<int>(nameof(CharacterMaximumLength)); } }

        /// <inheritdoc/>
        public Int32? CharacterOctetLength { get { return GetValue<int>(nameof(CharacterOctetLength)); } }

        /// <inheritdoc/>
        public Byte? NumericPrecision { get { return GetValue<byte>(nameof(NumericPrecision)); } }

        /// <inheritdoc/>
        public Int16? NumericPrecisionRadix { get { return GetValue<short>(nameof(NumericPrecisionRadix)); } }

        /// <inheritdoc/>
        public Int32? NumericScale { get { return GetValue<int>(nameof(NumericScale)); } }

        /// <inheritdoc/>
        public Int16? DateTimePrecision { get { return GetValue<short>(nameof(DateTimePrecision)); } }

        /// <inheritdoc/>
        public String? CharacterSetCatalog { get { return GetValue(nameof(CharacterSetCatalog)); } }

        /// <inheritdoc/>
        public String? CharacterSetSchema { get { return GetValue(nameof(CharacterSetSchema)); } }

        /// <inheritdoc/>
        public String? CharacterSetName { get { return GetValue(nameof(CharacterSetName)); } }

        /// <inheritdoc/>
        public String? CollationCatalog { get { return GetValue(nameof(CollationCatalog)); } }

        /// <inheritdoc/>
        public String? CollationSchema { get { return GetValue(nameof(CollationSchema)); } }

        /// <inheritdoc/>
        public String? CollationName { get { return GetValue(nameof(CollationName)); } }

        /// <summary>
        /// Constructor for Catalog Schema Information Schema
        /// </summary>
        public DomainInformationSchema() : base() { }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(DatabaseName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(DomainName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(DataType), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(DomainDefault), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterMaximumLength), typeof(Int32)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterOctetLength), typeof(Int32)){ AllowDBNull = true},
            new DataColumn(nameof(NumericPrecision), typeof(Byte)){ AllowDBNull = true},
            new DataColumn(nameof(NumericPrecisionRadix), typeof(Int16)){ AllowDBNull = true},
            new DataColumn(nameof(NumericScale), typeof(Int32)){ AllowDBNull = true},
            new DataColumn(nameof(DateTimePrecision), typeof(Int16)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterSetCatalog), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterSetSchema), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterSetName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CollationCatalog), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CollationSchema), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CollationName), typeof(String)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static Command SchemaCommand(IConnection connection)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = SqlScript.Domain.InformationSchema;
            return command;

        }
    }
}
