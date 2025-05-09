// Ignore Spelling: Utc

using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Generic Base class for Model Process Argument
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public abstract class ProcessArgumentCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<IProcessKey>,
        IWriteData<IModelKey>, IWriteData<IProcessKey>,
        IRemoveItem<IProcessKey>,
        IReadTemporal<IModelKey>
        where TItem : ProcessArgumentItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelId)
        { return LoadCommand(connection, modelId: modelId.ModelId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey, ITemporalKey asOfUtcDate)
        { return LoadCommand(connection, modelId: modelKey.ModelId, asOfUtcDate: asOfUtcDate.AsOfUtcDate); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IProcessKey ProcessKey)
        { return LoadCommand(connection, ProcessId: ProcessKey.ProcessId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IProcessKey ProcessKey, ITemporalKey asOfUtcDate)
        { return LoadCommand(connection, ProcessId: ProcessKey.ProcessId, asOfUtcDate: asOfUtcDate.AsOfUtcDate); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, IModelKey modelId)
        { return LoadCommand(connection, modelId: modelId.ModelId, includeHistory: true); }

        Command LoadCommand(IConnection connection,
            Guid? modelId = null, Guid? ProcessId = null,
            DateTime? asOfUtcDate = null, Boolean includeHistory = false)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = ProcessArgument.GetProcedure;
            command.AddParameter(Model.ModelId, modelId);
            command.AddParameter(Process.ProcessId, ProcessId);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelId)
        { return SaveCommand(connection, modelId: modelId.ModelId); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IProcessKey ProcessKey)
        { return SaveCommand(connection, ProcessId: ProcessKey.ProcessId); }

        Command SaveCommand(IConnection connection, Guid? modelId = null, Guid? ProcessId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = ProcessArgument.SetProcedure;
            command.AddParameter(Model.ModelId, modelId);
            command.AddParameter(Process.ProcessId, ProcessId);

            IEnumerable<TItem> data = this.Where(w =>
                (ProcessId is null || w.ProcessId == ProcessId));
            command.AddParameter(WriteData.Data, ProcessArgument.TableType, data);
            return command;
        }

        /// <inheritdoc/>
        public virtual void Remove(IProcessKey ProcessItem)
        {
            ProcessKey key = new ProcessKey(ProcessItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
