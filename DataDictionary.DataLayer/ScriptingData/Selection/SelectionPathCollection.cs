using DataDictionary.DataLayer.ModelData;
using System.Data;
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
        IReadData, IReadData<ISelectionKey>, IReadData<IModelKey>,
        IWriteData, IWriteData<ISelectionKey>, IWriteData<IModelKey>,
        IRemoveItem<ISelectionKey>
        where TItem : BindingTableRow, ISelectionPathItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ISelectionKey key)
        { return LoadCommand(connection, (key.SelectionId, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey key)
        { return LoadCommand(connection, (null, key.ModelId)); }

        Command LoadCommand(IConnection connection, (Guid? SelectionId, Guid? modelId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetScriptingSelectionPath]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@SelectionId", parameters.SelectionId);
            
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ISelectionKey key)
        { return SaveCommand(connection, (key.SelectionId, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey key)
        { return SaveCommand(connection, (null, key.ModelId)); }


        Command SaveCommand(IConnection connection, (Guid? SelectionId, Guid? modelId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetScriptingSelectionPath]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@SelectionId", parameters.SelectionId);

            IEnumerable<TItem> data = this.Where(w =>
                (parameters.SelectionId is null || w.SelectionId == parameters.SelectionId));
            command.AddParameter("@Data", "[App_DataDictionary].[typeScriptingSelectionPath]", data);
            return command;
        }

        /// <inheritdoc/>
        public void Remove(ISelectionKey InstanceKey)
        {
            SelectionKey key = new SelectionKey(InstanceKey);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
