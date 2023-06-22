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
    public interface IDbTableName : IDbSchemaName
    {
        public String? TableName { get; }
    }

    public interface IDbTableItem : IDbTableName, IDbIsSystem
    {
        public String? TableType { get; }
    }

    public class DbTableItem : BindingTableRow, IDbTableItem, INotifyPropertyChanged
    {
        public String? CatalogName { get { return GetValue("TABLE_CATALOG"); } }
        public String? SchemaName { get { return GetValue("TABLE_SCHEMA"); } }
        public String? TableName { get { return GetValue("TABLE_NAME"); } }
        public String? TableType { get { return GetValue("TABLE_TYPE"); } }
        public Boolean IsSystem { get { return TableName is "__RefactorLog" or "sysdiagrams"; } }

        public static IDataReader GetDataReader(IConnection connection)
        { return connection.GetReader(Schema.Collection.Tables); }

        public virtual SqlCommand GetProperties(IConnection connection)
        {
            return (new DbExtendedPropertyGetCommand(connection)
            { Level0Name = SchemaName, Level0Type = "SCHEMA", Level1Name = TableName, Level1Type = "TABLE" }).
            GetCommand();
        }
    }
}
