using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.ScriptingData.Selection
{
    /// <summary>
    /// Generic Base class for Scripting Selection Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    [Obsolete("To be removed", true)]
    public abstract class SelectionCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<ISelectionKey>,
        IWriteData, IWriteData<ISelectionKey>,
        IRemoveItem<ISelectionKey>
        where TItem : BindingTableRow, ISelectionItem, ISelectionKey, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ISelectionKey key)
        { return LoadCommand(connection, (key.SelectionId, null)); }

        Command LoadCommand(IConnection connection, (Guid? SelectionId, String? SelectionTitle) parameters)
        {
            //TODO: Stored procedure does not exist. Need to look at design.

            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetScriptingSelection]";

            command.AddParameter("@SelectionId", parameters.SelectionId);
            command.AddParameter("@SelectionTitle", parameters.SelectionTitle);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ISelectionKey key)
        { return SaveCommand(connection, (null, key.SelectionId)); }


        Command SaveCommand(IConnection connection, (Guid? modelId, Guid? SelectionId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetScriptingSelection]";
            command.AddParameter("@SelectionId", parameters.SelectionId);

            IEnumerable<TItem> data = this.Where(w => parameters.SelectionId is null || w.SelectionId == parameters.SelectionId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeScriptingSelection]", data);
            return command;
        }

        /// <inheritdoc/>
        public void Remove(ISelectionKey SelectionKey)
        {
            SelectionKey key = new SelectionKey(SelectionKey);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
