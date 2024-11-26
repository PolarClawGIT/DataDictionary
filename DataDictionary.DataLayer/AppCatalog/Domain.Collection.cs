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
    /// Generic Base class for Database Domain Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class DomainCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<ICatalogKey>,
        IWriteData<IModelKey>, IWriteData<ICatalogKey>,
        IRemoveItem<ICatalogKey>, IRemoveItem<IDomainKeyName>
        where TItem : DomainItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ICatalogKey catalogKey)
        { return LoadCommand(connection, catalogId: catalogKey.CatalogId); }

        Command LoadCommand(IConnection connection, Guid? modelId = null, Guid? catalogId = null, Guid? domainId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Domain.GetProcedure;
            command.AddParameter(Model.ModelId, modelId);
            command.AddParameter(Catalog.CatalogId, catalogId);
            command.AddParameter(Domain.DomainId, domainId);

            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelId)
        { return SaveCommand(connection, modelId: modelId.ModelId); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ICatalogKey catalogKey)
        { return SaveCommand(connection, catalogId: catalogKey.CatalogId); }

        Command SaveCommand(IConnection connection, Guid? modelId = null, Guid? catalogId = null, Guid? domainId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Domain.SetProcedure;
            command.AddParameter(Model.ModelId, modelId);
            command.AddParameter(Catalog.CatalogId, catalogId);
            command.AddParameter(Domain.DomainId, domainId);

            IEnumerable<TItem> data = this.Where(w => catalogId is null || w.CatalogId == catalogId);
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
                { // Update Old
                    oldMatches.CharacterMaximumLength = newMatches.CharacterMaximumLength;
                    oldMatches.CharacterOctetLength = newMatches.CharacterMaximumLength;
                    oldMatches.CharacterSetCatalog = newMatches.CharacterSetCatalog;
                    oldMatches.CharacterSetName = newMatches.CharacterSetName;
                    oldMatches.CharacterSetSchema = newMatches.CharacterSetSchema;
                    oldMatches.CollationCatalog = newMatches.CharacterSetCatalog;
                    oldMatches.CollationName = newMatches.CollationName;
                    oldMatches.CollationSchema = newMatches.CollationSchema;
                    oldMatches.DataType = newMatches.DataType;
                    oldMatches.DateTimePrecision = newMatches.DateTimePrecision;
                    oldMatches.DomainDefault = newMatches.DomainDefault;
                    oldMatches.NumericPrecision = newMatches.NumericPrecision;
                    oldMatches.NumericPrecisionRadix = newMatches.NumericPrecisionRadix;
                    oldMatches.NumericScale = newMatches.NumericScale;
                }
                else if (oldValue is DomainItem newMissing)
                { this.Remove(key); } // Delete Old
                else if (newValue is IDomain oldMissing)
                { Add(DomainItem.Create<TItem>(catalogKey, oldMissing)); }// Add New
            }
        }
    }
}
