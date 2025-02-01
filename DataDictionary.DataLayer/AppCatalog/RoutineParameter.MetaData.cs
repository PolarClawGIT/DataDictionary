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
        public Int32? OrdinalPosition { get { return GetValue<Int32>(nameof(OrdinalPosition)); } }

        /// <inheritdoc/>
        public String? DataType { get { return GetValue(nameof(DataType)); } }

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

        /// <inheritdoc/>
        public String? DomainCatalog { get { return GetValue(nameof(DomainCatalog)); } }

        /// <inheritdoc/>
        public String? DomainSchema { get { return GetValue(nameof(DomainSchema)); } }

        /// <inheritdoc/>
        public String? DomainName { get { return GetValue(nameof(DomainName)); } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(DatabaseName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(RoutineName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(RoutineType), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(ParameterName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(OrdinalPosition), typeof(Int32)){ AllowDBNull = false},
            new DataColumn(nameof(DataType), typeof(String)){ AllowDBNull = true},
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
            new DataColumn(nameof(DomainCatalog), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(DomainSchema), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(DomainName), typeof(String)){ AllowDBNull = true},
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
