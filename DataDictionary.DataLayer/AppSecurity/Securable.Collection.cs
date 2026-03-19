// Ignore Spelling: Securable

using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>
    /// Generic Base class for Securable (Security Object) items.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public class SecurableCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<ISecurableKey>
        where TItem : BindingTableRow, ISecurableItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, securableId: null); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ISecurableKey key)
        { return LoadCommand(connection, securableId: key.SecurableId); }

        /// <inheritdoc/>
        Command IReadData<ISecurableKey>.LoadCommand(IConnection connection, ISecurableKey key, ITemporalKey asOfUtcDate)
        { throw new NotSupportedException(); }

        Command LoadCommand(IConnection connection, Guid? securableId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Securable.GetProcedure;
            command.AddParameter(Securable.Identifier, securableId);
            return command;
        }
    }
}
