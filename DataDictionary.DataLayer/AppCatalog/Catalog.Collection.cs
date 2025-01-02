using DataDictionary.DataLayer.AppModel;
using System.Data;
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
        IWriteData, IWriteData<ICatalogKey>,
        IRemoveItem<ICatalogKey>,
        ITemporalData, ITemporalData<ICatalogKey>
        where TItem : CatalogItem, ICatalogItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, catalogId: null); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey key)
        { return LoadCommand(connection, modelId: key.ModelId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ICatalogKey key)
        { return LoadCommand(connection, catalogId: key.CatalogId); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection)
        { return LoadCommand(connection, includeHistory: true); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, ICatalogKey key)
        { return LoadCommand(connection, catalogId: key.CatalogId, includeHistory: true); }

        Command LoadCommand(IConnection connection,
            Guid? modelId = null, Guid? catalogId = null,
            DateTime? asOfUtcDate = null, Boolean includeHistory = false)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Catalog.GetProcedure;
            command.AddParameter(Model.ModelId, modelId);
            command.AddParameter(Catalog.CatalogId, catalogId);
            command.AddParameter(Temporal.AsOfUtcDate, asOfUtcDate);
            command.AddParameter(Temporal.IncludeHistory, includeHistory);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection, catalogId: null); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ICatalogKey catalogKey)
        { return SaveCommand(connection, catalogId: catalogKey.CatalogId); }

        Command SaveCommand(IConnection connection, Guid? catalogId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Catalog.SetProcedure;
            command.AddParameter(Catalog.CatalogId, catalogId);

            IEnumerable<TItem> data = this.Where(w => catalogId is null || w.CatalogId == catalogId);
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
        /// <param name="catalogs"></param>
        /// <returns></returns>
        /// <remarks>Merge behavior: existing values are updated, new values are added</remarks>
        public virtual ICatalogKey Import(IEnumerable<ICatalog> catalogs)
        {
            CatalogKey? catalogKey = null;

            IEnumerable<CatalogKeyName> allKeys = this.
                Select(s => new CatalogKeyName(s)).
                Union(catalogs.Select(s => new CatalogKeyName(s))).ToList();

            foreach (var key in allKeys) // Only one value is expected
            {
                TItem? oldValue = this.FirstOrDefault(w => key.Equals(w));
                ICatalog? newValue = catalogs.FirstOrDefault(w => key.Equals(w));

                if (oldValue is CatalogItem oldMatches && newValue is ICatalog newMatches)
                { // Update Old
                    oldMatches.Update(newMatches);
                    catalogKey = new CatalogKey(oldMatches);
                }
                else if (oldValue is CatalogItem newMissing)
                { // Nothing to do, old items are not removed this way
                    catalogKey = new CatalogKey(newMissing);
                }
                else if (newValue is ICatalog oldMissing)
                { // Add New
                    TItem newItem = CatalogItem.Create<TItem>(oldMissing);
                    catalogKey = new CatalogKey(newItem);
                    Add(newItem);
                }
            }

            return catalogKey ?? new CatalogKey();
        }
    }
}
