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
    public interface IDbConstraintItem : IDbConstraintKey, IDbCatalogKey, IBindingTableRow, IDbConstraintKeyTableReference
    {
        String? ConstraintType { get; }
    }

    public class DbConstraintItem : BindingTableRow, IDbConstraintItem, INotifyPropertyChanged, IDbExtendedProperties
    {
        public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } }
        public String? CatalogName { get { return GetValue("CatalogName"); } }
        public String? SchemaName { get { return GetValue("SchemaName"); } }
        public String? ConstraintName { get { return GetValue("ConstraintName"); } }
        public String? ConstraintType { get { return GetValue("ConstraintType"); } }
        public String? ReferenceSchemaName { get { return GetValue("ReferenceSchemaName"); } }
        public String? ReferenceTableName { get { return GetValue("ReferenceTableName"); } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(String)){ AllowDBNull = true},
            new DataColumn("CatalogName", typeof(String)){ AllowDBNull = false},
            new DataColumn("SchemaName", typeof(String)){ AllowDBNull = false},
            new DataColumn("ConstraintName", typeof(String)){ AllowDBNull = false},
            new DataColumn("ConstraintType", typeof(String)){ AllowDBNull = false},
            new DataColumn("ReferenceSchemaName", typeof(String)){ AllowDBNull = false},
            new DataColumn("ReferenceTableName", typeof(String)){ AllowDBNull = false},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static Command GetSchema(IConnection connection)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = DbScript.DbConstraintItem;
            return command;
        }

        public virtual Command GetProperties(IConnection connection)
        {
            return (new DbExtendedPropertyGetCommand(connection)
            {   Level0Name = ReferenceSchemaName, Level0Type = "SCHEMA",
                Level1Name = ReferenceTableName, Level1Type = "TABLE",
                Level2Name = ConstraintName, Level2Type = "CONSTRAINT" }).
            GetCommand();
        }

        public override String ToString()
        { return new DbConstraintKey(this).ToString(); }
    }
}
