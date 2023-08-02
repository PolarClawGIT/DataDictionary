using DataDictionary.DataLayer.ApplicationData;
using DataDictionary.DataLayer.DomainData;
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
    public interface IDbCatalogItem : IDbCatalogName, IDbIsSystem, IBindingTableRow
    {
        //Guid? CatalogId { get; }
        String? SourceServerName { get; }
    }

    public class DbCatalogItem : BindingTableRow, IDbCatalogItem, INotifyPropertyChanged
    {
        public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } protected set { SetValue<Guid>("CatalogId", value); } }
        public virtual String? CatalogName { get { return GetValue("CatalogName"); } }
        public virtual String? SourceServerName { get { return GetValue("SourceServerName"); } }
        public virtual Boolean IsSystem { get { return CatalogName is "tempdb" or "master" or "msdb" or "model"; } }

        public DbCatalogItem() : base()
        { CatalogId = Guid.NewGuid(); }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("CatalogName", typeof(String)){ AllowDBNull = false},
            new DataColumn("SourceServerName", typeof(String)){ AllowDBNull = false},
            new DataColumn("SysStart", typeof(DateTime)){ AllowDBNull = true},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static Command GetSchema(IConnection connection)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = DbScript.DbCatalogItem;
            command.Parameters.Add(new SqlParameter("@Server", SqlDbType.NVarChar) { Value = connection.ServerName });
            return command;
        }

        public static Command GetData(IConnection connection, IModelIdentifier modelId)
        { return GetData(connection, (modelId.ModelId, null, null)); }

        static Command GetData(IConnection connection, (Guid? modelId, Guid? catalogId, String? catalogName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDatabaseCatalog]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@CatalogId", parameters.catalogId);
            command.AddParameter("@CatalogName", parameters.catalogName);
            return command;
        }

        public static Command SetData(IConnection connection, IModelIdentifier modelId, IBindingTable<DbCatalogItem> source)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDatabaseCatalog]";
            command.AddParameter("@ModelId", modelId.ModelId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDatabaseCatalog]", source);
            return command;
        }

        public override String ToString()
        { return new DbCatalogName(this).ToString(); }
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
