using DataDictionary.DataLayer.AppModel;
using DataDictionary.DataLayer.DatabaseData;
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

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Generic Base class for Database Catalogs Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class CatalogCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<IModelKey>, IReadData<ICatalogKey>,
        IWriteData<IModelKey>, IWriteData<ICatalogKey>,
        IRemoveItem<ICatalogKey>,
        ITemporalData, ITemporalData<ICatalogKey>
        where TItem : CatalogItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, (null, null, null, false)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey key)
        { return LoadCommand(connection, (key.ModelId, null, null, false)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ICatalogKey key)
        { return LoadCommand(connection, (null, key.CatalogId, null, false)); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection)
        { return LoadCommand(connection, (null, null, null, true)); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, ICatalogKey key)
        { return LoadCommand(connection, (null, key.CatalogId, null, true)); }

        Command LoadCommand(IConnection connection, (Guid? modelId, Guid? catalogId, DateTime? asOfUtcDate, Boolean includeHistory) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Catalog.GetProcedure;
            command.AddParameter(Model.ModelId, parameters.modelId);
            command.AddParameter(Catalog.CatalogId, parameters.catalogId);
            command.AddParameter(Temporal.AsOfUtcDate, parameters.asOfUtcDate);
            command.AddParameter(Temporal.IncludeHistory, parameters.includeHistory);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelKey)
        { return SaveCommand(connection, (modelKey.ModelId, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ICatalogKey catalogKey)
        { return SaveCommand(connection, (null, catalogKey.CatalogId)); }

        Command SaveCommand(IConnection connection, (Guid? modelId, Guid? catalogId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Catalog.SetProcedure;
            command.AddParameter(Model.ModelId, parameters.modelId);
            command.AddParameter(Catalog.CatalogId, parameters.catalogId);

            IEnumerable<TItem> data = this.Where(w => parameters.catalogId is null || w.CatalogId == parameters.catalogId);
            command.AddParameter(WriteData.Data, Catalog.TableType, data);
            return command;
        }

        /// <inheritdoc/>
        public virtual void Remove(ICatalogKey catalogItem)
        {
            CatalogKey key = new CatalogKey(catalogItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }

        /// <summary>
        /// Imports the InformationSchema values.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        /// <remarks>Merge behavior: existing values are updated, new values are added</remarks>
        public virtual ICatalogKey Import(IConnection connection)
        {
            IEnumerable<CatalogMetaData> schemas = CatalogMetaData.GetSchema(connection);
            CatalogKey? catalogKey = null;

            IEnumerable<CatalogKeyName> allKeys = this.
                Select(s => new CatalogKeyName(s)).
                Union(schemas.Select(s => new CatalogKeyName(s)));

            foreach (var schemaKey in allKeys) // Only one value is expected
            {
                TItem? oldValue = this.FirstOrDefault(w => schemaKey.Equals(w));
                CatalogMetaData? newValue = schemas.FirstOrDefault(w => schemaKey.Equals(w));

                if (oldValue is CatalogItem oldMatches && newValue is CatalogMetaData newMatches)
                { // Update Old
                    oldMatches.SourceDate = DateTime.Now;
                    oldMatches.ServerName = newMatches.ServerName;
                    catalogKey = new CatalogKey(oldMatches);
                }
                else if (oldValue is CatalogItem newMissing)
                { // Nothing to do, old items are not removed this way
                    catalogKey = new CatalogKey(newMissing);
                }
                else if (newValue is CatalogMetaData oldMissing) // Add New
                {
                    TItem newItem = CatalogItem.Create<TItem>(oldMissing);
                    catalogKey = new CatalogKey(newItem);
                }
            }

            return catalogKey ?? new CatalogKey();
        }
    }
}
