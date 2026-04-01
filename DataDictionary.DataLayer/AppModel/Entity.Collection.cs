// Ignore Spelling: Utc

using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Generic Base class for Model Entity's
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class EntityCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<IEntityKey>,
        IWriteData<IModelKey>, IWriteData<IEntityKey>,
        IRemoveItem<IEntityKey>,
        IReadTemporal<IModelKey>, IReadTemporal<IEntityKey>
        where TItem : EntityItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelId)
        { return LoadCommand(connection, modelId: modelId.ModelId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey, ITemporalKey asOfUtcDate)
        { return LoadCommand(connection, modelId: modelKey.ModelId, asOfUtcDate: asOfUtcDate.AsOfUtcDate); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IEntityKey entityKey)
        { return LoadCommand(connection, entityId: entityKey.EntityId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IEntityKey entityKey, ITemporalKey asOfUtcDate)
        { return LoadCommand(connection, entityId: entityKey.EntityId, asOfUtcDate: asOfUtcDate.AsOfUtcDate); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, IModelKey modelId)
        { return LoadCommand(connection, modelId: modelId.ModelId, includeHistory: true); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, IEntityKey key)
        { return LoadCommand(connection, entityId: key.EntityId, includeHistory: true); }

        Command LoadCommand(IConnection connection,
            Guid? modelId = null, Guid? entityId = null,
            DateTime? asOfUtcDate = null, Boolean includeHistory = false)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Entity.GetProcedure;
            command.AddParameter(Model.Identifier, modelId);
            command.AddParameter(Entity.Identifier, entityId);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelId)
        { return SaveCommand(connection, modelId: modelId.ModelId); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IEntityKey entityKey)
        { return SaveCommand(connection, entityId: entityKey.EntityId); }

        Command SaveCommand(IConnection connection, Guid? modelId = null, Guid? entityId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Entity.SetProcedure;
            command.AddParameter(Model.Identifier, modelId);
            command.AddParameter(Entity.Identifier, entityId);

            IEnumerable<TItem> data = this.Where(w =>
                (entityId is null || w.EntityId == entityId));
            command.AddParameter(WriteData.Data, Entity.TableType, data);
            return command;
        }

        /// <inheritdoc/>
        public virtual void Remove(IEntityKey entityItem)
        {
            EntityKey key = new EntityKey(entityItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
