using DataDictionary.DataLayer.DatabaseData;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Class used to get and temporary store the Information Schema of a Catalog
    /// </summary>
    class CatalogInformationSchema : BindingTableRow, ICatalogKeyName
    {
        /// <summary>
        /// Server Name as the Server identifies itself.
        /// </summary>
        public String ServerName { get { return GetValue(nameof(ServerName)) ?? String.Empty; } }

        /// <summary>
        /// Database Name as the Database identifies itself.
        /// </summary>
        public String DatabaseName { get { return GetValue(nameof(DatabaseName)) ?? String.Empty; } }

        /// <summary>
        /// The Date the database was created
        /// </summary>
        public DateTime CreateDate { get { return GetValue<DateTime>(nameof(CreateDate)) ?? DateTime.Now; } }

        /// <summary>
        /// Owner of the Database
        /// </summary>
        public String Owner { get { return GetValue(nameof(Owner)) ?? String.Empty; } }

        /// <summary>
        /// Constructor for Catalog Information Schema
        /// </summary>
        public CatalogInformationSchema() : base() { }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(ServerName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(DatabaseName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(CreateDate), typeof(DateTime)){ AllowDBNull = false},
            new DataColumn(nameof(Owner), typeof(String)){ AllowDBNull = false},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <summary>
        /// Gets the Information Schema from the Database
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static IReadOnlyList<CatalogInformationSchema> GetSchema (IConnection connection)
        {
            BindingTable<CatalogInformationSchema> schemas = new BindingTable<CatalogInformationSchema>();
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = Catalog.InformationSchema;

            schemas.Load(connection.ExecuteReader(command));

            return schemas;
        }
    }
}
