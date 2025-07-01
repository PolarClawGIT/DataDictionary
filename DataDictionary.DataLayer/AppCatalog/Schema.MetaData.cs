using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Class used to store the Information Schema of a Catalog Schema
    /// </summary>
    public class SchemaMetaData : BindingTableRow, ISchema
    {
        /// <inheritdoc/>
        public String DatabaseName { get { return GetValue(nameof(DatabaseName)) ?? String.Empty; } }

        /// <inheritdoc/>
        public String SchemaName { get { return GetValue(nameof(SchemaName)) ?? String.Empty; } }

        /// <summary>
        /// Constructor for Catalog Schema Information Schema
        /// </summary>
        public SchemaMetaData() : base() { }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(DatabaseName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(String)){ AllowDBNull = false},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <summary>
        /// Gets the Information Schema from the Database
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static IEnumerable<ISchema> GetSchema(IConnection connection)
        {
            BindingTable<SchemaMetaData> schemas = new BindingTable<SchemaMetaData>();
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = SchemaScript.GetInformationSchema(typeof(Schema));

            schemas.Load(connection.ExecuteReader(command));

            return schemas;
        }

    }
}
