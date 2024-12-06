using DataDictionary.DataLayer.AppModel;
using DataDictionary.DataLayer.ModelData;
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
        IWriteData, IWriteData<ICatalogKey>, IWriteData<ISchemaKey>,
        IRemoveItem<ICatalogKey>, IRemoveItem<ISchemaKeyName>,
        ITemporalData<ICatalogKey>, ITemporalData<ISchemaKey>, IInfomationSchemaCollection<ISchema>
        where TItem : SchemaItem, ISchemaItem, new()
    {

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ICatalogKey catalogKey)
        { return LoadCommand(connection, catalogId: catalogKey.CatalogId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ISchemaKey schemaKey)
        { return LoadCommand(connection, schemaId: schemaKey.SchemaId); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, ICatalogKey catalogKey)
        { return LoadCommand(connection, catalogId: catalogKey.CatalogId, includeHistory: true); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, ISchemaKey schemaKey)
        { return LoadCommand(connection, schemaId: schemaKey.SchemaId, includeHistory: true); }

        Command LoadCommand(IConnection connection,
            Guid? modelId = null, Guid? catalogId = null, Guid? schemaId = null,
            DateTime? asOfUtcDate = null, Boolean includeHistory = false)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Schema.GetProcedure;
            command.AddParameter(Model.ModelId, modelId);
            command.AddParameter(Catalog.CatalogId, catalogId);
            command.AddParameter(Schema.SchemaId, schemaId);
            command.AddParameter(Temporal.AsOfUtcDate, asOfUtcDate);
            command.AddParameter(Temporal.IncludeHistory, includeHistory);

            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection, catalogId: null); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ICatalogKey catalogKey)
        { return SaveCommand(connection, catalogId:catalogKey.CatalogId); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ISchemaKey schemaKey)
        { return SaveCommand(connection, schemaId: schemaKey.SchemaId); }

        Command SaveCommand(IConnection connection, Guid? catalogId = null, Guid? schemaId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Schema.SetProcedure;
            command.AddParameter(Catalog.CatalogId, catalogId);
            command.AddParameter(Schema.SchemaId, schemaId);

            IEnumerable<TItem> data = this.Where(w =>
                (catalogId is null || w.CatalogId == catalogId) &&
                (schemaId is null || w.SchemaId == schemaId));
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
                Union(schemas.Select(s => new SchemaKeyName(s))).ToList();

            foreach (var key in allKeys)
            {
                TItem? oldValue = this.FirstOrDefault(w => key.Equals(w));
                ISchema? newValue = schemas.FirstOrDefault(w => key.Equals(w));

                if (oldValue is SchemaItem oldMatches && newValue is ISchema newMatches)
                { oldMatches.Update(newMatches); } // Update Old
                else if (oldValue is SchemaItem newMissing)
                { this.Remove(key); } // Delete Old
                else if (newValue is ISchema oldMissing)
                { Add(SchemaItem.Create<TItem>(catalogKey, oldMissing)); }// Add New
            }
        }
    }
}
