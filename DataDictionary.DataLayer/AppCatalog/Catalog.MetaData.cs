using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Class used to store the Information Schema of a Catalog
    /// </summary>
    public class CatalogMetaData : BindingTableRow, ICatalog
    {
        /// <inheritdoc/>
        public String? CatalogTitle { get { return GetValue(nameof(DatabaseName)) ?? String.Empty; } }

        /// <inheritdoc/>
        public String? CatalogDescription { get {return String.Empty; } }

        /// <inheritdoc/>
        public String ServerName { get { return GetValue(nameof(ServerName)) ?? String.Empty; } }

        /// <inheritdoc/>
        public String DatabaseName { get { return GetValue(nameof(DatabaseName)) ?? String.Empty; } }

        /// <summary>
        /// The Date the database was created
        /// </summary>
        public DateTime CreateDate { get { return GetValue<DateTime>(nameof(CreateDate)) ?? DateTime.Now; } }

        /// <summary>
        /// Owner of the Database
        /// </summary>
        public String Owner { get { return GetValue(nameof(Owner)) ?? String.Empty; } }

        /// <inheritdoc/>
        public DateTime? SourceDate { get; } = DateTime.Now;

        /// <summary>
        /// Constructor for Catalog Information Schema
        /// </summary>
        public CatalogMetaData() : base() { }

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
        public static IEnumerable<ICatalog> GetSchema (IConnection connection)
        {
            BindingTable<CatalogMetaData> schemas = new BindingTable<CatalogMetaData>();
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = Catalog.TSql_InformationSchema;

            schemas.Load(connection.ExecuteReader(command));

            return schemas;
        }
    }
}
