using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Generic Base class for Model Entity Definitions
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class EntityDefinitionCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<IEntityKey>,
        IWriteData<IModelKey>, IWriteData<IEntityKey>,
        IRemoveItem<IEntityKey>,
        ITemporalData<IModelKey>, ITemporalData<IEntityKey>
        where TItem : EntityDefinitionItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelId)
        { return LoadCommand(connection, modelId: modelId.ModelId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IEntityKey entityKey)
        { return LoadCommand(connection, entityId: entityKey.EntityId); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, IModelKey modelId)
        { return LoadCommand(connection, modelId: modelId.ModelId, includeHistory: true); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, IEntityKey entityKey)
        { return LoadCommand(connection, entityId: entityKey.EntityId, includeHistory: true); }

        Command LoadCommand(IConnection connection,
            Guid? modelId = null, Guid? entityId = null,
            DateTime? asOfUtcDate = null, Boolean includeHistory = false)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = EntityDefinition.GetProcedure;
            command.AddParameter(Model.ModelId, modelId);
            command.AddParameter(Entity.EntityId, entityId);
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
            command.CommandText = EntityDefinition.SetProcedure;
            command.AddParameter(Model.ModelId, modelId);
            command.AddParameter(Entity.EntityId, entityId);

            IEnumerable<TItem> data = this.Where(w =>
                (entityId is null || w.EntityId == entityId));
            command.AddParameter(WriteData.Data, EntityDefinition.TableType, data);
            return command;
        }

        /// <inheritdoc/>
        public virtual void Remove(IEntityKey EntityItem)
        {
            EntityKey key = new EntityKey(EntityItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
