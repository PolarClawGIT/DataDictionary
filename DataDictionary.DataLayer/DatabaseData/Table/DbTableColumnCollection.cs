using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Schema;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DatabaseData.Table
{
    /// <summary>
    /// Generic Base class for Database Table Columns
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class DbTableColumnCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<IDbCatalogKey>, IReadSchema<IDbCatalogKey>,
        IWriteData<IModelKey>, IWriteData<IDbCatalogKey>,
        IRemoveData<IDbCatalogKey>, IRemoveData<IDbSchemaKeyName>, IRemoveData<IDbTableKeyName>, IRemoveData<IDbTableColumnKeyName>
        where TItem : BindingTableRow, IDbTableColumnItem, IDbCatalogKey, IDbSchemaKeyName, IDbTableKeyName, IDbTableColumnKeyName, new()
    {
        /// <inheritdoc/>
        public Command SchemaCommand(IConnection connection, IDbCatalogKey catalogKey)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = DbScript.DbTableColumnItem;
            command.Parameters.Add(new SqlParameter("@CatalogId", SqlDbType.UniqueIdentifier) { Value = catalogKey.CatalogId });
            return command;
        }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, (modelKey.ModelId, null, null, null, null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IDbCatalogKey catalogKey)
        { return LoadCommand(connection, (null, catalogKey.CatalogId, null, null, null, null)); }

        Command LoadCommand(IConnection connection, (Guid? modelId, Guid? catalogId, string? catalogName, string? schemaName, string? tableName, string? columnName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDatabaseTableColumn]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@CatalogId", parameters.catalogId);
            command.AddParameter("@CatalogName", parameters.catalogName);
            command.AddParameter("@SchemaName", parameters.schemaName);
            command.AddParameter("@TableName", parameters.tableName);
            command.AddParameter("@ColumnName", parameters.columnName);
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
            command.CommandText = "[App_DataDictionary].[procSetDatabaseTableColumn]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@CatalogId", parameters.catalogId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDatabaseTableColumn]", this);
            return command;
        }

        /// <inheritdoc/>
        public void Remove(IDbCatalogKey catalogItem)
        {
            DbCatalogKey key = new DbCatalogKey(catalogItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }

        /// <inheritdoc/>
        public void Remove(IDbSchemaKeyName schemaItem)
        {
            DbSchemaKeyName key = new DbSchemaKeyName(schemaItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }

        /// <inheritdoc/>
        public void Remove(IDbTableKeyName tableItem)
        {
            DbTableKeyName key = new DbTableKeyName(tableItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }

        /// <inheritdoc/>
        public void Remove(IDbTableColumnKeyName columnItem)
        {
            DbTableColumnKeyName key = new DbTableColumnKeyName(columnItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }

    /// <summary>
    /// Default List/Collection of Database Table Columns
    /// </summary>
    public class DbTableColumnCollection: DbTableColumnCollection<DbTableColumnItem>
    { }
}
