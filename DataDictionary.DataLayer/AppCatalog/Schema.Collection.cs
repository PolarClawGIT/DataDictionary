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
        where TItem : BindingTableRow, ISchemaItem, ICatalogKey, ISchemaKeyName, new()
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
        /// Imports the Catalog InformationSchema value.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="key"></param>
        public virtual void ImportSchema(IConnection connection, ICatalogKey key)
        {
            //TODO: Test, Then move to InfoSchema.

            IEnumerable<SchemaInformationSchema> schemas = SchemaInformationSchema.GetSchema(connection);
            CatalogKey catalogKey = new CatalogKey(key);

            var allKeys = this.Where(w => catalogKey.Equals(w)).
                Select(s => new SchemaKeyName(s)).
                Union(schemas.Select(s => new SchemaKeyName(s)));

            foreach (var schemaKey in allKeys)
            {
                var oldValue = this.FirstOrDefault(w => schemaKey.Equals(w));
                var newValue = schemas.FirstOrDefault(w => schemaKey.Equals(w));

                if (oldValue is SchemaItem oldMatches && newValue is SchemaInformationSchema newMatches)
                {   // Update Old, Nothing really to do. No non-key values
                    oldMatches.DatabaseName = newMatches.DatabaseName;
                    oldMatches.SchemaName = newMatches.SchemaName;
                }
                else if(oldValue is SchemaItem newMissing) 
                { // Delete Old
                    this.Remove(schemaKey);
                }
                else if(newValue is SchemaInformationSchema oldMissing) 
                { // Add New
                    TItem newItem = new TItem();

                    if (newItem is SchemaItem value)
                    {
                        value.CatalogId = key.CatalogId;
                        value.DatabaseName = oldMissing.DatabaseName;
                        value.SchemaName = oldMissing.SchemaName;
                        Add(newItem);
                    }
                    else
                    {
                        Exception ex = new InvalidOperationException("Could not Import Schema");
                        ex.Data.Add("Expected type", nameof(SchemaItem));
                        ex.Data.Add("Actual type", newItem.GetType().Name);
                        ex.Data.Add("Rows returned", schemas.Count());
                        throw ex;
                    }
                }

            }

        }
    }
}
