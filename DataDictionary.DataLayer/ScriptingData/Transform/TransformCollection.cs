using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.ScriptingData.Transform
{
    /// <summary>
    /// Generic Base class for Scripting Transform Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    [Obsolete("To be removed", true)]
    public abstract class TransformCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<ITransformKey>,
        IWriteData, IWriteData<ITransformKey>,
        IRemoveItem<ITransformKey>
        where TItem : BindingTableRow, ITransformItem, ITransformKey, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ITransformKey key)
        { return LoadCommand(connection, (key.TransformId, null)); }

        Command LoadCommand(IConnection connection, (Guid? TransformId, String? TransformTitle) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetScriptingTransform]";

            command.AddParameter("@TransformId", parameters.TransformId);
            command.AddParameter("@TransformTitle", parameters.TransformTitle);
            return command;
        }


        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection, (null,null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ITransformKey key)
        { return SaveCommand(connection, (null, key.TransformId)); }


        Command SaveCommand(IConnection connection, (Guid? modelId, Guid? transformId) parameters )
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetScriptingTransform]";
            command.AddParameter("@TransformId", parameters.transformId);

            IEnumerable<TItem> data = this.Where(w => parameters.transformId is null || w.TransformId == parameters.transformId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeScriptingTransform]", data);
            return command;
        }

        /// <inheritdoc/>
        public void Remove(ITransformKey transformKey)
        {
            TransformKey key = new TransformKey(transformKey);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }

    }
}
