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
    /// Class used to store the Information Schema of a Catalog Routine
    /// </summary>
    public class RoutineMetaData : BindingTableRow, IRoutine
    {
        /// <inheritdoc/>
        public String DatabaseName 
        { get { return GetValue(nameof(DatabaseName)) ?? String.Empty; } }

        /// <inheritdoc/>
        public String SchemaName 
        { get { return GetValue(nameof(SchemaName)) ?? String.Empty; } }

        /// <inheritdoc/>
        public String? RoutineName 
        { get { return GetValue(nameof(RoutineName)) ?? String.Empty; } }

        /// <inheritdoc/>
        public String? RoutineType 
        { get { return GetValue(nameof(RoutineType)) ?? String.Empty; } }

        /// <summary>
        /// Constructor for Catalog Routine Information Schema
        /// </summary>
        public RoutineMetaData() : base() { }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(DatabaseName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(RoutineName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(RoutineType), typeof(String)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <summary>
        /// Gets the Information Schema from the Database
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static IEnumerable<IRoutine> GetSchema(IConnection connection)
        {
            BindingTable<RoutineMetaData> schemas = new BindingTable<RoutineMetaData>();
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = Routine.TSql_InformationSchema;

            schemas.Load(connection.ExecuteReader(command));

            return schemas;
        }
    }
}
