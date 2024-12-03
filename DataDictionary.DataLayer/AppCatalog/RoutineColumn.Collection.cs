using DataDictionary.DataLayer.ModelData;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Generic Base class for Database Routine Columns
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class RoutineColumnCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<ICatalogKey>, IReadData<IRoutineKey>,
        IWriteData, IWriteData<ICatalogKey>, IWriteData<IRoutineKey>,
        IRemoveItem<ICatalogKey>, IRemoveItem<ISchemaKeyName>, IRemoveItem<IRoutineKeyName>, IRemoveItem<IRoutineColumnKeyName>,
        ITemporalData<ICatalogKey>, ITemporalData<IRoutineKey>, IInfomationSchemaCollection<IRoutineColumn>
        where TItem : RoutineColumnItem, IRoutineColumn, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ICatalogKey catalogKey)
        { return LoadCommand(connection, catalogId: catalogKey.CatalogId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IRoutineKey RoutineKey)
        { return LoadCommand(connection, routineId: RoutineKey.RoutineId); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, ICatalogKey catalogKey)
        { return LoadCommand(connection, catalogId: catalogKey.CatalogId, includeHistory: true); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, IRoutineKey RoutineKey)
        { return LoadCommand(connection, routineId: RoutineKey.RoutineId, includeHistory: true); }

        Command LoadCommand(IConnection connection,
            Guid? modelId = null, Guid? catalogId = null, Guid? routineId = null,
            DateTime? asOfUtcDate = null, Boolean includeHistory = false)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = RoutineColumn.GetProcedure;
            command.AddParameter(AppModel.Model.ModelId, modelId);
            command.AddParameter(Catalog.CatalogId, catalogId);
            command.AddParameter(Routine.RoutineId, routineId);
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
        public Command SaveCommand(IConnection connection, IRoutineKey RoutineKey)
        { return SaveCommand(connection, routineId: RoutineKey.RoutineId); }

        Command SaveCommand(IConnection connection, Guid? catalogId = null, Guid? routineId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = RoutineColumn.SetProcedure;

            command.AddParameter(Catalog.CatalogId, catalogId);
            command.AddParameter(Routine.RoutineId, routineId);

            IEnumerable<TItem> data = this.Where(w =>
                (catalogId is null || w.CatalogId == catalogId));
            command.AddParameter(WriteData.Data, RoutineColumn.TSql_InformationSchema, data);
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
        public virtual void Remove(IRoutineKeyName tableItem)
        {
            RoutineKeyName key = new RoutineKeyName(tableItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }

        /// <inheritdoc/>
        public virtual void Remove(IRoutineColumnKeyName columnItem)
        {
            RoutineColumnKeyName key = new RoutineColumnKeyName(columnItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }

        /// <inheritdoc/>
        public virtual void Import(ICatalogKey catalogKey, IEnumerable<IRoutineColumn> columns)
        {
            IEnumerable<RoutineColumnKeyName> allKeys = this.Where(w => catalogKey.Equals(w)).
                Select(s => new RoutineColumnKeyName(s)).
                Union(columns.Select(s => new RoutineColumnKeyName(s)));

            foreach (var key in allKeys)
            {
                TItem? oldValue = this.FirstOrDefault(w => key.Equals(w));
                IRoutineColumn? newValue = columns.FirstOrDefault(w => key.Equals(w));

                if (oldValue is RoutineColumnItem oldMatches && newValue is IRoutineColumn newMatches)
                { oldMatches.Update(newMatches); } // Update Old
                else if (oldValue is RoutineColumnItem newMissing)
                { this.Remove(key); } // Delete Old
                else if (newValue is IRoutineColumn oldMissing)
                { Add(RoutineColumnItem.Create<TItem>(catalogKey, oldMissing)); }// Add New
            }
        }
    }
}
