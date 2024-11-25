using DataDictionary.DataLayer.DatabaseData;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Class used to get and temporary store the Information Schema of a Catalog Schema
    /// </summary>
    class SchemaInformationSchema : BindingTableRow, ISchemaKeyName
    {
        /// <summary>
        /// Database Name as the Database identifies itself.
        /// </summary>
        public String DatabaseName { get { return GetValue(nameof(DatabaseName)) ?? String.Empty; } }

        /// <summary>
        /// Database Name as the Database identifies itself.
        /// </summary>
        public String SchemaName { get { return GetValue(nameof(SchemaName)) ?? String.Empty; } }

        /// <summary>
        /// Constructor for Catalog Schema Information Schema
        /// </summary>
        public SchemaInformationSchema() : base() { }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(DatabaseName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(String)){ AllowDBNull = false},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static IReadOnlyList<SchemaInformationSchema> GetSchema(IConnection connection)
        {
            BindingTable<SchemaInformationSchema> schemas = new BindingTable<SchemaInformationSchema>();
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = Schema.InformationSchema;

            schemas.Load(connection.ExecuteReader(command));

            return schemas;
        }

    }
}
