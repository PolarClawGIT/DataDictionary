using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Class used to store the Information Schema of a Catalog TableColumn
    /// </summary>
    public class RoutineParameterMetaData: BindingTableRow, IRoutineParameter
    {
        /// <inheritdoc/>
        public String? DatabaseName { get { return GetValue(nameof(DatabaseName)); } }

        /// <inheritdoc/>
        public String? SchemaName { get { return GetValue(nameof(SchemaName)); } }

        /// <inheritdoc/>
        public String? RoutineName { get { return GetValue(nameof(RoutineName)); } }

        /// <inheritdoc/>
        public String? RoutineType { get { return GetValue(nameof(RoutineType)); } }

        /// <inheritdoc/>
        public String? ParameterName { get { return GetValue(nameof(ParameterName)); } }

        /// <inheritdoc/>
        public Int32? OrdinalPosition { get { return GetValue<int>(nameof(OrdinalPosition)); } }

        /// <inheritdoc/>
        public String? DataType { get { return GetValue(nameof(DataType)); } }

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

        /// <inheritdoc/>
        public String? DomainCatalog { get { return GetValue(nameof(DomainCatalog)); } }

        /// <inheritdoc/>
        public String? DomainSchema { get { return GetValue(nameof(DomainSchema)); } }

        /// <inheritdoc/>
        public String? DomainName { get { return GetValue(nameof(DomainName)); } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(DatabaseName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(RoutineName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(RoutineType), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(ParameterName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(OrdinalPosition), typeof(int)){ AllowDBNull = false},
            new DataColumn(nameof(DataType), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterMaximumLength), typeof(int)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterOctetLength), typeof(int)){ AllowDBNull = true},
            new DataColumn(nameof(NumericPrecision), typeof(byte)){ AllowDBNull = true},
            new DataColumn(nameof(NumericPrecisionRadix), typeof(short)){ AllowDBNull = true},
            new DataColumn(nameof(NumericScale), typeof(int)){ AllowDBNull = true},
            new DataColumn(nameof(DateTimePrecision), typeof(short)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterSetCatalog), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterSetSchema), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterSetName), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(CollationCatalog), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(CollationSchema), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(CollationName), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(DomainCatalog), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(DomainSchema), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(DomainName), typeof(string)){ AllowDBNull = true},
        };

        /// <summary>
        /// Constructor for the Database Table Column
        /// </summary>
        public RoutineParameterMetaData() : base() { }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <summary>
        /// Gets the Information Schema from the Database
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static IEnumerable<IRoutineParameter> GetSchema(IConnection connection)
        {
            BindingTable<RoutineParameterMetaData> schemas = new BindingTable<RoutineParameterMetaData>();
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = RoutineParameter.TSql_InformationSchema;

            schemas.Load(connection.ExecuteReader(command));

            return schemas;
        }
    }
}
