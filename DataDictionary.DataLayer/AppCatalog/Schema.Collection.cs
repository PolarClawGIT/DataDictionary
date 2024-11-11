using DataDictionary.DataLayer.DatabaseData;
using DataDictionary.DataLayer.ModelData;
using Microsoft.Data.SqlClient;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Generic Base class for Catalog Schema Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class SchemaCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<ICatalogKey>, IReadData<ISchemaKey>,
        IWriteData<IModelKey>, IWriteData<ICatalogKey>,
        IRemoveItem<ICatalogKey>, IRemoveItem<ISchemaKeyName>,
        ITemporalData<ICatalogKey>, ITemporalData<ISchemaKey>
        where TItem : BindingTableRow, ISchemaItem, ICatalogKey, ISchemaKeyName, new()
    {
        /// <inheritdoc/>
        [Obsolete()]
        public Command SchemaCommand(IConnection connection, ICatalogKey catalogKey)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = DbScript.DbSchemaItem;
            command.Parameters.Add(new SqlParameter(SqlScript.Catalog.CatalogId, SqlDbType.UniqueIdentifier) { Value = catalogKey.CatalogId });
            return command;
        }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, (modelKey.ModelId, null, null, null, false)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ICatalogKey catalogKey)
        { return LoadCommand(connection, (null, catalogKey.CatalogId, null, null, false)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ISchemaKey key)
        { return LoadCommand(connection, (null, null , key.SchemaId, null, false)); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, ICatalogKey key)
        { return LoadCommand(connection, (null, key.CatalogId, null, null, true)); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, ISchemaKey key)
        { return LoadCommand(connection, (null, null, key.SchemaId, null, true)); }

        Command LoadCommand(IConnection connection, (Guid? modelId, Guid? catalogId, Guid? schemaId, DateTime? asOfUtcDate, Boolean includeHistory) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = SqlScript.Schema.GetMethod;
            command.AddParameter(SqlScript.Model.ModelId, parameters.modelId);
            command.AddParameter(SqlScript.Catalog.CatalogId, parameters.catalogId);
            command.AddParameter(SqlScript.Schema.SchemaId, parameters.schemaId);
            command.AddParameter(SqlScript.Common.AsOfUtcDate, parameters.asOfUtcDate);
            command.AddParameter(SqlScript.Common.IncludeHistory, parameters.includeHistory);

            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelKey)
        { return SaveCommand(connection, (modelKey.ModelId, null, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ICatalogKey catalogKey)
        { return SaveCommand(connection, (null, catalogKey.CatalogId, null)); }

        Command SaveCommand(IConnection connection, (Guid? modelId, Guid? catalogId, Guid? schemaId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = SqlScript.Schema.SetMethod;
            command.AddParameter(SqlScript.Model.ModelId, parameters.modelId);
            command.AddParameter(SqlScript.Catalog.CatalogId, parameters.catalogId);
            command.AddParameter(SqlScript.Schema.SchemaId, parameters.schemaId);

            IEnumerable<TItem> data = this.Where(w => parameters.catalogId is null || w.CatalogId == parameters.catalogId);
            command.AddParameter(SqlScript.Common.Data, SqlScript.Schema.TableType, data);
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
        public virtual void Remove(ISchemaKeyName schemaItem)
        {
            SchemaKeyName key = new SchemaKeyName(schemaItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
