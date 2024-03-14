using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.ApplicationData.Transform
{
    /// <summary>
    /// Generic Base class for Transform Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    [Obsolete("To be replaced by Scripting Objects")]
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
            command.CommandText = "[App_DataDictionary].[procGetApplicationTransform]";

            command.AddParameter("@TransformId", parameters.TransformId);
            command.AddParameter("@TransformTitle", parameters.TransformTitle);
            return command;
        }


        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ITransformKey key)
        { return SaveCommand(connection, key.TransformId); }


        Command SaveCommand(IConnection connection, Guid? transformId)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetApplicationTransform]";
            command.AddParameter("@TransformId", transformId);

            IEnumerable<TItem> data = this.Where(w => transformId is null || w.TransformId == transformId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeApplicationTransform]", data);
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

    /// <summary>
    /// Default List/Collection of Transform Items.
    /// </summary>
    public class TransformCollection : TransformCollection<TransformItem>
    { }
}
