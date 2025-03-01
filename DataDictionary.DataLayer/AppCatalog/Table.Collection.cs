// Ignore Spelling: Utc

using DataDictionary.DataLayer.AppModel;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Generic Base class for Database Tables
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class TableCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<ICatalogKey>, IReadData<ITableKey>,
        IWriteData, IWriteData<ICatalogKey>, IWriteData<ITableKey>,
        IRemoveItem<ICatalogKey>, IRemoveItem<ISchemaKeyName>, IRemoveItem<ITableKeyName>,
        IReadTemporal<ICatalogKey>, IInfomationSchemaCollection<ITable>
        where TItem : TableItem, ITableItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey, ITemporalKey asOfUtcDate)
        { return LoadCommand(connection, modelId: modelKey.ModelId, asOfUtcDate: asOfUtcDate.AsOfUtcDate); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ICatalogKey catalogKey)
        { return LoadCommand(connection, catalogId: catalogKey.CatalogId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ICatalogKey catalogKey, ITemporalKey asOfUtcDate)
        { return LoadCommand(connection, catalogId: catalogKey.CatalogId, asOfUtcDate: asOfUtcDate.AsOfUtcDate); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ITableKey tableKey)
        { return LoadCommand(connection, tableId: tableKey.TableId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ITableKey tableKey, ITemporalKey asOfUtcDate)
        { return LoadCommand(connection, tableId: tableKey.TableId, asOfUtcDate: asOfUtcDate.AsOfUtcDate); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, ICatalogKey catalogKey)
        { return LoadCommand(connection, catalogId: catalogKey.CatalogId, includeHistory: true); }

        Command LoadCommand(IConnection connection,
            Guid? modelId = null, Guid? catalogId = null, Guid? tableId = null,
            DateTime? asOfUtcDate = null, Boolean includeHistory = false)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Table.GetProcedure;
            command.AddParameter(AppModel.Model.ModelId, modelId);
            command.AddParameter(Catalog.CatalogId, catalogId);
            command.AddParameter(Table.TableId, tableId);
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
        public Command SaveCommand(IConnection connection, ITableKey tableKey)
        { return SaveCommand(connection, tableId: tableKey.TableId); }

        Command SaveCommand(IConnection connection, Guid? catalogId = null, Guid? tableId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Table.SetProcedure;
            command.AddParameter(Catalog.CatalogId, catalogId);
            command.AddParameter(Table.TableId, tableId);

            IEnumerable<TItem> data = this.Where(w =>
                (catalogId is null || w.CatalogId == catalogId) &&
                (tableId is null || w.TableId == tableId));
            command.AddParameter(WriteData.Data, Table.TableType, data);
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

        /// <inheritdoc/>
        public virtual void Remove(ITableKeyName tableItem)
        {
            TableKeyName key = new TableKeyName(tableItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }

        /// <inheritdoc/>
        public virtual void Import(ICatalogKey catalogKey, IEnumerable<ITable> tables)
        {
            IEnumerable<TableKeyName> allKeys = this.Where(w => catalogKey.Equals(w)).
                Select(s => new TableKeyName(s)).
                Union(tables.Select(s => new TableKeyName(s))).ToList();

            foreach (var key in allKeys)
            {
                TItem? oldValue = this.FirstOrDefault(w => key.Equals(w));
                ITable? newValue = tables.FirstOrDefault(w => key.Equals(w));

                if (oldValue is TableItem oldMatches && newValue is ITable newMatches)
                { oldMatches.Update(newMatches); } // Update Old
                else if (oldValue is TableItem newMissing)
                { this.Remove(key); } // Delete Old
                else if (newValue is ITable oldMissing)
                { Add(TableItem.Create<TItem>(catalogKey, oldMissing)); }// Add New
            }
        }
    }
}
