using DataDictionary.DataLayer.ModelData;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Generic Base class for Database Reference Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public class ReferenceCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<ICatalogKey>, IReadData<IReferenceKey>,
        IWriteData, IWriteData<ICatalogKey>, IWriteData<IReferenceKey>,
        IRemoveItem<ICatalogKey>,
        ITemporalData<ICatalogKey>, ITemporalData<IReferenceKey>, IInfomationSchemaCollection<IReference>
        where TItem : ReferenceItem, IReferenceItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId : modelKey.ModelId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ICatalogKey catalogKey)
        { return LoadCommand(connection, catalogId: catalogKey.CatalogId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IReferenceKey referenceKey)
        { return LoadCommand(connection, referenceId: referenceKey.ReferenceId); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, IReferenceKey referenceKey)
        { return LoadCommand(connection, referenceId: referenceKey.ReferenceId, includeHistory: true); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, ICatalogKey catalogKey)
        { return LoadCommand(connection, catalogId: catalogKey.CatalogId, includeHistory: true); }

        Command LoadCommand(IConnection connection, 
            Guid? modelId = null, Guid? catalogId = null, Guid? referenceId = null,
            DateTime? asOfUtcDate = null, Boolean includeHistory = false)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Reference.GetProcedure;
            command.AddParameter(AppModel.Model.ModelId, modelId);
            command.AddParameter(Catalog.CatalogId, catalogId);
            command.AddParameter(Reference.ReferenceId, referenceId);
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
        public Command SaveCommand(IConnection connection, IReferenceKey referenceKey)
        { return SaveCommand(connection, referenceId: referenceKey.ReferenceId); }

        Command SaveCommand(IConnection connection, Guid? catalogId = null, Guid? referenceId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Reference.SetProcedure;
            command.AddParameter(Catalog.CatalogId, catalogId);
            command.AddParameter(Reference.ReferenceId, referenceId);

            IEnumerable<TItem> data = this.Where(w =>
                (catalogId is null || w.CatalogId == catalogId) &&
                (referenceId is null || w.ReferenceId == referenceId));
            command.AddParameter(WriteData.Data, Reference.TableType, data);
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
        public virtual void Remove(IReferenceKeyName dependencyItem)
        {
            ReferenceKeyName key = new ReferenceKeyName(dependencyItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }

        /// <inheritdoc/>
        public virtual void Import(ICatalogKey catalogKey, IEnumerable<IReference> references)
        {
            IEnumerable<ReferenceKeyName> allKeys = this.Where(w => catalogKey.Equals(w)).
                Select(s => new ReferenceKeyName(s)).
                Union(references.Select(s => new ReferenceKeyName(s)));

            foreach (var key in allKeys)
            {
                TItem? oldValue = this.FirstOrDefault(w => key.Equals(w));
                IReference? newValue = references.FirstOrDefault(w => key.Equals(w));

                if (oldValue is ReferenceItem oldMatches && newValue is IReference newMatches)
                { oldMatches.Update(newMatches); } // Update Old
                else if (oldValue is ReferenceItem newMissing)
                { this.Remove(key); } // Delete Old
                else if (newValue is IReference oldMissing)
                { Add(ReferenceItem.Create<TItem>(catalogKey, oldMissing)); }// Add New
            }
        }
    }

}
