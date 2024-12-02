using DataDictionary.DataLayer.AppModel;
using DataDictionary.DataLayer.ModelData;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Generic Base class for Database Extended Property Items.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class PropertyCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<ICatalogKey>, IReadData<IPropertyKey>,
        IWriteData, IWriteData<ICatalogKey>, IWriteData<IPropertyKey>,
        IRemoveItem<ICatalogKey>, IRemoveItem<IPropertyKeyName>,
        ITemporalData<ICatalogKey>, ITemporalData<IPropertyKey>, IInfomationSchemaCollection<IProperty>
        where TItem : PropertyItem, IPropertyItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, catalogId: null); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ICatalogKey catalogKey)
        { return LoadCommand(connection, catalogId: catalogKey.CatalogId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IPropertyKey propertyKey)
        { return LoadCommand(connection, propertyId: propertyKey.PropertyId); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, ICatalogKey catalogKey)
        { return LoadCommand(connection, catalogId: catalogKey.CatalogId, includeHistory: true); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, IPropertyKey propertyKey)
        { return LoadCommand(connection, propertyId: propertyKey.PropertyId, includeHistory: true); }


        Command LoadCommand(IConnection connection,
            Guid? modelId = null, Guid? catalogId = null, Guid? propertyId = null,
            DateTime? asOfUtcDate = null, Boolean includeHistory = false)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Property.GetProcedure;
            command.AddParameter(Model.ModelId, modelId);
            command.AddParameter(Catalog.CatalogId, catalogId);
            command.AddParameter(Property.PropertyId, propertyId);
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
        public Command SaveCommand(IConnection connection, IPropertyKey propertyKey)
        { return SaveCommand(connection, propertyId: propertyKey.PropertyId); }

        /// <inheritdoc/>
        Command SaveCommand(IConnection connection, Guid? catalogId = null, Guid? propertyId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Property.SetProcedure;
            command.AddParameter(Catalog.CatalogId, catalogId);

            IEnumerable<TItem> data = this.Where(w =>
                (catalogId is null || w.CatalogId == catalogId) &&
                (propertyId is null || w.PropertyId == propertyId));
            command.AddParameter(WriteData.Data, Property.TableType, this);
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
        public virtual void Remove(IPropertyKeyName propertyKey)
        {
            PropertyKeyName key = new PropertyKeyName(propertyKey);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }

        /// <inheritdoc/>
        public virtual void Import(ICatalogKey catalogKey, IEnumerable<IProperty> properties)
        {
            IEnumerable<PropertyKeyName> allKeys = this.Where(w => catalogKey.Equals(w)).
                Select(s => new PropertyKeyName(s)).
                Union(properties.Select(s => new PropertyKeyName(s)));

            foreach (var key in allKeys)
            {
                TItem? oldValue = this.FirstOrDefault(w => key.Equals(w));
                IProperty? newValue = properties.FirstOrDefault(w => key.Equals(w));

                if (oldValue is PropertyItem oldMatches && newValue is IProperty newMatches)
                { oldMatches.Update(newMatches); } // Update Old
                else if (oldValue is PropertyItem newMissing)
                { this.Remove(key); } // Delete Old
                else if (newValue is IProperty oldMissing)
                { Add(PropertyItem.Create<TItem>(catalogKey, oldMissing)); }// Add New
            }
        }
    }
}
