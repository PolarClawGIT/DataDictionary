using DataDictionary.DataLayer.ModelData;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Generic Base class for Database Constraint Items.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class ConstraintCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<ICatalogKey>, IReadData<IConstraintKey>,
        IWriteData, IWriteData<ICatalogKey>, IWriteData<IConstraintKey>,
        IRemoveItem<ICatalogKey>, IRemoveItem<IConstraintKeyName>,
        ITemporalData<ICatalogKey>, ITemporalData<IConstraintKey>,
        IInfomationSchemaCollection<IConstraint>
        where TItem : ConstraintItem, IConstraintItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ICatalogKey catalogKey)
        { return LoadCommand(connection, catalogId: catalogKey.CatalogId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IConstraintKey constraintKey)
        { return LoadCommand(connection, constraintId: constraintKey.ConstraintId); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, ICatalogKey catalogKey)
        { return LoadCommand(connection, catalogId: catalogKey.CatalogId, includeHistory: true); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, IConstraintKey constraintKey)
        { return LoadCommand(connection, constraintId: constraintKey.ConstraintId, includeHistory: true); }

        Command LoadCommand(IConnection connection,
            Guid? modelId = null, Guid? catalogId = null, Guid? constraintId = null,
            DateTime? asOfUtcDate = null, Boolean includeHistory = false)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Constraint.GetProcedure;
            command.CommandText = Table.GetProcedure;
            command.AddParameter(AppModel.Model.ModelId, modelId);
            command.AddParameter(Catalog.CatalogId, catalogId);
            command.AddParameter(Constraint.ConstraintId, constraintId);
            command.AddParameter(Temporal.AsOfUtcDate, asOfUtcDate);
            command.AddParameter(Temporal.IncludeHistory, includeHistory);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection, catalogId: null); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IConstraintKey constraintKey)
        { return SaveCommand(connection, constraintId: constraintKey.ConstraintId); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ICatalogKey catalogKey)
        { return SaveCommand(connection, catalogId: catalogKey.CatalogId); }

        /// <inheritdoc/>
        Command SaveCommand(IConnection connection, Guid? catalogId = null, Guid? constraintId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Constraint.SetProcedure;
            command.AddParameter(Catalog.CatalogId, catalogId);
            command.AddParameter(Constraint.ConstraintId, constraintId);

            IEnumerable<TItem> data = this.Where(w =>
                (catalogId is null || w.CatalogId == catalogId) &&
                (constraintId is null || w.ConstraintId == constraintId));
            command.AddParameter(WriteData.Data, Constraint.TableType, data);
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
        public virtual void Remove(IConstraintKeyName constraintItem)
        {
            ConstraintKeyName key = new ConstraintKeyName(constraintItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }

        /// <inheritdoc/>
        public void Import(ICatalogKey catalogKey, IEnumerable<IConstraint> source)
        {
            IEnumerable<ConstraintKeyName> allKeys = this.Where(w => catalogKey.Equals(w)).
                Select(s => new ConstraintKeyName(s)).
                Union(source.Select(s => new ConstraintKeyName(s)));

            foreach (var key in allKeys)
            {
                TItem? oldValue = this.FirstOrDefault(w => key.Equals(w));
                IConstraint? newValue = source.FirstOrDefault(w => key.Equals(w));

                if (oldValue is ConstraintItem oldMatches && newValue is IConstraint newMatches)
                { oldMatches.Update(newMatches); } // Update Old
                else if (oldValue is ConstraintItem newMissing)
                { this.Remove(key); } // Delete Old
                else if (newValue is IConstraint oldMissing)
                { Add(ConstraintItem.Create<TItem>(catalogKey, oldMissing)); }// Add New
            }
        }
    }
}
