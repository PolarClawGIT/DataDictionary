using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbConstraintColumnItem: IDbConstraintKey, IDbCatalogKey, IBindingTableRow
    {
        String? ReferenceSchemaName { get; }
        String? ReferenceTableName { get; }
        String? ReferenceColumnName { get; }
        Int32? OrdinalPosition { get; } 
    }

    public class DbConstraintColumnItem : BindingTableRow, IDbConstraintColumnItem, INotifyPropertyChanged
    {
        public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } }
        public String? CatalogName { get { return GetValue("CatalogName"); } }
        public String? SchemaName { get { return GetValue("SchemaName"); } }
        public String? ConstraintName { get { return GetValue("ConstraintName"); } }
        public String? ReferenceSchemaName { get { return GetValue("ReferenceSchemaName"); } }
        public String? ReferenceTableName { get { return GetValue("ReferenceTableName"); } }
        public String? ReferenceColumnName { get { return GetValue("ReferenceColumnName"); } }
        public Int32? OrdinalPosition { get { return GetValue<Int32>("OrdinalPosition"); } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(String)){ AllowDBNull = true},
            new DataColumn("CatalogName", typeof(String)){ AllowDBNull = false},
            new DataColumn("SchemaName", typeof(String)){ AllowDBNull = false},
            new DataColumn("ConstraintName", typeof(String)){ AllowDBNull = false},
            new DataColumn("ReferenceSchemaName", typeof(String)){ AllowDBNull = false},
            new DataColumn("ReferenceTableName", typeof(String)){ AllowDBNull = false},
            new DataColumn("ReferenceColumnName", typeof(String)){ AllowDBNull = false},
            new DataColumn("OrdinalPosition", typeof(Int32)){ AllowDBNull = false},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static Command GetSchema(IConnection connection)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = DbScript.DbConstraintColumnItem;
            return command;
        }
    }

}
