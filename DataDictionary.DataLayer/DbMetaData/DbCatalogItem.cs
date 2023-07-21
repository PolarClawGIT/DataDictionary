using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbCatalogItem : IDbCatalogName, IDbIsSystem
    {
        //Guid? CatalogId { get; }
        String? SourceServerName { get; }
    }

    public class DbCatalogItem : BindingTableRow, IDbCatalogItem, INotifyPropertyChanged
    {
        //public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } }
        public virtual String? CatalogName { get { return GetValue("CatalogName"); } }
        public virtual String? SourceServerName { get { return GetValue("SourceServerName"); } }
        public virtual Boolean IsSystem { get { return CatalogName is "tempdb" or "master" or "msdb" or "model"; } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("CatalogName", typeof(String)){ AllowDBNull = false},
            new DataColumn("SourceServerName", typeof(String)){ AllowDBNull = false},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static IDataReader GetSchema(IConnection connection)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = DbScript.DbCatalogItem;
            command.Parameters.Add(new SqlParameter("@Server", SqlDbType.NVarChar) { Value = connection.ServerName });

            return connection.ExecuteReader(command);
        }
    }

    public static class DbCatalogItemExtension
    {
        public static DbCatalogItem? GetCatalog(this IEnumerable<DbCatalogItem> source, IDbCatalogName item)
        {
            DbCatalogName itemName = new DbCatalogName(item);
            return source.FirstOrDefault(w => itemName == w);
        }

        public static DbCatalogItem? GetCatalog(this IDbCatalogName item, IEnumerable<DbCatalogItem> source)
        {
            DbCatalogName itemName = new DbCatalogName(item);
            return source.FirstOrDefault(w => itemName == w);
        }

        public static DbCatalogItem? GetCatalog(this IEnumerable<DbCatalogItem> source, (String ServerName, String DatabaseName) item)
        {
            return source.FirstOrDefault(
                w => item.DatabaseName.Equals(w.CatalogName, ModelFactory.CompareString) &&
                item.ServerName.Equals(w.SourceServerName, ModelFactory.CompareString));
        }
    }
}
