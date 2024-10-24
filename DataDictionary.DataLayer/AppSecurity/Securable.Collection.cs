// Ignore Spelling: Securable

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        { return LoadCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ISecurableKey key)
        { return LoadCommand(connection, (key.SecurableId, null)); }

        Command LoadCommand(IConnection connection, (Guid? SecurableId, String? SecurableTitle) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[AppSecurity].[procGetSecurable]";
            command.AddParameter("@SecurableId", parameters.SecurableId);
            return command;
        }
    }
}
