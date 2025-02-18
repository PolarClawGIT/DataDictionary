// Ignore Spelling: Utc

using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Generic Base class for Model Definitions
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class DefinitionCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<IModelKey>, IReadData<IDefinitionKey>,
        IWriteData<IModelKey>, IWriteData<IDefinitionKey>,
        IDeleteData<IDefinitionKey>,
        IRemoveItem<IDefinitionKey>,
        IReadTemporal<IModelKey>, IReadTemporal<IDefinitionKey>
        where TItem : DefinitionItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, modelId : null); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId : modelKey.ModelId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey, ITemporalKey asOfUtcDate)
        { return LoadCommand(connection, modelId: modelKey.ModelId, asOfUtcDate: asOfUtcDate.AsOfUtcDate); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IDefinitionKey definitionKey)
        { return LoadCommand(connection, definitionId : definitionKey.DefinitionId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IDefinitionKey definitionKey, ITemporalKey asOfUtcDate)
        { return LoadCommand(connection, definitionId: definitionKey.DefinitionId, asOfUtcDate: asOfUtcDate.AsOfUtcDate); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId, includeHistory: true); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, IDefinitionKey definitionKey)
        { return LoadCommand(connection, definitionId: definitionKey.DefinitionId, includeHistory: true); }

        Command LoadCommand(IConnection connection,
            Guid? modelId = null, Guid? definitionId = null,
            DateTime? asOfUtcDate = null, Boolean includeHistory = false) 
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Definition.GetProcedure;
            command.AddParameter(Model.ModelId, modelId);
            command.AddParameter(Definition.DefinitionId, definitionId);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelKey)
        { return SaveCommand(connection, modelId: modelKey.ModelId); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IDefinitionKey definitionKey)
        { return SaveCommand(connection, definitionId : definitionKey.DefinitionId); }

        Command SaveCommand(IConnection connection, Guid? modelId = null, Guid? definitionId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Definition.SetProcedure;
            command.AddParameter(Model.ModelId, modelId);
            command.AddParameter(Definition.DefinitionId, definitionId);

            IEnumerable<TItem> data = this.Where(w =>
                (definitionId is null || w.DefinitionId == definitionId));
            command.AddParameter(WriteData.Data, Definition.TableType, data);
            return command;
        }

        /// <inheritdoc/>
        [Obsolete("Do not think this is needed")]
        public Command DeleteCommand(IConnection connection, IDefinitionKey parameters)
        { return DeleteCommand(connection, (parameters.DefinitionId, null)); }

        [Obsolete("Do not think this is needed")]
        Command DeleteCommand(IConnection connection, (Guid? DefinitionId, String? dummy) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDomainDefinition]";
            command.AddParameter("@DefinitionId", parameters.DefinitionId);

            return command;
        }

        /// <inheritdoc/>
        public virtual void Remove(IDefinitionKey DefinitionItem)
        {
            DefinitionKey key = new DefinitionKey(DefinitionItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
