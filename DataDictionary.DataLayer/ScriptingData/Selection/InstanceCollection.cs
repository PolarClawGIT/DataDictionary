using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.ScriptingData.Selection
{
    /// <summary>
    /// Generic Base class for Scripting Instance Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class InstanceCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<ISelectionKey>, IReadData<IInstanceKey>,
        IWriteData, IWriteData<ISelectionKey>, IWriteData<IInstanceKey>,
        IRemoveItem<IInstanceKey>
        where TItem : BindingTableRow, IInstanceItem, IInstanceKey, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ISelectionKey key)
        { return LoadCommand(connection, (key.SelectionId, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IInstanceKey key)
        { return LoadCommand(connection, (null, key.InstanceId)); }

        Command LoadCommand(IConnection connection, (Guid? SelectionId, Guid? InstanceId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetScriptingSelectionInstance]";
            command.AddParameter("@SelectionId", parameters.SelectionId);
            command.AddParameter("@InstanceId", parameters.InstanceId);
            
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ISelectionKey key)
        { return SaveCommand(connection, (key.SelectionId, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IInstanceKey key)
        { return SaveCommand(connection, (null, key.InstanceId)); }


        Command SaveCommand(IConnection connection, (Guid? SelectionId, Guid? InstanceId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetScriptingSelectionInstance]";
            command.AddParameter("@SelectionId", parameters.SelectionId);
            command.AddParameter("@InstanceId", parameters.InstanceId);

            IEnumerable<TItem> data = this.Where(w => parameters.InstanceId is null || w.InstanceId == parameters.InstanceId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeScriptingSelectionInstance]", data);
            return command;
        }

        /// <inheritdoc/>
        public void Remove(IInstanceKey InstanceKey)
        {
            InstanceKey key = new InstanceKey(InstanceKey);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }

    /// <summary>
    /// Default List/Collection of Scripting Instance Items.
    /// </summary>
    public class InstanceCollection : InstanceCollection<InstanceItem>
    { }
}
