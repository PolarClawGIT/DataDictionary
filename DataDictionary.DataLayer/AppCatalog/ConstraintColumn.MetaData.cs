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
    /// Class used to store the Information Schema of a Catalog Constraint Column
    /// </summary>
    public class ConstraintColumnMetaData : BindingTableRow, IConstraintColumn
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
        public Int32? OrdinalPosition { get { return GetValue<Int32>(nameof(OrdinalPosition)); } }

        /// <inheritdoc/>
        public String? ColumnName { get { return GetValue(nameof(ColumnName)); } }

        /// <inheritdoc/>
        public String? ReferencedColumnName { get { return GetValue(nameof(ReferencedColumnName)); } }

        /// <inheritdoc/>
        public String? ReferencedSchemaName { get { return GetValue(nameof(ReferencedSchemaName)); } }

        /// <inheritdoc/>
        public String? ReferencedTableName { get { return GetValue(nameof(ReferencedTableName)); } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(DatabaseName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(TableName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(ConstraintName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(ColumnName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(OrdinalPosition), typeof(int)){ AllowDBNull = true},
            new DataColumn(nameof(ReferencedSchemaName), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(ReferencedTableName), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(ReferencedColumnName), typeof(string)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <summary>
        /// Gets the Information Schema from the Database
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static IEnumerable<IConstraintColumn> GetSchema(IConnection connection)
        {
            BindingTable<ConstraintColumnMetaData> schemas = new BindingTable<ConstraintColumnMetaData>();
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = ConstraintColumn.TSql_InformationSchema;

            schemas.Load(connection.ExecuteReader(command));

            return schemas;
        }
    }
}
