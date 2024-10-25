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
    /// Generic Base class for Authorization Securable Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class AuthorizationSecurableCollection<TItem> : BindingTable<TItem>, IReadData
        where TItem : BindingTableRow, IAuthorizationSecurableItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[AppSecurity].[procGetAuthorizationSecurable]";
            return command;
        }
    }

}
