using DataDictionary.DataLayer.AppModel;
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
        where TItem : SchemaItem, new()
    {

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, (modelKey.ModelId, null, null, null, false)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ICatalogKey catalogKey)
        { return LoadCommand(connection, (null, catalogKey.CatalogId, null, null, false)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ISchemaKey key)
        { return LoadCommand(connection, (null, null, key.SchemaId, null, false)); }

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
            command.CommandText = Schema.GetProcedure;
            command.AddParameter(Model.ModelId, parameters.modelId);
            command.AddParameter(Catalog.CatalogId, parameters.catalogId);
            command.AddParameter(Schema.SchemaId, parameters.schemaId);
            command.AddParameter(Temporal.AsOfUtcDate, parameters.asOfUtcDate);
            command.AddParameter(Temporal.IncludeHistory, parameters.includeHistory);

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
            command.CommandText = Schema.GetProcedure;
            command.AddParameter(Model.ModelId, parameters.modelId);
            command.AddParameter(Catalog.CatalogId, parameters.catalogId);
            command.AddParameter(Schema.SchemaId, parameters.schemaId);

            IEnumerable<TItem> data = this.Where(w => parameters.catalogId is null || w.CatalogId == parameters.catalogId);
            command.AddParameter(WriteData.Data, Schema.TableType, data);
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

        /// <summary>
        /// Imports the InformationSchema values.
        /// </summary>
        /// <param name="catalogKey"></param>
        /// <param name="schemas"></param>
        /// <remarks>Merge behavior: existing values are updated (do nothing), new values are added, missing values are removed</remarks>
        public virtual void Import(ICatalogKey catalogKey, IEnumerable<ISchema> schemas)
        {
            IEnumerable<SchemaKeyName> allKeys = this.Where(w => catalogKey.Equals(w)).
                Select(s => new SchemaKeyName(s)).
                Union(schemas.Select(s => new SchemaKeyName(s)));

            foreach (var key in allKeys)
            {
                TItem? oldValue = this.FirstOrDefault(w => key.Equals(w));
                ISchema? newValue = schemas.FirstOrDefault(w => key.Equals(w));

                if (oldValue is SchemaItem oldMatches && newValue is ISchema newMatches)
                { } // Update Old, Nothing really to do.
                else if (oldValue is SchemaItem newMissing)
                { this.Remove(key); } // Delete Old
                else if (newValue is ISchema oldMissing)
                { Add(SchemaItem.Create<TItem>(catalogKey, oldMissing)); }// Add New
            }
        }
    }
}
