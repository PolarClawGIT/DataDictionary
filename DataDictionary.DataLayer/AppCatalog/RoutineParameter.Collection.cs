// Ignore Spelling: Utc

using DataDictionary.DataLayer.AppModel;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Generic Base class for Database Routine Parameter Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class RoutineParameterCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<ICatalogKey>, IReadData<IRoutineKey>,
        IWriteData, IWriteData<ICatalogKey>, IWriteData<IRoutineKey>,
        IRemoveItem<ICatalogKey>, IRemoveItem<ISchemaKeyName>, IRemoveItem<IRoutineKeyName>, IRemoveItem<IRoutineParameterKeyName>,
        ITemporalData<ICatalogKey>, IInfomationSchemaCollection<IRoutineParameter>
        where TItem : RoutineParameterItem, IRoutineParameterItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId); }
        
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey, DateTime asOfUtcDate)
        { return LoadCommand(connection, modelId: modelKey.ModelId, asOfUtcDate: asOfUtcDate); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ICatalogKey catalogKey)
        { return LoadCommand(connection, catalogId:catalogKey.CatalogId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ICatalogKey catalogKey, DateTime asOfUtcDate)
        { return LoadCommand(connection, catalogId: catalogKey.CatalogId, asOfUtcDate: asOfUtcDate); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IRoutineKey routineKey)
        { return LoadCommand(connection, routineId: routineKey.RoutineId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IRoutineKey routineKey, DateTime asOfUtcDate)
        { return LoadCommand(connection, routineId: routineKey.RoutineId, asOfUtcDate: asOfUtcDate); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, ICatalogKey catalogKey)
        { return LoadCommand(connection, catalogId: catalogKey.CatalogId, includeHistory: true); }

        Command LoadCommand(IConnection connection, 
            Guid? modelId = null, Guid? catalogId = null, Guid? routineId = null,
            DateTime? asOfUtcDate = null, Boolean includeHistory = false)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = RoutineParameter.GetProcedure;
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
        public Command SaveCommand(IConnection connection, IRoutineKey routineKey)
        { return SaveCommand(connection, routineId: routineKey.RoutineId); }

        /// <inheritdoc/>
        Command SaveCommand(IConnection connection, Guid? catalogId = null, Guid? routineId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = RoutineParameter.SetProcedure;
            command.AddParameter(Catalog.CatalogId, catalogId);
            command.AddParameter(Routine.RoutineId, routineId);

            IEnumerable<TItem> data = this.Where(w =>
                (catalogId is null || w.CatalogId == catalogId));
            command.AddParameter(WriteData.Data, RoutineParameter.TableType, data);
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
        public virtual void Remove(IRoutineKeyName routineItem)
        {
            RoutineKeyName key = new RoutineKeyName(routineItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }

        /// <inheritdoc/>
        public virtual void Remove(IRoutineParameterKeyName parameterItem)
        {
            RoutineParameterKeyName key = new RoutineParameterKeyName(parameterItem);

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
        public virtual void Import(ICatalogKey catalogKey, IEnumerable<IRoutineParameter> routines)
        {
            IEnumerable<RoutineParameterKeyName> allKeys = this.Where(w => catalogKey.Equals(w)).
                Select(s => new RoutineParameterKeyName(s)).
                Union(routines.Select(s => new RoutineParameterKeyName(s))).ToList();

            foreach (var key in allKeys)
            {
                TItem? oldValue = this.FirstOrDefault(w => key.Equals(w));
                IRoutineParameter? newValue = routines.FirstOrDefault(w => key.Equals(w));

                if (oldValue is RoutineParameterItem oldMatches && newValue is IRoutineParameter newMatches)
                { oldMatches.Update(newMatches); } // Update Old
                else if (oldValue is RoutineParameterItem newMissing)
                { this.Remove(key); } // Delete Old
                else if (newValue is IRoutineParameter oldMissing)
                { Add(RoutineParameterItem.Create<TItem>(catalogKey, oldMissing)); }// Add New
            }
        }
    }
}
