using Microsoft.Data.SqlClient;
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
    public interface IDbTableItem : IDbTableName, IDbIsSystem
    {
        Guid? CatalogId { get; }
        String? TableType { get; }
    }

    public class DbTableItem : BindingTableRow, IDbTableItem, INotifyPropertyChanged
    {
        public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } }
        public String? CatalogName { get { return GetValue("TABLE_CATALOG"); } }
        public String? SchemaName { get { return GetValue("TABLE_SCHEMA"); } }
        public String? TableName { get { return GetValue("TABLE_NAME"); } }
        public String? TableType { get { return GetValue("TABLE_TYPE"); } }
        public Boolean IsSystem { get { return TableName is "__RefactorLog" or "sysdiagrams"; } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(String)){ AllowDBNull = true},
            new DataColumn("TABLE_CATALOG", typeof(String)){ AllowDBNull = false},
            new DataColumn("TABLE_SCHEMA", typeof(String)){ AllowDBNull = false},
            new DataColumn("TABLE_NAME", typeof(String)){ AllowDBNull = false},
            new DataColumn("TABLE_TYPE", typeof(String)){ AllowDBNull = false},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static IDataReader GetSchema(IConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = DbScript.DbTableItem;

            return connection.ExecuteReader(command);
        }

        public virtual SqlCommand GetProperties(IConnection connection)
        {
            return (new DbExtendedPropertyGetCommand(connection)
            { Level0Name = SchemaName, Level0Type = "SCHEMA", Level1Name = TableName, Level1Type = "TABLE" }).
            GetCommand();
        }
    }
}
