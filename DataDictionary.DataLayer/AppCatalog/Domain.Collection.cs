using DataDictionary.DataLayer.AppModel;
using DataDictionary.DataLayer.ModelData;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Generic Base class for Database Domain Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class DomainCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<ICatalogKey>, IReadData<IDomainKey>,
        IWriteData, IWriteData<ICatalogKey>, IWriteData<IDomainKey>,
        IRemoveItem<ICatalogKey>, IRemoveItem<IDomainKeyName>,
        ITemporalData<ICatalogKey>, ITemporalData<IDomainKey>, IInfomationSchemaCollection<IDomain>
        where TItem : DomainItem, IDomainItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ICatalogKey catalogKey)
        { return LoadCommand(connection, catalogId: catalogKey.CatalogId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IDomainKey domainKey)
        { return LoadCommand(connection, domainId: domainKey.DomainId); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, IDomainKey domainKey)
        { return LoadCommand(connection, domainId: domainKey.DomainId, includeHistory: true); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, ICatalogKey catalogKey)
        { return LoadCommand(connection, catalogId: catalogKey.CatalogId, includeHistory: true); }

        Command LoadCommand(IConnection connection,
            Guid? modelId = null, Guid? catalogId = null, Guid? domainId = null,
            DateTime? asOfUtcDate = null, Boolean includeHistory = false)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Domain.GetProcedure;
            command.AddParameter(Model.ModelId, modelId);
            command.AddParameter(Catalog.CatalogId, catalogId);
            command.AddParameter(Domain.DomainId, domainId);
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

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IDomainKey domainKey)
        { return SaveCommand(connection, domainId: domainKey.DomainId); }

        Command SaveCommand(IConnection connection,
            Guid? modelId = null, Guid? catalogId = null, Guid? domainId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Domain.SetProcedure;
            command.AddParameter(Model.ModelId, modelId);
            command.AddParameter(Catalog.CatalogId, catalogId);
            command.AddParameter(Domain.DomainId, domainId);

            IEnumerable<TItem> data = this.Where(w =>
                (catalogId is null || w.CatalogId == catalogId) &&
                (domainId is null || w.DomainId == domainId));
            command.AddParameter(WriteData.Data, Domain.TableType, data);
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
        public virtual void Remove(IDomainKeyName domainItem)
        {
            DomainKeyName key = new DomainKeyName(domainItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }

        /// <inheritdoc/>
        public virtual void Import(ICatalogKey catalogKey, IEnumerable<IDomain> domains)
        {
            IEnumerable<DomainKeyName> allKeys = this.Where(w => catalogKey.Equals(w)).
                Select(s => new DomainKeyName(s)).
                Union(domains.Select(s => new DomainKeyName(s)));

            foreach (var key in allKeys)
            {
                TItem? oldValue = this.FirstOrDefault(w => key.Equals(w));
                IDomain? newValue = domains.FirstOrDefault(w => key.Equals(w));

                if (oldValue is DomainItem oldMatches && newValue is IDomain newMatches)
                { oldMatches.Update(newMatches); } // Update Old
                else if (oldValue is DomainItem newMissing)
                { this.Remove(key); } // Delete Old
                else if (newValue is IDomain oldMissing)
                { Add(DomainItem.Create<TItem>(catalogKey, oldMissing)); }// Add New
            }
        }
    }
}
