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
    /// Class used to store the Object reference to other objects
    /// </summary>
    public class ReferenceMetaData : BindingTableRow, IReference
    {
        /// <inheritdoc/>
        public String? DatabaseName { get { return GetValue(nameof(DatabaseName)); } }

        /// <inheritdoc/>
        public String? SchemaName { get { return GetValue(nameof(SchemaName)); } }

        /// <inheritdoc/>
        public String? ObjectName { get { return GetValue(nameof(SchemaName)); } }

        /// <inheritdoc/>
        public String? ObjectType { get { return GetValue(nameof(SchemaName)); } }

        /// <inheritdoc/>
        public String? ReferencedDatabaseName { get { return GetValue(nameof(SchemaName)); } }

        /// <inheritdoc/>
        public String? ReferencedSchemaName { get { return GetValue(nameof(SchemaName)); } }

        /// <inheritdoc/>
        public String? ReferencedObjectName { get { return GetValue(nameof(SchemaName)); } }

        /// <inheritdoc/>
        public String? ReferencedColumnName { get { return GetValue(nameof(SchemaName)); } }

        /// <inheritdoc/>
        public String? ReferencedType { get { return GetValue(nameof(SchemaName)); } }

        /// <inheritdoc/>
        public Boolean? IsCallerDependent
        { get { return GetValue<Boolean>(nameof(IsCallerDependent), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public Boolean? IsAmbiguous 
        { get { return GetValue<Boolean>(nameof(IsAmbiguous), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public Boolean? IsSelected
        { get { return GetValue<Boolean>(nameof(IsSelected), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public Boolean? IsModified
        { get { return GetValue<Boolean>(nameof(IsModified), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public Boolean? IsSelectAll
        { get { return GetValue<Boolean>(nameof(IsSelectAll), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public Boolean? IsAllColumnsFound
        { get { return GetValue<Boolean>(nameof(IsAllColumnsFound), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public Boolean? IsInsertAll
        { get { return GetValue<Boolean>(nameof(IsInsertAll), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public Boolean? IsIncomplete
        { get { return GetValue<Boolean>(nameof(IsIncomplete), BindingItemParsers.BooleanTryParse); } }


        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(DatabaseName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(ObjectName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(ObjectType), typeof(String)){ AllowDBNull = false},

            new DataColumn(nameof(ReferencedDatabaseName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ReferencedSchemaName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ReferencedObjectName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ReferencedColumnName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ReferencedType), typeof(String)){ AllowDBNull = true},

            new DataColumn(nameof(IsCallerDependent), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(IsAmbiguous), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(IsSelected), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(IsModified), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(IsSelectAll), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(IsAllColumnsFound), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(IsInsertAll), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(IsIncomplete), typeof(bool)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <summary>
        /// Gets the Information Schema from the Database
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static IEnumerable<IReference> GetSchema(IConnection connection)
        {
            BindingTable<ReferenceMetaData> schemas = new BindingTable<ReferenceMetaData>();
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;

            command.CommandText = SchemaScript.GetInformationSchema(typeof(Reference));

            schemas.Load(connection.ExecuteReader(command));

            return schemas;
        }

    }
}
