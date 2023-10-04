using DataDictionary.DataLayer.ApplicationData.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DatabaseData.Catalog
{
    /// <summary>
    /// Generic Base class for Database Catalogs Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class DbCatalogCollection<TItem> : BindingTable<TItem>, IReadData, IReadData<IModelKey>, IReadSchema, IWriteData<IModelKey>
        where TItem : DbCatalogItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = DbScript.DbCatalogItem;
            command.Parameters.Add(new SqlParameter("@Server", SqlDbType.NVarChar) { Value = connection.ServerName });
            return command;
        }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelId)
        { return LoadCommand(connection, (modelId.ModelId, null, null)); }

        Command LoadCommand(IConnection connection, (Guid? modelId, Guid? catalogId, string? catalogName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDatabaseCatalog]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@CatalogId", parameters.catalogId);
            command.AddParameter("@CatalogName", parameters.catalogName);
            return command;
        }

        /// <inheritdoc/>
        public Command SchemaCommand(IConnection connection)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = DbScript.DbCatalogItem;
            command.Parameters.Add(new SqlParameter("@Server", SqlDbType.NVarChar) { Value = connection.ServerName });
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelId)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDatabaseCatalog]";
            command.AddParameter("@ModelId", modelId.ModelId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDatabaseCatalog]", this);
            return command;
        }
    }

    /// <summary>
    /// Default List/Collection of Database Catalogs Items
    /// </summary>
    public class DbCatalogCollection : DbCatalogCollection<DbCatalogItem>
    { }
}
