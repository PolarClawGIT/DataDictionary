// Ignore Spelling: Utc

using DataDictionary.DataLayer.AppModel;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Generic Base class for Database Table Columns
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class TableColumnCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<ICatalogKey>, IReadData<ITableKey>,
        IWriteData, IWriteData<ICatalogKey>, IWriteData<ITableKey>,
        IRemoveItem<ICatalogKey>, IRemoveItem<ISchemaKeyName>, IRemoveItem<ITableKeyName>, IRemoveItem<ITableColumnKeyName>,
        ITemporalData<ICatalogKey>, ITemporalData<ITableKey>, IInfomationSchemaCollection<ITableColumn>
        where TItem : TableColumnItem, ITableColumn, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey, DateTime asOfUtcDate)
        { return LoadCommand(connection, modelId: modelKey.ModelId, asOfUtcDate: asOfUtcDate); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ICatalogKey catalogKey)
        { return LoadCommand(connection, catalogId: catalogKey.CatalogId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ICatalogKey catalogKey, DateTime asOfUtcDate)
        { return LoadCommand(connection, catalogId: catalogKey.CatalogId, asOfUtcDate: asOfUtcDate); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ITableKey tableKey)
        { return LoadCommand(connection, tableId: tableKey.TableId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ITableKey tableKey, DateTime asOfUtcDate)
        { return LoadCommand(connection, tableId: tableKey.TableId, asOfUtcDate: asOfUtcDate); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, ICatalogKey catalogKey)
        { return LoadCommand(connection, catalogId: catalogKey.CatalogId, includeHistory: true); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, ITableKey tableKey)
        { return LoadCommand(connection, tableId: tableKey.TableId, includeHistory: true); }

        Command LoadCommand(IConnection connection,
            Guid? modelId = null, Guid? catalogId = null, Guid? tableId = null,
            DateTime? asOfUtcDate = null, Boolean includeHistory = false)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = TableColumn.GetProcedure;
            command.AddParameter(AppModel.Model.ModelId, modelId);
            command.AddParameter(Catalog.CatalogId, catalogId);
            command.AddParameter(Table.TableId, tableId);
            command.AddParameter(Temporal.AsOfUtcDate, asOfUtcDate);
            command.AddParameter(Temporal.IncludeHistory, includeHistory);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection, catalogId : null); }

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
            command.CommandText = TableColumn.SetProcedure;

            command.AddParameter(Catalog.CatalogId, catalogId);
            command.AddParameter(Table.TableId, tableId);

            IEnumerable<TItem> data = this.Where(w =>
                (catalogId is null || w.CatalogId == catalogId));
            command.AddParameter(WriteData.Data, TableColumn.TableType, data);
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
        public virtual void Remove(ITableColumnKeyName columnItem)
        {
            TableColumnKeyName key = new TableColumnKeyName(columnItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }

        /// <inheritdoc/>
        public virtual void Import(ICatalogKey catalogKey, IEnumerable<ITableColumn> columns)
        {
            IEnumerable<TableColumnKeyName> allKeys = this.Where(w => catalogKey.Equals(w)).
                Select(s => new TableColumnKeyName(s)).
                Union(columns.Select(s => new TableColumnKeyName(s))).ToList();

            foreach (var key in allKeys)
            {
                TItem? oldValue = this.FirstOrDefault(w => key.Equals(w));
                ITableColumn? newValue = columns.FirstOrDefault(w => key.Equals(w));

                if (oldValue is TableColumnItem oldMatches && newValue is ITableColumn newMatches)
                { oldMatches.Update(newMatches); } // Update Old
                else if (oldValue is TableColumnItem newMissing)
                { this.Remove(key); } // Delete Old
                else if (newValue is ITableColumn oldMissing)
                { Add(TableColumnItem.Create<TItem>(catalogKey, oldMissing)); }// Add New
            }
        }
    }
}
