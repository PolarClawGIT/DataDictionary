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
    /// <summary>
    /// Class used to store the Information Schema of a Catalog Domain
    /// </summary>
    public class DomainMetaData : BindingTableRow, IDomain
    {
        /// <inheritdoc/>
        public String DatabaseName { get { return GetValue(nameof(DatabaseName)) ?? String.Empty; } }

        /// <inheritdoc/>
        public String SchemaName { get { return GetValue(nameof(SchemaName)) ?? String.Empty; } }

        /// <inheritdoc/>
        public String DomainName { get { return GetValue(nameof(DomainName)) ?? String.Empty; } }

        /// <inheritdoc/>
        public String? DataType { get { return GetValue(nameof(DataType)); } }

        /// <inheritdoc/>
        public String? DomainDefault { get { return GetValue(nameof(DomainDefault)); } }

        /// <inheritdoc/>
        public Int16? CharacterMaximumLength { get { return GetValue<Int16>(nameof(CharacterMaximumLength)); } }

        /// <inheritdoc/>
        public Int16? CharacterOctetLength { get { return GetValue<Int16>(nameof(CharacterOctetLength)); } }

        /// <inheritdoc/>
        public Byte? NumericPrecision { get { return GetValue<Byte>(nameof(NumericPrecision)); } }

        /// <inheritdoc/>
        public Byte? NumericPrecisionRadix { get { return GetValue<Byte>(nameof(NumericPrecisionRadix)); } }

        /// <inheritdoc/>
        public Byte? NumericScale { get { return GetValue<Byte>(nameof(NumericScale)); } }

        /// <inheritdoc/>
        public Byte? DateTimePrecision { get { return GetValue<Byte>(nameof(DateTimePrecision)); } }

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
        /// Constructor for Catalog Domain Information Schema
        /// </summary>
        public DomainMetaData() : base() { }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(DatabaseName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(DomainName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(DataType), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(DomainDefault), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterMaximumLength), typeof(Int16)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterOctetLength), typeof(Int16)){ AllowDBNull = true},
            new DataColumn(nameof(NumericPrecision), typeof(Byte)){ AllowDBNull = true},
            new DataColumn(nameof(NumericPrecisionRadix), typeof(Byte)){ AllowDBNull = true},
            new DataColumn(nameof(NumericScale), typeof(Byte)){ AllowDBNull = true},
            new DataColumn(nameof(DateTimePrecision), typeof(Byte)){ AllowDBNull = true},
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

        /// <summary>
        /// Gets the Information Schema from the Database
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static IEnumerable<IDomain> GetSchema(IConnection connection)
        {
            BindingTable<DomainMetaData> schemas = new BindingTable<DomainMetaData>();
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = Domain.TSql_InformationSchema;

            schemas.Load(connection.ExecuteReader(command));

            return schemas;
        }
    }
}
