using DataDictionary.DataLayer.ModelData;
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
    public abstract class DbCatalogCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<IModelKey>, IReadData<IDbCatalogKey>, IReadSchema<IDbCatalogKey>,
        IWriteData<IModelKey>, IWriteData<IDbCatalogKey>,
        IRemoveData<IDbCatalogKey>
        where TItem : BindingTableRow, IDbCatalogItem, IDbCatalogKey, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, (null, null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, (modelKey.ModelId, null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IDbCatalogKey catalogKey)
        { return LoadCommand(connection, (null, catalogKey.CatalogId, null)); }

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
        public Command SchemaCommand(IConnection connection, IDbCatalogKey catalogKey)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = DbScript.DbCatalogItem;
            command.Parameters.Add(new SqlParameter("@Server", SqlDbType.NVarChar) { Value = connection.ServerName });
            command.Parameters.Add(new SqlParameter("@CatalogId", SqlDbType.UniqueIdentifier) { Value = catalogKey.CatalogId});
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelKey)
        { return SaveCommand(connection, (modelKey.ModelId, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IDbCatalogKey catalogKey)
        { return SaveCommand(connection, (null, catalogKey.CatalogId)); }

        Command SaveCommand(IConnection connection, (Guid? modelId, Guid? catalogId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDatabaseCatalog]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@CatalogId", parameters.catalogId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDatabaseCatalog]", this);
            return command;
        }

        /// <inheritdoc/>
        public void Remove(IDbCatalogKey catalogItem)
        {
            DbCatalogKey key = new DbCatalogKey(catalogItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }

    /// <summary>
    /// Default List/Collection of Database Catalogs Items
    /// </summary>
    public class DbCatalogCollection : DbCatalogCollection<DbCatalogItem>
    { }
}
