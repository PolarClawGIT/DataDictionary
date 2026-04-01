// Ignore Spelling: Utc

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Generic Base class for Model Process's
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class ProcessCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<IProcessKey>,
        IWriteData<IModelKey>, IWriteData<IProcessKey>,
        IRemoveItem<IProcessKey>,
        IReadTemporal<IModelKey>, IReadTemporal<IProcessKey>
        where TItem : ProcessItem, new()
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

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, IProcessKey key)
        { return LoadCommand(connection, ProcessId: key.ProcessId, includeHistory: true); }

        Command LoadCommand(IConnection connection,
            Guid? modelId = null, Guid? ProcessId = null,
            DateTime? asOfUtcDate = null, Boolean includeHistory = false)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Process.GetProcedure;
            command.AddParameter(Model.Identifier, modelId);
            command.AddParameter(Process.Identifier, ProcessId);
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
            command.CommandText = Process.SetProcedure;
            command.AddParameter(Model.Identifier, modelId);
            command.AddParameter(Process.Identifier, ProcessId);

            IEnumerable<TItem> data = this.Where(w =>
                (ProcessId is null || w.ProcessId == ProcessId));
            command.AddParameter(WriteData.Data, Process.TableType, data);
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
