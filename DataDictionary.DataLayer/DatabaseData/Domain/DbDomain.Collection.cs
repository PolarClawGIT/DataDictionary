using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
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

namespace DataDictionary.DataLayer.DatabaseData.Domain
{
    /// <summary>
    /// Generic Base class for Database Domain Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class DbDomainCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<ICatalogKey>, IReadSchema<ICatalogKey>,
        IWriteData<IModelKey>, IWriteData<ICatalogKey>,
        IRemoveItem<ICatalogKey>, IRemoveItem<IDbDomainKeyName>
        where TItem : BindingTableRow, IDbDomainItem, ICatalogKey, IDbDomainKeyName, new()
    {
        /// <inheritdoc/>
        public Command SchemaCommand(IConnection connection, ICatalogKey catalogKey)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = DbScript.DbDomainItem;
            command.Parameters.Add(new SqlParameter("@CatalogId", SqlDbType.UniqueIdentifier) { Value = catalogKey.CatalogId });
            return command;
        }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelId)
        { return LoadCommand(connection, (modelId.ModelId, null, null, null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ICatalogKey catalogKey)
        { return LoadCommand(connection, (null, catalogKey.CatalogId, null, null, null)); }

        Command LoadCommand(IConnection connection, (Guid? modelId, Guid? catalogId, string? catalogName, string? schemaName, string? domainName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDatabaseDomain]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@CatalogId", parameters.catalogId);
            command.AddParameter("@CatalogName", parameters.catalogName);
            command.AddParameter("@SchemaName", parameters.schemaName);
            command.AddParameter("@DomainName", parameters.domainName);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelId)
        { return SaveCommand(connection, (modelId.ModelId, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ICatalogKey catalogKey)
        { return SaveCommand(connection, (null, catalogKey.CatalogId)); }

        Command SaveCommand(IConnection connection, (Guid? modelId, Guid? catalogId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDatabaseDomain]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@CatalogId", parameters.catalogId);

            IEnumerable<TItem> data = this.Where(w => parameters.catalogId is null || w.CatalogId == parameters.catalogId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDatabaseDomain]", data);
            return command;
        }

        /// <inheritdoc/>
        public virtual void Remove(ICatalogKey catalogItem)
        {
            CatalogKey key = new CatalogKey(catalogItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }

        /// <inheritdoc/>
        public virtual void Remove(IDbDomainKeyName domainItem)
        {
            DbDomainKeyName key = new DbDomainKeyName(domainItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }

    /// <summary>
    /// Default List/Collection of Database Domain Items
    /// </summary>
    public class DbDomainCollection : DbDomainCollection<DbDomainItem>
    { }
}
