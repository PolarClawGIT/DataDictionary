using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Class used to store the Information Schema of a Catalog Constraint
    /// </summary>
    public class ConstraintMetaData : BindingTableRow, IConstraint
    {
        /// <inheritdoc/>
        public String? DatabaseName { get { return GetValue(nameof(DatabaseName)); } }

        /// <inheritdoc/>
        public String? SchemaName { get { return GetValue(nameof(SchemaName)); } }

        /// <inheritdoc/>
        public String? ConstraintName { get { return GetValue(nameof(ConstraintName)); } }

        /// <inheritdoc/>
        public String? TableName { get { return GetValue(nameof(TableName)); } }

        /// <inheritdoc/>
        public String? ConstraintType { get { return GetValue(nameof(ConstraintType)); } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(DatabaseName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(ConstraintName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(TableName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(ConstraintType), typeof(string)){ AllowDBNull = false},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <summary>
        /// Gets the Information Schema from the Database
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static IEnumerable<IConstraint> GetSchema(IConnection connection)
        {
            BindingTable<ConstraintMetaData> schemas = new BindingTable<ConstraintMetaData>();
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = SchemaScript.GetInformationSchema(typeof(Constraint));

            schemas.Load(connection.ExecuteReader(command));

            return schemas;
        }
    }
}
