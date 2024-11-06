using DataDictionary.DataLayer.DatabaseData;
using Microsoft.Data.SqlClient;
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
    /// Class used to get and temporary store the Information Schema of a Catalog
    /// </summary>
    public class CatalogInformationSchema : BindingTableRow,
        IReadSchema
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
        /// The Date the information was pulled per the Server
        /// </summary>
        public DateTime SourceDate { get { return GetValue<DateTime>(nameof(SourceDate)) ?? DateTime.Now; } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(ServerName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(DatabaseName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(SourceDate), typeof(DateTime)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <summary>
        /// Creates a BindingTable to hold the Information Schema of the Catalog
        /// </summary>
        /// <returns></returns>
        public static BindingTable<CatalogInformationSchema> Create()
        { return new BindingTable<CatalogInformationSchema>(); }

        /// <inheritdoc/>
        public Command SchemaCommand(IConnection connection)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            //command.CommandText = DbScript.DbCatalogItem;
            command.Parameters.Add(new SqlParameter("@Server", SqlDbType.NVarChar) { Value = connection.ServerName });
            return command;

        }
    }
}
