using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>
    /// Generic Base class for Authorization Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class AuthorizationCollection<TItem> : BindingTable<TItem>, IReadData
        where TItem : BindingTableRow, IAuthorizationItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[AppSecurity].[procGetAuthorization]";
            return command;
        }
    }
}
