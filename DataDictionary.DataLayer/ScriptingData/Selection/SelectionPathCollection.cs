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
    /// Generic Base class for Scripting Selection Path Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class SelectionPathCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<ISelectionKey>, IReadData<ISelectionPathKey>,
        IWriteData, IWriteData<ISelectionKey>, IWriteData<ISelectionPathKey>,
        IRemoveItem<ISelectionPathKey>
        where TItem : BindingTableRow, ISelectionPathItem, ISelectionPathKey, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ISelectionKey key)
        { return LoadCommand(connection, (key.SelectionId, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ISelectionPathKey key)
        { return LoadCommand(connection, (null, key.SelectionPathId)); }

        Command LoadCommand(IConnection connection, (Guid? SelectionId, Guid? PathId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetScriptingSelectionPath]";
            command.AddParameter("@SelectionId", parameters.SelectionId);
            command.AddParameter("@SelectionPathId", parameters.PathId);
            
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ISelectionKey key)
        { return SaveCommand(connection, (key.SelectionId, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ISelectionPathKey key)
        { return SaveCommand(connection, (null, key.SelectionPathId)); }


        Command SaveCommand(IConnection connection, (Guid? SelectionId, Guid? PathId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetScriptingSelectionPath]";
            command.AddParameter("@SelectionId", parameters.SelectionId);
            command.AddParameter("@SelectionPathId", parameters.PathId);

            IEnumerable<TItem> data = this.Where(w => 
                (parameters.SelectionId is null || w.SelectionId == parameters.SelectionId) &&
                (parameters.PathId is null || w.SelectionPathId == parameters.PathId));
            command.AddParameter("@Data", "[App_DataDictionary].[typeScriptingSelectionPath]", data);
            return command;
        }

        /// <inheritdoc/>
        public void Remove(ISelectionPathKey InstanceKey)
        {
            SelectionPathKey key = new SelectionPathKey(InstanceKey);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }

    /// <summary>
    /// Default List/Collection of Scripting Selection Path Items.
    /// </summary>
    public class SelectionPathCollection : SelectionPathCollection<SelectionPathItem>
    { }
}
